using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MessageBoxSave : MonoBehaviour
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

	
	public void Init(int id_, bool close)
	{
		this.FindScripts();
		this.guiMain_.OpenMenu(false);
		this.id = id_;
		this.closeMenu = close;
		this.uiObjects[1].GetComponent<Toggle>().isOn = false;
		if (PlayerPrefs.GetInt("MessageBoxSave_" + this.id.ToString()) == 1)
		{
			this.BUTTON_Yes();
		}
		int num = this.id;
		if (num == 0)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1442);
			return;
		}
		if (num != 1)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1443);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		if (this.uiObjects[1].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("MessageBoxSave_" + this.id.ToString(), 1);
		}
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		this.BUTTON_Abbrechen();
	}

	
	public void BUTTON_No()
	{
		if (this.uiObjects[1].GetComponent<Toggle>().isOn)
		{
			PlayerPrefs.SetInt("MessageBoxSave_" + this.id.ToString(), 1);
		}
		int num = this.id;
		if (num != 0)
		{
			if (num == 1)
			{
				this.guiMain_.uiObjects[2].GetComponent<Toggle>().isOn = false;
			}
		}
		else
		{
			this.guiMain_.uiObjects[3].GetComponent<Toggle>().isOn = false;
		}
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private bool closeMenu;

	
	private int id;
}
