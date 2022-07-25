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

    [Header("Die Setups")]
    [SerializeField] GameObject diceSetupPanel2;
    [SerializeField] DieSetup currentDieSetup;
    [SerializeField] DieSetup goldenDieSetup;
    [SerializeField] GameObject diceSetupPanel4;
    [SerializeField] List<DieSetup> allDieSetups = new List<DieSetup>();
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

        string effectString = researchAction.Effect;

        string colorHex = "FFFFFF";
        if (researchAction.DieColor == null) colorHex = ColorUtility.ToHtmlStringRGB(die.DieColor.Color);
        else colorHex = ColorUtility.ToHtmlStringRGB(researchAction.DieColor.Color);

        string colorName = "";
        if (researchAction.DieColor == null) colorName = die.DieColor.Name;
        else colorName = researchAction.DieColor.Name;

        string colorString = "<color=#" + colorHex + ">" + colorName + "</color>";
        effectString = effectString.Replace("$color", colorString);
        effectTextmesh.text = effectString;

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

            Die dieForResearch;
            if (researchAction.DieColor == null) dieForResearch = die;
            else dieForResearch = GameManager.Instance.DiceSet.Find(x => x.Die.DieColor == researchAction.DieColor).Die;

            if (dieForResearch.DieColor.Joker)
            {
                for (int i = 0; i < allDieSetups.Count; i++)
                {
                    allDieSetups[i].UpdateDieDisplay(GameManager.Instance.DiceSet[i].Die);
                }
                diceSetupPanel2.SetActive(false);
                diceSetupPanel4.SetActive(true);
            }
            else
            {
                currentDieSetup.UpdateDieDisplay(dieForResearch);
                goldenDieSetup.UpdateDieDisplay(goldenDie);
                diceSetupPanel2.SetActive(true);
                diceSetupPanel4.SetActive(false);
            }

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
	void HandleTechnologySelected(Action selectedAction, DieSlot dieSlot)
    {
        if (GoldTracker.Instance.CurrentGold >= selectedAction.ResearchCost)
        {
            buttonOkay.onClick.RemoveAllListeners();
            buttonOkay.onClick.AddListener(() =>
            {
                GoldTracker.Instance.AddGold(-selectedAction.ResearchCost);
                GameManager.Instance.DiceSet.Find(x => x.Die == currentDie).ActionUsed = true;

                buttonOkay.gameObject.SetActive(false);
                buttonCancel.gameObject.SetActive(false);

                technologySelectionPanel.SetActive(false);
                dieSlot.UpdateFace(selectedAction);
                dieSlot.UpdateDie();

                GameEvents.ActionCompleted(dieSlot.Die, researchAction, true);
            });
            buttonOkay.GetComponent<Image>().color = Color.white;
            buttonOkay.enabled = true;
        }
    }
    #endregion



    #region Coroutines

    #endregion
}