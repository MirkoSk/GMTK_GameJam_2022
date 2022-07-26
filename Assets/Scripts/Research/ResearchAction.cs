using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ResearchAction")]
public class ResearchAction : Action
{

	#region Variable Declarations
	[Header("Research")]
	public DieColor DieColor;
	public int NumberOfResearches = 1;
	#endregion



	#region Public Properties

	#endregion



	#region Unity Event Functions
	private void Awake()
	{
		type = ActionType.Research;
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}
