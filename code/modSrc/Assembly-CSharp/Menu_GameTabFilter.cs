using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_GameTabFilter : MonoBehaviour
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
		if (!this.main_)
		{
			return;
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

	
	private void OnEnable()
	{
		this.FindScripts();
		this.SetToggles();
	}

	
	public void Init(bool isMenuOpen_)
	{
		this.isMenuOpen = isMenuOpen_;
	}

	
	private void SetToggles()
	{
		for (int i = 0; i < this.mS_.gameTabFilter.Length; i++)
		{
			this.uiObjects[i].GetComponent<Toggle>().isOn = this.mS_.gameTabFilter[i];
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.isMenuOpen)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		for (int i = 0; i < this.mS_.gameTabFilter.Length; i++)
		{
			this.mS_.gameTabFilter[i] = this.uiObjects[i].GetComponent<Toggle>().isOn;
		}
		this.gameTabContent.GetComponent<GamesGroupContent>().timer = 10f;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
	}

	
	public GameObject gameTabContent;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private bool isMenuOpen;
}
