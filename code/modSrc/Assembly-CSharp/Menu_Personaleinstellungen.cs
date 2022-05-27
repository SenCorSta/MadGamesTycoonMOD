using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Personaleinstellungen : MonoBehaviour
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

	
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Dropdown>().value = this.mS_.personal_pausen;
		this.uiObjects[1].GetComponent<Dropdown>().value = this.mS_.personal_druck;
		this.uiObjects[4].GetComponent<Slider>().value = (float)this.mS_.personal_motivation;
		this.uiObjects[5].GetComponent<Slider>().value = (float)this.mS_.personal_crunch;
		this.uiObjects[6].GetComponent<Toggle>().isOn = this.mS_.personal_dontLeaveBuilding;
		this.uiObjects[7].GetComponent<Toggle>().isOn = this.mS_.personal_RobotDontLeaveBuilding;
		this.uiObjects[8].GetComponent<Toggle>().isOn = this.mS_.personal_ki;
		this.SLIDER_Motivation();
		this.SLIDER_Crunch();
	}

	
	public void InitDropdowns()
	{
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1002));
		list.Add(this.tS_.GetText(1003));
		list.Add(this.tS_.GetText(1004));
		this.uiObjects[0].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[0].GetComponent<Dropdown>().AddOptions(list);
		List<string> list2 = new List<string>();
		list2.Add(this.tS_.GetText(1005));
		list2.Add(this.tS_.GetText(1006));
		list2.Add(this.tS_.GetText(1007));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list2);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.mS_.personal_pausen = this.uiObjects[0].GetComponent<Dropdown>().value;
		this.mS_.personal_druck = this.uiObjects[1].GetComponent<Dropdown>().value;
		this.mS_.personal_motivation = Mathf.RoundToInt(this.uiObjects[4].GetComponent<Slider>().value);
		this.mS_.personal_crunch = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.mS_.personal_dontLeaveBuilding = this.uiObjects[6].GetComponent<Toggle>().isOn;
		this.mS_.personal_RobotDontLeaveBuilding = this.uiObjects[7].GetComponent<Toggle>().isOn;
		this.mS_.personal_ki = this.uiObjects[8].GetComponent<Toggle>().isOn;
	}

	
	public void SLIDER_Motivation()
	{
		this.uiObjects[2].GetComponent<Text>().text = this.uiObjects[4].GetComponent<Slider>().value.ToString() + "%";
	}

	
	public void SLIDER_Crunch()
	{
		this.uiObjects[3].GetComponent<Text>().text = this.uiObjects[5].GetComponent<Slider>().value.ToString() + "%";
		if (Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value) >= 100)
		{
			this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(1626);
		}
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;
}
