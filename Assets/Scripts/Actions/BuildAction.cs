using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BuildAction")]
public class BuildAction : Action
{

	#region Variable Declarations
	public string Effect;
	public int Cost;
	public List<Building> Buildings = new List<Building>();
    #endregion



    #region Public Properties

    #endregion



    #region Public Functions
    private void Awake()
    {
        type = ActionType.Build;
    }
    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}
