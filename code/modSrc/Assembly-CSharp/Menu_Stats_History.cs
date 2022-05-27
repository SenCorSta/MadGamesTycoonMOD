using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Stats_History : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.uiObjects[4].GetComponent<Text>().text = (this.seite + 1).ToString() + " / " + (this.mS_.history.Count / 100 + 1).ToString();
	}

	
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[1].GetComponent<Text>().text = "";
		for (int i = 0; i < this.mS_.history.Count; i++)
		{
			if (i >= this.seite * 100 && i < this.seite * 100 + 100)
			{
				Text component = this.uiObjects[1].GetComponent<Text>();
				component.text = component.text + this.mS_.history[i] + "\n\n";
			}
		}
		if (this.uiObjects[1].GetComponent<Text>().text.Length <= 0)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(303);
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Seite(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.seite += i;
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > this.mS_.history.Count / 100)
		{
			this.seite = this.mS_.history.Count / 100;
		}
		this.Init();
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private engineFeatures eF_;

	
	private genres genres_;

	
	public int seite;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;
}
