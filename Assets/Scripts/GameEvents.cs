using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
	public delegate void ActionHandler(Action action);
	public static event ActionHandler OnActionSelected;
	public static void ActionSelected(Action action)
    {
		Debug.Log("[GAME_EVENT] OnActionSelected for ActionType: " + action);
		OnActionSelected?.Invoke(action);
    }
}
