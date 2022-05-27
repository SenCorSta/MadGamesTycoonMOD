using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D8 RID: 472
public class Menu_RandomEventGlobal : MonoBehaviour
{
	// Token: 0x060011CB RID: 4555 RVA: 0x000BB735 File Offset: 0x000B9935
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000BB740 File Offset: 0x000B9940
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

	// Token: 0x060011CD RID: 4557 RVA: 0x000BB808 File Offset: 0x000B9A08
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x000BB824 File Offset: 0x000B9A24
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

	// Token: 0x060011CF RID: 4559 RVA: 0x000BBE28 File Offset: 0x000BA028
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

	// Token: 0x060011D0 RID: 4560 RVA: 0x000BC0BD File Offset: 0x000BA2BD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000BC0BD File Offset: 0x000BA2BD
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001645 RID: 5701
	public GameObject[] uiObjects;

	// Token: 0x04001646 RID: 5702
	private GameObject main_;

	// Token: 0x04001647 RID: 5703
	private mainScript mS_;

	// Token: 0x04001648 RID: 5704
	private textScript tS_;

	// Token: 0x04001649 RID: 5705
	private GUI_Main guiMain_;

	// Token: 0x0400164A RID: 5706
	private sfxScript sfx_;

	// Token: 0x0400164B RID: 5707
	private unlockScript unlock_;

	// Token: 0x0400164C RID: 5708
	private bool closeMenu;
}
