using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class RolledDieVisualizer : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] Die die;

	[Space]
	[SerializeField] Image border;
	[SerializeField] Image face;

	// Private
	Face currentFaceUp;
	Button button;
	bool dieCurrentlySelected;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		button = transform.GetRequiredComponent<Button>();
    }

    private void OnEnable () 
	{
		GameEvents.OnActionSelected += HighlightDie;
		GameEvents.OnActionCompleted += UpdateDieVisuals;

		// Show golden die?
		if (GameManager.Instance.DiceSet[GameManager.Instance.DiceSet.Count - 1].Die == die && !GameManager.Instance.GoldenDieComplete)
        {
			gameObject.SetActive(false);
			return;
		}

		// Update dice visuals
		DieState currentDieState = GameManager.Instance.DiceSet.Find(x => x.Die == die);
		currentFaceUp = currentDieState.CurrentFaceUp;
		face.sprite = currentFaceUp.Sprite;
		UpdateDieVisuals(die, null, false);
	}

    private void OnDisable()
    {
		GameEvents.OnActionSelected -= HighlightDie;
		GameEvents.OnActionCompleted -= UpdateDieVisuals;
	}
    #endregion



    #region Public Functions
    public void SelectDieAction()
    {
		if (dieCurrentlySelected) return;

		dieCurrentlySelected = true;
		GameEvents.ActionSelected(die, currentFaceUp.Action);
	}
    #endregion



    #region Private Functions
	void UpdateDieVisuals(Die die, Action action, bool success)
    {
		if (die != this.die) return;

		transform.DOScale(Vector3.one, 0.5f);
		dieCurrentlySelected = false;

		DieState currentDieState = GameManager.Instance.DiceSet.Find(x => x.Die == die);

		if (!currentDieState.ActionUsed)
		{
			border.color = die.Color;
			face.color = die.Color;
			button.enabled = true;
		}
		else
		{
			border.color = Color.grey;
			face.color = Color.grey;
			button.enabled = false;
		}
	}

	void HighlightDie(Die die, Action action)
    {
		if (die != this.die)
        {
			transform.DOScale(Vector3.one, 0.5f);
			dieCurrentlySelected = false;
        }
		else
		{
			transform.DOScale(Vector3.one * 1.2f, 0.5f);
		}
    }
    #endregion



    #region Coroutines

    #endregion
}