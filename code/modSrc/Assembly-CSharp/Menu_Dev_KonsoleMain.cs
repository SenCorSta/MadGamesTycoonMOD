using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000161 RID: 353
public class Menu_Dev_KonsoleMain : MonoBehaviour
{
	// Token: 0x06000D27 RID: 3367 RVA: 0x0000933F File Offset: 0x0000753F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0009F5B8 File Offset: 0x0009D7B8
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

	// Token: 0x06000D29 RID: 3369 RVA: 0x00009347 File Offset: 0x00007547
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

	// Token: 0x06000D2A RID: 3370 RVA: 0x00009365 File Offset: 0x00007565
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0009F6A0 File Offset: 0x0009D8A0
	public void BUTTON_NewKonsole()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[318]);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().Init(this.rS_, 1);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1583);
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0009F73C File Offset: 0x0009D93C
	public void BUTTON_NewHandheld()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[318]);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().Init(this.rS_, 2);
		this.guiMain_.uiObjects[318].GetComponent<Menu_Dev_Konsole>().uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1584);
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0000938B File Offset: 0x0000758B
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

	// Token: 0x040011C3 RID: 4547
	public GameObject[] uiObjects;

	// Token: 0x040011C4 RID: 4548
	private roomScript rS_;

	// Token: 0x040011C5 RID: 4549
	private GameObject main_;

	// Token: 0x040011C6 RID: 4550
	private mainScript mS_;

	// Token: 0x040011C7 RID: 4551
	private textScript tS_;

	// Token: 0x040011C8 RID: 4552
	private GUI_Main guiMain_;

	// Token: 0x040011C9 RID: 4553
	private sfxScript sfx_;

	// Token: 0x040011CA RID: 4554
	private unlockScript unlock_;

	// Token: 0x040011CB RID: 4555
	private forschungSonstiges forschungSonstiges_;
}
