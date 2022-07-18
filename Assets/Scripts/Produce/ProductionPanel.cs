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

        // Set up action button
        buttonAction.gameObject.SetActive(true);
        buttonAction.GetComponentInChildren<TextMeshProUGUI>().text = "Produce";
        buttonAction.GetComponent<Image>().color = Color.grey;
        buttonAction.enabled = false;
        buttonAction.onClick.RemoveAllListeners();
        buttonAction.onClick.AddListener(() =>
        {
            GoldTracker.Instance.AddGold(productionValueOfSelectedDistrict);
            GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
            buttonAction.gameObject.SetActive(false);
            
            GameEvents.ActionCompleted(die, action, true);
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
            buttonAction.GetComponent<Image>().color = Color.white;
            buttonAction.enabled = true;
            productionValueOfSelectedDistrict = district.ProductionValue;
        }
        else
        {
            buttonAction.GetComponent<Image>().color = Color.grey;
            buttonAction.enabled = false;
        }
    }
	#endregion
	
	
	
	#region Coroutines
	
	#endregion
}