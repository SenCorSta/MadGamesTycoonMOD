using System;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class QAScreen : MonoBehaviour
{
	// Token: 0x060019C2 RID: 6594 RVA: 0x00011607 File Offset: 0x0000F807
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x0010D60C File Offset: 0x0010B80C
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

	// Token: 0x060019C4 RID: 6596 RVA: 0x0010D6B8 File Offset: 0x0010B8B8
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

	// Token: 0x040020EF RID: 8431
	public MeshRenderer renderer;

	// Token: 0x040020F0 RID: 8432
	public Material[] mat;

	// Token: 0x040020F1 RID: 8433
	private Material newMat;

	// Token: 0x040020F2 RID: 8434
	private float timer;

	// Token: 0x040020F3 RID: 8435
	private GameObject main_;

	// Token: 0x040020F4 RID: 8436
	private roomScript roomS_;

	// Token: 0x040020F5 RID: 8437
	private mapScript mapS_;

	// Token: 0x040020F6 RID: 8438
	private mainScript mS_;

	// Token: 0x040020F7 RID: 8439
	private objectScript oS_;

	// Token: 0x040020F8 RID: 8440
	private games games_;
}
