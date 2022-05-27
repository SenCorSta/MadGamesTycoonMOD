using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B3 RID: 435
public class Menu_MessageBoxSave : MonoBehaviour
{
	// Token: 0x06001064 RID: 4196 RVA: 0x0000B9A5 File Offset: 0x00009BA5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x000BB06C File Offset: 0x000B926C
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

	// Token: 0x06001066 RID: 4198 RVA: 0x000BB118 File Offset: 0x000B9318
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

	// Token: 0x06001067 RID: 4199 RVA: 0x0000B9AD File Offset: 0x00009BAD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x000BB1D0 File Offset: 0x000B93D0
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

	// Token: 0x06001069 RID: 4201 RVA: 0x000BB228 File Offset: 0x000B9428
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

	// Token: 0x040014E2 RID: 5346
	public GameObject[] uiObjects;

	// Token: 0x040014E3 RID: 5347
	private GameObject main_;

	// Token: 0x040014E4 RID: 5348
	private mainScript mS_;

	// Token: 0x040014E5 RID: 5349
	private textScript tS_;

	// Token: 0x040014E6 RID: 5350
	private GUI_Main guiMain_;

	// Token: 0x040014E7 RID: 5351
	private sfxScript sfx_;

	// Token: 0x040014E8 RID: 5352
	private bool closeMenu;

	// Token: 0x040014E9 RID: 5353
	private int id;
}
