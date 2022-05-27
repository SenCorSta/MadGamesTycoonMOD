using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000349 RID: 841
public class CurvePointControl : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	// Token: 0x06001FA2 RID: 8098 RVA: 0x001497EA File Offset: 0x001479EA
	public void OnDrag(PointerEventData eventData)
	{
		base.transform.position = Input.mousePosition;
		DrawCurve.use.UpdateLine(this.objectNumber, Input.mousePosition);
	}

	// Token: 0x040027B3 RID: 10163
	public int objectNumber;
}
