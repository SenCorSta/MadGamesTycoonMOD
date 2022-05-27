using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class Item_FanGenre : MonoBehaviour
{
	// Token: 0x0600069B RID: 1691 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
