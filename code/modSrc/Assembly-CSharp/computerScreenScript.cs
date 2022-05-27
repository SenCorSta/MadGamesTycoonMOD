using System;
using UnityEngine;

// Token: 0x020002D9 RID: 729
public class computerScreenScript : MonoBehaviour
{
	// Token: 0x060019EF RID: 6639 RVA: 0x000117BE File Offset: 0x0000F9BE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019F0 RID: 6640 RVA: 0x0010E2D0 File Offset: 0x0010C4D0
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.mapS_)
		{
			this.mapS_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.oS_)
		{
			this.oS_ = base.transform.root.gameObject.GetComponent<objectScript>();
			if (this.mS_.multiplayer && !this.oS_)
			{
				UnityEngine.Object.Destroy(this);
				return;
			}
		}
	}

	// Token: 0x060019F1 RID: 6641 RVA: 0x0010E380 File Offset: 0x0010C580
	private void Update()
	{
		if (!this.force)
		{
			if (!this.oS_)
			{
				this.FindScripts();
				return;
			}
			if (this.oS_.picked)
			{
				return;
			}
			if (!this.renderer.isVisible)
			{
				return;
			}
			this.roomS_ = this.mapS_.mapRoomScript[Mathf.RoundToInt(base.transform.root.transform.position.x), Mathf.RoundToInt(base.transform.root.transform.position.z)];
			if (!this.roomS_)
			{
				return;
			}
			if (this.roomS_.taskID == -1 || !this.oS_.inUse)
			{
				this.renderer.material = this.mat[0];
				return;
			}
			this.timer += this.mS_.GetDeltaTime();
		}
		else
		{
			this.timer += Time.deltaTime;
		}
		if (this.timer > this.rnd)
		{
			this.timer = 0f;
			this.rnd = UnityEngine.Random.Range(0.5f, 1.5f);
			this.renderer.material = this.mat[UnityEngine.Random.Range(1, this.mat.Length)];
		}
	}

	// Token: 0x04002124 RID: 8484
	public MeshRenderer renderer;

	// Token: 0x04002125 RID: 8485
	public Material[] mat;

	// Token: 0x04002126 RID: 8486
	public bool force;

	// Token: 0x04002127 RID: 8487
	private float timer;

	// Token: 0x04002128 RID: 8488
	private float rnd;

	// Token: 0x04002129 RID: 8489
	private GameObject main_;

	// Token: 0x0400212A RID: 8490
	private roomScript roomS_;

	// Token: 0x0400212B RID: 8491
	private mapScript mapS_;

	// Token: 0x0400212C RID: 8492
	private mainScript mS_;

	// Token: 0x0400212D RID: 8493
	private objectScript oS_;
}
