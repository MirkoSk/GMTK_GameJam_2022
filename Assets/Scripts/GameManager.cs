using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DieState
{
	public Die Die;
	public Face CurrentFaceUp;
	public bool ActionUsed;
}

public class GameManager : MonoBehaviour 
{

	#region Variable Declarations
	public static GameManager Instance;

	// Serialized Fields
	[SerializeField] List<DieState> diceSet = new List<DieState>();

	// Private
	bool goldenDieComplete;
	#endregion
	
	
	
	#region Public Properties
	public List<DieState> DiceSet { get => diceSet; }
	public bool GoldenDieComplete { get => goldenDieComplete; }
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
		if (Instance == null) Instance = this;
		else if (Instance != null) Destroy(gameObject);
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}