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
    int numberOfDistricts;
    int currentlyMousOveredDistrict;
    List<int> currentlySelectedDistricts = new List<int>();
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

        if (Input.GetMouseButtonDown(0) && currentlyMousOveredDistrict != 0 && !currentlySelectedDistricts.Contains(currentlyMousOveredDistrict))
        {
            if (currentlySelectedDistricts.Count >= numberOfDistricts)
            {
                HideSelectedOutlinesFromDistrict(currentlySelectedDistricts[0]);
                currentlySelectedDistricts.RemoveAt(0);
            }

            District district = GameManager.Instance.Districts.Find(x => x.ID == currentlyMousOveredDistrict);
            ShowSelectedOutlinesOfDistrict(district);
            currentlySelectedDistricts.Add(currentlyMousOveredDistrict);

            List<District> selectedDistricts = new List<District>();
            for (int i = 0; i < currentlySelectedDistricts.Count; i++)
            {
                selectedDistricts.Add(GameManager.Instance.Districts.Find(x => x.ID == currentlySelectedDistricts[i]));
            }
            GameEvents.DistrictSelectionChanged(selectedDistricts);
        }
    }

    private void OnDisable()
    {
        if (currentlyMousOveredDistrict != 0) HideMouseOverOutlinesFromLastSelectedDistrict();
        if (currentlySelectedDistricts.Count > 0) currentlySelectedDistricts.ForEach((district) => { HideSelectedOutlinesFromDistrict(district); });
        currentlyMousOveredDistrict = 0;
        currentlySelectedDistricts.Clear();
        active = false;
    }
    #endregion



    #region Public Functions
    public void Initialize(DieColor dieColor, int numberOfDistricts)
    {
        this.dieColor = dieColor;
        this.numberOfDistricts = numberOfDistricts;
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

    void HideSelectedOutlinesFromDistrict(int districtID)
    {
        District district = GameManager.Instance.Districts.Find(x => x.ID == districtID);
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