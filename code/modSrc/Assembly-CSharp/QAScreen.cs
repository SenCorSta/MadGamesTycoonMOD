using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class QAScreen : MonoBehaviour
{
	// Token: 0x06001A0C RID: 6668 RVA: 0x0010926F File Offset: 0x0010746F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x00109278 File Offset: 0x00107478
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.oS_)
		{
			this.oS_ = base.transform.root.gameObject.GetComponent<objectScript>();
		}
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x00109324 File Offset: 0x00107524
	private void Update()
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
		if (this.timer > 2f)
		{
			this.timer = 0f;
			if (!this.newMat)
			{
				this.newMat = new Material(this.mat[0]);
			}
			if (this.roomS_.taskGameObject)
			{
				if (this.roomS_.taskGameObject.GetComponent<taskGameplayVerbessern>() && this.roomS_.taskGameObject.GetComponent<taskGameplayVerbessern>().gS_ && this.newMat)
				{
					this.newMat.mainTexture = this.roomS_.taskGameObject.GetComponent<taskGameplayVerbessern>().gS_.GetScreenshotTexture2D();
					this.renderer.material = this.newMat;
					return;
				}
				if (this.roomS_.taskGameObject.GetComponent<taskBugfixing>() && this.roomS_.taskGameObject.GetComponent<taskBugfixing>().gS_ && this.newMat)
				{
					this.newMat.mainTexture = this.roomS_.taskGameObject.GetComponent<taskBugfixing>().gS_.GetScreenshotTexture2D();
					this.renderer.material = this.newMat;
					return;
				}
				if (this.roomS_.taskGameObject.GetComponent<taskSpielbericht>() && this.roomS_.taskGameObject.GetComponent<taskSpielbericht>().gS_ && this.newMat)
				{
					this.newMat.mainTexture = this.roomS_.taskGameObject.GetComponent<taskSpielbericht>().gS_.GetScreenshotTexture2D();
					this.renderer.material = this.newMat;
					return;
				}
			}
			this.newMat.mainTexture = this.games_.arrayGamesScripts[UnityEngine.Random.Range(0, this.games_.arrayGamesScripts.Length)].GetScreenshotTexture2D();
			this.renderer.material = this.newMat;
			return;
		}
	}

	// Token: 0x04002109 RID: 8457
	public MeshRenderer renderer;

	// Token: 0x0400210A RID: 8458
	public Material[] mat;

	// Token: 0x0400210B RID: 8459
	private Material newMat;

	// Token: 0x0400210C RID: 8460
	private float timer;

	// Token: 0x0400210D RID: 8461
	private GameObject main_;

	// Token: 0x0400210E RID: 8462
	private roomScript roomS_;

	// Token: 0x0400210F RID: 8463
	private mapScript mapS_;

	// Token: 0x04002110 RID: 8464
	private mainScript mS_;

	// Token: 0x04002111 RID: 8465
	private objectScript oS_;

	// Token: 0x04002112 RID: 8466
	private games games_;
}
