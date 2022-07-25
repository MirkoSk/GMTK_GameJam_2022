using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] TextMeshProUGUI effectTextmesh;
    [SerializeField] TextMeshProUGUI costTextmesh;
    [SerializeField] List<Image> shapes = new List<Image>();
    [SerializeField] GameObject colorSelectionButtons;
    [SerializeField] Button buttonAction;
    [SerializeField] Button buttonOkay;
    [SerializeField] Button buttonCancel;

    // Private
    BuildAction buildAction;
    DieColor buildColor;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
        GameEvents.OnBuildingPlacementChanged += UpdateOkButton;
    }

    private void OnDisable()
    {
        GameEvents.OnBuildingPlacementChanged -= UpdateOkButton;
    }
    #endregion



    #region Public Functions
    public void Activate(Die die, Action action)
    {
        try
        {
            buildAction = action as BuildAction;
        }
        catch (System.Exception)
        {
            Debug.LogError("Tried to build with non-build action.");
            throw;
        }
        
        effectTextmesh.text = buildAction.Effect;
        costTextmesh.text = buildAction.Cost.ToString();
        InitializeShapes(die.DieColor.Color);
        
        gameObject.SetActive(true);

        // Set up buttons
        buttonAction.GetComponentInChildren<TextMeshProUGUI>().text = "Build";

        if (die.DieColor.Joker)
        {
            buttonAction.GetComponent<Image>().color = Color.grey;
            buttonAction.enabled = false;

            colorSelectionButtons.SetActive(true);
        }
        else
        {
            buttonAction.GetComponent<Image>().color = Color.white;
            buttonAction.enabled = true;

            colorSelectionButtons.SetActive(false);
            buildColor = die.DieColor;
        }
        
        buttonAction.onClick.RemoveAllListeners();
        buttonAction.onClick.AddListener(() => 
        {
            buttonAction.gameObject.SetActive(false);
            colorSelectionButtons.SetActive(false);
            buttonOkay.gameObject.SetActive(true);
            buttonOkay.GetComponent<Image>().color = Color.grey;
            buttonOkay.enabled = false;
            buttonOkay.onClick.RemoveAllListeners();
            buttonOkay.onClick.AddListener(() => 
            {
                GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
                GoldTracker.Instance.AddGold(-buildAction.Cost);

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
            SelectBuilding(buildAction, buildColor);
            GameEvents.ActionConfirmed(die, action);
        });
        buttonAction.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void SelectBuildColor(DieColor dieColor)
    {
        buildColor = dieColor;
        InitializeShapes(dieColor.Color);
        buttonAction.GetComponent<Image>().color = Color.white;
        buttonAction.enabled = true;
    }
    #endregion



    #region Private Functions
    void SelectBuilding(BuildAction buildAction, DieColor buildColor)
    {
        int randomNumber = Random.Range(0, buildAction.Buildings.Count);
        Building selectedBuilding = buildAction.Buildings[randomNumber].GetComponent<Building>();

        for (int i = 0; i < shapes.Count; i++)
        {
            if (i != randomNumber) shapes[i].enabled = false;
            else shapes[i].GetComponent<BuildingSpawner>().Initialize(buildAction.Buildings[randomNumber], buildColor, buildAction.Production);
        }
    }

    void UpdateOkButton(bool validBuildingPlacement)
    {
        if (validBuildingPlacement && GoldTracker.Instance.CurrentGold >= buildAction.Cost)
        {
            buttonOkay.GetComponent<Image>().color = Color.white;
            buttonOkay.enabled = true;
        }
        else
        {
            buttonOkay.GetComponent<Image>().color = Color.grey;
            buttonOkay.enabled = false;
        }
    }

    void InitializeShapes(Color color)
    {
        for (int i = 0; i < shapes.Count; i++)
        {
            if (i < buildAction.Buildings.Count)
            {
                shapes[i].sprite = buildAction.Buildings[i].GetComponent<Building>().Shape;
                shapes[i].color = color;
                shapes[i].enabled = true;
                shapes[i].gameObject.SetActive(true);
            }
            else
            {
                shapes[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion



    #region Coroutines

    #endregion
}