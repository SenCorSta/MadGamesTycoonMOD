﻿using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class uiRoomMouseOver : MonoBehaviour
{
	// Token: 0x0600190A RID: 6410 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x000F9102 File Offset: 0x000F7302
	private void OnMouseOver()
	{
		Debug.Log("KKKKKKKKKKK");
		base.StartCoroutine(this.iSetAsLastSibling());
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x000F911B File Offset: 0x000F731B
	private IEnumerator iSetAsLastSibling()
	{
		yield return new WaitForEndOfFrame();
		base.gameObject.transform.SetAsLastSibling();
		yield break;
	}
}
