using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MitarberKuendigt : MonoBehaviour
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
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
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

	
	public void Init(characterScript cS_)
	{
		Debug.Log("MITARBEITER KÜNDIGT: " + cS_.myName);
		this.FindScripts();
		this.guiMain_.OpenMenu(false);
		if (cS_)
		{
			this.rS_ = cS_.roomS_;
			this.sfx_.PlaySound(41, false);
			string text = this.tS_.GetText(509);
			text = text.Replace("<NAME>", cS_.myName);
			this.uiObjects[0].GetComponent<Text>().text = text;
			cS_.RemoveObjectUsing();
			cS_.Entlassen(false);
		}
		else
		{
			this.BUTTON_Abbrechen();
		}
		if (this.settings_.hideKuendigungen)
		{
			this.BUTTON_Abbrechen();
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
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	
	public void BUTTON_JumpToRoom()
	{
		if (this.rS_ && this.guiMain_ && this.guiMain_.camera_)
		{
			this.guiMain_.camera_.transform.parent.position = new Vector3(this.rS_.uiPos.x, this.guiMain_.camera_.transform.parent.position.y, this.rS_.uiPos.z);
		}
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private settingsScript settings_;

	
	private roomScript rS_;
}
