using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class Map : MonoBehaviour 
{

	#region Variable Declarations
	// Serialized Fields
	[SerializeField] Vector2 mapSize;
	[SerializeField] GameObject cellPrefab;

	// Private
	Grid grid;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion
	
	
	
	#region Unity Event Functions
	private void Awake () 
	{
		grid = transform.GetRequiredComponent<Grid>();
	}
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
	void SpawnMap(Vector2 mapSize)
    {
		for (int y = 0; y < mapSize.y; y++)
		{
			for (int x = 0; x < mapSize.x; x++)
			{
				GameObject newCell = Instantiate(cellPrefab, grid.GetCellCenterWorld(new Vector3Int(x, y, 0)), Quaternion.identity, transform);
				newCell.name = cellPrefab.name + " (" + x + ", " + y + ")";
			}
		}
    }
    #endregion



    #region Coroutines

    #endregion
}