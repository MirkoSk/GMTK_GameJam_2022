using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsTracker : MonoBehaviour 
{

	#region Variable Declarations
	public static PointsTracker Instance;

	// Serialized Fields
	[SerializeField] TextMeshProUGUI pointsTextmesh;

	// Private
	int currentPoints;
	#endregion



	#region Public Properties
	public int CurrentPoints { get => currentPoints; }
	#endregion



	#region Unity Event Functions
	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != null) Destroy(gameObject);
	}

    private void Start()
    {
		pointsTextmesh.text = currentPoints.ToString();
    }
    #endregion



    #region Public Functions
    public void AddPoints(int amount)
    {
		currentPoints += amount;
		pointsTextmesh.text = currentPoints.ToString();
    }
	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}