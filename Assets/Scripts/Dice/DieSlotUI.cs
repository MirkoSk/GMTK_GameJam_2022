using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DieSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region Variable Declarations
    // Serialized Fields

    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null) return;

		TechnologyVisualizer techVis = eventData.pointerDrag.GetComponentInParent<TechnologyVisualizer>();
		if (techVis != null) techVis.UpdateDropOffLocation(transform.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null) return;

		TechnologyVisualizer techVis = eventData.pointerDrag.GetComponentInParent<TechnologyVisualizer>();
		if (techVis != null) techVis.UpdateDropOffLocation(Vector3.zero);
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}