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
	Vector3 dropOffLocation;
	Vector3 originalPosition;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    public void OnBeginDrag(PointerEventData eventData)
	{
		originalPosition = faceImage.transform.position;
		dropOffLocation = Vector3.zero;
		faceImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		faceImage.transform.position = eventData.position;
    }

	public void OnEndDrag(PointerEventData eventData)
	{
		if (dropOffLocation != Vector3.zero) faceImage.transform.position = dropOffLocation;
		else faceImage.transform.position = originalPosition;

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

	public void UpdateDropOffLocation(Vector3 position)
    {
		dropOffLocation = position;
    }
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}