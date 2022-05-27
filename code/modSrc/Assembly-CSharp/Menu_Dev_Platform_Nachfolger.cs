using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013E RID: 318
public class Menu_Dev_Platform_Nachfolger : MonoBehaviour
{
	// Token: 0x06000B9C RID: 2972 RVA: 0x0007DDBC File Offset: 0x0007BFBC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
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

	// Token: 0x06000B9E RID: 2974 RVA: 0x0007DDBC File Offset: 0x0007BFBC
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0007DEAC File Offset: 0x0007C0AC
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
		string text = this.tS_.GetText(1858);
		text = text.Replace("<NAME>", "<color=blue>" + this.gS_.GetNameWithTag() + "</color>");
		this.uiObjects[0].GetComponent<Text>().text = text;
		this.uiObjects[2].GetComponent<Button>().interactable = true;
		this.uiObjects[4].GetComponent<Button>().interactable = true;
		this.uiObjects[6].GetComponent<Button>().interactable = true;
		this.Unlock(65, this.uiObjects[5], this.uiObjects[4]);
		this.forschungSonstiges_.Unlock(38, this.uiObjects[7], this.uiObjects[6]);
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0007DF9C File Offset: 0x0007C19C
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[97]);
		this.guiMain_.uiObjects[97].GetComponent<Menu_Dev_NachfolgerSelect>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0007DFFC File Offset: 0x0007C1FC
	public void BUTTON_PCundKonsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNachfolger(this.rS_, this.gS_.myID, 0);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0007E068 File Offset: 0x0007C268
	public void BUTTON_Handy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNachfolger(this.rS_, this.gS_.myID, 5);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0007E0D4 File Offset: 0x0007C2D4
	public void BUTTON_Arcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNachfolger(this.rS_, this.gS_.myID, 4);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0007E13D File Offset: 0x0007C33D
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

	// Token: 0x04000FE4 RID: 4068
	public GameObject[] uiObjects;

	// Token: 0x04000FE5 RID: 4069
	private GameObject main_;

	// Token: 0x04000FE6 RID: 4070
	private mainScript mS_;

	// Token: 0x04000FE7 RID: 4071
	private textScript tS_;

	// Token: 0x04000FE8 RID: 4072
	private GUI_Main guiMain_;

	// Token: 0x04000FE9 RID: 4073
	private sfxScript sfx_;

	// Token: 0x04000FEA RID: 4074
	private unlockScript unlock_;

	// Token: 0x04000FEB RID: 4075
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000FEC RID: 4076
	private roomScript rS_;

	// Token: 0x04000FED RID: 4077
	private gameScript gS_;
}
