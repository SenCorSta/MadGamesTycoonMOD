using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A2 RID: 418
public class Menu_W_PublisherExklusivKuendigen : MonoBehaviour
{
	// Token: 0x06000FB2 RID: 4018 RVA: 0x0000B25B File Offset: 0x0000945B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x000B425C File Offset: 0x000B245C
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

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0000B263 File Offset: 0x00009463
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x000B4308 File Offset: 0x000B2508
	public void Init()
	{
		this.FindScripts();
		this.pS_ = null;
		this.pS_ = this.mS_.GetExklusivPublisher();
		if (!this.pS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.pS_.GetLogo();
		string text = this.tS_.GetText(1050);
		text = text.Replace("<NUM>", "<color=blue>" + this.mS_.exklusivVertrag_laufzeit.ToString() + "</color>");
		text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f));
		long strafzahlung = this.GetStrafzahlung();
		text = this.tS_.GetText(1914);
		text = text.Replace("<NUM>", "<color=blue>" + this.mS_.GetMoney(strafzahlung, true) + "</color>");
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x000B4458 File Offset: 0x000B2658
	public long GetStrafzahlung()
	{
		if (this.pS_)
		{
			return (long)Mathf.RoundToInt((float)((this.mS_.year - 1975) * 250000) * (this.pS_.stars / 100f) / 120f * (float)this.mS_.exklusivVertrag_laufzeit * 2.5f + (float)(this.mS_.exklusivVertrag_laufzeit * (30000 * (this.mS_.difficulty + 1))));
		}
		return 0L;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0000B26B File Offset: 0x0000946B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000B44E0 File Offset: 0x000B26E0
	public void BUTTON_Kuendigen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.pS_)
		{
			this.guiMain_.uiObjects[383].SetActive(true);
			this.guiMain_.uiObjects[383].GetComponent<Menu_W_PublisherKuendigen_MB>().Init(this.pS_);
			return;
		}
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
	private publisherScript pS_;
}
