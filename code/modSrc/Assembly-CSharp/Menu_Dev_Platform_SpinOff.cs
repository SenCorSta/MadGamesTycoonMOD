using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013E RID: 318
public class Menu_Dev_Platform_SpinOff : MonoBehaviour
{
	// Token: 0x06000B92 RID: 2962 RVA: 0x00008335 File Offset: 0x00006535
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0008DD24 File Offset: 0x0008BF24
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

	// Token: 0x06000B94 RID: 2964 RVA: 0x00008335 File Offset: 0x00006535
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0008DE0C File Offset: 0x0008C00C
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

	// Token: 0x06000B96 RID: 2966 RVA: 0x0008DEFC File Offset: 0x0008C0FC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[310]);
		this.guiMain_.uiObjects[310].GetComponent<Menu_Dev_SpinoffSelect>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x0008DF60 File Offset: 0x0008C160
	public void BUTTON_PCundKonsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitSpinoff(this.rS_, this.gS_.myID, 0);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x0008DFCC File Offset: 0x0008C1CC
	public void BUTTON_Handy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitSpinoff(this.rS_, this.gS_.myID, 5);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x0008E038 File Offset: 0x0008C238
	public void BUTTON_Arcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitSpinoff(this.rS_, this.gS_.myID, 4);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x0000833D File Offset: 0x0000653D
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

	// Token: 0x04000FE6 RID: 4070
	public GameObject[] uiObjects;

	// Token: 0x04000FE7 RID: 4071
	private GameObject main_;

	// Token: 0x04000FE8 RID: 4072
	private mainScript mS_;

	// Token: 0x04000FE9 RID: 4073
	private textScript tS_;

	// Token: 0x04000FEA RID: 4074
	private GUI_Main guiMain_;

	// Token: 0x04000FEB RID: 4075
	private sfxScript sfx_;

	// Token: 0x04000FEC RID: 4076
	private unlockScript unlock_;

	// Token: 0x04000FED RID: 4077
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000FEE RID: 4078
	private roomScript rS_;

	// Token: 0x04000FEF RID: 4079
	private gameScript gS_;
}
