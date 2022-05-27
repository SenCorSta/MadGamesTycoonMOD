using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000145 RID: 325
public class Menu_Dev_ShowBeschreibung : MonoBehaviour
{
	// Token: 0x06000BE7 RID: 3047 RVA: 0x0008043B File Offset: 0x0007E63B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00080444 File Offset: 0x0007E644
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00080514 File Offset: 0x0007E714
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		if (this.gS_ == null)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		if (this.gS_.beschreibung == null)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		if (this.gS_.beschreibung.Length <= 0)
		{
			this.uiObjects[1].GetComponent<InputField>().text = this.tS_.GetText(999);
			return;
		}
		this.uiObjects[1].GetComponent<InputField>().text = this.gS_.beschreibung;
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000805E2 File Offset: 0x0007E7E2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x000805FD File Offset: 0x0007E7FD
	public void BUTTON_OK()
	{
		this.BUTTON_Close();
	}

	// Token: 0x0400102B RID: 4139
	public GameObject[] uiObjects;

	// Token: 0x0400102C RID: 4140
	private GameObject main_;

	// Token: 0x0400102D RID: 4141
	private mainScript mS_;

	// Token: 0x0400102E RID: 4142
	private textScript tS_;

	// Token: 0x0400102F RID: 4143
	private GUI_Main guiMain_;

	// Token: 0x04001030 RID: 4144
	private sfxScript sfx_;

	// Token: 0x04001031 RID: 4145
	private Menu_DevGame mDevGame_;

	// Token: 0x04001032 RID: 4146
	private gameScript gS_;
}
