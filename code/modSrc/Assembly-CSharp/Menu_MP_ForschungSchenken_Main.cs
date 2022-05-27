using System;
using UnityEngine;


public class Menu_MP_ForschungSchenken_Main : MonoBehaviour
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
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
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
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Genres()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(0);
	}

	
	public void BUTTON_Themen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(1);
	}

	
	public void BUTTON_EngineFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(2);
	}

	
	public void BUTTON_GameplayFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(3);
	}

	
	public void BUTTON_Hardware()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(4);
	}

	
	public void BUTTON_Sonstiges()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(5);
	}

	
	public void BUTTON_HardwareFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[266]);
		this.guiMain_.uiObjects[266].GetComponent<Menu_MP_ForschungSchenken>().Init(6);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private unlockScript unlock_;
}
