using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B4 RID: 436
public class Menu_MessageBoxSave : MonoBehaviour
{
	// Token: 0x0600107E RID: 4222 RVA: 0x000AF05A File Offset: 0x000AD25A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x000AF064 File Offset: 0x000AD264
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

	// Token: 0x06001080 RID: 4224 RVA: 0x000AF110 File Offset: 0x000AD310
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

	// Token: 0x06001081 RID: 4225 RVA: 0x000AF1C7 File Offset: 0x000AD3C7
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000AF1E4 File Offset: 0x000AD3E4
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

	// Token: 0x06001083 RID: 4227 RVA: 0x000AF23C File Offset: 0x000AD43C
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

	// Token: 0x040014ED RID: 5357
	public GameObject[] uiObjects;

	// Token: 0x040014EE RID: 5358
	private GameObject main_;

	// Token: 0x040014EF RID: 5359
	private mainScript mS_;

	// Token: 0x040014F0 RID: 5360
	private textScript tS_;

	// Token: 0x040014F1 RID: 5361
	private GUI_Main guiMain_;

	// Token: 0x040014F2 RID: 5362
	private sfxScript sfx_;

	// Token: 0x040014F3 RID: 5363
	private bool closeMenu;

	// Token: 0x040014F4 RID: 5364
	private int id;
}
