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
		RaycastHit hitInfo;
		if (Physics.Raycast(positionRay, out hitInfo, 100f, raycastLayerMask))
		{
			if (hitInfo.transform.tag.Contains(Constants.TAG_CELL))
			{
				parent.transform.position = hitInfo.transform.parent.position;

				if (collisions.Count > 0)
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
				parent.transform.position = hitInfo.point;
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