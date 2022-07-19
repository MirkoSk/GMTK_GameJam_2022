using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTracker : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] TextMeshProUGUI turnTextmesh;

	// Private
	int currentTurn = 1;
	#endregion



	#region Public Properties

	#endregion



	#region Unity Event Functions
	private void Start()
	{
		turnTextmesh.text = currentTurn.ToString();
	}

    private void OnEnable()
    {
		GameEvents.OnDiceRolled += AddTurn;
    }

	private void OnDisable()
	{
		GameEvents.OnDiceRolled -= AddTurn;
	}
	#endregion



	#region Public Functions
	public void AddTurn()
	{
		currentTurn++;
		turnTextmesh.text = currentTurn.ToString();
	}
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}