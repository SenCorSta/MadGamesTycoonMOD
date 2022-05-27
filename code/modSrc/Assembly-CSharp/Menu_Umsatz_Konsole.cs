using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019C RID: 412
public class Menu_Umsatz_Konsole : MonoBehaviour
{
	// Token: 0x06000F9F RID: 3999 RVA: 0x000A646B File Offset: 0x000A466B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x000A6474 File Offset: 0x000A4674
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000A6630 File Offset: 0x000A4830
	public void Init(platformScript plat_)
	{
		this.FindScripts();
		this.pS_ = plat_;
		if (!this.pS_)
		{
			this.BUTTON_Close();
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetManufacturer();
		this.pS_.SetPic(this.uiObjects[7]);
		string text = "";
		text = text + this.tS_.GetText(491) + "\n";
		text = text + this.tS_.GetText(696) + "\n";
		text += "\n";
		text = text + this.tS_.GetText(6) + "\n";
		text = text + this.tS_.GetText(492) + "\n";
		text = text + this.tS_.GetText(530) + "\n";
		text += "\n";
		text += this.tS_.GetText(1239);
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.pS_.weeksOnMarket.ToString() + "\n";
		text = text + this.mS_.GetMoney((long)this.pS_.units, false) + "\n";
		text += "\n";
		text += "<color=red>";
		text = text + this.mS_.GetMoney(this.pS_.entwicklungsKosten, true) + "\n";
		text = text + this.mS_.GetMoney((long)this.pS_.costs_marketing, true) + "\n";
		text = text + this.mS_.GetMoney(this.pS_.costs_production, true) + "\n";
		text += "</color>";
		text += "\n";
		text += "<color=green>";
		text += this.mS_.GetMoney(this.pS_.umsatzTotal, true);
		text += "</color>";
		this.uiObjects[4].GetComponent<Text>().text = text;
		if (this.pS_.GetGesamtGewinn() < 0L)
		{
			this.uiObjects[5].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.pS_.GetGesamtGewinn(), true) + "</color>";
			return;
		}
		this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.pS_.GetGesamtGewinn(), true) + "</color>";
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x000A692A File Offset: 0x000A4B2A
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x000A6948 File Offset: 0x000A4B48
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[335].activeSelf && !this.guiMain_.uiObjects[336].activeSelf && !this.guiMain_.uiObjects[343].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x04001402 RID: 5122
	public GameObject[] uiObjects;

	// Token: 0x04001403 RID: 5123
	private GameObject main_;

	// Token: 0x04001404 RID: 5124
	private mainScript mS_;

	// Token: 0x04001405 RID: 5125
	private textScript tS_;

	// Token: 0x04001406 RID: 5126
	private GUI_Main guiMain_;

	// Token: 0x04001407 RID: 5127
	private sfxScript sfx_;

	// Token: 0x04001408 RID: 5128
	private genres genres_;

	// Token: 0x04001409 RID: 5129
	private themes themes_;

	// Token: 0x0400140A RID: 5130
	private licences licences_;

	// Token: 0x0400140B RID: 5131
	private engineFeatures eF_;

	// Token: 0x0400140C RID: 5132
	private cameraMovementScript cmS_;

	// Token: 0x0400140D RID: 5133
	private unlockScript unlock_;

	// Token: 0x0400140E RID: 5134
	private gameplayFeatures gF_;

	// Token: 0x0400140F RID: 5135
	private games games_;

	// Token: 0x04001410 RID: 5136
	private platforms platforms_;

	// Token: 0x04001411 RID: 5137
	private platformScript pS_;
}
