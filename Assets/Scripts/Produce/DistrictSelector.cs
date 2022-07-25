using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictSelector : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [SerializeField] LayerMask raycastLayerMask;

    // Private
    bool active;
    DieColor dieColor;
    int currentlyMousOveredDistrict;
    int currentlySelectedDistrict;
    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Update()
    {
        if (!active) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, raycastLayerMask))
        {
            Cell cell = hit.collider.GetComponentInParent<Cell>();

            if (cell == null && currentlyMousOveredDistrict == 0) return;
            else if (cell == null && currentlyMousOveredDistrict != 0)
            {
                HideMouseOverOutlinesFromLastSelectedDistrict();
                currentlyMousOveredDistrict = 0;
            }
            else if (cell.DistrictID != currentlyMousOveredDistrict)
            {
                if (currentlyMousOveredDistrict != 0) HideMouseOverOutlinesFromLastSelectedDistrict();

                if (cell.DistrictID != 0)
                {
                    District district = GameManager.Instance.Districts.Find(x => x.ID == cell.DistrictID);
                    if (district.DieColor != null && (district.DieColor == dieColor || dieColor.Joker))
                    {
                        ShowMouseOverOutlinesOfDistrict(district);
                        currentlyMousOveredDistrict = district.ID;
                    }
                }
                else currentlyMousOveredDistrict = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && currentlyMousOveredDistrict != currentlySelectedDistrict)
        {
            if (currentlySelectedDistrict != 0) HideSelectedOutlinesFromLastSelectedDistrict();

            if (currentlyMousOveredDistrict != 0)
            {
                District district = GameManager.Instance.Districts.Find(x => x.ID == currentlyMousOveredDistrict);
                ShowSelectedOutlinesOfDistrict(district);
                currentlySelectedDistrict = currentlyMousOveredDistrict;

                GameEvents.DistrictSelectionChanged(district);
            }
            else
            {
                currentlySelectedDistrict = 0;
                GameEvents.DistrictSelectionChanged(null);
            }
        }
    }

    private void OnDisable()
    {
        if (currentlyMousOveredDistrict != 0) HideMouseOverOutlinesFromLastSelectedDistrict();
        if (currentlySelectedDistrict != 0) HideSelectedOutlinesFromLastSelectedDistrict();
        currentlyMousOveredDistrict = 0;
        currentlySelectedDistrict = 0;
        active = false;
    }
    #endregion



    #region Public Functions
    public void Initialize(DieColor dieColor)
    {
        this.dieColor = dieColor;
        active = true;
    }
    #endregion



    #region Private Functions
    void HideMouseOverOutlinesFromLastSelectedDistrict()
    {
        District district = GameManager.Instance.Districts.Find(x => x.ID == currentlyMousOveredDistrict);
        district.Cells.ForEach((cell) =>
        {
            cell.ToggleOutlinesMouseOver(false, Color.white);
        });
    }

    void ShowMouseOverOutlinesOfDistrict(District district)
    {
        district.Cells.ForEach((cell) =>
        {
            cell.ToggleOutlinesMouseOver(true, district.DieColor.Color);
        });
    }

    void HideSelectedOutlinesFromLastSelectedDistrict()
    {
        District district = GameManager.Instance.Districts.Find(x => x.ID == currentlySelectedDistrict);
        district.Cells.ForEach((cell) =>
        {
            cell.ToggleOutlinesSelected(false, Color.white);
        });
    }

    void ShowSelectedOutlinesOfDistrict(District district)
    {
        district.Cells.ForEach((cell) =>
        {
            cell.ToggleOutlinesSelected(true, district.DieColor.Color);
        });
    }
    #endregion



    #region Coroutines

    #endregion
}