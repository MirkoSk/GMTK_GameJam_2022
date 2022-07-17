using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ProduceAction")]
public class ProduceAction : Action
{

	#region Variable Declarations
	// Serialized Fields

	// Private

	#endregion



	#region Public Properties

	#endregion



	#region Public Functions
	private void Awake()
	{
		type = ActionType.Produce;
	}
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}
