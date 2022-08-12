using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnTracker : MonoBehaviour 
{

	#region Variable Declarations
	public static TurnTracker Instance;

	// Serialized Fields
	[SerializeField] TextMeshProUGUI turnTextmesh;

	// Private
	int currentTurn;
	AudioSource audioSource;
	#endregion



	#region Public Properties
	public int CurrentTurn { get => currentTurn; }
	#endregion



	#region Unity Event Functions
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != null) Destroy(gameObject);

		audioSource = transform.GetRequiredComponent<AudioSource>();
	}

	private void Start()
	{
		turnTextmesh.text = currentTurn.ToString();
	}

    private void OnEnable()
    {
		GameEvents.OnNewTurn += AddTurn;
    }

	private void OnDisable()
	{
		GameEvents.OnNewTurn -= AddTurn;
	}
	#endregion



	#region Public Functions
	public void AddTurn()
	{
		currentTurn++;
		turnTextmesh.text = currentTurn.ToString();
		audioSource.Play();
	}
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}