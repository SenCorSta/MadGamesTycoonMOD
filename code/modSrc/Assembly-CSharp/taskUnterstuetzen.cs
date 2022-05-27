using System;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class taskUnterstuetzen : MonoBehaviour
{
	// Token: 0x06001C47 RID: 7239 RVA: 0x001178E5 File Offset: 0x00115AE5
	private void Awake()
	{
		base.transform.position = new Vector3(250f, 0f, 0f);
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x00117906 File Offset: 0x00115B06
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C49 RID: 7241 RVA: 0x00117910 File Offset: 0x00115B10
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
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
	}

	// Token: 0x06001C4A RID: 7242 RVA: 0x00117A14 File Offset: 0x00115C14
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C4B RID: 7243 RVA: 0x00117A45 File Offset: 0x00115C45
	private void Update()
	{
		this.FindMyRoom();
	}

	// Token: 0x06001C4C RID: 7244 RVA: 0x00117A50 File Offset: 0x00115C50
	private void FindMyRoom()
	{
		if (!this.rS_)
		{
			GameObject gameObject = GameObject.Find("Room_" + this.roomID.ToString());
			if (gameObject)
			{
				this.rS_ = gameObject.GetComponent<roomScript>();
				return;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x00117AA5 File Offset: 0x00115CA5
	public bool IsCrunchtime()
	{
		return this.rS_ && this.rS_.IsCrunchtimeRead();
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x00002715 File Offset: 0x00000915
	public void Work(float f, int what)
	{
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x00002715 File Offset: 0x00000915
	private void CompleteFeature()
	{
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x00002715 File Offset: 0x00000915
	private void Complete()
	{
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x00117AC1 File Offset: 0x00115CC1
	public void Abbrechen()
	{
		this.FindScripts();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002348 RID: 9032
	public int myID = -1;

	// Token: 0x04002349 RID: 9033
	public int roomID = -1;

	// Token: 0x0400234A RID: 9034
	public roomScript rS_;

	// Token: 0x0400234B RID: 9035
	private GameObject main_;

	// Token: 0x0400234C RID: 9036
	private mainScript mS_;

	// Token: 0x0400234D RID: 9037
	private engineFeatures eF_;

	// Token: 0x0400234E RID: 9038
	private gameplayFeatures gF_;

	// Token: 0x0400234F RID: 9039
	private GUI_Main guiMain_;

	// Token: 0x04002350 RID: 9040
	private textScript tS_;

	// Token: 0x04002351 RID: 9041
	private roomDataScript rdS_;

	// Token: 0x04002352 RID: 9042
	private sfxScript sfx_;
}
