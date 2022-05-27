using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A1 RID: 417
public class Menu_W_PublisherExklusiv : MonoBehaviour
{
	// Token: 0x06000FAC RID: 4012 RVA: 0x0000B238 File Offset: 0x00009438
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x000B4014 File Offset: 0x000B2214
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000B40C0 File Offset: 0x000B22C0
	public void Init(publisherScript pS_)
	{
		this.FindScripts();
		if (!pS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.publisherS_ = pS_;
		this.laufzeit = pS_.exklusivLaufzeit;
		this.sofortzahlung = pS_.GetMoneyExklusiv();
		this.uiObjects[0].GetComponent<Image>().sprite = pS_.GetLogo();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.sofortzahlung, true);
		string text = this.tS_.GetText(1048);
		text = text.Replace("<NUM>", this.laufzeit.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1049);
		text = text.Replace("<NAME>", "<color=blue>" + pS_.GetName() + "</color>");
		this.uiObjects[3].GetComponent<Text>().text = text;
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x0000B240 File Offset: 0x00009440
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x000B41C0 File Offset: 0x000B23C0
	public void BUTTON_Yes()
	{
		if (this.publisherS_)
		{
			this.mS_.Earn((long)this.sofortzahlung, 1);
			this.mS_.exklusivVertrag_ID = this.publisherS_.myID;
			this.mS_.exklusivVertrag_laufzeit = this.laufzeit;
			if (this.mS_.achScript_)
			{
				this.mS_.achScript_.SetAchivement(42);
			}
		}
		this.guiMain_.uiObjects[200].GetComponent<Menu_PublisherExklusiv>().BUTTON_Close();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400142F RID: 5167
	public GameObject[] uiObjects;

	// Token: 0x04001430 RID: 5168
	private GameObject main_;

	// Token: 0x04001431 RID: 5169
	private mainScript mS_;

	// Token: 0x04001432 RID: 5170
	private textScript tS_;

	// Token: 0x04001433 RID: 5171
	private GUI_Main guiMain_;

	// Token: 0x04001434 RID: 5172
	private sfxScript sfx_;

	// Token: 0x04001435 RID: 5173
	private publisherScript publisherS_;

	// Token: 0x04001436 RID: 5174
	public int laufzeit;

	// Token: 0x04001437 RID: 5175
	public int sofortzahlung;
}
