using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A0 RID: 416
public class Menu_W_BuyLicence : MonoBehaviour
{
	// Token: 0x06000FA5 RID: 4005 RVA: 0x0000B193 File Offset: 0x00009393
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x000B3F4C File Offset: 0x000B214C
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

	// Token: 0x06000FA7 RID: 4007 RVA: 0x0000B19B File Offset: 0x0000939B
	public void Init(int id)
	{
		this.FindScripts();
		this.myID = id;
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetTooltip(this.myID);
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x0000B1CD File Offset: 0x000093CD
	private void Update()
	{
		if (this.licences_.licence_ANGEBOT[this.myID] <= 0)
		{
			this.BUTTON_Abbrechen();
			return;
		}
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x0000B1EB File Offset: 0x000093EB
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x0000B206 File Offset: 0x00009406
	public void BUTTON_Yes()
	{
		this.licences_.Buy(this.myID);
		this.guiMain_.uiObjects[52].GetComponent<Menu_BuyLicence>().TAB_LicenceBuy(0);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001426 RID: 5158
	public GameObject[] uiObjects;

	// Token: 0x04001427 RID: 5159
	private platformScript pS_;

	// Token: 0x04001428 RID: 5160
	private GameObject main_;

	// Token: 0x04001429 RID: 5161
	private mainScript mS_;

	// Token: 0x0400142A RID: 5162
	private textScript tS_;

	// Token: 0x0400142B RID: 5163
	private GUI_Main guiMain_;

	// Token: 0x0400142C RID: 5164
	private sfxScript sfx_;

	// Token: 0x0400142D RID: 5165
	private licences licences_;

	// Token: 0x0400142E RID: 5166
	public int myID;
}
