using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] GameObject buttonRoll;
	[SerializeField] GameObject diceLayout;

	// Private
	
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		
	}
	#endregion
	
	
	
	#region Public Functions
	public void RollTheDice()
    {
		int diceRolls = GameManager.Instance.GoldenDieComplete ? 4 : 3;
        for (int i = 0; i < diceRolls; i++)
        {
			DieState currentDieState = GameManager.Instance.DiceSet[i];
			currentDieState.CurrentFaceUp = currentDieState.Die.Faces[Random.Range(0, 5)];
        }

		ShowRolledDice();
    }
	#endregion
	
	
	
	#region Private Functions
	void ShowRolledDice()
    {
		buttonRoll.SetActive(false);
		diceLayout.SetActive(true);
    }
	#endregion



	#region Coroutines

	#endregion
}