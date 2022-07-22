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
	Vector3 originalPosition;
	Vector3 lastPosition;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		originalPosition = faceImage.transform.position;
    }

    private void OnEnable()
    {
		GameEvents.OnTechnologySelected += HandleTechnologySelected;
		faceImage.transform.position = originalPosition;
		active = true;
    }

    private void OnDisable()
    {
        GameEvents.OnTechnologySelected -= HandleTechnologySelected;
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
			lastPosition = faceImage.transform.position;

			GameEvents.TechnologySelected(action, selectedSlot);
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
	void HandleTechnologySelected(Action researchedAction, DieSlot dieSlot)
    {
		// Another technology has been selected
		if (researchedAction != action && dieSlot != selectedSlot) active = false;
    }
	#endregion



	#region Coroutines

	#endregion
}