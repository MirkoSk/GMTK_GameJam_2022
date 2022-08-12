using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldTracker : MonoBehaviour 
{

	#region Variable Declarations
	public static GoldTracker Instance;

	public int CurrentGold;

	// Serialized Fields
	[SerializeField] TextMeshProUGUI goldTextmesh;

	// Private
	AudioSource audioSource;
	#endregion



	#region Public Properties

	#endregion



	#region Unity Event Functions
	private void Awake () 
	{
		if (Instance == null) Instance = this;
		else if (Instance != null) Destroy(gameObject);

		audioSource = transform.GetRequiredComponent<AudioSource>();
	}

    private void Start()
    {
		goldTextmesh.text = CurrentGold.ToString();
    }
	#endregion



	#region Public Functions
	public bool AddGold(int amount)
    {
		CurrentGold += amount;
		if (CurrentGold < 0)
        {
			CurrentGold -= amount;
			return false;
		}
		else
		{
			goldTextmesh.text = CurrentGold.ToString();
			if (amount > 0) audioSource.Play();
			return true;
		}
    }
	#endregion



	#region Private Functions

    #endregion



    #region Coroutines

    #endregion
}