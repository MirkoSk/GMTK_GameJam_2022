using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

	#region Variable Declarations
	[SerializeField] Sprite shape;
	[SerializeField] GameObject goldImage;
	[SerializeField] GameObject invalidImage;
	[SerializeField] LayerMask cellCheckLayerMask;

	Die die;
	int productionValue;
	District district;
	#endregion



	#region Public Properties
	public Sprite Shape { get => shape; }
    #endregion



    #region Unity Event Functions
    private void OnEnable()
    {
		GameEvents.OnActionCompleted += UpdateDistrict;
    }

	private void OnDisable()
	{
		GameEvents.OnActionCompleted -= UpdateDistrict;
	}
	#endregion



	#region Public Functions
	public void Initialize(Die die, int productionValue)
    {
		this.die = die;
		this.productionValue = productionValue;
		Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
			renderers[i].material.color = die.Color;
        }
    }

	public void IndicateValidPlacement(bool placeable)
	{
		if (placeable)
        {
			goldImage.SetActive(true);
			invalidImage.SetActive(false);
        }
		else
        {
			goldImage.SetActive(false);
			invalidImage.SetActive(true);
		}
	}
	#endregion



	#region Private Functions
	void UpdateDistrict(Die die, Action action, bool success)
    {
		RaycastHit hit;
		Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, cellCheckLayerMask);
		int districtID = hit.transform.GetComponentInParent<Cell>().DistrictID;
		district = GameManager.Instance.Districts.Find(x => x.ID == districtID);
		district.ProductionValue += productionValue;
		if (district.Die == null) district.Die = die;

		GameEvents.OnActionCompleted -= UpdateDistrict;
	}
	#endregion



	#region Coroutines

	#endregion
}
