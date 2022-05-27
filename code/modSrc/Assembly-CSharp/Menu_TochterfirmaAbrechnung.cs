using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200019B RID: 411
public class Menu_TochterfirmaAbrechnung : MonoBehaviour
{
	// Token: 0x06000F99 RID: 3993 RVA: 0x000A5E8E File Offset: 0x000A408E
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000A5E98 File Offset: 0x000A4098
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

	// Token: 0x06000F9B RID: 3995 RVA: 0x000A6038 File Offset: 0x000A4238
	public void Init(gameScript game_)
	{
		Debug.Log("MENU_TOCHTERFIRMA_ABRECHNUNG");
		this.FindScripts();
		this.gS_ = game_;
		if (!this.gS_.devS_ && this.gS_.developerID != -1)
		{
			this.gS_.FindMyDeveloper();
		}
		if (!this.gS_.pS_)
		{
			this.gS_.FindMyPublisher();
		}
		if (!this.gS_ || !this.gS_.pS_ || !this.gS_.devS_)
		{
			Debug.Log("MENU_TOCHTERFIRMA_ABRECHNUNG(): Abbruch -> Script fehlt");
			this.BUTTON_Close();
			return;
		}
		if (!this.gS_.GetPublisherOrDeveloperIsTochterfirma())
		{
			Debug.Log("MENU_TOCHTERFIRMA_ABRECHNUNG(): Abbruch -> Ist nicht meine Tochterfirma");
			this.BUTTON_Close();
			return;
		}
		publisherScript publisherScript = null;
		if (this.gS_.GetPublisherIsTochtefirma())
		{
			publisherScript = this.gS_.pS_;
		}
		if (this.gS_.GetDeveloperIsTochtefirma())
		{
			publisherScript = this.gS_.devS_;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[6].GetComponent<Image>().sprite = publisherScript.GetLogo();
		string text = this.tS_.GetText(1988);
		if (this.gS_.GetPublisherIsTochtefirma())
		{
			text = text.Replace("<NAME1>", "<color=blue>" + this.gS_.pS_.GetName() + "</color>");
		}
		if (this.gS_.GetDeveloperIsTochtefirma())
		{
			text = text.Replace("<NAME1>", "<color=blue>" + this.gS_.devS_.GetName() + "</color>");
		}
		text = text.Replace("<NAME2>", "<color=blue>" + this.gS_.GetNameWithTag() + "</color>");
		this.uiObjects[2].GetComponent<Text>().text = text;
		text = "";
		text = text + this.tS_.GetText(491) + "\n";
		text = text + this.tS_.GetText(275) + "\n";
		text += "\n";
		text = text + this.tS_.GetText(276) + "\n";
		text = text + this.tS_.GetText(1990) + "\n";
		text = text + this.tS_.GetText(503) + "\n";
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.gS_.weeksOnMarket.ToString() + "\n";
		text = text + this.mS_.GetMoney(this.gS_.sellsTotal, false) + "\n";
		text += "\n";
		text = text + this.mS_.GetMoney(this.gS_.umsatzTotal, true) + "\n";
		text = text + "<color=red>" + this.mS_.GetMoney(this.gS_.umsatzTotal - this.gS_.tw_gewinnanteil * 2L, true) + "</color>\n";
		text = text + Mathf.RoundToInt(this.games_.tf_gewinnbeteiligungTochterfirma).ToString() + "%\n";
		this.uiObjects[4].GetComponent<Text>().text = text;
		this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.gS_.tw_gewinnanteil, true) + "</color>";
		if (this.mS_.settings_.disableTochterfirmaAbrechnung)
		{
			this.BUTTON_Close();
		}
		this.sfx_.PlaySound(40, false);
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x000A641E File Offset: 0x000A461E
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x000A6439 File Offset: 0x000A4639
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x040013F3 RID: 5107
	public GameObject[] uiObjects;

	// Token: 0x040013F4 RID: 5108
	private GameObject main_;

	// Token: 0x040013F5 RID: 5109
	private mainScript mS_;

	// Token: 0x040013F6 RID: 5110
	private textScript tS_;

	// Token: 0x040013F7 RID: 5111
	private GUI_Main guiMain_;

	// Token: 0x040013F8 RID: 5112
	private sfxScript sfx_;

	// Token: 0x040013F9 RID: 5113
	private genres genres_;

	// Token: 0x040013FA RID: 5114
	private themes themes_;

	// Token: 0x040013FB RID: 5115
	private licences licences_;

	// Token: 0x040013FC RID: 5116
	private engineFeatures eF_;

	// Token: 0x040013FD RID: 5117
	private cameraMovementScript cmS_;

	// Token: 0x040013FE RID: 5118
	private unlockScript unlock_;

	// Token: 0x040013FF RID: 5119
	private gameplayFeatures gF_;

	// Token: 0x04001400 RID: 5120
	private games games_;

	// Token: 0x04001401 RID: 5121
	private gameScript gS_;
}
