using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class textChat : MonoBehaviour
{
	// Token: 0x06001AE2 RID: 6882 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x0010E192 File Offset: 0x0010C392
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer > 30f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002209 RID: 8713
	private float timer;
}
