using System;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class destroyInTime : MonoBehaviour
{
	// Token: 0x06001A0D RID: 6669 RVA: 0x000118BA File Offset: 0x0000FABA
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.timeToLife)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002142 RID: 8514
	public float timeToLife = 3f;

	// Token: 0x04002143 RID: 8515
	private float timer;
}
