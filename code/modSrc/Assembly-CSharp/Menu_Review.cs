﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Review : MonoBehaviour
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.reviewText_)
		{
			this.reviewText_ = this.main_.GetComponent<reviewText>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	
	private void Update()
	{
		if (this.game_)
		{
			float b = (float)this.game_.reviewTotal;
			this.reviewTotalLerp = Mathf.Lerp(this.reviewTotalLerp, b, 0.04f);
			this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(this.reviewTotalLerp).ToString() + "%";
			this.uiObjects[22].GetComponent<Text>().text = Mathf.RoundToInt(this.reviewTotalLerp).ToString() + "%";
			this.uiObjects[30].GetComponent<Text>().text = Mathf.RoundToInt(this.reviewTotalLerp).ToString() + "%";
			this.ShowRewards(true);
		}
	}

	
	private void ShowRewards(bool playSound)
	{
		if (this.game_.typ_addon || this.game_.typ_addonStandalone || this.game_.typ_bundle || this.game_.typ_mmoaddon || this.game_.typ_bundleAddon)
		{
			return;
		}
		if (Mathf.RoundToInt(this.reviewTotalLerp) >= 80)
		{
			if (!this.uiObjects[4].activeSelf)
			{
				this.uiObjects[4].SetActive(true);
				if (playSound)
				{
					this.sfx_.PlaySound(31, false);
				}
			}
		}
		else if (this.uiObjects[4].activeSelf)
		{
			this.uiObjects[4].SetActive(false);
		}
		if (Mathf.RoundToInt(this.reviewTotalLerp) == this.game_.reviewTotal && this.game_.reviewTotal < 30)
		{
			if (!this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(true);
				if (playSound)
				{
					this.sfx_.PlaySound(32, false);
					return;
				}
			}
		}
		else if (this.uiObjects[12].activeSelf)
		{
			this.uiObjects[12].SetActive(false);
		}
	}

	
	public void InitContractGame(gameScript s_)
	{
		this.Init(s_);
		this.showContractAbrechnung = true;
	}

	
	public void Init(gameScript s_)
	{
		this.FindScripts();
		this.reviewTotalLerp = 0f;
		this.game_ = s_;
		if (this.game_.beschreibung.Length <= 0)
		{
			this.uiObjects[41].SetActive(false);
		}
		else
		{
			this.uiObjects[41].SetActive(true);
		}
		this.uiObjects[14].SetActive(true);
		this.uiObjects[15].SetActive(false);
		this.uiObjects[26].SetActive(false);
		if (s_.reviewTotalText != -1)
		{
			this.reviewTotalLerp = (float)s_.reviewTotal;
			this.ShowRewards(false);
		}
		if (this.game_.handy)
		{
			this.uiObjects[34].SetActive(false);
			this.uiObjects[35].SetActive(true);
			this.uiObjects[38].SetActive(false);
		}
		if (this.game_.arcade)
		{
			this.uiObjects[34].SetActive(false);
			this.uiObjects[35].SetActive(false);
			this.uiObjects[38].SetActive(true);
		}
		if (!this.game_.arcade && !this.game_.handy)
		{
			this.uiObjects[34].SetActive(true);
			this.uiObjects[35].SetActive(false);
			this.uiObjects[38].SetActive(false);
		}
		this.uiObjects[13].GetComponent<Text>().text = this.tS_.GetText(278);
		if (this.game_.retro)
		{
			this.uiObjects[13].GetComponent<Text>().text = "<color=green>" + this.tS_.GetText(908) + "</color>";
		}
		this.uiObjects[24].GetComponent<Image>().sprite = this.games_.gamePEGI[this.game_.usk];
		this.uiObjects[36].GetComponent<Image>().sprite = this.games_.gamePEGI[this.game_.usk];
		this.uiObjects[39].GetComponent<Image>().sprite = this.games_.gamePEGI[this.game_.usk];
		this.uiObjects[1].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[37].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[40].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[3].GetComponent<Text>().text = this.reviewText_.GetReviewText(this.game_);
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetDeveloperName() + " / " + this.game_.GetPublisherName();
		if (this.game_.subgenre != -1)
		{
			this.uiObjects[6].GetComponent<Text>().text = this.game_.GetGenreString() + " / " + this.game_.GetSubGenreString();
		}
		else
		{
			this.uiObjects[6].GetComponent<Text>().text = this.game_.GetGenreString();
		}
		this.uiObjects[8].GetComponent<Text>().text = this.game_.reviewGameplay.ToString() + "%";
		this.uiObjects[9].GetComponent<Text>().text = this.game_.reviewGrafik.ToString() + "%";
		this.uiObjects[10].GetComponent<Text>().text = this.game_.reviewSound.ToString() + "%";
		this.uiObjects[11].GetComponent<Text>().text = this.game_.reviewSteuerung.ToString() + "%";
		this.uiObjects[5].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		if (this.game_.typ_addon || this.game_.typ_addonStandalone || this.game_.typ_mmoaddon)
		{
			this.uiObjects[14].SetActive(false);
			this.uiObjects[15].SetActive(true);
			this.uiObjects[26].SetActive(false);
			this.uiObjects[17].GetComponent<Text>().text = this.game_.GetNameWithTag();
			this.uiObjects[18].GetComponent<Text>().text = this.game_.GetReleaseDateString();
			this.uiObjects[19].GetComponent<Text>().text = this.game_.GetDeveloperName() + " / " + this.game_.GetPublisherName();
			this.uiObjects[25].GetComponent<Image>().sprite = this.games_.gamePEGI[this.game_.usk];
			if (this.game_.subgenre != -1)
			{
				this.uiObjects[20].GetComponent<Text>().text = this.game_.GetGenreString() + " / " + this.game_.GetSubGenreString();
			}
			else
			{
				this.uiObjects[20].GetComponent<Text>().text = this.game_.GetGenreString();
			}
			this.uiObjects[21].GetComponent<Image>().sprite = this.game_.GetScreenshot();
			gameScript gameScript = this.game_.FindVorgaengerScript();
			if (gameScript)
			{
				this.uiObjects[23].GetComponent<Text>().text = gameScript.GetNameWithTag();
			}
		}
		if (this.game_.typ_bundle || this.game_.typ_bundleAddon)
		{
			this.uiObjects[14].SetActive(false);
			this.uiObjects[15].SetActive(false);
			this.uiObjects[26].SetActive(true);
			this.uiObjects[27].GetComponent<Text>().text = this.game_.GetNameWithTag();
			this.uiObjects[28].GetComponent<Text>().text = this.game_.GetReleaseDateString();
			this.uiObjects[31].GetComponent<Text>().text = this.game_.GetDeveloperName() + " / " + this.game_.GetPublisherName();
			this.uiObjects[32].GetComponent<Image>().sprite = this.games_.gamePEGI[this.game_.usk];
			this.uiObjects[29].GetComponent<Image>().sprite = this.game_.GetScreenshot();
			string text = "";
			for (int i = 0; i < this.game_.bundleID.Length; i++)
			{
				gameScript bundleGame = this.game_.GetBundleGame(i);
				if (bundleGame)
				{
					text = text + bundleGame.GetNameWithTag() + "\n";
				}
			}
			this.uiObjects[33].GetComponent<Text>().text = text;
		}
	}

	
	public void BUTTON_Close()
	{
		if (this.game_ && Mathf.RoundToInt(this.reviewTotalLerp) != this.game_.reviewTotal)
		{
			this.reviewTotalLerp = (float)this.game_.reviewTotal;
			return;
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(19);
		}
		if (!this.unlock_.Get(26))
		{
			this.unlock_.CheckUnlock(true);
		}
		if (!this.guiMain_.uiObjects[45].activeSelf && !this.guiMain_.uiObjects[115].activeSelf && !this.guiMain_.uiObjects[116].activeSelf && !this.guiMain_.uiObjects[124].activeSelf && !this.guiMain_.uiObjects[232].activeSelf && !this.guiMain_.uiObjects[243].activeSelf && !this.guiMain_.uiObjects[190].activeSelf && !this.guiMain_.uiObjects[191].activeSelf && !this.guiMain_.uiObjects[183].activeSelf && !this.guiMain_.uiObjects[191].activeSelf && !this.guiMain_.uiObjects[110].activeSelf && !this.guiMain_.uiObjects[192].activeSelf && !this.guiMain_.uiObjects[287].activeSelf && !this.guiMain_.uiObjects[288].activeSelf && !this.guiMain_.uiObjects[284].activeSelf && !this.guiMain_.uiObjects[303].activeSelf && !this.guiMain_.uiObjects[305].activeSelf && !this.guiMain_.uiObjects[316].activeSelf && !this.guiMain_.uiObjects[340].activeSelf && !this.guiMain_.uiObjects[356].activeSelf && !this.guiMain_.uiObjects[360].activeSelf && !this.guiMain_.uiObjects[363].activeSelf && !this.guiMain_.uiObjects[374].activeSelf && !this.guiMain_.uiObjects[375].activeSelf && !this.guiMain_.uiObjects[394].activeSelf)
		{
			if (this.showContractAbrechnung)
			{
				this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[101]);
				this.guiMain_.uiObjects[101].GetComponent<Menu_Dev_AuftragsSpielEnd>().Init(this.game_);
			}
			else
			{
				this.guiMain_.CloseMenu();
			}
		}
		this.showContractAbrechnung = false;
	}

	
	public void BUTTON_Spielbeschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[199]);
		this.guiMain_.uiObjects[199].GetComponent<Menu_Dev_ShowBeschreibung>().Init(this.game_);
	}

	
	public gameScript game_;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	private reviewText reviewText_;

	
	private unlockScript unlock_;

	
	private games games_;

	
	public GameObject[] uiObjects;

	
	private float reviewTotalLerp;

	
	private bool showContractAbrechnung;
}
