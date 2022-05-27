using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013D RID: 317
public class Menu_Dev_Platform_Nachfolger : MonoBehaviour
{
	// Token: 0x06000B88 RID: 2952 RVA: 0x000082F5 File Offset: 0x000064F5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0008D9A8 File Offset: 0x0008BBA8
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

	// Token: 0x06000B8A RID: 2954 RVA: 0x000082F5 File Offset: 0x000064F5
	private void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0008DA90 File Offset: 0x0008BC90
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

	// Token: 0x06000B8C RID: 2956 RVA: 0x0008DB80 File Offset: 0x0008BD80
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[97]);
		this.guiMain_.uiObjects[97].GetComponent<Menu_Dev_NachfolgerSelect>().Init(this.rS_);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0008DBE0 File Offset: 0x0008BDE0
	public void BUTTON_PCundKonsole()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNachfolger(this.rS_, this.gS_.myID, 0);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0008DC4C File Offset: 0x0008BE4C
	public void BUTTON_Handy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNachfolger(this.rS_, this.gS_.myID, 5);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0008DCB8 File Offset: 0x0008BEB8
	public void BUTTON_Arcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNachfolger(this.rS_, this.gS_.myID, 4);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x000082FD File Offset: 0x000064FD
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

	// Token: 0x04000FDC RID: 4060
	public GameObject[] uiObjects;

	// Token: 0x04000FDD RID: 4061
	private GameObject main_;

	// Token: 0x04000FDE RID: 4062
	private mainScript mS_;

	// Token: 0x04000FDF RID: 4063
	private textScript tS_;

	// Token: 0x04000FE0 RID: 4064
	private GUI_Main guiMain_;

	// Token: 0x04000FE1 RID: 4065
	private sfxScript sfx_;

	// Token: 0x04000FE2 RID: 4066
	private unlockScript unlock_;

	// Token: 0x04000FE3 RID: 4067
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000FE4 RID: 4068
	private roomScript rS_;

	// Token: 0x04000FE5 RID: 4069
	private gameScript gS_;
}
