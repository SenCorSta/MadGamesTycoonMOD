using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A1 RID: 417
public class Menu_W_BuyLicence : MonoBehaviour
{
	// Token: 0x06000FBD RID: 4029 RVA: 0x000A756F File Offset: 0x000A576F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000A7578 File Offset: 0x000A5778
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

	// Token: 0x06000FBF RID: 4031 RVA: 0x000A7640 File Offset: 0x000A5840
	public void Init(int id)
	{
		this.FindScripts();
		this.myID = id;
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetTooltip(this.myID);
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x000A7672 File Offset: 0x000A5872
	private void Update()
	{
		if (this.licences_.licence_ANGEBOT[this.myID] <= 0)
		{
			this.BUTTON_Abbrechen();
			return;
		}
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000A7690 File Offset: 0x000A5890
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x000A76AB File Offset: 0x000A58AB
	public void BUTTON_Yes()
	{
		this.licences_.Buy(this.myID);
		this.guiMain_.uiObjects[52].GetComponent<Menu_BuyLicence>().TAB_LicenceBuy(0);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400142F RID: 5167
	public GameObject[] uiObjects;

	// Token: 0x04001430 RID: 5168
	private platformScript pS_;

	// Token: 0x04001431 RID: 5169
	private GameObject main_;

	// Token: 0x04001432 RID: 5170
	private mainScript mS_;

	// Token: 0x04001433 RID: 5171
	private textScript tS_;

	// Token: 0x04001434 RID: 5172
	private GUI_Main guiMain_;

	// Token: 0x04001435 RID: 5173
	private sfxScript sfx_;

	// Token: 0x04001436 RID: 5174
	private licences licences_;

	// Token: 0x04001437 RID: 5175
	public int myID;
}
