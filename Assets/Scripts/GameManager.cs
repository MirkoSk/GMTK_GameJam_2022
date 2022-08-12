using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DieState
{
	public Die Die;
	public Die.Face CurrentFaceUp;
	public bool ActionUsed;
}

[System.Serializable]
public class District
{
	public int ID;
	public DieColor DieColor;
	public int ProductionValue;
	public List<Cell> Cells = new List<Cell>();

	public District(int id)
	{
		ID = id;
	}

	public District(int id, DieColor dieColor)
    {
		ID = id;
		DieColor = dieColor;
    }
}

public class GameManager : MonoBehaviour 
{

	#region Variable Declarations
	public static GameManager Instance;

	// Serialized Fields
	[SerializeField] List<DieState> diceSet = new List<DieState>();

	// Private
	List<District> districts = new List<District>();
	#endregion
	
	
	
	#region Public Properties
	public List<DieState> DiceSet { get => diceSet; }
	public bool GoldenDieComplete
	{
		get
		{
			Die goldenDie = diceSet.Find(x => x.Die.DieColor.Joker).Die;
			bool goldenDieComplete = true;
            for (int i = 0; i < goldenDie.Faces.Length; i++)
            {
				if (goldenDie.Faces[i].Action == null) goldenDieComplete = false;
            }
			return goldenDieComplete;
		}
	}
	public List<District> Districts { get => districts; }
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
		if (Instance == null) Instance = this;
		else if (Instance != null) Destroy(gameObject);
	}

    private void Start()
    {
		CreateDistrictList();
    }

    private void OnEnable()
    {
		GameEvents.OnActionCompleted += HandleActionCompletion;
    }

	private void OnDisable()
	{
		GameEvents.OnActionCompleted -= HandleActionCompletion;
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions
	void CreateDistrictList()
	{
		Cell[] cells = FindObjectsOfType<Cell>();
		for (int i = 0; i < cells.Length; i++)
		{
			if (cells[i].DistrictID == 0) continue;
			else if (districts.Count == 0)
            {
				districts.Add(new District(cells[i].DistrictID));
				districts[0].Cells.Add(cells[i]);
			}
			else if (districts.Find(x => x.ID == cells[i].DistrictID) == null)
			{
				districts.Add(new District(cells[i].DistrictID));
				districts[districts.Count - 1].Cells.Add(cells[i]);
			}
			else
            {
				districts.Find(x => x.ID == cells[i].DistrictID).Cells.Add(cells[i]);
			}
		}
	}

	void HandleActionCompletion(Die die, Action action, bool success)
	{
		bool allDiceActionsUsed = true;

		diceSet.ForEach((dieState) =>
		{
			if (dieState.CurrentFaceUp != null && dieState.CurrentFaceUp.Action != null && !dieState.ActionUsed) allDiceActionsUsed = false;
		});

		if (allDiceActionsUsed)
        {
			diceSet.ForEach((dieState) => 
			{
				dieState.CurrentFaceUp = null;
				dieState.ActionUsed = false; 
			});
			GameEvents.NewTurn();
		}
	}
	#endregion



	#region Coroutines

	#endregion
}