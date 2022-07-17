using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	#region Variable Declarations
	[SerializeField] Sprite shape;

	Die die;
	#endregion



	#region Public Properties
	public Sprite Shape { get => shape; }
	#endregion



	#region Unity Event Functions
	
	#endregion



	#region Public Functions
	public void Initialize(Die die)
    {
		this.die = die;
		Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
			renderers[i].material.color = die.Color;
        }
    }
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}
