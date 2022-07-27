using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Die")]
public class Die : ScriptableObject
{
    [System.Serializable]
    public class Face
    {
        public Action Action;
        public int CurrentLevel = 1;
    }

    #region Variable Declarations
    public DieColor DieColor;
	public Face[] Faces = new Face[6];
    public Die StartupDefault;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnDisable()
    {
        for (int i = 0; i < Faces.Length; i++)
        {
            Faces[i].CurrentLevel = 1;
        }

        if (StartupDefault)
        {
            for (int i = 0; i < Faces.Length; i++)
            {
                Faces[i].Action = StartupDefault.Faces[i].Action;
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
