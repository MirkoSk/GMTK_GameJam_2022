using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class DieSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] int id;
	[SerializeField] TextMeshProUGUI levelTextmesh;

	// Private
	Image faceImage;
	DieSetup dieSetup;
	Action action;
    #endregion



    #region Public Properties
	public Die Die { get => dieSetup.Die; }
	public Action Action { get => action; }
	public int ID { get => id; }
    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		Initialize();
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

		if (faceImage == null) Initialize();

		if (action == null)
		{
			faceImage.sprite = null;
			faceImage.color = new Color(1,1,1,0);
			levelTextmesh.text = "";
		}
		else
		{
			faceImage.sprite = action.FaceSprite;

			if (action.Type == ActionType.Produce && (action as ProduceAction).DieColor != null) faceImage.color = (action as ProduceAction).DieColor.Color;
			else if (action.Type == ActionType.Research && (action as ResearchAction).DieColor != null) faceImage.color = (action as ResearchAction).DieColor.Color;
			else faceImage.color = Die.DieColor.Color;
			levelTextmesh.text = Die.Faces[id].CurrentLevel.ToString();
		}
	}

	public void UpdateDie()
    {
		if (action == null) return;

		dieSetup.Die.Faces[dieSetup.DieSlots.IndexOf(this)].Action = action;
    }
	#endregion



	#region Private Functions
	void Initialize()
    {
		dieSetup = transform.GetRequiredComponentInParent<DieSetup>();
		faceImage = transform.GetRequiredComponent<Image>();
	}
	#endregion



	#region Coroutines

	#endregion
}