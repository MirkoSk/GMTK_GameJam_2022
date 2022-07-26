using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/UpgradeAction")]
public class UpgradeAction : Action
{

    #region Variable Declarations
    public List<int> UpgradeCosts = new List<int>();
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
        type = ActionType.Upgrade;
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}
