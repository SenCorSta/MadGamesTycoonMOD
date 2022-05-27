using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_BuyAntiCheat : MonoBehaviour
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

	
	public void Init(antiCheatScript script_)
	{
		if (!script_)
		{
			return;
		}
		this.acS_ = script_;
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetTooltip();
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.acS_.inBesitz = true;
		this.mS_.Pay((long)this.acS_.GetPrice(), 6);
		this.guiMain_.uiObjects[234].GetComponent<Menu_BuyAntiCheat>().TAB_AntiCheatBuy(0);
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private antiCheatScript acS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;
}
