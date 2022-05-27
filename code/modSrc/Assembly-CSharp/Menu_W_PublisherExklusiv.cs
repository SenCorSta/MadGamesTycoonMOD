using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A2 RID: 418
public class Menu_W_PublisherExklusiv : MonoBehaviour
{
	// Token: 0x06000FC4 RID: 4036 RVA: 0x000A76DD File Offset: 0x000A58DD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x000A76E8 File Offset: 0x000A58E8
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

	// Token: 0x06000FC6 RID: 4038 RVA: 0x000A7794 File Offset: 0x000A5994
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

	// Token: 0x06000FC7 RID: 4039 RVA: 0x000A7892 File Offset: 0x000A5A92
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x000A78B0 File Offset: 0x000A5AB0
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

	// Token: 0x04001438 RID: 5176
	public GameObject[] uiObjects;

	// Token: 0x04001439 RID: 5177
	private GameObject main_;

	// Token: 0x0400143A RID: 5178
	private mainScript mS_;

	// Token: 0x0400143B RID: 5179
	private textScript tS_;

	// Token: 0x0400143C RID: 5180
	private GUI_Main guiMain_;

	// Token: 0x0400143D RID: 5181
	private sfxScript sfx_;

	// Token: 0x0400143E RID: 5182
	private publisherScript publisherS_;

	// Token: 0x0400143F RID: 5183
	public int laufzeit;

	// Token: 0x04001440 RID: 5184
	public int sofortzahlung;
}
