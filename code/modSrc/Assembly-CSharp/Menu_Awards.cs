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

	
	public void Multiplayer_FindWinners(int IDbestGrafik, int IDbestSound, int IDbestStudio, int bestStudioPlayer_, int IDbestPublisher, int bestPublisherPlayer_, int IDbestGame, int IDbadGame)
	{
		this.bestGrafik = null;
		this.bestSound = null;
		this.bestStudio = null;
		this.bestStudioPlayer = -1;
		this.bestPublisher = null;
		this.bestPublisherPlayer = -1;
		this.bestGame = null;
		this.badGame = null;
		this.bestStudioPlayer = bestStudioPlayer_;
		this.bestPublisherPlayer = bestPublisherPlayer_;
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
			this.bestStudioPlayer = -1;
			this.bestPublisher = null;
			this.bestPublisherPlayer = -1;
			this.bestGame = null;
			this.badGame = null;
			if (this.mS_.multiplayer)
			{
				for (int i = 0; i < this.mpCalls_.playersMP.Count; i++)
				{
					this.mpCalls_.playersMP[i].awards_SOTY = 0L;
					this.mpCalls_.playersMP[i].awards_POTY = 0L;
				}
			}
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
			this.mpCalls_.SERVER_Send_Award(bestGrafik_, bestSound_, bestStudio_, this.bestStudioPlayer, bestPublisher_, this.bestPublisherPlayer, bestGame_, badGame_);
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
		this.mS_.AddMadGameConvetionVerlauf(bestGrafik_, bestSound_, bestStudio_, this.bestStudioPlayer, bestPublisher_, this.bestPublisherPlayer, bestGame_, badBame_);
	}

	
	private IEnumerator ShowAwards()
	{
		if (this.mS_.settings_ && this.mS_.settings_.hideAwards)
		{
			this.timeToWait = 0f;
		}
		float myFans = (float)this.genres_.GetAmountFans();
		yield return new WaitForSeconds(this.timeToWait);
		gameScript gameScript = this.bestGrafik;
		if (gameScript)
		{
			gameScript.AddHype(50f);
			if (!gameScript.playerGame)
			{
				this.uiObjects[0].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[0].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.multiplayerSlot),
						"</color>"
					});
				}
				this.sfx_.PlaySound(45);
				if (!gameScript.playerGame && gameScript.multiplayerSlot == -1)
				{
					gameScript.AddIpPoints(40f);
				}
			}
			else
			{
				float num = myFans * 0.02f;
				if (num > 40000f)
				{
					num = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(38);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num + 1000f), false) + "</color>");
				this.uiObjects[0].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num + 1000f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.awards[0]++;
				this.mS_.award_Grafik++;
				this.mS_.AddStudioPoints(40);
				gameScript.AddIpPoints(40f);
				this.uiObjects[6].SetActive(true);
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
			if (!gameScript.playerGame)
			{
				this.uiObjects[1].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[1].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.multiplayerSlot),
						"</color>"
					});
				}
				this.sfx_.PlaySound(45);
				if (!gameScript.playerGame && gameScript.multiplayerSlot == -1)
				{
					gameScript.AddIpPoints(40f);
				}
			}
			else
			{
				float num2 = myFans * 0.015f;
				if (num2 > 40000f)
				{
					num2 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(39);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num2 + 1000f), false) + "</color>");
				this.uiObjects[1].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num2 + 1000f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.awards[1]++;
				this.mS_.award_Sound++;
				this.mS_.AddStudioPoints(40);
				gameScript.AddIpPoints(40f);
				this.uiObjects[7].SetActive(true);
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
			this.uiObjects[2].GetComponent<Text>().text = publisherScript.GetName();
			this.sfx_.PlaySound(45);
		}
		else if (!this.mS_.multiplayer || this.bestStudioPlayer == this.mpCalls_.myID)
		{
			float num3 = myFans * 0.045f;
			if (num3 > 40000f)
			{
				num3 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
			}
			if (this.mS_.achScript_)
			{
				this.mS_.achScript_.SetAchivement(36);
			}
			string text = this.tS_.GetText(763);
			text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num3 + 2500f), false) + "</color>");
			this.uiObjects[2].GetComponent<Text>().text = "<color=blue>" + this.mS_.companyName + "</color>\n" + text;
			this.mS_.AddFans(Mathf.RoundToInt(num3 + 2500f), -1);
			this.sfx_.PlaySound(44);
			this.mS_.awards[2]++;
			this.mS_.award_Studio++;
			this.mS_.AddStudioPoints(100);
			this.uiObjects[8].SetActive(true);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = "<color=magenta>" + this.mpCalls_.GetCompanyName(this.bestStudioPlayer) + "</color>";
			this.sfx_.PlaySound(45);
		}
		yield return new WaitForSeconds(this.timeToWait);
		publisherScript = this.bestPublisher;
		if (publisherScript)
		{
			this.uiObjects[3].GetComponent<Text>().text = publisherScript.GetName();
			this.sfx_.PlaySound(45);
		}
		else if (!this.mS_.multiplayer || this.bestPublisherPlayer == this.mpCalls_.myID)
		{
			float num4 = myFans * 0.04f;
			if (num4 > 40000f)
			{
				num4 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
			}
			if (this.mS_.achScript_)
			{
				this.mS_.achScript_.SetAchivement(37);
			}
			string text = this.tS_.GetText(763);
			text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num4 + 2500f), false) + "</color>");
			this.uiObjects[3].GetComponent<Text>().text = "<color=blue>" + this.mS_.companyName + "</color>\n" + text;
			this.mS_.AddFans(Mathf.RoundToInt(num4 + 2500f), -1);
			this.sfx_.PlaySound(44);
			this.mS_.awards[3]++;
			this.mS_.award_Publisher++;
			this.mS_.AddStudioPoints(100);
			this.uiObjects[9].SetActive(true);
		}
		else
		{
			this.uiObjects[3].GetComponent<Text>().text = "<color=magenta>" + this.mpCalls_.GetCompanyName(this.bestPublisherPlayer) + "</color>";
			this.sfx_.PlaySound(45);
		}
		yield return new WaitForSeconds(this.timeToWait);
		gameScript = this.bestGame;
		if (gameScript)
		{
			gameScript.AddHype(100f);
			gameScript.goty = true;
			if (!gameScript.playerGame)
			{
				this.uiObjects[4].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[4].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.multiplayerSlot),
						"</color>"
					});
				}
				this.sfx_.PlaySound(45);
				if (!gameScript.playerGame && gameScript.multiplayerSlot == -1)
				{
					gameScript.AddIpPoints(100f);
				}
			}
			else
			{
				float num5 = myFans * 0.05f;
				if (num5 > 40000f)
				{
					num5 = (float)(40000 + UnityEngine.Random.Range(0, 10000));
				}
				if (this.mS_.achScript_)
				{
					this.mS_.achScript_.SetAchivement(35);
				}
				string text = this.tS_.GetText(763);
				text = text.Replace("<NUM>", "<color=green>+" + this.mS_.GetMoney((long)Mathf.RoundToInt(num5 + 3000f), false) + "</color>");
				this.uiObjects[4].GetComponent<Text>().text = "<color=blue>" + gameScript.GetNameWithTag() + "</color>\n" + text;
				this.mS_.AddFans(Mathf.RoundToInt(num5 + 3000f), -1);
				this.sfx_.PlaySound(44);
				this.mS_.awards[4]++;
				this.mS_.award_GOTY++;
				this.mS_.AddStudioPoints(75);
				gameScript.AddIpPoints(100f);
				this.uiObjects[10].SetActive(true);
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
			if (!gameScript.playerGame)
			{
				this.uiObjects[5].GetComponent<Text>().text = gameScript.GetNameWithTag();
				if (this.mS_.multiplayer && gameScript.multiplayerSlot != -1)
				{
					this.uiObjects[5].GetComponent<Text>().text = string.Concat(new string[]
					{
						"<color=magenta>",
						gameScript.GetNameWithTag(),
						"\n",
						this.mpCalls_.GetCompanyName(gameScript.multiplayerSlot),
						"</color>"
					});
				}
				this.sfx_.PlaySound(44);
			}
			else
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
				this.mS_.awards[5]++;
				this.uiObjects[11].SetActive(true);
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
		return script_ && !script_.inDevelopment && !script_.schublade && !script_.pubAngebot && !script_.auftragsspiel && (script_.typ_standard || script_.typ_nachfolger || script_.typ_remaster || script_.typ_spinoff) && !script_.typ_addon && !script_.typ_addonStandalone && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_budget && !script_.typ_bundleAddon && !script_.typ_goty;
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
			return this.games_.arrayGamesScripts[num];
		}
		return null;
	}

	
	private publisherScript GetBestesStudio()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		long[] array2 = new long[array.Length];
		int num = 0;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && !this.games_.arrayGamesScripts[i].inDevelopment && !this.games_.arrayGamesScripts[i].schublade && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_remaster || this.games_.arrayGamesScripts[i].typ_spinoff))
			{
				if (this.games_.arrayGamesScripts[i].developerID != -1)
				{
					array2[this.games_.arrayGamesScripts[i].developerID] += (long)this.games_.arrayGamesScripts[i].reviewTotal;
				}
				else if (this.games_.arrayGamesScripts[i].reviewTotal >= 80)
				{
					if (!this.mS_.multiplayer)
					{
						num += this.games_.arrayGamesScripts[i].reviewTotal;
					}
					else
					{
						player_mp player_mp = this.mpCalls_.FindPlayer(this.games_.arrayGamesScripts[i].multiplayerSlot);
						if (player_mp != null)
						{
							player_mp.awards_SOTY += (long)this.games_.arrayGamesScripts[i].reviewTotal;
						}
					}
				}
			}
		}
		long num2 = -1L;
		int num3 = -1;
		for (int j = 0; j < array2.Length; j++)
		{
			if (array2[j] > num2)
			{
				num2 = array2[j];
				num3 = j;
			}
		}
		if (this.mS_.multiplayer)
		{
			long num4 = -1L;
			int num5 = -1;
			for (int k = 0; k < this.mpCalls_.playersMP.Count; k++)
			{
				if (this.mpCalls_.playersMP[k].awards_SOTY > num4)
				{
					num4 = this.mpCalls_.playersMP[k].awards_SOTY;
					num5 = this.mpCalls_.playersMP[k].playerID;
				}
			}
			if (num2 < num4)
			{
				this.bestStudioPlayer = num5;
				return null;
			}
		}
		if (!this.mS_.multiplayer && num2 < (long)num)
		{
			return null;
		}
		return array[num3].GetComponent<publisherScript>();
	}

	
	private publisherScript GetBesterPublisher()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		long[] array2 = new long[array.Length];
		long num = 0L;
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (!this.games_.arrayGamesScripts[i].warBeiAwards && !this.games_.arrayGamesScripts[i].inDevelopment && !this.games_.arrayGamesScripts[i].schublade && !this.games_.arrayGamesScripts[i].pubAngebot && !this.games_.arrayGamesScripts[i].auftragsspiel && !this.games_.arrayGamesScripts[i].handy && this.games_.arrayGamesScripts[i].gameTyp != 2 && (this.games_.arrayGamesScripts[i].typ_standard || this.games_.arrayGamesScripts[i].typ_nachfolger || this.games_.arrayGamesScripts[i].typ_remaster || this.games_.arrayGamesScripts[i].typ_spinoff))
			{
				if (this.games_.arrayGamesScripts[i].publisherID != -1)
				{
					array2[this.games_.arrayGamesScripts[i].publisherID] += this.games_.arrayGamesScripts[i].sellsTotal;
				}
				else if (this.games_.arrayGamesScripts[i].reviewTotal >= 80)
				{
					if (!this.mS_.multiplayer)
					{
						num += this.games_.arrayGamesScripts[i].sellsTotal;
					}
					else
					{
						player_mp player_mp = this.mpCalls_.FindPlayer(this.games_.arrayGamesScripts[i].multiplayerSlot);
						if (player_mp != null)
						{
							player_mp.awards_POTY += this.games_.arrayGamesScripts[i].sellsTotal;
						}
					}
				}
			}
		}
		long num2 = -1L;
		int num3 = -1;
		for (int j = 0; j < array2.Length; j++)
		{
			if (array2[j] > num2)
			{
				num2 = array2[j];
				num3 = j;
			}
		}
		if (this.mS_.multiplayer)
		{
			long num4 = -1L;
			int num5 = -1;
			for (int k = 0; k < this.mpCalls_.playersMP.Count; k++)
			{
				if (this.mpCalls_.playersMP[k].awards_SOTY > num4)
				{
					num4 = this.mpCalls_.playersMP[k].awards_SOTY;
					num5 = this.mpCalls_.playersMP[k].playerID;
				}
			}
			if (num2 < num4)
			{
				this.bestPublisherPlayer = num5;
				return null;
			}
		}
		if (!this.mS_.multiplayer && num2 < num)
		{
			return null;
		}
		return array[num3].GetComponent<publisherScript>();
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

	
	public int bestStudioPlayer;

	
	public publisherScript bestPublisher;

	
	public int bestPublisherPlayer;

	
	public gameScript bestGame;

	
	public gameScript badGame;
}
