using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchPanel : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] TextMeshProUGUI effectTextmesh;
    [SerializeField] GameObject technologySelectionPanel;
    [SerializeField] Button buttonAction;
    [SerializeField] Button buttonOkay;
    [SerializeField] Button buttonCancel;
    [SerializeField] DieSetup currentDieSetup;
    [SerializeField] DieSetup goldenDieSetup;
    [SerializeField] Die goldenDie;

    // Private
    ResearchAction researchAction;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
		
	}
	#endregion
	
	
	
	#region Public Functions
	public void Activate(Die die, Action action)
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

        effectTextmesh.text = researchAction.Effect;
        technologySelectionPanel.SetActive(false);
        gameObject.SetActive(true);

        // Set up action button
        buttonAction.gameObject.SetActive(true);
        buttonAction.GetComponentInChildren<TextMeshProUGUI>().text = "Research";
        buttonAction.GetComponent<Image>().color = Color.white;
        buttonAction.enabled = true;
        buttonAction.onClick.RemoveAllListeners();
        buttonAction.onClick.AddListener(() =>
        {
            effectTextmesh.text = "";
            technologySelectionPanel.SetActive(true);
            currentDieSetup.UpdateDieDisplay(die);
            goldenDieSetup.UpdateDieDisplay(goldenDie);

            buttonAction.gameObject.SetActive(false);
            buttonOkay.gameObject.SetActive(true);
            buttonOkay.GetComponent<Image>().color = Color.grey;
            buttonOkay.enabled = false;
            buttonOkay.onClick.RemoveAllListeners();
            buttonOkay.onClick.AddListener(() =>
            {
                GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
                // TODO: Execute effect of research action

                buttonOkay.gameObject.SetActive(false);
                buttonCancel.gameObject.SetActive(false);
                technologySelectionPanel.SetActive(false);

                GameEvents.ActionCompleted(die, action, true);
            });
            
            buttonCancel.gameObject.SetActive(true);
            buttonCancel.onClick.RemoveAllListeners();
            buttonCancel.onClick.AddListener(() =>
            {
                GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;

                buttonOkay.gameObject.SetActive(false);
                buttonCancel.gameObject.SetActive(false);
                technologySelectionPanel.SetActive(false);

                GameEvents.ActionCompleted(die, action, false);
            });

            GameEvents.ActionConfirmed(die, action);
        });
    }

	public void Deactivate()
    {
		gameObject.SetActive(false);
    }
	#endregion
	
	
	
	#region Private Functions
	
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}