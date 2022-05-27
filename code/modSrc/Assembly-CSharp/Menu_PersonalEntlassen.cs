using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_PersonalEntlassen : MonoBehaviour
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
		this.listPersonal.Clear();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		for (int i = 0; i < this.listPersonal.Count; i++)
		{
			if (this.listPersonal[i])
			{
				this.listPersonal[i].Entlassen(true);
			}
		}
		this.BUTTON_Abbrechen();
	}

	
	public void AddCharacter(characterScript cS_)
	{
		this.FindScripts();
		this.listPersonal.Add(cS_);
		string text = "";
		for (int i = 0; i < this.listPersonal.Count; i++)
		{
			if (this.listPersonal[i])
			{
				text += this.listPersonal[i].myName;
				if (i + 1 < this.listPersonal.Count)
				{
					text += ", ";
				}
			}
		}
		string text2 = this.tS_.GetText(186);
		text2 = text2.Replace("<NAME>", "<color=blue>" + text + "</color>");
		this.uiObjects[0].GetComponent<Text>().text = text2;
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private List<characterScript> listPersonal = new List<characterScript>();
}
