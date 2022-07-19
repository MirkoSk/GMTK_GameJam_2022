using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] LayerMask raycastLayerMask;

	// Private
	Building parent;
	List<Collider> collisions = new List<Collider>();
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		parent = transform.GetRequiredComponentInParent<Building>();
    }

    public void OnBeginDrag(PointerEventData eventData)
	{

	}

	public void OnDrag(PointerEventData eventData)
	{
		Ray positionRay = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
		RaycastHit hit;
		if (Physics.Raycast(positionRay, out hit, 100f, raycastLayerMask))
		{
			Cell cell = hit.collider.GetComponentInParent<Cell>();
			if (cell != null)
			{
				parent.transform.position = cell.transform.position;

				District currentDistrict = GameManager.Instance.Districts.Find(x => x.ID == cell.DistrictID);
				if (collisions.Count > 0 || (currentDistrict != null && currentDistrict.Die != null && currentDistrict.Die != parent.Die))
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
			else
			{
				parent.transform.position = hit.point;
				parent.IndicateValidPlacement(false);
				GameEvents.BuildingPlacementChanged(false);
			}

		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{

	}

    private void OnTriggerEnter(Collider other)
    {
		if (!collisions.Contains(other)) collisions.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
		if (collisions.Contains(other)) collisions.Remove(other);
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}