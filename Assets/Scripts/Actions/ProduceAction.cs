using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ProduceAction")]
public class ProduceAction : Action
{

	#region Variable Declarations
	[Header("Produce")]
	public string PlayerPrompt;
	public DieColor DieColor;
	public bool ProduceTwice;
	public int NumberOfDistricts = 1;
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
