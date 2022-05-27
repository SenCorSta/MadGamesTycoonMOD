using System;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class animateMaterial : MonoBehaviour
{
	// Token: 0x060019C9 RID: 6601 RVA: 0x0001160F File Offset: 0x0000F80F
	private void Start()
	{
		this.FindScripts();
		this.myRenderer = base.GetComponent<MeshRenderer>();
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x00011623 File Offset: 0x0000F823
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

	// Token: 0x060019CB RID: 6603 RVA: 0x0010D994 File Offset: 0x0010BB94
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

	// Token: 0x040020F9 RID: 8441
	private GameObject main_;

	// Token: 0x040020FA RID: 8442
	private mainScript mS_;

	// Token: 0x040020FB RID: 8443
	public Material[] frames;

	// Token: 0x040020FC RID: 8444
	public float speed = 1f;

	// Token: 0x040020FD RID: 8445
	private MeshRenderer myRenderer;

	// Token: 0x040020FE RID: 8446
	private float timer;

	// Token: 0x040020FF RID: 8447
	private int aktFrame;
}
