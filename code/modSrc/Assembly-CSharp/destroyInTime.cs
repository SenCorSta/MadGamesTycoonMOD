using System;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class destroyInTime : MonoBehaviour
{
	// Token: 0x06001A57 RID: 6743 RVA: 0x0010A8C2 File Offset: 0x00108AC2
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.timeToLife)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400215C RID: 8540
	public float timeToLife = 3f;

	// Token: 0x0400215D RID: 8541
	private float timer;
}
