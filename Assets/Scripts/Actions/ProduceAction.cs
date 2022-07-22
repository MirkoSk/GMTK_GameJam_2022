using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ProduceAction")]
public class ProduceAction : Action
{

	#region Variable Declarations

	[Header("Produce")]
	public DieColor DieColor;
	#endregion



	#region Public Properties

	#endregion



	#region Unity Event Functions
	private void Awake()
	{
		type = ActionType.Produce;
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}
