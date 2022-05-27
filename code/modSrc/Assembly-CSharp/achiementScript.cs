using System;
using HeathenEngineering.SteamAPI;
using Steamworks;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class achiementScript : MonoBehaviour
{
	// Token: 0x06001C6C RID: 7276 RVA: 0x001184D8 File Offset: 0x001166D8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x001184E0 File Offset: 0x001166E0
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
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x0011858C File Offset: 0x0011678C
	public void SetAchivement(int i)
	{
		if (!this.mS_.achivementsDisabled[i] && !this.mS_.achivements[i])
		{
			this.mS_.achivements[i] = true;
			this.guiMain_.CreateTopNewsAchivement(i);
		}
		if (this.winnerAchievement[i].isAchieved)
		{
			return;
		}
		if (this.mS_.tS_ && this.steamSettings.client.user.Id.ToString() == "0")
		{
			this.guiMain_.MessageBox(this.mS_.tS_.GetText(1916), true);
		}
		switch (i)
		{
		case 0:
			SteamUserStats.SetAchievement("ACH_GENRE_ACTION");
			break;
		case 1:
			SteamUserStats.SetAchievement("ACH_GENRE_SKILL");
			break;
		case 2:
			SteamUserStats.SetAchievement("ACH_GENRE_PUZZLE");
			break;
		case 3:
			SteamUserStats.SetAchievement("ACH_GENRE_ADVENTURE");
			break;
		case 4:
			SteamUserStats.SetAchievement("ACH_GENRE_RPG");
			break;
		case 5:
			SteamUserStats.SetAchievement("ACH_GENRE_STRATEGY");
			break;
		case 6:
			SteamUserStats.SetAchievement("ACH_GENRE_PLATFORMER");
			break;
		case 7:
			SteamUserStats.SetAchievement("ACH_GENRE_SIMULATION");
			break;
		case 8:
			SteamUserStats.SetAchievement("ACH_GENRE_SPORT");
			break;
		case 9:
			SteamUserStats.SetAchievement("ACH_GENRE_ECONOMIC");
			break;
		case 10:
			SteamUserStats.SetAchievement("ACH_GENRE_FIGHTING");
			break;
		case 11:
			SteamUserStats.SetAchievement("ACH_GENRE_BUILDING");
			break;
		case 12:
			SteamUserStats.SetAchievement("ACH_GENRE_MOVIE");
			break;
		case 13:
			SteamUserStats.SetAchievement("ACH_GENRE_RTS");
			break;
		case 14:
			SteamUserStats.SetAchievement("ACH_GENRE_FPS");
			break;
		case 15:
			SteamUserStats.SetAchievement("ACH_GENRE_NOVEL");
			break;
		case 16:
			SteamUserStats.SetAchievement("ACH_GENRE_TPS");
			break;
		case 17:
			SteamUserStats.SetAchievement("ACH_GENRE_RACING");
			break;
		case 18:
			SteamUserStats.SetAchievement("ACH_GAME_RETRO");
			break;
		case 19:
			SteamUserStats.SetAchievement("ACH_GAME_MMO");
			break;
		case 20:
			SteamUserStats.SetAchievement("ACH_GAME_F2P");
			break;
		case 21:
			SteamUserStats.SetAchievement("ACH_GAME_ARCADE");
			break;
		case 22:
			SteamUserStats.SetAchievement("ACH_GAME_HANDY");
			break;
		case 23:
			SteamUserStats.SetAchievement("ACH_CREATE_ENGINE");
			break;
		case 24:
			SteamUserStats.SetAchievement("ACH_CREATE_KONSOLE");
			break;
		case 25:
			SteamUserStats.SetAchievement("ACH_CREATE_HANDHELD");
			break;
		case 26:
			SteamUserStats.SetAchievement("ACH_CREATE_CONTRACTGAME");
			break;
		case 27:
			SteamUserStats.SetAchievement("ACH_CREATE_BUNDLE");
			break;
		case 28:
			SteamUserStats.SetAchievement("ACH_CREATE_BUDGET");
			break;
		case 29:
			SteamUserStats.SetAchievement("ACH_CREATE_ADDONBUNDLE");
			break;
		case 30:
			SteamUserStats.SetAchievement("ACH_CREATE_SPINOFF");
			break;
		case 31:
			SteamUserStats.SetAchievement("ACH_CREATE_NACHFOLGER");
			break;
		case 32:
			SteamUserStats.SetAchievement("ACH_CREATE_PORT");
			break;
		case 33:
			SteamUserStats.SetAchievement("ACH_CREATE_REMASTER");
			break;
		case 34:
			SteamUserStats.SetAchievement("ACH_AWARD_TRENDSETTER");
			break;
		case 35:
			SteamUserStats.SetAchievement("ACH_AWARD_GOTY");
			break;
		case 36:
			SteamUserStats.SetAchievement("ACH_AWARD_DOTY");
			break;
		case 37:
			SteamUserStats.SetAchievement("ACH_AWARD_POTY");
			break;
		case 38:
			SteamUserStats.SetAchievement("ACH_AWARD_GRAFIK");
			break;
		case 39:
			SteamUserStats.SetAchievement("ACH_AWARD_SOUND");
			break;
		case 40:
			SteamUserStats.SetAchievement("ACH_AWARD_BADGAME");
			break;
		case 41:
			SteamUserStats.SetAchievement("ACH_GET_OVERHYPE");
			break;
		case 42:
			SteamUserStats.SetAchievement("ACH_PUBLISHERVERTRAG");
			break;
		case 43:
			SteamUserStats.SetAchievement("ACH_AUFTRAGSANSEHEN_100");
			break;
		case 44:
			SteamUserStats.SetAchievement("ACH_SELFPUBLISH");
			break;
		case 45:
			SteamUserStats.SetAchievement("ACH_YEAR2050");
			break;
		case 46:
			SteamUserStats.SetAchievement("ACH_NPCPUBLISH");
			break;
		case 47:
			SteamUserStats.SetAchievement("ACH_PUBLISHERRELATION_MAX");
			break;
		case 48:
			SteamUserStats.SetAchievement("ACH_SELL_1MIL");
			break;
		case 49:
			SteamUserStats.SetAchievement("ACH_SELL_10MIL");
			break;
		case 50:
			SteamUserStats.SetAchievement("ACH_SELL_50MIL");
			break;
		case 51:
			SteamUserStats.SetAchievement("ACH_REVIEW_80");
			break;
		case 52:
			SteamUserStats.SetAchievement("ACH_REVIEW_90");
			break;
		case 53:
			SteamUserStats.SetAchievement("ACH_REVIEW_100");
			break;
		case 54:
			SteamUserStats.SetAchievement("ACH_FANS_1MIL");
			break;
		case 55:
			SteamUserStats.SetAchievement("ACH_MULTIPLAYER");
			break;
		case 56:
			SteamUserStats.SetAchievement("ACH_MULTIPLAYER_4");
			break;
		case 57:
			SteamUserStats.SetAchievement("ACH_IP_MAX");
			break;
		case 58:
			SteamUserStats.SetAchievement("ACH_STUDIOLEVEL");
			break;
		case 59:
			SteamUserStats.SetAchievement("ACH_PERSONAL_100P");
			break;
		case 60:
			SteamUserStats.SetAchievement("ACH_PERSONAL_20");
			break;
		case 61:
			SteamUserStats.SetAchievement("ACH_PERSONAL_50");
			break;
		case 62:
			SteamUserStats.SetAchievement("ACH_PERSONAL_100");
			break;
		case 63:
			SteamUserStats.SetAchievement("ACH_MONEY_50MIL");
			break;
		case 64:
			SteamUserStats.SetAchievement("ACH_MONEY_500MIL");
			break;
		case 65:
			SteamUserStats.SetAchievement("ACH_MONEY_1MRD");
			break;
		case 66:
			SteamUserStats.SetAchievement("ACH_KONSOLE_10MIL");
			break;
		case 67:
			SteamUserStats.SetAchievement("ACH_KONSOLE_50MIL");
			break;
		case 68:
			SteamUserStats.SetAchievement("ACH_PERSONAL_LEGEND");
			break;
		}
		this.winnerAchievement[i].isAchieved = true;
		this.steamSettings.client.StoreStatsAndAchievements();
		Debug.Log("ACH: " + i.ToString());
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x00118BC6 File Offset: 0x00116DC6
	public void ResetAchivements()
	{
		SteamUserStats.ResetAllStats(true);
		this.steamSettings.client.StoreStatsAndAchievements();
	}

	// Token: 0x0400236E RID: 9070
	private mainScript mS_;

	// Token: 0x0400236F RID: 9071
	private GameObject main_;

	// Token: 0x04002370 RID: 9072
	private GUI_Main guiMain_;

	// Token: 0x04002371 RID: 9073
	private sfxScript sfx_;

	// Token: 0x04002372 RID: 9074
	private textScript tS_;

	// Token: 0x04002373 RID: 9075
	public SteamSettings steamSettings;

	// Token: 0x04002374 RID: 9076
	public AchievementObject[] winnerAchievement;
}
