using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TechnologyVisualizer : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] Image faceImage;
	[SerializeField] TextMeshProUGUI costTextmesh;

	// Private
	Face face;
	DieSlot selectedSlot;
	Vector3 lastPosition;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    public void OnBeginDrag(PointerEventData eventData)
	{
		lastPosition = faceImage.transform.position;
		selectedSlot = null;
		faceImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		faceImage.transform.position = eventData.position;
    }

	public void OnEndDrag(PointerEventData eventData)
	{
		if (selectedSlot != null)
        {
			faceImage.transform.position = selectedSlot.transform.position;
			lastPosition = faceImage.transform.position;

			GameEvents.TechnologySelected(face, selectedSlot);
		}
		else
        {
			faceImage.transform.position = lastPosition;
		}

		faceImage.raycastTarget = true;
	}
	#endregion



	#region Public Functions
	public void Initialize(Face face)
    {
		this.face = face;
		faceImage.sprite = face.Sprite;
		costTextmesh.text = face.Action.ResearchCost.ToString();
    }

	public void UpdateSelectedSlot(DieSlot dieSlot)
    {
		selectedSlot = dieSlot;
    }
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}