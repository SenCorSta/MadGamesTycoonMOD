using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013F RID: 319
public class Menu_Dev_PortChoosePlatform : MonoBehaviour
{
	// Token: 0x06000B9C RID: 2972 RVA: 0x00008375 File Offset: 0x00006575
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0008E0A4 File Offset: 0x0008C2A4
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

	// Token: 0x06000B9E RID: 2974 RVA: 0x0000837D File Offset: 0x0000657D
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(10);
		}
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0008E18C File Offset: 0x0008C38C
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
		string text = this.tS_.GetText(1544);
		text = text.Replace("<NAME>", this.gS_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		this.uiObjects[1].SetActive(false);
		this.uiObjects[2].GetComponent<Button>().interactable = true;
		this.uiObjects[4].GetComponent<Button>().interactable = true;
		this.uiObjects[6].GetComponent<Button>().interactable = true;
		this.uiObjects[8].SetActive(false);
		this.uiObjects[9].SetActive(false);
		this.uiObjects[10].SetActive(false);
		this.Unlock(65, this.uiObjects[5], this.uiObjects[4]);
		this.forschungSonstiges_.Unlock(38, this.uiObjects[7], this.uiObjects[6]);
		if (game_.portExist[0])
		{
			this.uiObjects[2].GetComponent<Button>().interactable = false;
			this.uiObjects[8].SetActive(true);
		}
		if (game_.portExist[1])
		{
			this.uiObjects[4].GetComponent<Button>().interactable = false;
			this.uiObjects[9].SetActive(true);
		}
		if (game_.portExist[2])
		{
			this.uiObjects[6].GetComponent<Button>().interactable = false;
			this.uiObjects[10].SetActive(true);
		}
		this.DisableButtons();
		if (game_.gameTyp == 1 || game_.gameTyp == 2)
		{
			this.uiObjects[6].GetComponent<Button>().interactable = false;
			this.uiObjects[1].SetActive(true);
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0008E364 File Offset: 0x0008C564
	public void DisableButtons()
	{
		if (this.gS_.handy)
		{
			this.uiObjects[4].GetComponent<Button>().interactable = false;
			this.uiObjects[9].SetActive(true);
			return;
		}
		if (this.gS_.arcade)
		{
			this.uiObjects[6].GetComponent<Button>().interactable = false;
			this.uiObjects[10].SetActive(true);
			return;
		}
		this.uiObjects[2].GetComponent<Button>().interactable = false;
		this.uiObjects[8].SetActive(true);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x0000839F File Offset: 0x0000659F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0008E3F4 File Offset: 0x0008C5F4
	public void BUTTON_PCundKonsole()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 0);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0008E4AC File Offset: 0x0008C6AC
	public void BUTTON_Handy()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 5);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0008E564 File Offset: 0x0008C764
	public void BUTTON_Arcade()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 4);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x000083BA File Offset: 0x000065BA
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

	// Token: 0x04000FF0 RID: 4080
	public GameObject[] uiObjects;

	// Token: 0x04000FF1 RID: 4081
	private GameObject main_;

	// Token: 0x04000FF2 RID: 4082
	private mainScript mS_;

	// Token: 0x04000FF3 RID: 4083
	private textScript tS_;

	// Token: 0x04000FF4 RID: 4084
	private GUI_Main guiMain_;

	// Token: 0x04000FF5 RID: 4085
	private sfxScript sfx_;

	// Token: 0x04000FF6 RID: 4086
	private unlockScript unlock_;

	// Token: 0x04000FF7 RID: 4087
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000FF8 RID: 4088
	private roomScript rS_;

	// Token: 0x04000FF9 RID: 4089
	private gameScript gS_;
}
