using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Awards : MonoBehaviour
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
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Multiplayer_FindWinners(int IDbestGrafik, int IDbestSound, int IDbestStudio, int IDbestPublisher, int IDbestGame, int IDbadGame)
	{
		this.bestGrafik = null;
		this.bestSound = null;
		this.bestStudio = null;
		this.bestPublisher = null;
		this.bestGame = null;
		this.badGame = null;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (IDbestGrafik == component.myID)
				{
					this.bestGrafik = component;
				}
				if (IDbestSound == component.myID)
				{
					this.bestSound = component;
				}
				if (IDbestGame == component.myID)
				{
					this.bestGame = component;
				}
				if (IDbadGame == component.myID)
				{
					this.badGame = component;
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("Publisher");
		if (array.Length != 0)
		{
			for (int j = 0; j < array.Length; j++)
			{
				publisherScript component2 = array[j].GetComponent<publisherScript>();
				if (IDbestStudio == component2.myID)
				{
					this.bestStudio = component2;
				}
				if (IDbestPublisher == component2.myID)
				{
					this.bestPublisher = component2;
				}
			}
		}
	}

	
	public void Init()
	{
		this.FindScripts();
		this.sfx_.PlaySound(31);
		this.timeToWait = 1f;
		if (!this.mS_.multiplayer || (this.mS_.multiplayer && this.mS_.mpCalls_.isServer))
		{
			this.bestGrafik = null;
			this.bestSound = null;
			this.bestStudio = null;
			this.bestPublisher = null;
			this.bestGame = null;
			this.badGame = null;
			this.bestGrafik = this.GetBesteGrafik();
			this.bestSound = this.GetBesterSound();
			this.bestStudio = this.GetBestesStudio();
			this.bestPublisher = this.GetBesterPublisher();
			this.bestGame = this.GetBestesSpiel();
			this.badGame = this.GetBadGame(this.bestGame);
		}
		if (this.mS_.multiplayer && this.mpCalls_.isServer)
		{
			this.guiMain_.BUTTON_GameSpeed(0f);
			int bestGrafik_ = -1;
			int bestSound_ = -1;
			int bestStudio_ = -1;
			int bestPublisher_ = -1;
			int bestGame_ = -1;
			int badGame_ = -1;
			if (this.bestGrafik)
			{
				bestGrafik_ = this.bestGrafik.myID;
			}
			if (this.bestSound)
			{
				bestSound_ = this.bestSound.myID;
			}
			if (this.bestStudio)
			{
				bestStudio_ = this.bestStudio.myID;
			}
			if (this.bestPublisher)
			{
				bestPublisher_ = this.bestPublisher.myID;
			}
			if (this.bestGame)
			{
				bestGame_ = this.bestGame.myID;
			}
			if (this.badGame)
			{
				badGame_ = this.badGame.myID;
			}
			this.mpCalls_.SERVER_Send_Award(bestGrafik_, bestSound_, bestStudio_, bestPublisher_, bestGame_, badGame_);
		}
		this.uiObjects[0].GetComponent<Text>().text = "";
		this.uiObjects[1].GetComponent<Text>().text = "";
		this.uiObjects[2].GetComponent<Text>().text = "";
		this.uiObjects[3].GetComponent<Text>().text = "";
		this.uiObjects[4].GetComponent<Text>().text = "";
		this.uiObjects[5].GetComponent<Text>().text = "";
		this.uiObjects[6].SetActive(false);
		this.uiObjects[7].SetActive(false);
		this.uiObjects[8].SetActive(false);
		this.uiObjects[9].SetActive(false);
		this.uiObjects[10].SetActive(false);
		this.uiObjects[11].SetActive(false);
		this.AddVerlauf();
		base.StartCoroutine(this.ShowAwards());
	}

	
	private void AddVerlauf()
	{
		int bestGrafik_ = -1;
		int bestSound_ = -1;
		int bestStudio_ = -1;
		int bestPublisher_ = -1;
		int bestGame_ = -1;
		int badBame_ = -1;
		if (this.bestGrafik)
		{
			bestGrafik_ = this.bestGrafik.myID;
		}
		if (this.bestSound)
		{
			bestSound_ = this.bestSound.myID;
		}
		if (this.bestStudio)
		{
			bestStudio_ = this.bestStudio.myID;
		}
		if (this.bestPublisher)
		{
			bestPublisher_ = this.bestPublisher.myID;
		}
		if (this.bestGame)
		{
			bestGame_ = this.bestGame.myID;
		}
		if (this.badGame)
		{
			badBame_ = this.badGame.myID;
		}
		this.mS_.AddMadGameConvetionVerlauf(bestGrafik_, bestSound_, bestStudio_, bestPublisher_, bestGame_, badBame_);
	}

	
	private IEnumerator ShowAwards()
	{
		if (this.mS_.settings_ && this.mS_.settings_.hideAwards)
		{
			this.timeToWait = 0f;
		}
		float myFans = (float)this.genres_.GetAmountFans();
		this.mS_.awardBonus = 0;
		this.mS_.awardBonusAmount = 0f;
		yield return new WaitForSeconds(this.timeToWait);
		gameScript gameScript = this.bestGrafik;
		if (gameScript)
		{
			gameScript.AddHype(50f);
			if (gameScript.developerID == this.mS_.myID)
			{
				float num = myFans * 0.02f;
				if (num > 40000f)
				{
					num = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				this.mS_.awardBonus = 20;
				this.mS_.awardBonusAmount += 0.05f;
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(38);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num + 1000f), false) + "</color>");
				this.uiObjects[0].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num + 1000f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.award_Grafik++;
				this.mS_.AddStudioPoints(40);
				gameScript.AddIpPoints(40f);
				this.uiObjects[6].SetActive(true);
			}
			else
			{
				this.uiObjects[0].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[0].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.ownerID),
						"</color>"
					});
				}
				this.sfx_.PlaySound(45);
				if (gameScript.OwnerIsNPC())
				{
					gameScript.AddIpPoints(40f);
				}
			}
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = "-";
		}
		yield return new WaitForSeconds(this.timeToWait);
		gameScript = this.bestSound;
		if (gameScript)
		{
			gameScript.AddHype(50f);
			if (gameScript.developerID == this.mS_.myID)
			{
				float num2 = myFans * 0.015f;
				if (num2 > 40000f)
				{
					num2 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				this.mS_.awardBonus = 20;
				this.mS_.awardBonusAmount += 0.05f;
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(39);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num2 + 1000f), false) + "</color>");
				this.uiObjects[1].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num2 + 1000f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.award_Sound++;
				this.mS_.AddStudioPoints(40);
				gameScript.AddIpPoints(40f);
				this.uiObjects[7].SetActive(true);
			}
			else
			{
				this.uiObjects[1].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[1].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.ownerID),
						"</color>"
					});
				}
				this.sfx_.PlaySound(45);
				if (gameScript.OwnerIsNPC())
				{
					gameScript.AddIpPoints(40f);
				}
			}
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = "-";
		}
		yield return new WaitForSeconds(this.timeToWait);
		publisherScript publisherScript = this.bestStudio;
		if (publisherScript)
		{
			if (publisherScript.myID != this.mS_.myID)
			{
				this.sfx_.PlaySound(45);
				this.uiObjects[2].GetComponent<Text>().text = publisherScript.GetName();
				if (publisherScript.isPlayer)
				{
					this.uiObjects[2].GetComponent<Text>().text = "<color=magenta>" + publisherScript.GetName() + "</color>";
				}
			}
			else
			{
				float num3 = myFans * 0.045f;
				if (num3 > 40000f)
				{
					num3 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				this.mS_.awardBonus = 20;
				this.mS_.awardBonusAmount += 0.2f;
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(36);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num3 + 2500f), false) + "</color>");
				this.uiObjects[2].GetComponent<Text>().text = "<color=blue>" + publisherScript.GetName() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num3 + 2500f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.award_Studio++;
				this.mS_.AddStudioPoints(100);
				this.uiObjects[8].SetActive(true);
			}
		}
		yield return new WaitForSeconds(this.timeToWait);
		publisherScript = this.bestPublisher;
		if (publisherScript)
		{
			if (publisherScript.myID != this.mS_.myID)
			{
				this.uiObjects[3].GetComponent<Text>().text = publisherScript.GetName();
				this.sfx_.PlaySound(45);
				if (publisherScript.isPlayer)
				{
					this.uiObjects[3].GetComponent<Text>().text = "<color=magenta>" + publisherScript.GetName() + "</color>";
				}
			}
			else
			{
				float num4 = myFans * 0.04f;
				if (num4 > 40000f)
				{
					num4 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				this.mS_.awardBonus = 20;
				this.mS_.awardBonusAmount += 0.2f;
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(37);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num4 + 2500f), false) + "</color>");
				this.uiObjects[3].GetComponent<Text>().text = "<color=blue>" + publisherScript.GetName() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num4 + 2500f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.award_Publisher++;
				this.mS_.AddStudioPoints(100);
				this.uiObjects[9].SetActive(true);
			}
		}
		yield return new WaitForSeconds(this.timeToWait);
		gameScript = this.bestGame;
		if (gameScript)
		{
			gameScript.AddHype(100f);
			gameScript.goty = true;
			if (gameScript.developerID == this.mS_.myID)
			{
				float num5 = myFans * 0.05f;
				if (num5 > 40000f)
				{
					num5 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				this.mS_.awardBonus = 20;
				this.mS_.awardBonusAmount += 0.1f;
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(35);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num5 + 3000f), false) + "</color>");
				this.uiObjects[4].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num5 + 3000f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.award_GOTY++;
				this.mS_.AddStudioPoints(75);
				gameScript.AddIpPoints(100f);
				this.uiObjects[10].SetActive(true);
			}
			else
			{
				this.uiObjects[4].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[4].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.ownerID),
						"</color>"
					});
				}
				this.sfx_.PlaySound(45);
				if (gameScript.OwnerIsNPC())
				{
					gameScript.AddIpPoints(100f);
				}
			}
		}
		else
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
		}
		yield return new WaitForSeconds(this.timeToWait);
		gameScript = this.badGame;
		if (gameScript)
		{
			gameScript.AddHype(-50f);
			if (gameScript.developerID == this.mS_.myID)
			{
				float num6 = myFans * 0.05f;
				if (num6 > 40000f)
				{
					num6 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(40);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=red>-" + this.mS_.GetMoney((long)Mathf.RoundToInt(num6 + 2000f), false) + "</color>");
				this.uiObjects[5].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(-Mathf.RoundToInt(num6 + 2000f), -1);
				this.sfx_.PlaySound(45);
				this.uiObjects[11].SetActive(true);
			}
			else
			{
				this.uiObjects[5].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.GameFromMitspieler())
				{
					this.uiObjects[5].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.ownerID),
						"</color>"
					});
				}
				this.sfx_.PlaySound(44);
			}
		}
		else
		{
			this.uiObjects[5].GetComponent<Text>().text = "-";
		}
		this.SetAsTeilgenommen();
		this.timeToWait = 0f;
		if (this.mS_.settings_ && this.mS_.settings_.hideAwards)
		{
			this.BUTTON_Abbrechen();
		}
		yield break;
	}

	
	private void SetAsTeilgenommen()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && this.CheckGame(this.games_.arrayGamesScripts[i]))
			{
				this.games_.arrayGamesScripts[i].warBeiAwards = true;
			}
		}
	}

	
	private bool CheckGame(gameScript script_)
	{
		return script_ && !script_.inDevelopment && !script_.schublade && !script_.pubAngebot && !script_.auftragsspiel && (script_.typ_standard || script_.typ_nachfolger || script_.typ_remaster || script_.typ_spinoff) && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_budget && !script_.typ_bundleAddon && !script_.typ_goty && script_.weeksOnMarket > 0;
	}

	
	private gameScript GetBesteGrafik()
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && this.CheckGame(this.games_.arrayGamesScripts[i]) && num2 < this.games_.arrayGamesScripts[i].reviewGrafik)
			{
				num = i;
				num2 = this.games_.arrayGamesScripts[i].reviewGrafik;
			}
		}
		if (num != -1)
		{
			if (!this.games_.arrayGamesScripts[num].devS_)
			{
				this.games_.arrayGamesScripts[num].FindMyDeveloper();
			}
			if (this.games_.arrayGamesScripts[num].devS_)
			{
				this.mS_.AddAwards(0, this.games_.arrayGamesScripts[num].devS_);
			}
			return this.games_.arrayGamesScripts[num];
		}
		return null;
	}

	
	private gameScript GetBesterSound()
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && this.CheckGame(this.games_.arrayGamesScripts[i]) && num2 < this.games_.arrayGamesScripts[i].reviewSound)
			{
				num = i;
				num2 = this.games_.arrayGamesScripts[i].reviewSound;
			}
		}
		if (num != -1)
		{
			if (!this.games_.arrayGamesScripts[num].devS_)
			{
				this.games_.arrayGamesScripts[num].FindMyDeveloper();
			}
			if (this.games_.arrayGamesScripts[num].devS_)
			{
				this.mS_.AddAwards(1, this.games_.arrayGamesScripts[num].devS_);
			}
			return this.games_.arrayGamesScripts[num];
		}
		return null;
	}

	
	private gameScript GetBestesSpiel()
	{
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && this.CheckGame(this.games_.arrayGamesScripts[i]))
			{
				int num3 = this.games_.arrayGamesScripts[i].reviewTotal + Mathf.RoundToInt(this.games_.arrayGamesScripts[i].GetUserReviewPercent());
				if (num2 < num3)
				{
					num = i;
					num2 = num3;
				}
			}
		}
		if (num != -1)
		{
			if (!this.games_.arrayGamesScripts[num].devS_)
			{
				this.games_.arrayGamesScripts[num].FindMyDeveloper();
			}
			if (this.games_.arrayGamesScripts[num].devS_)
			{
				this.mS_.AddAwards(4, this.games_.arrayGamesScripts[num].devS_);
			}
			return this.games_.arrayGamesScripts[num];
		}
		return null;
	}

	
	private gameScript GetBadGame(gameScript bestGame_)
	{
		int num = -1;
		int num2 = 999;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && this.CheckGame(this.games_.arrayGamesScripts[i]) && num2 > this.games_.arrayGamesScripts[i].reviewTotal && this.games_.arrayGamesScripts[i].reviewTotal < 60)
			{
				num = i;
				num2 = this.games_.arrayGamesScripts[i].reviewTotal;
			}
		}
		if (num == -1)
		{
			return null;
		}
		if (bestGame_ != this.games_.arrayGamesScripts[num])
		{
			if (!this.games_.arrayGamesScripts[num].devS_)
			{
				this.games_.arrayGamesScripts[num].FindMyDeveloper();
			}
			if (this.games_.arrayGamesScripts[num].devS_)
			{
				this.mS_.AddAwards(5, this.games_.arrayGamesScripts[num].devS_);
			}
			return this.games_.arrayGamesScripts[num];
		}
		return null;
	}

	
	private publisherScript GetBestesStudio()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<publisherScript>().awards_bestStudioPoints = 0L;
		}
		for (int j = 0; j < this.games_.arrayGamesScripts.Length; j++)
		{
			if (!this.games_.arrayGamesScripts[j].warBeiAwards && !this.games_.arrayGamesScripts[j].inDevelopment && !this.games_.arrayGamesScripts[j].schublade && !this.games_.arrayGamesScripts[j].pubAngebot && !this.games_.arrayGamesScripts[j].auftragsspiel && (this.games_.arrayGamesScripts[j].typ_standard || this.games_.arrayGamesScripts[j].typ_nachfolger || this.games_.arrayGamesScripts[j].typ_remaster || this.games_.arrayGamesScripts[j].typ_spinoff))
			{
				if (!this.games_.arrayGamesScripts[j].devS_)
				{
					this.games_.arrayGamesScripts[j].FindMyDeveloper();
				}
				if (this.games_.arrayGamesScripts[j].devS_)
				{
					if (!this.games_.arrayGamesScripts[j].devS_.isPlayer)
					{
						this.games_.arrayGamesScripts[j].devS_.awards_bestStudioPoints += (long)this.games_.arrayGamesScripts[j].reviewTotal;
					}
					else if (this.games_.arrayGamesScripts[j].reviewTotal >= 80)
					{
						this.games_.arrayGamesScripts[j].devS_.awards_bestStudioPoints += (long)this.games_.arrayGamesScripts[j].reviewTotal;
					}
				}
			}
		}
		long num = -1L;
		publisherScript publisherScript = null;
		for (int k = 0; k < array.Length; k++)
		{
			publisherScript component = array[k].GetComponent<publisherScript>();
			if (component && component.awards_bestStudioPoints > num)
			{
				num = component.awards_bestStudioPoints;
				publisherScript = component;
			}
		}
		if (publisherScript)
		{
			publisherScript.awards[2]++;
		}
		return publisherScript;
	}

	
	private publisherScript GetBesterPublisher()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<publisherScript>().awards_bestPublisherPoints = 0L;
		}
		for (int j = 0; j < this.games_.arrayGamesScripts.Length; j++)
		{
			if (!this.games_.arrayGamesScripts[j].warBeiAwards && !this.games_.arrayGamesScripts[j].inDevelopment && !this.games_.arrayGamesScripts[j].schublade && !this.games_.arrayGamesScripts[j].pubAngebot && !this.games_.arrayGamesScripts[j].auftragsspiel && !this.games_.arrayGamesScripts[j].handy && this.games_.arrayGamesScripts[j].gameTyp != 2 && (this.games_.arrayGamesScripts[j].typ_standard || this.games_.arrayGamesScripts[j].typ_nachfolger || this.games_.arrayGamesScripts[j].typ_remaster || this.games_.arrayGamesScripts[j].typ_spinoff))
			{
				if (!this.games_.arrayGamesScripts[j].pS_)
				{
					this.games_.arrayGamesScripts[j].FindMyPublisher();
				}
				if (this.games_.arrayGamesScripts[j].pS_)
				{
					if (!this.games_.arrayGamesScripts[j].pS_.isPlayer)
					{
						this.games_.arrayGamesScripts[j].pS_.awards_bestPublisherPoints += this.games_.arrayGamesScripts[j].sellsTotal;
					}
					else if (this.games_.arrayGamesScripts[j].reviewTotal >= 80)
					{
						this.games_.arrayGamesScripts[j].pS_.awards_bestPublisherPoints += this.games_.arrayGamesScripts[j].sellsTotal;
					}
				}
			}
		}
		long num = -1L;
		publisherScript publisherScript = null;
		for (int k = 0; k < array.Length; k++)
		{
			publisherScript component = array[k].GetComponent<publisherScript>();
			if (component && component.awards_bestPublisherPoints > num)
			{
				num = component.awards_bestPublisherPoints;
				publisherScript = component;
			}
		}
		if (publisherScript)
		{
			publisherScript.awards[3]++;
		}
		return publisherScript;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.timeToWait > 0f)
		{
			this.timeToWait = 0f;
			return;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
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

	
	private games games_;

	
	private mpCalls mpCalls_;

	
	private float timeToWait;

	
	public gameScript bestGrafik;

	
	public gameScript bestSound;

	
	public publisherScript bestStudio;

	
	public publisherScript bestPublisher;

	
	public gameScript bestGame;

	
	public gameScript badGame;
}
