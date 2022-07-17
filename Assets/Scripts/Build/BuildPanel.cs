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
                GameEvents.ActionCompleted(die, action);
            });
            buttonCancel.gameObject.SetActive(true);
            // TODO: Cancel button should do something different
            buttonCancel.onClick.AddListener(() => 
            {
                GameManager.Instance.DiceSet.Find(x => x.Die == die).ActionUsed = true;
                buttonOkay.gameObject.SetActive(false);
                buttonCancel.gameObject.SetActive(false);
                GameEvents.ActionCompleted(die, action); 
            });
            SelectBuilding(buildAction);
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
    void SelectBuilding(BuildAction buildAction)
    {
        int randomNumber = Random.Range(0, buildAction.Buildings.Count);
        Building selectedBuilding = buildAction.Buildings[randomNumber].GetComponent<Building>();

        for (int i = 0; i < shapes.Count; i++)
        {
            if (i != randomNumber) shapes[i].enabled = false;
            else shapes[i].GetComponent<BuildingSpawner>().Building = buildAction.Buildings[randomNumber];
        }
    }
    #endregion



    #region Coroutines

    #endregion
}