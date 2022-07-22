using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Die")]
public class Die : ScriptableObject
{

	#region Variable Declarations
	public Color Color;
	public Material Material;
	public Face[] Faces = new Face[6];
    public Die StartupDefault;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        if (StartupDefault)
        {
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i] = StartupDefault.Faces[i];
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
