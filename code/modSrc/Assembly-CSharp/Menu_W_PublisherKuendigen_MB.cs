using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_PublisherKuendigen_MB : MonoBehaviour
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

	
	public void Init(publisherScript script_)
	{
		this.pS_ = script_;
		if (!this.pS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.FindScripts();
		string text = this.tS_.GetText(1915);
		text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
		this.uiObjects[0].GetComponent<Text>().text = text;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		Menu_W_PublisherExklusivKuendigen component = this.guiMain_.uiObjects[382].GetComponent<Menu_W_PublisherExklusivKuendigen>();
		this.guiMain_.uiObjects[382].SetActive(false);
		this.mS_.Pay(component.GetStrafzahlung(), 14);
		this.pS_.relation = 0f;
		this.mS_.RemovePublisherExklusivVertrag();
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private publisherScript pS_;
}
