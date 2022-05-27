using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_PortChoosePlatform : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(10);
		}
	}

	
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

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_PCundKonsole()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 0);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	
	public void BUTTON_Handy()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 5);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	
	public void BUTTON_Arcade()
	{
		this.guiMain_.uiObjects[312].SetActive(false);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitPort(this.rS_, this.gS_.myID, 4);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().uiObjects[14].GetComponent<Text>().text = this.tS_.GetText(1063);
	}

	
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

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private unlockScript unlock_;

	
	private forschungSonstiges forschungSonstiges_;

	
	private roomScript rS_;

	
	private gameScript gS_;
}
