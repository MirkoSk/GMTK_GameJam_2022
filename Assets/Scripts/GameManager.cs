using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DieState
{
	public Die Die;
	public Face CurrentFaceUp;
	public bool ActionUsed;
}

[System.Serializable]
public class District
{
	public int ID;
	public Die Die;
	public int ProductionValue;

	public District(int id)
	{
		ID = id;
	}

	public District(int id, Die die/*, int productionValue*/)
    {
		ID = id;
		Die = die;
		//ProductionValue = productionValue;
    }
}

public class GameManager : MonoBehaviour 
{

	#region Variable Declarations
	public static GameManager Instance;

	// Serialized Fields
	[SerializeField] List<DieState> diceSet = new List<DieState>();

	// Private
	bool goldenDieComplete;
	List<District> districts = new List<District>();
	#endregion
	
	
	
	#region Public Properties
	public List<DieState> DiceSet { get => diceSet; }
	public bool GoldenDieComplete { get => goldenDieComplete; }
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
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions
	void CreateDistrictList()
	{
		Cell[] cells = FindObjectsOfType<Cell>();
		for (int i = 0; i < cells.Length; i++)
		{
			if (cells[i].District == 0) continue;
			else if (districts.Count == 0) districts.Add(new District(cells[i].District));
			else if (districts.Find(x => x.ID == cells[i].District) == null)
			{
				districts.Add(new District(cells[i].District));
			}
		}
	}
	#endregion



	#region Coroutines

	#endregion
}