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

	public static event ActionHandler OnActionCompleted;
	public static void ActionCompleted(Die die, Action action)
    {
		Debug.Log("[GAME_EVENT] OnActionCompleted for " + action + " of " + die);
		OnActionCompleted?.Invoke(die, action);
	}
}
