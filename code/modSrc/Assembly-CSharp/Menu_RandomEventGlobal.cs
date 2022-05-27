using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_RandomEventGlobal : MonoBehaviour
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

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(int forceEvent)
	{
		this.FindScripts();
		int num;
		if (forceEvent == -1)
		{
			num = UnityEngine.Random.Range(0, 14);
		}
		else
		{
			num = forceEvent;
		}
		switch (num)
		{
		case 0:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1080);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 1:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1081);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(56, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 2:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1082);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 3:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1083);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 4:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1084);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 5:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1085);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(56, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 6:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1086);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 7:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1087);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 8:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1316);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 9:
			if (this.unlock_.unlock[21])
			{
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1088);
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
				this.sfx_.PlaySound(55, true);
				this.mS_.SetGlobalEvent(num);
			}
			else
			{
				this.Init(4);
			}
			break;
		case 10:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1377);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 11:
			if (this.unlock_.unlock[21])
			{
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1384);
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
				this.sfx_.PlaySound(55, true);
				this.mS_.SetGlobalEvent(num);
			}
			else
			{
				this.Init(3);
			}
			break;
		case 12:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1089);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		case 13:
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1889);
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.iconGlobalEvents[num];
			this.sfx_.PlaySound(55, true);
			this.mS_.SetGlobalEvent(num);
			break;
		}
		if (this.mS_.settings_ && this.mS_.settings_.hideEvents)
		{
			this.BUTTON_Abbrechen();
		}
	}

	
	public void History()
	{
		this.FindScripts();
		if (this.mS_.year == 1984 && this.mS_.month == 2)
		{
			this.Init(0);
			return;
		}
		if (this.mS_.year == 1990 && this.mS_.month == 3)
		{
			this.Init(1);
			return;
		}
		if (this.mS_.year == 1995 && this.mS_.month == 4)
		{
			this.Init(10);
			return;
		}
		if (this.mS_.year == 2000 && this.mS_.month == 5)
		{
			this.Init(5);
			return;
		}
		if (this.mS_.year == 2004 && this.mS_.month == 2)
		{
			this.Init(11);
			return;
		}
		if (this.mS_.year == 2005 && this.mS_.month == 9)
		{
			this.Init(9);
			return;
		}
		if (this.mS_.year == 2007 && this.mS_.month == 4)
		{
			this.Init(4);
			return;
		}
		if (this.mS_.year == 2010 && this.mS_.month == 7)
		{
			this.Init(8);
			return;
		}
		if (this.mS_.year == 2013 && this.mS_.month == 8)
		{
			this.Init(2);
			return;
		}
		if (this.mS_.year == 2017 && this.mS_.month == 2)
		{
			this.Init(12);
			return;
		}
		if (this.mS_.year == 2019 && this.mS_.month == 4)
		{
			this.Init(7);
			return;
		}
		if (this.mS_.year == 2021 && this.mS_.month == 5)
		{
			this.Init(13);
			return;
		}
		if (this.mS_.year == 2023 && this.mS_.month == 3)
		{
			this.Init(6);
			return;
		}
		if (this.mS_.year == 2024 && this.mS_.month == 9)
		{
			this.Init(3);
			return;
		}
		if (this.mS_.year == 2026 && this.mS_.month == 1)
		{
			this.mS_.settings_history = false;
			this.Init(-1);
			return;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private unlockScript unlock_;

	
	private bool closeMenu;
}
