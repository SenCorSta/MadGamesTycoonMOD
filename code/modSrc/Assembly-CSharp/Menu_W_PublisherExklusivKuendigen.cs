using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A3 RID: 419
public class Menu_W_PublisherExklusivKuendigen : MonoBehaviour
{
	// Token: 0x06000FCA RID: 4042 RVA: 0x000A793D File Offset: 0x000A5B3D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x000A7948 File Offset: 0x000A5B48
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

	// Token: 0x06000FCC RID: 4044 RVA: 0x000A79F2 File Offset: 0x000A5BF2
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x000A79FC File Offset: 0x000A5BFC
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

	// Token: 0x06000FCE RID: 4046 RVA: 0x000A7B4C File Offset: 0x000A5D4C
	public long GetStrafzahlung()
	{
		if (this.pS_)
		{
			return (long)Mathf.RoundToInt((float)((this.mS_.year - 1975) * 250000) * (this.pS_.stars / 100f) / 120f * (float)this.mS_.exklusivVertrag_laufzeit * 2.5f + (float)(this.mS_.exklusivVertrag_laufzeit * (30000 * (this.mS_.difficulty + 1))));
		}
		return 0L;
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x000A7BD3 File Offset: 0x000A5DD3
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x000A7BFC File Offset: 0x000A5DFC
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

	// Token: 0x04001441 RID: 5185
	public GameObject[] uiObjects;

	// Token: 0x04001442 RID: 5186
	private GameObject main_;

	// Token: 0x04001443 RID: 5187
	private mainScript mS_;

	// Token: 0x04001444 RID: 5188
	private textScript tS_;

	// Token: 0x04001445 RID: 5189
	private GUI_Main guiMain_;

	// Token: 0x04001446 RID: 5190
	private sfxScript sfx_;

	// Token: 0x04001447 RID: 5191
	private publisherScript pS_;
}
