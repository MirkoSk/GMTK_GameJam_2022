using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
	Build,
	Produce,
	Research
}

public class Action : ScriptableObject
{

	#region Variable Declarations
	public string Effect;
	public int ResearchCost = 1;

	protected ActionType type;
	#endregion



	#region Public Properties
	public ActionType Type { get => type; }
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}
