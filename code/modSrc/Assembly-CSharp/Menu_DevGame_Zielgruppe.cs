using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_Zielgruppe : MonoBehaviour
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.zielgruppe = this.mDevGame_.g_GameZielgruppe;
		this.UpdateGUI();
	}

	
	private void UpdateGUI()
	{
		this.uiObjects[1].GetComponent<Image>().color = Color.white;
		this.uiObjects[2].GetComponent<Image>().color = Color.white;
		this.uiObjects[3].GetComponent<Image>().color = Color.white;
		this.uiObjects[4].GetComponent<Image>().color = Color.white;
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
		this.uiObjects[1 + this.zielgruppe].GetComponent<Image>().color = this.guiMain_.colors[4];
	}

	
	public void BUTTON_GameZielgruppe(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.zielgruppe = i;
		this.UpdateGUI();
	}

	
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		this.mDevGame_.SetZielgruppe(this.zielgruppe);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private Menu_DevGame mDevGame_;

	
	private games games_;

	
	private int zielgruppe;
}
