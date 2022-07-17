using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
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



	public delegate void BuildingPlacementHandler(bool valid);
	public static event BuildingPlacementHandler OnBuildingPlacementChanged;
	public static void BuildingPlacementChanged(bool valid)
    {
		//Debug.Log("[GAME_EVENT] OnBuildingPlacementChanged: " + valid);
		OnBuildingPlacementChanged?.Invoke(valid);
    }
}
