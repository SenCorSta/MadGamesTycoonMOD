using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_PublisherExklusivKuendigen : MonoBehaviour
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

	
	private void OnEnable()
	{
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		this.pS_ = null;
		this.pS_ = this.mS_.GetExklusivPublisher();
		if (!this.pS_)
		{
			this.BUTTON_Abbrechen();
			return;
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.pS_.GetLogo();
		string text = this.tS_.GetText(1050);
		text = text.Replace("<NUM>", "<color=blue>" + this.mS_.exklusivVertrag_laufzeit.ToString() + "</color>");
		text = text.Replace("<NAME>", "<color=blue>" + this.pS_.GetName() + "</color>");
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.pS_.stars / 20f));
		long strafzahlung = this.GetStrafzahlung();
		text = this.tS_.GetText(1914);
		text = text.Replace("<NUM>", "<color=blue>" + this.mS_.GetMoney(strafzahlung, true) + "</color>");
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	
	public long GetStrafzahlung()
	{
		if (this.pS_)
		{
			return (long)Mathf.RoundToInt((float)((this.mS_.year - 1975) * 250000) * (this.pS_.stars / 100f) / 120f * (float)this.mS_.exklusivVertrag_laufzeit * 2.5f + (float)(this.mS_.exklusivVertrag_laufzeit * (30000 * (this.mS_.difficulty + 1))));
		}
		return 0L;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Kuendigen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.pS_)
		{
			this.guiMain_.uiObjects[383].SetActive(true);
			this.guiMain_.uiObjects[383].GetComponent<Menu_W_PublisherKuendigen_MB>().Init(this.pS_);
			return;
		}
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private publisherScript pS_;
}
