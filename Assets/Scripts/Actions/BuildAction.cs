using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BuildAction")]
public class BuildAction : Action
{

	#region Variable Declarations
	public int Cost;
	public List<GameObject> Buildings = new List<GameObject>();
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
