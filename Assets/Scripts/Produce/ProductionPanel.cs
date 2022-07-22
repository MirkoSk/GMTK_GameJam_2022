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
    [SerializeField] Button buttonAction;
    [SerializeField] Button buttonOkay;
    [SerializeField] Button buttonCancel;
    [SerializeField] DistrictSelector districtSelector;

    // Private
    int productionValueOfSelectedDistrict;
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

        gameObject.SetActive(true);

        // Set up buttons
        buttonAction.gameObject.SetActive(false);
        buttonOkay.gameObject.SetActive(true);
        buttonOkay.GetComponent<Image>().color = Color.grey;
        buttonOkay.enabled = false;
        buttonOkay.onClick.RemoveAllListeners();
        buttonOkay.onClick.AddListener(() =>
        {
            GoldTracker.Instance.AddGold(productionValueOfSelectedDistrict);
            GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
            buttonOkay.gameObject.SetActive(false);
            buttonCancel.gameObject.SetActive(false);
            
            GameEvents.ActionCompleted(die, action, true);
        });
        buttonCancel.gameObject.SetActive(true);
        buttonCancel.onClick.RemoveAllListeners();
        buttonCancel.onClick.AddListener(() =>
        {
            GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
            buttonOkay.gameObject.SetActive(false);
            buttonCancel.gameObject.SetActive(false);

            GameEvents.ActionCompleted(die, action, false);
        });

        if (produceAction.DieColor == null) districtSelector.Initialize(die.DieColor);
        else districtSelector.Initialize(produceAction.DieColor);
    }

	public void Deactivate()
    {
        gameObject.SetActive(false);
    }
	#endregion
	
	
	
	#region Private Functions
	void UpdateProduceButton(District district)
    {
        if (district != null)
        {
            buttonOkay.GetComponent<Image>().color = Color.white;
            buttonOkay.enabled = true;
            productionValueOfSelectedDistrict = district.ProductionValue;
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