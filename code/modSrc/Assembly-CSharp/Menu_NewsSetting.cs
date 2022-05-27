using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_NewsSetting : MonoBehaviour
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
		this.Init();
	}

	
	private void Init()
	{
		this.uiObjects[0].GetComponent<Toggle>().isOn = this.mS_.newsSetting[0];
		this.uiObjects[1].GetComponent<Toggle>().isOn = this.mS_.newsSetting[1];
		this.uiObjects[2].GetComponent<Toggle>().isOn = this.mS_.newsSetting[2];
		this.uiObjects[3].GetComponent<Toggle>().isOn = this.mS_.newsSetting[3];
		this.uiObjects[4].GetComponent<Toggle>().isOn = this.mS_.newsSetting[4];
		this.uiObjects[5].GetComponent<Toggle>().isOn = this.mS_.newsSetting[5];
		this.uiObjects[6].GetComponent<Toggle>().isOn = this.mS_.newsSetting[6];
		this.uiObjects[7].GetComponent<Toggle>().isOn = this.mS_.newsSetting[7];
		this.uiObjects[8].GetComponent<Toggle>().isOn = this.mS_.newsSetting[8];
		this.uiObjects[9].GetComponent<Toggle>().isOn = this.mS_.newsSetting[9];
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		this.mS_.newsSetting[0] = this.uiObjects[0].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[1] = this.uiObjects[1].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[2] = this.uiObjects[2].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[3] = this.uiObjects[3].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[4] = this.uiObjects[4].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[5] = this.uiObjects[5].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[6] = this.uiObjects[6].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[7] = this.uiObjects[7].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[8] = this.uiObjects[8].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[9] = this.uiObjects[9].GetComponent<Toggle>().isOn;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;
}
