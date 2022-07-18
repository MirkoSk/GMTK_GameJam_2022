using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] GameObject diceConfigPanel;
	[SerializeField] BuildPanel buildPanel;
	[SerializeField] ProductionPanel productionPanel;
	[SerializeField] GameObject researchPanel;
	[SerializeField] GameObject buttonAction;

    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
		ShowDiceConfigPanel(null, null, false);
	}

    private void OnEnable () 
	{
		GameEvents.OnActionSelected += UpdatePanelContent;
		GameEvents.OnActionCompleted += ShowDiceConfigPanel;
	}

	private void OnDisable()
	{
		GameEvents.OnActionSelected -= UpdatePanelContent;
		GameEvents.OnActionCompleted -= ShowDiceConfigPanel;
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions
	void UpdatePanelContent(Die die, Action action)
    {
        switch (action.Type)
        {
            case ActionType.Build:
				buildPanel.Activate(die, action);
				diceConfigPanel.SetActive(false);
                break;
            case ActionType.Produce:
				productionPanel.Activate(die, action);
				diceConfigPanel.SetActive(false);
				break;
            case ActionType.Research:
				researchPanel.SetActive(true);
				diceConfigPanel.SetActive(false);
				buttonAction.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Research";
				break;
            default:
                break;
        }
		buttonAction.SetActive(true);
    }

	void ShowDiceConfigPanel(Die die, Action action, bool success)
    {
		DeactivateAllPanels();
		diceConfigPanel.SetActive(true);
	}

	void DeactivateAllPanels()
    {
		diceConfigPanel.SetActive(false);
		buildPanel.Deactivate();
		productionPanel.Deactivate();
		researchPanel.SetActive(false);
		buttonAction.SetActive(false);
	}
	#endregion



	#region Coroutines

	#endregion
}