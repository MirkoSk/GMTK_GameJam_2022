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
	Action currentAction;
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
		GameEvents.OnDiceRolled += ActivateDie;
		GameEvents.OnActionSelected += HighlightDie;
		GameEvents.OnActionConfirmed += DeactivateDie;
		GameEvents.OnActionCompleted += UpdateDieVisuals;

		// Show golden die?
		if (GameManager.Instance.DiceSet.Find(x => x.Die.name.Contains("Gold")).Die == die && !GameManager.Instance.GoldenDieComplete)
        {
			gameObject.SetActive(false);
			return;
		}

		// Update dice visuals
		DieState currentDieState = GameManager.Instance.DiceSet.Find(x => x.Die == die);
		currentAction = currentDieState.CurrentAction;
		face.sprite = currentAction.FaceSprite;
		UpdateDieVisuals(die, null, false);
	}

    private void OnDisable()
    {
		GameEvents.OnDiceRolled -= ActivateDie;
		GameEvents.OnActionSelected -= HighlightDie;
		GameEvents.OnActionConfirmed -= DeactivateDie;
		GameEvents.OnActionCompleted -= UpdateDieVisuals;
	}
    #endregion



    #region Public Functions
    public void SelectDieAction()
    {
		if (dieCurrentlySelected) return;

		GameEvents.ActionSelected(die, currentAction);
	}
    #endregion



    #region Private Functions
	void ActivateDie()
    {
		border.color = die.DieColor.Color;
		face.color = die.DieColor.Color;
		button.enabled = true;
	}

	void UpdateDieVisuals(Die die, Action action, bool success)
    {
		transform.DOScale(Vector3.one, 0.5f);
		dieCurrentlySelected = false;

		DieState currentDieState = GameManager.Instance.DiceSet.Find(x => x.Die == this.die);

		if (!currentDieState.ActionUsed)
		{
			border.color = this.die.DieColor.Color;
			face.color = this.die.DieColor.Color;
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
			dieCurrentlySelected = true;
		}
    }

	void DeactivateDie(Die die, Action action)
    {
		button.enabled = false;
    }
    #endregion



    #region Coroutines

    #endregion
}