using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpawner : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] LayerMask raycastLayerMask;

	// Private
	GameObject building;
	Die die;
	GameObject draggable;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (building == null) return;

		Ray positionRay = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
		RaycastHit hitInfo;
		Physics.Raycast(positionRay, out hitInfo, 100f, raycastLayerMask);
		draggable = Instantiate(building, hitInfo.point, Quaternion.identity);
		draggable.GetComponent<Building>().Initialize(die);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (building == null) return;

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
	public void Initialize(GameObject buildingPrefab, Die die)
    {
		building = buildingPrefab;
		this.die = die;
    }
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}