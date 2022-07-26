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
    ResearchAction researchAction;
    List<TechnologyVisualizer> researchedTechnologies = new List<TechnologyVisualizer>();
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        GameEvents.OnActionConfirmed += SelectTechnologies;
        GameEvents.OnTechnologySelected += HandleTechnologySelected;
    }

    private void OnDisable()
    {
        GameEvents.OnActionConfirmed -= SelectTechnologies;
        GameEvents.OnTechnologySelected -= HandleTechnologySelected;
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    void SelectTechnologies(Die die, Action action)
    {
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

    void HandleTechnologySelected(TechnologyVisualizer technologyVisualizer, Action researchedAction, DieSlot dieSlot)
    {
        if (!researchedTechnologies.Contains(technologyVisualizer)) researchedTechnologies.Add(technologyVisualizer);

        // Deactivate all remaining technology options when research limit is reached
        if (researchedTechnologies.Count >= researchAction.NumberOfResearches)
        {
            for (int i = 0; i < technologies.Count; i++)
            {
                if (technologies[i].Action != researchedAction && technologies[i].SelectedSlot != dieSlot) technologies[i].Active = false;
            }
        }
    }
    #endregion



    #region Coroutines

    #endregion
}