using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000118 RID: 280
public class Menu_DevGameMain : MonoBehaviour
{
	// Token: 0x06000990 RID: 2448 RVA: 0x0006937B File Offset: 0x0006757B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00069384 File Offset: 0x00067584
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

	// Token: 0x06000992 RID: 2450 RVA: 0x0006946A File Offset: 0x0006766A
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(10);
		}
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0006948C File Offset: 0x0006768C
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

	// Token: 0x06000994 RID: 2452 RVA: 0x0006954D File Offset: 0x0006774D
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x00069574 File Offset: 0x00067774
	public void BUTTON_NewGame()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNewGame(this.rS_, 0);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(318);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x00069608 File Offset: 0x00067808
	public void BUTTON_Nachfolger()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[97]);
		this.guiMain_.uiObjects[97].GetComponent<Menu_Dev_NachfolgerSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(319);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0006969C File Offset: 0x0006789C
	public void BUTTON_Remaster()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[98]);
		this.guiMain_.uiObjects[98].GetComponent<Menu_Dev_RemasterSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(320);
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00069730 File Offset: 0x00067930
	public void BUTTON_Handy()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNewGame(this.rS_, 5);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1499);
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x000697C4 File Offset: 0x000679C4
	public void BUTTON_Arcade()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitNewGame(this.rS_, 4);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1500);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00069858 File Offset: 0x00067A58
	public void BUTTON_SpinOff()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[310]);
		this.guiMain_.uiObjects[310].GetComponent<Menu_Dev_SpinoffSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1535);
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x000698F0 File Offset: 0x00067AF0
	public void BUTTON_Port()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[312]);
		this.guiMain_.uiObjects[312].GetComponent<Menu_Dev_PortSelect>().Init(this.rS_);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00069988 File Offset: 0x00067B88
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

	// Token: 0x04000DED RID: 3565
	public GameObject[] uiObjects;

	// Token: 0x04000DEE RID: 3566
	private roomScript rS_;

	// Token: 0x04000DEF RID: 3567
	private GameObject main_;

	// Token: 0x04000DF0 RID: 3568
	private mainScript mS_;

	// Token: 0x04000DF1 RID: 3569
	private textScript tS_;

	// Token: 0x04000DF2 RID: 3570
	private GUI_Main guiMain_;

	// Token: 0x04000DF3 RID: 3571
	private sfxScript sfx_;

	// Token: 0x04000DF4 RID: 3572
	private unlockScript unlock_;

	// Token: 0x04000DF5 RID: 3573
	private forschungSonstiges forschungSonstiges_;
}
