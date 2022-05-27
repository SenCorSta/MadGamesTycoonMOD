using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Immobilien : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
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
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
	}

	
	public void Init(roomScript script_)
	{
		this.FindScripts();
		this.rS_ = script_;
		if (!this.rS_)
		{
			this.BUTTON_Close();
			return;
		}
		int count = this.rS_.listGameObjects.Count;
		string text = this.tS_.GetText(1065);
		text = text.Replace("<NUM1>", count.ToString());
		text = text.Replace("<NUM2>", this.mS_.GetMoney((long)this.GetPreis(), true));
		this.uiObjects[0].GetComponent<Text>().text = text;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public void BUTTON_OK()
	{
		int preis = this.GetPreis();
		if (this.mS_.NotEnoughMoney(preis))
		{
			this.guiMain_.ShowNoMoney();
			return;
		}
		this.mS_.Pay((long)preis, 19);
		this.mS_.buildings[this.rS_.myID] = true;
		this.rS_.Demolish();
		this.mS_.SendSystemMessage("<IMMOBILIE>");
		this.BUTTON_Close();
	}

	
	private int GetPreis()
	{
		int count = this.rS_.listGameObjects.Count;
		int num = count * ((this.mS_.difficulty + 1) * 1600);
		if (count <= 100)
		{
			num = num;
		}
		if (count > 100 && count <= 200)
		{
			num *= 2;
		}
		if (count > 200 && count <= 300)
		{
			num *= 5;
		}
		if (count > 300 && count <= 400)
		{
			num *= 10;
		}
		if (count > 400 && count <= 500)
		{
			num *= 15;
		}
		if (count > 500 && count <= 600)
		{
			num *= 20;
		}
		if (count > 600)
		{
			num *= 30;
		}
		if (this.mS_.globalEvent == 6)
		{
			num *= 2;
		}
		if (this.mS_.globalEvent == 8)
		{
			num /= 2;
		}
		return num;
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private roomScript rS_;
}
