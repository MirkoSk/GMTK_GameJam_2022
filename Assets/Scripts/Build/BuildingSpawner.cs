using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] LayerMask raycastLayerMask;

	// Private
	bool active;
	GameObject building;
	DieColor dieColor;
	GameObject draggable;
	int buildingProductionValue;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (building == null || !active) return;

		Ray positionRay = Camera.main.ScreenPointToRay(new Vector3(eventData.position.x, eventData.position.y, 0));
		RaycastHit hitInfo;
		Physics.Raycast(positionRay, out hitInfo, 100f, raycastLayerMask);
		draggable = Instantiate(building, hitInfo.point, Quaternion.identity);
		draggable.GetComponent<Building>().Initialize(dieColor, buildingProductionValue);
		eventData.pointerDrag = draggable.GetComponentInChildren<BuildingDragger>().gameObject;
		AudioManager.Instance.PlayBuildingDragSoundVariant();
		active = false;
	}

	public void OnDrag(PointerEventData eventData)
	{

	}
	#endregion



	#region Public Functions
	public void Initialize(GameObject buildingPrefab, DieColor dieColor, int productionValue)
    {
		building = buildingPrefab;
		this.dieColor = dieColor;
		this.buildingProductionValue = productionValue;
		active = true;
    }
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}