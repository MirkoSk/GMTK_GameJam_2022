using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpawner : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	#region Variable Declarations
	public GameObject Building;

	// Serialized Fields
	[SerializeField] LayerMask raycastLayerMask;

	// Private
	GameObject draggable;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Building == null) return;

		Ray positionRay = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
		RaycastHit hitInfo;
		Physics.Raycast(positionRay, out hitInfo, 100f, raycastLayerMask);
		draggable = Instantiate(Building, hitInfo.point, Quaternion.identity);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (Building == null) return;

		Ray positionRay = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
		RaycastHit hitInfo;
		if (Physics.Raycast(positionRay, out hitInfo, 100f, raycastLayerMask))
		{
			if (hitInfo.transform.tag.Contains(Constants.TAG_CELL))
			{
				draggable.transform.position = hitInfo.transform.parent.position;
			}
			else
			{
				draggable.transform.position = hitInfo.point;
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}