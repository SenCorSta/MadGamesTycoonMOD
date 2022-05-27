using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A5 RID: 421
public class Menu_W_SellLicence : MonoBehaviour
{
	// Token: 0x06000FD8 RID: 4056 RVA: 0x000A7E45 File Offset: 0x000A6045
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x000A7E50 File Offset: 0x000A6050
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x000A7F18 File Offset: 0x000A6118
	public void Init(int id)
	{
		this.FindScripts();
		this.myID = id;
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetTooltip(this.myID);
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x000A7F4A File Offset: 0x000A614A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x000A7F65 File Offset: 0x000A6165
	public void BUTTON_Yes()
	{
		this.licences_.Sell(this.myID);
		this.guiMain_.uiObjects[54].GetComponent<Menu_SellLicence>().Init();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400144F RID: 5199
	public GameObject[] uiObjects;

	// Token: 0x04001450 RID: 5200
	private platformScript pS_;

	// Token: 0x04001451 RID: 5201
	private GameObject main_;

	// Token: 0x04001452 RID: 5202
	private mainScript mS_;

	// Token: 0x04001453 RID: 5203
	private textScript tS_;

	// Token: 0x04001454 RID: 5204
	private GUI_Main guiMain_;

	// Token: 0x04001455 RID: 5205
	private sfxScript sfx_;

	// Token: 0x04001456 RID: 5206
	private licences licences_;

	// Token: 0x04001457 RID: 5207
	public int myID;
}
