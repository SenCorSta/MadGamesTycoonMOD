using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200018F RID: 399
public class Menu_Engine_Abrechnung : MonoBehaviour
{
	// Token: 0x06000F1F RID: 3871 RVA: 0x0000ABD2 File Offset: 0x00008DD2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x000ADFFC File Offset: 0x000AC1FC
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

	// Token: 0x06000F21 RID: 3873 RVA: 0x000AE19C File Offset: 0x000AC39C
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.gS_.FindMyEngineNew();
		Debug.Log("ENGINE ABRECHNUNG: " + game_.myName + " / " + game_.myID.ToString());
		if (this.gS_.engineS_ && (!this.gS_.engineS_.sellEngine || this.gS_.engineS_.gewinnbeteiligung <= 0))
		{
			this.BUTTON_Close();
			return;
		}
		if (!this.gS_.devS_ && this.gS_.developerID != -1)
		{
			this.gS_.FindMyDeveloper();
		}
		if (!this.gS_.pS_)
		{
			this.gS_.FindMyPublisher();
		}
		if (!this.gS_ || !this.gS_.engineS_ || !this.gS_.pS_)
		{
			Debug.Log("MENU_ENGINE_ABRECHNUNG(): Abbruch");
			this.BUTTON_Close();
			return;
		}
		if (this.gS_.devS_ && this.gS_.devS_.IsMyTochterfirma())
		{
			Debug.Log("MENU_ENGINE_ABRECHNUNG(): Abbruch -> Ist meine Tochterfirma");
			this.BUTTON_Close();
			return;
		}
		string newValue = "";
		if (this.gS_.devS_)
		{
			newValue = this.gS_.devS_.GetName();
		}
		if (this.gS_.developerID == -1)
		{
			newValue = this.mS_.companyName;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.engineS_.GetName();
		string text = this.tS_.GetText(502);
		text = text.Replace("<NAME1>", newValue);
		text = text.Replace("<NAME2>", this.gS_.GetNameWithTag());
		this.uiObjects[2].GetComponent<Text>().text = text;
		if (this.gS_.IsMyAuftragsspiel())
		{
			text = this.tS_.GetText(1815);
			text = text.Replace("<NAME1>", this.gS_.GetNameWithTag());
			this.uiObjects[2].GetComponent<Text>().text = text;
		}
		text = "";
		text = text + this.tS_.GetText(491) + "\n";
		text = text + this.tS_.GetText(275) + "\n";
		text += "\n";
		text = text + this.tS_.GetText(489) + "\n";
		text = text + this.tS_.GetText(503) + "\n";
		text = text + this.tS_.GetText(504) + "\n";
		this.uiObjects[3].GetComponent<Text>().text = text;
		text = "";
		text = text + this.gS_.weeksOnMarket.ToString() + "\n";
		text = text + this.mS_.GetMoney(this.gS_.sellsTotal, false) + "\n";
		text += "\n";
		text = text + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "\n";
		text = text + this.gS_.engineS_.gewinnbeteiligung + "%\n";
		float num = (float)this.gS_.GetGesamtGewinn();
		num = num / 100f * (float)this.gS_.engineS_.gewinnbeteiligung / (float)this.gS_.sellsTotal;
		text = text + this.mS_.Round(num, 2).ToString() + "$\n";
		this.uiObjects[4].GetComponent<Text>().text = text;
		if (num <= 0f)
		{
			Debug.Log("ENGINE ABRECHNUNG: ABBRUCH -> " + this.gS_.myID);
			this.BUTTON_Close();
			return;
		}
		num = (float)this.gS_.GetGesamtGewinn();
		num = num / 100f * (float)this.gS_.engineS_.gewinnbeteiligung;
		this.uiObjects[5].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney((long)Mathf.RoundToInt(num), true) + "</color>";
		this.gS_.engineS_.umsatz += Mathf.RoundToInt(num);
		this.mS_.Earn((long)Mathf.RoundToInt(num), 4);
		if (this.mS_.settings_.disableEngineAbrechnung)
		{
			this.BUTTON_Close();
		}
		this.sfx_.PlaySound(40, false);
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0000ABDA File Offset: 0x00008DDA
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x0000ABF5 File Offset: 0x00008DF5
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x0400136D RID: 4973
	public GameObject[] uiObjects;

	// Token: 0x0400136E RID: 4974
	private GameObject main_;

	// Token: 0x0400136F RID: 4975
	private mainScript mS_;

	// Token: 0x04001370 RID: 4976
	private textScript tS_;

	// Token: 0x04001371 RID: 4977
	private GUI_Main guiMain_;

	// Token: 0x04001372 RID: 4978
	private sfxScript sfx_;

	// Token: 0x04001373 RID: 4979
	private genres genres_;

	// Token: 0x04001374 RID: 4980
	private themes themes_;

	// Token: 0x04001375 RID: 4981
	private licences licences_;

	// Token: 0x04001376 RID: 4982
	private engineFeatures eF_;

	// Token: 0x04001377 RID: 4983
	private cameraMovementScript cmS_;

	// Token: 0x04001378 RID: 4984
	private unlockScript unlock_;

	// Token: 0x04001379 RID: 4985
	private gameplayFeatures gF_;

	// Token: 0x0400137A RID: 4986
	private games games_;

	// Token: 0x0400137B RID: 4987
	private gameScript gS_;
}
