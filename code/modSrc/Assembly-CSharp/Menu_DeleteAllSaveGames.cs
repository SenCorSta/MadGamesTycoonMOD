using System;
using UnityEngine;


public class Menu_DeleteAllSaveGames : MonoBehaviour
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

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 0; i <= 20; i++)
		{
			ES3.DeleteFile(this.mS_.GetSavegameTitle() + i.ToString() + ".txt");
		}
		if (this.guiMain_.uiObjects[150].activeSelf)
		{
			this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().Init();
		}
		if (this.guiMain_.uiObjects[156].activeSelf)
		{
			this.guiMain_.uiObjects[156].GetComponent<Menu_SaveGame>().Init();
		}
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
