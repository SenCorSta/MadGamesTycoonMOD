using System;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class animateMaterial : MonoBehaviour
{
	// Token: 0x06001A13 RID: 6675 RVA: 0x00109630 File Offset: 0x00107830
	private void Start()
	{
		this.FindScripts();
		this.myRenderer = base.GetComponent<MeshRenderer>();
	}

	// Token: 0x06001A14 RID: 6676 RVA: 0x00109644 File Offset: 0x00107844
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x00109684 File Offset: 0x00107884
	private void Update()
	{
		this.timer += this.speed * this.mS_.GetDeltaTime();
		if ((double)this.timer > 1.0)
		{
			this.timer = 0f;
			this.aktFrame++;
			if (this.aktFrame >= this.frames.Length)
			{
				this.aktFrame = 0;
			}
			this.myRenderer.material = this.frames[this.aktFrame];
		}
	}

	// Token: 0x04002113 RID: 8467
	private GameObject main_;

	// Token: 0x04002114 RID: 8468
	private mainScript mS_;

	// Token: 0x04002115 RID: 8469
	public Material[] frames;

	// Token: 0x04002116 RID: 8470
	public float speed = 1f;

	// Token: 0x04002117 RID: 8471
	private MeshRenderer myRenderer;

	// Token: 0x04002118 RID: 8472
	private float timer;

	// Token: 0x04002119 RID: 8473
	private int aktFrame;
}
