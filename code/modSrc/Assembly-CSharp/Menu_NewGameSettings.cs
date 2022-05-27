using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200017B RID: 379
public class Menu_NewGameSettings : MonoBehaviour
{
	// Token: 0x06000E1D RID: 3613 RVA: 0x00009E3B File Offset: 0x0000803B
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x000A6860 File Offset: 0x000A4A60
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.main_)
		{
			return;
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
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x000A69B0 File Offset: 0x000A4BB0
	private void OnEnable()
	{
		this.FindScripts();
		if (this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().uiObjects[8].GetComponent<Dropdown>().value != 0)
		{
			this.uiObjects[0].GetComponent<Toggle>().isOn = true;
			this.uiObjects[0].GetComponent<Toggle>().interactable = false;
			return;
		}
		this.uiObjects[0].GetComponent<Toggle>().interactable = true;
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x00009E49 File Offset: 0x00008049
	public void Init()
	{
		this.FindScripts();
		this.TOGGLE_RandomEvents();
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x00009E57 File Offset: 0x00008057
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x000A6A28 File Offset: 0x000A4C28
	public void BUTTON_OK()
	{
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		Menu_NewGame component = this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>();
		if (!this.mS_.multiplayer)
		{
			this.mS_.LoadOffice(component.uiObjects[8].GetComponent<Dropdown>().value + 3, false);
		}
		else
		{
			this.mS_.LoadOffice(this.mS_.office, false);
		}
		this.mS_.CreateStartAuto(component.uiObjects[8].GetComponent<Dropdown>().value + 3);
		this.mS_.InitNewGame();
		if (!this.mS_.multiplayer)
		{
			this.mS_.companyName = component.uiObjects[0].GetComponent<InputField>().text;
			this.mS_.logo = component.logo;
			this.mS_.country = component.country;
			this.mS_.difficulty = component.uiObjects[1].GetComponent<Dropdown>().value;
			this.mS_.companySpecialGenre = component.genre;
			if (component.uiObjects[7].GetComponent<Dropdown>().value < 4)
			{
				this.mS_.anzKonkurrenten = (component.uiObjects[7].GetComponent<Dropdown>().value + 1) * 20;
			}
			else
			{
				this.mS_.anzKonkurrenten = 99999;
			}
			this.mS_.settings_TutorialOff = this.uiObjects[0].GetComponent<Toggle>().isOn;
			this.mS_.settings_RandomEventsOff = this.uiObjects[1].GetComponent<Toggle>().isOn;
			this.mS_.settings_RandomReviews = this.uiObjects[2].GetComponent<Toggle>().isOn;
			this.mS_.settings_history = this.uiObjects[6].GetComponent<Toggle>().isOn;
			this.mS_.settings_plattformEnd = this.uiObjects[7].GetComponent<Toggle>().isOn;
			if (this.uiObjects[3].GetComponent<Toggle>().isOn)
			{
				this.SetRandomPlattformPop();
			}
			if (this.uiObjects[4].GetComponent<Toggle>().isOn)
			{
				for (int i = 0; i < this.genres_.genres_UNLOCK.Length; i++)
				{
					for (int j = 0; j < 8; j++)
					{
						this.genres_.genres_FOCUS[i, j] = 0;
					}
					for (int k = 0; k < 40; k++)
					{
						int num = UnityEngine.Random.Range(0, 8);
						this.genres_.genres_FOCUS[i, num]++;
						if (this.genres_.genres_FOCUS[i, num] > 10)
						{
							this.genres_.genres_FOCUS[i, num]--;
							k--;
						}
					}
					for (int l = 0; l < 3; l++)
					{
						this.genres_.genres_ALIGN[i, l] = UnityEngine.Random.Range(0, 11);
					}
				}
			}
			if (this.uiObjects[5].GetComponent<Toggle>().isOn)
			{
				for (int m = 0; m < this.genres_.genres_UNLOCK.Length; m++)
				{
					int num2 = this.genres_.genres_UNLOCK.Length;
					for (int n = 0; n < num2; n++)
					{
						this.genres_.genres_COMBINATION[m, n] = false;
					}
					this.genres_.genres_COMBINATION[m, UnityEngine.Random.Range(0, num2)] = true;
					this.genres_.genres_COMBINATION[m, UnityEngine.Random.Range(0, num2)] = true;
					this.genres_.genres_COMBINATION[m, UnityEngine.Random.Range(0, num2)] = true;
					this.genres_.genres_COMBINATION[m, UnityEngine.Random.Range(0, num2)] = true;
					this.genres_.genres_COMBINATION[m, m] = false;
				}
			}
			this.mS_.speedSetting = this.mS_.gameSpeeds[component.uiObjects[3].GetComponent<Dropdown>().value];
			int num3 = 1976;
			switch (component.uiObjects[2].GetComponent<Dropdown>().value)
			{
			case 0:
				num3 = 1976;
				break;
			case 1:
				num3 = 1985;
				this.mS_.money = 2000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.UnlockPC();
				break;
			case 2:
				num3 = 1995;
				this.mS_.money = 4000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.fS_.RES_POINTS_LEFT[1] = 0f;
				this.fS_.RES_POINTS_LEFT[28] = 0f;
				this.UnlockPC();
				break;
			case 3:
				num3 = 2005;
				this.mS_.money = 8000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.fS_.RES_POINTS_LEFT[1] = 0f;
				this.fS_.RES_POINTS_LEFT[2] = 0f;
				this.fS_.RES_POINTS_LEFT[28] = 0f;
				this.fS_.RES_POINTS_LEFT[31] = 0f;
				this.UnlockPC();
				break;
			case 4:
				num3 = 2015;
				this.mS_.money = 12000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.fS_.RES_POINTS_LEFT[1] = 0f;
				this.fS_.RES_POINTS_LEFT[2] = 0f;
				this.fS_.RES_POINTS_LEFT[3] = 0f;
				this.fS_.RES_POINTS_LEFT[28] = 0f;
				this.fS_.RES_POINTS_LEFT[31] = 0f;
				this.fS_.RES_POINTS_LEFT[32] = 0f;
				this.UnlockPC();
				break;
			}
			this.InitStartjahr(num3);
			this.SetFirmenwert(num3);
			this.mS_.CreateStartAuftragsspiele();
		}
		else
		{
			this.mS_.CreateStartAuftragsspiele();
			this.mS_.year = 1976;
			this.mS_.month = 1;
			this.mS_.anzKonkurrenten = 20;
			this.mS_.speedSetting = this.mS_.gameSpeeds[1];
			this.mS_.settings_TutorialOff = true;
			this.guiMain_.uiObjects[201].SetActive(false);
			this.guiMain_.uiObjects[202].SetActive(true);
			this.mS_.speedSetting = this.mS_.gameSpeeds[this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[44].GetComponent<Dropdown>().value];
			this.mS_.settings_arbeitsgeschwindigkeitAnpassen = this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[45].GetComponent<Toggle>().isOn;
			this.mS_.companySpecialGenre = this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[46].GetComponent<Dropdown>().value;
			if (this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[42].GetComponent<Toggle>().isOn)
			{
				this.SetRandomPlattformPop();
			}
			if (this.mS_.mpCalls_.isServer)
			{
				if (this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[43].GetComponent<Toggle>().isOn)
				{
					for (int num4 = 0; num4 < this.genres_.genres_UNLOCK.Length; num4++)
					{
						for (int num5 = 0; num5 < 8; num5++)
						{
							this.genres_.genres_FOCUS[num4, num5] = 0;
						}
						for (int num6 = 0; num6 < 40; num6++)
						{
							int num7 = UnityEngine.Random.Range(0, 8);
							this.genres_.genres_FOCUS[num4, num7]++;
							if (this.genres_.genres_FOCUS[num4, num7] > 10)
							{
								this.genres_.genres_FOCUS[num4, num7]--;
								num6--;
							}
						}
						for (int num8 = 0; num8 < 3; num8++)
						{
							this.genres_.genres_ALIGN[num4, num8] = UnityEngine.Random.Range(0, 11);
						}
						this.mS_.mpCalls_.SERVER_Send_GenreDesign(num4, Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 0]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 1]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 2]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 3]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 4]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 5]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 6]), Mathf.RoundToInt((float)this.genres_.genres_FOCUS[num4, 7]), Mathf.RoundToInt((float)this.genres_.genres_ALIGN[num4, 0]), Mathf.RoundToInt((float)this.genres_.genres_ALIGN[num4, 1]), Mathf.RoundToInt((float)this.genres_.genres_ALIGN[num4, 2]));
					}
				}
				if (this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[52].GetComponent<Toggle>().isOn)
				{
					for (int num9 = 0; num9 < this.genres_.genres_UNLOCK.Length; num9++)
					{
						int num10 = this.genres_.genres_UNLOCK.Length;
						for (int num11 = 0; num11 < num10; num11++)
						{
							this.genres_.genres_COMBINATION[num9, num11] = false;
						}
						this.genres_.genres_COMBINATION[num9, UnityEngine.Random.Range(0, num10)] = true;
						this.genres_.genres_COMBINATION[num9, UnityEngine.Random.Range(0, num10)] = true;
						this.genres_.genres_COMBINATION[num9, UnityEngine.Random.Range(0, num10)] = true;
						this.genres_.genres_COMBINATION[num9, UnityEngine.Random.Range(0, num10)] = true;
						this.genres_.genres_COMBINATION[num9, num9] = false;
						this.mS_.mpCalls_.SERVER_Send_GenreCombination(num9);
					}
				}
			}
			int num12 = 1976;
			switch (this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[32].GetComponent<Dropdown>().value)
			{
			case 0:
				num12 = 1976;
				break;
			case 1:
				num12 = 1985;
				this.mS_.money = 2000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				break;
			case 2:
				num12 = 1995;
				this.mS_.money = 4000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.fS_.RES_POINTS_LEFT[1] = 0f;
				this.fS_.RES_POINTS_LEFT[28] = 0f;
				break;
			case 3:
				num12 = 2005;
				this.mS_.money = 8000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.fS_.RES_POINTS_LEFT[1] = 0f;
				this.fS_.RES_POINTS_LEFT[2] = 0f;
				this.fS_.RES_POINTS_LEFT[28] = 0f;
				this.fS_.RES_POINTS_LEFT[31] = 0f;
				break;
			case 4:
				num12 = 2015;
				this.mS_.money = 12000000L;
				this.fS_.RES_POINTS_LEFT[0] = 0f;
				this.fS_.RES_POINTS_LEFT[1] = 0f;
				this.fS_.RES_POINTS_LEFT[2] = 0f;
				this.fS_.RES_POINTS_LEFT[3] = 0f;
				this.fS_.RES_POINTS_LEFT[28] = 0f;
				this.fS_.RES_POINTS_LEFT[31] = 0f;
				this.fS_.RES_POINTS_LEFT[32] = 0f;
				break;
			}
			this.InitStartjahr(num12);
			this.SetFirmenwert(num12);
			if (this.mS_.mpCalls_.isServer)
			{
				this.SendAllPlatforms();
				this.SendAllPublisher();
				this.SendAllCopyProtect();
				this.SendAllAntiCheat();
				this.mS_.mpCalls_.SERVER_Send_Hardware();
				this.mS_.mpCalls_.SERVER_Send_HardwareFeatures();
				this.mS_.mpCalls_.SERVER_Send_EngineFeatures();
				this.mS_.mpCalls_.SERVER_Send_GameplayFeatures();
				this.SendAllnpcEngines();
			}
		}
		this.guiMain_.UpdateOnce();
		UnityEngine.Object.Destroy(GameObject.Find("CHARNEWGAME"));
		this.mS_.DestroyMainMenuObjects();
		this.guiMain_.uiObjects[151].SetActive(false);
		this.guiMain_.uiObjects[159].SetActive(false);
		this.guiMain_.uiObjects[162].SetActive(false);
		this.guiMain_.OpenMenu(false);
		this.guiMain_.MessageBox(this.tS_.GetText(838), true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x000A7810 File Offset: 0x000A5A10
	private void InitStartjahr(int startjahr)
	{
		if (startjahr != 1976)
		{
			int i = 0;
			while (i < 10000)
			{
				i++;
				this.unlock_.CheckUnlock(false);
				this.mS_.month++;
				for (int j = 0; j < 4; j++)
				{
					this.mS_.platforms_.UpdatePlatformSells(false, false);
					this.mS_.copyProtect_.UpdateEffekt();
					this.mS_.antiCheat_.UpdateEffekt();
				}
				if (this.mS_.month == 13)
				{
					this.mS_.month = 1;
					this.mS_.year++;
					this.mS_.ResetJahresbilanz();
				}
				if (this.mS_.year == startjahr)
				{
					this.unlock_.CheckUnlock(false);
					break;
				}
			}
			for (int k = 0; k < this.gF_.gameplayFeatures_UNLOCK.Length; k++)
			{
				if (this.gF_.gameplayFeatures_UNLOCK[k])
				{
					this.gF_.gameplayFeatures_RES_POINTS_LEFT[k] = 0f;
				}
			}
			for (int l = 0; l < this.eF_.engineFeatures_UNLOCK.Length; l++)
			{
				if (this.eF_.engineFeatures_UNLOCK[l])
				{
					this.eF_.engineFeatures_RES_POINTS_LEFT[l] = 0f;
				}
			}
		}
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x000A7964 File Offset: 0x000A5B64
	private void SetFirmenwert(int startjahr)
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component)
				{
					component.firmenwert += component.firmenwert * (long)(startjahr - 1976);
				}
			}
		}
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x000A79C4 File Offset: 0x000A5BC4
	private void SetRandomPlattformPop()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component.typ != 4)
				{
					int num = UnityEngine.Random.Range(0, 4);
					if (component.date_year_end >= 999999)
					{
						num = 3;
					}
					switch (num)
					{
					case 0:
						component.units_max = UnityEngine.Random.Range(1000, 100000);
						if (component.date_year_end < 999999)
						{
							component.date_year_end = UnityEngine.Random.RandomRange(component.date_year + 2, component.date_year + 4);
						}
						break;
					case 1:
						component.units_max = UnityEngine.Random.Range(1000000, 10000000);
						if (component.date_year_end < 999999)
						{
							component.date_year_end = UnityEngine.Random.RandomRange(component.date_year + 4, component.date_year + 6);
						}
						break;
					case 2:
						component.units_max = UnityEngine.Random.Range(20000000, 30000000);
						if (component.date_year_end < 999999)
						{
							component.date_year_end = UnityEngine.Random.RandomRange(component.date_year + 6, component.date_year + 8);
						}
						break;
					case 3:
						component.units_max = UnityEngine.Random.Range(60000000, 100000000);
						if (component.date_year_end < 999999)
						{
							component.date_year_end = UnityEngine.Random.RandomRange(component.date_year + 8, component.date_year + 10);
						}
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x000A7B48 File Offset: 0x000A5D48
	private void SendAllPlatforms()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component)
				{
					this.mS_.mpCalls_.SERVER_Send_Platform(component);
				}
			}
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x000A7B9C File Offset: 0x000A5D9C
	private void SendAllPublisher()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component)
				{
					this.mS_.mpCalls_.SERVER_Send_Publisher(component);
				}
			}
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x000A7BF0 File Offset: 0x000A5DF0
	private void SendAllCopyProtect()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				copyProtectScript component = array[i].GetComponent<copyProtectScript>();
				if (component)
				{
					this.mS_.mpCalls_.SERVER_Send_CopyProtect(component);
				}
			}
		}
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x000A7C44 File Offset: 0x000A5E44
	private void SendAllAntiCheat()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				antiCheatScript component = array[i].GetComponent<antiCheatScript>();
				if (component)
				{
					this.mS_.mpCalls_.SERVER_Send_AntiCheat(component);
				}
			}
		}
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x000A7C98 File Offset: 0x000A5E98
	private void SendAllnpcEngines()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component)
				{
					this.mS_.mpCalls_.SERVER_Send_NpcEngine(component);
				}
			}
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x000A7CEC File Offset: 0x000A5EEC
	private void UnlockPC()
	{
		GameObject gameObject = GameObject.Find("PLATFORM_17");
		if (gameObject)
		{
			gameObject.GetComponent<platformScript>().inBesitz = true;
		}
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x000A7D18 File Offset: 0x000A5F18
	public void TOGGLE_RandomEvents()
	{
		if (this.uiObjects[1].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[6].GetComponent<Toggle>().interactable = false;
			this.uiObjects[6].GetComponent<Toggle>().isOn = false;
			return;
		}
		this.uiObjects[6].GetComponent<Toggle>().interactable = true;
	}

	// Token: 0x040012B1 RID: 4785
	public GameObject[] uiObjects;

	// Token: 0x040012B2 RID: 4786
	private GameObject main_;

	// Token: 0x040012B3 RID: 4787
	private mainScript mS_;

	// Token: 0x040012B4 RID: 4788
	private textScript tS_;

	// Token: 0x040012B5 RID: 4789
	private GUI_Main guiMain_;

	// Token: 0x040012B6 RID: 4790
	private sfxScript sfx_;

	// Token: 0x040012B7 RID: 4791
	private genres genres_;

	// Token: 0x040012B8 RID: 4792
	private unlockScript unlock_;

	// Token: 0x040012B9 RID: 4793
	private engineFeatures eF_;

	// Token: 0x040012BA RID: 4794
	private gameplayFeatures gF_;

	// Token: 0x040012BB RID: 4795
	private forschungSonstiges fS_;
}
