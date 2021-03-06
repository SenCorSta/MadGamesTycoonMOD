using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Trendsetter : MonoBehaviour
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
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

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(gameScript script_)
	{
		this.FindScripts();
		this.sfx_.PlaySound(31);
		if (script_)
		{
			string text = this.tS_.GetText(760);
			text = text.Replace("<NAME>", script_.GetNameWithTag());
			this.uiObjects[0].GetComponent<Text>().text = text;
			this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(script_.maingenre);
			this.uiObjects[2].GetComponent<Text>().text = this.genres_.GetName(script_.maingenre);
			this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetThemes(script_.gameMainTheme);
			script_.trendsetter = true;
			this.mS_.trendWeeks = UnityEngine.Random.Range(50, 100);
			this.mS_.trendGenre = script_.maingenre;
			int num = 0;
			bool flag = false;
			while (!flag)
			{
				this.mS_.trendAntiGenre = UnityEngine.Random.Range(0, this.genres_.genres_LEVEL.Length);
				if (this.genres_.genres_UNLOCK[this.mS_.trendAntiGenre] && this.mS_.trendAntiGenre != this.mS_.trendGenre)
				{
					flag = true;
				}
				num++;
				if (num > 10000)
				{
					flag = true;
				}
			}
			this.mS_.trendTheme = script_.gameMainTheme;
			if (this.mS_.trendAntiTheme == this.mS_.trendTheme)
			{
				if (this.mS_.trendAntiTheme > 0)
				{
					this.mS_.trendAntiTheme--;
				}
				else
				{
					this.mS_.trendAntiTheme++;
				}
			}
			if (!this.mS_.myPubS_)
			{
				this.mS_.FindMyPublisherScript();
			}
			this.mS_.AddAwards(6, this.mS_.myPubS_);
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_Trend();
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_Trend();
					return;
				}
			}
		}
		else
		{
			this.BUTTON_Abbrechen();
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
		this.guiMain_.CreateTopNewsTrend(this.genres_.GetName(this.mS_.trendGenre) + " / " + this.tS_.GetThemes(this.mS_.trendTheme), this.genres_.GetPic(this.mS_.trendGenre));
	}

	
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;
}
