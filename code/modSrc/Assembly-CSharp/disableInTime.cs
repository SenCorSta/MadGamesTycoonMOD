using System;
using UnityEngine;

// Token: 0x020002E2 RID: 738
public class disableInTime : MonoBehaviour
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x00011940 File Offset: 0x0000FB40
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.timeToLife)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x0001196E File Offset: 0x0000FB6E
	private void OnEnable()
	{
		this.timer = 0f;
	}

	// Token: 0x04002145 RID: 8517
	public float timeToLife = 3f;

	// Token: 0x04002146 RID: 8518
	private float timer;
}
