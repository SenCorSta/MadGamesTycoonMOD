using System;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class taskSupport : MonoBehaviour
{
	// Token: 0x06001C31 RID: 7217 RVA: 0x001172D5 File Offset: 0x001154D5
	private void Awake()
	{
		base.transform.position = new Vector3(200f, 0f, 0f);
	}

	// Token: 0x06001C32 RID: 7218 RVA: 0x001172F6 File Offset: 0x001154F6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C33 RID: 7219 RVA: 0x00117300 File Offset: 0x00115500
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
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x001173A6 File Offset: 0x001155A6
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001C35 RID: 7221 RVA: 0x001173D7 File Offset: 0x001155D7
	public float GetProzent()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return this.mS_.GetAnrufe100Prozent();
	}

	// Token: 0x06001C36 RID: 7222 RVA: 0x001173F7 File Offset: 0x001155F7
	public Sprite GetPic()
	{
		return this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[0];
	}

	// Token: 0x06001C37 RID: 7223 RVA: 0x00117414 File Offset: 0x00115614
	public void Work(float f)
	{
		if (!this.mS_)
		{
			return;
		}
		if (this.mS_.anrufe > 0)
		{
			this.mS_.anrufe -= 15 + Mathf.RoundToInt(f * 1.5f);
			if (this.mS_.anrufe <= 0)
			{
				this.mS_.anrufe = 0;
			}
		}
	}

	// Token: 0x06001C38 RID: 7224 RVA: 0x0003D679 File Offset: 0x0003B879
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002338 RID: 9016
	public int myID = -1;

	// Token: 0x04002339 RID: 9017
	private GameObject main_;

	// Token: 0x0400233A RID: 9018
	public mainScript mS_;

	// Token: 0x0400233B RID: 9019
	private GUI_Main guiMain_;

	// Token: 0x0400233C RID: 9020
	private textScript tS_;

	// Token: 0x0400233D RID: 9021
	private roomDataScript rdS_;
}
