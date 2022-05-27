using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013F RID: 319
public class Menu_Dev_Platform_SpinOff : MonoBehaviour
{
	// Token: 0x06000BA6 RID: 2982 RVA: 0x0007E175 File Offset: 0x0007C375
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x0007E180 File Offset: 0x0007C380
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

	// Token: 0x06000BA8 RID: 2984 RVA: 0x0007E175 File Offset: 0x0007C375
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0007E268 File Offset: 0x0007C468
	public void Init(roomScript script_, gameScript game_)
	{
		this.FindScripts();
		if (!script_ || !game_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.rS_ = script_;
		this.gS_ = game_;
		string text = this.tS_.GetText(1859);
		text = text.Replace("<NAME>", "<color=blue>" + this.gS_.GetNameWithTag() + "</color>");
		this.uiObjects[0].GetComponent<Text>().text = text;
		this.uiObjects[2].GetComponent<Button>().interactable = true;
		this.uiObjects[4].GetComponent<Button>().interactable = true;
		this.uiObjects[6].GetComponent<Button>().interactable = true;
		this.Unlock(65, this.uiObjects[5], this.uiObjects[4]);
		this.forschungSonstiges_.Unlock(38, this.uiObjects[7], this.uiObjects[6]);
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x0007E358 File Offset: 0x0007C558
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[310]);
		this.guiMain_.uiObjects[310].GetComponent<Menu_Dev_SpinoffSelect>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0007E3BC File Offset: 0x0007C5BC
	public void BUTTON_PCundKonsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitSpinoff(this.rS_, this.gS_.myID, 0);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0007E428 File Offset: 0x0007C628
	public void BUTTON_Handy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitSpinoff(this.rS_, this.gS_.myID, 5);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x0007E494 File Offset: 0x0007C694
	public void BUTTON_Arcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitSpinoff(this.rS_, this.gS_.myID, 4);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BAE RID: 2990 RVA: 0x0007E4FD File Offset: 0x0007C6FD
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

	// Token: 0x04000FEE RID: 4078
	public GameObject[] uiObjects;

	// Token: 0x04000FEF RID: 4079
	private GameObject main_;

	// Token: 0x04000FF0 RID: 4080
	private mainScript mS_;

	// Token: 0x04000FF1 RID: 4081
	private textScript tS_;

	// Token: 0x04000FF2 RID: 4082
	private GUI_Main guiMain_;

	// Token: 0x04000FF3 RID: 4083
	private sfxScript sfx_;

	// Token: 0x04000FF4 RID: 4084
	private unlockScript unlock_;

	// Token: 0x04000FF5 RID: 4085
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000FF6 RID: 4086
	private roomScript rS_;

	// Token: 0x04000FF7 RID: 4087
	private gameScript gS_;
}
