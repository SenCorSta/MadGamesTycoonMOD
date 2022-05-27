using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class CurvePointControl : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	
	public void OnDrag(PointerEventData eventData)
	{
		base.transform.position = Input.mousePosition;
		DrawCurve.use.UpdateLine(this.objectNumber, Input.mousePosition);
	}

	
	public int objectNumber;
}
