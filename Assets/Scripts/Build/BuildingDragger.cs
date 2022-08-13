using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class BuildingDragger : MonoBehaviour, IDragHandler
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] LayerMask raycastLayerMask;

	// Private
	Building parent;
	PointerEventData eventData;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		parent = transform.GetRequiredComponentInParent<Building>();
    }

    private void OnEnable()
    {
		GameEvents.OnActionCompleted += Deactivate;
    }

	private void OnDisable()
	{
		GameEvents.OnActionCompleted -= Deactivate;
	}

	public void OnDrag(PointerEventData eventData)
	{
		StartCoroutine(UpdateBuildingPlacement(eventData));
	}

    private void OnTriggerEnter(Collider other)
    {
		if (!parent.Collisions.Contains(other)) parent.Collisions.Add(other);
	}

    private void OnTriggerExit(Collider other)
    {
		if (parent.Collisions.Contains(other)) parent.Collisions.Remove(other);
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions
	IEnumerator UpdateBuildingPlacement(PointerEventData eventData)
	{
		Ray positionRay = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
		RaycastHit hit;
		if (Physics.Raycast(positionRay, out hit, 100f, raycastLayerMask))
		{
			Cell cell = hit.collider.GetComponentInParent<Cell>();
			if (cell != null)
			{
				if (parent.transform.position != cell.transform.position)
				{
					parent.transform.position = cell.transform.position;
					AudioManager.Instance.PlayBuildingDragSound();

					yield return new WaitForSecondsRealtime(0.1f);

					District currentDistrict = GameManager.Instance.Districts.Find(x => x.ID == cell.DistrictID);
					if (parent.Collisions.Count > 0 || (currentDistrict != null && currentDistrict.DieColor != null && currentDistrict.DieColor != parent.DieColor))
					{
						parent.IndicateValidPlacement(false);
						GameEvents.BuildingPlacementChanged(false);
					}
					else
					{
						parent.IndicateValidPlacement(true);
						GameEvents.BuildingPlacementChanged(true);
					}
				}
			}
			else
			{
				parent.transform.position = hit.point;
				parent.IndicateValidPlacement(false);
				GameEvents.BuildingPlacementChanged(false);
			}
		}
	}

	void Deactivate(Die die, Action action, bool success)
    {
		GetComponent<Collider>().isTrigger = false;
		enabled = false;
    }
	#endregion



	#region Coroutines

	#endregion
}