using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class disableInTime : MonoBehaviour
{
	// Token: 0x06001A5D RID: 6749 RVA: 0x0010A948 File Offset: 0x00108B48
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.timeToLife)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x0010A976 File Offset: 0x00108B76
	private void OnEnable()
	{
		this.timer = 0f;
	}

	// Token: 0x0400215F RID: 8543
	public float timeToLife = 3f;

	// Token: 0x04002160 RID: 8544
	private float timer;
}
