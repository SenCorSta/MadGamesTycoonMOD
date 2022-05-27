using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000140 RID: 320
public class Menu_Dev_PortChoosePlatform : MonoBehaviour
{
	// Token: 0x06000BB0 RID: 2992 RVA: 0x0007E535 File Offset: 0x0007C735
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x0007E540 File Offset: 0x0007C740
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

	// Token: 0x06000BB2 RID: 2994 RVA: 0x0007E626 File Offset: 0x0007C826
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(10);
		}
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0007E648 File Offset: 0x0007C848
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

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0007E820 File Offset: 0x0007CA20
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

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0007E8AE File Offset: 0x0007CAAE
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x0007E8CC File Offset: 0x0007CACC
	public void BUTTON_PCundKonsole()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 0);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x0007E984 File Offset: 0x0007CB84
	public void BUTTON_Handy()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 5);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0007EA3C File Offset: 0x0007CC3C
	public void BUTTON_Arcade()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 4);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0007EAF1 File Offset: 0x0007CCF1
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

	// Token: 0x04000FF8 RID: 4088
	public GameObject[] uiObjects;

	// Token: 0x04000FF9 RID: 4089
	private GameObject main_;

	// Token: 0x04000FFA RID: 4090
	private mainScript mS_;

	// Token: 0x04000FFB RID: 4091
	private textScript tS_;

	// Token: 0x04000FFC RID: 4092
	private GUI_Main guiMain_;

	// Token: 0x04000FFD RID: 4093
	private sfxScript sfx_;

	// Token: 0x04000FFE RID: 4094
	private unlockScript unlock_;

	// Token: 0x04000FFF RID: 4095
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04001000 RID: 4096
	private roomScript rS_;

	// Token: 0x04001001 RID: 4097
	private gameScript gS_;
}
