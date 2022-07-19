using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	#region Variable Declarations
	[SerializeField] Sprite shape;
	[SerializeField] GameObject goldImage;
	[SerializeField] GameObject invalidImage;
	[SerializeField] LayerMask cellCheckLayerMask;

	Die die;
	int productionValue;
	District currentDistrict;
	bool buildingPlaced;
	#endregion



	#region Public Properties
	public Sprite Shape { get => shape; }
	public Die Die { get => die; }
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
	#endregion



	#region Public Functions
	public void Initialize(Die die, int productionValue)
    {
		this.die = die;
		this.productionValue = productionValue;
		Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
			renderers[i].material.color = die.Color;
        }
    }

	public void IndicateValidPlacement(bool placeable)
	{
		if (placeable)
        {
			goldImage.SetActive(true);
			invalidImage.SetActive(false);
        }
		else
        {
			goldImage.SetActive(false);
			invalidImage.SetActive(true);
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
					currentDistrict = new District(districtID, die);
				}
				else
				{
					currentDistrict.Die = die;
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

		District gmDistrict = GameManager.Instance.Districts.Find(x => x.ID == currentDistrict.ID);
		if (gmDistrict.Die == null) gmDistrict.Die = die;
		gmDistrict.ProductionValue += productionValue;
		buildingPlaced = true;

		GameEvents.OnBuildingPlacementChanged -= UpdateDistrict;
    }
	#endregion



	#region Coroutines

	#endregion
}
