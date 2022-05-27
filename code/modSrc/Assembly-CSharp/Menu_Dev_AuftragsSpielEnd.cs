using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012C RID: 300
public class Menu_Dev_AuftragsSpielEnd : MonoBehaviour
{
	// Token: 0x06000AA4 RID: 2724 RVA: 0x00073595 File Offset: 0x00071795
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x000735A0 File Offset: 0x000717A0
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

	// Token: 0x06000AA6 RID: 2726 RVA: 0x00073740 File Offset: 0x00071940
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

	// Token: 0x06000AA7 RID: 2727 RVA: 0x00073A96 File Offset: 0x00071C96
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x00073AB1 File Offset: 0x00071CB1
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x04000ED3 RID: 3795
	public GameObject[] uiObjects;

	// Token: 0x04000ED4 RID: 3796
	private GameObject main_;

	// Token: 0x04000ED5 RID: 3797
	private mainScript mS_;

	// Token: 0x04000ED6 RID: 3798
	private textScript tS_;

	// Token: 0x04000ED7 RID: 3799
	private GUI_Main guiMain_;

	// Token: 0x04000ED8 RID: 3800
	private sfxScript sfx_;

	// Token: 0x04000ED9 RID: 3801
	private genres genres_;

	// Token: 0x04000EDA RID: 3802
	private themes themes_;

	// Token: 0x04000EDB RID: 3803
	private licences licences_;

	// Token: 0x04000EDC RID: 3804
	private engineFeatures eF_;

	// Token: 0x04000EDD RID: 3805
	private cameraMovementScript cmS_;

	// Token: 0x04000EDE RID: 3806
	private unlockScript unlock_;

	// Token: 0x04000EDF RID: 3807
	private gameplayFeatures gF_;

	// Token: 0x04000EE0 RID: 3808
	private games games_;

	// Token: 0x04000EE1 RID: 3809
	private gameScript gS_;
}
