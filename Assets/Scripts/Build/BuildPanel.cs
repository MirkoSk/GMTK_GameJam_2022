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
    [SerializeField] Button buttonAction;
    [SerializeField] Button buttonOkay;
    [SerializeField] Button buttonCancel;

    // Private

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
        BuildAction buildAction;

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
        for (int i = 0; i < shapes.Count; i++)
        {
            if (i < buildAction.Buildings.Count)
            {
                shapes[i].sprite = buildAction.Buildings[i].GetComponent<Building>().Shape;
                shapes[i].color = die.Color;
                shapes[i].enabled = true;
                shapes[i].gameObject.SetActive(true);
            }
            else
            {
                shapes[i].gameObject.SetActive(false);
            }
        }
        gameObject.SetActive(true);

        // Set up action button
        buttonAction.GetComponentInChildren<TextMeshProUGUI>().text = "Build";
        buttonAction.GetComponent<Image>().color = Color.white;
        buttonAction.enabled = true;
        buttonAction.onClick.RemoveAllListeners();
        buttonAction.onClick.AddListener(() => 
        {
            buttonAction.gameObject.SetActive(false);
            buttonOkay.gameObject.SetActive(true);
            buttonOkay.onClick.AddListener(() => 
            {
                GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
                buttonOkay.gameObject.SetActive(false);
                buttonCancel.gameObject.SetActive(false);
                GameEvents.ActionCompleted(die, action, true);
            });
            buttonCancel.gameObject.SetActive(true);
            buttonCancel.onClick.AddListener(() => 
            {
                GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
                buttonOkay.gameObject.SetActive(false);
                buttonCancel.gameObject.SetActive(false);
                GameEvents.ActionCompleted(die, action, false); 
            });
            SelectBuilding(buildAction, die);
            GameEvents.ActionConfirmed(die, action);
        });
        buttonAction.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    #endregion



    #region Private Functions
    void SelectBuilding(BuildAction buildAction, Die die)
    {
        int randomNumber = Random.Range(0, buildAction.Buildings.Count);
        Building selectedBuilding = buildAction.Buildings[randomNumber].GetComponent<Building>();

        for (int i = 0; i < shapes.Count; i++)
        {
            if (i != randomNumber) shapes[i].enabled = false;
            else shapes[i].GetComponent<BuildingSpawner>().Initialize(buildAction.Buildings[randomNumber], die, buildAction.Production);
        }
    }

    void UpdateOkButton(bool validBuildingPlacement)
    {
        if (validBuildingPlacement)
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
    #endregion



    #region Coroutines

    #endregion
}