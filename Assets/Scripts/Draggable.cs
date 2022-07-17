using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	#region Variable Declarations
	// Serialized Fields
	[Tooltip("Leave empty if on same GameObject.")]
	[SerializeField] Image image;

    // Private

    #endregion



    #region Public Properties

    #endregion



    #region Unity Event Functions
    private void Awake()
    {
		if (image == null) image = transform.GetRequiredComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
	#endregion



	#region Public Functions

	#endregion



	#region Private Functions

	#endregion



	#region Coroutines

	#endregion
}