using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class fanletterTime : MonoBehaviour
{
	// Token: 0x06001A1E RID: 6686 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x000119C9 File Offset: 0x0000FBC9
	public void Init(float f)
	{
		this.anzeigeDauer = f;
		this.timer = 0f;
		this.pause = false;
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x0010EDFC File Offset: 0x0010CFFC
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

	// Token: 0x04002153 RID: 8531
	public float anzeigeDauer = 1f;

	// Token: 0x04002154 RID: 8532
	public float timer;

	// Token: 0x04002155 RID: 8533
	public Animation myAnimation;

	// Token: 0x04002156 RID: 8534
	public bool pause;
}
