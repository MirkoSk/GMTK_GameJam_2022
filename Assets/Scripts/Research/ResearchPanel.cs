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
    Die currentDie;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        GameEvents.OnTechnologySelected += HandleTechnologySelected;
    }

    private void OnDisable()
    {
        GameEvents.OnTechnologySelected -= HandleTechnologySelected;
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

        currentDie = die;

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
	void HandleTechnologySelected(Face selectedFace, DieSlot dieSlot)
    {
        buttonOkay.onClick.RemoveAllListeners();
        buttonOkay.onClick.AddListener(() => 
        {
            GoldTracker.Instance.AddGold(-selectedFace.Action.ResearchCost);
            GameManager.Instance.DiceSet.Find(x => x.Die == currentDie).ActionUsed = true;

            buttonOkay.gameObject.SetActive(false);
            buttonCancel.gameObject.SetActive(false);

            technologySelectionPanel.SetActive(false);
            dieSlot.UpdateFace(selectedFace);
            dieSlot.UpdateDie();

            GameEvents.ActionCompleted(dieSlot.Die, researchAction, true);
        });
        buttonOkay.GetComponent<Image>().color = Color.white;
        buttonOkay.enabled = true;
    }
    #endregion



    #region Coroutines

    #endregion
}