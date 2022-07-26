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
	bool active;
	Action action;
	DieSlot selectedSlot;
	Vector3 lastPosition;
    #endregion



    #region Public Properties
	public Action Action { get => action; }
	public DieSlot SelectedSlot { get => selectedSlot; }
	public bool Active { get => active; set => active = value; }
    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
		faceImage.transform.localPosition = Vector3.zero;
		faceImage.transform.localScale = Vector3.one;
		active = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
	{
		if (!active) return;

		lastPosition = faceImage.transform.position;
		selectedSlot = null;
		faceImage.raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!active) return;

		faceImage.transform.position = eventData.position;
    }

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!active) return;

		if (selectedSlot != null)
        {
			faceImage.transform.position = selectedSlot.transform.position;
			float scalingFactor = selectedSlot.GetComponent<RectTransform>().rect.width / (faceImage.GetComponent<RectTransform>().rect.width * faceImage.transform.localScale.x);
			faceImage.transform.localScale *= scalingFactor;
			lastPosition = faceImage.transform.position;

			GameEvents.TechnologySelected(this, action, selectedSlot);
		}
		else
        {
			faceImage.transform.position = lastPosition;
		}

		faceImage.raycastTarget = true;
	}
	#endregion



	#region Public Functions
	public void Initialize(Action action)
    {
		this.action = action;
		faceImage.sprite = action.FaceSprite;
		if (action.Type == ActionType.Produce && (action as ProduceAction).DieColor != null) faceImage.color = (action as ProduceAction).DieColor.Color;
		else if (action.Type == ActionType.Research && (action as ResearchAction).DieColor != null) faceImage.color = (action as ResearchAction).DieColor.Color;
		else faceImage.color = Color.white;
		costTextmesh.text = action.ResearchCost.ToString();
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