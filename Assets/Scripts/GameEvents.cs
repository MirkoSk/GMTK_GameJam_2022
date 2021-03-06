using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
	#region Turns
	public delegate void TurnHandler();
	public static event TurnHandler OnNewTurn;
	public static void NewTurn()
	{
		Debug.Log("[GAME_EVENT] OnNewTurn");
		OnNewTurn?.Invoke();
	}

	public static event TurnHandler OnDiceRolled;
	public static void DiceRolled()
    {
		Debug.Log("[GAME_EVENT] OnDiceRolled");
		OnDiceRolled?.Invoke();
    }
	#endregion



	#region Actions
	public delegate void ActionHandler(Die die, Action action);
	public static event ActionHandler OnActionSelected;
	public static void ActionSelected(Die die, Action action)
    {
		Debug.Log("[GAME_EVENT] OnActionSelected for " + action + " of " + die);
		OnActionSelected?.Invoke(die, action);
    }

	public static event ActionHandler OnActionConfirmed;
	public static void ActionConfirmed(Die die, Action action)
    {
		Debug.Log("[GAME_EVENT] OnActionConfirmed for " + action + " of " + die);
		OnActionConfirmed?.Invoke(die, action);
	}

	public delegate void ActionCompletionHandler(Die die, Action action, bool success);
	public static event ActionCompletionHandler OnActionCompleted;
	public static void ActionCompleted(Die die, Action action, bool success)
    {
		Debug.Log("[GAME_EVENT] OnActionCompleted for " + action + " of " + die + ". Success: " + success);
		OnActionCompleted?.Invoke(die, action, success);
	}
	#endregion


	// Build
	public delegate void BuildingPlacementHandler(bool valid);
	public static event BuildingPlacementHandler OnBuildingPlacementChanged;
	public static void BuildingPlacementChanged(bool valid)
    {
		//Debug.Log("[GAME_EVENT] OnBuildingPlacementChanged: " + valid);
		OnBuildingPlacementChanged?.Invoke(valid);
    }

	// Produce
	public delegate void DistrictSelectionHandler(List<District> districts);
	public static event DistrictSelectionHandler OnDistrictSelectionChanged;
	public static void DistrictSelectionChanged(List<District> districts)
	{
		string districtIDs = "null";
		if (districts != null && districts.Count > 0)
        {
			districtIDs = "";
			districts.ForEach((district) => { districtIDs += district.ID + ", "; });
        }
		Debug.Log("[GAME_EVENT] OnDistrictSelectionChanged. Selected district ids: " + districtIDs);
		OnDistrictSelectionChanged?.Invoke(districts);
	}

	// Research
	public delegate void TechnologySelectionHandler(TechnologyVisualizer technologyVisualizer, Action researchedAction, DieSlot dieSlot);
	public static event TechnologySelectionHandler OnTechnologySelected;
	public static void TechnologySelected(TechnologyVisualizer technologyVisualizer, Action researchedAction, DieSlot dieSlot)
    {
		Debug.Log("[GAME_EVENT] OnTechnologySelected: " + researchedAction + " on die " + dieSlot.Die);
		OnTechnologySelected?.Invoke(technologyVisualizer, researchedAction, dieSlot);
	}
}
