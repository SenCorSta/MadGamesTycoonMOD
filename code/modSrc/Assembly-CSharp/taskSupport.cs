using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class taskSupport : MonoBehaviour
{
	// Token: 0x06001BE7 RID: 7143 RVA: 0x000131FB File Offset: 0x000113FB
	private void Awake()
	{
		base.transform.position = new Vector3(200f, 0f, 0f);
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x0001321C File Offset: 0x0001141C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x00119A98 File Offset: 0x00117C98
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

	// Token: 0x06001BEA RID: 7146 RVA: 0x00013224 File Offset: 0x00011424
	public void Init(bool fromSavegame)
	{
		if (!fromSavegame)
		{
			this.myID = UnityEngine.Random.Range(1, 100000000);
		}
		base.name = "Task_" + this.myID.ToString();
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x00013255 File Offset: 0x00011455
	public float GetProzent()
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return this.mS_.GetAnrufe100Prozent();
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x00013275 File Offset: 0x00011475
	public Sprite GetPic()
	{
		return this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[0];
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x00119B40 File Offset: 0x00117D40
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

	// Token: 0x06001BEE RID: 7150 RVA: 0x00004174 File Offset: 0x00002374
	public void Abbrechen()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400231E RID: 8990
	public int myID = -1;

	// Token: 0x0400231F RID: 8991
	private GameObject main_;

	// Token: 0x04002320 RID: 8992
	public mainScript mS_;

	// Token: 0x04002321 RID: 8993
	private GUI_Main guiMain_;

	// Token: 0x04002322 RID: 8994
	private textScript tS_;

	// Token: 0x04002323 RID: 8995
	private roomDataScript rdS_;
}
