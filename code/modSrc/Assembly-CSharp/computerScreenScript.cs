using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class computerScreenScript : MonoBehaviour
{
	// Token: 0x06001A39 RID: 6713 RVA: 0x0010A119 File Offset: 0x00108319
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x0010A124 File Offset: 0x00108324
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

	// Token: 0x06001A3B RID: 6715 RVA: 0x0010A1D4 File Offset: 0x001083D4
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

	// Token: 0x0400213E RID: 8510
	public MeshRenderer renderer;

	// Token: 0x0400213F RID: 8511
	public Material[] mat;

	// Token: 0x04002140 RID: 8512
	public bool force;

	// Token: 0x04002141 RID: 8513
	private float timer;

	// Token: 0x04002142 RID: 8514
	private float rnd;

	// Token: 0x04002143 RID: 8515
	private GameObject main_;

	// Token: 0x04002144 RID: 8516
	private roomScript roomS_;

	// Token: 0x04002145 RID: 8517
	private mapScript mapS_;

	// Token: 0x04002146 RID: 8518
	private mainScript mS_;

	// Token: 0x04002147 RID: 8519
	private objectScript oS_;
}
