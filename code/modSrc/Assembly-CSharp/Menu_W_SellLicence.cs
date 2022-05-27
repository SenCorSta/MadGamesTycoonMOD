using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A4 RID: 420
public class Menu_W_SellLicence : MonoBehaviour
{
	// Token: 0x06000FC0 RID: 4032 RVA: 0x0000B2B4 File Offset: 0x000094B4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000B46FC File Offset: 0x000B28FC
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

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0000B2BC File Offset: 0x000094BC
	public void Init(int id)
	{
		this.FindScripts();
		this.myID = id;
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetTooltip(this.myID);
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x0000B2EE File Offset: 0x000094EE
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x0000B309 File Offset: 0x00009509
	public void BUTTON_Yes()
	{
		this.licences_.Sell(this.myID);
		this.guiMain_.uiObjects[54].GetComponent<Menu_SellLicence>().Init();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001446 RID: 5190
	public GameObject[] uiObjects;

	// Token: 0x04001447 RID: 5191
	private platformScript pS_;

	// Token: 0x04001448 RID: 5192
	private GameObject main_;

	// Token: 0x04001449 RID: 5193
	private mainScript mS_;

	// Token: 0x0400144A RID: 5194
	private textScript tS_;

	// Token: 0x0400144B RID: 5195
	private GUI_Main guiMain_;

	// Token: 0x0400144C RID: 5196
	private sfxScript sfx_;

	// Token: 0x0400144D RID: 5197
	private licences licences_;

	// Token: 0x0400144E RID: 5198
	public int myID;
}
