using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechnologySelector : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] List<TechnologyVisualizer> technologies = new List<TechnologyVisualizer>();
    [SerializeField] TechnologyVisualizer upgradeTech;

    [Space]
    [SerializeField] List<Action> researchableActions = new List<Action>();

    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        GameEvents.OnActionConfirmed += SelectTechnologies;
    }

    private void OnDisable()
    {
        GameEvents.OnActionConfirmed -= SelectTechnologies;
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    void SelectTechnologies(Die die, Action action)
    {
        ResearchAction researchAction;
        try
        {
            researchAction = action as ResearchAction;
        }
        catch (System.Exception)
        {
            Debug.LogError("Tried to research with non-research action.");
            throw;
        }

        for (int i = 0; i < technologies.Count; i++)
        {
            technologies[i].Initialize(researchableActions[Random.Range(0, researchableActions.Count)]);
        }
    }
    #endregion



    #region Coroutines

    #endregion
}