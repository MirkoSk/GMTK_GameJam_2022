using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void OnEnable () 
	{
		if (GameManager.Instance.DiceSet[GameManager.Instance.DiceSet.Count - 1].Die == die && !GameManager.Instance.GoldenDieComplete)
        {
			gameObject.SetActive(false);
			return;
		}

		DieState currentDieState = GameManager.Instance.DiceSet.Find(x => x.Die == die);
		
		border.color = die.Color;
		face.color = die.Color;
		currentFaceUp = currentDieState.CurrentFaceUp;
		face.sprite = currentFaceUp.Sprite;

		if (!currentDieState.ActionUsed)
		{
			// TODO: Set button active
		}
        else
        {
			// TODO: Set button inactive
        }
	}
    #endregion



    #region Public Functions
	public void ActivateDieAction()
    {
		GameEvents.ActionSelected(currentFaceUp.Action);
	}
    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}