using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] GameObject diceConfigPanel;
	[SerializeField] GameObject buildPanel;
	[SerializeField] GameObject productionPanel;
	[SerializeField] GameObject researchPanel;
	[SerializeField] GameObject buttonAction;

    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Start()
    {
		diceConfigPanel.SetActive(true);
		buildPanel.SetActive(false);
		productionPanel.SetActive(false);
		researchPanel.SetActive(false);
		buttonAction.SetActive(false);
	}

    private void OnEnable () 
	{
		GameEvents.OnActionSelected += UpdatePanelContent;
	}

	private void OnDisable()
	{
		GameEvents.OnActionSelected -= UpdatePanelContent;
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions
	void UpdatePanelContent(Action action)
    {
        switch (action.Type)
        {
            case ActionType.Build:
				buildPanel.SetActive(true);
				diceConfigPanel.SetActive(false);
				buttonAction.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Build";
                break;
            case ActionType.Produce:
				productionPanel.SetActive(true);
				diceConfigPanel.SetActive(false);
				buttonAction.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Produce";
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
	#endregion



	#region Coroutines

	#endregion
}