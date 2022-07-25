using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DieColor")]
public class DieColor : ScriptableObject
{

	#region Variable Declarations
	public string Name;
	public Color Color;
	public Material Material;

	[Space]
	public bool Joker;
	[ConditionalHide("Joker", true, false)]
	public List<DieColor> JokerColors = new List<DieColor>();
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}
