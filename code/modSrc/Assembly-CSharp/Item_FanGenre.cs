using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class Item_FanGenre : MonoBehaviour
{
	// Token: 0x06000692 RID: 1682 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
