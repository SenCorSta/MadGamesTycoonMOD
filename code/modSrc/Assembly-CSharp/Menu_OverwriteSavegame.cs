using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_OverwriteSavegame : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	public void Init(int slot_, string saveGameName)
	{
		this.slot = slot_;
		this.uiObjects[0].GetComponent<Text>().text = saveGameName;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		ES3.DeleteFile(this.mS_.GetSavegameTitle() + this.slot.ToString() + ".txt");
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[156].GetComponent<Menu_SaveGame>().BUTTON_SaveGame(this.slot);
		this.BUTTON_Abbrechen();
	}

	
	private ES3Writer writer;

	
	private ES3Reader reader;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private int slot;
}
