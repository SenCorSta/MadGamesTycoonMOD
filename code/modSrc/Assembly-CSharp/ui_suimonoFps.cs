using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003E RID: 62
public class ui_suimonoFps : MonoBehaviour
{
	// Token: 0x060000DF RID: 223 RVA: 0x0000296D File Offset: 0x00000B6D
	private void Start()
	{
		base.InvokeRepeating("SetType", 0.1f, 0.5f);
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x000213C4 File Offset: 0x0001F5C4
	private void LateUpdate()
	{
		if (this.showFPS)
		{
			this.timeleft -= Time.deltaTime;
			this.accum += Time.timeScale / Time.deltaTime;
			this.frames += 1f;
			if (this.timeleft <= 0f)
			{
				this.timeleft = this.updateInterval;
				this.accum = 0f;
				this.frames = 0f;
				return;
			}
		}
		else
		{
			this.textObj_fps.text = "";
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00021458 File Offset: 0x0001F658
	private void SetType()
	{
		if (this.textObj_fps != null && this.accum > 0f && this.frames > 0f)
		{
			this.textObj_fps.text = "FPS: " + (this.accum / this.frames).ToString("f0");
		}
	}

	// Token: 0x04000257 RID: 599
	public Text textObj_fps;

	// Token: 0x04000258 RID: 600
	public bool showFPS = true;

	// Token: 0x04000259 RID: 601
	private float updateInterval = 0.5f;

	// Token: 0x0400025A RID: 602
	private float accum;

	// Token: 0x0400025B RID: 603
	private float frames;

	// Token: 0x0400025C RID: 604
	private float timeleft;
}
