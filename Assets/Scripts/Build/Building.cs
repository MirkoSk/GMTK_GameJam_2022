using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	#region Variable Declarations
	[SerializeField] Sprite shape;
	[SerializeField] List<GameObject> goldImages = new List<GameObject>();
	[SerializeField] List<GameObject> invalidImages = new List<GameObject>();
	[SerializeField] LayerMask cellCheckLayerMask;
	[SerializeField] LayerMask collisionLayerMask;

	DieColor dieColor;
	int productionValue;
	District currentDistrict;
	List<Collider> collisions = new List<Collider>();
	bool buildingPlaced;
	Coroutine rotateCoroutine;
	#endregion



	#region Public Properties
	public Sprite Shape { get => shape; }
	public DieColor DieColor { get => dieColor; }
	public List<Collider> Collisions { get => collisions; }
    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
		GameEvents.OnBuildingPlacementChanged += UpdateDistrict;
		GameEvents.OnActionCompleted += PlaceBuilding;
    }

	private void OnDisable()
	{
		GameEvents.OnBuildingPlacementChanged -= UpdateDistrict;
		GameEvents.OnActionCompleted -= PlaceBuilding;
	}

	private void Update()
	{
		if (buildingPlaced) return;

		if (Input.GetMouseButtonDown(1) && rotateCoroutine == null)
		{
			rotateCoroutine = StartCoroutine(RotateBuilding());
		}
	}
	#endregion



	#region Public Functions
	public void Initialize(DieColor dieColor, int productionValue)
    {
		this.dieColor = dieColor;
		this.productionValue = productionValue;
		Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
			renderers[i].material.color = dieColor.Color;
        }
    }

	public void IndicateValidPlacement(bool placeable)
	{
		if (placeable)
        {
			goldImages.ForEach((image) => { image.SetActive(true); });
			invalidImages.ForEach((image) => { image.SetActive(false); });
		}
		else
        {
			goldImages.ForEach((image) => { image.SetActive(false); });
			invalidImages.ForEach((image) => { image.SetActive(true); });
		}
	}
	#endregion



	#region Private Functions
	void UpdateDistrict(bool validPlacement)
    {
		if (validPlacement)
		{
			RaycastHit hit;
			Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, cellCheckLayerMask);
			int districtID = hit.transform.GetComponentInParent<Cell>().DistrictID;
			if (districtID != 0)
            {
				if (currentDistrict == null)
                {
					currentDistrict = new District(districtID, dieColor);
				}
				else
				{
					currentDistrict.DieColor = dieColor;
					currentDistrict.ID = districtID;
				}
			}
		}
		else
        {
			currentDistrict = null;
        }
	}

	void PlaceBuilding(Die die, Action action, bool success)
    {
		if (buildingPlaced) return;

		if (!success)
        {
			Destroy(gameObject);
			return;
		}

		District gmDistrict = GameManager.Instance.Districts.Find(x => x.ID == currentDistrict.ID);
		if (gmDistrict.DieColor == null) gmDistrict.DieColor = dieColor;
		gmDistrict.ProductionValue += productionValue;
		buildingPlaced = true;

		GameEvents.OnBuildingPlacementChanged -= UpdateDistrict;
    }
	#endregion



	#region Coroutines
	IEnumerator RotateBuilding()
	{
		yield return new WaitForSeconds(0.1f);
		
		transform.Rotate(Vector3.up, 90);

		yield return new WaitForSeconds(0.1f);

		bool validPlacement = true;
		BuildingDragger[] draggers = GetComponentsInChildren<BuildingDragger>();
        for (int i = 0; i < draggers.Length; i++)
        {
			BoxCollider collider = draggers[i].GetComponent<BoxCollider>();
			Vector3 colliderSize = new Vector3(collider.transform.localScale.x * collider.size.x, collider.transform.localScale.y * collider.size.y, collider.transform.localScale.z * collider.size.z);

			//Debug.DrawLine(collider.transform.position + collider.center, collider.transform.position + collider.center + collider.transform.right * colliderSize.x * 0.5f, Color.cyan, 3f);
			//Debug.DrawLine(collider.transform.position + collider.center, collider.transform.position + collider.center + collider.transform.forward * colliderSize.z * 0.5f, Color.cyan, 3f);

			if (Physics.CheckBox(collider.transform.position + collider.center, colliderSize * 0.5f, collider.transform.rotation, collisionLayerMask, QueryTriggerInteraction.Ignore))
            {
				validPlacement = false;
			}
		}

		if (validPlacement)
		{
			IndicateValidPlacement(true);
			GameEvents.BuildingPlacementChanged(true);
		}
		else
		{
			IndicateValidPlacement(false);
			GameEvents.BuildingPlacementChanged(false);
		}

		rotateCoroutine = null;
	}
	#endregion
}
