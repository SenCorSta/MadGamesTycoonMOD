using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Bundle : MonoBehaviour
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
	}

	
	private void OnEnable()
	{
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.games.Length; i++)
		{
			this.SetGame(i, this.games[i]);
		}
	}

	
	public void SetGame(int slot, gameScript script_)
	{
		this.games[slot] = script_;
		if (!script_)
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = this.tS_.GetText(1344);
			this.uiObjects[2 + slot].GetComponent<Text>().text = this.tS_.GetText(1345);
			this.uiObjects[7 + slot].GetComponent<Text>().text = this.tS_.GetText(1344);
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[27 + slot].GetComponent<Text>().text = "";
		}
		else
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = script_.GetTooltip();
			this.uiObjects[2 + slot].GetComponent<Text>().text = "<b>" + script_.GetNameWithTag() + "</b>";
			this.uiObjects[7 + slot].GetComponent<Text>().text = script_.GetReleaseDateString();
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.genres_.GetPic(script_.maingenre);
			this.uiObjects[27 + slot].GetComponent<Text>().text = Mathf.RoundToInt((float)script_.reviewTotal).ToString() + "%";
		}
		this.guiMain_.DrawStarsColor(this.uiObjects[1], Mathf.RoundToInt(this.GetQuality()), Color.white);
	}

	
	public float GetQuality()
	{
		return this.GetTotalReview() / 20f;
	}

	
	public float GetTotalReview()
	{
		float num = 0f;
		for (int i = 0; i < this.games.Length; i++)
		{
			if (this.games[i])
			{
				num += (float)this.games[i].reviewTotal;
			}
		}
		if (num > 0f)
		{
			num /= 3f;
		}
		if (num > 90f)
		{
			num = 90f;
		}
		return num;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Game(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[268]);
		this.guiMain_.uiObjects[268].GetComponent<Menu_BundleSelect>().Init(i);
	}

	
	public void BUTTON_Remove(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.SetGame(i, null);
	}

	
	public void BUTTON_OK()
	{
		int num = 0;
		for (int i = 0; i < this.games.Length; i++)
		{
			if (this.games[i])
			{
				num++;
			}
		}
		if (num <= 1)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1343), false);
			return;
		}
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1346), false);
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int j = 0; j < array.Length; j++)
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && component.GetNameSimple() == this.uiObjects[0].GetComponent<InputField>().text)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(618), false);
					return;
				}
			}
		}
		this.CreateBundleGame();
		this.uiObjects[0].GetComponent<InputField>().text = "";
		for (int k = 0; k < this.games.Length; k++)
		{
			this.games[k] = null;
		}
	}

	
	private void CreateBundleGame()
	{
		for (int i = 0; i < this.games.Length; i++)
		{
			if (this.games[i])
			{
				this.games[i].bundle_created = true;
			}
		}
		gameScript gameScript = this.games_.CreateNewGame(false, true);
		this.games_.FindGames();
		if (this.mS_.multiplayer)
		{
			gameScript.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
		gameScript.SetMyName(this.uiObjects[0].GetComponent<InputField>().text);
		gameScript.ownerID = this.mS_.myID;
		gameScript.playerGame = true;
		gameScript.typ_standard = false;
		gameScript.typ_bundle = true;
		gameScript.warBeiAwards = true;
		gameScript.developerID = -1;
		gameScript.publisherID = -1;
		gameScript.date_year = this.mS_.year;
		gameScript.date_month = this.mS_.month;
		gameScript.usk = 0;
		for (int j = 0; j < this.games.Length; j++)
		{
			if (this.games[j])
			{
				gameScript.maingenre = this.games[j].maingenre;
				gameScript.gameMainTheme = this.games[j].gameMainTheme;
				break;
			}
		}
		gameScript.reviewGameplay = Mathf.RoundToInt(this.GetTotalReview());
		gameScript.reviewGrafik = Mathf.RoundToInt(this.GetTotalReview());
		gameScript.reviewSound = Mathf.RoundToInt(this.GetTotalReview());
		gameScript.reviewSteuerung = Mathf.RoundToInt(this.GetTotalReview());
		gameScript.reviewTotal = Mathf.RoundToInt(this.GetTotalReview());
		gameScript.usk = 0;
		for (int k = 0; k < this.games.Length; k++)
		{
			if (this.games[k])
			{
				gameScript.bundleID[k] = this.games[k].myID;
				if (this.games[k].usk > gameScript.usk)
				{
					gameScript.usk = this.games[k].usk;
				}
				for (int l = 0; l < this.games[k].gameLanguage.Length; l++)
				{
					if (this.games[k].gameLanguage[l])
					{
						gameScript.gameLanguage[l] = true;
					}
				}
				gameScript.points_gameplay += this.games[k].points_gameplay;
				gameScript.points_grafik += this.games[k].points_grafik;
				gameScript.points_sound += this.games[k].points_sound;
				gameScript.points_technik += this.games[k].points_technik;
			}
		}
		int num = 0;
		for (int m = 0; m < this.games.Length; m++)
		{
			if (this.games[m])
			{
				for (int n = 0; n < this.games[m].gamePlatform.Length; n++)
				{
					if (this.games[m].gamePlatform[n] != -1 && this.games[m].gamePlatform[n] != gameScript.gamePlatform[0] && this.games[m].gamePlatform[n] != gameScript.gamePlatform[1] && this.games[m].gamePlatform[n] != gameScript.gamePlatform[2] && this.games[m].gamePlatform[n] != gameScript.gamePlatform[3] && num < 4)
					{
						gameScript.gamePlatform[num] = this.games[m].gamePlatform[n];
						num++;
					}
				}
			}
		}
		for (int num2 = 0; num2 < gameScript.standard_edition.Length; num2++)
		{
			gameScript.standard_edition[num2] = false;
			gameScript.deluxe_edition[num2] = false;
			gameScript.collectors_edition[num2] = false;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[218]);
		this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(gameScript, null, true, true);
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_GameData(gameScript);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_GameData(gameScript);
			}
		}
		this.guiMain_.uiObjects[227].SetActive(false);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private games games_;

	
	public gameScript[] games;
}
