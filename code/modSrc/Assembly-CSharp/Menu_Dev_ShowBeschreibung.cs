using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_ShowBeschreibung : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		if (this.gS_ == null)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		if (this.gS_.beschreibung == null)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		if (this.gS_.beschreibung.Length <= 0)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		this.uiObjects[1].GetComponent<InputField>().text = this.gS_.beschreibung;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		this.BUTTON_Close();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private Menu_DevGame mDevGame_;

	
	private gameScript gS_;
}
