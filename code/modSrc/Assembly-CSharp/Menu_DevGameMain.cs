using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000117 RID: 279
public class Menu_DevGameMain : MonoBehaviour
{
	// Token: 0x06000981 RID: 2433 RVA: 0x00006E56 File Offset: 0x00005056
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0007A4A8 File Offset: 0x000786A8
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

	// Token: 0x06000983 RID: 2435 RVA: 0x00006E5E File Offset: 0x0000505E
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(10);
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0007A590 File Offset: 0x00078790
	public void Init(roomScript script_)
	{
		this.FindScripts();
		if (!script_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.rS_ = script_;
		this.Unlock(26, this.uiObjects[1], this.uiObjects[0]);
		this.Unlock(27, this.uiObjects[3], this.uiObjects[2]);
		this.Unlock(65, this.uiObjects[5], this.uiObjects[4]);
		this.Unlock(66, this.uiObjects[9], this.uiObjects[8]);
		this.Unlock(67, this.uiObjects[11], this.uiObjects[10]);
		this.forschungSonstiges_.Unlock(38, this.uiObjects[7], this.uiObjects[6]);
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00006E80 File Offset: 0x00005080
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0007A654 File Offset: 0x00078854
	public void BUTTON_NewGame()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNewGame(this.rS_, 0);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(318);
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0007A6E8 File Offset: 0x000788E8
	public void BUTTON_Nachfolger()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[97]);
		this.guiMain_.uiObjects[97].GetComponent<Menu_Dev_NachfolgerSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(319);
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x0007A77C File Offset: 0x0007897C
	public void BUTTON_Remaster()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[98]);
		this.guiMain_.uiObjects[98].GetComponent<Menu_Dev_RemasterSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(320);
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0007A810 File Offset: 0x00078A10
	public void BUTTON_Handy()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNewGame(this.rS_, 5);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1499);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0007A8A4 File Offset: 0x00078AA4
	public void BUTTON_Arcade()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNewGame(this.rS_, 4);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1500);
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0007A938 File Offset: 0x00078B38
	public void BUTTON_SpinOff()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[310]);
		this.guiMain_.uiObjects[310].GetComponent<Menu_Dev_SpinoffSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1535);
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0007A9D0 File Offset: 0x00078BD0
	public void BUTTON_Port()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[312]);
		this.guiMain_.uiObjects[312].GetComponent<Menu_Dev_PortSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00006EA6 File Offset: 0x000050A6
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

	// Token: 0x04000DE5 RID: 3557
	public GameObject[] uiObjects;

	// Token: 0x04000DE6 RID: 3558
	private roomScript rS_;

	// Token: 0x04000DE7 RID: 3559
	private GameObject main_;

	// Token: 0x04000DE8 RID: 3560
	private mainScript mS_;

	// Token: 0x04000DE9 RID: 3561
	private textScript tS_;

	// Token: 0x04000DEA RID: 3562
	private GUI_Main guiMain_;

	// Token: 0x04000DEB RID: 3563
	private sfxScript sfx_;

	// Token: 0x04000DEC RID: 3564
	private unlockScript unlock_;

	// Token: 0x04000DED RID: 3565
	private forschungSonstiges forschungSonstiges_;
}
