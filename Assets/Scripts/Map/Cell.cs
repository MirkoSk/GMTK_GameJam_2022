using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] int districtID;
	[SerializeField] GameObject outlinesMouseOver;
	[SerializeField] GameObject outlinesSelected;
	[InspectorButton("RemoveIDsBelowStreets", ButtonWidth = 200f)]
	[SerializeField] bool removeIDsBelowStreets;

	// Private

	#endregion



	#region Public Properties
	public int DistrictID { get => districtID; }
    #endregion



    #region Unity Event Functions
    
    #endregion



    #region Public Functions
    public void ToggleOutlinesMouseOver(bool visible, Color color)
    {
		if (visible && outlinesSelected.gameObject.activeSelf) return;

		outlinesMouseOver.gameObject.SetActive(visible);
		Renderer[] renderers = outlinesMouseOver.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
			renderers[i].material.color = color;
        }
    }

	public void ToggleOutlinesSelected(bool visible, Color color)
	{
		if (visible && outlinesMouseOver.gameObject.activeSelf) ToggleOutlinesMouseOver(false, color);

		outlinesSelected.gameObject.SetActive(visible);
		Renderer[] renderers = outlinesSelected.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].material.color = color;
		}
	}
	#endregion



	#region Private Functions
	void RemoveIDsBelowStreets()
    {
		if (Physics.Raycast(transform.position, Vector3.up)) districtID = 0;
    }
	#endregion



	#region Coroutines

	#endregion
}