using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieSetup : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] Die die;

	[Header("References")]
	[SerializeField] Image background;
	[SerializeField] List<Image> faces = new List<Image>();

	// Private

	#endregion



	#region Public Properties

	#endregion



	#region Unity Event Functions
	private void Start () 
	{
		background.color = die.Color;
        for (int i = 0; i < faces.Count; i++)
        {
			faces[i].sprite = die.Faces[i].Sprite;
			faces[i].color = die.Color;
		}
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}