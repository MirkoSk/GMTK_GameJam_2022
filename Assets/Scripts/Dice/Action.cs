using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
	Build,
	Produce,
	Research,
	Upgrade
}

public class Action : ScriptableObject
{

	#region Variable Declarations
	[Header("Action")]
	public Sprite FaceSprite;
	public string Effect;
	public int ResearchCost = 1;

	protected ActionType type;
	#endregion



	#region Public Properties
	public ActionType Type { get => type; }
    #endregion



    #region Unity Event Functions

    #endregion



    #region Public Functions

    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}
