using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_MarketAnalyse : MonoBehaviour
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(16);
		}
	}

	
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		string text = this.tS_.GetText(441);
		text = text.Replace("<GENRE>", this.genres_.GetName(this.gS_.maingenre));
		text = text.Replace("<THEME>", this.tS_.GetThemes(this.gS_.gameMainTheme));
		this.uiObjects[10].GetComponent<Text>().text = text;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.maingenre);
		for (int i = 0; i < this.gS_.gamePlatform.Length; i++)
		{
			if (this.gS_.gamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.gS_.gamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					this.uiObjects[6 + i].SetActive(true);
					component.SetPic(this.uiObjects[6 + i]);
					this.uiObjects[6 + i].GetComponent<tooltip>().c = component.GetTooltip();
				}
			}
			else
			{
				this.uiObjects[6 + i].SetActive(false);
			}
		}
		Vector4 amountGamesWithGenreAndTopic = this.games_.GetAmountGamesWithGenreAndTopic(this.gS_);
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.x).ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.y).ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.z).ToString();
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.w).ToString();
		if (this.gS_.typ_contractGame)
		{
			this.BUTTON_Close();
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[195]);
		this.guiMain_.uiObjects[195].GetComponent<Menu_Dev_USK>().Init(this.gS_);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private gameScript gS_;
}
