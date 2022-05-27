using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000346 RID: 838
public class CurvePointControl : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	// Token: 0x06001F4F RID: 8015 RVA: 0x00014B8F File Offset: 0x00012D8F
	public void OnDrag(PointerEventData eventData)
	{
		base.transform.position = Input.mousePosition;
		DrawCurve.use.UpdateLine(this.objectNumber, Input.mousePosition);
	}

	// Token: 0x0400279D RID: 10141
	public int objectNumber;
}
