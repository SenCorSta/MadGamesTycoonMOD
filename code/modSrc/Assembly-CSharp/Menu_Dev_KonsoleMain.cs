using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000162 RID: 354
public class Menu_Dev_KonsoleMain : MonoBehaviour
{
	// Token: 0x06000D3F RID: 3391 RVA: 0x00090C3B File Offset: 0x0008EE3B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x00090C44 File Offset: 0x0008EE44
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
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x00090D2A File Offset: 0x0008EF2A
	public void Init(roomScript script_)
	{
		this.FindScripts();
		if (!script_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.rS_ = script_;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x00090D48 File Offset: 0x0008EF48
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x00090D70 File Offset: 0x0008EF70
	public void BUTTON_NewKonsole()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[318]);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().Init(this.rS_, 1);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1583);
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x00090E0C File Offset: 0x0008F00C
	public void BUTTON_NewHandheld()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[318]);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().Init(this.rS_, 2);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1584);
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x00090EA7 File Offset: 0x0008F0A7
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			lock_.SetActive(false);
			return;
		}
		button_.GetComponent<Button>().interactable = false;
		lock_.SetActive(true);
	}

	// Token: 0x040011CB RID: 4555
	public GameObject[] uiObjects;

	// Token: 0x040011CC RID: 4556
	private roomScript rS_;

	// Token: 0x040011CD RID: 4557
	private GameObject main_;

	// Token: 0x040011CE RID: 4558
	private mainScript mS_;

	// Token: 0x040011CF RID: 4559
	private textScript tS_;

	// Token: 0x040011D0 RID: 4560
	private GUI_Main guiMain_;

	// Token: 0x040011D1 RID: 4561
	private sfxScript sfx_;

	// Token: 0x040011D2 RID: 4562
	private unlockScript unlock_;

	// Token: 0x040011D3 RID: 4563
	private forschungSonstiges forschungSonstiges_;
}
