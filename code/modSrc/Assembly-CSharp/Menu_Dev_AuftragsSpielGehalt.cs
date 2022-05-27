using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_AuftragsSpielGehalt : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void Init(gameScript game_)
	{
		this.FindScripts();
		if (game_)
		{
			this.gS_ = game_;
			string text = this.tS_.GetText(630);
			text = text.Replace("<NUM1>", this.mS_.GetMoney((long)this.gS_.auftragsspiel_gehalt, true));
			text = text.Replace("<NUM2>", this.mS_.GetMoney((long)this.gS_.auftragsspiel_bonus, true));
			text = text.Replace("<NUM3>", this.gS_.auftragsspiel_mindestbewertung.ToString() + "%");
			this.uiObjects[0].GetComponent<Text>().text = text;
			GameObject gameObject = GameObject.Find("PUB_" + this.gS_.publisherID.ToString());
			if (gameObject)
			{
				this.uiObjects[1].GetComponent<Image>().sprite = gameObject.GetComponent<publisherScript>().GetLogo();
			}
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Game(this.gS_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Game(this.gS_);
				}
			}
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		if (this.gS_)
		{
			this.mS_.Earn((long)this.gS_.auftragsspiel_gehalt, 6);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private gameScript gS_;

	
	public int myID;
}
