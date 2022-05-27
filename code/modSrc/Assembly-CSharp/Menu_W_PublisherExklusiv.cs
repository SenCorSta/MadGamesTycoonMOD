using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_PublisherExklusiv : MonoBehaviour
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

	
	public void Init(publisherScript pS_)
	{
		this.FindScripts();
		if (!pS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.publisherS_ = pS_;
		this.laufzeit = pS_.exklusivLaufzeit;
		this.sofortzahlung = pS_.GetMoneyExklusiv();
		this.uiObjects[0].GetComponent<Image>().sprite = pS_.GetLogo();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.sofortzahlung, true);
		string text = this.tS_.GetText(1048);
		text = text.Replace("<NUM>", this.laufzeit.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
		text = this.tS_.GetText(1049);
		text = text.Replace("<NAME>", "<color=blue>" + pS_.GetName() + "</color>");
		this.uiObjects[3].GetComponent<Text>().text = text;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		if (this.publisherS_)
		{
			this.mS_.Earn((long)this.sofortzahlung, 1);
			this.mS_.exklusivVertrag_ID = this.publisherS_.myID;
			this.mS_.exklusivVertrag_laufzeit = this.laufzeit;
			if (this.mS_.achScript_)
			{
				this.mS_.achScript_.SetAchivement(42);
			}
		}
		this.guiMain_.uiObjects[200].GetComponent<Menu_PublisherExklusiv>().BUTTON_Close();
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private publisherScript publisherS_;

	
	public int laufzeit;

	
	public int sofortzahlung;
}
