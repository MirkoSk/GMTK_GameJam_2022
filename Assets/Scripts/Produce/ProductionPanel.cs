using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductionPanel : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] TextMeshProUGUI effectTextmesh;
    [SerializeField] TextMeshProUGUI playerPromptTextmesh;
    [SerializeField] Button buttonAction;
    [SerializeField] Button buttonOkay;
    [SerializeField] Button buttonCancel;
    [SerializeField] DistrictSelector districtSelector;

    // Private
    int productionValueOfSelectedDistricts;
    Die die;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        GameEvents.OnDistrictSelectionChanged += UpdateProduceButton;
    }

    private void OnDisable()
    {
        GameEvents.OnDistrictSelectionChanged -= UpdateProduceButton;
    }
    #endregion



    #region Public Functions
    public void Activate(Die die, Action action)
    {
        ProduceAction produceAction;

        try
        {
            produceAction = action as ProduceAction;
        }
        catch (System.Exception)
        {
            Debug.LogError("Tried to produce with non-produce action.");
            throw;
        }

        this.die = die;
        string effectString = produceAction.Effect;

        string colorHex = "FFFFFF";
        if (produceAction.DieColor == null) colorHex = ColorUtility.ToHtmlStringRGB(die.DieColor.Color);
        else colorHex = ColorUtility.ToHtmlStringRGB(produceAction.DieColor.Color);

        string colorName = "";
        if (produceAction.DieColor == null) colorName = die.DieColor.Name;
        else colorName = produceAction.DieColor.Name;

        string colorString = "<color=#" + colorHex + ">" + colorName + "</color>";
        effectString = effectString.Replace("$color", colorString);
        effectTextmesh.text = effectString;
        playerPromptTextmesh.text = produceAction.PlayerPrompt;

        gameObject.SetActive(true);

        // Set up buttons
        buttonAction.gameObject.SetActive(false);
        buttonOkay.gameObject.SetActive(true);
        buttonOkay.GetComponent<Image>().color = Color.grey;
        buttonOkay.enabled = false;
        buttonOkay.onClick.RemoveAllListeners();
        buttonOkay.onClick.AddListener(() =>
        {
            if (produceAction.ProduceTwice) GoldTracker.Instance.AddGold(productionValueOfSelectedDistricts * 2);
            else GoldTracker.Instance.AddGold(productionValueOfSelectedDistricts);
            GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
            buttonOkay.gameObject.SetActive(false);
            buttonCancel.gameObject.SetActive(false);
            AudioManager.Instance.PlayButtonClick();

            GameEvents.ActionCompleted(die, action, true);
        });
        buttonCancel.gameObject.SetActive(true);
        buttonCancel.onClick.RemoveAllListeners();
        buttonCancel.onClick.AddListener(() =>
        {
            GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
            buttonOkay.gameObject.SetActive(false);
            buttonCancel.gameObject.SetActive(false);
            AudioManager.Instance.PlayButtonClick();

            GameEvents.ActionCompleted(die, action, false);
        });

        if (produceAction.DieColor == null) districtSelector.Initialize(die.DieColor, produceAction.NumberOfDistricts);
        else districtSelector.Initialize(produceAction.DieColor, produceAction.NumberOfDistricts);
    }

	public void Deactivate()
    {
        gameObject.SetActive(false);
    }
	#endregion
	
	
	
	#region Private Functions
	void UpdateProduceButton(List<District> districts)
    {
        productionValueOfSelectedDistricts = 0;

        if (districts != null && districts.Count > 0)
        {
            districts.ForEach((district) =>
            {
                productionValueOfSelectedDistricts += district.ProductionValue + (GameManager.Instance.DiceSet.Find(x => x.Die == die).CurrentFaceUp.CurrentLevel - 1);
            });
            buttonOkay.GetComponent<Image>().color = Color.white;
            buttonOkay.enabled = true;
        }
        else
        {
            buttonOkay.GetComponent<Image>().color = Color.grey;
            buttonOkay.enabled = false;
        }
    }
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}