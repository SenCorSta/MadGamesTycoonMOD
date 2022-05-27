using System;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class textChat : MonoBehaviour
{
	// Token: 0x06001A98 RID: 6808 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x00011DEF File Offset: 0x0000FFEF
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 30f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040021EF RID: 8687
	private float timer;
}
