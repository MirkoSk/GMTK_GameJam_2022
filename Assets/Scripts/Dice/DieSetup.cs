using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieSetup : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] Die die;

	[Header("References")]
	[SerializeField] Image background;
	[SerializeField] List<DieSlot> dieSlots = new List<DieSlot>();

	// Private

	#endregion



	#region Public Properties
	public Die Die { get => die; }
	public List<DieSlot> DieSlots { get => dieSlots; }
	#endregion



	#region Unity Event Functions
	private void Start () 
	{
		GameEvents.OnActionCompleted += HandleActionCompletion;

		if (die != null) UpdateDieDisplay();
	}

	private void OnDestroy()
	{
		GameEvents.OnActionCompleted -= HandleActionCompletion;
	}
	#endregion



	#region Public Functions
	public void UpdateDieDisplay(Die newDie = null)
	{
		if (newDie != null) die = newDie;

		background.color = die.Color;
		for (int i = 0; i < dieSlots.Count; i++)
		{
			dieSlots[i].UpdateFace(die.Faces[i]);
		}
	}
	#endregion



	#region Private Functions
	void HandleActionCompletion(Die die, Action action, bool success)
    {
		if (die == this.die && action.Type == ActionType.Research && success)
        {
			UpdateDieDisplay();
        }
    }
	#endregion



	#region Coroutines

	#endregion
}