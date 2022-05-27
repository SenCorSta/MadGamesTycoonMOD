using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019B RID: 411
public class Menu_Umsatz_Konsole : MonoBehaviour
{
	// Token: 0x06000F87 RID: 3975 RVA: 0x0000B0A1 File Offset: 0x000092A1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x000B2F3C File Offset: 0x000B113C
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

	// Token: 0x06000F89 RID: 3977 RVA: 0x000B30F8 File Offset: 0x000B12F8
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

	// Token: 0x06000F8A RID: 3978 RVA: 0x0000B0A9 File Offset: 0x000092A9
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x000B33F4 File Offset: 0x000B15F4
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[335].activeSelf && !this.guiMain_.uiObjects[336].activeSelf && !this.guiMain_.uiObjects[343].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x040013F9 RID: 5113
	public GameObject[] uiObjects;

	// Token: 0x040013FA RID: 5114
	private GameObject main_;

	// Token: 0x040013FB RID: 5115
	private mainScript mS_;

	// Token: 0x040013FC RID: 5116
	private textScript tS_;

	// Token: 0x040013FD RID: 5117
	private GUI_Main guiMain_;

	// Token: 0x040013FE RID: 5118
	private sfxScript sfx_;

	// Token: 0x040013FF RID: 5119
	private genres genres_;

	// Token: 0x04001400 RID: 5120
	private themes themes_;

	// Token: 0x04001401 RID: 5121
	private licences licences_;

	// Token: 0x04001402 RID: 5122
	private engineFeatures eF_;

	// Token: 0x04001403 RID: 5123
	private cameraMovementScript cmS_;

	// Token: 0x04001404 RID: 5124
	private unlockScript unlock_;

	// Token: 0x04001405 RID: 5125
	private gameplayFeatures gF_;

	// Token: 0x04001406 RID: 5126
	private games games_;

	// Token: 0x04001407 RID: 5127
	private platforms platforms_;

	// Token: 0x04001408 RID: 5128
	private platformScript pS_;
}
