using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_FinanzJahr : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
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

	
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.SetData();
	}

	
	private void SetData()
	{
		string text = "";
		text = text + "<b>" + this.tS_.GetText(711) + "</b>\n";
		text = text + this.tS_.GetText(707) + "\n";
		text = text + this.tS_.GetText(708) + "\n";
		text = text + this.tS_.GetText(19) + "\n";
		text = text + this.tS_.GetText(20) + "\n";
		text = text + this.tS_.GetText(117) + "\n";
		text = text + this.tS_.GetText(747) + "\n";
		text = text + this.tS_.GetText(530) + "\n";
		text = text + this.tS_.GetText(1656) + "\n";
		text = text + this.tS_.GetText(1655) + "\n";
		text = text + this.tS_.GetText(709) + "\n";
		text = text + this.tS_.GetText(570) + "\n";
		text = text + this.tS_.GetText(1842) + "\n";
		text = text + this.tS_.GetText(710) + "\n";
		text = text + this.tS_.GetText(211) + "\n";
		text = text + this.tS_.GetText(734) + "\n";
		text = text + this.tS_.GetText(1923) + "\n";
		text = text + this.tS_.GetText(163) + "\n";
		text += "\n";
		text = text + "<b>" + this.tS_.GetText(712) + "</b>\n";
		text = text + this.tS_.GetText(713) + "\n";
		text = text + this.tS_.GetText(1236) + "\n";
		text = text + this.tS_.GetText(1177) + "\n";
		text = text + this.tS_.GetText(714) + "\n";
		text = text + this.tS_.GetText(715) + "\n";
		text = text + this.tS_.GetText(716) + "\n";
		text = text + this.tS_.GetText(943) + "\n";
		text = text + this.tS_.GetText(708) + "\n";
		text = text + this.tS_.GetText(570) + "\n";
		text = text + this.tS_.GetText(1842) + "\n";
		text = text + this.tS_.GetText(1923) + "\n";
		text += this.tS_.GetText(163);
		this.uiObjects[0].GetComponent<Text>().text = text;
		text = "<color=red>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[0], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[1], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[2], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[3], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[4], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[5], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[12], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[13], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[14], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[6], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[7], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[15], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[8], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[10], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[11], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[16], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[9], true) + "\n";
		text += "</color>\n";
		text += "<color=green>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[50], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[57], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[58], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[51], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[52], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[53], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[59], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[54], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[55], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[60], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahr[61], true) + "\n";
		text += this.mS_.GetMoney(this.mS_.finanzenJahr[56], true);
		text += "</color>";
		this.uiObjects[1].GetComponent<Text>().text = text;
		text = "<color=red>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[0], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[1], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[2], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[3], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[4], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[5], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[12], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[13], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[14], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[6], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[7], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[15], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[8], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[10], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[11], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[16], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[9], true) + "\n";
		text += "</color>\n";
		text += "<color=green>\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[50], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[57], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[58], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[51], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[52], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[53], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[59], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[54], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[55], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[60], true) + "\n";
		text = text + this.mS_.GetMoney(this.mS_.finanzenJahrLast[61], true) + "\n";
		text += this.mS_.GetMoney(this.mS_.finanzenJahrLast[56], true);
		text += "</color>";
		this.uiObjects[3].GetComponent<Text>().text = text;
		if (this.mS_.finanzenJahr_GetGewinn() < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.finanzenJahr_GetGewinn(), true) + "</color>";
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.mS_.finanzenJahr_GetGewinn(), true) + "</color>";
		}
		if (this.mS_.finanzenJahrLast_GetGewinn() < 0L)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=red>" + this.mS_.GetMoney(this.mS_.finanzenJahrLast_GetGewinn(), true) + "</color>";
			return;
		}
		this.uiObjects[4].GetComponent<Text>().text = "<color=green>" + this.mS_.GetMoney(this.mS_.finanzenJahrLast_GetGewinn(), true) + "</color>";
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;
}
