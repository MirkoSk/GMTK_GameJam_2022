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
		if (die != null) UpdateDieDisplay();
	}
	#endregion
	
	
	
	#region Public Functions
	
	#endregion
	
	
	
	#region Private Functions
	public void UpdateDieDisplay(Die newDie = null)
    {
		if (newDie != null) die = newDie;

		background.color = die.Color;
		for (int i = 0; i < faces.Count; i++)
		{
			if (die.Faces[i] != null)
			{
				faces[i].sprite = die.Faces[i].Sprite;
			}
			else
			{
				faces[i].enabled = false;
			}

			faces[i].color = die.Color;
		}
	}
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}