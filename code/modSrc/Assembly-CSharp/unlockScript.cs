using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class unlockScript : MonoBehaviour
{
	// Token: 0x06001F9C RID: 8092 RVA: 0x00148475 File Offset: 0x00146675
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x00148480 File Offset: 0x00146680
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = base.GetComponent<genres>();
		}
		if (!this.eF_)
		{
			this.eF_ = base.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = base.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = base.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = base.GetComponent<hardwareFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = base.GetComponent<games>();
		}
		if (!this.tS_)
		{
			this.tS_ = base.GetComponent<textScript>();
		}
		if (!this.themes_)
		{
			this.themes_ = base.GetComponent<themes>();
		}
		if (!this.rdS_)
		{
			this.rdS_ = base.GetComponent<roomDataScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x001485AC File Offset: 0x001467AC
	public void NewGameUnlocks()
	{
		for (int i = 0; i < this.unlock.Length; i++)
		{
			this.unlock[i] = false;
		}
		this.unlock[0] = true;
		this.unlock[2] = true;
		this.unlock[13] = true;
		this.unlock[14] = true;
		this.unlock[58] = true;
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x00148605 File Offset: 0x00146805
	public bool Get(int i)
	{
		return this.unlock[i];
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x00148610 File Offset: 0x00146810
	public void CheckUnlock(bool showMessage)
	{
		this.FindScripts();
		for (int i = 0; i < this.genres_.genres_UNLOCK.Length; i++)
		{
			if (!this.genres_.genres_UNLOCK[i] && this.genres_.genres_DATE_YEAR[i] == this.mS_.year && this.genres_.genres_DATE_MONTH[i] == this.mS_.month)
			{
				this.genres_.genres_UNLOCK[i] = true;
				if (showMessage)
				{
					this.guiMain_.CreateTopNewsForschung(this.genres_.GetName(i), this.genres_.GetPic(i));
				}
			}
		}
		int num = 0;
		for (int j = 0; j < this.eF_.engineFeatures_UNLOCK.Length; j++)
		{
			if (!this.eF_.engineFeatures_UNLOCK[j] && this.eF_.engineFeatures_DATE_YEAR[j] == this.mS_.year && this.eF_.engineFeatures_DATE_MONTH[j] == this.mS_.month)
			{
				this.eF_.engineFeatures_UNLOCK[j] = true;
				if (showMessage)
				{
					this.guiMain_.CreateTopNewsForschung(this.eF_.GetName(j), this.eF_.GetTypPic(j));
				}
			}
			if (!this.Get(58))
			{
				if (this.eF_.engineFeatures_UNLOCK[j] && this.eF_.engineFeatures_RES_POINTS_LEFT[j] <= 0f)
				{
					num++;
				}
				if (num >= 5)
				{
					this.unlock[58] = true;
					if (showMessage)
					{
						this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(240), this.guiMain_.uiSprites[4]);
						this.guiMain_.UnlockBox(this.tS_.GetText(878), true);
					}
				}
			}
		}
		for (int k = 0; k < this.gF_.gameplayFeatures_UNLOCK.Length; k++)
		{
			if (!this.gF_.gameplayFeatures_UNLOCK[k] && this.gF_.gameplayFeatures_DATE_YEAR[k] == this.mS_.year && this.gF_.gameplayFeatures_DATE_MONTH[k] == this.mS_.month)
			{
				this.gF_.gameplayFeatures_UNLOCK[k] = true;
				if (showMessage)
				{
					this.guiMain_.CreateTopNewsForschung(this.gF_.GetName(k), this.gF_.GetTypSprite(k));
				}
			}
		}
		for (int l = 0; l < this.hardware_.hardware_UNLOCK.Length; l++)
		{
			if (!this.hardware_.hardware_UNLOCK[l] && this.hardware_.hardware_DATE_YEAR[l] == this.mS_.year && this.hardware_.hardware_DATE_MONTH[l] == this.mS_.month)
			{
				this.hardware_.hardware_UNLOCK[l] = true;
				if (showMessage)
				{
					this.guiMain_.CreateTopNewsForschung(this.hardware_.GetName(l), this.hardware_.GetTypPic(l));
				}
			}
		}
		for (int m = 0; m < this.hardwareFeatures_.hardFeat_UNLOCK.Length; m++)
		{
			if (!this.hardwareFeatures_.hardFeat_UNLOCK[m] && this.hardwareFeatures_.hardFeat_DATE_YEAR[m] == this.mS_.year && this.hardwareFeatures_.hardFeat_DATE_MONTH[m] == this.mS_.month)
			{
				this.hardwareFeatures_.hardFeat_UNLOCK[m] = true;
				if (showMessage)
				{
					this.guiMain_.CreateTopNewsForschung(this.hardwareFeatures_.GetName(m), this.hardwareFeatures_.GetSprite(m));
				}
			}
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int n = 0; n < array.Length; n++)
		{
			if (array[n])
			{
				platformScript component = array[n].GetComponent<platformScript>();
				if (component && component.OwnerIsNPC())
				{
					if (!component.isUnlocked)
					{
						if (component.date_year == this.mS_.year && component.date_month == this.mS_.month)
						{
							component.isUnlocked = true;
							if (showMessage)
							{
								this.guiMain_.CreateTopNewsPlatform(component);
							}
						}
					}
					else if (!this.mS_.settings_plattformEnd)
					{
						if (!component.vomMarktGenommen && component.date_year_end == this.mS_.year && component.date_month_end == this.mS_.month)
						{
							component.vomMarktGenommen = true;
							if (showMessage)
							{
								this.guiMain_.CreateTopNewsPlatformRemove(component);
							}
						}
					}
					else if (!component.vomMarktGenommen && component.GetPlattformEnd())
					{
						component.vomMarktGenommen = true;
						component.date_year_end = this.mS_.year;
						component.date_month_end = this.mS_.month;
						if (showMessage)
						{
							this.guiMain_.CreateTopNewsPlatformRemove(component);
						}
					}
					if (!this.Get(65) && component.isUnlocked && component.typ == 3)
					{
						this.unlock[65] = true;
						if (showMessage)
						{
							this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1060), this.guiMain_.uiSprites[40]);
							this.guiMain_.UnlockBox(this.tS_.GetText(1506), true);
							return;
						}
					}
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("Engine");
		for (int num2 = 0; num2 < array.Length; num2++)
		{
			if (array[num2])
			{
				engineScript component2 = array[num2].GetComponent<engineScript>();
				if (component2 && !component2.isUnlocked && component2.ownerID != this.mS_.myID && component2.date_year == this.mS_.year && component2.date_month == this.mS_.month)
				{
					component2.isUnlocked = true;
					component2.InitNpcEngine();
					if (showMessage)
					{
						this.guiMain_.CreateTopNewsNpcEngine(component2.GetName());
					}
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("CopyProtect");
		for (int num3 = 0; num3 < array.Length; num3++)
		{
			if (array[num3])
			{
				copyProtectScript component3 = array[num3].GetComponent<copyProtectScript>();
				if (component3 && !component3.isUnlocked && component3.date_year == this.mS_.year && component3.date_month == this.mS_.month)
				{
					component3.isUnlocked = true;
					if (showMessage)
					{
						this.guiMain_.CreateTopNewsCopyProtect(component3.GetName());
					}
					if (!this.Get(31))
					{
						this.unlock[31] = true;
						this.guiMain_.UnlockBox(this.tS_.GetText(895), true);
					}
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("AntiCheat");
		for (int num4 = 0; num4 < array.Length; num4++)
		{
			if (array[num4])
			{
				antiCheatScript component4 = array[num4].GetComponent<antiCheatScript>();
				if (component4 && !component4.isUnlocked && component4.date_year == this.mS_.year && component4.date_month == this.mS_.month)
				{
					component4.isUnlocked = true;
					if (showMessage)
					{
						this.guiMain_.CreateTopNewsAntiCheat(component4.GetName());
					}
					if (!this.Get(64))
					{
						this.unlock[64] = true;
						this.guiMain_.UnlockBox(this.tS_.GetText(1207), true);
					}
				}
			}
		}
		array = GameObject.FindGameObjectsWithTag("Publisher");
		int num5 = 0;
		for (int num6 = 0; num6 < array.Length; num6++)
		{
			if (array[num6])
			{
				publisherScript component5 = array[num6].GetComponent<publisherScript>();
				if (component5 && component5.isUnlocked)
				{
					num5++;
				}
			}
		}
		if (num5 < this.mS_.anzKonkurrenten)
		{
			for (int num7 = 0; num7 < array.Length; num7++)
			{
				if (array[num7])
				{
					publisherScript component6 = array[num7].GetComponent<publisherScript>();
					if (component6 && !component6.isUnlocked && component6.date_year == this.mS_.year && component6.date_month == this.mS_.month && num5 < this.mS_.anzKonkurrenten)
					{
						num5++;
						component6.Unlock();
						if (showMessage && component6.isUnlocked)
						{
							this.guiMain_.CreateTopNewsNewPublisher(component6.GetName(), component6.GetLogo());
						}
					}
				}
			}
		}
		if (!this.Get(60) && this.mS_.year == 1984 && this.mS_.month == 12 && this.mS_.week == 5)
		{
			this.unlock[60] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1200), this.guiMain_.uiSprites[29]);
				this.guiMain_.UnlockBox(this.tS_.GetText(1196), true);
				return;
			}
		}
		if (!this.Get(61) && this.mS_.year == 1994 && this.mS_.month == 12 && this.mS_.week == 5)
		{
			this.unlock[61] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1201), this.guiMain_.uiSprites[29]);
				this.guiMain_.UnlockBox(this.tS_.GetText(1197), true);
				return;
			}
		}
		if (!this.Get(62) && this.mS_.year == 2004 && this.mS_.month == 12 && this.mS_.week == 5)
		{
			this.unlock[62] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1202), this.guiMain_.uiSprites[29]);
				this.guiMain_.UnlockBox(this.tS_.GetText(1198), true);
				return;
			}
		}
		if (!this.Get(63) && this.mS_.year == 2014 && this.mS_.month == 12 && this.mS_.week == 5)
		{
			this.unlock[63] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1203), this.guiMain_.uiSprites[29]);
				this.guiMain_.UnlockBox(this.tS_.GetText(1199), true);
				return;
			}
		}
		if (!this.Get(27) && this.mS_.year == 1986 && this.mS_.month == 4)
		{
			this.unlock[27] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(320), this.games_.gameTypSprites[2]);
				this.guiMain_.UnlockBox(this.tS_.GetText(881), true);
				return;
			}
		}
		if (!this.Get(56) && this.mS_.year == 1993 && this.mS_.month == 3)
		{
			this.unlock[56] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(521), this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[3]);
				this.guiMain_.UnlockBox(this.tS_.GetText(880), true);
				return;
			}
		}
		if (!this.Get(8) && this.mS_.year == 1993 && this.mS_.month == 11)
		{
			this.unlock[8] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(28), this.rdS_.roomData_SPRITE[10]);
				this.guiMain_.UnlockBox(this.tS_.GetText(885), true);
				return;
			}
		}
		if (!this.Get(59) && this.mS_.year == 2003 && this.mS_.month == 2)
		{
			this.unlock[59] = true;
			this.unlock[22] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1117), this.games_.gameTypSprites[7]);
				this.guiMain_.UnlockBox(this.tS_.GetText(1118), true);
				return;
			}
		}
		if (!this.Get(57) && this.mS_.year == 2005 && this.mS_.month == 2)
		{
			this.unlock[57] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(522), this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().sprites[4]);
				this.guiMain_.UnlockBox(this.tS_.GetText(879), true);
				return;
			}
		}
		if (!this.Get(21) && this.gF_.gameplayFeatures_RES_POINTS_LEFT.Length != 0 && this.gF_.gameplayFeatures_RES_POINTS_LEFT[23] == 0f)
		{
			this.unlock[21] = true;
			this.unlock[9] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(323), this.gF_.GetTypSprite(23));
				this.guiMain_.UnlockBox(this.tS_.GetText(882), true);
				return;
			}
		}
		if (!this.Get(25) && this.mS_.money - this.mS_.kredit >= 1000000L)
		{
			this.unlock[25] = true;
			if (showMessage)
			{
				this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(570), this.guiMain_.uiSprites[7]);
				this.guiMain_.UnlockBox(this.tS_.GetText(884), true);
				return;
			}
		}
		if (!this.Get(26) || !this.Get(28) || !this.Get(29) || !this.Get(30) || !this.Get(66) || !this.Get(67))
		{
			array = GameObject.FindGameObjectsWithTag("Game");
			int num8 = 0;
			if (array.Length != 0)
			{
				for (int num9 = 0; num9 < array.Length; num9++)
				{
					if (array[num9])
					{
						gameScript component7 = array[num9].GetComponent<gameScript>();
						if (component7 && !component7.inDevelopment && !component7.schublade && !component7.pubAngebot && !component7.auftragsspiel)
						{
							if (component7.developerID == this.mS_.myID || component7.IsMyAuftragsspiel())
							{
								num8++;
								if (num8 >= 5 && !this.Get(28))
								{
									this.unlock[28] = true;
									if (showMessage)
									{
										this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(361), this.guiMain_.uiSprites[17]);
										this.guiMain_.UnlockBox(this.tS_.GetText(892), true);
										return;
									}
								}
								if (num8 >= 10 && !this.Get(29))
								{
									this.unlock[29] = true;
									if (showMessage)
									{
										this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(362), this.guiMain_.uiSprites[17]);
										this.guiMain_.UnlockBox(this.tS_.GetText(893), true);
										return;
									}
								}
								if (num8 >= 15 && !this.Get(30))
								{
									this.unlock[30] = true;
									if (showMessage)
									{
										this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(363), this.guiMain_.uiSprites[17]);
										this.guiMain_.UnlockBox(this.tS_.GetText(894), true);
										return;
									}
								}
							}
							if (!this.Get(26) && component7.developerID == this.mS_.myID && component7.reviewTotal >= 70)
							{
								this.unlock[26] = true;
								if (showMessage)
								{
									this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(319), this.games_.gameTypSprites[1]);
									this.guiMain_.UnlockBox(this.tS_.GetText(896), true);
									return;
								}
							}
							if (!this.Get(66) && component7.developerID == this.mS_.myID && component7.sellsTotal >= 50000L)
							{
								this.unlock[66] = true;
								if (showMessage)
								{
									this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1535), this.games_.gameTypSprites[13]);
									this.guiMain_.UnlockBox(this.tS_.GetText(1538), true);
									return;
								}
							}
							if (!this.Get(67) && component7.developerID == this.mS_.myID && component7.isOnMarket)
							{
								this.unlock[67] = true;
								if (showMessage)
								{
									this.guiMain_.CreateTopNewsUnlock(this.tS_.GetText(1063), this.games_.gameTypSprites[14]);
									return;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0400278A RID: 10122
	private mainScript mS_;

	// Token: 0x0400278B RID: 10123
	private genres genres_;

	// Token: 0x0400278C RID: 10124
	private engineFeatures eF_;

	// Token: 0x0400278D RID: 10125
	private gameplayFeatures gF_;

	// Token: 0x0400278E RID: 10126
	private hardware hardware_;

	// Token: 0x0400278F RID: 10127
	private hardwareFeatures hardwareFeatures_;

	// Token: 0x04002790 RID: 10128
	private games games_;

	// Token: 0x04002791 RID: 10129
	private themes themes_;

	// Token: 0x04002792 RID: 10130
	private textScript tS_;

	// Token: 0x04002793 RID: 10131
	private roomDataScript rdS_;

	// Token: 0x04002794 RID: 10132
	private GUI_Main guiMain_;

	// Token: 0x04002795 RID: 10133
	public bool[] unlock;

	// Token: 0x04002796 RID: 10134
	public const int roomEntwicklung = 0;

	// Token: 0x04002797 RID: 10135
	public const int roomHardware = 1;

	// Token: 0x04002798 RID: 10136
	public const int roomForschung = 2;

	// Token: 0x04002799 RID: 10137
	public const int roomMotion = 8;

	// Token: 0x0400279A RID: 10138
	public const int roomServer = 9;

	// Token: 0x0400279B RID: 10139
	public const int roomAufenthalt = 13;

	// Token: 0x0400279C RID: 10140
	public const int roomWC = 14;

	// Token: 0x0400279D RID: 10141
	public const int gameTypClassic = 20;

	// Token: 0x0400279E RID: 10142
	public const int gameTypMMO = 21;

	// Token: 0x0400279F RID: 10143
	public const int gameTypF2P = 22;

	// Token: 0x040027A0 RID: 10144
	public const int gameLicence = 25;

	// Token: 0x040027A1 RID: 10145
	public const int gameNachfolger = 26;

	// Token: 0x040027A2 RID: 10146
	public const int gameRemaster = 27;

	// Token: 0x040027A3 RID: 10147
	public const int gamePlatform2 = 28;

	// Token: 0x040027A4 RID: 10148
	public const int gamePlatform3 = 29;

	// Token: 0x040027A5 RID: 10149
	public const int gamePlatform4 = 30;

	// Token: 0x040027A6 RID: 10150
	public const int gameCopyProtect = 31;

	// Token: 0x040027A7 RID: 10151
	public const int marketingInternet = 56;

	// Token: 0x040027A8 RID: 10152
	public const int marketingStreamer = 57;

	// Token: 0x040027A9 RID: 10153
	public const int createEngine = 58;

	// Token: 0x040027AA RID: 10154
	public const int onlineSells = 59;

	// Token: 0x040027AB RID: 10155
	public const int inventar2 = 60;

	// Token: 0x040027AC RID: 10156
	public const int inventar3 = 61;

	// Token: 0x040027AD RID: 10157
	public const int inventar4 = 62;

	// Token: 0x040027AE RID: 10158
	public const int inventar5 = 63;

	// Token: 0x040027AF RID: 10159
	public const int gameAntiCheat = 64;

	// Token: 0x040027B0 RID: 10160
	public const int handyGames = 65;

	// Token: 0x040027B1 RID: 10161
	public const int gameSpinoff = 66;

	// Token: 0x040027B2 RID: 10162
	public const int gamePorts = 67;
}
