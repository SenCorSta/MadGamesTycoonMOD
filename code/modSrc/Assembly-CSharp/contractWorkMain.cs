using System;
using UnityEngine;
using UnityEngine.UI;


public class contractWorkMain : MonoBehaviour
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
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = this.main_.GetComponent<roomDataScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
	}

	
	public contractAuftragsspiel CreateContractGame()
	{
		contractAuftragsspiel component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1]).GetComponent<contractAuftragsspiel>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		return component;
	}

	
	public contractWork CreateContractWork()
	{
		contractWork component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0]).GetComponent<contractWork>();
		component.main_ = this.main_;
		component.mS_ = this.mS_;
		component.tS_ = this.tS_;
		return component;
	}

	
	public void UpdateContractGame(bool forceNewContract)
	{
	}

	
	private int GetGameSize()
	{
		if (this.fS_.IsErforscht(0) && UnityEngine.Random.Range(0, 100) > 70)
		{
			return 1;
		}
		if (this.mS_.year >= 1980 && this.fS_.IsErforscht(1) && UnityEngine.Random.Range(0, 100) > 70)
		{
			return 2;
		}
		if (this.mS_.year >= 1987 && this.fS_.IsErforscht(2) && UnityEngine.Random.Range(0, 100) > 70)
		{
			return 3;
		}
		if (this.mS_.year >= 1995 && this.fS_.IsErforscht(3) && UnityEngine.Random.Range(0, 100) > 70)
		{
			return 4;
		}
		return 0;
	}

	
	private int GetGameGenre()
	{
		int result = 0;
		for (int i = 0; i < this.genres_.genres_RES_POINTS.Length; i++)
		{
			if (this.genres_.IsErforscht(i))
			{
				result = i;
				if (UnityEngine.Random.Range(0, 100) > 70)
				{
					return i;
				}
			}
		}
		return result;
	}

	
	private int GetPlatform()
	{
		int result = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.isUnlocked && !component.vomMarktGenommen && component.typ != 3 && component.typ != 4)
				{
					result = component.myID;
					if (UnityEngine.Random.Range(0, 100) > 70)
					{
						return component.myID;
					}
				}
			}
		}
		return result;
	}

	
	public void UpdateContractWork(bool forceNewContract)
	{
		this.FindScripts();
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					contractWork component = array[i].GetComponent<contractWork>();
					if (component)
					{
						if (component.IsAngenommen())
						{
							component.zeitInWochen--;
							if (component.zeitInWochen < 0)
							{
								this.guiMain_.UpdateAuftragsansehen(-component.GetAuftragsansehen());
								this.mS_.Pay((long)Mathf.RoundToInt((float)component.GetStrafe()), 14);
								string text = this.tS_.GetText(610);
								text = text.Replace("<NAME>", "<b><color=blue>" + component.GetName() + "</color></b>");
								text = text.Replace("<NUM>", "<b><color=red>" + this.mS_.GetMoney((long)component.GetStrafe(), true) + "</color></b>");
								switch (component.art)
								{
								case 0:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[1]);
									break;
								case 1:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[3]);
									break;
								case 2:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[4]);
									break;
								case 3:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[5]);
									break;
								case 4:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[10]);
									break;
								case 5:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[14]);
									break;
								case 6:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[17]);
									break;
								case 7:
									this.guiMain_.CreateLeftNews(-1, this.guiMain_.uiSprites[12], text, this.rdS_.roomData_SPRITE[8]);
									break;
								}
								UnityEngine.Object.Destroy(array[i]);
							}
						}
						else
						{
							component.wochenAlsAngebot++;
							if (component.wochenAlsAngebot > 16 && UnityEngine.Random.Range(0, 100) > 90)
							{
								UnityEngine.Object.Destroy(array[i]);
							}
						}
					}
				}
			}
		}
		if (this.mS_.globalEvent != 2 && ((array.Length < 20 && (float)UnityEngine.Random.Range(0, 100) > 80f - this.mS_.auftragsAnsehen * 0.5f) || forceNewContract))
		{
			contractWork contractWork = this.CreateContractWork();
			contractWork.myID = UnityEngine.Random.Range(1, 999999999);
			switch (UnityEngine.Random.Range(0, 8))
			{
			case 0:
				contractWork.art = 0;
				break;
			case 1:
				if (this.forschungSonstiges_.IsErforscht(28))
				{
					contractWork.art = 1;
				}
				break;
			case 2:
				if (this.forschungSonstiges_.IsErforscht(31))
				{
					contractWork.art = 2;
				}
				break;
			case 3:
				if (this.forschungSonstiges_.IsErforscht(32))
				{
					contractWork.art = 3;
				}
				break;
			case 4:
				if (this.unlock_.Get(8))
				{
					contractWork.art = 4;
				}
				break;
			case 5:
				if (this.forschungSonstiges_.IsErforscht(33))
				{
					contractWork.art = 5;
				}
				break;
			case 6:
				if (this.forschungSonstiges_.IsErforscht(38))
				{
					contractWork.art = 6;
				}
				break;
			case 7:
				if (this.forschungSonstiges_.IsErforscht(39))
				{
					contractWork.art = 7;
				}
				break;
			}
			contractWork.angenommen = false;
			contractWork.wochenAlsAngebot = 0;
			if (contractWork.art != 5 && contractWork.art != 6)
			{
				contractWork.typ = this.tS_.GetRandomContractNumber(contractWork.art);
			}
			contractWork.points = (float)(20 * UnityEngine.Random.Range(10, 30 + Mathf.RoundToInt(this.mS_.auftragsAnsehen * 5f)));
			contractWork.gehalt = Mathf.RoundToInt(contractWork.points * (6f - (float)this.mS_.difficulty)) * 13;
			contractWork.strafe = Mathf.RoundToInt(UnityEngine.Random.Range((float)contractWork.gehalt * 0.1f, (float)contractWork.gehalt * 0.3f));
			contractWork.auftraggeberID = this.GetRandomPublisherID();
			switch (contractWork.art)
			{
			case 1:
				contractWork.points *= 0.8f;
				break;
			case 2:
				contractWork.points *= 0.6f;
				break;
			case 3:
				contractWork.points *= 0.4f;
				break;
			case 4:
				contractWork.points *= 0.3f;
				break;
			case 5:
			{
				contractWork.points *= 1000f;
				int num = Mathf.RoundToInt(contractWork.points) / 100 * 100;
				contractWork.points = (float)num;
				break;
			}
			case 6:
				contractWork.points *= 0.8f;
				break;
			case 7:
				contractWork.points *= 0.8f;
				break;
			}
			contractWork.points *= this.pointsRegulator;
			if (contractWork.art != 5)
			{
				contractWork.zeitInWochen = Mathf.RoundToInt(contractWork.points / 50f + (float)UnityEngine.Random.Range(5, 10));
			}
			else
			{
				contractWork.zeitInWochen = Mathf.RoundToInt(contractWork.points / 20000f + (float)UnityEngine.Random.Range(5, 10));
			}
			contractWork.Init();
		}
		this.UpdateGUI();
	}

	
	private int GetRandomPublisherID()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		if (array.Length != 0)
		{
			bool flag = false;
			while (!flag)
			{
				int num = UnityEngine.Random.Range(0, array.Length);
				if (array[num])
				{
					publisherScript component = array[num].GetComponent<publisherScript>();
					if (component && component.isUnlocked)
					{
						return component.myID;
					}
				}
			}
		}
		return 0;
	}

	
	public void UpdateGUI()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		if (array.Length != 0)
		{
			int num = 0;
			this.anzDEV = 0;
			this.anzQA = 0;
			this.anzGFX = 0;
			this.anzSFX = 0;
			this.anzMotion = 0;
			this.anzProduction = 0;
			this.anzWerkstatt = 0;
			this.anzHardware = 0;
			for (int i = 0; i < array.Length; i++)
			{
				contractWork component = array[i].GetComponent<contractWork>();
				if (component && !component.IsAngenommen())
				{
					num++;
					switch (component.art)
					{
					case 0:
						this.anzDEV++;
						break;
					case 1:
						this.anzQA++;
						break;
					case 2:
						this.anzGFX++;
						break;
					case 3:
						this.anzSFX++;
						break;
					case 4:
						this.anzMotion++;
						break;
					case 5:
						this.anzProduction++;
						break;
					case 6:
						this.anzWerkstatt++;
						break;
					case 7:
						this.anzHardware++;
						break;
					}
				}
			}
			if (!this.uiObjects[0].activeSelf)
			{
				this.uiObjects[0].SetActive(true);
			}
			this.uiObjects[0].transform.GetChild(0).GetComponent<Text>().text = num.ToString();
			string text = this.tS_.GetText(639);
			if (this.unlock_.Get(0))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(19),
					": <color=blue><b>",
					this.anzDEV.ToString(),
					"</b></color>\n"
				});
			}
			if (this.forschungSonstiges_.IsErforscht(28))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(21),
					": <color=blue><b>",
					this.anzQA.ToString(),
					"</b></color>\n"
				});
			}
			if (this.forschungSonstiges_.IsErforscht(31))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(22),
					": <color=blue><b>",
					this.anzGFX.ToString(),
					"</b></color>\n"
				});
			}
			if (this.forschungSonstiges_.IsErforscht(32))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(23),
					": <color=blue><b>",
					this.anzSFX.ToString(),
					"</b></color>\n"
				});
			}
			if (this.unlock_.Get(8))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(28),
					": <color=blue><b>",
					this.anzMotion.ToString(),
					"</b></color>\n"
				});
			}
			if (this.forschungSonstiges_.IsErforscht(33))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(32),
					": <color=blue><b>",
					this.anzProduction.ToString(),
					"</b></color>\n"
				});
			}
			if (this.forschungSonstiges_.IsErforscht(38))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(1508),
					": <color=blue><b>",
					this.anzWerkstatt.ToString(),
					"</b></color>\n"
				});
			}
			if (this.forschungSonstiges_.IsErforscht(39))
			{
				text = string.Concat(new string[]
				{
					text,
					this.tS_.GetText(26),
					": <color=blue><b>",
					this.anzHardware.ToString(),
					"</b></color>\n"
				});
			}
			this.uiObjects[0].GetComponent<tooltip>().c = text;
		}
		else if (this.uiObjects[0].activeSelf)
		{
			this.uiObjects[0].SetActive(false);
		}
		this.anzContractGames = 0;
		if (this.mS_.games_)
		{
			if (this.mS_.games_.arrayGamesScripts.Length != 0)
			{
				string text2 = "<b>" + this.tS_.GetText(640) + "</b>\n\n";
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				for (int j = 0; j < this.mS_.games_.arrayGamesScripts.Length; j++)
				{
					if (!this.mS_.games_.arrayGamesScripts[j].pS_)
					{
						this.mS_.games_.arrayGamesScripts[j].FindMyPublisher();
					}
					if (this.mS_.games_.arrayGamesScripts[j].pS_ && !this.mS_.games_.arrayGamesScripts[j].pS_.tochterfirma && this.mS_.games_.arrayGamesScripts[j].auftragsspiel && !this.mS_.games_.arrayGamesScripts[j].auftragsspiel_Inivs)
					{
						num2++;
						this.anzContractGames++;
						switch (this.mS_.games_.arrayGamesScripts[j].gameSize)
						{
						case 0:
							num3++;
							break;
						case 1:
							num4++;
							break;
						case 2:
							num5++;
							break;
						case 3:
							num6++;
							break;
						case 4:
							num7++;
							break;
						}
					}
				}
				if (num2 > 0)
				{
					if (!this.uiObjects[1].activeSelf)
					{
						this.uiObjects[1].SetActive(true);
					}
					this.uiObjects[1].transform.GetChild(0).GetComponent<Text>().text = num2.ToString();
					if (num3 > 0)
					{
						text2 = string.Concat(new string[]
						{
							text2,
							this.tS_.GetText(329),
							": <color=blue><b>",
							num3.ToString(),
							"</b></color>\n"
						});
					}
					if (num4 > 0)
					{
						text2 = string.Concat(new string[]
						{
							text2,
							this.tS_.GetText(330),
							": <color=blue><b>",
							num4.ToString(),
							"</b></color>\n"
						});
					}
					if (num5 > 0)
					{
						text2 = string.Concat(new string[]
						{
							text2,
							this.tS_.GetText(331),
							": <color=blue><b>",
							num5.ToString(),
							"</b></color>\n"
						});
					}
					if (num6 > 0)
					{
						text2 = string.Concat(new string[]
						{
							text2,
							this.tS_.GetText(332),
							": <color=blue><b>",
							num6.ToString(),
							"</b></color>\n"
						});
					}
					if (num7 > 0)
					{
						text2 = string.Concat(new string[]
						{
							text2,
							this.tS_.GetText(333),
							": <color=blue><b>",
							num7.ToString(),
							"</b></color>\n"
						});
					}
					this.uiObjects[1].GetComponent<tooltip>().c = text2;
					return;
				}
				if (this.uiObjects[1].activeSelf)
				{
					this.uiObjects[1].SetActive(false);
					return;
				}
			}
			else if (this.uiObjects[1].activeSelf)
			{
				this.uiObjects[1].SetActive(false);
				return;
			}
		}
		else if (this.uiObjects[1].activeSelf)
		{
			this.uiObjects[1].SetActive(false);
		}
	}

	
	public float pointsRegulator = 1f;

	
	public int anzContractGames;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private roomDataScript rdS_;

	
	private forschungSonstiges fS_;

	
	private genres genres_;

	
	private unlockScript unlock_;

	
	private forschungSonstiges forschungSonstiges_;

	
	private platforms platforms_;

	
	public int anzDEV;

	
	public int anzQA;

	
	public int anzGFX;

	
	public int anzSFX;

	
	public int anzMotion;

	
	public int anzProduction;

	
	public int anzWerkstatt;

	
	public int anzHardware;
}
