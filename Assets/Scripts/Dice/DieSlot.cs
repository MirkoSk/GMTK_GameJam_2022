using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DieSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	#region Variable Declarations
	// Serialized Fields

	// Private
	Image faceImage;
	DieSetup dieSetup;
	Action action;
    #endregion



    #region Public Properties
	public Die Die { get => dieSetup.Die; }
    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		dieSetup = transform.GetRequiredComponentInParent<DieSetup>();
		faceImage = transform.GetRequiredComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null) return;

		TechnologyVisualizer techVis = eventData.pointerDrag.GetComponentInParent<TechnologyVisualizer>();
		if (techVis != null) techVis.UpdateSelectedSlot(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null) return;

		TechnologyVisualizer techVis = eventData.pointerDrag.GetComponentInParent<TechnologyVisualizer>();
		if (techVis != null) techVis.UpdateSelectedSlot(this);
	}
	#endregion



	#region Public Functions
	public void UpdateFace(Action action)
	{
		this.action = action;

		if (action == null)
		{
			faceImage.color = new Color(1,1,1,0);
		}
		else
		{
			faceImage.sprite = action.FaceSprite;
			faceImage.color = dieSetup.Die.DieColor.Color;
		}
	}

	public void UpdateDie()
    {
		if (action == null) return;

		dieSetup.Die.Actions[dieSetup.DieSlots.IndexOf(this)] = action;
    }
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}