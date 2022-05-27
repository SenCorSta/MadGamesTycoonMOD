using System;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class fanletterTime : MonoBehaviour
{
	// Token: 0x06001A68 RID: 6760 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x0010AE20 File Offset: 0x00109020
	public void Init(float f)
	{
		this.anzeigeDauer = f;
		this.timer = 0f;
		this.pause = false;
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x0010AE3C File Offset: 0x0010903C
	private void Update()
	{
		this.timer += Time.deltaTime;
		if (!this.pause)
		{
			if (this.timer > 1f)
			{
				this.pause = true;
				this.myAnimation["showFanLetter"].speed = 0f;
				return;
			}
		}
		else if (this.timer > this.anzeigeDauer)
		{
			this.pause = false;
			this.myAnimation["showFanLetter"].speed = 1f;
		}
	}

	// Token: 0x0400216D RID: 8557
	public float anzeigeDauer = 1f;

	// Token: 0x0400216E RID: 8558
	public float timer;

	// Token: 0x0400216F RID: 8559
	public Animation myAnimation;

	// Token: 0x04002170 RID: 8560
	public bool pause;
}
