using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012B RID: 299
public class Menu_Dev_AuftragsSpielEnd : MonoBehaviour
{
	// Token: 0x06000A93 RID: 2707 RVA: 0x00007916 File Offset: 0x00005B16
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x00083BB4 File Offset: 0x00081DB4
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
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00083D54 File Offset: 0x00081F54
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		if (this.mS_.achScript_)
		{
			this.mS_.achScript_.SetAchivement(26);
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		float num = (float)this.gS_.reviewTotal / 20f;
		if (!this.gS_.auftragsspiel_zeitAbgelaufen)
		{
			this.uiObjects[2].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
			this.uiObjects[3].GetComponent<Text>().text = "<color=green>+" + this.mS_.Round(num, 2) + "</color>";
			this.guiMain_.UpdateAuftragsansehen(num);
		}
		else
		{
			this.uiObjects[2].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			this.uiObjects[3].GetComponent<Text>().text = "<color=red>-5.0%</color>";
			this.guiMain_.UpdateAuftragsansehen(-5f);
		}
		string text = this.tS_.GetText(626);
		text = text.Replace("<NUM>", this.gS_.auftragsspiel_mindestbewertung.ToString());
		this.uiObjects[4].GetComponent<Text>().text = text;
		long num2;
		if (this.gS_.reviewTotal >= this.gS_.auftragsspiel_mindestbewertung)
		{
			this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
			this.uiObjects[6].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney((long)this.gS_.auftragsspiel_bonus, true) + "</color>";
			this.mS_.Earn((long)this.gS_.auftragsspiel_bonus, 6);
			this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.gS_.auftragsspiel_bonus + this.gS_.auftragsspiel_gehalt), true);
			num2 = (long)(this.gS_.auftragsspiel_bonus + this.gS_.auftragsspiel_gehalt) - this.gS_.GetEntwicklungskosten();
		}
		else
		{
			this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			this.uiObjects[6].GetComponent<Text>().text = "<color=black>$0</color>";
			this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gS_.auftragsspiel_gehalt, true);
			num2 = (long)this.gS_.auftragsspiel_gehalt - this.gS_.GetEntwicklungskosten();
		}
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(this.gS_.GetEntwicklungskosten(), true);
		if (num2 > 0L)
		{
			this.uiObjects[9].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(num2, true) + "</color>";
			return;
		}
		this.uiObjects[9].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(num2, true) + "</color>";
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0000791E File Offset: 0x00005B1E
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x00007939 File Offset: 0x00005B39
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x04000ECB RID: 3787
	public GameObject[] uiObjects;

	// Token: 0x04000ECC RID: 3788
	private GameObject main_;

	// Token: 0x04000ECD RID: 3789
	private mainScript mS_;

	// Token: 0x04000ECE RID: 3790
	private textScript tS_;

	// Token: 0x04000ECF RID: 3791
	private GUI_Main guiMain_;

	// Token: 0x04000ED0 RID: 3792
	private sfxScript sfx_;

	// Token: 0x04000ED1 RID: 3793
	private genres genres_;

	// Token: 0x04000ED2 RID: 3794
	private themes themes_;

	// Token: 0x04000ED3 RID: 3795
	private licences licences_;

	// Token: 0x04000ED4 RID: 3796
	private engineFeatures eF_;

	// Token: 0x04000ED5 RID: 3797
	private cameraMovementScript cmS_;

	// Token: 0x04000ED6 RID: 3798
	private unlockScript unlock_;

	// Token: 0x04000ED7 RID: 3799
	private gameplayFeatures gF_;

	// Token: 0x04000ED8 RID: 3800
	private games games_;

	// Token: 0x04000ED9 RID: 3801
	private gameScript gS_;
}
