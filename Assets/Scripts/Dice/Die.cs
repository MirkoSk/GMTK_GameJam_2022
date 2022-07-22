using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Die")]
public class Die : ScriptableObject
{

    #region Variable Declarations
    public DieColor DieColor;
	public Action[] Actions = new Action[6];
    public Die StartupDefault;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        if (StartupDefault)
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                Actions[i] = StartupDefault.Actions[i];
            }
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
