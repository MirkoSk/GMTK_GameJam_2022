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

        effectTextmesh.text = produceAction.Effect;
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

        districtSelector.Initialize(die);
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