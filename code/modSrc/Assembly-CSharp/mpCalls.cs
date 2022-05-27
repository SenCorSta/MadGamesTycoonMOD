using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000284 RID: 644
public class mpCalls : MonoBehaviour
{
	// Token: 0x06001927 RID: 6439 RVA: 0x000F97E0 File Offset: 0x000F79E0
	public player_mp FindPlayer(int id_)
	{
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			if (this.playersMP[i].playerID == id_)
			{
				return this.playersMP[i];
			}
		}
		return null;
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x000F9828 File Offset: 0x000F7A28
	public string GetCompanyName(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return "---";
		}
		if (!player_mp.myPubScript_)
		{
			GameObject gameObject = GameObject.Find("PUB_" + id_.ToString());
			if (gameObject)
			{
				player_mp.myPubScript_ = gameObject.GetComponent<publisherScript>();
			}
		}
		if (player_mp.myPubScript_)
		{
			return player_mp.myPubScript_.GetName();
		}
		return "";
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x000F989C File Offset: 0x000F7A9C
	public string GetPlayerName(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return "---";
		}
		return player_mp.playerName;
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x000F98C0 File Offset: 0x000F7AC0
	public bool GetReady(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		return player_mp != null && player_mp.ready;
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x000F98E0 File Offset: 0x000F7AE0
	public long GetMoney(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return 0L;
		}
		return player_mp.money;
	}

	// Token: 0x0600192C RID: 6444 RVA: 0x000F9904 File Offset: 0x000F7B04
	public int GetFans(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return 0;
		}
		return player_mp.fans;
	}

	// Token: 0x0600192D RID: 6445 RVA: 0x000F9924 File Offset: 0x000F7B24
	public int GetLogo(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return 0;
		}
		if (!player_mp.myPubScript_)
		{
			GameObject gameObject = GameObject.Find("PUB_" + id_.ToString());
			if (gameObject)
			{
				player_mp.myPubScript_ = gameObject.GetComponent<publisherScript>();
			}
		}
		if (player_mp.myPubScript_)
		{
			return player_mp.myPubScript_.logoID;
		}
		return 0;
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x000F9990 File Offset: 0x000F7B90
	public int GetCountry(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return 0;
		}
		if (!player_mp.myPubScript_)
		{
			GameObject gameObject = GameObject.Find("PUB_" + id_.ToString());
			if (gameObject)
			{
				player_mp.myPubScript_ = gameObject.GetComponent<publisherScript>();
			}
		}
		if (player_mp.myPubScript_)
		{
			return player_mp.myPubScript_.country;
		}
		return 0;
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x000F99FC File Offset: 0x000F7BFC
	public bool GetPause(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		return player_mp != null && player_mp.playerPause;
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x000F9A1C File Offset: 0x000F7C1C
	public void SetPause(bool b)
	{
		player_mp player_mp = this.FindPlayer(this.mS_.myID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.playerPause = b;
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x000F9A48 File Offset: 0x000F7C48
	public bool GetAllPlayersReady()
	{
		for (int i = 1; i < this.playersMP.Count; i++)
		{
			if (!this.playersMP[i].playerReady)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x000F9A84 File Offset: 0x000F7C84
	public void SetPlayersUnready()
	{
		for (int i = 1; i < this.playersMP.Count; i++)
		{
			this.playersMP[i].playerReady = false;
		}
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x000F9AB9 File Offset: 0x000F7CB9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x000F9AC4 File Offset: 0x000F7CC4
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
		if (!this.arbeitsmarkt_)
		{
			this.arbeitsmarkt_ = this.main_.GetComponent<arbeitsmarkt>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.save_)
		{
			this.save_ = this.main_.GetComponent<savegameScript>();
		}
		if (!this.mpMain_)
		{
			this.mpMain_ = this.guiMain_.uiObjects[201].GetComponent<mpMain>();
		}
		if (!this.mapScript_)
		{
			this.mapScript_ = this.main_.GetComponent<mapScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeatures_)
		{
			this.hardwareFeatures_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
		if (!this.publisher_)
		{
			this.publisher_ = this.main_.GetComponent<publisher>();
		}
		if (!this.copyProtect_)
		{
			this.copyProtect_ = this.main_.GetComponent<copyProtect>();
		}
		if (!this.antiCheat_)
		{
			this.antiCheat_ = this.main_.GetComponent<anitCheat>();
		}
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x000F9D78 File Offset: 0x000F7F78
	private void Update()
	{
		this.FindScripts();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		if (this.isClient && this.mS_.myID != -1 && !NetworkClient.isConnected)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1039), false);
			this.mpMain_.StopNetwork();
			this.mS_.multiplayer = false;
			this.mS_.myID = -1;
			this.playersMP.Clear();
		}
		bool flag = this.isServer;
		this.Send1Sec();
		this.Send10Sec();
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x000F9E14 File Offset: 0x000F8014
	private void UpdateTimeout()
	{
		for (int i = 1; i < this.playersMP.Count; i++)
		{
			this.playersMP[i].timeout += Time.deltaTime;
			if (this.playersMP[i].timeout > 30f)
			{
				this.RemovePlayer(this.playersMP[i].playerID);
				return;
			}
		}
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x000F9E84 File Offset: 0x000F8084
	private void Send1Sec()
	{
		if (this.mS_.myID != -1)
		{
			this.timer += Time.deltaTime;
			if (this.timer < 1f)
			{
				return;
			}
			this.timer = 0f;
			player_mp player_mp = this.FindPlayer(this.mS_.myID);
			if (player_mp == null)
			{
				return;
			}
			player_mp.money = this.mS_.money;
			player_mp.fans = this.genres_.GetAmountFans();
			if (this.isServer)
			{
				this.SERVER_Send_Money();
				this.SERVER_Send_AutoPause();
			}
			if (this.isClient)
			{
				this.CLIENT_Send_Money();
				if (this.mS_.settings_autoPauseForMultiplayer)
				{
					if (this.guiMain_.menuOpen)
					{
						this.CLIENT_Send_Command(2);
						return;
					}
					this.CLIENT_Send_Command(3);
				}
			}
		}
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x000F9F50 File Offset: 0x000F8150
	private void Send10Sec()
	{
		if (this.mS_.myID != -1)
		{
			this.timer10Secs += Time.deltaTime;
			if (this.timer10Secs < 10f)
			{
				return;
			}
			this.timer10Secs = 0f;
			if (this.isServer)
			{
				this.SERVER_Send_Forschung(this.mS_.myID);
			}
			if (this.isClient)
			{
				this.CLIENT_Send_Forschung();
			}
		}
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x000F9FC0 File Offset: 0x000F81C0
	public bool AutoPause()
	{
		if (this.mS_ && this.mS_.multiplayer && this.mS_.settings_autoPauseForMultiplayer)
		{
			for (int i = 0; i < this.playersMP.Count; i++)
			{
				if (this.playersMP[i].playerPause)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x000FA020 File Offset: 0x000F8220
	public int AddPlayer(mpPlayer mpPlayer_)
	{
		Debug.Log("AddPlayer()");
		if (!this.mS_.mpLobbyOpen)
		{
			mpPlayer_.connectionToClient.Disconnect();
			return -1;
		}
		int num = 100000 + this.playersMP.Count;
		this.playersMP.Add(new player_mp(num));
		publisherScript myPubScript_ = this.mS_.CreatePlayerPublisher(num);
		player_mp player_mp = this.FindPlayer(num);
		player_mp.myPubScript_ = myPubScript_;
		Debug.Log("PLAYER-ID: " + num);
		if (this.playersMP.Count <= 1)
		{
			this.mS_.myID = num;
			player_mp.playerID = this.mS_.myID;
			player_mp.playerName = this.mS_.playerName;
			player_mp.playerReady = true;
			this.mS_.SetCompanyName(this.mpMain_.uiObjects[12].GetComponent<InputField>().text);
			this.mS_.SetCompanyLogoID(0);
			this.mS_.SetCountryID(0);
			this.mS_.SetFanGenreID(0);
		}
		else
		{
			this.SERVER_Send_AddPlayer();
		}
		this.SERVER_Send_ID(num, mpPlayer_);
		this.SERVER_Send_Difficulty();
		this.SERVER_Send_Startjahr();
		this.SERVER_Send_Office();
		this.SERVER_Send_Spielgeschwindigkeit();
		this.SERVER_Send_Genres(num, mpPlayer_);
		if (this.mS_.settings_autoPauseForMultiplayer)
		{
			this.SERVER_Send_Command(7);
		}
		else
		{
			this.SERVER_Send_Command(6);
		}
		if (this.mS_.settings_RandomEventsOff)
		{
			this.SERVER_Send_Command(9);
		}
		else
		{
			this.SERVER_Send_Command(8);
		}
		if (this.mS_.settings_RandomReviews)
		{
			this.SERVER_Send_Command(11);
		}
		else
		{
			this.SERVER_Send_Command(10);
		}
		if (this.mpMain_.uiObjects[42].GetComponent<Toggle>().isOn)
		{
			this.SERVER_Send_Command(13);
		}
		else
		{
			this.SERVER_Send_Command(12);
		}
		if (this.mpMain_.uiObjects[43].GetComponent<Toggle>().isOn)
		{
			this.SERVER_Send_Command(15);
		}
		else
		{
			this.SERVER_Send_Command(14);
		}
		if (this.mpMain_.uiObjects[45].GetComponent<Toggle>().isOn)
		{
			this.SERVER_Send_Command(17);
		}
		else
		{
			this.SERVER_Send_Command(16);
		}
		if (this.mpMain_.uiObjects[52].GetComponent<Toggle>().isOn)
		{
			this.SERVER_Send_Command(19);
		}
		else
		{
			this.SERVER_Send_Command(18);
		}
		if (this.mpMain_.uiObjects[53].GetComponent<Toggle>().isOn)
		{
			this.SERVER_Send_Command(21);
		}
		else
		{
			this.SERVER_Send_Command(20);
		}
		return num;
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x000FA298 File Offset: 0x000F8498
	public void RemovePlayer(int playerID_)
	{
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			GameObject gameObject = GameObject.Find("PUB_" + playerID_.ToString());
			if (gameObject)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			if (this.playersMP[i].playerID == playerID_)
			{
				this.playersMP.RemoveAt(i);
				break;
			}
		}
		this.SERVER_Send_PlayerLeave(playerID_);
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x000FA30C File Offset: 0x000F850C
	public void SetupClient()
	{
		this.UnregisterServer();
		NetworkClient.RegisterHandler<mpCalls.s_PlayerInfos>(new Action<NetworkConnection, mpCalls.s_PlayerInfos>(this.SERVER_Get_PlayerInfos), true);
		NetworkClient.RegisterHandler<mpCalls.s_PlayerID>(new Action<NetworkConnection, mpCalls.s_PlayerID>(this.SERVER_Get_ID), true);
		NetworkClient.RegisterHandler<mpCalls.s_Command>(new Action<NetworkConnection, mpCalls.s_Command>(this.SERVER_Get_Command), true);
		NetworkClient.RegisterHandler<mpCalls.s_Save>(new Action<NetworkConnection, mpCalls.s_Save>(this.SERVER_Get_Save), true);
		NetworkClient.RegisterHandler<mpCalls.s_Load>(new Action<NetworkConnection, mpCalls.s_Load>(this.SERVER_Get_Load), true);
		NetworkClient.RegisterHandler<mpCalls.s_GameSpeed>(new Action<NetworkConnection, mpCalls.s_GameSpeed>(this.SERVER_Get_GameSpeed), true);
		NetworkClient.RegisterHandler<mpCalls.s_CreateArbeitsmarkt>(new Action<NetworkConnection, mpCalls.s_CreateArbeitsmarkt>(this.SERVER_Get_CreateArbeitsmarkt), true);
		NetworkClient.RegisterHandler<mpCalls.s_DeleteArbeitsmarkt>(new Action<NetworkConnection, mpCalls.s_DeleteArbeitsmarkt>(this.SERVER_Get_DeleteArbeitsmarkt), true);
		NetworkClient.RegisterHandler<mpCalls.s_Trend>(new Action<NetworkConnection, mpCalls.s_Trend>(this.SERVER_Get_Trend), true);
		NetworkClient.RegisterHandler<mpCalls.s_Lizenz>(new Action<NetworkConnection, mpCalls.s_Lizenz>(this.SERVER_Get_Lizenz), true);
		NetworkClient.RegisterHandler<mpCalls.s_Game>(new Action<NetworkConnection, mpCalls.s_Game>(this.SERVER_Get_Game), true);
		NetworkClient.RegisterHandler<mpCalls.s_Publisher>(new Action<NetworkConnection, mpCalls.s_Publisher>(this.SERVER_Get_Publisher), true);
		NetworkClient.RegisterHandler<mpCalls.s_GameData>(new Action<NetworkConnection, mpCalls.s_GameData>(this.SERVER_Get_GameData), true);
		NetworkClient.RegisterHandler<mpCalls.s_Money>(new Action<NetworkConnection, mpCalls.s_Money>(this.SERVER_Get_Money), true);
		NetworkClient.RegisterHandler<mpCalls.s_Chat>(new Action<NetworkConnection, mpCalls.s_Chat>(this.SERVER_Get_Chat), true);
		NetworkClient.RegisterHandler<mpCalls.s_Engine>(new Action<NetworkConnection, mpCalls.s_Engine>(this.SERVER_Get_Engine), true);
		NetworkClient.RegisterHandler<mpCalls.s_Payment>(new Action<NetworkConnection, mpCalls.s_Payment>(this.SERVER_Get_Payment), true);
		NetworkClient.RegisterHandler<mpCalls.s_Awards>(new Action<NetworkConnection, mpCalls.s_Awards>(this.SERVER_Get_Awards), true);
		NetworkClient.RegisterHandler<mpCalls.s_EngineAbrechnung>(new Action<NetworkConnection, mpCalls.s_EngineAbrechnung>(this.SERVER_Get_EngineAbrechnung), true);
		NetworkClient.RegisterHandler<mpCalls.s_GlobalEvent>(new Action<NetworkConnection, mpCalls.s_GlobalEvent>(this.SERVER_Get_GlobalEvent), true);
		NetworkClient.RegisterHandler<mpCalls.s_Difficulty>(new Action<NetworkConnection, mpCalls.s_Difficulty>(this.SERVER_Get_Difficulty), true);
		NetworkClient.RegisterHandler<mpCalls.s_Startjahr>(new Action<NetworkConnection, mpCalls.s_Startjahr>(this.SERVER_Get_Startjahr), true);
		NetworkClient.RegisterHandler<mpCalls.s_Office>(new Action<NetworkConnection, mpCalls.s_Office>(this.SERVER_Get_Office), true);
		NetworkClient.RegisterHandler<mpCalls.s_Spielgeschwindigkeit>(new Action<NetworkConnection, mpCalls.s_Spielgeschwindigkeit>(this.SERVER_Get_Spielgeschwindigkeit), true);
		NetworkClient.RegisterHandler<mpCalls.s_AutoPause>(new Action<NetworkConnection, mpCalls.s_AutoPause>(this.SERVER_Get_AutoPause), true);
		NetworkClient.RegisterHandler<mpCalls.s_Map>(new Action<NetworkConnection, mpCalls.s_Map>(this.SERVER_Get_Map), true);
		NetworkClient.RegisterHandler<mpCalls.s_Object>(new Action<NetworkConnection, mpCalls.s_Object>(this.SERVER_Get_Object), true);
		NetworkClient.RegisterHandler<mpCalls.s_ObjectDelete>(new Action<NetworkConnection, mpCalls.s_ObjectDelete>(this.SERVER_Get_ObjectDelete), true);
		NetworkClient.RegisterHandler<mpCalls.s_PlatformData>(new Action<NetworkConnection, mpCalls.s_PlatformData>(this.SERVER_Get_PlatformData), true);
		NetworkClient.RegisterHandler<mpCalls.s_Help>(new Action<NetworkConnection, mpCalls.s_Help>(this.SERVER_Get_Help), true);
		NetworkClient.RegisterHandler<mpCalls.s_GenreDesign>(new Action<NetworkConnection, mpCalls.s_GenreDesign>(this.SERVER_Get_GenreDesign), true);
		NetworkClient.RegisterHandler<mpCalls.s_GenreCombination>(new Action<NetworkConnection, mpCalls.s_GenreCombination>(this.SERVER_Get_GenreCombination), true);
		NetworkClient.RegisterHandler<mpCalls.s_GenreBeliebtheit>(new Action<NetworkConnection, mpCalls.s_GenreBeliebtheit>(this.SERVER_Get_GenreBeliebtheit), true);
		NetworkClient.RegisterHandler<mpCalls.s_PlayerLeave>(new Action<NetworkConnection, mpCalls.s_PlayerLeave>(this.SERVER_Get_PlayerLeave), true);
		NetworkClient.RegisterHandler<mpCalls.s_AddPlayer>(new Action<NetworkConnection, mpCalls.s_AddPlayer>(this.SERVER_Get_AddPlayer), true);
		NetworkClient.RegisterHandler<mpCalls.s_Platform>(new Action<NetworkConnection, mpCalls.s_Platform>(this.SERVER_Get_Platform), true);
		NetworkClient.RegisterHandler<mpCalls.s_exklusivKonsolenSells>(new Action<NetworkConnection, mpCalls.s_exklusivKonsolenSells>(this.SERVER_Get_ExklusivKonsolenSells), true);
		NetworkClient.RegisterHandler<mpCalls.s_CopyProtect>(new Action<NetworkConnection, mpCalls.s_CopyProtect>(this.SERVER_Get_CopyProtect), true);
		NetworkClient.RegisterHandler<mpCalls.s_AntiCheat>(new Action<NetworkConnection, mpCalls.s_AntiCheat>(this.SERVER_Get_AntiCheat), true);
		NetworkClient.RegisterHandler<mpCalls.s_Hardware>(new Action<NetworkConnection, mpCalls.s_Hardware>(this.SERVER_Get_Hardware), true);
		NetworkClient.RegisterHandler<mpCalls.s_HardwareFeatures>(new Action<NetworkConnection, mpCalls.s_HardwareFeatures>(this.SERVER_Get_HardwareFeatures), true);
		NetworkClient.RegisterHandler<mpCalls.s_EngineFeatures>(new Action<NetworkConnection, mpCalls.s_EngineFeatures>(this.SERVER_Get_EngineFeatures), true);
		NetworkClient.RegisterHandler<mpCalls.s_GameplayFeatures>(new Action<NetworkConnection, mpCalls.s_GameplayFeatures>(this.SERVER_Get_GameplayFeatures), true);
		NetworkClient.RegisterHandler<mpCalls.s_NpcEngine>(new Action<NetworkConnection, mpCalls.s_NpcEngine>(this.SERVER_Get_NpcEngine), true);
		NetworkClient.RegisterHandler<mpCalls.s_Genres>(new Action<NetworkConnection, mpCalls.s_Genres>(this.SERVER_Get_Genres), true);
		NetworkClient.RegisterHandler<mpCalls.s_Forschung>(new Action<NetworkConnection, mpCalls.s_Forschung>(this.SERVER_Get_Forschung), true);
		NetworkClient.RegisterHandler<mpCalls.s_Firmenwert>(new Action<NetworkConnection, mpCalls.s_Firmenwert>(this.SERVER_Get_Firmenwert), true);
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x000FA670 File Offset: 0x000F8870
	public void UnregisterClient()
	{
		NetworkClient.UnregisterHandler<mpCalls.s_PlayerInfos>();
		NetworkClient.UnregisterHandler<mpCalls.s_PlayerID>();
		NetworkClient.UnregisterHandler<mpCalls.s_Command>();
		NetworkClient.UnregisterHandler<mpCalls.s_Save>();
		NetworkClient.UnregisterHandler<mpCalls.s_Load>();
		NetworkClient.UnregisterHandler<mpCalls.s_GameSpeed>();
		NetworkClient.UnregisterHandler<mpCalls.s_CreateArbeitsmarkt>();
		NetworkClient.UnregisterHandler<mpCalls.s_DeleteArbeitsmarkt>();
		NetworkClient.UnregisterHandler<mpCalls.s_Trend>();
		NetworkClient.UnregisterHandler<mpCalls.s_Lizenz>();
		NetworkClient.UnregisterHandler<mpCalls.s_Game>();
		NetworkClient.UnregisterHandler<mpCalls.s_Publisher>();
		NetworkClient.UnregisterHandler<mpCalls.s_GameData>();
		NetworkClient.UnregisterHandler<mpCalls.s_Money>();
		NetworkClient.UnregisterHandler<mpCalls.s_Chat>();
		NetworkClient.UnregisterHandler<mpCalls.s_Engine>();
		NetworkClient.UnregisterHandler<mpCalls.s_Payment>();
		NetworkClient.UnregisterHandler<mpCalls.s_Awards>();
		NetworkClient.UnregisterHandler<mpCalls.s_EngineAbrechnung>();
		NetworkClient.UnregisterHandler<mpCalls.s_Difficulty>();
		NetworkClient.UnregisterHandler<mpCalls.s_Startjahr>();
		NetworkClient.UnregisterHandler<mpCalls.s_Office>();
		NetworkClient.UnregisterHandler<mpCalls.s_Spielgeschwindigkeit>();
		NetworkClient.UnregisterHandler<mpCalls.s_AutoPause>();
		NetworkClient.UnregisterHandler<mpCalls.s_Map>();
		NetworkClient.UnregisterHandler<mpCalls.s_Object>();
		NetworkClient.UnregisterHandler<mpCalls.s_ObjectDelete>();
		NetworkClient.UnregisterHandler<mpCalls.s_PlatformData>();
		NetworkClient.UnregisterHandler<mpCalls.s_Help>();
		NetworkClient.UnregisterHandler<mpCalls.s_GenreDesign>();
		NetworkClient.UnregisterHandler<mpCalls.s_GenreCombination>();
		NetworkClient.UnregisterHandler<mpCalls.s_GenreBeliebtheit>();
		NetworkClient.UnregisterHandler<mpCalls.s_PlayerLeave>();
		NetworkClient.UnregisterHandler<mpCalls.s_AddPlayer>();
		NetworkClient.UnregisterHandler<mpCalls.s_Platform>();
		NetworkClient.UnregisterHandler<mpCalls.s_exklusivKonsolenSells>();
		NetworkClient.UnregisterHandler<mpCalls.s_CopyProtect>();
		NetworkClient.UnregisterHandler<mpCalls.s_AntiCheat>();
		NetworkClient.UnregisterHandler<mpCalls.s_Hardware>();
		NetworkClient.UnregisterHandler<mpCalls.s_HardwareFeatures>();
		NetworkClient.UnregisterHandler<mpCalls.s_EngineFeatures>();
		NetworkClient.UnregisterHandler<mpCalls.s_GameplayFeatures>();
		NetworkClient.UnregisterHandler<mpCalls.s_NpcEngine>();
		NetworkClient.UnregisterHandler<mpCalls.s_Genres>();
		NetworkClient.UnregisterHandler<mpCalls.s_Forschung>();
		NetworkClient.UnregisterHandler<mpCalls.s_Firmenwert>();
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x000FA794 File Offset: 0x000F8994
	public void SetupServer()
	{
		this.UnregisterClient();
		NetworkServer.RegisterHandler<mpCalls.c_PlayerInfos>(new Action<NetworkConnection, mpCalls.c_PlayerInfos>(this.CLIENT_Get_PlayerInfos), true);
		NetworkServer.RegisterHandler<mpCalls.c_Command>(new Action<NetworkConnection, mpCalls.c_Command>(this.CLIENT_Get_Command), true);
		NetworkServer.RegisterHandler<mpCalls.c_DeleteArbeitsmarkt>(new Action<NetworkConnection, mpCalls.c_DeleteArbeitsmarkt>(this.CLIENT_Get_DeleteArbeitsmarkt), true);
		NetworkServer.RegisterHandler<mpCalls.c_BuyLizenz>(new Action<NetworkConnection, mpCalls.c_BuyLizenz>(this.CLIENT_Get_BuyLizenz), true);
		NetworkServer.RegisterHandler<mpCalls.c_Game>(new Action<NetworkConnection, mpCalls.c_Game>(this.CLIENT_Get_Game), true);
		NetworkServer.RegisterHandler<mpCalls.c_GameData>(new Action<NetworkConnection, mpCalls.c_GameData>(this.CLIENT_Get_GameData), true);
		NetworkServer.RegisterHandler<mpCalls.c_Money>(new Action<NetworkConnection, mpCalls.c_Money>(this.CLIENT_Get_Money), true);
		NetworkServer.RegisterHandler<mpCalls.c_Chat>(new Action<NetworkConnection, mpCalls.c_Chat>(this.CLIENT_Get_Chat), true);
		NetworkServer.RegisterHandler<mpCalls.c_Engine>(new Action<NetworkConnection, mpCalls.c_Engine>(this.CLIENT_Get_Engine), true);
		NetworkServer.RegisterHandler<mpCalls.c_Payment>(new Action<NetworkConnection, mpCalls.c_Payment>(this.CLIENT_Get_Payment), true);
		NetworkServer.RegisterHandler<mpCalls.c_Trend>(new Action<NetworkConnection, mpCalls.c_Trend>(this.CLIENT_Get_Trend), true);
		NetworkServer.RegisterHandler<mpCalls.c_Map>(new Action<NetworkConnection, mpCalls.c_Map>(this.CLIENT_Get_Map), true);
		NetworkServer.RegisterHandler<mpCalls.c_Object>(new Action<NetworkConnection, mpCalls.c_Object>(this.CLIENT_Get_Object), true);
		NetworkServer.RegisterHandler<mpCalls.c_ObjectDelete>(new Action<NetworkConnection, mpCalls.c_ObjectDelete>(this.CLIENT_Get_ObjectDelete), true);
		NetworkServer.RegisterHandler<mpCalls.c_Help>(new Action<NetworkConnection, mpCalls.c_Help>(this.CLIENT_Get_Help), true);
		NetworkServer.RegisterHandler<mpCalls.c_Platform>(new Action<NetworkConnection, mpCalls.c_Platform>(this.CLIENT_Get_Platform), true);
		NetworkServer.RegisterHandler<mpCalls.c_exklusivKonsolenSells>(new Action<NetworkConnection, mpCalls.c_exklusivKonsolenSells>(this.CLIENT_Get_ExklusivKonsolenSells), true);
		NetworkServer.RegisterHandler<mpCalls.c_Forschung>(new Action<NetworkConnection, mpCalls.c_Forschung>(this.CLIENT_Get_Forschung), true);
		NetworkServer.RegisterHandler<mpCalls.c_Publisher>(new Action<NetworkConnection, mpCalls.c_Publisher>(this.CLIENT_Get_Publisher), true);
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000FA900 File Offset: 0x000F8B00
	public void UnregisterServer()
	{
		NetworkServer.UnregisterHandler<mpCalls.c_PlayerInfos>();
		NetworkServer.UnregisterHandler<mpCalls.c_Command>();
		NetworkServer.UnregisterHandler<mpCalls.c_DeleteArbeitsmarkt>();
		NetworkServer.UnregisterHandler<mpCalls.c_BuyLizenz>();
		NetworkServer.UnregisterHandler<mpCalls.c_Game>();
		NetworkServer.UnregisterHandler<mpCalls.c_GameData>();
		NetworkServer.UnregisterHandler<mpCalls.c_Money>();
		NetworkServer.UnregisterHandler<mpCalls.c_Chat>();
		NetworkServer.UnregisterHandler<mpCalls.c_Engine>();
		NetworkServer.UnregisterHandler<mpCalls.c_Payment>();
		NetworkServer.UnregisterHandler<mpCalls.c_Trend>();
		NetworkServer.UnregisterHandler<mpCalls.c_Map>();
		NetworkServer.UnregisterHandler<mpCalls.c_Object>();
		NetworkServer.UnregisterHandler<mpCalls.c_ObjectDelete>();
		NetworkServer.UnregisterHandler<mpCalls.c_Help>();
		NetworkServer.UnregisterHandler<mpCalls.c_Platform>();
		NetworkServer.UnregisterHandler<mpCalls.c_exklusivKonsolenSells>();
		NetworkServer.UnregisterHandler<mpCalls.c_Forschung>();
		NetworkServer.UnregisterHandler<mpCalls.c_Publisher>();
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x000FA96C File Offset: 0x000F8B6C
	public void CLIENT_Send_ObjectDelete(int id_)
	{
		Debug.Log("CLIENT_Send_Object_Delete()");
		NetworkClient.Send<mpCalls.c_ObjectDelete>(new mpCalls.c_ObjectDelete
		{
			playerID = this.mS_.myID,
			objectID = id_
		}, 0);
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x000FA9AC File Offset: 0x000F8BAC
	public void CLIENT_Get_ObjectDelete(NetworkConnection conn, mpCalls.c_ObjectDelete msg)
	{
		Debug.Log("CLIENT_Get_ObjectDelete()");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		for (int i = 0; i < player_mp.objects.Count; i++)
		{
			if (player_mp.objects[i].id == msg.objectID)
			{
				player_mp.objects.RemoveAt(i);
				break;
			}
		}
		mpCalls.s_ObjectDelete s_ObjectDelete = default(mpCalls.s_ObjectDelete);
		s_ObjectDelete.playerID = msg.playerID;
		s_ObjectDelete.objectID = msg.objectID;
		NetworkServer.SendToAll<mpCalls.c_ObjectDelete>(msg, 0, false);
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x000FAA3C File Offset: 0x000F8C3C
	public void CLIENT_Send_Object(int id_, int typ_, float x_, float y_, float rot_)
	{
		Debug.Log("CLIENT_Send_Object()");
		NetworkClient.Send<mpCalls.c_Object>(new mpCalls.c_Object
		{
			playerID = this.mS_.myID,
			objectID = id_,
			typ = typ_,
			x = x_,
			y = y_,
			rot = rot_
		}, 0);
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x000FAAA0 File Offset: 0x000F8CA0
	public void CLIENT_Get_Object(NetworkConnection conn, mpCalls.c_Object msg)
	{
		Debug.Log("CLIENT_Get_Object()");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.objects.Add(new object_mp(msg.objectID, msg.typ, msg.x, msg.y, msg.rot));
		mpCalls.s_Object s_Object = default(mpCalls.s_Object);
		s_Object.playerID = msg.playerID;
		s_Object.objectID = msg.objectID;
		s_Object.typ = msg.typ;
		s_Object.x = msg.x;
		s_Object.y = msg.y;
		s_Object.rot = msg.rot;
		NetworkServer.SendToAll<mpCalls.c_Object>(msg, 0, false);
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x000FAB54 File Offset: 0x000F8D54
	public void CLIENT_Send_Map(int posx, int posy)
	{
		Debug.Log("CLIENT_Send_Map()");
		int typ = 0;
		if (this.mapScript_.mapRoomScript[posx, posy])
		{
			typ = this.mapScript_.mapRoomScript[posx, posy].typ;
		}
		NetworkClient.Send<mpCalls.c_Map>(new mpCalls.c_Map
		{
			playerID = this.mS_.myID,
			x = (byte)posx,
			y = (byte)posy,
			id = this.mapScript_.mapRoomID[posx, posy],
			typ = typ,
			door = this.mapScript_.mapDoors[posx, posy],
			window = (byte)this.mapScript_.mapWindows[posx, posy]
		}, 0);
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x000FAC28 File Offset: 0x000F8E28
	public void CLIENT_Get_Map(NetworkConnection conn, mpCalls.c_Map msg)
	{
		Debug.Log("CLIENT_Get_Map()");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.mapRoomID[(int)msg.x, (int)msg.y] = msg.id;
		player_mp.mapRoomTyp[(int)msg.x, (int)msg.y] = msg.typ;
		player_mp.mapDoors[(int)msg.x, (int)msg.y] = msg.door;
		player_mp.mapWindows[(int)msg.x, (int)msg.y] = (int)msg.window;
		mpCalls.s_Map s_Map = default(mpCalls.s_Map);
		s_Map.playerID = msg.playerID;
		s_Map.x = msg.x;
		s_Map.y = msg.y;
		s_Map.id = msg.id;
		s_Map.typ = msg.typ;
		s_Map.door = msg.door;
		s_Map.window = msg.window;
		NetworkServer.SendToAll<mpCalls.c_Map>(msg, 0, false);
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x000FAD30 File Offset: 0x000F8F30
	public void CLIENT_Send_Trend()
	{
		Debug.Log("CLIENT_Send_Trend()");
		NetworkClient.Send<mpCalls.c_Trend>(new mpCalls.c_Trend
		{
			trendWeeks = this.mS_.trendWeeks,
			trendTheme = this.mS_.trendTheme,
			trendAntiTheme = this.mS_.trendAntiTheme,
			trendGenre = this.mS_.trendGenre,
			trendAntiGenre = this.mS_.trendAntiGenre
		}, 0);
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x000FADB0 File Offset: 0x000F8FB0
	public void CLIENT_Get_Trend(NetworkConnection conn, mpCalls.c_Trend msg)
	{
		Debug.Log("CLIENT_Get_Trend()");
		this.mS_.trendWeeks = msg.trendWeeks;
		this.mS_.trendTheme = msg.trendTheme;
		this.mS_.trendAntiTheme = msg.trendAntiTheme;
		this.mS_.trendGenre = msg.trendGenre;
		this.mS_.trendAntiGenre = msg.trendAntiGenre;
		this.mS_.ShowTrendNews();
		this.SERVER_Send_Trend();
	}

	// Token: 0x06001948 RID: 6472 RVA: 0x000FAE30 File Offset: 0x000F9030
	public void CLIENT_Send_Publisher(publisherScript script_)
	{
		Debug.Log("CLIENT_Send_Publisher()");
		if (script_)
		{
			NetworkClient.Send<mpCalls.c_Publisher>(new mpCalls.c_Publisher
			{
				myID = script_.myID,
				isUnlocked = script_.isUnlocked,
				name_EN = script_.name_EN,
				name_GE = script_.name_GE,
				name_TU = script_.name_TU,
				name_CH = script_.name_CH,
				name_FR = script_.name_FR,
				name_JA = script_.name_JA,
				date_year = script_.date_year,
				date_month = script_.date_month,
				stars = script_.stars,
				logoID = script_.logoID,
				developer = script_.developer,
				publisher = script_.publisher,
				onlyMobile = script_.onlyMobile,
				share = script_.share,
				fanGenre = script_.fanGenre,
				firmenwert = script_.firmenwert,
				notForSale = script_.notForSale,
				lockToBuy = script_.lockToBuy,
				isPlayer = script_.isPlayer,
				ownerID = script_.ownerID,
				country = script_.country,
				awards = (int[])script_.awards.Clone()
			}, 0);
			return;
		}
		Debug.Log("ERROR: CLIENT_Send_Publisher() -> Missing PublisherScript");
	}

	// Token: 0x06001949 RID: 6473 RVA: 0x000FAFB0 File Offset: 0x000F91B0
	public void CLIENT_Get_Publisher(NetworkConnection conn, mpCalls.c_Publisher msg)
	{
		if (this.save_.loadingSavegame)
		{
			return;
		}
		Debug.Log("CLIENT_Get_Publisher() " + UnityEngine.Random.Range(0, 10000).ToString());
		GameObject gameObject = GameObject.Find("PUB_" + msg.myID.ToString());
		if (gameObject)
		{
			publisherScript component = gameObject.GetComponent<publisherScript>();
			component.myID = msg.myID;
			component.isUnlocked = msg.isUnlocked;
			component.name_EN = msg.name_EN;
			component.name_GE = msg.name_GE;
			component.name_TU = msg.name_TU;
			component.name_CH = msg.name_CH;
			component.name_FR = msg.name_FR;
			component.name_JA = msg.name_JA;
			component.date_year = msg.date_year;
			component.date_month = msg.date_month;
			component.stars = msg.stars;
			component.logoID = msg.logoID;
			component.developer = msg.developer;
			component.publisher = msg.publisher;
			component.onlyMobile = msg.onlyMobile;
			component.share = msg.share;
			component.fanGenre = msg.fanGenre;
			component.firmenwert = msg.firmenwert;
			component.notForSale = msg.notForSale;
			component.lockToBuy = msg.lockToBuy;
			component.isPlayer = msg.isPlayer;
			component.ownerID = msg.ownerID;
			component.country = msg.country;
			component.awards = (int[])msg.awards.Clone();
			this.SERVER_Send_Publisher(component);
		}
	}

	// Token: 0x0600194A RID: 6474 RVA: 0x000FB150 File Offset: 0x000F9350
	public void CLIENT_Send_Payment(int toPlayer, int what, int money)
	{
		Debug.Log("CLIENT_Send_Payment()");
		NetworkClient.Send<mpCalls.c_Payment>(new mpCalls.c_Payment
		{
			playerID = this.mS_.myID,
			toPlayerID = toPlayer,
			what = what,
			money = money
		}, 0);
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x000FB1A0 File Offset: 0x000F93A0
	public void CLIENT_Get_Payment(NetworkConnection conn, mpCalls.c_Payment msg)
	{
		Debug.Log("CLIENT_Get_Payment()");
		if (msg.toPlayerID != this.mS_.myID)
		{
			this.SERVER_Send_Payment(msg.playerID, msg.toPlayerID, msg.what, msg.money);
			return;
		}
		switch (msg.what)
		{
		case 0:
		{
			string text = this.tS_.GetText(1044);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.money, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.money, 4);
			return;
		}
		case 1:
		{
			string text = this.tS_.GetText(1045);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.money, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.money, 4);
			return;
		}
		case 2:
		{
			string text = this.tS_.GetText(1658);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.money, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.money, 10);
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x000FB33C File Offset: 0x000F953C
	public void CLIENT_Send_Forschung()
	{
		Debug.Log("CLIENT_Send_Forschung()");
		player_mp player_mp = this.FindPlayer(this.mS_.myID);
		if (player_mp == null)
		{
			return;
		}
		bool flag = false;
		if (player_mp.forschungSonstiges.Length != this.fS_.RES_POINTS.Length)
		{
			player_mp.forschungSonstiges = new bool[this.fS_.RES_POINTS.Length];
		}
		if (player_mp.genres.Length != this.genres_.genres_UNLOCK.Length)
		{
			player_mp.genres = new bool[this.genres_.genres_UNLOCK.Length];
		}
		if (player_mp.themes.Length != this.themes_.themes_RES_POINTS_LEFT.Length)
		{
			player_mp.themes = new bool[this.themes_.themes_RES_POINTS_LEFT.Length];
		}
		if (player_mp.engineFeatures.Length != this.eF_.engineFeatures_RES_POINTS.Length)
		{
			player_mp.engineFeatures = new bool[this.eF_.engineFeatures_RES_POINTS.Length];
		}
		if (player_mp.gameplayFeatures.Length != this.gF_.gameplayFeatures_RES_POINTS.Length)
		{
			player_mp.gameplayFeatures = new bool[this.gF_.gameplayFeatures_RES_POINTS.Length];
		}
		if (player_mp.hardware.Length != this.hardware_.hardware_RES_POINTS.Length)
		{
			player_mp.hardware = new bool[this.hardware_.hardware_RES_POINTS.Length];
		}
		if (player_mp.hardwareFeatures.Length != this.hardwareFeatures_.hardFeat_RES_POINTS.Length)
		{
			player_mp.hardwareFeatures = new bool[this.hardwareFeatures_.hardFeat_RES_POINTS.Length];
		}
		for (int i = 0; i < player_mp.forschungSonstiges.Length; i++)
		{
			if (this.fS_.RES_POINTS_LEFT[i] <= 0f && !player_mp.forschungSonstiges[i])
			{
				player_mp.forschungSonstiges[i] = true;
				flag = true;
			}
			if (this.fS_.RES_POINTS_LEFT[i] > 0f && player_mp.forschungSonstiges[i])
			{
				player_mp.forschungSonstiges[i] = false;
				flag = true;
			}
		}
		for (int j = 0; j < player_mp.genres.Length; j++)
		{
			if (this.genres_.genres_RES_POINTS_LEFT[j] <= 0f && !player_mp.genres[j])
			{
				player_mp.genres[j] = true;
				flag = true;
			}
			if (this.genres_.genres_RES_POINTS_LEFT[j] > 0f && player_mp.genres[j])
			{
				player_mp.genres[j] = false;
				flag = true;
			}
		}
		for (int k = 0; k < player_mp.themes.Length; k++)
		{
			if (this.themes_.themes_RES_POINTS_LEFT[k] <= 0f && !player_mp.themes[k])
			{
				player_mp.themes[k] = true;
				flag = true;
			}
			if (this.themes_.themes_RES_POINTS_LEFT[k] > 0f && player_mp.themes[k])
			{
				player_mp.themes[k] = false;
				flag = true;
			}
		}
		for (int l = 0; l < player_mp.engineFeatures.Length; l++)
		{
			if (this.eF_.engineFeatures_RES_POINTS_LEFT[l] <= 0f && !player_mp.engineFeatures[l])
			{
				player_mp.engineFeatures[l] = true;
				flag = true;
			}
			if (this.eF_.engineFeatures_RES_POINTS_LEFT[l] > 0f && player_mp.engineFeatures[l])
			{
				player_mp.engineFeatures[l] = false;
				flag = true;
			}
		}
		for (int m = 0; m < player_mp.gameplayFeatures.Length; m++)
		{
			if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[m] <= 0f && !player_mp.gameplayFeatures[m])
			{
				player_mp.gameplayFeatures[m] = true;
				flag = true;
			}
			if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[m] > 0f && player_mp.gameplayFeatures[m])
			{
				player_mp.gameplayFeatures[m] = false;
				flag = true;
			}
		}
		for (int n = 0; n < player_mp.hardware.Length; n++)
		{
			if (this.hardware_.hardware_RES_POINTS_LEFT[n] <= 0f && !player_mp.hardware[n])
			{
				player_mp.hardware[n] = true;
				flag = true;
			}
			if (this.hardware_.hardware_RES_POINTS_LEFT[n] > 0f && player_mp.hardware[n])
			{
				player_mp.hardware[n] = false;
				flag = true;
			}
		}
		for (int num = 0; num < player_mp.hardwareFeatures.Length; num++)
		{
			if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[num] <= 0f && !player_mp.hardwareFeatures[num])
			{
				player_mp.hardwareFeatures[num] = true;
				flag = true;
			}
			if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[num] > 0f && player_mp.hardwareFeatures[num])
			{
				player_mp.hardwareFeatures[num] = false;
				flag = true;
			}
		}
		if (flag)
		{
			NetworkClient.Send<mpCalls.c_Forschung>(new mpCalls.c_Forschung
			{
				playerID = this.mS_.myID,
				forschungSonstiges = (bool[])player_mp.forschungSonstiges.Clone(),
				genres = (bool[])player_mp.genres.Clone(),
				themes = (bool[])player_mp.themes.Clone(),
				engineFeatures = (bool[])player_mp.engineFeatures.Clone(),
				gameplayFeatures = (bool[])player_mp.gameplayFeatures.Clone(),
				hardware = (bool[])player_mp.hardware.Clone(),
				hardwareFeatures = (bool[])player_mp.hardwareFeatures.Clone()
			}, 0);
		}
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x000FB874 File Offset: 0x000F9A74
	public void CLIENT_Get_Forschung(NetworkConnection conn, mpCalls.c_Forschung msg)
	{
		Debug.Log("CLIENT_Get_Forschung()");
		if (this.save_.loadingSavegame)
		{
			return;
		}
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.forschungSonstiges = (bool[])msg.forschungSonstiges.Clone();
		player_mp.genres = (bool[])msg.genres.Clone();
		player_mp.themes = (bool[])msg.themes.Clone();
		player_mp.engineFeatures = (bool[])msg.engineFeatures.Clone();
		player_mp.gameplayFeatures = (bool[])msg.gameplayFeatures.Clone();
		player_mp.hardware = (bool[])msg.hardware.Clone();
		player_mp.hardwareFeatures = (bool[])msg.hardwareFeatures.Clone();
		this.SERVER_Send_Forschung(msg.playerID);
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x000FB950 File Offset: 0x000F9B50
	public void CLIENT_Send_Help(int toPlayer, int what, int valueA, int valueB, int valueC)
	{
		Debug.Log("CLIENT_Send_Help()");
		NetworkClient.Send<mpCalls.c_Help>(new mpCalls.c_Help
		{
			playerID = this.mS_.myID,
			toPlayerID = toPlayer,
			what = what,
			valueA = valueA,
			valueB = valueB,
			valueC = valueC
		}, 0);
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x000FB9B4 File Offset: 0x000F9BB4
	public void CLIENT_Get_Help(NetworkConnection conn, mpCalls.c_Help msg)
	{
		Debug.Log("CLIENT_Get_Help()");
		if (msg.toPlayerID == this.mS_.myID)
		{
			switch (msg.what)
			{
			case 0:
			{
				string text = this.tS_.GetText(1327);
				text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.valueA, true));
				this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
				this.mS_.Earn((long)msg.valueA, 1);
				return;
			}
			case 1:
			{
				GameObject gameObject = GameObject.Find("ENGINE_" + msg.valueA.ToString());
				if (gameObject)
				{
					engineScript component = gameObject.GetComponent<engineScript>();
					if (component && !component.gekauft)
					{
						component.gekauft = true;
						string text = this.tS_.GetText(1330);
						text = text.Replace("<NAME>", component.GetName());
						this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
						return;
					}
				}
				break;
			}
			case 2:
			{
				string text = this.tS_.GetText(1332);
				text = text.Replace("<NAME>", this.licences_.GetName(msg.valueA));
				this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
				this.licences_.licence_GEKAUFT[msg.valueA] += msg.valueB;
				return;
			}
			case 3:
			{
				string text = this.tS_.GetText(1339);
				switch (msg.valueB)
				{
				case 0:
					text = text.Replace("<NAME>", this.genres_.GetName(msg.valueA));
					this.genres_.genres_RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				case 1:
					text = text.Replace("<NAME>", this.tS_.GetThemes(msg.valueA));
					this.themes_.themes_RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				case 2:
					text = text.Replace("<NAME>", this.eF_.GetName(msg.valueA));
					this.eF_.engineFeatures_RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				case 3:
					text = text.Replace("<NAME>", this.gF_.GetName(msg.valueA));
					this.gF_.gameplayFeatures_RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				case 4:
					text = text.Replace("<NAME>", this.hardware_.GetName(msg.valueA));
					this.hardware_.hardware_RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				case 5:
					text = text.Replace("<NAME>", this.fS_.GetName(msg.valueA));
					this.fS_.RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				case 6:
					text = text.Replace("<NAME>", this.hardwareFeatures_.GetName(msg.valueA));
					this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[msg.valueA] = 0f;
					break;
				}
				this.SERVER_Send_Forschung(this.mS_.myID);
				this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
				return;
			}
			default:
				return;
			}
		}
		else
		{
			this.SERVER_Send_Help(msg.playerID, msg.toPlayerID, msg.what, msg.valueA, msg.valueB, msg.valueC);
		}
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x000FBD8C File Offset: 0x000F9F8C
	public void CLIENT_Send_Platform(platformScript script_)
	{
		Debug.Log("CLIENT_Send_Platform()");
		NetworkClient.Send<mpCalls.c_Platform>(new mpCalls.c_Platform
		{
			myID = script_.myID,
			date_year = script_.date_year,
			date_month = script_.date_month,
			date_year_end = script_.date_year_end,
			date_month_end = script_.date_month_end,
			price = script_.price,
			dev_costs = script_.dev_costs,
			tech = script_.tech,
			typ = script_.typ,
			marktanteil = script_.marktanteil,
			needFeatures = (int[])script_.needFeatures.Clone(),
			units = script_.units,
			units_max = script_.units_max,
			name_EN = script_.name_EN,
			name_GE = script_.name_GE,
			name_TU = script_.name_TU,
			name_CH = script_.name_CH,
			name_FR = script_.name_FR,
			name_HU = script_.name_HU,
			name_JA = script_.name_JA,
			manufacturer_EN = script_.manufacturer_EN,
			manufacturer_GE = script_.manufacturer_GE,
			manufacturer_TU = script_.manufacturer_TU,
			manufacturer_CH = script_.manufacturer_CH,
			manufacturer_FR = script_.manufacturer_FR,
			manufacturer_HU = script_.manufacturer_HU,
			manufacturer_JA = script_.manufacturer_JA,
			pic1_file = script_.pic1_file,
			pic2_file = script_.pic2_file,
			pic2_year = script_.pic2_year,
			games = script_.games,
			exklusivGames = script_.exklusivGames,
			isUnlocked = script_.isUnlocked,
			vomMarktGenommen = script_.vomMarktGenommen,
			complex = script_.complex,
			internet = script_.internet,
			powerFromMarket = script_.powerFromMarket,
			myName = script_.myName,
			ownerID = script_.ownerID,
			gameID = script_.gameID,
			anzController = script_.anzController,
			conHueShift = script_.conHueShift,
			conSaturation = script_.conSaturation,
			component_cpu = script_.component_cpu,
			component_gfx = script_.component_gfx,
			component_ram = script_.component_ram,
			component_hdd = script_.component_hdd,
			component_sfx = script_.component_sfx,
			component_cooling = script_.component_cooling,
			component_disc = script_.component_disc,
			component_controller = script_.component_controller,
			component_case = script_.component_case,
			component_monitor = script_.component_monitor,
			hwFeatures = (bool[])script_.hwFeatures.Clone(),
			entwicklungsKosten = script_.entwicklungsKosten,
			einnahmen = script_.einnahmen,
			hype = script_.hype,
			costs_marketing = script_.costs_marketing,
			costs_mitarbeiter = script_.costs_mitarbeiter,
			startProduktionskosten = script_.startProduktionskosten,
			verkaufspreis = script_.verkaufspreis,
			kostenreduktion = script_.kostenreduktion,
			autoPreis = script_.autoPreis,
			thridPartyGames = script_.thridPartyGames,
			umsatzTotal = script_.umsatzTotal,
			costs_production = script_.costs_production,
			sellsPerWeek = (int[])script_.sellsPerWeek.Clone(),
			weeksOnMarket = script_.weeksOnMarket,
			review = script_.review,
			performancePoints = script_.performancePoints
		}, 0);
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x000FC160 File Offset: 0x000FA360
	public void CLIENT_Get_Platform(NetworkConnection conn, mpCalls.c_Platform msg)
	{
		Debug.Log("CLIENT_Get_Platform()");
		GameObject gameObject = GameObject.Find("PLATFORM_" + msg.myID.ToString());
		platformScript platformScript;
		if (gameObject)
		{
			platformScript = gameObject.GetComponent<platformScript>();
			if (platformScript.ownerID == this.mS_.myID)
			{
				return;
			}
			platformScript.myID = msg.myID;
			platformScript.date_year = msg.date_year;
			platformScript.date_month = msg.date_month;
			platformScript.date_year_end = msg.date_year_end;
			platformScript.date_month_end = msg.date_month_end;
			platformScript.price = msg.price;
			platformScript.dev_costs = msg.dev_costs;
			platformScript.tech = msg.tech;
			platformScript.typ = msg.typ;
			platformScript.needFeatures = (int[])msg.needFeatures.Clone();
			platformScript.units = msg.units;
			platformScript.units_max = msg.units_max;
			platformScript.name_EN = msg.name_EN;
			platformScript.name_GE = msg.name_GE;
			platformScript.name_TU = msg.name_TU;
			platformScript.name_CH = msg.name_CH;
			platformScript.name_FR = msg.name_FR;
			platformScript.name_HU = msg.name_HU;
			platformScript.name_JA = msg.name_JA;
			platformScript.manufacturer_EN = msg.manufacturer_EN;
			platformScript.manufacturer_GE = msg.manufacturer_GE;
			platformScript.manufacturer_TU = msg.manufacturer_TU;
			platformScript.manufacturer_CH = msg.manufacturer_CH;
			platformScript.manufacturer_FR = msg.manufacturer_FR;
			platformScript.manufacturer_HU = msg.manufacturer_HU;
			platformScript.manufacturer_JA = msg.manufacturer_JA;
			platformScript.pic1_file = msg.pic1_file;
			platformScript.pic2_file = msg.pic2_file;
			platformScript.pic2_year = msg.pic2_year;
			platformScript.games = msg.games;
			platformScript.exklusivGames = msg.exklusivGames;
			platformScript.isUnlocked = msg.isUnlocked;
			platformScript.vomMarktGenommen = msg.vomMarktGenommen;
			platformScript.complex = msg.complex;
			platformScript.internet = msg.internet;
			platformScript.powerFromMarket = msg.powerFromMarket;
			platformScript.myName = msg.myName;
			platformScript.ownerID = msg.ownerID;
			platformScript.gameID = msg.gameID;
			platformScript.anzController = msg.anzController;
			platformScript.conHueShift = msg.conHueShift;
			platformScript.conSaturation = msg.conSaturation;
			platformScript.component_cpu = msg.component_cpu;
			platformScript.component_gfx = msg.component_gfx;
			platformScript.component_ram = msg.component_ram;
			platformScript.component_hdd = msg.component_hdd;
			platformScript.component_sfx = msg.component_sfx;
			platformScript.component_cooling = msg.component_cooling;
			platformScript.component_disc = msg.component_disc;
			platformScript.component_controller = msg.component_controller;
			platformScript.component_case = msg.component_case;
			platformScript.component_monitor = msg.component_monitor;
			platformScript.hwFeatures = (bool[])msg.hwFeatures.Clone();
			platformScript.entwicklungsKosten = msg.entwicklungsKosten;
			platformScript.einnahmen = msg.einnahmen;
			platformScript.hype = msg.hype;
			platformScript.costs_marketing = msg.costs_marketing;
			platformScript.costs_mitarbeiter = msg.costs_mitarbeiter;
			platformScript.startProduktionskosten = msg.startProduktionskosten;
			platformScript.verkaufspreis = msg.verkaufspreis;
			platformScript.kostenreduktion = msg.kostenreduktion;
			platformScript.autoPreis = msg.autoPreis;
			platformScript.thridPartyGames = msg.thridPartyGames;
			platformScript.umsatzTotal = msg.umsatzTotal;
			platformScript.costs_production = msg.costs_production;
			platformScript.weeksOnMarket = msg.weeksOnMarket;
			platformScript.review = msg.review;
			platformScript.performancePoints = msg.performancePoints;
		}
		else
		{
			platformScript = this.platforms_.CreatePlatform();
			platformScript.myID = msg.myID;
			platformScript.date_year = msg.date_year;
			platformScript.date_month = msg.date_month;
			platformScript.date_year_end = msg.date_year_end;
			platformScript.date_month_end = msg.date_month_end;
			platformScript.price = msg.price;
			platformScript.dev_costs = msg.dev_costs;
			platformScript.tech = msg.tech;
			platformScript.typ = msg.typ;
			platformScript.needFeatures = (int[])msg.needFeatures.Clone();
			platformScript.units = msg.units;
			platformScript.units_max = msg.units_max;
			platformScript.name_EN = msg.name_EN;
			platformScript.name_GE = msg.name_GE;
			platformScript.name_TU = msg.name_TU;
			platformScript.name_CH = msg.name_CH;
			platformScript.name_FR = msg.name_FR;
			platformScript.name_HU = msg.name_HU;
			platformScript.name_JA = msg.name_JA;
			platformScript.manufacturer_EN = msg.manufacturer_EN;
			platformScript.manufacturer_GE = msg.manufacturer_GE;
			platformScript.manufacturer_TU = msg.manufacturer_TU;
			platformScript.manufacturer_CH = msg.manufacturer_CH;
			platformScript.manufacturer_FR = msg.manufacturer_FR;
			platformScript.manufacturer_HU = msg.manufacturer_HU;
			platformScript.manufacturer_JA = msg.manufacturer_JA;
			platformScript.pic1_file = msg.pic1_file;
			platformScript.pic2_file = msg.pic2_file;
			platformScript.pic2_year = msg.pic2_year;
			platformScript.games = msg.games;
			platformScript.exklusivGames = msg.exklusivGames;
			platformScript.isUnlocked = msg.isUnlocked;
			platformScript.vomMarktGenommen = msg.vomMarktGenommen;
			platformScript.complex = msg.complex;
			platformScript.internet = msg.internet;
			platformScript.powerFromMarket = msg.powerFromMarket;
			platformScript.myName = msg.myName;
			platformScript.ownerID = msg.ownerID;
			platformScript.gameID = msg.gameID;
			platformScript.anzController = msg.anzController;
			platformScript.conHueShift = msg.conHueShift;
			platformScript.conSaturation = msg.conSaturation;
			platformScript.component_cpu = msg.component_cpu;
			platformScript.component_gfx = msg.component_gfx;
			platformScript.component_ram = msg.component_ram;
			platformScript.component_hdd = msg.component_hdd;
			platformScript.component_sfx = msg.component_sfx;
			platformScript.component_cooling = msg.component_cooling;
			platformScript.component_disc = msg.component_disc;
			platformScript.component_controller = msg.component_controller;
			platformScript.component_case = msg.component_case;
			platformScript.component_monitor = msg.component_monitor;
			platformScript.hwFeatures = (bool[])msg.hwFeatures.Clone();
			platformScript.entwicklungsKosten = msg.entwicklungsKosten;
			platformScript.einnahmen = msg.einnahmen;
			platformScript.hype = msg.hype;
			platformScript.costs_marketing = msg.costs_marketing;
			platformScript.costs_mitarbeiter = msg.costs_mitarbeiter;
			platformScript.startProduktionskosten = msg.startProduktionskosten;
			platformScript.verkaufspreis = msg.verkaufspreis;
			platformScript.kostenreduktion = msg.kostenreduktion;
			platformScript.autoPreis = msg.autoPreis;
			platformScript.thridPartyGames = msg.thridPartyGames;
			platformScript.umsatzTotal = msg.umsatzTotal;
			platformScript.costs_production = msg.costs_production;
			platformScript.weeksOnMarket = msg.weeksOnMarket;
			platformScript.review = msg.review;
			platformScript.performancePoints = msg.performancePoints;
			platformScript.Init();
			if (!platformScript.OwnerIsNPC() && !platformScript.vomMarktGenommen)
			{
				this.guiMain_.CreateTopNewsPlatform(platformScript);
				string text = this.tS_.GetText(1629);
				text = text.Replace("<NAME>", msg.myName);
				this.guiMain_.AddChat(msg.ownerID, text);
			}
			if (!platformScript.OwnerIsNPC() && platformScript.vomMarktGenommen)
			{
				this.guiMain_.CreateTopNewsPlatformRemove(platformScript);
			}
		}
		this.SERVER_Send_Platform(platformScript);
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x000FC8D0 File Offset: 0x000FAAD0
	public void CLIENT_Send_Engine(engineScript script_)
	{
		Debug.Log("CLIENT_Send_Engine()");
		NetworkClient.Send<mpCalls.c_Engine>(new mpCalls.c_Engine
		{
			myID = script_.myID,
			ownerID = script_.ownerID,
			isUnlocked = script_.isUnlocked,
			gekauft = script_.gekauft,
			myName = script_.myName,
			features = script_.features,
			spezialgenre = script_.spezialgenre,
			spezialplatform = script_.spezialplatform,
			sellEngine = script_.sellEngine,
			preis = script_.preis,
			gewinnbeteiligung = script_.gewinnbeteiligung
		}, 0);
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x000FC988 File Offset: 0x000FAB88
	public void CLIENT_Get_Engine(NetworkConnection conn, mpCalls.c_Engine msg)
	{
		Debug.Log("CLIENT_Get_Engine()");
		GameObject gameObject = GameObject.Find("ENGINE_" + msg.myID.ToString());
		engineScript engineScript;
		if (gameObject)
		{
			engineScript = gameObject.GetComponent<engineScript>();
			if (engineScript.ownerID == this.mS_.myID)
			{
				return;
			}
			engineScript.myID = msg.myID;
			engineScript.ownerID = msg.ownerID;
			engineScript.isUnlocked = msg.isUnlocked;
			if (msg.ownerID != -1)
			{
				engineScript.isUnlocked = true;
			}
			engineScript.gekauft = msg.gekauft;
			engineScript.myName = msg.myName;
			engineScript.features = (bool[])msg.features.Clone();
			engineScript.spezialgenre = msg.spezialgenre;
			engineScript.spezialplatform = msg.spezialplatform;
			engineScript.sellEngine = msg.sellEngine;
			engineScript.preis = msg.preis;
			engineScript.gewinnbeteiligung = msg.gewinnbeteiligung;
		}
		else
		{
			engineScript = this.eF_.CreateEngine();
			engineScript.myID = msg.myID;
			engineScript.ownerID = msg.ownerID;
			engineScript.isUnlocked = msg.isUnlocked;
			if (msg.ownerID != -1)
			{
				engineScript.isUnlocked = true;
			}
			engineScript.gekauft = msg.gekauft;
			engineScript.myName = msg.myName;
			engineScript.features = (bool[])msg.features.Clone();
			engineScript.spezialgenre = msg.spezialgenre;
			engineScript.spezialplatform = msg.spezialplatform;
			engineScript.sellEngine = msg.sellEngine;
			engineScript.preis = msg.preis;
			engineScript.gewinnbeteiligung = msg.gewinnbeteiligung;
			engineScript.Init();
			this.guiMain_.CreateTopNewsNpcEngine(engineScript.GetName());
			string text = this.tS_.GetText(1270);
			text = text.Replace("<NAME>", msg.myName);
			this.guiMain_.AddChat(msg.ownerID, text);
		}
		this.SERVER_Send_Engine(engineScript);
	}

	// Token: 0x06001954 RID: 6484 RVA: 0x000FCB84 File Offset: 0x000FAD84
	public void CLIENT_Send_Chat(string c)
	{
		Debug.Log("CLIENT_Send_Chat()");
		NetworkClient.Send<mpCalls.c_Chat>(new mpCalls.c_Chat
		{
			playerID = this.mS_.myID,
			text = c
		}, 0);
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x000FCBC4 File Offset: 0x000FADC4
	public void CLIENT_Get_Chat(NetworkConnection conn, mpCalls.c_Chat msg)
	{
		Debug.Log("CLIENT_Get_Chat()");
		this.guiMain_.AddChat(msg.playerID, msg.text);
		this.SERVER_Send_Chat(msg.playerID, msg.text);
	}

	// Token: 0x06001956 RID: 6486 RVA: 0x000FCBFC File Offset: 0x000FADFC
	public void CLIENT_Send_Money()
	{
		Debug.Log("CLIENT_Send_Money()");
		NetworkClient.Send<mpCalls.c_Money>(new mpCalls.c_Money
		{
			playerID = this.mS_.myID,
			money = this.mS_.money,
			fans = this.genres_.GetAmountFans()
		}, 0);
	}

	// Token: 0x06001957 RID: 6487 RVA: 0x000FCC58 File Offset: 0x000FAE58
	public void CLIENT_Get_Money(NetworkConnection conn, mpCalls.c_Money msg)
	{
		Debug.Log("CLIENT_Get_Money()");
		if (this.save_.loadingSavegame)
		{
			return;
		}
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.timeout = 0f;
		player_mp.money = msg.money;
		player_mp.fans = msg.fans;
	}

	// Token: 0x06001958 RID: 6488 RVA: 0x000FCCB4 File Offset: 0x000FAEB4
	public void CLIENT_Send_ExklusivKonsolenSells(gameScript script_, long i)
	{
		Debug.Log("CLIENT_Send_ExklusivKonsolenSells()");
		NetworkClient.Send<mpCalls.c_exklusivKonsolenSells>(new mpCalls.c_exklusivKonsolenSells
		{
			gameID = script_.myID,
			exklusivKonsolenSells = i
		}, 0);
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x000FCCF0 File Offset: 0x000FAEF0
	public void CLIENT_Get_ExklusivKonsolenSells(NetworkConnection conn, mpCalls.c_exklusivKonsolenSells msg)
	{
		Debug.Log("CLIENT_Get_ExklusivKonsolenSells()");
		GameObject game = this.GetGame(msg.gameID);
		if (game)
		{
			gameScript component = game.GetComponent<gameScript>();
			component.myID = msg.gameID;
			component.exklusivKonsolenSells += msg.exklusivKonsolenSells;
			this.SERVER_Send_ExklusivKonsolenSells(component, msg.exklusivKonsolenSells);
		}
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x000FCD50 File Offset: 0x000FAF50
	public void CLIENT_Send_GameData(gameScript script_)
	{
		Debug.Log("CLIENT_Send_GameData()");
		NetworkClient.Send<mpCalls.c_GameData>(new mpCalls.c_GameData
		{
			gameID = script_.myID,
			sellsTotal = script_.sellsTotal,
			umsatzTotal = script_.umsatzTotal,
			isOnMarket = script_.isOnMarket,
			weeksOnMarket = script_.weeksOnMarket,
			userPositiv = script_.userPositiv,
			userNegativ = script_.userNegativ,
			costs_entwicklung = script_.costs_entwicklung,
			costs_mitarbeiter = script_.costs_mitarbeiter,
			costs_marketing = script_.costs_marketing,
			costs_enginegebuehren = script_.costs_enginegebuehren,
			costs_server = script_.costs_server,
			costs_production = script_.costs_production,
			costs_updates = script_.costs_updates,
			abonnements = script_.abonnements,
			abonnementsWoche = script_.abonnementsWoche,
			bestAbonnements = script_.bestAbonnements,
			bestChartPosition = script_.bestChartPosition,
			exklusivKonsolenSells = script_.exklusivKonsolenSells,
			ipPunkte = script_.ipPunkte,
			pubAngebot = script_.pubAngebot,
			pubAngebot_Weeks = script_.pubAngebot_Weeks,
			pubAngebot_Verhandlung = script_.pubAngebot_Verhandlung,
			pubAngebot_Retail = script_.pubAngebot_Retail,
			pubAngebot_Digital = script_.pubAngebot_Digital,
			pubAngebot_Garantiesumme = script_.pubAngebot_Garantiesumme,
			pubAngebot_Gewinnbeteiligung = script_.pubAngebot_Gewinnbeteiligung,
			auftragsspiel = script_.auftragsspiel,
			auftragsspiel_gehalt = script_.auftragsspiel_gehalt,
			auftragsspiel_bonus = script_.auftragsspiel_bonus,
			auftragsspiel_zeitInWochen = script_.auftragsspiel_zeitInWochen,
			auftragsspiel_wochenAlsAngebot = script_.auftragsspiel_wochenAlsAngebot,
			auftragsspiel_zeitAbgelaufen = script_.auftragsspiel_zeitAbgelaufen,
			auftragsspiel_mindestbewertung = script_.auftragsspiel_mindestbewertung,
			ipName = script_.ipName,
			sellsPerWeek = (int[])script_.sellsPerWeek.Clone()
		}, 0);
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x000FCF54 File Offset: 0x000FB154
	public void CLIENT_Get_GameData(NetworkConnection conn, mpCalls.c_GameData msg)
	{
		Debug.Log("CLIENT_Get_GameData()");
		GameObject game = this.GetGame(msg.gameID);
		if (game)
		{
			gameScript component = game.GetComponent<gameScript>();
			component.myID = msg.gameID;
			component.sellsTotal = msg.sellsTotal;
			component.umsatzTotal = msg.umsatzTotal;
			component.isOnMarket = msg.isOnMarket;
			component.weeksOnMarket = msg.weeksOnMarket;
			component.userPositiv = msg.userPositiv;
			component.userNegativ = msg.userNegativ;
			component.costs_entwicklung = msg.costs_entwicklung;
			component.costs_mitarbeiter = msg.costs_mitarbeiter;
			component.costs_marketing = msg.costs_marketing;
			component.costs_enginegebuehren = msg.costs_enginegebuehren;
			component.costs_server = msg.costs_server;
			component.costs_production = msg.costs_production;
			component.costs_updates = msg.costs_updates;
			component.abonnements = msg.abonnements;
			component.abonnementsWoche = msg.abonnementsWoche;
			component.bestAbonnements = msg.bestAbonnements;
			component.bestChartPosition = msg.bestChartPosition;
			component.exklusivKonsolenSells = msg.exklusivKonsolenSells;
			component.ipPunkte = msg.ipPunkte;
			component.pubAngebot = msg.pubAngebot;
			component.pubAngebot_Weeks = msg.pubAngebot_Weeks;
			component.pubAngebot_Verhandlung = msg.pubAngebot_Verhandlung;
			component.pubAngebot_Retail = msg.pubAngebot_Retail;
			component.pubAngebot_Digital = msg.pubAngebot_Digital;
			component.pubAngebot_Garantiesumme = msg.pubAngebot_Garantiesumme;
			component.pubAngebot_Gewinnbeteiligung = msg.pubAngebot_Gewinnbeteiligung;
			component.auftragsspiel = msg.auftragsspiel;
			component.auftragsspiel_gehalt = msg.auftragsspiel_gehalt;
			component.auftragsspiel_bonus = msg.auftragsspiel_bonus;
			component.auftragsspiel_zeitInWochen = msg.auftragsspiel_zeitInWochen;
			component.auftragsspiel_wochenAlsAngebot = msg.auftragsspiel_wochenAlsAngebot;
			component.auftragsspiel_zeitAbgelaufen = msg.auftragsspiel_zeitAbgelaufen;
			component.auftragsspiel_mindestbewertung = msg.auftragsspiel_mindestbewertung;
			component.ipName = msg.ipName;
			component.sellsPerWeek = (int[])msg.sellsPerWeek.Clone();
			this.games_.UpdateChartsWeek();
			this.guiMain_.UpdateCharts();
			this.SERVER_Send_GameData(component);
		}
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x000FD164 File Offset: 0x000FB364
	public void CLIENT_Send_Game(gameScript script_)
	{
		Debug.Log("CLIENT_Send_Game()");
		NetworkClient.Send<mpCalls.c_Game>(new mpCalls.c_Game
		{
			gameID = script_.myID,
			myName = script_.GetNameSimple(),
			ipName = script_.ipName,
			inDevelopment = script_.inDevelopment,
			developerID = script_.developerID,
			publisherID = script_.publisherID,
			ownerID = script_.ownerID,
			engineID = script_.engineID,
			hype = script_.hype,
			isOnMarket = script_.isOnMarket,
			warBeiAwards = script_.warBeiAwards,
			weeksOnMarket = script_.weeksOnMarket,
			usk = script_.usk,
			freigabeBudget = script_.freigabeBudget,
			reviewGameplay = script_.reviewGameplay,
			reviewGrafik = script_.reviewGrafik,
			reviewSound = script_.reviewSound,
			reviewSteuerung = script_.reviewSteuerung,
			reviewTotal = script_.reviewTotal,
			reviewGameplayText = script_.reviewGameplayText,
			reviewGrafikText = script_.reviewGrafikText,
			reviewSoundText = script_.reviewSoundText,
			reviewSteuerungText = script_.reviewSteuerungText,
			reviewTotalText = script_.reviewTotalText,
			date_year = script_.date_year,
			date_month = script_.date_month,
			date_start_year = script_.date_start_year,
			date_start_month = script_.date_start_month,
			sellsTotal = script_.sellsTotal,
			umsatzTotal = script_.umsatzTotal,
			costs_entwicklung = script_.costs_entwicklung,
			costs_mitarbeiter = script_.costs_mitarbeiter,
			costs_marketing = script_.costs_marketing,
			costs_enginegebuehren = script_.costs_enginegebuehren,
			costs_server = script_.costs_server,
			costs_production = script_.costs_production,
			costs_updates = script_.costs_updates,
			typ_standard = script_.typ_standard,
			typ_nachfolger = script_.typ_nachfolger,
			originalIP = script_.originalIP,
			teile = script_.teile,
			typ_contractGame = script_.typ_contractGame,
			typ_remaster = script_.typ_remaster,
			typ_spinoff = script_.typ_spinoff,
			typ_addon = script_.typ_addon,
			typ_addonStandalone = script_.typ_addonStandalone,
			typ_mmoaddon = script_.typ_mmoaddon,
			typ_bundle = script_.typ_bundle,
			typ_budget = script_.typ_budget,
			typ_bundleAddon = script_.typ_bundleAddon,
			typ_goty = script_.typ_goty,
			originalGameID = script_.originalGameID,
			portID = script_.portID,
			mainIP = script_.mainIP,
			ipPunkte = script_.ipPunkte,
			exklusiv = script_.exklusiv,
			herstellerExklusiv = script_.herstellerExklusiv,
			retro = script_.retro,
			handy = script_.handy,
			arcade = script_.arcade,
			goty = script_.goty,
			nachfolger_created = script_.nachfolger_created,
			remaster_created = script_.remaster_created,
			budget_created = script_.budget_created,
			goty_created = script_.goty_created,
			trendsetter = script_.trendsetter,
			spielbericht = script_.spielbericht,
			amountUpdates = script_.amountUpdates,
			bonusSellsUpdates = script_.bonusSellsUpdates,
			amountAddons = script_.amountAddons,
			bonusSellsAddons = script_.bonusSellsAddons,
			amountMMOAddons = script_.amountMMOAddons,
			bonusSellsMMOAddons = script_.bonusSellsMMOAddons,
			addonQuality = script_.addonQuality,
			devAktFeature = script_.devAktFeature,
			devPoints = script_.devPoints,
			devPointsStart = script_.devPointsStart,
			devPoints_Gesamt = script_.devPoints_Gesamt,
			devPointsStart_Gesamt = script_.devPointsStart_Gesamt,
			points_gameplay = script_.points_gameplay,
			points_grafik = script_.points_grafik,
			points_sound = script_.points_sound,
			points_technik = script_.points_technik,
			points_bugs = script_.points_bugs,
			beschreibung = script_.beschreibung,
			gameTyp = script_.gameTyp,
			gameSize = script_.gameSize,
			gameZielgruppe = script_.gameZielgruppe,
			maingenre = script_.maingenre,
			subgenre = script_.subgenre,
			gameMainTheme = script_.gameMainTheme,
			gameSubTheme = script_.gameSubTheme,
			gameLicence = script_.gameLicence,
			gameCopyProtect = script_.gameCopyProtect,
			gameAntiCheat = script_.gameAntiCheat,
			gameAP_Gameplay = script_.gameAP_Gameplay,
			gameAP_Grafik = script_.gameAP_Grafik,
			gameAP_Sound = script_.gameAP_Sound,
			gameAP_Technik = script_.gameAP_Technik,
			gameLanguage = (bool[])script_.gameLanguage.Clone(),
			gameGameplayFeatures = (bool[])script_.gameGameplayFeatures.Clone(),
			gamePlatform = (int[])script_.gamePlatform.Clone(),
			gameEngineFeature = (int[])script_.gameEngineFeature.Clone(),
			gameplayFeatures_DevDone = (bool[])script_.gameplayFeatures_DevDone.Clone(),
			engineFeature_DevDone = (bool[])script_.engineFeature_DevDone.Clone(),
			gameplayStudio = (bool[])script_.gameplayStudio.Clone(),
			grafikStudio = (bool[])script_.grafikStudio.Clone(),
			soundStudio = (bool[])script_.soundStudio.Clone(),
			motionCaptureStudio = (bool[])script_.motionCaptureStudio.Clone(),
			bundleID = (int[])script_.bundleID.Clone(),
			portExist = (bool[])script_.portExist.Clone(),
			sellsPerWeek = (int[])script_.sellsPerWeek.Clone(),
			verkaufspreis = (int[])script_.verkaufspreis.Clone(),
			releaseDate = script_.releaseDate,
			abonnements = script_.abonnements,
			abonnementsWoche = script_.abonnementsWoche,
			aboPreis = script_.aboPreis,
			pubOffer = script_.pubOffer,
			pubAngebot = script_.pubAngebot,
			pubAngebot_Weeks = script_.pubAngebot_Weeks,
			pubAngebot_Verhandlung = script_.pubAngebot_Verhandlung,
			pubAngebot_Retail = script_.pubAngebot_Retail,
			pubAngebot_Digital = script_.pubAngebot_Digital,
			pubAngebot_Garantiesumme = script_.pubAngebot_Garantiesumme,
			pubAngebot_Gewinnbeteiligung = script_.pubAngebot_Gewinnbeteiligung,
			auftragsspiel = script_.auftragsspiel,
			auftragsspiel_gehalt = script_.auftragsspiel_gehalt,
			auftragsspiel_bonus = script_.auftragsspiel_bonus,
			auftragsspiel_zeitInWochen = script_.auftragsspiel_zeitInWochen,
			auftragsspiel_wochenAlsAngebot = script_.auftragsspiel_wochenAlsAngebot,
			auftragsspiel_zeitAbgelaufen = script_.auftragsspiel_zeitAbgelaufen,
			auftragsspiel_mindestbewertung = script_.auftragsspiel_mindestbewertung,
			f2pConverted = script_.f2pConverted
		}, 0);
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x000FD8D8 File Offset: 0x000FBAD8
	public void CLIENT_Get_Game(NetworkConnection conn, mpCalls.c_Game msg)
	{
		GameObject game = this.GetGame(msg.gameID);
		gameScript gameScript;
		if (!game)
		{
			gameScript = this.games_.CreateNewGame(true, false);
		}
		else
		{
			gameScript = game.GetComponent<gameScript>();
		}
		gameScript.myID = msg.gameID;
		gameScript.SetMyName(msg.myName);
		gameScript.ipName = msg.ipName;
		gameScript.inDevelopment = msg.inDevelopment;
		gameScript.developerID = msg.developerID;
		gameScript.publisherID = msg.publisherID;
		gameScript.ownerID = msg.ownerID;
		gameScript.engineID = msg.engineID;
		gameScript.hype = msg.hype;
		gameScript.isOnMarket = msg.isOnMarket;
		gameScript.warBeiAwards = msg.warBeiAwards;
		gameScript.weeksOnMarket = msg.weeksOnMarket;
		gameScript.usk = msg.usk;
		gameScript.freigabeBudget = msg.freigabeBudget;
		gameScript.reviewGameplay = msg.reviewGameplay;
		gameScript.reviewGrafik = msg.reviewGrafik;
		gameScript.reviewSound = msg.reviewSound;
		gameScript.reviewSteuerung = msg.reviewSteuerung;
		gameScript.reviewTotal = msg.reviewTotal;
		gameScript.reviewGameplayText = msg.reviewGameplayText;
		gameScript.reviewGrafikText = msg.reviewGrafikText;
		gameScript.reviewSoundText = msg.reviewSoundText;
		gameScript.reviewSteuerungText = msg.reviewSteuerungText;
		gameScript.reviewTotalText = msg.reviewTotalText;
		gameScript.date_year = msg.date_year;
		gameScript.date_month = msg.date_month;
		gameScript.date_start_year = msg.date_start_year;
		gameScript.date_start_month = msg.date_start_month;
		gameScript.sellsTotal = msg.sellsTotal;
		gameScript.umsatzTotal = msg.umsatzTotal;
		gameScript.costs_entwicklung = msg.costs_entwicklung;
		gameScript.costs_mitarbeiter = msg.costs_mitarbeiter;
		gameScript.costs_marketing = msg.costs_marketing;
		gameScript.costs_enginegebuehren = msg.costs_enginegebuehren;
		gameScript.costs_server = msg.costs_server;
		gameScript.costs_production = msg.costs_production;
		gameScript.costs_updates = msg.costs_updates;
		gameScript.typ_standard = msg.typ_standard;
		gameScript.typ_nachfolger = msg.typ_nachfolger;
		gameScript.originalIP = msg.originalIP;
		gameScript.teile = msg.teile;
		gameScript.typ_contractGame = msg.typ_contractGame;
		gameScript.typ_remaster = msg.typ_remaster;
		gameScript.typ_spinoff = msg.typ_spinoff;
		gameScript.typ_addon = msg.typ_addon;
		gameScript.typ_addonStandalone = msg.typ_addonStandalone;
		gameScript.typ_mmoaddon = msg.typ_mmoaddon;
		gameScript.typ_bundle = msg.typ_bundle;
		gameScript.typ_budget = msg.typ_budget;
		gameScript.typ_bundleAddon = msg.typ_bundleAddon;
		gameScript.typ_goty = msg.typ_goty;
		gameScript.originalGameID = msg.originalGameID;
		gameScript.portID = msg.portID;
		gameScript.mainIP = msg.mainIP;
		gameScript.ipPunkte = msg.ipPunkte;
		gameScript.exklusiv = msg.exklusiv;
		gameScript.herstellerExklusiv = msg.herstellerExklusiv;
		gameScript.retro = msg.retro;
		gameScript.handy = msg.handy;
		gameScript.arcade = msg.arcade;
		gameScript.goty = msg.goty;
		gameScript.nachfolger_created = msg.nachfolger_created;
		gameScript.remaster_created = msg.remaster_created;
		gameScript.budget_created = msg.budget_created;
		gameScript.goty_created = msg.goty_created;
		gameScript.trendsetter = msg.trendsetter;
		gameScript.spielbericht = msg.spielbericht;
		gameScript.amountUpdates = msg.amountUpdates;
		gameScript.bonusSellsUpdates = msg.bonusSellsUpdates;
		gameScript.amountAddons = msg.amountAddons;
		gameScript.bonusSellsAddons = msg.bonusSellsAddons;
		gameScript.amountMMOAddons = msg.amountMMOAddons;
		gameScript.bonusSellsMMOAddons = msg.bonusSellsMMOAddons;
		gameScript.addonQuality = msg.addonQuality;
		gameScript.devAktFeature = msg.devAktFeature;
		gameScript.devPoints = msg.devPoints;
		gameScript.devPointsStart = msg.devPointsStart;
		gameScript.devPoints_Gesamt = msg.devPoints_Gesamt;
		gameScript.devPointsStart_Gesamt = msg.devPointsStart_Gesamt;
		gameScript.points_gameplay = msg.points_gameplay;
		gameScript.points_grafik = msg.points_grafik;
		gameScript.points_sound = msg.points_sound;
		gameScript.points_technik = msg.points_technik;
		gameScript.points_bugs = msg.points_bugs;
		gameScript.beschreibung = msg.beschreibung;
		gameScript.gameTyp = msg.gameTyp;
		gameScript.gameSize = msg.gameSize;
		gameScript.gameZielgruppe = msg.gameZielgruppe;
		gameScript.maingenre = msg.maingenre;
		gameScript.subgenre = msg.subgenre;
		gameScript.gameMainTheme = msg.gameMainTheme;
		gameScript.gameSubTheme = msg.gameSubTheme;
		gameScript.gameLicence = msg.gameLicence;
		gameScript.gameCopyProtect = msg.gameCopyProtect;
		gameScript.gameAntiCheat = msg.gameAntiCheat;
		gameScript.gameAP_Gameplay = msg.gameAP_Gameplay;
		gameScript.gameAP_Grafik = msg.gameAP_Grafik;
		gameScript.gameAP_Sound = msg.gameAP_Sound;
		gameScript.gameAP_Technik = msg.gameAP_Technik;
		gameScript.gameLanguage = (bool[])msg.gameLanguage.Clone();
		gameScript.gameGameplayFeatures = (bool[])msg.gameGameplayFeatures.Clone();
		gameScript.gamePlatform = (int[])msg.gamePlatform.Clone();
		gameScript.gameEngineFeature = (int[])msg.gameEngineFeature.Clone();
		gameScript.gameplayFeatures_DevDone = (bool[])msg.gameplayFeatures_DevDone.Clone();
		gameScript.engineFeature_DevDone = (bool[])msg.engineFeature_DevDone.Clone();
		gameScript.gameplayStudio = (bool[])msg.gameplayStudio.Clone();
		gameScript.grafikStudio = (bool[])msg.grafikStudio.Clone();
		gameScript.soundStudio = (bool[])msg.soundStudio.Clone();
		gameScript.motionCaptureStudio = (bool[])msg.motionCaptureStudio.Clone();
		gameScript.bundleID = (int[])msg.bundleID.Clone();
		gameScript.portExist = (bool[])msg.portExist.Clone();
		gameScript.sellsPerWeek = (int[])msg.sellsPerWeek.Clone();
		gameScript.verkaufspreis = (int[])msg.verkaufspreis.Clone();
		gameScript.releaseDate = msg.releaseDate;
		gameScript.abonnements = msg.abonnements;
		gameScript.abonnementsWoche = msg.abonnementsWoche;
		gameScript.aboPreis = msg.aboPreis;
		gameScript.pubOffer = msg.pubOffer;
		gameScript.pubAngebot = msg.pubAngebot;
		gameScript.pubAngebot_Weeks = msg.pubAngebot_Weeks;
		gameScript.pubAngebot_Verhandlung = msg.pubAngebot_Verhandlung;
		gameScript.pubAngebot_Retail = msg.pubAngebot_Retail;
		gameScript.pubAngebot_Digital = msg.pubAngebot_Digital;
		gameScript.pubAngebot_Garantiesumme = msg.pubAngebot_Garantiesumme;
		gameScript.pubAngebot_Gewinnbeteiligung = msg.pubAngebot_Gewinnbeteiligung;
		gameScript.auftragsspiel = msg.auftragsspiel;
		gameScript.auftragsspiel_gehalt = msg.auftragsspiel_gehalt;
		gameScript.auftragsspiel_bonus = msg.auftragsspiel_bonus;
		gameScript.auftragsspiel_zeitInWochen = msg.auftragsspiel_zeitInWochen;
		gameScript.auftragsspiel_wochenAlsAngebot = msg.auftragsspiel_wochenAlsAngebot;
		gameScript.auftragsspiel_zeitAbgelaufen = msg.auftragsspiel_zeitAbgelaufen;
		gameScript.auftragsspiel_mindestbewertung = msg.auftragsspiel_mindestbewertung;
		gameScript.f2pConverted = msg.f2pConverted;
		gameScript.SetGameObjectName();
		gameScript.InitUI();
		if (gameScript.isOnMarket)
		{
			gameScript.SetOnMarket();
		}
		this.games_.FindGames();
		if (this.mS_.newsSetting[0] && gameScript.isOnMarket && gameScript.GameFromMitspieler())
		{
			string text = this.tS_.GetText(494);
			text = text.Replace("<NAME1>", gameScript.GetDeveloperName());
			text = text.Replace("<NAME2>", gameScript.GetNameWithTag());
			this.guiMain_.CreateTopNewsInfo(text);
			text = this.tS_.GetText(1269);
			text = text.Replace("<NAME>", msg.myName);
			this.guiMain_.AddChat(gameScript.GetIdFromMitspieler(), text);
		}
		this.games_.UpdateChartsWeek();
		this.guiMain_.UpdateCharts();
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x000FE0B0 File Offset: 0x000FC2B0
	public void CLIENT_Send_BuyLizenz(int objectID_)
	{
		NetworkClient.Send<mpCalls.c_BuyLizenz>(new mpCalls.c_BuyLizenz
		{
			playerID = this.mS_.myID,
			objectID = objectID_
		}, 0);
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x000FE0E6 File Offset: 0x000FC2E6
	public void CLIENT_Get_BuyLizenz(NetworkConnection conn, mpCalls.c_BuyLizenz msg)
	{
		Debug.Log("CLIENT_Get_BuyLizenz");
		this.licences_.licence_ANGEBOT[msg.objectID] = 0;
		this.SERVER_Send_Lizenz(msg.objectID);
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x000FE114 File Offset: 0x000FC314
	public void CLIENT_Send_DeleteArbeitsmarkt(int objectID_, bool eingestellt)
	{
		NetworkClient.Send<mpCalls.c_DeleteArbeitsmarkt>(new mpCalls.c_DeleteArbeitsmarkt
		{
			playerID = this.mS_.myID,
			objectID = objectID_,
			eingestellt = eingestellt
		}, 0);
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x000FE154 File Offset: 0x000FC354
	public void CLIENT_Get_DeleteArbeitsmarkt(NetworkConnection conn, mpCalls.c_DeleteArbeitsmarkt msg)
	{
		Debug.Log("CLIENT_Get_DeleteArbeitsmarkt");
		GameObject gameObject = GameObject.Find("AA_" + msg.objectID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<charArbeitsmarkt>().RemoveFromArbeitsmarkt(msg.eingestellt);
		}
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x000FE1A0 File Offset: 0x000FC3A0
	public void CLIENT_Send_Command(int command)
	{
		Debug.Log("CLIENT_Send_Command() " + command);
		this.FindScripts();
		NetworkClient.Send<mpCalls.c_Command>(new mpCalls.c_Command
		{
			playerID = this.mS_.myID,
			command = command
		}, 0);
	}

	// Token: 0x06001963 RID: 6499 RVA: 0x000FE1F4 File Offset: 0x000FC3F4
	public void CLIENT_Get_Command(NetworkConnection conn, mpCalls.c_Command msg)
	{
		Debug.Log("CLIENT_Get_Command");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		switch (msg.command)
		{
		case 1:
			player_mp.playerReady = true;
			return;
		case 2:
			player_mp.playerPause = true;
			return;
		case 3:
			player_mp.playerPause = false;
			return;
		default:
			return;
		}
	}

	// Token: 0x06001964 RID: 6500 RVA: 0x000FE250 File Offset: 0x000FC450
	public void CLIENT_Send_PlayerInfos()
	{
		this.FindScripts();
		Debug.Log("CLIENT_Send_PlayerInfos");
		NetworkClient.Send<mpCalls.c_PlayerInfos>(new mpCalls.c_PlayerInfos
		{
			playerID = this.mS_.myID,
			playerName = this.mS_.playerName,
			ready = this.mpMain_.uiObjects[51].GetComponent<Toggle>().isOn
		}, 0);
	}

	// Token: 0x06001965 RID: 6501 RVA: 0x000FE2C0 File Offset: 0x000FC4C0
	public void CLIENT_Get_PlayerInfos(NetworkConnection conn, mpCalls.c_PlayerInfos msg)
	{
		Debug.Log("CLIENT_Get_PlayerInfos");
		if (this.save_.loadingSavegame)
		{
			return;
		}
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.playerName = msg.playerName;
		player_mp.ready = msg.ready;
		player_mp.myPubScript_;
		this.SERVER_Send_PlayerInfos();
	}

	// Token: 0x06001966 RID: 6502 RVA: 0x000FE320 File Offset: 0x000FC520
	public void SERVER_Send_PlayerLeave(int playerID_)
	{
		Debug.Log("SERVER_Send_PlayerLeave()");
		NetworkServer.SendToAll<mpCalls.s_PlayerLeave>(new mpCalls.s_PlayerLeave
		{
			playerID = playerID_
		}, 0, false);
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x000FE350 File Offset: 0x000FC550
	public void SERVER_Get_PlayerLeave(NetworkConnection conn, mpCalls.s_PlayerLeave msg)
	{
		Debug.Log("SERVER_Get_PlayerLeave()");
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			GameObject gameObject = GameObject.Find("PUB_" + msg.playerID.ToString());
			if (gameObject)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			if (this.playersMP[i].playerID == msg.playerID)
			{
				this.playersMP.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x000FE3D0 File Offset: 0x000FC5D0
	public void SERVER_Send_PlatformData(platformScript script_)
	{
		Debug.Log("SERVER_Send_PlatformData()");
		NetworkServer.SendToAll<mpCalls.s_PlatformData>(new mpCalls.s_PlatformData
		{
			platformID = script_.myID,
			marktanteil = script_.marktanteil,
			units = script_.units,
			units_max = script_.units_max,
			date_year_end = script_.date_year_end
		}, 0, false);
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x000FE438 File Offset: 0x000FC638
	public void SERVER_Get_PlatformData(NetworkConnection conn, mpCalls.s_PlatformData msg)
	{
		Debug.Log("SERVER_Get_PlatformData()");
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component.myID == msg.platformID)
				{
					component.marktanteil = msg.marktanteil;
					component.units = msg.units;
					component.units_max = msg.units_max;
					component.date_year_end = msg.date_year_end;
					return;
				}
			}
		}
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x000FE4BC File Offset: 0x000FC6BC
	public void SERVER_Send_ObjectDelete(int objectID_)
	{
		Debug.Log("SERVER_Send_ObjectDelete()");
		NetworkServer.SendToAll<mpCalls.s_ObjectDelete>(new mpCalls.s_ObjectDelete
		{
			playerID = this.mS_.myID,
			objectID = objectID_
		}, 0, false);
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x000FE500 File Offset: 0x000FC700
	public void SERVER_Get_ObjectDelete(NetworkConnection conn, mpCalls.s_ObjectDelete msg)
	{
		Debug.Log("SERVER_Get_ObjectDelete()");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		for (int i = 0; i < player_mp.objects.Count; i++)
		{
			if (player_mp.objects[i].id == msg.objectID)
			{
				player_mp.objects.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x000FE564 File Offset: 0x000FC764
	public void SERVER_Send_Object(int id_, int typ_, float x_, float y_, float rot_)
	{
		Debug.Log("SERVER_Send_Object()");
		NetworkServer.SendToAll<mpCalls.s_Object>(new mpCalls.s_Object
		{
			playerID = this.mS_.myID,
			objectID = id_,
			typ = typ_,
			x = x_,
			y = y_,
			rot = rot_
		}, 0, false);
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x000FE5C8 File Offset: 0x000FC7C8
	public void SERVER_Get_Object(NetworkConnection conn, mpCalls.s_Object msg)
	{
		Debug.Log("SERVER_Get_Object()");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.objects.Add(new object_mp(msg.objectID, msg.typ, msg.x, msg.y, msg.rot));
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x000FE620 File Offset: 0x000FC820
	public void SERVER_Send_Map(int posx, int posy)
	{
		Debug.Log("SERVER_Send_Map()");
		int typ = 0;
		if (this.mapScript_.mapRoomScript[posx, posy])
		{
			typ = this.mapScript_.mapRoomScript[posx, posy].typ;
		}
		NetworkServer.SendToAll<mpCalls.s_Map>(new mpCalls.s_Map
		{
			playerID = this.mS_.myID,
			x = (byte)posx,
			y = (byte)posy,
			id = this.mapScript_.mapRoomID[posx, posy],
			typ = typ,
			door = this.mapScript_.mapDoors[posx, posy],
			window = (byte)this.mapScript_.mapWindows[posx, posy]
		}, 0, false);
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x000FE6F4 File Offset: 0x000FC8F4
	public void SERVER_Get_Map(NetworkConnection conn, mpCalls.s_Map msg)
	{
		Debug.Log("SERVER_Get_Map()");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.mapRoomID[(int)msg.x, (int)msg.y] = msg.id;
		player_mp.mapRoomTyp[(int)msg.x, (int)msg.y] = msg.typ;
		player_mp.mapDoors[(int)msg.x, (int)msg.y] = msg.door;
		player_mp.mapWindows[(int)msg.x, (int)msg.y] = (int)msg.window;
	}

	// Token: 0x06001970 RID: 6512 RVA: 0x000FE790 File Offset: 0x000FC990
	public void SERVER_Send_GlobalEvent(int eventID, int wochen)
	{
		Debug.Log("SERVER_Send_GlobalEvent()");
		NetworkServer.SendToAll<mpCalls.s_GlobalEvent>(new mpCalls.s_GlobalEvent
		{
			eventID = eventID,
			wochen = wochen
		}, 0, false);
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x000FE7C8 File Offset: 0x000FC9C8
	public void SERVER_Get_GlobalEvent(NetworkConnection conn, mpCalls.s_GlobalEvent msg)
	{
		Debug.Log("SERVER_Get_GlobalEvent()");
		this.FindScripts();
		this.mS_.SetGlobalEvent(msg.eventID);
		this.mS_.globalEventWeeks = msg.wochen;
		base.StartCoroutine(this.iSERVER_Get_GlobalEvent(msg.eventID, msg.wochen));
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x000FE820 File Offset: 0x000FCA20
	private IEnumerator iSERVER_Get_GlobalEvent(int eventID, int wochen)
	{
		bool done = false;
		while (!done)
		{
			if (!this.guiMain_.menuOpen)
			{
				done = true;
				if (this.guiMain_)
				{
					this.guiMain_.uiObjects[216].SetActive(true);
					this.guiMain_.uiObjects[216].GetComponent<Menu_RandomEventGlobal>().Init(eventID);
				}
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x000FE838 File Offset: 0x000FCA38
	public void SERVER_Send_EngineAbrechnung(int toPlayer, int gameID)
	{
		Debug.Log("SERVER_Send_EngineAbrechnung()");
		NetworkServer.SendToAll<mpCalls.s_EngineAbrechnung>(new mpCalls.s_EngineAbrechnung
		{
			toPlayerID = toPlayer,
			gameID = gameID
		}, 0, false);
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x000FE870 File Offset: 0x000FCA70
	public void SERVER_Get_EngineAbrechnung(NetworkConnection conn, mpCalls.s_EngineAbrechnung msg)
	{
		Debug.Log("SERVER_Get_EngineAbrechnung()");
		if (msg.toPlayerID != this.mS_.myID)
		{
			return;
		}
		if (this.guiMain_)
		{
			GameObject game = this.GetGame(msg.gameID);
			if (game)
			{
				this.guiMain_.OpenEngineAbrechnung(game.GetComponent<gameScript>());
			}
		}
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x000FE8D0 File Offset: 0x000FCAD0
	public void SERVER_Send_Award(int bestGrafik_, int bestSound_, int bestStudio_, int bestPublisher_, int bestGame_, int badGame_)
	{
		Debug.Log("SERVER_Send_Award()");
		NetworkServer.SendToAll<mpCalls.s_Awards>(new mpCalls.s_Awards
		{
			bestGrafik = bestGrafik_,
			bestSound = bestSound_,
			bestStudio = bestStudio_,
			bestPublisher = bestPublisher_,
			bestGame = bestGame_,
			badGame = badGame_
		}, 0, false);
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x000FE92C File Offset: 0x000FCB2C
	public void SERVER_Get_Awards(NetworkConnection conn, mpCalls.s_Awards msg)
	{
		Debug.Log("SERVER_Get_Awards()");
		Menu_Awards component = this.guiMain_.uiObjects[143].GetComponent<Menu_Awards>();
		component.gameObject.SetActive(true);
		component.Multiplayer_FindWinners(msg.bestGrafik, msg.bestSound, msg.bestStudio, msg.bestPublisher, msg.bestGame, msg.badGame);
		this.mS_.MadGamesAward(true);
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x000FE99C File Offset: 0x000FCB9C
	public void SERVER_Send_Payment(int playerID_, int toPlayerID_, int what, int money)
	{
		Debug.Log("SERVER_Send_Payment()");
		NetworkServer.SendToAll<mpCalls.s_Payment>(new mpCalls.s_Payment
		{
			playerID = playerID_,
			toPlayerID = toPlayerID_,
			what = what,
			money = money
		}, 0, false);
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x000FE9E4 File Offset: 0x000FCBE4
	public void SERVER_Get_Payment(NetworkConnection conn, mpCalls.s_Payment msg)
	{
		Debug.Log("SERVER_Get_Payment()");
		if (msg.toPlayerID != this.mS_.myID)
		{
			return;
		}
		switch (msg.what)
		{
		case 0:
		{
			string text = this.tS_.GetText(1044);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.money, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.money, 4);
			return;
		}
		case 1:
		{
			string text = this.tS_.GetText(1045);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.money, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.money, 4);
			return;
		}
		case 2:
		{
			string text = this.tS_.GetText(1658);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.money, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.money, 10);
			return;
		}
		case 3:
			this.mS_.Earn((long)msg.money, 4);
			return;
		case 4:
			this.mS_.Earn((long)msg.money, 10);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001979 RID: 6521 RVA: 0x000FEB90 File Offset: 0x000FCD90
	public void SERVER_Send_GenreCombination(int genre_)
	{
		Debug.Log("SERVER_Send_GenreCombination()");
		bool[] array = new bool[this.genres_.genres_LEVEL.Length];
		for (int i = 0; i < this.genres_.genres_LEVEL.Length; i++)
		{
			array[i] = this.genres_.genres_COMBINATION[genre_, i];
		}
		NetworkServer.SendToAll<mpCalls.s_GenreCombination>(new mpCalls.s_GenreCombination
		{
			genreSlot = genre_,
			genres_COMBINATION = array
		}, 0, false);
	}

	// Token: 0x0600197A RID: 6522 RVA: 0x000FEC08 File Offset: 0x000FCE08
	public void SERVER_Get_GenreCombination(NetworkConnection conn, mpCalls.s_GenreCombination msg)
	{
		Debug.Log("SERVER_Get_GenreCombination()");
		for (int i = 0; i < msg.genres_COMBINATION.Length; i++)
		{
			this.genres_.genres_COMBINATION[msg.genreSlot, i] = msg.genres_COMBINATION[i];
			if (msg.genreSlot == 0)
			{
				Debug.Log("G1: " + this.genres_.genres_COMBINATION[msg.genreSlot, i].ToString());
			}
		}
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x000FEC84 File Offset: 0x000FCE84
	public void SERVER_Send_GenreDesign(int genre_, int f0, int f1, int f2, int f3, int f4, int f5, int f6, int f7, int a0, int a1, int a2)
	{
		Debug.Log("SERVER_Send_GenreDesign()");
		NetworkServer.SendToAll<mpCalls.s_GenreDesign>(new mpCalls.s_GenreDesign
		{
			genreSlot = genre_,
			genres_focus0 = f0,
			genres_focus1 = f1,
			genres_focus2 = f2,
			genres_focus3 = f3,
			genres_focus4 = f4,
			genres_focus5 = f5,
			genres_focus6 = f6,
			genres_focus7 = f7,
			genres_align0 = a0,
			genres_align1 = a1,
			genres_align2 = a2
		}, 0, false);
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x000FED14 File Offset: 0x000FCF14
	public void SERVER_Get_GenreDesign(NetworkConnection conn, mpCalls.s_GenreDesign msg)
	{
		Debug.Log("SERVER_Get_GenreDesign()");
		this.genres_.genres_FOCUS[msg.genreSlot, 0] = msg.genres_focus0;
		this.genres_.genres_FOCUS[msg.genreSlot, 1] = msg.genres_focus1;
		this.genres_.genres_FOCUS[msg.genreSlot, 2] = msg.genres_focus2;
		this.genres_.genres_FOCUS[msg.genreSlot, 3] = msg.genres_focus3;
		this.genres_.genres_FOCUS[msg.genreSlot, 4] = msg.genres_focus4;
		this.genres_.genres_FOCUS[msg.genreSlot, 5] = msg.genres_focus5;
		this.genres_.genres_FOCUS[msg.genreSlot, 6] = msg.genres_focus6;
		this.genres_.genres_FOCUS[msg.genreSlot, 7] = msg.genres_focus7;
		this.genres_.genres_ALIGN[msg.genreSlot, 0] = msg.genres_align0;
		this.genres_.genres_ALIGN[msg.genreSlot, 1] = msg.genres_align1;
		this.genres_.genres_ALIGN[msg.genreSlot, 2] = msg.genres_align2;
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x000FEE6C File Offset: 0x000FD06C
	public void SERVER_Send_Forschung(int playerID_)
	{
		Debug.Log("SERVER_Send_Forschung()");
		bool flag = false;
		if (playerID_ == this.mS_.myID)
		{
			player_mp player_mp = this.FindPlayer(this.mS_.myID);
			if (player_mp == null)
			{
				return;
			}
			if (player_mp.forschungSonstiges.Length != this.fS_.RES_POINTS.Length)
			{
				player_mp.forschungSonstiges = new bool[this.fS_.RES_POINTS.Length];
			}
			if (player_mp.genres.Length != this.genres_.genres_UNLOCK.Length)
			{
				player_mp.genres = new bool[this.genres_.genres_UNLOCK.Length];
			}
			if (player_mp.themes.Length != this.themes_.themes_RES_POINTS_LEFT.Length)
			{
				player_mp.themes = new bool[this.themes_.themes_RES_POINTS_LEFT.Length];
			}
			if (player_mp.engineFeatures.Length != this.eF_.engineFeatures_RES_POINTS.Length)
			{
				player_mp.engineFeatures = new bool[this.eF_.engineFeatures_RES_POINTS.Length];
			}
			if (player_mp.gameplayFeatures.Length != this.gF_.gameplayFeatures_RES_POINTS.Length)
			{
				player_mp.gameplayFeatures = new bool[this.gF_.gameplayFeatures_RES_POINTS.Length];
			}
			if (player_mp.hardware.Length != this.hardware_.hardware_RES_POINTS.Length)
			{
				player_mp.hardware = new bool[this.hardware_.hardware_RES_POINTS.Length];
			}
			if (player_mp.hardwareFeatures.Length != this.hardwareFeatures_.hardFeat_RES_POINTS.Length)
			{
				player_mp.hardwareFeatures = new bool[this.hardwareFeatures_.hardFeat_RES_POINTS.Length];
			}
			for (int i = 0; i < player_mp.forschungSonstiges.Length; i++)
			{
				if (this.fS_.RES_POINTS_LEFT[i] <= 0f && !player_mp.forschungSonstiges[i])
				{
					player_mp.forschungSonstiges[i] = true;
					flag = true;
				}
			}
			for (int j = 0; j < player_mp.genres.Length; j++)
			{
				if (this.genres_.genres_RES_POINTS_LEFT[j] <= 0f && !player_mp.genres[j])
				{
					player_mp.genres[j] = true;
					flag = true;
				}
			}
			for (int k = 0; k < player_mp.themes.Length; k++)
			{
				if (this.themes_.themes_RES_POINTS_LEFT[k] <= 0f && !player_mp.themes[k])
				{
					player_mp.themes[k] = true;
					flag = true;
				}
			}
			for (int l = 0; l < player_mp.engineFeatures.Length; l++)
			{
				if (this.eF_.engineFeatures_RES_POINTS_LEFT[l] <= 0f && !player_mp.engineFeatures[l])
				{
					player_mp.engineFeatures[l] = true;
					flag = true;
				}
			}
			for (int m = 0; m < player_mp.gameplayFeatures.Length; m++)
			{
				if (this.gF_.gameplayFeatures_RES_POINTS_LEFT[m] <= 0f && !player_mp.gameplayFeatures[m])
				{
					player_mp.gameplayFeatures[m] = true;
					flag = true;
				}
			}
			for (int n = 0; n < player_mp.hardware.Length; n++)
			{
				if (this.hardware_.hardware_RES_POINTS_LEFT[n] <= 0f && !player_mp.hardware[n])
				{
					player_mp.hardware[n] = true;
					flag = true;
				}
			}
			for (int num = 0; num < player_mp.hardwareFeatures.Length; num++)
			{
				if (this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[num] <= 0f && !player_mp.hardwareFeatures[num])
				{
					player_mp.hardwareFeatures[num] = true;
					flag = true;
				}
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			player_mp player_mp = this.FindPlayer(playerID_);
			if (player_mp == null)
			{
				return;
			}
			NetworkServer.SendToAll<mpCalls.s_Forschung>(new mpCalls.s_Forschung
			{
				playerID = playerID_,
				forschungSonstiges = (bool[])player_mp.forschungSonstiges.Clone(),
				genres = (bool[])player_mp.genres.Clone(),
				themes = (bool[])player_mp.themes.Clone(),
				engineFeatures = (bool[])player_mp.engineFeatures.Clone(),
				gameplayFeatures = (bool[])player_mp.gameplayFeatures.Clone(),
				hardware = (bool[])player_mp.hardware.Clone(),
				hardwareFeatures = (bool[])player_mp.hardwareFeatures.Clone()
			}, 0, false);
		}
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x000FF28C File Offset: 0x000FD48C
	public void SERVER_Get_Forschung(NetworkConnection conn, mpCalls.s_Forschung msg)
	{
		Debug.Log("SERVER_Get_Forschung()");
		if (msg.playerID == this.mS_.myID)
		{
			return;
		}
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.forschungSonstiges = (bool[])msg.forschungSonstiges.Clone();
		player_mp.genres = (bool[])msg.genres.Clone();
		player_mp.themes = (bool[])msg.themes.Clone();
		player_mp.engineFeatures = (bool[])msg.engineFeatures.Clone();
		player_mp.gameplayFeatures = (bool[])msg.gameplayFeatures.Clone();
		player_mp.hardware = (bool[])msg.hardware.Clone();
		player_mp.hardwareFeatures = (bool[])msg.hardwareFeatures.Clone();
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x000FF364 File Offset: 0x000FD564
	public void SERVER_Send_GenreBeliebtheit()
	{
		Debug.Log("SERVER_Send_GenreBeliebtheit()");
		NetworkServer.SendToAll<mpCalls.s_GenreBeliebtheit>(new mpCalls.s_GenreBeliebtheit
		{
			genreBeliebtheit = (float[])this.genres_.genres_BELIEBTHEIT.Clone()
		}, 0, false);
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000FF3A7 File Offset: 0x000FD5A7
	public void SERVER_Get_GenreBeliebtheit(NetworkConnection conn, mpCalls.s_GenreBeliebtheit msg)
	{
		Debug.Log("SERVER_GenreBeliebtheit()");
		this.genres_.genres_BELIEBTHEIT = (float[])msg.genreBeliebtheit.Clone();
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x000FF3D0 File Offset: 0x000FD5D0
	public void SERVER_Send_Help(int playerID_, int toPlayerID_, int what, int valueA, int valueB, int valueC)
	{
		Debug.Log("SERVER_Send_Help()");
		NetworkServer.SendToAll<mpCalls.s_Help>(new mpCalls.s_Help
		{
			playerID = playerID_,
			toPlayerID = toPlayerID_,
			what = what,
			valueA = valueA,
			valueB = valueB,
			valueC = valueC
		}, 0, false);
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x000FF42C File Offset: 0x000FD62C
	public void SERVER_Get_Help(NetworkConnection conn, mpCalls.s_Help msg)
	{
		Debug.Log("SERVER_Get_Help()");
		if (msg.toPlayerID != this.mS_.myID)
		{
			return;
		}
		switch (msg.what)
		{
		case 0:
		{
			string text = this.tS_.GetText(1327);
			text = text.Replace("<NUM>", this.mS_.GetMoney((long)msg.valueA, true));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.mS_.Earn((long)msg.valueA, 1);
			return;
		}
		case 1:
		{
			GameObject gameObject = GameObject.Find("ENGINE_" + msg.valueA.ToString());
			if (gameObject)
			{
				engineScript component = gameObject.GetComponent<engineScript>();
				if (component && !component.gekauft)
				{
					component.gekauft = true;
					string text = this.tS_.GetText(1330);
					text = text.Replace("<NAME>", component.GetName());
					this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
					return;
				}
			}
			break;
		}
		case 2:
		{
			string text = this.tS_.GetText(1332);
			text = text.Replace("<NAME>", this.licences_.GetName(msg.valueA));
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			this.licences_.licence_GEKAUFT[msg.valueA] += msg.valueB;
			return;
		}
		case 3:
		{
			string text = this.tS_.GetText(1339);
			switch (msg.valueB)
			{
			case 0:
				text = text.Replace("<NAME>", this.genres_.GetName(msg.valueA));
				this.genres_.genres_RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			case 1:
				text = text.Replace("<NAME>", this.tS_.GetThemes(msg.valueA));
				this.themes_.themes_RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			case 2:
				text = text.Replace("<NAME>", this.eF_.GetName(msg.valueA));
				this.eF_.engineFeatures_RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			case 3:
				text = text.Replace("<NAME>", this.gF_.GetName(msg.valueA));
				this.gF_.gameplayFeatures_RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			case 4:
				text = text.Replace("<NAME>", this.hardware_.GetName(msg.valueA));
				this.hardware_.hardware_RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			case 5:
				text = text.Replace("<NAME>", this.fS_.GetName(msg.valueA));
				this.fS_.RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			case 6:
				text = text.Replace("<NAME>", this.hardwareFeatures_.GetName(msg.valueA));
				this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT[msg.valueA] = 0f;
				break;
			}
			this.CLIENT_Send_Forschung();
			this.guiMain_.AddChat(msg.playerID, "<color=green>" + text + "</color>");
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x000FF7CC File Offset: 0x000FD9CC
	public void SERVER_Send_Platform(platformScript script_)
	{
		Debug.Log("SERVER_Send_Platform()");
		NetworkServer.SendToAll<mpCalls.s_Platform>(new mpCalls.s_Platform
		{
			myID = script_.myID,
			date_year = script_.date_year,
			date_month = script_.date_month,
			date_year_end = script_.date_year_end,
			date_month_end = script_.date_month_end,
			price = script_.price,
			dev_costs = script_.dev_costs,
			tech = script_.tech,
			typ = script_.typ,
			marktanteil = script_.marktanteil,
			needFeatures = (int[])script_.needFeatures.Clone(),
			units = script_.units,
			units_max = script_.units_max,
			name_EN = script_.name_EN,
			name_GE = script_.name_GE,
			name_TU = script_.name_TU,
			name_CH = script_.name_CH,
			name_FR = script_.name_FR,
			name_HU = script_.name_HU,
			name_JA = script_.name_JA,
			manufacturer_EN = script_.manufacturer_EN,
			manufacturer_GE = script_.manufacturer_GE,
			manufacturer_TU = script_.manufacturer_TU,
			manufacturer_CH = script_.manufacturer_CH,
			manufacturer_FR = script_.manufacturer_FR,
			manufacturer_HU = script_.manufacturer_HU,
			manufacturer_JA = script_.manufacturer_JA,
			pic1_file = script_.pic1_file,
			pic2_file = script_.pic2_file,
			pic2_year = script_.pic2_year,
			games = script_.games,
			exklusivGames = script_.exklusivGames,
			isUnlocked = script_.isUnlocked,
			vomMarktGenommen = script_.vomMarktGenommen,
			complex = script_.complex,
			internet = script_.internet,
			powerFromMarket = script_.powerFromMarket,
			myName = script_.myName,
			ownerID = script_.ownerID,
			gameID = script_.gameID,
			anzController = script_.anzController,
			conHueShift = script_.conHueShift,
			conSaturation = script_.conSaturation,
			component_cpu = script_.component_cpu,
			component_gfx = script_.component_gfx,
			component_ram = script_.component_ram,
			component_hdd = script_.component_hdd,
			component_sfx = script_.component_sfx,
			component_cooling = script_.component_cooling,
			component_disc = script_.component_disc,
			component_controller = script_.component_controller,
			component_case = script_.component_case,
			component_monitor = script_.component_monitor,
			hwFeatures = (bool[])script_.hwFeatures.Clone(),
			entwicklungsKosten = script_.entwicklungsKosten,
			einnahmen = script_.einnahmen,
			hype = script_.hype,
			costs_marketing = script_.costs_marketing,
			costs_mitarbeiter = script_.costs_mitarbeiter,
			startProduktionskosten = script_.startProduktionskosten,
			verkaufspreis = script_.verkaufspreis,
			kostenreduktion = script_.kostenreduktion,
			autoPreis = script_.autoPreis,
			thridPartyGames = script_.thridPartyGames,
			umsatzTotal = script_.umsatzTotal,
			costs_production = script_.costs_production,
			sellsPerWeek = (int[])script_.sellsPerWeek.Clone(),
			weeksOnMarket = script_.weeksOnMarket,
			review = script_.review,
			performancePoints = script_.performancePoints
		}, 0, false);
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x000FFBA0 File Offset: 0x000FDDA0
	public void SERVER_Get_Platform(NetworkConnection conn, mpCalls.s_Platform msg)
	{
		Debug.Log("SERVER_Get_Platform()");
		GameObject gameObject = GameObject.Find("PLATFORM_" + msg.myID.ToString());
		if (!gameObject)
		{
			platformScript platformScript = this.platforms_.CreatePlatform();
			platformScript.myID = msg.myID;
			platformScript.date_year = msg.date_year;
			platformScript.date_month = msg.date_month;
			platformScript.date_year_end = msg.date_year_end;
			platformScript.date_month_end = msg.date_month_end;
			platformScript.price = msg.price;
			platformScript.dev_costs = msg.dev_costs;
			platformScript.tech = msg.tech;
			platformScript.typ = msg.typ;
			platformScript.marktanteil = msg.marktanteil;
			platformScript.needFeatures = (int[])msg.needFeatures.Clone();
			platformScript.units = msg.units;
			platformScript.units_max = msg.units_max;
			platformScript.name_EN = msg.name_EN;
			platformScript.name_GE = msg.name_GE;
			platformScript.name_TU = msg.name_TU;
			platformScript.name_CH = msg.name_CH;
			platformScript.name_FR = msg.name_FR;
			platformScript.name_HU = msg.name_HU;
			platformScript.name_JA = msg.name_JA;
			platformScript.manufacturer_EN = msg.manufacturer_EN;
			platformScript.manufacturer_GE = msg.manufacturer_GE;
			platformScript.manufacturer_TU = msg.manufacturer_TU;
			platformScript.manufacturer_CH = msg.manufacturer_CH;
			platformScript.manufacturer_FR = msg.manufacturer_FR;
			platformScript.manufacturer_HU = msg.manufacturer_HU;
			platformScript.manufacturer_JA = msg.manufacturer_JA;
			platformScript.pic1_file = msg.pic1_file;
			platformScript.pic2_file = msg.pic2_file;
			platformScript.pic2_year = msg.pic2_year;
			platformScript.games = msg.games;
			platformScript.exklusivGames = msg.exklusivGames;
			platformScript.isUnlocked = msg.isUnlocked;
			platformScript.vomMarktGenommen = msg.vomMarktGenommen;
			platformScript.complex = msg.complex;
			platformScript.internet = msg.internet;
			platformScript.powerFromMarket = msg.powerFromMarket;
			platformScript.myName = msg.myName;
			platformScript.ownerID = msg.ownerID;
			platformScript.gameID = msg.gameID;
			platformScript.anzController = msg.anzController;
			platformScript.conHueShift = msg.conHueShift;
			platformScript.conSaturation = msg.conSaturation;
			platformScript.component_cpu = msg.component_cpu;
			platformScript.component_gfx = msg.component_gfx;
			platformScript.component_ram = msg.component_ram;
			platformScript.component_hdd = msg.component_hdd;
			platformScript.component_sfx = msg.component_sfx;
			platformScript.component_cooling = msg.component_cooling;
			platformScript.component_disc = msg.component_disc;
			platformScript.component_controller = msg.component_controller;
			platformScript.component_case = msg.component_case;
			platformScript.component_monitor = msg.component_monitor;
			platformScript.hwFeatures = (bool[])msg.hwFeatures.Clone();
			platformScript.entwicklungsKosten = msg.entwicklungsKosten;
			platformScript.einnahmen = msg.einnahmen;
			platformScript.hype = msg.hype;
			platformScript.costs_marketing = msg.costs_marketing;
			platformScript.costs_mitarbeiter = msg.costs_mitarbeiter;
			platformScript.startProduktionskosten = msg.startProduktionskosten;
			platformScript.verkaufspreis = msg.verkaufspreis;
			platformScript.kostenreduktion = msg.kostenreduktion;
			platformScript.autoPreis = msg.autoPreis;
			platformScript.thridPartyGames = msg.thridPartyGames;
			platformScript.umsatzTotal = msg.umsatzTotal;
			platformScript.costs_production = msg.costs_production;
			platformScript.weeksOnMarket = msg.weeksOnMarket;
			platformScript.review = msg.review;
			platformScript.performancePoints = msg.performancePoints;
			platformScript.Init();
			if (platformScript.date_year == 1976 && platformScript.date_month == 1)
			{
				platformScript.isUnlocked = true;
				platformScript.inBesitz = true;
			}
			if (!platformScript.OwnerIsNPC() && !platformScript.vomMarktGenommen)
			{
				this.guiMain_.CreateTopNewsPlatform(platformScript);
				string text = this.tS_.GetText(1629);
				text = text.Replace("<NAME>", msg.myName);
				this.guiMain_.AddChat(msg.ownerID, text);
			}
			if (!platformScript.OwnerIsNPC() && platformScript.vomMarktGenommen)
			{
				this.guiMain_.CreateTopNewsPlatformRemove(platformScript);
			}
			return;
		}
		platformScript component = gameObject.GetComponent<platformScript>();
		if (component.ownerID == this.mS_.myID)
		{
			return;
		}
		component.myID = msg.myID;
		component.date_year = msg.date_year;
		component.date_month = msg.date_month;
		component.date_year_end = msg.date_year_end;
		component.date_month_end = msg.date_month_end;
		component.price = msg.price;
		component.dev_costs = msg.dev_costs;
		component.tech = msg.tech;
		component.typ = msg.typ;
		component.marktanteil = msg.marktanteil;
		component.needFeatures = (int[])msg.needFeatures.Clone();
		component.units = msg.units;
		component.units_max = msg.units_max;
		component.name_EN = msg.name_EN;
		component.name_GE = msg.name_GE;
		component.name_TU = msg.name_TU;
		component.name_CH = msg.name_CH;
		component.name_FR = msg.name_FR;
		component.name_HU = msg.name_HU;
		component.name_JA = msg.name_JA;
		component.manufacturer_EN = msg.manufacturer_EN;
		component.manufacturer_GE = msg.manufacturer_GE;
		component.manufacturer_TU = msg.manufacturer_TU;
		component.manufacturer_CH = msg.manufacturer_CH;
		component.manufacturer_FR = msg.manufacturer_FR;
		component.manufacturer_HU = msg.manufacturer_HU;
		component.manufacturer_JA = msg.manufacturer_JA;
		component.pic1_file = msg.pic1_file;
		component.pic2_file = msg.pic2_file;
		component.pic2_year = msg.pic2_year;
		component.games = msg.games;
		component.exklusivGames = msg.exklusivGames;
		component.isUnlocked = msg.isUnlocked;
		component.vomMarktGenommen = msg.vomMarktGenommen;
		component.complex = msg.complex;
		component.internet = msg.internet;
		component.powerFromMarket = msg.powerFromMarket;
		component.myName = msg.myName;
		component.ownerID = msg.ownerID;
		component.gameID = msg.gameID;
		component.anzController = msg.anzController;
		component.conHueShift = msg.conHueShift;
		component.conSaturation = msg.conSaturation;
		component.component_cpu = msg.component_cpu;
		component.component_gfx = msg.component_gfx;
		component.component_ram = msg.component_ram;
		component.component_hdd = msg.component_hdd;
		component.component_sfx = msg.component_sfx;
		component.component_cooling = msg.component_cooling;
		component.component_disc = msg.component_disc;
		component.component_controller = msg.component_controller;
		component.component_case = msg.component_case;
		component.component_monitor = msg.component_monitor;
		component.hwFeatures = (bool[])msg.hwFeatures.Clone();
		component.entwicklungsKosten = msg.entwicklungsKosten;
		component.einnahmen = msg.einnahmen;
		component.hype = msg.hype;
		component.costs_marketing = msg.costs_marketing;
		component.costs_mitarbeiter = msg.costs_mitarbeiter;
		component.startProduktionskosten = msg.startProduktionskosten;
		component.verkaufspreis = msg.verkaufspreis;
		component.kostenreduktion = msg.kostenreduktion;
		component.autoPreis = msg.autoPreis;
		component.thridPartyGames = msg.thridPartyGames;
		component.umsatzTotal = msg.umsatzTotal;
		component.costs_production = msg.costs_production;
		component.weeksOnMarket = msg.weeksOnMarket;
		component.review = msg.review;
		component.performancePoints = msg.performancePoints;
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00100340 File Offset: 0x000FE540
	public void SERVER_Send_Engine(engineScript script_)
	{
		Debug.Log("SERVER_Send_Engine()");
		NetworkServer.SendToAll<mpCalls.s_Engine>(new mpCalls.s_Engine
		{
			engineID = script_.myID,
			ownerID = script_.ownerID,
			isUnlocked = script_.isUnlocked,
			gekauft = script_.gekauft,
			myName = script_.myName,
			features = script_.features,
			spezialgenre = script_.spezialgenre,
			spezialplatform = script_.spezialplatform,
			sellEngine = script_.sellEngine,
			preis = script_.preis,
			gewinnbeteiligung = script_.gewinnbeteiligung
		}, 0, false);
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x001003F8 File Offset: 0x000FE5F8
	public void SERVER_Get_Engine(NetworkConnection conn, mpCalls.s_Engine msg)
	{
		Debug.Log("SERVER_Get_Engine()");
		GameObject gameObject = GameObject.Find("ENGINE_" + msg.engineID.ToString());
		if (!gameObject)
		{
			engineScript engineScript = this.eF_.CreateEngine();
			engineScript.myID = msg.engineID;
			engineScript.ownerID = msg.ownerID;
			engineScript.isUnlocked = msg.isUnlocked;
			if (msg.ownerID != -1)
			{
				engineScript.isUnlocked = true;
			}
			engineScript.gekauft = msg.gekauft;
			engineScript.myName = msg.myName;
			engineScript.features = (bool[])msg.features.Clone();
			engineScript.spezialgenre = msg.spezialgenre;
			engineScript.spezialplatform = msg.spezialplatform;
			engineScript.sellEngine = msg.sellEngine;
			engineScript.preis = msg.preis;
			engineScript.gewinnbeteiligung = msg.gewinnbeteiligung;
			engineScript.specialPlatformS_ = null;
			engineScript.Init();
			this.guiMain_.CreateTopNewsNpcEngine(engineScript.GetName());
			string text = this.tS_.GetText(1270);
			text = text.Replace("<NAME>", msg.myName);
			this.guiMain_.AddChat(msg.ownerID, text);
			return;
		}
		engineScript component = gameObject.GetComponent<engineScript>();
		if (component.ownerID == this.mS_.myID)
		{
			return;
		}
		component.myID = msg.engineID;
		component.ownerID = msg.ownerID;
		component.isUnlocked = msg.isUnlocked;
		if (msg.ownerID != -1)
		{
			component.isUnlocked = true;
		}
		component.gekauft = msg.gekauft;
		component.myName = msg.myName;
		component.features = (bool[])msg.features.Clone();
		component.spezialgenre = msg.spezialgenre;
		component.spezialplatform = msg.spezialplatform;
		component.sellEngine = msg.sellEngine;
		component.preis = msg.preis;
		component.gewinnbeteiligung = msg.gewinnbeteiligung;
		component.specialPlatformS_ = null;
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x001005F4 File Offset: 0x000FE7F4
	public void SERVER_Send_Chat(int playerID_, string c)
	{
		Debug.Log("SERVER_Send_Chat()");
		NetworkServer.SendToAll<mpCalls.s_Chat>(new mpCalls.s_Chat
		{
			playerID = playerID_,
			text = c
		}, 0, false);
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x0010062B File Offset: 0x000FE82B
	public void SERVER_Get_Chat(NetworkConnection conn, mpCalls.s_Chat msg)
	{
		Debug.Log("SERVER_Get_Chat()");
		this.guiMain_.AddChat(msg.playerID, msg.text);
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x00100650 File Offset: 0x000FE850
	public void SERVER_Send_ExklusivKonsolenSells(gameScript script_, long i)
	{
		Debug.Log("SERVER_Send_ExklusivKonsolenSells()");
		NetworkServer.SendToAll<mpCalls.s_exklusivKonsolenSells>(new mpCalls.s_exklusivKonsolenSells
		{
			gameID = script_.myID,
			exklusivKonsolenSells = i
		}, 0, false);
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x0010068C File Offset: 0x000FE88C
	public void SERVER_Get_ExklusivKonsolenSells(NetworkConnection conn, mpCalls.s_exklusivKonsolenSells msg)
	{
		Debug.Log("SERVER_Get_ExklusivKonsolenSells()");
		GameObject game = this.GetGame(msg.gameID);
		if (game)
		{
			gameScript component = game.GetComponent<gameScript>();
			if (component.ownerID == this.mS_.myID || component.publisherID == this.mS_.myID)
			{
				return;
			}
			component.myID = msg.gameID;
			component.exklusivKonsolenSells += msg.exklusivKonsolenSells;
		}
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x00100708 File Offset: 0x000FE908
	public void SERVER_Send_GameData(gameScript script_)
	{
		Debug.Log("SERVER_Send_GameData()");
		NetworkServer.SendToAll<mpCalls.s_GameData>(new mpCalls.s_GameData
		{
			gameID = script_.myID,
			sellsTotal = script_.sellsTotal,
			umsatzTotal = script_.umsatzTotal,
			isOnMarket = script_.isOnMarket,
			weeksOnMarket = script_.weeksOnMarket,
			userPositiv = script_.userPositiv,
			userNegativ = script_.userNegativ,
			costs_entwicklung = script_.costs_entwicklung,
			costs_mitarbeiter = script_.costs_mitarbeiter,
			costs_marketing = script_.costs_marketing,
			costs_enginegebuehren = script_.costs_enginegebuehren,
			costs_server = script_.costs_server,
			costs_production = script_.costs_production,
			costs_updates = script_.costs_updates,
			abonnements = script_.abonnements,
			abonnementsWoche = script_.abonnementsWoche,
			bestAbonnements = script_.bestAbonnements,
			bestChartPosition = script_.bestChartPosition,
			exklusivKonsolenSells = script_.exklusivKonsolenSells,
			ipPunkte = script_.ipPunkte,
			pubAngebot = script_.pubAngebot,
			pubAngebot_Weeks = script_.pubAngebot_Weeks,
			pubAngebot_Verhandlung = script_.pubAngebot_Verhandlung,
			pubAngebot_Retail = script_.pubAngebot_Retail,
			pubAngebot_Digital = script_.pubAngebot_Digital,
			pubAngebot_Garantiesumme = script_.pubAngebot_Garantiesumme,
			pubAngebot_Gewinnbeteiligung = script_.pubAngebot_Gewinnbeteiligung,
			auftragsspiel = script_.auftragsspiel,
			auftragsspiel_gehalt = script_.auftragsspiel_gehalt,
			auftragsspiel_bonus = script_.auftragsspiel_bonus,
			auftragsspiel_zeitInWochen = script_.auftragsspiel_zeitInWochen,
			auftragsspiel_wochenAlsAngebot = script_.auftragsspiel_wochenAlsAngebot,
			auftragsspiel_zeitAbgelaufen = script_.auftragsspiel_zeitAbgelaufen,
			auftragsspiel_mindestbewertung = script_.auftragsspiel_mindestbewertung,
			ipName = script_.ipName,
			lastChartPosition = script_.lastChartPosition,
			sellsPerWeek = (int[])script_.sellsPerWeek.Clone()
		}, 0, false);
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x0010091C File Offset: 0x000FEB1C
	public void SERVER_Get_GameData(NetworkConnection conn, mpCalls.s_GameData msg)
	{
		Debug.Log("SERVER_Get_GameData()");
		GameObject game = this.GetGame(msg.gameID);
		if (game)
		{
			gameScript component = game.GetComponent<gameScript>();
			if (component.ownerID == this.mS_.myID || component.publisherID == this.mS_.myID)
			{
				return;
			}
			component.myID = msg.gameID;
			component.sellsTotal = msg.sellsTotal;
			component.umsatzTotal = msg.umsatzTotal;
			component.isOnMarket = msg.isOnMarket;
			component.weeksOnMarket = msg.weeksOnMarket;
			component.userPositiv = msg.userPositiv;
			component.userNegativ = msg.userNegativ;
			component.costs_entwicklung = msg.costs_entwicklung;
			component.costs_mitarbeiter = msg.costs_mitarbeiter;
			component.costs_marketing = msg.costs_marketing;
			component.costs_enginegebuehren = msg.costs_enginegebuehren;
			component.costs_server = msg.costs_server;
			component.costs_production = msg.costs_production;
			component.costs_updates = msg.costs_updates;
			component.abonnements = msg.abonnements;
			component.abonnementsWoche = msg.abonnementsWoche;
			component.bestAbonnements = msg.bestAbonnements;
			component.bestChartPosition = msg.bestChartPosition;
			component.exklusivKonsolenSells = msg.exklusivKonsolenSells;
			component.ipPunkte = msg.ipPunkte;
			component.pubAngebot = msg.pubAngebot;
			component.pubAngebot_Weeks = msg.pubAngebot_Weeks;
			component.pubAngebot_Verhandlung = msg.pubAngebot_Verhandlung;
			component.pubAngebot_Retail = msg.pubAngebot_Retail;
			component.pubAngebot_Digital = msg.pubAngebot_Digital;
			component.pubAngebot_Garantiesumme = msg.pubAngebot_Garantiesumme;
			component.pubAngebot_Gewinnbeteiligung = msg.pubAngebot_Gewinnbeteiligung;
			component.auftragsspiel = msg.auftragsspiel;
			component.auftragsspiel_gehalt = msg.auftragsspiel_gehalt;
			component.auftragsspiel_bonus = msg.auftragsspiel_bonus;
			component.auftragsspiel_zeitInWochen = msg.auftragsspiel_zeitInWochen;
			component.auftragsspiel_wochenAlsAngebot = msg.auftragsspiel_wochenAlsAngebot;
			component.auftragsspiel_zeitAbgelaufen = msg.auftragsspiel_zeitAbgelaufen;
			component.auftragsspiel_mindestbewertung = msg.auftragsspiel_mindestbewertung;
			component.ipName = msg.ipName;
			component.lastChartPosition = msg.lastChartPosition;
			component.sellsPerWeek = (int[])msg.sellsPerWeek.Clone();
			this.games_.UpdateChartsWeek();
			this.guiMain_.UpdateCharts();
		}
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x00100B58 File Offset: 0x000FED58
	public void SERVER_Send_GameplayFeatures()
	{
		Debug.Log("SERVER_Send_GameplayFeatures()");
		NetworkServer.SendToAll<mpCalls.s_GameplayFeatures>(new mpCalls.s_GameplayFeatures
		{
			gameplayFeatures_TYP = (int[])this.gF_.gameplayFeatures_TYP.Clone(),
			gameplayFeatures_RES_POINTS = (int[])this.gF_.gameplayFeatures_RES_POINTS.Clone(),
			gameplayFeatures_RES_POINTS_LEFT = (float[])this.gF_.gameplayFeatures_RES_POINTS_LEFT.Clone(),
			gameplayFeatures_PRICE = (int[])this.gF_.gameplayFeatures_PRICE.Clone(),
			gameplayFeatures_DEV_COSTS = (int[])this.gF_.gameplayFeatures_DEV_COSTS.Clone(),
			gameplayFeatures_DATE_YEAR = (int[])this.gF_.gameplayFeatures_DATE_YEAR.Clone(),
			gameplayFeatures_DATE_MONTH = (int[])this.gF_.gameplayFeatures_DATE_MONTH.Clone(),
			gameplayFeatures_GAMEPLAY = (int[])this.gF_.gameplayFeatures_GAMEPLAY.Clone(),
			gameplayFeatures_GRAPHIC = (int[])this.gF_.gameplayFeatures_GRAPHIC.Clone(),
			gameplayFeatures_SOUND = (int[])this.gF_.gameplayFeatures_SOUND.Clone(),
			gameplayFeatures_TECHNIK = (int[])this.gF_.gameplayFeatures_TECHNIK.Clone(),
			gameplayFeatures_LEVEL = (int[])this.gF_.gameplayFeatures_LEVEL.Clone(),
			gameplayFeatures_UNLOCK = (bool[])this.gF_.gameplayFeatures_UNLOCK.Clone(),
			gameplayFeatures_ICONFILE = (string[])this.gF_.gameplayFeatures_ICONFILE.Clone(),
			gameplayFeatures_NAME_EN = (string[])this.gF_.gameplayFeatures_NAME_EN.Clone(),
			gameplayFeatures_NAME_GE = (string[])this.gF_.gameplayFeatures_NAME_GE.Clone(),
			gameplayFeatures_NAME_TU = (string[])this.gF_.gameplayFeatures_NAME_TU.Clone(),
			gameplayFeatures_NAME_CH = (string[])this.gF_.gameplayFeatures_NAME_CH.Clone(),
			gameplayFeatures_NAME_FR = (string[])this.gF_.gameplayFeatures_NAME_FR.Clone(),
			gameplayFeatures_NAME_PB = (string[])this.gF_.gameplayFeatures_NAME_PB.Clone(),
			gameplayFeatures_NAME_CT = (string[])this.gF_.gameplayFeatures_NAME_CT.Clone(),
			gameplayFeatures_NAME_HU = (string[])this.gF_.gameplayFeatures_NAME_HU.Clone(),
			gameplayFeatures_NAME_ES = (string[])this.gF_.gameplayFeatures_NAME_ES.Clone(),
			gameplayFeatures_NAME_CZ = (string[])this.gF_.gameplayFeatures_NAME_CZ.Clone(),
			gameplayFeatures_NAME_KO = (string[])this.gF_.gameplayFeatures_NAME_KO.Clone(),
			gameplayFeatures_NAME_RU = (string[])this.gF_.gameplayFeatures_NAME_RU.Clone(),
			gameplayFeatures_NAME_IT = (string[])this.gF_.gameplayFeatures_NAME_IT.Clone(),
			gameplayFeatures_NAME_AR = (string[])this.gF_.gameplayFeatures_NAME_AR.Clone(),
			gameplayFeatures_NAME_JA = (string[])this.gF_.gameplayFeatures_NAME_JA.Clone(),
			gameplayFeatures_NAME_PL = (string[])this.gF_.gameplayFeatures_NAME_PL.Clone(),
			gameplayFeatures_DESC_EN = (string[])this.gF_.gameplayFeatures_DESC_EN.Clone(),
			gameplayFeatures_DESC_GE = (string[])this.gF_.gameplayFeatures_DESC_GE.Clone(),
			gameplayFeatures_DESC_TU = (string[])this.gF_.gameplayFeatures_DESC_TU.Clone(),
			gameplayFeatures_DESC_CH = (string[])this.gF_.gameplayFeatures_DESC_CH.Clone(),
			gameplayFeatures_DESC_FR = (string[])this.gF_.gameplayFeatures_DESC_FR.Clone(),
			gameplayFeatures_DESC_PB = (string[])this.gF_.gameplayFeatures_DESC_PB.Clone(),
			gameplayFeatures_DESC_CT = (string[])this.gF_.gameplayFeatures_DESC_CT.Clone(),
			gameplayFeatures_DESC_HU = (string[])this.gF_.gameplayFeatures_DESC_HU.Clone(),
			gameplayFeatures_DESC_ES = (string[])this.gF_.gameplayFeatures_DESC_ES.Clone(),
			gameplayFeatures_DESC_CZ = (string[])this.gF_.gameplayFeatures_DESC_CZ.Clone(),
			gameplayFeatures_DESC_KO = (string[])this.gF_.gameplayFeatures_DESC_KO.Clone(),
			gameplayFeatures_DESC_RU = (string[])this.gF_.gameplayFeatures_DESC_RU.Clone(),
			gameplayFeatures_DESC_IT = (string[])this.gF_.gameplayFeatures_DESC_IT.Clone(),
			gameplayFeatures_DESC_AR = (string[])this.gF_.gameplayFeatures_DESC_AR.Clone(),
			gameplayFeatures_DESC_JA = (string[])this.gF_.gameplayFeatures_DESC_JA.Clone(),
			gameplayFeatures_DESC_PL = (string[])this.gF_.gameplayFeatures_DESC_PL.Clone(),
			gameplayFeatures_GOOD = (bool[])this.gF_.Return1DimensionArray_GOOD().Clone(),
			gameplayFeatures_BAD = (bool[])this.gF_.Return1DimensionArray_BAD().Clone(),
			gameplayFeatures_LOCKPLATFORM = (bool[])this.gF_.Return1DimensionArray_LOCKPLATFORM().Clone()
		}, 0, false);
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x001010DC File Offset: 0x000FF2DC
	public void SERVER_Get_GameplayFeatures(NetworkConnection conn, mpCalls.s_GameplayFeatures msg)
	{
		Debug.Log("SERVER_Get_GameplayFeatures()");
		this.gF_.gameplayFeatures_TYP = (int[])msg.gameplayFeatures_TYP.Clone();
		this.gF_.gameplayFeatures_RES_POINTS = (int[])msg.gameplayFeatures_RES_POINTS.Clone();
		this.gF_.gameplayFeatures_RES_POINTS_LEFT = (float[])msg.gameplayFeatures_RES_POINTS_LEFT.Clone();
		this.gF_.gameplayFeatures_PRICE = (int[])msg.gameplayFeatures_PRICE.Clone();
		this.gF_.gameplayFeatures_DEV_COSTS = (int[])msg.gameplayFeatures_DEV_COSTS.Clone();
		this.gF_.gameplayFeatures_DATE_YEAR = (int[])msg.gameplayFeatures_DATE_YEAR.Clone();
		this.gF_.gameplayFeatures_DATE_MONTH = (int[])msg.gameplayFeatures_DATE_MONTH.Clone();
		this.gF_.gameplayFeatures_GAMEPLAY = (int[])msg.gameplayFeatures_GAMEPLAY.Clone();
		this.gF_.gameplayFeatures_GRAPHIC = (int[])msg.gameplayFeatures_GRAPHIC.Clone();
		this.gF_.gameplayFeatures_SOUND = (int[])msg.gameplayFeatures_SOUND.Clone();
		this.gF_.gameplayFeatures_TECHNIK = (int[])msg.gameplayFeatures_TECHNIK.Clone();
		this.gF_.gameplayFeatures_LEVEL = (int[])msg.gameplayFeatures_LEVEL.Clone();
		this.gF_.gameplayFeatures_UNLOCK = (bool[])msg.gameplayFeatures_UNLOCK.Clone();
		this.gF_.gameplayFeatures_ICONFILE = (string[])msg.gameplayFeatures_ICONFILE.Clone();
		this.gF_.gameplayFeatures_NAME_EN = (string[])msg.gameplayFeatures_NAME_EN.Clone();
		this.gF_.gameplayFeatures_NAME_GE = (string[])msg.gameplayFeatures_NAME_GE.Clone();
		this.gF_.gameplayFeatures_NAME_TU = (string[])msg.gameplayFeatures_NAME_TU.Clone();
		this.gF_.gameplayFeatures_NAME_CH = (string[])msg.gameplayFeatures_NAME_CH.Clone();
		this.gF_.gameplayFeatures_NAME_FR = (string[])msg.gameplayFeatures_NAME_FR.Clone();
		this.gF_.gameplayFeatures_NAME_PB = (string[])msg.gameplayFeatures_NAME_PB.Clone();
		this.gF_.gameplayFeatures_NAME_CT = (string[])msg.gameplayFeatures_NAME_CT.Clone();
		this.gF_.gameplayFeatures_NAME_HU = (string[])msg.gameplayFeatures_NAME_HU.Clone();
		this.gF_.gameplayFeatures_NAME_ES = (string[])msg.gameplayFeatures_NAME_ES.Clone();
		this.gF_.gameplayFeatures_NAME_CZ = (string[])msg.gameplayFeatures_NAME_CZ.Clone();
		this.gF_.gameplayFeatures_NAME_KO = (string[])msg.gameplayFeatures_NAME_KO.Clone();
		this.gF_.gameplayFeatures_NAME_RU = (string[])msg.gameplayFeatures_NAME_RU.Clone();
		this.gF_.gameplayFeatures_NAME_IT = (string[])msg.gameplayFeatures_NAME_IT.Clone();
		this.gF_.gameplayFeatures_NAME_AR = (string[])msg.gameplayFeatures_NAME_AR.Clone();
		this.gF_.gameplayFeatures_NAME_JA = (string[])msg.gameplayFeatures_NAME_JA.Clone();
		this.gF_.gameplayFeatures_NAME_PL = (string[])msg.gameplayFeatures_NAME_PL.Clone();
		this.gF_.gameplayFeatures_DESC_EN = (string[])msg.gameplayFeatures_DESC_EN.Clone();
		this.gF_.gameplayFeatures_DESC_GE = (string[])msg.gameplayFeatures_DESC_GE.Clone();
		this.gF_.gameplayFeatures_DESC_TU = (string[])msg.gameplayFeatures_DESC_TU.Clone();
		this.gF_.gameplayFeatures_DESC_CH = (string[])msg.gameplayFeatures_DESC_CH.Clone();
		this.gF_.gameplayFeatures_DESC_FR = (string[])msg.gameplayFeatures_DESC_FR.Clone();
		this.gF_.gameplayFeatures_DESC_PB = (string[])msg.gameplayFeatures_DESC_PB.Clone();
		this.gF_.gameplayFeatures_DESC_CT = (string[])msg.gameplayFeatures_DESC_CT.Clone();
		this.gF_.gameplayFeatures_DESC_HU = (string[])msg.gameplayFeatures_DESC_HU.Clone();
		this.gF_.gameplayFeatures_DESC_ES = (string[])msg.gameplayFeatures_DESC_ES.Clone();
		this.gF_.gameplayFeatures_DESC_CZ = (string[])msg.gameplayFeatures_DESC_CZ.Clone();
		this.gF_.gameplayFeatures_DESC_KO = (string[])msg.gameplayFeatures_DESC_KO.Clone();
		this.gF_.gameplayFeatures_DESC_RU = (string[])msg.gameplayFeatures_DESC_RU.Clone();
		this.gF_.gameplayFeatures_DESC_IT = (string[])msg.gameplayFeatures_DESC_IT.Clone();
		this.gF_.gameplayFeatures_DESC_AR = (string[])msg.gameplayFeatures_DESC_AR.Clone();
		this.gF_.gameplayFeatures_DESC_JA = (string[])msg.gameplayFeatures_DESC_JA.Clone();
		this.gF_.gameplayFeatures_DESC_PL = (string[])msg.gameplayFeatures_DESC_PL.Clone();
		this.gF_.Copy2DimensionArray_GOOD(msg.gameplayFeatures_GOOD);
		this.gF_.Copy2DimensionArray_BAD(msg.gameplayFeatures_BAD);
		this.gF_.Copy2DimensionArray_LOCKPLATFORM(msg.gameplayFeatures_LOCKPLATFORM);
		this.gF_.Init();
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x0010160C File Offset: 0x000FF80C
	public void SERVER_Send_Genres(int id_, mpPlayer mpPlayer_)
	{
		Debug.Log("SERVER_Send_Genres()");
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return;
		}
		if (player_mp.playerID == this.mS_.myID)
		{
			Debug.Log("SERVER_Send_Genres(): Abbruch! Nicht zu Server senden.");
			return;
		}
		mpCalls.s_Genres msg = new mpCalls.s_Genres
		{
			genres_BELIEBTHEIT = (float[])this.genres_.genres_BELIEBTHEIT.Clone(),
			genres_BELIEBTHEIT_SOLL = (bool[])this.genres_.genres_BELIEBTHEIT_SOLL.Clone(),
			genres_RES_POINTS = (int[])this.genres_.genres_RES_POINTS.Clone(),
			genres_RES_POINTS_LEFT = (float[])this.genres_.genres_RES_POINTS_LEFT.Clone(),
			genres_PRICE = (int[])this.genres_.genres_PRICE.Clone(),
			genres_DEV_COSTS = (int[])this.genres_.genres_DEV_COSTS.Clone(),
			genres_DATE_YEAR = (int[])this.genres_.genres_DATE_YEAR.Clone(),
			genres_DATE_MONTH = (int[])this.genres_.genres_DATE_MONTH.Clone(),
			genres_LEVEL = (int[])this.genres_.genres_LEVEL.Clone(),
			genres_UNLOCK = (bool[])this.genres_.genres_UNLOCK.Clone(),
			genres_GAMEPLAY = (float[])this.genres_.genres_GAMEPLAY.Clone(),
			genres_GRAPHIC = (float[])this.genres_.genres_GRAPHIC.Clone(),
			genres_SOUND = (float[])this.genres_.genres_SOUND.Clone(),
			genres_CONTROL = (float[])this.genres_.genres_CONTROL.Clone(),
			genres_ICONFILE = (string[])this.genres_.genres_ICONFILE.Clone(),
			genres_NAME_EN = (string[])this.genres_.genres_NAME_EN.Clone(),
			genres_NAME_GE = (string[])this.genres_.genres_NAME_GE.Clone(),
			genres_NAME_TU = (string[])this.genres_.genres_NAME_TU.Clone(),
			genres_NAME_CH = (string[])this.genres_.genres_NAME_CH.Clone(),
			genres_NAME_FR = (string[])this.genres_.genres_NAME_FR.Clone(),
			genres_NAME_PB = (string[])this.genres_.genres_NAME_PB.Clone(),
			genres_NAME_HU = (string[])this.genres_.genres_NAME_HU.Clone(),
			genres_NAME_CT = (string[])this.genres_.genres_NAME_CT.Clone(),
			genres_NAME_ES = (string[])this.genres_.genres_NAME_ES.Clone(),
			genres_NAME_PL = (string[])this.genres_.genres_NAME_PL.Clone(),
			genres_NAME_CZ = (string[])this.genres_.genres_NAME_CZ.Clone(),
			genres_NAME_KO = (string[])this.genres_.genres_NAME_KO.Clone(),
			genres_NAME_IT = (string[])this.genres_.genres_NAME_IT.Clone(),
			genres_NAME_AR = (string[])this.genres_.genres_NAME_AR.Clone(),
			genres_NAME_JA = (string[])this.genres_.genres_NAME_JA.Clone(),
			genres_DESC_EN = (string[])this.genres_.genres_DESC_EN.Clone(),
			genres_DESC_GE = (string[])this.genres_.genres_DESC_GE.Clone(),
			genres_DESC_TU = (string[])this.genres_.genres_DESC_TU.Clone(),
			genres_DESC_CH = (string[])this.genres_.genres_DESC_CH.Clone(),
			genres_DESC_FR = (string[])this.genres_.genres_DESC_FR.Clone(),
			genres_DESC_PB = (string[])this.genres_.genres_DESC_PB.Clone(),
			genres_DESC_HU = (string[])this.genres_.genres_DESC_HU.Clone(),
			genres_DESC_CT = (string[])this.genres_.genres_DESC_CT.Clone(),
			genres_DESC_ES = (string[])this.genres_.genres_DESC_ES.Clone(),
			genres_DESC_PL = (string[])this.genres_.genres_DESC_PL.Clone(),
			genres_DESC_CZ = (string[])this.genres_.genres_DESC_CZ.Clone(),
			genres_DESC_KO = (string[])this.genres_.genres_DESC_KO.Clone(),
			genres_DESC_IT = (string[])this.genres_.genres_DESC_IT.Clone(),
			genres_DESC_AR = (string[])this.genres_.genres_DESC_AR.Clone(),
			genres_DESC_JA = (string[])this.genres_.genres_DESC_JA.Clone(),
			genres_FANS = (int[])this.genres_.genres_FANS.Clone(),
			genres_MARKT = (int[])this.genres_.genres_MARKT.Clone(),
			genres_TARGETGROUP = (bool[])this.genres_.Return1DimensionArray_TARGETGROUP().Clone(),
			genres_COMBINATION = (bool[])this.genres_.Return1DimensionArray_COMBINATION().Clone(),
			genres_FOCUS = (int[])this.genres_.Return1DimensionArray_FOCUS().Clone(),
			genres_ALIGN = (int[])this.genres_.Return1DimensionArray_ALIGN().Clone()
		};
		NetworkServer.SendToClientOfPlayer<mpCalls.s_Genres>(mpPlayer_.netIdentity, msg, 0);
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x00101BF8 File Offset: 0x000FFDF8
	public void SERVER_Get_Genres(NetworkConnection conn, mpCalls.s_Genres msg)
	{
		Debug.Log("SERVER_Get_Genres()");
		this.genres_.genres_BELIEBTHEIT = (float[])msg.genres_BELIEBTHEIT.Clone();
		this.genres_.genres_BELIEBTHEIT_SOLL = (bool[])msg.genres_BELIEBTHEIT_SOLL.Clone();
		this.genres_.genres_RES_POINTS = (int[])msg.genres_RES_POINTS.Clone();
		this.genres_.genres_RES_POINTS_LEFT = (float[])msg.genres_RES_POINTS_LEFT.Clone();
		this.genres_.genres_PRICE = (int[])msg.genres_PRICE.Clone();
		this.genres_.genres_DEV_COSTS = (int[])msg.genres_DEV_COSTS.Clone();
		this.genres_.genres_DATE_YEAR = (int[])msg.genres_DATE_YEAR.Clone();
		this.genres_.genres_DATE_MONTH = (int[])msg.genres_DATE_MONTH.Clone();
		this.genres_.genres_LEVEL = (int[])msg.genres_LEVEL.Clone();
		this.genres_.genres_UNLOCK = (bool[])msg.genres_UNLOCK.Clone();
		this.genres_.genres_GAMEPLAY = (float[])msg.genres_GAMEPLAY.Clone();
		this.genres_.genres_GRAPHIC = (float[])msg.genres_GRAPHIC.Clone();
		this.genres_.genres_SOUND = (float[])msg.genres_SOUND.Clone();
		this.genres_.genres_CONTROL = (float[])msg.genres_CONTROL.Clone();
		this.genres_.genres_ICONFILE = (string[])msg.genres_ICONFILE.Clone();
		this.genres_.genres_NAME_EN = (string[])msg.genres_NAME_EN.Clone();
		this.genres_.genres_NAME_GE = (string[])msg.genres_NAME_GE.Clone();
		this.genres_.genres_NAME_TU = (string[])msg.genres_NAME_TU.Clone();
		this.genres_.genres_NAME_CH = (string[])msg.genres_NAME_CH.Clone();
		this.genres_.genres_NAME_FR = (string[])msg.genres_NAME_FR.Clone();
		this.genres_.genres_NAME_PB = (string[])msg.genres_NAME_PB.Clone();
		this.genres_.genres_NAME_HU = (string[])msg.genres_NAME_HU.Clone();
		this.genres_.genres_NAME_CT = (string[])msg.genres_NAME_CT.Clone();
		this.genres_.genres_NAME_ES = (string[])msg.genres_NAME_ES.Clone();
		this.genres_.genres_NAME_PL = (string[])msg.genres_NAME_PL.Clone();
		this.genres_.genres_NAME_CZ = (string[])msg.genres_NAME_CZ.Clone();
		this.genres_.genres_NAME_KO = (string[])msg.genres_NAME_KO.Clone();
		this.genres_.genres_NAME_IT = (string[])msg.genres_NAME_IT.Clone();
		this.genres_.genres_NAME_AR = (string[])msg.genres_NAME_AR.Clone();
		this.genres_.genres_NAME_JA = (string[])msg.genres_NAME_JA.Clone();
		this.genres_.genres_DESC_EN = (string[])msg.genres_DESC_EN.Clone();
		this.genres_.genres_DESC_GE = (string[])msg.genres_DESC_GE.Clone();
		this.genres_.genres_DESC_TU = (string[])msg.genres_DESC_TU.Clone();
		this.genres_.genres_DESC_CH = (string[])msg.genres_DESC_CH.Clone();
		this.genres_.genres_DESC_FR = (string[])msg.genres_DESC_FR.Clone();
		this.genres_.genres_DESC_PB = (string[])msg.genres_DESC_PB.Clone();
		this.genres_.genres_DESC_HU = (string[])msg.genres_DESC_HU.Clone();
		this.genres_.genres_DESC_CT = (string[])msg.genres_DESC_CT.Clone();
		this.genres_.genres_DESC_ES = (string[])msg.genres_DESC_ES.Clone();
		this.genres_.genres_DESC_PL = (string[])msg.genres_DESC_PL.Clone();
		this.genres_.genres_DESC_CZ = (string[])msg.genres_DESC_CZ.Clone();
		this.genres_.genres_DESC_KO = (string[])msg.genres_DESC_KO.Clone();
		this.genres_.genres_DESC_IT = (string[])msg.genres_DESC_IT.Clone();
		this.genres_.genres_DESC_AR = (string[])msg.genres_DESC_AR.Clone();
		this.genres_.genres_DESC_JA = (string[])msg.genres_DESC_JA.Clone();
		this.genres_.genres_FANS = (int[])msg.genres_FANS.Clone();
		this.genres_.genres_MARKT = (int[])msg.genres_MARKT.Clone();
		this.genres_.Copy2DimensionArray_TARGETGROUP(msg.genres_TARGETGROUP);
		this.genres_.Copy2DimensionArray_COMBINATION(msg.genres_COMBINATION);
		this.genres_.Copy2DimensionArray_FOCUS(msg.genres_FOCUS);
		this.genres_.Copy2DimensionArray_ALIGN(msg.genres_ALIGN);
		this.genres_.Init();
		this.tS_.LoadContent_Themes();
		this.mS_.UnlockRandomThemeAndGenre();
		this.mpMain_.InitDropdowns();
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x00102174 File Offset: 0x00100374
	public void SERVER_Send_EngineFeatures()
	{
		Debug.Log("SERVER_Send_EngineFeatures()");
		NetworkServer.SendToAll<mpCalls.s_EngineFeatures>(new mpCalls.s_EngineFeatures
		{
			engineFeatures_TYP = (int[])this.eF_.engineFeatures_TYP.Clone(),
			engineFeatures_RES_POINTS = (int[])this.eF_.engineFeatures_RES_POINTS.Clone(),
			engineFeatures_RES_POINTS_LEFT = (float[])this.eF_.engineFeatures_RES_POINTS_LEFT.Clone(),
			engineFeatures_PRICE = (int[])this.eF_.engineFeatures_PRICE.Clone(),
			engineFeatures_DEV_COSTS = (int[])this.eF_.engineFeatures_DEV_COSTS.Clone(),
			engineFeatures_TECH = (int[])this.eF_.engineFeatures_TECH.Clone(),
			engineFeatures_DATE_YEAR = (int[])this.eF_.engineFeatures_DATE_YEAR.Clone(),
			engineFeatures_DATE_MONTH = (int[])this.eF_.engineFeatures_DATE_MONTH.Clone(),
			engineFeatures_GAMEPLAY = (int[])this.eF_.engineFeatures_GAMEPLAY.Clone(),
			engineFeatures_GRAPHIC = (int[])this.eF_.engineFeatures_GRAPHIC.Clone(),
			engineFeatures_SOUND = (int[])this.eF_.engineFeatures_SOUND.Clone(),
			engineFeatures_TECHNIK = (int[])this.eF_.engineFeatures_TECHNIK.Clone(),
			engineFeatures_LEVEL = (int[])this.eF_.engineFeatures_LEVEL.Clone(),
			engineFeatures_UNLOCK = (bool[])this.eF_.engineFeatures_UNLOCK.Clone(),
			engineFeatures_ICONFILE = (string[])this.eF_.engineFeatures_ICONFILE.Clone(),
			engineFeatures_NAME_EN = (string[])this.eF_.engineFeatures_NAME_EN.Clone(),
			engineFeatures_NAME_GE = (string[])this.eF_.engineFeatures_NAME_GE.Clone(),
			engineFeatures_NAME_TU = (string[])this.eF_.engineFeatures_NAME_TU.Clone(),
			engineFeatures_NAME_CH = (string[])this.eF_.engineFeatures_NAME_CH.Clone(),
			engineFeatures_NAME_FR = (string[])this.eF_.engineFeatures_NAME_FR.Clone(),
			engineFeatures_NAME_PB = (string[])this.eF_.engineFeatures_NAME_PB.Clone(),
			engineFeatures_NAME_CT = (string[])this.eF_.engineFeatures_NAME_CT.Clone(),
			engineFeatures_NAME_HU = (string[])this.eF_.engineFeatures_NAME_HU.Clone(),
			engineFeatures_NAME_ES = (string[])this.eF_.engineFeatures_NAME_ES.Clone(),
			engineFeatures_NAME_CZ = (string[])this.eF_.engineFeatures_NAME_CZ.Clone(),
			engineFeatures_NAME_KO = (string[])this.eF_.engineFeatures_NAME_KO.Clone(),
			engineFeatures_NAME_AR = (string[])this.eF_.engineFeatures_NAME_AR.Clone(),
			engineFeatures_NAME_RU = (string[])this.eF_.engineFeatures_NAME_RU.Clone(),
			engineFeatures_NAME_IT = (string[])this.eF_.engineFeatures_NAME_IT.Clone(),
			engineFeatures_NAME_JA = (string[])this.eF_.engineFeatures_NAME_JA.Clone(),
			engineFeatures_NAME_PL = (string[])this.eF_.engineFeatures_NAME_PL.Clone(),
			engineFeatures_DESC_EN = (string[])this.eF_.engineFeatures_DESC_EN.Clone(),
			engineFeatures_DESC_GE = (string[])this.eF_.engineFeatures_DESC_GE.Clone(),
			engineFeatures_DESC_TU = (string[])this.eF_.engineFeatures_DESC_TU.Clone(),
			engineFeatures_DESC_CH = (string[])this.eF_.engineFeatures_DESC_CH.Clone(),
			engineFeatures_DESC_FR = (string[])this.eF_.engineFeatures_DESC_FR.Clone(),
			engineFeatures_DESC_PB = (string[])this.eF_.engineFeatures_DESC_PB.Clone(),
			engineFeatures_DESC_CT = (string[])this.eF_.engineFeatures_DESC_CT.Clone(),
			engineFeatures_DESC_HU = (string[])this.eF_.engineFeatures_DESC_HU.Clone(),
			engineFeatures_DESC_ES = (string[])this.eF_.engineFeatures_DESC_ES.Clone(),
			engineFeatures_DESC_CZ = (string[])this.eF_.engineFeatures_DESC_CZ.Clone(),
			engineFeatures_DESC_KO = (string[])this.eF_.engineFeatures_DESC_KO.Clone(),
			engineFeatures_DESC_AR = (string[])this.eF_.engineFeatures_DESC_AR.Clone(),
			engineFeatures_DESC_RU = (string[])this.eF_.engineFeatures_DESC_RU.Clone(),
			engineFeatures_DESC_IT = (string[])this.eF_.engineFeatures_DESC_IT.Clone(),
			engineFeatures_DESC_JA = (string[])this.eF_.engineFeatures_DESC_JA.Clone(),
			engineFeatures_DESC_PL = (string[])this.eF_.engineFeatures_DESC_PL.Clone()
		}, 0, false);
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x001026C0 File Offset: 0x001008C0
	public void SERVER_Get_EngineFeatures(NetworkConnection conn, mpCalls.s_EngineFeatures msg)
	{
		Debug.Log("SERVER_Get_EngineFeatures()");
		this.eF_.engineFeatures_TYP = (int[])msg.engineFeatures_TYP.Clone();
		this.eF_.engineFeatures_RES_POINTS = (int[])msg.engineFeatures_RES_POINTS.Clone();
		this.eF_.engineFeatures_RES_POINTS_LEFT = (float[])msg.engineFeatures_RES_POINTS_LEFT.Clone();
		this.eF_.engineFeatures_PRICE = (int[])msg.engineFeatures_PRICE.Clone();
		this.eF_.engineFeatures_DEV_COSTS = (int[])msg.engineFeatures_DEV_COSTS.Clone();
		this.eF_.engineFeatures_TECH = (int[])msg.engineFeatures_TECH.Clone();
		this.eF_.engineFeatures_DATE_YEAR = (int[])msg.engineFeatures_DATE_YEAR.Clone();
		this.eF_.engineFeatures_DATE_MONTH = (int[])msg.engineFeatures_DATE_MONTH.Clone();
		this.eF_.engineFeatures_GAMEPLAY = (int[])msg.engineFeatures_GAMEPLAY.Clone();
		this.eF_.engineFeatures_GRAPHIC = (int[])msg.engineFeatures_GRAPHIC.Clone();
		this.eF_.engineFeatures_SOUND = (int[])msg.engineFeatures_SOUND.Clone();
		this.eF_.engineFeatures_TECHNIK = (int[])msg.engineFeatures_TECHNIK.Clone();
		this.eF_.engineFeatures_LEVEL = (int[])msg.engineFeatures_LEVEL.Clone();
		this.eF_.engineFeatures_UNLOCK = (bool[])msg.engineFeatures_UNLOCK.Clone();
		this.eF_.engineFeatures_ICONFILE = (string[])msg.engineFeatures_ICONFILE.Clone();
		this.eF_.engineFeatures_NAME_EN = (string[])msg.engineFeatures_NAME_EN.Clone();
		this.eF_.engineFeatures_NAME_GE = (string[])msg.engineFeatures_NAME_GE.Clone();
		this.eF_.engineFeatures_NAME_TU = (string[])msg.engineFeatures_NAME_TU.Clone();
		this.eF_.engineFeatures_NAME_CH = (string[])msg.engineFeatures_NAME_CH.Clone();
		this.eF_.engineFeatures_NAME_FR = (string[])msg.engineFeatures_NAME_FR.Clone();
		this.eF_.engineFeatures_NAME_PB = (string[])msg.engineFeatures_NAME_PB.Clone();
		this.eF_.engineFeatures_NAME_CT = (string[])msg.engineFeatures_NAME_CT.Clone();
		this.eF_.engineFeatures_NAME_HU = (string[])msg.engineFeatures_NAME_HU.Clone();
		this.eF_.engineFeatures_NAME_ES = (string[])msg.engineFeatures_NAME_ES.Clone();
		this.eF_.engineFeatures_NAME_CZ = (string[])msg.engineFeatures_NAME_CZ.Clone();
		this.eF_.engineFeatures_NAME_KO = (string[])msg.engineFeatures_NAME_KO.Clone();
		this.eF_.engineFeatures_NAME_AR = (string[])msg.engineFeatures_NAME_AR.Clone();
		this.eF_.engineFeatures_NAME_RU = (string[])msg.engineFeatures_NAME_RU.Clone();
		this.eF_.engineFeatures_NAME_IT = (string[])msg.engineFeatures_NAME_IT.Clone();
		this.eF_.engineFeatures_NAME_JA = (string[])msg.engineFeatures_NAME_JA.Clone();
		this.eF_.engineFeatures_NAME_PL = (string[])msg.engineFeatures_NAME_PL.Clone();
		this.eF_.engineFeatures_DESC_EN = (string[])msg.engineFeatures_DESC_EN.Clone();
		this.eF_.engineFeatures_DESC_GE = (string[])msg.engineFeatures_DESC_GE.Clone();
		this.eF_.engineFeatures_DESC_TU = (string[])msg.engineFeatures_DESC_TU.Clone();
		this.eF_.engineFeatures_DESC_CH = (string[])msg.engineFeatures_DESC_CH.Clone();
		this.eF_.engineFeatures_DESC_FR = (string[])msg.engineFeatures_DESC_FR.Clone();
		this.eF_.engineFeatures_DESC_PB = (string[])msg.engineFeatures_DESC_PB.Clone();
		this.eF_.engineFeatures_DESC_CT = (string[])msg.engineFeatures_DESC_CT.Clone();
		this.eF_.engineFeatures_DESC_HU = (string[])msg.engineFeatures_DESC_HU.Clone();
		this.eF_.engineFeatures_DESC_ES = (string[])msg.engineFeatures_DESC_ES.Clone();
		this.eF_.engineFeatures_DESC_CZ = (string[])msg.engineFeatures_DESC_CZ.Clone();
		this.eF_.engineFeatures_DESC_KO = (string[])msg.engineFeatures_DESC_KO.Clone();
		this.eF_.engineFeatures_DESC_AR = (string[])msg.engineFeatures_DESC_AR.Clone();
		this.eF_.engineFeatures_DESC_RU = (string[])msg.engineFeatures_DESC_RU.Clone();
		this.eF_.engineFeatures_DESC_IT = (string[])msg.engineFeatures_DESC_IT.Clone();
		this.eF_.engineFeatures_DESC_JA = (string[])msg.engineFeatures_DESC_JA.Clone();
		this.eF_.engineFeatures_DESC_PL = (string[])msg.engineFeatures_DESC_PL.Clone();
		this.eF_.Init();
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x00102BD8 File Offset: 0x00100DD8
	public void SERVER_Send_HardwareFeatures()
	{
		Debug.Log("SERVER_Send_HardwareFeatures()");
		NetworkServer.SendToAll<mpCalls.s_HardwareFeatures>(new mpCalls.s_HardwareFeatures
		{
			hardFeat_ICONFILE = (string[])this.hardwareFeatures_.hardFeat_ICONFILE.Clone(),
			hardFeat_RES_POINTS = (int[])this.hardwareFeatures_.hardFeat_RES_POINTS.Clone(),
			hardFeat_RES_POINTS_LEFT = (float[])this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT.Clone(),
			hardFeat_PRICE = (int[])this.hardwareFeatures_.hardFeat_PRICE.Clone(),
			hardFeat_DEV_COSTS = (int[])this.hardwareFeatures_.hardFeat_DEV_COSTS.Clone(),
			hardFeat_DATE_YEAR = (int[])this.hardwareFeatures_.hardFeat_DATE_YEAR.Clone(),
			hardFeat_DATE_MONTH = (int[])this.hardwareFeatures_.hardFeat_DATE_MONTH.Clone(),
			hardFeat_UNLOCK = (bool[])this.hardwareFeatures_.hardFeat_UNLOCK.Clone(),
			hardFeat_ONLYSTATIONARY = (bool[])this.hardwareFeatures_.hardFeat_ONLYSTATIONARY.Clone(),
			hardFeat_ONLYHANDHELD = (bool[])this.hardwareFeatures_.hardFeat_ONLYHANDHELD.Clone(),
			hardFeat_NEEDINTERNET = (bool[])this.hardwareFeatures_.hardFeat_NEEDINTERNET.Clone(),
			hardFeat_QUALITY = (float[])this.hardwareFeatures_.hardFeat_QUALITY.Clone(),
			hardFeat_NAME_EN = (string[])this.hardwareFeatures_.hardFeat_NAME_EN.Clone(),
			hardFeat_NAME_GE = (string[])this.hardwareFeatures_.hardFeat_NAME_GE.Clone(),
			hardFeat_NAME_TU = (string[])this.hardwareFeatures_.hardFeat_NAME_TU.Clone(),
			hardFeat_NAME_CH = (string[])this.hardwareFeatures_.hardFeat_NAME_CH.Clone(),
			hardFeat_NAME_FR = (string[])this.hardwareFeatures_.hardFeat_NAME_FR.Clone(),
			hardFeat_NAME_PB = (string[])this.hardwareFeatures_.hardFeat_NAME_PB.Clone(),
			hardFeat_NAME_CT = (string[])this.hardwareFeatures_.hardFeat_NAME_CT.Clone(),
			hardFeat_NAME_HU = (string[])this.hardwareFeatures_.hardFeat_NAME_HU.Clone(),
			hardFeat_NAME_ES = (string[])this.hardwareFeatures_.hardFeat_NAME_ES.Clone(),
			hardFeat_NAME_CZ = (string[])this.hardwareFeatures_.hardFeat_NAME_CZ.Clone(),
			hardFeat_NAME_KO = (string[])this.hardwareFeatures_.hardFeat_NAME_KO.Clone(),
			hardFeat_NAME_AR = (string[])this.hardwareFeatures_.hardFeat_NAME_AR.Clone(),
			hardFeat_NAME_RU = (string[])this.hardwareFeatures_.hardFeat_NAME_RU.Clone(),
			hardFeat_NAME_IT = (string[])this.hardwareFeatures_.hardFeat_NAME_IT.Clone(),
			hardFeat_NAME_JA = (string[])this.hardwareFeatures_.hardFeat_NAME_JA.Clone(),
			hardFeat_NAME_PL = (string[])this.hardwareFeatures_.hardFeat_NAME_PL.Clone(),
			hardFeat_DESC_EN = (string[])this.hardwareFeatures_.hardFeat_DESC_EN.Clone(),
			hardFeat_DESC_GE = (string[])this.hardwareFeatures_.hardFeat_DESC_GE.Clone(),
			hardFeat_DESC_TU = (string[])this.hardwareFeatures_.hardFeat_DESC_TU.Clone(),
			hardFeat_DESC_CH = (string[])this.hardwareFeatures_.hardFeat_DESC_CH.Clone(),
			hardFeat_DESC_FR = (string[])this.hardwareFeatures_.hardFeat_DESC_FR.Clone(),
			hardFeat_DESC_PB = (string[])this.hardwareFeatures_.hardFeat_DESC_PB.Clone(),
			hardFeat_DESC_CT = (string[])this.hardwareFeatures_.hardFeat_DESC_CT.Clone(),
			hardFeat_DESC_HU = (string[])this.hardwareFeatures_.hardFeat_DESC_HU.Clone(),
			hardFeat_DESC_ES = (string[])this.hardwareFeatures_.hardFeat_DESC_ES.Clone(),
			hardFeat_DESC_CZ = (string[])this.hardwareFeatures_.hardFeat_DESC_CZ.Clone(),
			hardFeat_DESC_KO = (string[])this.hardwareFeatures_.hardFeat_DESC_KO.Clone(),
			hardFeat_DESC_AR = (string[])this.hardwareFeatures_.hardFeat_DESC_AR.Clone(),
			hardFeat_DESC_RU = (string[])this.hardwareFeatures_.hardFeat_DESC_RU.Clone(),
			hardFeat_DESC_IT = (string[])this.hardwareFeatures_.hardFeat_DESC_IT.Clone(),
			hardFeat_DESC_JA = (string[])this.hardwareFeatures_.hardFeat_DESC_JA.Clone(),
			hardFeat_DESC_PL = (string[])this.hardwareFeatures_.hardFeat_DESC_PL.Clone()
		}, 0, false);
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x001030D0 File Offset: 0x001012D0
	public void SERVER_Get_HardwareFeatures(NetworkConnection conn, mpCalls.s_HardwareFeatures msg)
	{
		Debug.Log("SERVER_Get_HardwareFeatures()");
		this.hardwareFeatures_.hardFeat_ICONFILE = (string[])msg.hardFeat_ICONFILE.Clone();
		this.hardwareFeatures_.hardFeat_RES_POINTS = (int[])msg.hardFeat_RES_POINTS.Clone();
		this.hardwareFeatures_.hardFeat_RES_POINTS_LEFT = (float[])msg.hardFeat_RES_POINTS_LEFT.Clone();
		this.hardwareFeatures_.hardFeat_PRICE = (int[])msg.hardFeat_PRICE.Clone();
		this.hardwareFeatures_.hardFeat_DEV_COSTS = (int[])msg.hardFeat_DEV_COSTS.Clone();
		this.hardwareFeatures_.hardFeat_DATE_YEAR = (int[])msg.hardFeat_DATE_YEAR.Clone();
		this.hardwareFeatures_.hardFeat_DATE_MONTH = (int[])msg.hardFeat_DATE_MONTH.Clone();
		this.hardwareFeatures_.hardFeat_UNLOCK = (bool[])msg.hardFeat_UNLOCK.Clone();
		this.hardwareFeatures_.hardFeat_ONLYSTATIONARY = (bool[])msg.hardFeat_ONLYSTATIONARY.Clone();
		this.hardwareFeatures_.hardFeat_ONLYHANDHELD = (bool[])msg.hardFeat_ONLYHANDHELD.Clone();
		this.hardwareFeatures_.hardFeat_NEEDINTERNET = (bool[])msg.hardFeat_NEEDINTERNET.Clone();
		this.hardwareFeatures_.hardFeat_QUALITY = (float[])msg.hardFeat_QUALITY.Clone();
		this.hardwareFeatures_.hardFeat_NAME_EN = (string[])msg.hardFeat_NAME_EN.Clone();
		this.hardwareFeatures_.hardFeat_NAME_GE = (string[])msg.hardFeat_NAME_GE.Clone();
		this.hardwareFeatures_.hardFeat_NAME_TU = (string[])msg.hardFeat_NAME_TU.Clone();
		this.hardwareFeatures_.hardFeat_NAME_CH = (string[])msg.hardFeat_NAME_CH.Clone();
		this.hardwareFeatures_.hardFeat_NAME_FR = (string[])msg.hardFeat_NAME_FR.Clone();
		this.hardwareFeatures_.hardFeat_NAME_PB = (string[])msg.hardFeat_NAME_PB.Clone();
		this.hardwareFeatures_.hardFeat_NAME_CT = (string[])msg.hardFeat_NAME_CT.Clone();
		this.hardwareFeatures_.hardFeat_NAME_HU = (string[])msg.hardFeat_NAME_HU.Clone();
		this.hardwareFeatures_.hardFeat_NAME_ES = (string[])msg.hardFeat_NAME_ES.Clone();
		this.hardwareFeatures_.hardFeat_NAME_CZ = (string[])msg.hardFeat_NAME_CZ.Clone();
		this.hardwareFeatures_.hardFeat_NAME_KO = (string[])msg.hardFeat_NAME_KO.Clone();
		this.hardwareFeatures_.hardFeat_NAME_AR = (string[])msg.hardFeat_NAME_AR.Clone();
		this.hardwareFeatures_.hardFeat_NAME_RU = (string[])msg.hardFeat_NAME_RU.Clone();
		this.hardwareFeatures_.hardFeat_NAME_IT = (string[])msg.hardFeat_NAME_IT.Clone();
		this.hardwareFeatures_.hardFeat_NAME_JA = (string[])msg.hardFeat_NAME_JA.Clone();
		this.hardwareFeatures_.hardFeat_NAME_PL = (string[])msg.hardFeat_NAME_PL.Clone();
		this.hardwareFeatures_.hardFeat_DESC_EN = (string[])msg.hardFeat_DESC_EN.Clone();
		this.hardwareFeatures_.hardFeat_DESC_GE = (string[])msg.hardFeat_DESC_GE.Clone();
		this.hardwareFeatures_.hardFeat_DESC_TU = (string[])msg.hardFeat_DESC_TU.Clone();
		this.hardwareFeatures_.hardFeat_DESC_CH = (string[])msg.hardFeat_DESC_CH.Clone();
		this.hardwareFeatures_.hardFeat_DESC_FR = (string[])msg.hardFeat_DESC_FR.Clone();
		this.hardwareFeatures_.hardFeat_DESC_PB = (string[])msg.hardFeat_DESC_PB.Clone();
		this.hardwareFeatures_.hardFeat_DESC_CT = (string[])msg.hardFeat_DESC_CT.Clone();
		this.hardwareFeatures_.hardFeat_DESC_HU = (string[])msg.hardFeat_DESC_HU.Clone();
		this.hardwareFeatures_.hardFeat_DESC_ES = (string[])msg.hardFeat_DESC_ES.Clone();
		this.hardwareFeatures_.hardFeat_DESC_CZ = (string[])msg.hardFeat_DESC_CZ.Clone();
		this.hardwareFeatures_.hardFeat_DESC_KO = (string[])msg.hardFeat_DESC_KO.Clone();
		this.hardwareFeatures_.hardFeat_DESC_AR = (string[])msg.hardFeat_DESC_AR.Clone();
		this.hardwareFeatures_.hardFeat_DESC_RU = (string[])msg.hardFeat_DESC_RU.Clone();
		this.hardwareFeatures_.hardFeat_DESC_IT = (string[])msg.hardFeat_DESC_IT.Clone();
		this.hardwareFeatures_.hardFeat_DESC_JA = (string[])msg.hardFeat_DESC_JA.Clone();
		this.hardwareFeatures_.hardFeat_DESC_PL = (string[])msg.hardFeat_DESC_PL.Clone();
		this.hardwareFeatures_.Init();
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x00103598 File Offset: 0x00101798
	public void SERVER_Send_Hardware()
	{
		Debug.Log("SERVER_Send_Hardware()");
		NetworkServer.SendToAll<mpCalls.s_Hardware>(new mpCalls.s_Hardware
		{
			hardware_ICONFILE = (string[])this.hardware_.hardware_ICONFILE.Clone(),
			hardware_TYP = (int[])this.hardware_.hardware_TYP.Clone(),
			hardware_RES_POINTS = (int[])this.hardware_.hardware_RES_POINTS.Clone(),
			hardware_RES_POINTS_LEFT = (float[])this.hardware_.hardware_RES_POINTS_LEFT.Clone(),
			hardware_PRICE = (int[])this.hardware_.hardware_PRICE.Clone(),
			hardware_DEV_COSTS = (int[])this.hardware_.hardware_DEV_COSTS.Clone(),
			hardware_TECH = (int[])this.hardware_.hardware_TECH.Clone(),
			hardware_DATE_YEAR = (int[])this.hardware_.hardware_DATE_YEAR.Clone(),
			hardware_DATE_MONTH = (int[])this.hardware_.hardware_DATE_MONTH.Clone(),
			hardware_UNLOCK = (bool[])this.hardware_.hardware_UNLOCK.Clone(),
			hardware_ONLYSTATIONARY = (bool[])this.hardware_.hardware_ONLYSTATIONARY.Clone(),
			hardware_ONLYHANDHELD = (bool[])this.hardware_.hardware_ONLYHANDHELD.Clone(),
			hardware_NEED1 = (int[])this.hardware_.hardware_NEED1.Clone(),
			hardware_NEED2 = (int[])this.hardware_.hardware_NEED2.Clone(),
			hardware_NAME_EN = (string[])this.hardware_.hardware_NAME_EN.Clone(),
			hardware_NAME_GE = (string[])this.hardware_.hardware_NAME_GE.Clone(),
			hardware_NAME_TU = (string[])this.hardware_.hardware_NAME_TU.Clone(),
			hardware_NAME_CH = (string[])this.hardware_.hardware_NAME_CH.Clone(),
			hardware_NAME_FR = (string[])this.hardware_.hardware_NAME_FR.Clone(),
			hardware_NAME_PB = (string[])this.hardware_.hardware_NAME_PB.Clone(),
			hardware_NAME_CT = (string[])this.hardware_.hardware_NAME_CT.Clone(),
			hardware_NAME_HU = (string[])this.hardware_.hardware_NAME_HU.Clone(),
			hardware_NAME_ES = (string[])this.hardware_.hardware_NAME_ES.Clone(),
			hardware_NAME_CZ = (string[])this.hardware_.hardware_NAME_CZ.Clone(),
			hardware_NAME_KO = (string[])this.hardware_.hardware_NAME_KO.Clone(),
			hardware_NAME_AR = (string[])this.hardware_.hardware_NAME_AR.Clone(),
			hardware_NAME_RU = (string[])this.hardware_.hardware_NAME_RU.Clone(),
			hardware_NAME_IT = (string[])this.hardware_.hardware_NAME_IT.Clone(),
			hardware_NAME_JA = (string[])this.hardware_.hardware_NAME_JA.Clone(),
			hardware_NAME_PL = (string[])this.hardware_.hardware_NAME_PL.Clone(),
			hardware_DESC_EN = (string[])this.hardware_.hardware_DESC_EN.Clone(),
			hardware_DESC_GE = (string[])this.hardware_.hardware_DESC_GE.Clone(),
			hardware_DESC_TU = (string[])this.hardware_.hardware_DESC_TU.Clone(),
			hardware_DESC_CH = (string[])this.hardware_.hardware_DESC_CH.Clone(),
			hardware_DESC_FR = (string[])this.hardware_.hardware_DESC_FR.Clone(),
			hardware_DESC_PB = (string[])this.hardware_.hardware_DESC_PB.Clone(),
			hardware_DESC_CT = (string[])this.hardware_.hardware_DESC_CT.Clone(),
			hardware_DESC_HU = (string[])this.hardware_.hardware_DESC_HU.Clone(),
			hardware_DESC_ES = (string[])this.hardware_.hardware_DESC_ES.Clone(),
			hardware_DESC_CZ = (string[])this.hardware_.hardware_DESC_CZ.Clone(),
			hardware_DESC_KO = (string[])this.hardware_.hardware_DESC_KO.Clone(),
			hardware_DESC_AR = (string[])this.hardware_.hardware_DESC_AR.Clone(),
			hardware_DESC_RU = (string[])this.hardware_.hardware_DESC_RU.Clone(),
			hardware_DESC_IT = (string[])this.hardware_.hardware_DESC_IT.Clone(),
			hardware_DESC_JA = (string[])this.hardware_.hardware_DESC_JA.Clone(),
			hardware_DESC_PL = (string[])this.hardware_.hardware_DESC_PL.Clone()
		}, 0, false);
	}

	// Token: 0x06001996 RID: 6550 RVA: 0x00103AC8 File Offset: 0x00101CC8
	public void SERVER_Get_Hardware(NetworkConnection conn, mpCalls.s_Hardware msg)
	{
		Debug.Log("SERVER_Get_Hardware()");
		this.hardware_.hardware_ICONFILE = (string[])msg.hardware_ICONFILE.Clone();
		this.hardware_.hardware_TYP = (int[])msg.hardware_TYP.Clone();
		this.hardware_.hardware_RES_POINTS = (int[])msg.hardware_RES_POINTS.Clone();
		this.hardware_.hardware_RES_POINTS_LEFT = (float[])msg.hardware_RES_POINTS_LEFT.Clone();
		this.hardware_.hardware_PRICE = (int[])msg.hardware_PRICE.Clone();
		this.hardware_.hardware_DEV_COSTS = (int[])msg.hardware_DEV_COSTS.Clone();
		this.hardware_.hardware_TECH = (int[])msg.hardware_TECH.Clone();
		this.hardware_.hardware_DATE_YEAR = (int[])msg.hardware_DATE_YEAR.Clone();
		this.hardware_.hardware_DATE_MONTH = (int[])msg.hardware_DATE_MONTH.Clone();
		this.hardware_.hardware_UNLOCK = (bool[])msg.hardware_UNLOCK.Clone();
		this.hardware_.hardware_ONLYSTATIONARY = (bool[])msg.hardware_ONLYSTATIONARY.Clone();
		this.hardware_.hardware_ONLYHANDHELD = (bool[])msg.hardware_ONLYHANDHELD.Clone();
		this.hardware_.hardware_NEED1 = (int[])msg.hardware_NEED1.Clone();
		this.hardware_.hardware_NEED2 = (int[])msg.hardware_NEED2.Clone();
		this.hardware_.hardware_NAME_EN = (string[])msg.hardware_NAME_EN.Clone();
		this.hardware_.hardware_NAME_GE = (string[])msg.hardware_NAME_GE.Clone();
		this.hardware_.hardware_NAME_TU = (string[])msg.hardware_NAME_TU.Clone();
		this.hardware_.hardware_NAME_CH = (string[])msg.hardware_NAME_CH.Clone();
		this.hardware_.hardware_NAME_FR = (string[])msg.hardware_NAME_FR.Clone();
		this.hardware_.hardware_NAME_PB = (string[])msg.hardware_NAME_PB.Clone();
		this.hardware_.hardware_NAME_CT = (string[])msg.hardware_NAME_CT.Clone();
		this.hardware_.hardware_NAME_HU = (string[])msg.hardware_NAME_HU.Clone();
		this.hardware_.hardware_NAME_ES = (string[])msg.hardware_NAME_ES.Clone();
		this.hardware_.hardware_NAME_CZ = (string[])msg.hardware_NAME_CZ.Clone();
		this.hardware_.hardware_NAME_KO = (string[])msg.hardware_NAME_KO.Clone();
		this.hardware_.hardware_NAME_AR = (string[])msg.hardware_NAME_AR.Clone();
		this.hardware_.hardware_NAME_RU = (string[])msg.hardware_NAME_RU.Clone();
		this.hardware_.hardware_NAME_IT = (string[])msg.hardware_NAME_IT.Clone();
		this.hardware_.hardware_NAME_JA = (string[])msg.hardware_NAME_JA.Clone();
		this.hardware_.hardware_NAME_PL = (string[])msg.hardware_NAME_PL.Clone();
		this.hardware_.hardware_DESC_EN = (string[])msg.hardware_DESC_EN.Clone();
		this.hardware_.hardware_DESC_GE = (string[])msg.hardware_DESC_GE.Clone();
		this.hardware_.hardware_DESC_TU = (string[])msg.hardware_DESC_TU.Clone();
		this.hardware_.hardware_DESC_CH = (string[])msg.hardware_DESC_CH.Clone();
		this.hardware_.hardware_DESC_FR = (string[])msg.hardware_DESC_FR.Clone();
		this.hardware_.hardware_DESC_PB = (string[])msg.hardware_DESC_PB.Clone();
		this.hardware_.hardware_DESC_CT = (string[])msg.hardware_DESC_CT.Clone();
		this.hardware_.hardware_DESC_HU = (string[])msg.hardware_DESC_HU.Clone();
		this.hardware_.hardware_DESC_ES = (string[])msg.hardware_DESC_ES.Clone();
		this.hardware_.hardware_DESC_CZ = (string[])msg.hardware_DESC_CZ.Clone();
		this.hardware_.hardware_DESC_KO = (string[])msg.hardware_DESC_KO.Clone();
		this.hardware_.hardware_DESC_AR = (string[])msg.hardware_DESC_AR.Clone();
		this.hardware_.hardware_DESC_RU = (string[])msg.hardware_DESC_RU.Clone();
		this.hardware_.hardware_DESC_IT = (string[])msg.hardware_DESC_IT.Clone();
		this.hardware_.hardware_DESC_JA = (string[])msg.hardware_DESC_JA.Clone();
		this.hardware_.hardware_DESC_PL = (string[])msg.hardware_DESC_PL.Clone();
		this.hardware_.Init();
	}

	// Token: 0x06001997 RID: 6551 RVA: 0x00103FC4 File Offset: 0x001021C4
	public void SERVER_Send_AntiCheat(antiCheatScript script_)
	{
		Debug.Log("SERVER_Send_AntiCheat()");
		NetworkServer.SendToAll<mpCalls.s_AntiCheat>(new mpCalls.s_AntiCheat
		{
			myID = script_.myID,
			date_year = script_.date_year,
			date_month = script_.date_month,
			price = script_.price,
			dev_costs = script_.dev_costs,
			name_EN = script_.name_EN,
			name_GE = script_.name_GE,
			name_TU = script_.name_TU,
			name_CH = script_.name_CH,
			name_FR = script_.name_FR,
			name_CT = script_.name_CT,
			name_RU = script_.name_RU,
			name_IT = script_.name_IT,
			name_JA = script_.name_JA,
			isUnlocked = script_.isUnlocked,
			effekt = script_.effekt,
			neverLooseEffect = script_.neverLooseEffect
		}, 0, false);
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x001040C8 File Offset: 0x001022C8
	public void SERVER_Get_AntiCheat(NetworkConnection conn, mpCalls.s_AntiCheat msg)
	{
		Debug.Log("SERVER_Get_AntiCheat()");
		GameObject gameObject = GameObject.Find("ANTICHEAT_" + msg.myID.ToString());
		if (gameObject)
		{
			antiCheatScript component = gameObject.GetComponent<antiCheatScript>();
			component.myID = msg.myID;
			component.date_year = msg.date_year;
			component.date_month = msg.date_month;
			component.price = msg.price;
			component.dev_costs = msg.dev_costs;
			component.name_EN = msg.name_EN;
			component.name_GE = msg.name_GE;
			component.name_TU = msg.name_TU;
			component.name_CH = msg.name_CH;
			component.name_FR = msg.name_FR;
			component.name_CT = msg.name_CT;
			component.name_RU = msg.name_RU;
			component.name_IT = msg.name_IT;
			component.name_JA = msg.name_JA;
			component.isUnlocked = msg.isUnlocked;
			component.effekt = msg.effekt;
			component.neverLooseEffect = msg.neverLooseEffect;
			return;
		}
		antiCheatScript antiCheatScript = this.antiCheat_.CreateAntiCheat();
		antiCheatScript.myID = msg.myID;
		antiCheatScript.date_year = msg.date_year;
		antiCheatScript.date_month = msg.date_month;
		antiCheatScript.price = msg.price;
		antiCheatScript.dev_costs = msg.dev_costs;
		antiCheatScript.name_EN = msg.name_EN;
		antiCheatScript.name_GE = msg.name_GE;
		antiCheatScript.name_TU = msg.name_TU;
		antiCheatScript.name_CH = msg.name_CH;
		antiCheatScript.name_FR = msg.name_FR;
		antiCheatScript.name_CT = msg.name_CT;
		antiCheatScript.name_RU = msg.name_RU;
		antiCheatScript.name_IT = msg.name_IT;
		antiCheatScript.name_JA = msg.name_JA;
		antiCheatScript.isUnlocked = msg.isUnlocked;
		antiCheatScript.effekt = msg.effekt;
		antiCheatScript.neverLooseEffect = msg.neverLooseEffect;
		antiCheatScript.Init();
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x001042B4 File Offset: 0x001024B4
	public void SERVER_Send_NpcEngine(engineScript script_)
	{
		Debug.Log("SERVER_Send_NpcEngine()");
		NetworkServer.SendToAll<mpCalls.s_NpcEngine>(new mpCalls.s_NpcEngine
		{
			myID = script_.myID,
			ownerID = script_.ownerID,
			isUnlocked = script_.isUnlocked,
			gekauft = script_.gekauft,
			myName = script_.myName,
			umsatz = script_.umsatz,
			name_EN = script_.name_EN,
			name_GE = script_.name_GE,
			name_TU = script_.name_TU,
			name_CH = script_.name_CH,
			name_FR = script_.name_FR,
			name_HU = script_.name_HU,
			name_CT = script_.name_CT,
			name_CZ = script_.name_CZ,
			name_PB = script_.name_PB,
			name_IT = script_.name_IT,
			name_JA = script_.name_JA,
			spezialgenre = script_.spezialgenre,
			spezialplatform = script_.spezialplatform,
			sellEngine = script_.sellEngine,
			preis = script_.preis,
			gewinnbeteiligung = script_.gewinnbeteiligung,
			updating = script_.updating,
			devPoints = script_.devPoints,
			devPointsStart = script_.devPointsStart,
			date_year = script_.date_year,
			date_month = script_.date_month,
			archiv_engine = script_.archiv_engine,
			publisherBuyed = (bool[])script_.publisherBuyed.Clone(),
			features = (bool[])script_.features.Clone(),
			featuresInDev = (bool[])script_.featuresInDev.Clone()
		}, 0, false);
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x0010448C File Offset: 0x0010268C
	public void SERVER_Get_NpcEngine(NetworkConnection conn, mpCalls.s_NpcEngine msg)
	{
		Debug.Log("SERVER_Get_NpcEngine()");
		GameObject gameObject = GameObject.Find("ENGINE_" + msg.myID.ToString());
		if (gameObject)
		{
			engineScript component = gameObject.GetComponent<engineScript>();
			component.myID = msg.myID;
			component.ownerID = msg.ownerID;
			component.isUnlocked = msg.isUnlocked;
			component.gekauft = msg.gekauft;
			component.myName = msg.myName;
			component.umsatz = msg.umsatz;
			component.name_EN = msg.name_EN;
			component.name_GE = msg.name_GE;
			component.name_TU = msg.name_TU;
			component.name_CH = msg.name_CH;
			component.name_FR = msg.name_FR;
			component.name_HU = msg.name_HU;
			component.name_CT = msg.name_CT;
			component.name_CZ = msg.name_CZ;
			component.name_PB = msg.name_PB;
			component.name_IT = msg.name_IT;
			component.name_JA = msg.name_JA;
			component.spezialgenre = msg.spezialgenre;
			component.spezialplatform = msg.spezialplatform;
			component.sellEngine = msg.sellEngine;
			component.preis = msg.preis;
			component.gewinnbeteiligung = msg.gewinnbeteiligung;
			component.updating = msg.updating;
			component.devPoints = msg.devPoints;
			component.devPointsStart = msg.devPointsStart;
			component.date_year = msg.date_year;
			component.date_month = msg.date_month;
			component.archiv_engine = msg.archiv_engine;
			component.publisherBuyed = (bool[])msg.publisherBuyed.Clone();
			component.features = (bool[])msg.features.Clone();
			component.featuresInDev = (bool[])msg.featuresInDev.Clone();
			return;
		}
		engineScript engineScript = this.eF_.CreateEngine();
		engineScript.myID = msg.myID;
		engineScript.ownerID = msg.ownerID;
		engineScript.isUnlocked = msg.isUnlocked;
		engineScript.gekauft = msg.gekauft;
		engineScript.myName = msg.myName;
		engineScript.umsatz = msg.umsatz;
		engineScript.name_EN = msg.name_EN;
		engineScript.name_GE = msg.name_GE;
		engineScript.name_TU = msg.name_TU;
		engineScript.name_CH = msg.name_CH;
		engineScript.name_FR = msg.name_FR;
		engineScript.name_HU = msg.name_HU;
		engineScript.name_CT = msg.name_CT;
		engineScript.name_CZ = msg.name_CZ;
		engineScript.name_PB = msg.name_PB;
		engineScript.name_IT = msg.name_IT;
		engineScript.name_JA = msg.name_JA;
		engineScript.spezialgenre = msg.spezialgenre;
		engineScript.spezialplatform = msg.spezialplatform;
		engineScript.sellEngine = msg.sellEngine;
		engineScript.preis = msg.preis;
		engineScript.gewinnbeteiligung = msg.gewinnbeteiligung;
		engineScript.updating = msg.updating;
		engineScript.devPoints = msg.devPoints;
		engineScript.devPointsStart = msg.devPointsStart;
		engineScript.date_year = msg.date_year;
		engineScript.date_month = msg.date_month;
		engineScript.archiv_engine = msg.archiv_engine;
		engineScript.publisherBuyed = (bool[])msg.publisherBuyed.Clone();
		engineScript.features = (bool[])msg.features.Clone();
		engineScript.featuresInDev = (bool[])msg.featuresInDev.Clone();
		engineScript.Init();
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x00104804 File Offset: 0x00102A04
	public void SERVER_Send_CopyProtect(copyProtectScript script_)
	{
		Debug.Log("SERVER_Send_CopyProtect()");
		NetworkServer.SendToAll<mpCalls.s_CopyProtect>(new mpCalls.s_CopyProtect
		{
			myID = script_.myID,
			date_year = script_.date_year,
			date_month = script_.date_month,
			price = script_.price,
			dev_costs = script_.dev_costs,
			name_EN = script_.name_EN,
			name_GE = script_.name_GE,
			name_TU = script_.name_TU,
			name_CH = script_.name_CH,
			name_FR = script_.name_FR,
			name_CT = script_.name_CT,
			name_RU = script_.name_RU,
			name_IT = script_.name_IT,
			name_JA = script_.name_JA,
			isUnlocked = script_.isUnlocked,
			effekt = script_.effekt,
			neverLooseEffect = script_.neverLooseEffect
		}, 0, false);
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x00104908 File Offset: 0x00102B08
	public void SERVER_Get_CopyProtect(NetworkConnection conn, mpCalls.s_CopyProtect msg)
	{
		Debug.Log("SERVER_Get_CopyProtect()");
		GameObject gameObject = GameObject.Find("COPYPROTECT_" + msg.myID.ToString());
		if (gameObject)
		{
			copyProtectScript component = gameObject.GetComponent<copyProtectScript>();
			component.myID = msg.myID;
			component.date_year = msg.date_year;
			component.date_month = msg.date_month;
			component.price = msg.price;
			component.dev_costs = msg.dev_costs;
			component.name_EN = msg.name_EN;
			component.name_GE = msg.name_GE;
			component.name_TU = msg.name_TU;
			component.name_CH = msg.name_CH;
			component.name_FR = msg.name_FR;
			component.name_CT = msg.name_CT;
			component.name_RU = msg.name_RU;
			component.name_IT = msg.name_IT;
			component.name_JA = msg.name_JA;
			component.isUnlocked = msg.isUnlocked;
			component.effekt = msg.effekt;
			component.neverLooseEffect = msg.neverLooseEffect;
			return;
		}
		copyProtectScript copyProtectScript = this.copyProtect_.CreateCopyProtect();
		copyProtectScript.myID = msg.myID;
		copyProtectScript.date_year = msg.date_year;
		copyProtectScript.date_month = msg.date_month;
		copyProtectScript.price = msg.price;
		copyProtectScript.dev_costs = msg.dev_costs;
		copyProtectScript.name_EN = msg.name_EN;
		copyProtectScript.name_GE = msg.name_GE;
		copyProtectScript.name_TU = msg.name_TU;
		copyProtectScript.name_CH = msg.name_CH;
		copyProtectScript.name_FR = msg.name_FR;
		copyProtectScript.name_CT = msg.name_CT;
		copyProtectScript.name_RU = msg.name_RU;
		copyProtectScript.name_IT = msg.name_IT;
		copyProtectScript.name_JA = msg.name_JA;
		copyProtectScript.isUnlocked = msg.isUnlocked;
		copyProtectScript.effekt = msg.effekt;
		copyProtectScript.neverLooseEffect = msg.neverLooseEffect;
		copyProtectScript.Init();
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x00104AF4 File Offset: 0x00102CF4
	public void SERVER_Send_Firmenwert()
	{
		Debug.Log("SERVER_Send_Firmenwert()");
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		int[] array2 = new int[array.Length];
		long[] array3 = new long[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component)
				{
					array2[i] = component.myID;
					array3[i] = component.firmenwert;
				}
			}
		}
		NetworkServer.SendToAll<mpCalls.s_Firmenwert>(new mpCalls.s_Firmenwert
		{
			publisherID = (int[])array2.Clone(),
			firmenwert = (long[])array3.Clone()
		}, 0, false);
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x00104B9C File Offset: 0x00102D9C
	public void SERVER_Get_Firmenwert(NetworkConnection conn, mpCalls.s_Firmenwert msg)
	{
		Debug.Log("SERVER_Get_Firmenwert()");
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			publisherScript component = array[i].GetComponent<publisherScript>();
			if (component)
			{
				for (int j = 0; j < msg.publisherID.Length; j++)
				{
					if (msg.publisherID[j] == component.myID)
					{
						component.firmenwert = msg.firmenwert[j];
					}
				}
			}
		}
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x00104C10 File Offset: 0x00102E10
	public void SERVER_Send_Publisher(publisherScript script_)
	{
		Debug.Log("SERVER_Send_Publisher()");
		if (script_)
		{
			NetworkServer.SendToAll<mpCalls.s_Publisher>(new mpCalls.s_Publisher
			{
				myID = script_.myID,
				isUnlocked = script_.isUnlocked,
				name_EN = script_.name_EN,
				name_GE = script_.name_GE,
				name_TU = script_.name_TU,
				name_CH = script_.name_CH,
				name_FR = script_.name_FR,
				name_JA = script_.name_JA,
				date_year = script_.date_year,
				date_month = script_.date_month,
				stars = script_.stars,
				logoID = script_.logoID,
				developer = script_.developer,
				publisher = script_.publisher,
				onlyMobile = script_.onlyMobile,
				share = script_.share,
				fanGenre = script_.fanGenre,
				firmenwert = script_.firmenwert,
				notForSale = script_.notForSale,
				lockToBuy = script_.lockToBuy,
				isPlayer = script_.isPlayer,
				ownerID = script_.ownerID,
				country = script_.country,
				awards = (int[])script_.awards.Clone()
			}, 0, false);
			return;
		}
		Debug.Log("ERROR: SERVER_Send_Publisher() -> Missing PublisherScript");
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x00104D90 File Offset: 0x00102F90
	public void SERVER_Get_Publisher(NetworkConnection conn, mpCalls.s_Publisher msg)
	{
		Debug.Log("SERVER_Get_Publisher()");
		GameObject gameObject = GameObject.Find("PUB_" + msg.myID.ToString());
		if (gameObject)
		{
			publisherScript component = gameObject.GetComponent<publisherScript>();
			component.myID = msg.myID;
			component.isUnlocked = msg.isUnlocked;
			component.name_EN = msg.name_EN;
			component.name_GE = msg.name_GE;
			component.name_TU = msg.name_TU;
			component.name_CH = msg.name_CH;
			component.name_FR = msg.name_FR;
			component.name_JA = msg.name_JA;
			component.date_year = msg.date_year;
			component.date_month = msg.date_month;
			component.stars = msg.stars;
			component.logoID = msg.logoID;
			component.developer = msg.developer;
			component.publisher = msg.publisher;
			component.onlyMobile = msg.onlyMobile;
			component.share = msg.share;
			component.fanGenre = msg.fanGenre;
			component.firmenwert = msg.firmenwert;
			component.notForSale = msg.notForSale;
			component.lockToBuy = msg.lockToBuy;
			component.isPlayer = msg.isPlayer;
			component.ownerID = msg.ownerID;
			component.country = msg.country;
			component.awards = (int[])msg.awards.Clone();
			return;
		}
		publisherScript publisherScript = this.publisher_.CreatePublisher();
		publisherScript.myID = msg.myID;
		publisherScript.isUnlocked = msg.isUnlocked;
		publisherScript.name_EN = msg.name_EN;
		publisherScript.name_GE = msg.name_GE;
		publisherScript.name_TU = msg.name_TU;
		publisherScript.name_CH = msg.name_CH;
		publisherScript.name_FR = msg.name_FR;
		publisherScript.name_JA = msg.name_JA;
		publisherScript.date_year = msg.date_year;
		publisherScript.date_month = msg.date_month;
		publisherScript.stars = msg.stars;
		publisherScript.logoID = msg.logoID;
		publisherScript.developer = msg.developer;
		publisherScript.publisher = msg.publisher;
		publisherScript.onlyMobile = msg.onlyMobile;
		publisherScript.share = msg.share;
		publisherScript.fanGenre = msg.fanGenre;
		publisherScript.firmenwert = msg.firmenwert;
		publisherScript.notForSale = msg.notForSale;
		publisherScript.lockToBuy = msg.lockToBuy;
		publisherScript.isPlayer = msg.isPlayer;
		publisherScript.ownerID = msg.ownerID;
		publisherScript.country = msg.country;
		publisherScript.awards = (int[])msg.awards.Clone();
		publisherScript.Init();
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x00105038 File Offset: 0x00103238
	public void SERVER_Send_Game(gameScript script_)
	{
		Debug.Log("SERVER_Send_Game()");
		NetworkServer.SendToAll<mpCalls.s_Game>(new mpCalls.s_Game
		{
			gameID = script_.myID,
			myName = script_.GetNameSimple(),
			ipName = script_.ipName,
			inDevelopment = script_.inDevelopment,
			developerID = script_.developerID,
			publisherID = script_.publisherID,
			ownerID = script_.ownerID,
			engineID = script_.engineID,
			hype = script_.hype,
			isOnMarket = script_.isOnMarket,
			warBeiAwards = script_.warBeiAwards,
			weeksOnMarket = script_.weeksOnMarket,
			usk = script_.usk,
			freigabeBudget = script_.freigabeBudget,
			reviewGameplay = script_.reviewGameplay,
			reviewGrafik = script_.reviewGrafik,
			reviewSound = script_.reviewSound,
			reviewSteuerung = script_.reviewSteuerung,
			reviewTotal = script_.reviewTotal,
			reviewGameplayText = script_.reviewGameplayText,
			reviewGrafikText = script_.reviewGrafikText,
			reviewSoundText = script_.reviewSoundText,
			reviewSteuerungText = script_.reviewSteuerungText,
			reviewTotalText = script_.reviewTotalText,
			date_year = script_.date_year,
			date_month = script_.date_month,
			date_start_year = script_.date_start_year,
			date_start_month = script_.date_start_month,
			sellsTotal = script_.sellsTotal,
			umsatzTotal = script_.umsatzTotal,
			costs_entwicklung = script_.costs_entwicklung,
			costs_mitarbeiter = script_.costs_mitarbeiter,
			costs_marketing = script_.costs_marketing,
			costs_enginegebuehren = script_.costs_enginegebuehren,
			costs_server = script_.costs_server,
			costs_production = script_.costs_production,
			costs_updates = script_.costs_updates,
			typ_standard = script_.typ_standard,
			typ_nachfolger = script_.typ_nachfolger,
			originalIP = script_.originalIP,
			teile = script_.teile,
			typ_contractGame = script_.typ_contractGame,
			typ_remaster = script_.typ_remaster,
			typ_spinoff = script_.typ_spinoff,
			typ_addon = script_.typ_addon,
			typ_addonStandalone = script_.typ_addonStandalone,
			typ_mmoaddon = script_.typ_mmoaddon,
			typ_bundle = script_.typ_bundle,
			typ_budget = script_.typ_budget,
			typ_bundleAddon = script_.typ_bundleAddon,
			typ_goty = script_.typ_goty,
			originalGameID = script_.originalGameID,
			portID = script_.portID,
			mainIP = script_.mainIP,
			ipPunkte = script_.ipPunkte,
			exklusiv = script_.exklusiv,
			herstellerExklusiv = script_.herstellerExklusiv,
			retro = script_.retro,
			handy = script_.handy,
			arcade = script_.arcade,
			goty = script_.goty,
			nachfolger_created = script_.nachfolger_created,
			remaster_created = script_.remaster_created,
			budget_created = script_.budget_created,
			goty_created = script_.goty_created,
			trendsetter = script_.trendsetter,
			spielbericht = script_.spielbericht,
			amountUpdates = script_.amountUpdates,
			bonusSellsUpdates = script_.bonusSellsUpdates,
			amountAddons = script_.amountAddons,
			bonusSellsAddons = script_.bonusSellsAddons,
			amountMMOAddons = script_.amountMMOAddons,
			bonusSellsMMOAddons = script_.bonusSellsMMOAddons,
			addonQuality = script_.addonQuality,
			devAktFeature = script_.devAktFeature,
			devPoints = script_.devPoints,
			devPointsStart = script_.devPointsStart,
			devPoints_Gesamt = script_.devPoints_Gesamt,
			devPointsStart_Gesamt = script_.devPointsStart_Gesamt,
			points_gameplay = script_.points_gameplay,
			points_grafik = script_.points_grafik,
			points_sound = script_.points_sound,
			points_technik = script_.points_technik,
			points_bugs = script_.points_bugs,
			beschreibung = script_.beschreibung,
			gameTyp = script_.gameTyp,
			gameSize = script_.gameSize,
			gameZielgruppe = script_.gameZielgruppe,
			maingenre = script_.maingenre,
			subgenre = script_.subgenre,
			gameMainTheme = script_.gameMainTheme,
			gameSubTheme = script_.gameSubTheme,
			gameLicence = script_.gameLicence,
			gameCopyProtect = script_.gameCopyProtect,
			gameAntiCheat = script_.gameAntiCheat,
			gameAP_Gameplay = script_.gameAP_Gameplay,
			gameAP_Grafik = script_.gameAP_Grafik,
			gameAP_Sound = script_.gameAP_Sound,
			gameAP_Technik = script_.gameAP_Technik,
			gameLanguage = (bool[])script_.gameLanguage.Clone(),
			gameGameplayFeatures = (bool[])script_.gameGameplayFeatures.Clone(),
			gamePlatform = (int[])script_.gamePlatform.Clone(),
			gameEngineFeature = (int[])script_.gameEngineFeature.Clone(),
			gameplayFeatures_DevDone = (bool[])script_.gameplayFeatures_DevDone.Clone(),
			engineFeature_DevDone = (bool[])script_.engineFeature_DevDone.Clone(),
			gameplayStudio = (bool[])script_.gameplayStudio.Clone(),
			grafikStudio = (bool[])script_.grafikStudio.Clone(),
			soundStudio = (bool[])script_.soundStudio.Clone(),
			motionCaptureStudio = (bool[])script_.motionCaptureStudio.Clone(),
			bundleID = (int[])script_.bundleID.Clone(),
			portExist = (bool[])script_.portExist.Clone(),
			sellsPerWeek = (int[])script_.sellsPerWeek.Clone(),
			verkaufspreis = (int[])script_.verkaufspreis.Clone(),
			releaseDate = script_.releaseDate,
			abonnements = script_.abonnements,
			abonnementsWoche = script_.abonnementsWoche,
			aboPreis = script_.aboPreis,
			pubOffer = script_.pubOffer,
			pubAngebot = script_.pubAngebot,
			pubAngebot_Weeks = script_.pubAngebot_Weeks,
			pubAngebot_Verhandlung = script_.pubAngebot_Verhandlung,
			pubAngebot_Retail = script_.pubAngebot_Retail,
			pubAngebot_Digital = script_.pubAngebot_Digital,
			pubAngebot_Garantiesumme = script_.pubAngebot_Garantiesumme,
			pubAngebot_Gewinnbeteiligung = script_.pubAngebot_Gewinnbeteiligung,
			auftragsspiel = script_.auftragsspiel,
			auftragsspiel_gehalt = script_.auftragsspiel_gehalt,
			auftragsspiel_bonus = script_.auftragsspiel_bonus,
			auftragsspiel_zeitInWochen = script_.auftragsspiel_zeitInWochen,
			auftragsspiel_wochenAlsAngebot = script_.auftragsspiel_wochenAlsAngebot,
			auftragsspiel_zeitAbgelaufen = script_.auftragsspiel_zeitAbgelaufen,
			auftragsspiel_mindestbewertung = script_.auftragsspiel_mindestbewertung,
			f2pConverted = script_.f2pConverted
		}, 0, false);
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x001057AC File Offset: 0x001039AC
	public void SERVER_Get_Game(NetworkConnection conn, mpCalls.s_Game msg)
	{
		Debug.Log("SERVER_Get_Game()");
		GameObject game = this.GetGame(msg.gameID);
		gameScript gameScript;
		if (!game)
		{
			gameScript = this.games_.CreateNewGame(true, false);
		}
		else
		{
			gameScript = game.GetComponent<gameScript>();
			if (gameScript.IsMyGame() || gameScript.IsMyAuftragsspiel())
			{
				return;
			}
		}
		gameScript.myID = msg.gameID;
		gameScript.SetMyName(msg.myName);
		gameScript.ipName = msg.ipName;
		gameScript.inDevelopment = msg.inDevelopment;
		gameScript.developerID = msg.developerID;
		gameScript.publisherID = msg.publisherID;
		gameScript.ownerID = msg.ownerID;
		gameScript.engineID = msg.engineID;
		gameScript.hype = msg.hype;
		gameScript.isOnMarket = msg.isOnMarket;
		gameScript.warBeiAwards = msg.warBeiAwards;
		gameScript.weeksOnMarket = msg.weeksOnMarket;
		gameScript.usk = msg.usk;
		gameScript.freigabeBudget = msg.freigabeBudget;
		gameScript.reviewGameplay = msg.reviewGameplay;
		gameScript.reviewGrafik = msg.reviewGrafik;
		gameScript.reviewSound = msg.reviewSound;
		gameScript.reviewSteuerung = msg.reviewSteuerung;
		gameScript.reviewTotal = msg.reviewTotal;
		gameScript.reviewGameplayText = msg.reviewGameplayText;
		gameScript.reviewGrafikText = msg.reviewGrafikText;
		gameScript.reviewSoundText = msg.reviewSoundText;
		gameScript.reviewSteuerungText = msg.reviewSteuerungText;
		gameScript.reviewTotalText = msg.reviewTotalText;
		gameScript.date_year = msg.date_year;
		gameScript.date_month = msg.date_month;
		gameScript.date_start_year = msg.date_start_year;
		gameScript.date_start_month = msg.date_start_month;
		gameScript.sellsTotal = msg.sellsTotal;
		gameScript.umsatzTotal = msg.umsatzTotal;
		gameScript.costs_entwicklung = msg.costs_entwicklung;
		gameScript.costs_mitarbeiter = msg.costs_mitarbeiter;
		gameScript.costs_marketing = msg.costs_marketing;
		gameScript.costs_enginegebuehren = msg.costs_enginegebuehren;
		gameScript.costs_server = msg.costs_server;
		gameScript.costs_production = msg.costs_production;
		gameScript.costs_updates = msg.costs_updates;
		gameScript.typ_standard = msg.typ_standard;
		gameScript.typ_nachfolger = msg.typ_nachfolger;
		gameScript.originalIP = msg.originalIP;
		gameScript.teile = msg.teile;
		gameScript.typ_contractGame = msg.typ_contractGame;
		gameScript.typ_remaster = msg.typ_remaster;
		gameScript.typ_spinoff = msg.typ_spinoff;
		gameScript.typ_addon = msg.typ_addon;
		gameScript.typ_addonStandalone = msg.typ_addonStandalone;
		gameScript.typ_mmoaddon = msg.typ_mmoaddon;
		gameScript.typ_bundle = msg.typ_bundle;
		gameScript.typ_budget = msg.typ_budget;
		gameScript.typ_bundleAddon = msg.typ_bundleAddon;
		gameScript.typ_goty = msg.typ_goty;
		gameScript.originalGameID = msg.originalGameID;
		gameScript.portID = msg.portID;
		gameScript.mainIP = msg.mainIP;
		gameScript.ipPunkte = msg.ipPunkte;
		gameScript.exklusiv = msg.exklusiv;
		gameScript.herstellerExklusiv = msg.herstellerExklusiv;
		gameScript.retro = msg.retro;
		gameScript.handy = msg.handy;
		gameScript.arcade = msg.arcade;
		gameScript.goty = msg.goty;
		gameScript.nachfolger_created = msg.nachfolger_created;
		gameScript.remaster_created = msg.remaster_created;
		gameScript.budget_created = msg.budget_created;
		gameScript.goty_created = msg.goty_created;
		gameScript.trendsetter = msg.trendsetter;
		gameScript.spielbericht = msg.spielbericht;
		gameScript.amountUpdates = msg.amountUpdates;
		gameScript.bonusSellsUpdates = msg.bonusSellsUpdates;
		gameScript.amountAddons = msg.amountAddons;
		gameScript.bonusSellsAddons = msg.bonusSellsAddons;
		gameScript.amountMMOAddons = msg.amountMMOAddons;
		gameScript.bonusSellsMMOAddons = msg.bonusSellsMMOAddons;
		gameScript.addonQuality = msg.addonQuality;
		gameScript.devAktFeature = msg.devAktFeature;
		gameScript.devPoints = msg.devPoints;
		gameScript.devPointsStart = msg.devPointsStart;
		gameScript.devPoints_Gesamt = msg.devPoints_Gesamt;
		gameScript.devPointsStart_Gesamt = msg.devPointsStart_Gesamt;
		gameScript.points_gameplay = msg.points_gameplay;
		gameScript.points_grafik = msg.points_grafik;
		gameScript.points_sound = msg.points_sound;
		gameScript.points_technik = msg.points_technik;
		gameScript.points_bugs = msg.points_bugs;
		gameScript.beschreibung = msg.beschreibung;
		gameScript.gameTyp = msg.gameTyp;
		gameScript.gameSize = msg.gameSize;
		gameScript.gameZielgruppe = msg.gameZielgruppe;
		gameScript.maingenre = msg.maingenre;
		gameScript.subgenre = msg.subgenre;
		gameScript.gameMainTheme = msg.gameMainTheme;
		gameScript.gameSubTheme = msg.gameSubTheme;
		gameScript.gameLicence = msg.gameLicence;
		gameScript.gameCopyProtect = msg.gameCopyProtect;
		gameScript.gameAntiCheat = msg.gameAntiCheat;
		gameScript.gameAP_Gameplay = msg.gameAP_Gameplay;
		gameScript.gameAP_Grafik = msg.gameAP_Grafik;
		gameScript.gameAP_Sound = msg.gameAP_Sound;
		gameScript.gameAP_Technik = msg.gameAP_Technik;
		gameScript.gameLanguage = (bool[])msg.gameLanguage.Clone();
		gameScript.gameGameplayFeatures = (bool[])msg.gameGameplayFeatures.Clone();
		gameScript.gamePlatform = (int[])msg.gamePlatform.Clone();
		gameScript.gameEngineFeature = (int[])msg.gameEngineFeature.Clone();
		gameScript.gameplayFeatures_DevDone = (bool[])msg.gameplayFeatures_DevDone.Clone();
		gameScript.engineFeature_DevDone = (bool[])msg.engineFeature_DevDone.Clone();
		gameScript.gameplayStudio = (bool[])msg.gameplayStudio.Clone();
		gameScript.grafikStudio = (bool[])msg.grafikStudio.Clone();
		gameScript.soundStudio = (bool[])msg.soundStudio.Clone();
		gameScript.motionCaptureStudio = (bool[])msg.motionCaptureStudio.Clone();
		gameScript.bundleID = (int[])msg.bundleID.Clone();
		gameScript.portExist = (bool[])msg.portExist.Clone();
		gameScript.sellsPerWeek = (int[])msg.sellsPerWeek.Clone();
		gameScript.verkaufspreis = (int[])msg.verkaufspreis.Clone();
		gameScript.releaseDate = msg.releaseDate;
		gameScript.abonnements = msg.abonnements;
		gameScript.abonnementsWoche = msg.abonnementsWoche;
		gameScript.aboPreis = msg.aboPreis;
		gameScript.pubOffer = msg.pubOffer;
		gameScript.pubAngebot = msg.pubAngebot;
		gameScript.pubAngebot_Weeks = msg.pubAngebot_Weeks;
		gameScript.pubAngebot_Verhandlung = msg.pubAngebot_Verhandlung;
		gameScript.pubAngebot_Retail = msg.pubAngebot_Retail;
		gameScript.pubAngebot_Digital = msg.pubAngebot_Digital;
		gameScript.pubAngebot_Garantiesumme = msg.pubAngebot_Garantiesumme;
		gameScript.pubAngebot_Gewinnbeteiligung = msg.pubAngebot_Gewinnbeteiligung;
		gameScript.auftragsspiel = msg.auftragsspiel;
		gameScript.auftragsspiel_gehalt = msg.auftragsspiel_gehalt;
		gameScript.auftragsspiel_bonus = msg.auftragsspiel_bonus;
		gameScript.auftragsspiel_zeitInWochen = msg.auftragsspiel_zeitInWochen;
		gameScript.auftragsspiel_wochenAlsAngebot = msg.auftragsspiel_wochenAlsAngebot;
		gameScript.auftragsspiel_zeitAbgelaufen = msg.auftragsspiel_zeitAbgelaufen;
		gameScript.auftragsspiel_mindestbewertung = msg.auftragsspiel_mindestbewertung;
		gameScript.f2pConverted = msg.f2pConverted;
		gameScript.SetGameObjectName();
		gameScript.InitUI();
		if (gameScript.isOnMarket)
		{
			gameScript.SetOnMarket();
		}
		this.games_.FindGames();
		if (this.mS_.newsSetting[0] && gameScript.isOnMarket)
		{
			if (!gameScript.GameFromMitspieler())
			{
				string text = this.tS_.GetText(494);
				text = text.Replace("<NAME1>", gameScript.GetPublisherName());
				text = text.Replace("<NAME2>", gameScript.GetNameWithTag());
				this.guiMain_.CreateTopNewsInfo(text);
			}
			else
			{
				string text2 = this.tS_.GetText(494);
				text2 = text2.Replace("<NAME1>", gameScript.GetDeveloperName());
				text2 = text2.Replace("<NAME2>", gameScript.GetNameWithTag());
				this.guiMain_.CreateTopNewsInfo(text2);
				text2 = this.tS_.GetText(1269);
				text2 = text2.Replace("<NAME>", msg.myName);
				this.guiMain_.AddChat(gameScript.GetIdFromMitspieler(), text2);
			}
		}
		this.games_.UpdateChartsWeek();
		this.guiMain_.UpdateCharts();
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x00105FE4 File Offset: 0x001041E4
	public void SERVER_Send_Lizenz(int i)
	{
		Debug.Log("SERVER_Send_Lizenz()");
		NetworkServer.SendToAll<mpCalls.s_Lizenz>(new mpCalls.s_Lizenz
		{
			lizenzID = i,
			angebot = this.licences_.licence_ANGEBOT[i],
			quality = this.licences_.licence_QUALITY[i]
		}, 0, false);
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x0010603C File Offset: 0x0010423C
	public void SERVER_Get_Lizenz(NetworkConnection conn, mpCalls.s_Lizenz msg)
	{
		Debug.Log("SERVER_Get_Lizenz()");
		if (this.licences_.licence_ANGEBOT.Length >= msg.lizenzID)
		{
			this.licences_.licence_ANGEBOT[msg.lizenzID] = msg.angebot;
			this.licences_.licence_QUALITY[msg.lizenzID] = msg.quality;
		}
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x00106098 File Offset: 0x00104298
	public void SERVER_Send_Difficulty()
	{
		Debug.Log("SERVER_Send_Difficulty()");
		NetworkServer.SendToAll<mpCalls.s_Difficulty>(new mpCalls.s_Difficulty
		{
			difficulty = this.mS_.difficulty
		}, 0, false);
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x001060D4 File Offset: 0x001042D4
	public void SERVER_Get_Difficulty(NetworkConnection conn, mpCalls.s_Difficulty msg)
	{
		Debug.Log("SERVER_Get_Difficulty()");
		this.FindScripts();
		this.mS_.difficulty = msg.difficulty;
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[31].GetComponent<Dropdown>().value = this.mS_.difficulty;
		}
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x00106150 File Offset: 0x00104350
	public void SERVER_Send_Office()
	{
		Debug.Log("SERVER_Send_Office()");
		NetworkServer.SendToAll<mpCalls.s_Office>(new mpCalls.s_Office
		{
			office = this.mS_.office
		}, 0, false);
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x0010618C File Offset: 0x0010438C
	public void SERVER_Get_Office(NetworkConnection conn, mpCalls.s_Office msg)
	{
		Debug.Log("SERVER_Get_Office()");
		this.FindScripts();
		this.mS_.office = msg.office;
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[33].GetComponent<Dropdown>().value = this.mS_.GetDropdownSlotFromMapID(this.mS_.office);
		}
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x00106210 File Offset: 0x00104410
	public void SERVER_Send_Startjahr()
	{
		Debug.Log("SERVER_Send_Startjahr()");
		NetworkServer.SendToAll<mpCalls.s_Startjahr>(new mpCalls.s_Startjahr
		{
			startjahr = this.mpMain_.uiObjects[32].GetComponent<Dropdown>().value
		}, 0, false);
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x00106258 File Offset: 0x00104458
	public void SERVER_Get_Startjahr(NetworkConnection conn, mpCalls.s_Startjahr msg)
	{
		Debug.Log("SERVER_Get_Startjahr()");
		this.FindScripts();
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[32].GetComponent<Dropdown>().value = msg.startjahr;
		}
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x001062BC File Offset: 0x001044BC
	public void SERVER_Send_Spielgeschwindigkeit()
	{
		Debug.Log("SERVER_Send_Spielgeschwindigkeit()");
		NetworkServer.SendToAll<mpCalls.s_Spielgeschwindigkeit>(new mpCalls.s_Spielgeschwindigkeit
		{
			gamespeed = this.mpMain_.uiObjects[44].GetComponent<Dropdown>().value
		}, 0, false);
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x00106304 File Offset: 0x00104504
	public void SERVER_Get_Spielgeschwindigkeit(NetworkConnection conn, mpCalls.s_Spielgeschwindigkeit msg)
	{
		Debug.Log("SERVER_Send_Spielgeschwindigkeit()");
		this.FindScripts();
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[44].GetComponent<Dropdown>().value = msg.gamespeed;
		}
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x00106368 File Offset: 0x00104568
	public void SERVER_Send_Trend()
	{
		Debug.Log("SERVER_Send_Trend()");
		NetworkServer.SendToAll<mpCalls.s_Trend>(new mpCalls.s_Trend
		{
			trendWeeks = this.mS_.trendWeeks,
			trendTheme = this.mS_.trendTheme,
			trendAntiTheme = this.mS_.trendAntiTheme,
			trendGenre = this.mS_.trendGenre,
			trendAntiGenre = this.mS_.trendAntiGenre,
			trendNextGenre = this.mS_.trendNextGenre,
			trendNextAntiGenre = this.mS_.trendNextAntiGenre,
			trendNextTheme = this.mS_.trendNextTheme,
			trendNextAntiTheme = this.mS_.trendNextAntiTheme
		}, 0, false);
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x00106434 File Offset: 0x00104634
	public void SERVER_Get_Trend(NetworkConnection conn, mpCalls.s_Trend msg)
	{
		Debug.Log("SERVER_Get_Trend()");
		this.mS_.trendWeeks = msg.trendWeeks;
		this.mS_.trendTheme = msg.trendTheme;
		this.mS_.trendAntiTheme = msg.trendAntiTheme;
		this.mS_.trendGenre = msg.trendGenre;
		this.mS_.trendAntiGenre = msg.trendAntiGenre;
		this.mS_.trendNextGenre = msg.trendNextGenre;
		this.mS_.trendNextAntiGenre = msg.trendNextAntiGenre;
		this.mS_.trendNextTheme = msg.trendNextTheme;
		this.mS_.trendNextAntiTheme = msg.trendNextAntiTheme;
		this.mS_.ShowTrendNews();
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x001064F0 File Offset: 0x001046F0
	public void SERVER_Send_DeleteArbeitsmarkt(int objectID_, bool eingestellt)
	{
		Debug.Log("SERVER_Send_DeleteArbeitsmarkt()");
		NetworkServer.SendToAll<mpCalls.s_DeleteArbeitsmarkt>(new mpCalls.s_DeleteArbeitsmarkt
		{
			objectID = objectID_,
			eingestellt = eingestellt
		}, 0, false);
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x00106528 File Offset: 0x00104728
	public void SERVER_Get_DeleteArbeitsmarkt(NetworkConnection conn, mpCalls.s_DeleteArbeitsmarkt msg)
	{
		Debug.Log("SERVER_Get_DeleteArbeitsmarkt()");
		GameObject gameObject = GameObject.Find("AA_" + msg.objectID.ToString());
		if (gameObject)
		{
			this.disableSend = true;
			gameObject.GetComponent<charArbeitsmarkt>().RemoveFromArbeitsmarkt(msg.eingestellt);
			this.disableSend = false;
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x00106584 File Offset: 0x00104784
	public void SERVER_Send_CreateArbeitsmarkt(charArbeitsmarkt script_)
	{
		Debug.Log("SERVER_Send_CreateArbeitsmarkt()");
		NetworkServer.SendToAll<mpCalls.s_CreateArbeitsmarkt>(new mpCalls.s_CreateArbeitsmarkt
		{
			objectID = script_.myID,
			male = script_.male,
			myName = script_.myName,
			wochenAmArbeitsmarkt = script_.wochenAmArbeitsmarkt,
			legend = script_.legend,
			beruf = script_.beruf,
			s_gamedesign = script_.s_gamedesign,
			s_programmieren = script_.s_programmieren,
			s_grafik = script_.s_grafik,
			s_sound = script_.s_sound,
			s_pr = script_.s_pr,
			s_gametests = script_.s_gametests,
			s_technik = script_.s_technik,
			s_forschen = script_.s_forschen,
			perks = (bool[])script_.perks.Clone(),
			model_body = script_.model_body,
			model_eyes = script_.model_eyes,
			model_hair = script_.model_hair,
			model_beard = script_.model_beard,
			model_skinColor = script_.model_skinColor,
			model_hairColor = script_.model_hairColor,
			model_beardColor = script_.model_beardColor,
			model_HoseColor = script_.model_HoseColor,
			model_ShirtColor = script_.model_ShirtColor,
			model_Add1Color = script_.model_Add1Color
		}, 0, false);
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x001066FC File Offset: 0x001048FC
	public void SERVER_Get_CreateArbeitsmarkt(NetworkConnection conn, mpCalls.s_CreateArbeitsmarkt msg)
	{
		Debug.Log("SERVER_Get_CreateArbeitsmarkt()");
		charArbeitsmarkt charArbeitsmarkt = this.arbeitsmarkt_.CreateArbeitsmarktItem();
		charArbeitsmarkt.myID = msg.objectID;
		charArbeitsmarkt.male = msg.male;
		charArbeitsmarkt.myName = msg.myName;
		charArbeitsmarkt.wochenAmArbeitsmarkt = msg.wochenAmArbeitsmarkt;
		charArbeitsmarkt.legend = msg.legend;
		charArbeitsmarkt.beruf = msg.beruf;
		charArbeitsmarkt.s_gamedesign = msg.s_gamedesign;
		charArbeitsmarkt.s_programmieren = msg.s_programmieren;
		charArbeitsmarkt.s_grafik = msg.s_grafik;
		charArbeitsmarkt.s_sound = msg.s_sound;
		charArbeitsmarkt.s_pr = msg.s_pr;
		charArbeitsmarkt.s_gametests = msg.s_gametests;
		charArbeitsmarkt.s_technik = msg.s_technik;
		charArbeitsmarkt.s_forschen = msg.s_forschen;
		charArbeitsmarkt.perks = (bool[])msg.perks.Clone();
		charArbeitsmarkt.model_body = msg.model_body;
		charArbeitsmarkt.model_eyes = msg.model_eyes;
		charArbeitsmarkt.model_hair = msg.model_hair;
		charArbeitsmarkt.model_beard = msg.model_beard;
		charArbeitsmarkt.model_skinColor = msg.model_skinColor;
		charArbeitsmarkt.model_hairColor = msg.model_hairColor;
		charArbeitsmarkt.model_beardColor = msg.model_beardColor;
		charArbeitsmarkt.model_HoseColor = msg.model_HoseColor;
		charArbeitsmarkt.model_ShirtColor = msg.model_ShirtColor;
		charArbeitsmarkt.model_Add1Color = msg.model_Add1Color;
		charArbeitsmarkt.gameObject.name = "AA_" + charArbeitsmarkt.myID.ToString();
		if (charArbeitsmarkt.perks[1] && this.guiMain_ && this.tS_)
		{
			this.tS_.GetText(427);
			this.guiMain_.CreateTopNewsDevLegend(charArbeitsmarkt.myName, charArbeitsmarkt.beruf);
		}
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x001068C4 File Offset: 0x00104AC4
	public void SERVER_Send_GameSpeed(int speed)
	{
		Debug.Log("SERVER_Send_GameSpeed()");
		NetworkServer.SendToAll<mpCalls.s_GameSpeed>(new mpCalls.s_GameSpeed
		{
			speed = speed
		}, 0, false);
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x001068F3 File Offset: 0x00104AF3
	public void SERVER_Get_GameSpeed(NetworkConnection conn, mpCalls.s_GameSpeed msg)
	{
		Debug.Log("SERVER_Get_GameSpeed()");
		this.mS_.SetGameSpeed((float)msg.speed);
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x00106914 File Offset: 0x00104B14
	public void SERVER_Send_Command(int command)
	{
		Debug.Log("SERVER_Send_Command() " + command.ToString());
		NetworkServer.SendToAll<mpCalls.s_Command>(new mpCalls.s_Command
		{
			command = command
		}, 0, false);
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x00106950 File Offset: 0x00104B50
	public void SERVER_Get_Command(NetworkConnection conn, mpCalls.s_Command msg)
	{
		this.FindScripts();
		Debug.Log("SERVER_Get_Command() " + msg.command);
		switch (msg.command)
		{
		case 1:
			this.guiMain_.uiObjects[162].SetActive(true);
			return;
		case 2:
			this.guiMain_.uiObjects[202].SetActive(false);
			this.guiMain_.uiObjects[238].SetActive(false);
			return;
		case 3:
			this.mS_.WochenUpdates();
			return;
		case 4:
			this.mS_.MonatlicheUpdates();
			return;
		case 5:
			this.guiMain_.uiObjects[238].SetActive(true);
			return;
		case 6:
			this.mS_.settings_autoPauseForMultiplayer = false;
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[36].GetComponent<Toggle>().isOn = false;
			return;
		case 7:
			this.mS_.settings_autoPauseForMultiplayer = true;
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[36].GetComponent<Toggle>().isOn = true;
			return;
		case 8:
			this.mS_.settings_RandomEventsOff = false;
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[40].GetComponent<Toggle>().isOn = false;
			return;
		case 9:
			this.mS_.settings_RandomEventsOff = true;
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[40].GetComponent<Toggle>().isOn = true;
			return;
		case 10:
			this.mS_.settings_RandomReviews = false;
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[41].GetComponent<Toggle>().isOn = false;
			return;
		case 11:
			this.mS_.settings_RandomReviews = true;
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[41].GetComponent<Toggle>().isOn = true;
			return;
		case 12:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[42].GetComponent<Toggle>().isOn = false;
			return;
		case 13:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[42].GetComponent<Toggle>().isOn = true;
			return;
		case 14:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[43].GetComponent<Toggle>().isOn = false;
			return;
		case 15:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[43].GetComponent<Toggle>().isOn = true;
			return;
		case 16:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[45].GetComponent<Toggle>().isOn = false;
			return;
		case 17:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[45].GetComponent<Toggle>().isOn = true;
			return;
		case 18:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[52].GetComponent<Toggle>().isOn = false;
			return;
		case 19:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[52].GetComponent<Toggle>().isOn = true;
			return;
		case 20:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[53].GetComponent<Toggle>().isOn = false;
			return;
		case 21:
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[53].GetComponent<Toggle>().isOn = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x00106D40 File Offset: 0x00104F40
	public void SERVER_Send_Load(int saveID)
	{
		Debug.Log("SERVER_Send_Load() " + saveID.ToString());
		NetworkServer.SendToAll<mpCalls.s_Load>(new mpCalls.s_Load
		{
			saveID = saveID
		}, 0, false);
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x00106D7C File Offset: 0x00104F7C
	public void SERVER_Get_Load(NetworkConnection conn, mpCalls.s_Load msg)
	{
		Debug.Log("SERVER_Get_Load()");
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[238]);
		this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().BUTTON_LoadGame(msg.saveID);
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x00106DEC File Offset: 0x00104FEC
	public void SERVER_Send_Save(int saveID)
	{
		Debug.Log("SERVER_Send_Save() " + saveID.ToString());
		NetworkServer.SendToAll<mpCalls.s_Save>(new mpCalls.s_Save
		{
			saveID = saveID
		}, 0, false);
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x00106E27 File Offset: 0x00105027
	public void SERVER_Get_Save(NetworkConnection conn, mpCalls.s_Save msg)
	{
		Debug.Log("SERVER_Get_Save()");
		this.save_.Save(msg.saveID);
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x00106E44 File Offset: 0x00105044
	public void SERVER_Send_ID(int id_, mpPlayer mpPlayer_)
	{
		Debug.Log("SERVER_Send_ID");
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return;
		}
		mpCalls.s_PlayerID msg = new mpCalls.s_PlayerID
		{
			id = player_mp.playerID,
			version = this.mS_.buildVersion
		};
		NetworkServer.SendToClientOfPlayer<mpCalls.s_PlayerID>(mpPlayer_.netIdentity, msg, 0);
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x00106EA0 File Offset: 0x001050A0
	public void SERVER_Get_ID(NetworkConnection conn, mpCalls.s_PlayerID msg)
	{
		Debug.Log("SERVER_Get_ID()");
		if (msg.version == this.mS_.buildVersion)
		{
			this.mS_.myID = msg.id;
			this.CLIENT_Send_PlayerInfos();
			return;
		}
		this.guiMain_.MessageBox(this.tS_.GetText(1041), false);
		this.mpMain_.BUTTON_Close();
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x00106F10 File Offset: 0x00105110
	public void SERVER_Send_AddPlayer()
	{
		Debug.Log("SERVER_Send_AddPlayer");
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			NetworkServer.SendToAll<mpCalls.s_AddPlayer>(new mpCalls.s_AddPlayer
			{
				playerID = this.playersMP[i].playerID
			}, 0, false);
		}
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x00106F68 File Offset: 0x00105168
	public void SERVER_Get_AddPlayer(NetworkConnection conn, mpCalls.s_AddPlayer msg)
	{
		Debug.Log("SERVER_Get_AddPlayer");
		if (this.FindPlayer(msg.playerID) != null)
		{
			return;
		}
		this.playersMP.Add(new player_mp(msg.playerID));
		publisherScript myPubScript_ = this.mS_.CreatePlayerPublisher(msg.playerID);
		this.FindPlayer(msg.playerID).myPubScript_ = myPubScript_;
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x00106FC8 File Offset: 0x001051C8
	public void SERVER_Send_PlayerInfos()
	{
		Debug.Log("SERVER_Send_PlayerInfos");
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			NetworkServer.SendToAll<mpCalls.s_PlayerInfos>(new mpCalls.s_PlayerInfos
			{
				id = this.playersMP[i].playerID,
				playerName = this.playersMP[i].playerName,
				ready = this.playersMP[i].ready
			}, 0, false);
		}
	}

	// Token: 0x060019C0 RID: 6592 RVA: 0x00107050 File Offset: 0x00105250
	public void SERVER_Get_PlayerInfos(NetworkConnection conn, mpCalls.s_PlayerInfos msg)
	{
		Debug.Log("SERVER_Get_PlayerInfos");
		player_mp player_mp = this.FindPlayer(msg.id);
		if (player_mp == null)
		{
			return;
		}
		player_mp.playerName = msg.playerName;
		player_mp.ready = msg.ready;
	}

	// Token: 0x060019C1 RID: 6593 RVA: 0x00107090 File Offset: 0x00105290
	public void SERVER_Send_Money()
	{
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			NetworkServer.SendToAll<mpCalls.s_Money>(new mpCalls.s_Money
			{
				playerID = this.playersMP[i].playerID,
				money = this.playersMP[i].money,
				fans = this.playersMP[i].fans
			}, 0, false);
		}
	}

	// Token: 0x060019C2 RID: 6594 RVA: 0x0010710C File Offset: 0x0010530C
	public void SERVER_Get_Money(NetworkConnection conn, mpCalls.s_Money msg)
	{
		Debug.Log("SERVER_Get_Money");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.money = msg.money;
		player_mp.fans = msg.fans;
	}

	// Token: 0x060019C3 RID: 6595 RVA: 0x0010714C File Offset: 0x0010534C
	public void SERVER_Send_AutoPause()
	{
		for (int i = 0; i < this.playersMP.Count; i++)
		{
			NetworkServer.SendToAll<mpCalls.s_AutoPause>(new mpCalls.s_AutoPause
			{
				playerID = this.playersMP[i].playerID,
				pause = this.playersMP[i].playerPause
			}, 0, false);
		}
	}

	// Token: 0x060019C4 RID: 6596 RVA: 0x001071B0 File Offset: 0x001053B0
	public void SERVER_Get_AutoPause(NetworkConnection conn, mpCalls.s_AutoPause msg)
	{
		Debug.Log("SERVER_Get_Autopause");
		player_mp player_mp = this.FindPlayer(msg.playerID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.playerPause = msg.pause;
	}

	// Token: 0x060019C5 RID: 6597 RVA: 0x001071E4 File Offset: 0x001053E4
	private GameObject GetGame(int id_)
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].myID == id_)
			{
				return this.games_.arrayGamesScripts[i].gameObject;
			}
		}
		return null;
	}

	// Token: 0x04001C8D RID: 7309
	public bool isServer;

	// Token: 0x04001C8E RID: 7310
	public bool isClient;

	// Token: 0x04001C8F RID: 7311
	public bool disableSend;

	// Token: 0x04001C90 RID: 7312
	public List<player_mp> playersMP = new List<player_mp>();

	// Token: 0x04001C91 RID: 7313
	public mpPlayer[] players = new mpPlayer[4];

	// Token: 0x04001C92 RID: 7314
	public float timer;

	// Token: 0x04001C93 RID: 7315
	public float timer10Secs;

	// Token: 0x04001C94 RID: 7316
	public mainScript mS_;

	// Token: 0x04001C95 RID: 7317
	public GameObject main_;

	// Token: 0x04001C96 RID: 7318
	private GUI_Main guiMain_;

	// Token: 0x04001C97 RID: 7319
	private sfxScript sfx_;

	// Token: 0x04001C98 RID: 7320
	private textScript tS_;

	// Token: 0x04001C99 RID: 7321
	private mpMain mpMain_;

	// Token: 0x04001C9A RID: 7322
	private arbeitsmarkt arbeitsmarkt_;

	// Token: 0x04001C9B RID: 7323
	private licences licences_;

	// Token: 0x04001C9C RID: 7324
	private games games_;

	// Token: 0x04001C9D RID: 7325
	private engineFeatures eF_;

	// Token: 0x04001C9E RID: 7326
	private genres genres_;

	// Token: 0x04001C9F RID: 7327
	private savegameScript save_;

	// Token: 0x04001CA0 RID: 7328
	private mapScript mapScript_;

	// Token: 0x04001CA1 RID: 7329
	private gameplayFeatures gF_;

	// Token: 0x04001CA2 RID: 7330
	private hardware hardware_;

	// Token: 0x04001CA3 RID: 7331
	private hardwareFeatures hardwareFeatures_;

	// Token: 0x04001CA4 RID: 7332
	private forschungSonstiges fS_;

	// Token: 0x04001CA5 RID: 7333
	private themes themes_;

	// Token: 0x04001CA6 RID: 7334
	private platforms platforms_;

	// Token: 0x04001CA7 RID: 7335
	private publisher publisher_;

	// Token: 0x04001CA8 RID: 7336
	private copyProtect copyProtect_;

	// Token: 0x04001CA9 RID: 7337
	private anitCheat antiCheat_;

	// Token: 0x02000285 RID: 645
	public struct c_Publisher : NetworkMessage
	{
		// Token: 0x04001CAA RID: 7338
		public int myID;

		// Token: 0x04001CAB RID: 7339
		public bool isUnlocked;

		// Token: 0x04001CAC RID: 7340
		public string name_EN;

		// Token: 0x04001CAD RID: 7341
		public string name_GE;

		// Token: 0x04001CAE RID: 7342
		public string name_TU;

		// Token: 0x04001CAF RID: 7343
		public string name_CH;

		// Token: 0x04001CB0 RID: 7344
		public string name_FR;

		// Token: 0x04001CB1 RID: 7345
		public string name_JA;

		// Token: 0x04001CB2 RID: 7346
		public int date_year;

		// Token: 0x04001CB3 RID: 7347
		public int date_month;

		// Token: 0x04001CB4 RID: 7348
		public float stars;

		// Token: 0x04001CB5 RID: 7349
		public int logoID;

		// Token: 0x04001CB6 RID: 7350
		public bool developer;

		// Token: 0x04001CB7 RID: 7351
		public bool publisher;

		// Token: 0x04001CB8 RID: 7352
		public bool onlyMobile;

		// Token: 0x04001CB9 RID: 7353
		public float share;

		// Token: 0x04001CBA RID: 7354
		public int fanGenre;

		// Token: 0x04001CBB RID: 7355
		public long firmenwert;

		// Token: 0x04001CBC RID: 7356
		public bool notForSale;

		// Token: 0x04001CBD RID: 7357
		public int lockToBuy;

		// Token: 0x04001CBE RID: 7358
		public bool isPlayer;

		// Token: 0x04001CBF RID: 7359
		public int ownerID;

		// Token: 0x04001CC0 RID: 7360
		public int country;

		// Token: 0x04001CC1 RID: 7361
		public int[] awards;
	}

	// Token: 0x02000286 RID: 646
	public struct c_Forschung : NetworkMessage
	{
		// Token: 0x04001CC2 RID: 7362
		public int playerID;

		// Token: 0x04001CC3 RID: 7363
		public bool[] forschungSonstiges;

		// Token: 0x04001CC4 RID: 7364
		public bool[] genres;

		// Token: 0x04001CC5 RID: 7365
		public bool[] themes;

		// Token: 0x04001CC6 RID: 7366
		public bool[] engineFeatures;

		// Token: 0x04001CC7 RID: 7367
		public bool[] gameplayFeatures;

		// Token: 0x04001CC8 RID: 7368
		public bool[] hardware;

		// Token: 0x04001CC9 RID: 7369
		public bool[] hardwareFeatures;
	}

	// Token: 0x02000287 RID: 647
	public struct c_Help : NetworkMessage
	{
		// Token: 0x04001CCA RID: 7370
		public int playerID;

		// Token: 0x04001CCB RID: 7371
		public int toPlayerID;

		// Token: 0x04001CCC RID: 7372
		public int what;

		// Token: 0x04001CCD RID: 7373
		public int valueA;

		// Token: 0x04001CCE RID: 7374
		public int valueB;

		// Token: 0x04001CCF RID: 7375
		public int valueC;
	}

	// Token: 0x02000288 RID: 648
	public struct c_ObjectDelete : NetworkMessage
	{
		// Token: 0x04001CD0 RID: 7376
		public int playerID;

		// Token: 0x04001CD1 RID: 7377
		public int objectID;
	}

	// Token: 0x02000289 RID: 649
	public struct c_Object : NetworkMessage
	{
		// Token: 0x04001CD2 RID: 7378
		public int playerID;

		// Token: 0x04001CD3 RID: 7379
		public int objectID;

		// Token: 0x04001CD4 RID: 7380
		public int typ;

		// Token: 0x04001CD5 RID: 7381
		public float x;

		// Token: 0x04001CD6 RID: 7382
		public float y;

		// Token: 0x04001CD7 RID: 7383
		public float rot;
	}

	// Token: 0x0200028A RID: 650
	public struct c_Map : NetworkMessage
	{
		// Token: 0x04001CD8 RID: 7384
		public int playerID;

		// Token: 0x04001CD9 RID: 7385
		public byte x;

		// Token: 0x04001CDA RID: 7386
		public byte y;

		// Token: 0x04001CDB RID: 7387
		public int id;

		// Token: 0x04001CDC RID: 7388
		public int typ;

		// Token: 0x04001CDD RID: 7389
		public int door;

		// Token: 0x04001CDE RID: 7390
		public byte window;
	}

	// Token: 0x0200028B RID: 651
	public struct c_Trend : NetworkMessage
	{
		// Token: 0x04001CDF RID: 7391
		public int trendWeeks;

		// Token: 0x04001CE0 RID: 7392
		public int trendTheme;

		// Token: 0x04001CE1 RID: 7393
		public int trendAntiTheme;

		// Token: 0x04001CE2 RID: 7394
		public int trendGenre;

		// Token: 0x04001CE3 RID: 7395
		public int trendAntiGenre;
	}

	// Token: 0x0200028C RID: 652
	public struct c_Payment : NetworkMessage
	{
		// Token: 0x04001CE4 RID: 7396
		public int playerID;

		// Token: 0x04001CE5 RID: 7397
		public int toPlayerID;

		// Token: 0x04001CE6 RID: 7398
		public int what;

		// Token: 0x04001CE7 RID: 7399
		public int money;
	}

	// Token: 0x0200028D RID: 653
	public struct c_Engine : NetworkMessage
	{
		// Token: 0x04001CE8 RID: 7400
		public int myID;

		// Token: 0x04001CE9 RID: 7401
		public int ownerID;

		// Token: 0x04001CEA RID: 7402
		public bool isUnlocked;

		// Token: 0x04001CEB RID: 7403
		public bool gekauft;

		// Token: 0x04001CEC RID: 7404
		public string myName;

		// Token: 0x04001CED RID: 7405
		public bool[] features;

		// Token: 0x04001CEE RID: 7406
		public int spezialgenre;

		// Token: 0x04001CEF RID: 7407
		public int spezialplatform;

		// Token: 0x04001CF0 RID: 7408
		public bool sellEngine;

		// Token: 0x04001CF1 RID: 7409
		public int preis;

		// Token: 0x04001CF2 RID: 7410
		public int gewinnbeteiligung;
	}

	// Token: 0x0200028E RID: 654
	public struct c_Platform : NetworkMessage
	{
		// Token: 0x04001CF3 RID: 7411
		public int myID;

		// Token: 0x04001CF4 RID: 7412
		public int date_year;

		// Token: 0x04001CF5 RID: 7413
		public int date_month;

		// Token: 0x04001CF6 RID: 7414
		public int date_year_end;

		// Token: 0x04001CF7 RID: 7415
		public int date_month_end;

		// Token: 0x04001CF8 RID: 7416
		public int price;

		// Token: 0x04001CF9 RID: 7417
		public int dev_costs;

		// Token: 0x04001CFA RID: 7418
		public int tech;

		// Token: 0x04001CFB RID: 7419
		public int typ;

		// Token: 0x04001CFC RID: 7420
		public float marktanteil;

		// Token: 0x04001CFD RID: 7421
		public int[] needFeatures;

		// Token: 0x04001CFE RID: 7422
		public int units;

		// Token: 0x04001CFF RID: 7423
		public int units_max;

		// Token: 0x04001D00 RID: 7424
		public string name_EN;

		// Token: 0x04001D01 RID: 7425
		public string name_GE;

		// Token: 0x04001D02 RID: 7426
		public string name_TU;

		// Token: 0x04001D03 RID: 7427
		public string name_CH;

		// Token: 0x04001D04 RID: 7428
		public string name_FR;

		// Token: 0x04001D05 RID: 7429
		public string name_HU;

		// Token: 0x04001D06 RID: 7430
		public string name_JA;

		// Token: 0x04001D07 RID: 7431
		public string manufacturer_EN;

		// Token: 0x04001D08 RID: 7432
		public string manufacturer_GE;

		// Token: 0x04001D09 RID: 7433
		public string manufacturer_TU;

		// Token: 0x04001D0A RID: 7434
		public string manufacturer_CH;

		// Token: 0x04001D0B RID: 7435
		public string manufacturer_FR;

		// Token: 0x04001D0C RID: 7436
		public string manufacturer_HU;

		// Token: 0x04001D0D RID: 7437
		public string manufacturer_JA;

		// Token: 0x04001D0E RID: 7438
		public string pic1_file;

		// Token: 0x04001D0F RID: 7439
		public string pic2_file;

		// Token: 0x04001D10 RID: 7440
		public int pic2_year;

		// Token: 0x04001D11 RID: 7441
		public int games;

		// Token: 0x04001D12 RID: 7442
		public int exklusivGames;

		// Token: 0x04001D13 RID: 7443
		public int erfahrung;

		// Token: 0x04001D14 RID: 7444
		public bool isUnlocked;

		// Token: 0x04001D15 RID: 7445
		public bool inBesitz;

		// Token: 0x04001D16 RID: 7446
		public bool vomMarktGenommen;

		// Token: 0x04001D17 RID: 7447
		public int complex;

		// Token: 0x04001D18 RID: 7448
		public bool internet;

		// Token: 0x04001D19 RID: 7449
		public float powerFromMarket;

		// Token: 0x04001D1A RID: 7450
		public string myName;

		// Token: 0x04001D1B RID: 7451
		public int ownerID;

		// Token: 0x04001D1C RID: 7452
		public int gameID;

		// Token: 0x04001D1D RID: 7453
		public int anzController;

		// Token: 0x04001D1E RID: 7454
		public float conHueShift;

		// Token: 0x04001D1F RID: 7455
		public float conSaturation;

		// Token: 0x04001D20 RID: 7456
		public int component_cpu;

		// Token: 0x04001D21 RID: 7457
		public int component_gfx;

		// Token: 0x04001D22 RID: 7458
		public int component_ram;

		// Token: 0x04001D23 RID: 7459
		public int component_hdd;

		// Token: 0x04001D24 RID: 7460
		public int component_sfx;

		// Token: 0x04001D25 RID: 7461
		public int component_cooling;

		// Token: 0x04001D26 RID: 7462
		public int component_disc;

		// Token: 0x04001D27 RID: 7463
		public int component_controller;

		// Token: 0x04001D28 RID: 7464
		public int component_case;

		// Token: 0x04001D29 RID: 7465
		public int component_monitor;

		// Token: 0x04001D2A RID: 7466
		public bool[] hwFeatures;

		// Token: 0x04001D2B RID: 7467
		public float devPoints;

		// Token: 0x04001D2C RID: 7468
		public float devPointsStart;

		// Token: 0x04001D2D RID: 7469
		public long entwicklungsKosten;

		// Token: 0x04001D2E RID: 7470
		public long einnahmen;

		// Token: 0x04001D2F RID: 7471
		public float hype;

		// Token: 0x04001D30 RID: 7472
		public int costs_marketing;

		// Token: 0x04001D31 RID: 7473
		public int costs_mitarbeiter;

		// Token: 0x04001D32 RID: 7474
		public int startProduktionskosten;

		// Token: 0x04001D33 RID: 7475
		public int verkaufspreis;

		// Token: 0x04001D34 RID: 7476
		public float kostenreduktion;

		// Token: 0x04001D35 RID: 7477
		public bool autoPreis;

		// Token: 0x04001D36 RID: 7478
		public bool thridPartyGames;

		// Token: 0x04001D37 RID: 7479
		public long umsatzTotal;

		// Token: 0x04001D38 RID: 7480
		public long costs_production;

		// Token: 0x04001D39 RID: 7481
		public int[] sellsPerWeek;

		// Token: 0x04001D3A RID: 7482
		public int weeksOnMarket;

		// Token: 0x04001D3B RID: 7483
		public float review;

		// Token: 0x04001D3C RID: 7484
		public int performancePoints;
	}

	// Token: 0x0200028F RID: 655
	public struct c_Chat : NetworkMessage
	{
		// Token: 0x04001D3D RID: 7485
		public int playerID;

		// Token: 0x04001D3E RID: 7486
		public string text;
	}

	// Token: 0x02000290 RID: 656
	public struct c_Command : NetworkMessage
	{
		// Token: 0x04001D3F RID: 7487
		public int playerID;

		// Token: 0x04001D40 RID: 7488
		public int command;
	}

	// Token: 0x02000291 RID: 657
	public struct c_Money : NetworkMessage
	{
		// Token: 0x04001D41 RID: 7489
		public int playerID;

		// Token: 0x04001D42 RID: 7490
		public long money;

		// Token: 0x04001D43 RID: 7491
		public int fans;
	}

	// Token: 0x02000292 RID: 658
	public struct c_PlayerInfos : NetworkMessage
	{
		// Token: 0x04001D44 RID: 7492
		public int playerID;

		// Token: 0x04001D45 RID: 7493
		public string playerName;

		// Token: 0x04001D46 RID: 7494
		public bool ready;
	}

	// Token: 0x02000293 RID: 659
	public struct c_DeleteArbeitsmarkt : NetworkMessage
	{
		// Token: 0x04001D47 RID: 7495
		public int playerID;

		// Token: 0x04001D48 RID: 7496
		public int objectID;

		// Token: 0x04001D49 RID: 7497
		public bool eingestellt;
	}

	// Token: 0x02000294 RID: 660
	public struct c_BuyLizenz : NetworkMessage
	{
		// Token: 0x04001D4A RID: 7498
		public int playerID;

		// Token: 0x04001D4B RID: 7499
		public int objectID;
	}

	// Token: 0x02000295 RID: 661
	public struct c_exklusivKonsolenSells : NetworkMessage
	{
		// Token: 0x04001D4C RID: 7500
		public int gameID;

		// Token: 0x04001D4D RID: 7501
		public long exklusivKonsolenSells;
	}

	// Token: 0x02000296 RID: 662
	public struct c_GameData : NetworkMessage
	{
		// Token: 0x04001D4E RID: 7502
		public int gameID;

		// Token: 0x04001D4F RID: 7503
		public long sellsTotal;

		// Token: 0x04001D50 RID: 7504
		public long umsatzTotal;

		// Token: 0x04001D51 RID: 7505
		public bool isOnMarket;

		// Token: 0x04001D52 RID: 7506
		public int weeksOnMarket;

		// Token: 0x04001D53 RID: 7507
		public int userPositiv;

		// Token: 0x04001D54 RID: 7508
		public int userNegativ;

		// Token: 0x04001D55 RID: 7509
		public long costs_entwicklung;

		// Token: 0x04001D56 RID: 7510
		public long costs_mitarbeiter;

		// Token: 0x04001D57 RID: 7511
		public long costs_marketing;

		// Token: 0x04001D58 RID: 7512
		public long costs_enginegebuehren;

		// Token: 0x04001D59 RID: 7513
		public long costs_server;

		// Token: 0x04001D5A RID: 7514
		public long costs_production;

		// Token: 0x04001D5B RID: 7515
		public long costs_updates;

		// Token: 0x04001D5C RID: 7516
		public int[] sellsPerWeek;

		// Token: 0x04001D5D RID: 7517
		public int abonnements;

		// Token: 0x04001D5E RID: 7518
		public int abonnementsWoche;

		// Token: 0x04001D5F RID: 7519
		public int bestAbonnements;

		// Token: 0x04001D60 RID: 7520
		public int bestChartPosition;

		// Token: 0x04001D61 RID: 7521
		public long exklusivKonsolenSells;

		// Token: 0x04001D62 RID: 7522
		public float ipPunkte;

		// Token: 0x04001D63 RID: 7523
		public bool pubAngebot;

		// Token: 0x04001D64 RID: 7524
		public int pubAngebot_Weeks;

		// Token: 0x04001D65 RID: 7525
		public float pubAngebot_Verhandlung;

		// Token: 0x04001D66 RID: 7526
		public bool pubAngebot_Retail;

		// Token: 0x04001D67 RID: 7527
		public bool pubAngebot_Digital;

		// Token: 0x04001D68 RID: 7528
		public int pubAngebot_Garantiesumme;

		// Token: 0x04001D69 RID: 7529
		public float pubAngebot_Gewinnbeteiligung;

		// Token: 0x04001D6A RID: 7530
		public bool auftragsspiel;

		// Token: 0x04001D6B RID: 7531
		public int auftragsspiel_gehalt;

		// Token: 0x04001D6C RID: 7532
		public int auftragsspiel_bonus;

		// Token: 0x04001D6D RID: 7533
		public int auftragsspiel_zeitInWochen;

		// Token: 0x04001D6E RID: 7534
		public int auftragsspiel_wochenAlsAngebot;

		// Token: 0x04001D6F RID: 7535
		public bool auftragsspiel_zeitAbgelaufen;

		// Token: 0x04001D70 RID: 7536
		public int auftragsspiel_mindestbewertung;

		// Token: 0x04001D71 RID: 7537
		public string ipName;
	}

	// Token: 0x02000297 RID: 663
	public struct c_Game : NetworkMessage
	{
		// Token: 0x04001D72 RID: 7538
		public int gameID;

		// Token: 0x04001D73 RID: 7539
		public string myName;

		// Token: 0x04001D74 RID: 7540
		public string ipName;

		// Token: 0x04001D75 RID: 7541
		public bool playerGame;

		// Token: 0x04001D76 RID: 7542
		public bool inDevelopment;

		// Token: 0x04001D77 RID: 7543
		public int developerID;

		// Token: 0x04001D78 RID: 7544
		public int publisherID;

		// Token: 0x04001D79 RID: 7545
		public int ownerID;

		// Token: 0x04001D7A RID: 7546
		public int engineID;

		// Token: 0x04001D7B RID: 7547
		public float hype;

		// Token: 0x04001D7C RID: 7548
		public bool isOnMarket;

		// Token: 0x04001D7D RID: 7549
		public bool warBeiAwards;

		// Token: 0x04001D7E RID: 7550
		public int weeksOnMarket;

		// Token: 0x04001D7F RID: 7551
		public int usk;

		// Token: 0x04001D80 RID: 7552
		public int freigabeBudget;

		// Token: 0x04001D81 RID: 7553
		public int reviewGameplay;

		// Token: 0x04001D82 RID: 7554
		public int reviewGrafik;

		// Token: 0x04001D83 RID: 7555
		public int reviewSound;

		// Token: 0x04001D84 RID: 7556
		public int reviewSteuerung;

		// Token: 0x04001D85 RID: 7557
		public int reviewTotal;

		// Token: 0x04001D86 RID: 7558
		public int reviewGameplayText;

		// Token: 0x04001D87 RID: 7559
		public int reviewGrafikText;

		// Token: 0x04001D88 RID: 7560
		public int reviewSoundText;

		// Token: 0x04001D89 RID: 7561
		public int reviewSteuerungText;

		// Token: 0x04001D8A RID: 7562
		public int reviewTotalText;

		// Token: 0x04001D8B RID: 7563
		public int date_year;

		// Token: 0x04001D8C RID: 7564
		public int date_month;

		// Token: 0x04001D8D RID: 7565
		public int date_start_year;

		// Token: 0x04001D8E RID: 7566
		public int date_start_month;

		// Token: 0x04001D8F RID: 7567
		public long sellsTotal;

		// Token: 0x04001D90 RID: 7568
		public long umsatzTotal;

		// Token: 0x04001D91 RID: 7569
		public long costs_entwicklung;

		// Token: 0x04001D92 RID: 7570
		public long costs_mitarbeiter;

		// Token: 0x04001D93 RID: 7571
		public long costs_marketing;

		// Token: 0x04001D94 RID: 7572
		public long costs_enginegebuehren;

		// Token: 0x04001D95 RID: 7573
		public long costs_server;

		// Token: 0x04001D96 RID: 7574
		public long costs_production;

		// Token: 0x04001D97 RID: 7575
		public long costs_updates;

		// Token: 0x04001D98 RID: 7576
		public bool typ_standard;

		// Token: 0x04001D99 RID: 7577
		public bool typ_nachfolger;

		// Token: 0x04001D9A RID: 7578
		public int originalIP;

		// Token: 0x04001D9B RID: 7579
		public int teile;

		// Token: 0x04001D9C RID: 7580
		public bool typ_contractGame;

		// Token: 0x04001D9D RID: 7581
		public bool typ_remaster;

		// Token: 0x04001D9E RID: 7582
		public bool typ_spinoff;

		// Token: 0x04001D9F RID: 7583
		public bool typ_addon;

		// Token: 0x04001DA0 RID: 7584
		public bool typ_addonStandalone;

		// Token: 0x04001DA1 RID: 7585
		public bool typ_mmoaddon;

		// Token: 0x04001DA2 RID: 7586
		public bool typ_bundle;

		// Token: 0x04001DA3 RID: 7587
		public bool typ_budget;

		// Token: 0x04001DA4 RID: 7588
		public bool typ_bundleAddon;

		// Token: 0x04001DA5 RID: 7589
		public bool typ_goty;

		// Token: 0x04001DA6 RID: 7590
		public int originalGameID;

		// Token: 0x04001DA7 RID: 7591
		public int portID;

		// Token: 0x04001DA8 RID: 7592
		public int mainIP;

		// Token: 0x04001DA9 RID: 7593
		public float ipPunkte;

		// Token: 0x04001DAA RID: 7594
		public bool exklusiv;

		// Token: 0x04001DAB RID: 7595
		public bool herstellerExklusiv;

		// Token: 0x04001DAC RID: 7596
		public bool retro;

		// Token: 0x04001DAD RID: 7597
		public bool handy;

		// Token: 0x04001DAE RID: 7598
		public bool arcade;

		// Token: 0x04001DAF RID: 7599
		public bool goty;

		// Token: 0x04001DB0 RID: 7600
		public bool nachfolger_created;

		// Token: 0x04001DB1 RID: 7601
		public bool remaster_created;

		// Token: 0x04001DB2 RID: 7602
		public bool budget_created;

		// Token: 0x04001DB3 RID: 7603
		public bool goty_created;

		// Token: 0x04001DB4 RID: 7604
		public bool trendsetter;

		// Token: 0x04001DB5 RID: 7605
		public bool spielbericht;

		// Token: 0x04001DB6 RID: 7606
		public int amountUpdates;

		// Token: 0x04001DB7 RID: 7607
		public float bonusSellsUpdates;

		// Token: 0x04001DB8 RID: 7608
		public int amountAddons;

		// Token: 0x04001DB9 RID: 7609
		public float bonusSellsAddons;

		// Token: 0x04001DBA RID: 7610
		public int amountMMOAddons;

		// Token: 0x04001DBB RID: 7611
		public float bonusSellsMMOAddons;

		// Token: 0x04001DBC RID: 7612
		public float addonQuality;

		// Token: 0x04001DBD RID: 7613
		public int devAktFeature;

		// Token: 0x04001DBE RID: 7614
		public float devPoints;

		// Token: 0x04001DBF RID: 7615
		public float devPointsStart;

		// Token: 0x04001DC0 RID: 7616
		public float devPoints_Gesamt;

		// Token: 0x04001DC1 RID: 7617
		public float devPointsStart_Gesamt;

		// Token: 0x04001DC2 RID: 7618
		public float points_gameplay;

		// Token: 0x04001DC3 RID: 7619
		public float points_grafik;

		// Token: 0x04001DC4 RID: 7620
		public float points_sound;

		// Token: 0x04001DC5 RID: 7621
		public float points_technik;

		// Token: 0x04001DC6 RID: 7622
		public float points_bugs;

		// Token: 0x04001DC7 RID: 7623
		public string beschreibung;

		// Token: 0x04001DC8 RID: 7624
		public int gameTyp;

		// Token: 0x04001DC9 RID: 7625
		public int gameSize;

		// Token: 0x04001DCA RID: 7626
		public int gameZielgruppe;

		// Token: 0x04001DCB RID: 7627
		public int maingenre;

		// Token: 0x04001DCC RID: 7628
		public int subgenre;

		// Token: 0x04001DCD RID: 7629
		public int gameMainTheme;

		// Token: 0x04001DCE RID: 7630
		public int gameSubTheme;

		// Token: 0x04001DCF RID: 7631
		public int gameLicence;

		// Token: 0x04001DD0 RID: 7632
		public int gameCopyProtect;

		// Token: 0x04001DD1 RID: 7633
		public int gameAntiCheat;

		// Token: 0x04001DD2 RID: 7634
		public int gameAP_Gameplay;

		// Token: 0x04001DD3 RID: 7635
		public int gameAP_Grafik;

		// Token: 0x04001DD4 RID: 7636
		public int gameAP_Sound;

		// Token: 0x04001DD5 RID: 7637
		public int gameAP_Technik;

		// Token: 0x04001DD6 RID: 7638
		public bool[] gameLanguage;

		// Token: 0x04001DD7 RID: 7639
		public bool[] gameGameplayFeatures;

		// Token: 0x04001DD8 RID: 7640
		public int[] gamePlatform;

		// Token: 0x04001DD9 RID: 7641
		public int[] gameEngineFeature;

		// Token: 0x04001DDA RID: 7642
		public bool[] gameplayFeatures_DevDone;

		// Token: 0x04001DDB RID: 7643
		public bool[] engineFeature_DevDone;

		// Token: 0x04001DDC RID: 7644
		public bool[] gameplayStudio;

		// Token: 0x04001DDD RID: 7645
		public bool[] grafikStudio;

		// Token: 0x04001DDE RID: 7646
		public bool[] soundStudio;

		// Token: 0x04001DDF RID: 7647
		public bool[] motionCaptureStudio;

		// Token: 0x04001DE0 RID: 7648
		public int[] bundleID;

		// Token: 0x04001DE1 RID: 7649
		public bool[] portExist;

		// Token: 0x04001DE2 RID: 7650
		public int[] sellsPerWeek;

		// Token: 0x04001DE3 RID: 7651
		public int[] verkaufspreis;

		// Token: 0x04001DE4 RID: 7652
		public int releaseDate;

		// Token: 0x04001DE5 RID: 7653
		public int abonnements;

		// Token: 0x04001DE6 RID: 7654
		public int abonnementsWoche;

		// Token: 0x04001DE7 RID: 7655
		public int aboPreis;

		// Token: 0x04001DE8 RID: 7656
		public bool pubOffer;

		// Token: 0x04001DE9 RID: 7657
		public bool pubAngebot;

		// Token: 0x04001DEA RID: 7658
		public int pubAngebot_Weeks;

		// Token: 0x04001DEB RID: 7659
		public float pubAngebot_Verhandlung;

		// Token: 0x04001DEC RID: 7660
		public bool pubAngebot_Retail;

		// Token: 0x04001DED RID: 7661
		public bool pubAngebot_Digital;

		// Token: 0x04001DEE RID: 7662
		public int pubAngebot_Garantiesumme;

		// Token: 0x04001DEF RID: 7663
		public float pubAngebot_Gewinnbeteiligung;

		// Token: 0x04001DF0 RID: 7664
		public bool auftragsspiel;

		// Token: 0x04001DF1 RID: 7665
		public int auftragsspiel_gehalt;

		// Token: 0x04001DF2 RID: 7666
		public int auftragsspiel_bonus;

		// Token: 0x04001DF3 RID: 7667
		public int auftragsspiel_zeitInWochen;

		// Token: 0x04001DF4 RID: 7668
		public int auftragsspiel_wochenAlsAngebot;

		// Token: 0x04001DF5 RID: 7669
		public bool auftragsspiel_zeitAbgelaufen;

		// Token: 0x04001DF6 RID: 7670
		public int auftragsspiel_mindestbewertung;

		// Token: 0x04001DF7 RID: 7671
		public bool f2pConverted;
	}

	// Token: 0x02000298 RID: 664
	public struct s_AddPlayer : NetworkMessage
	{
		// Token: 0x04001DF8 RID: 7672
		public int playerID;
	}

	// Token: 0x02000299 RID: 665
	public struct s_Forschung : NetworkMessage
	{
		// Token: 0x04001DF9 RID: 7673
		public int playerID;

		// Token: 0x04001DFA RID: 7674
		public bool[] forschungSonstiges;

		// Token: 0x04001DFB RID: 7675
		public bool[] genres;

		// Token: 0x04001DFC RID: 7676
		public bool[] themes;

		// Token: 0x04001DFD RID: 7677
		public bool[] engineFeatures;

		// Token: 0x04001DFE RID: 7678
		public bool[] gameplayFeatures;

		// Token: 0x04001DFF RID: 7679
		public bool[] hardware;

		// Token: 0x04001E00 RID: 7680
		public bool[] hardwareFeatures;
	}

	// Token: 0x0200029A RID: 666
	public struct s_PlayerLeave : NetworkMessage
	{
		// Token: 0x04001E01 RID: 7681
		public int playerID;
	}

	// Token: 0x0200029B RID: 667
	public struct s_GenreBeliebtheit : NetworkMessage
	{
		// Token: 0x04001E02 RID: 7682
		public float[] genreBeliebtheit;
	}

	// Token: 0x0200029C RID: 668
	public struct s_GenreCombination : NetworkMessage
	{
		// Token: 0x04001E03 RID: 7683
		public int genreSlot;

		// Token: 0x04001E04 RID: 7684
		public bool[] genres_COMBINATION;
	}

	// Token: 0x0200029D RID: 669
	public struct s_GenreDesign : NetworkMessage
	{
		// Token: 0x04001E05 RID: 7685
		public int genreSlot;

		// Token: 0x04001E06 RID: 7686
		public int genres_focus0;

		// Token: 0x04001E07 RID: 7687
		public int genres_focus1;

		// Token: 0x04001E08 RID: 7688
		public int genres_focus2;

		// Token: 0x04001E09 RID: 7689
		public int genres_focus3;

		// Token: 0x04001E0A RID: 7690
		public int genres_focus4;

		// Token: 0x04001E0B RID: 7691
		public int genres_focus5;

		// Token: 0x04001E0C RID: 7692
		public int genres_focus6;

		// Token: 0x04001E0D RID: 7693
		public int genres_focus7;

		// Token: 0x04001E0E RID: 7694
		public int genres_align0;

		// Token: 0x04001E0F RID: 7695
		public int genres_align1;

		// Token: 0x04001E10 RID: 7696
		public int genres_align2;
	}

	// Token: 0x0200029E RID: 670
	public struct s_Help : NetworkMessage
	{
		// Token: 0x04001E11 RID: 7697
		public int playerID;

		// Token: 0x04001E12 RID: 7698
		public int toPlayerID;

		// Token: 0x04001E13 RID: 7699
		public int what;

		// Token: 0x04001E14 RID: 7700
		public int valueA;

		// Token: 0x04001E15 RID: 7701
		public int valueB;

		// Token: 0x04001E16 RID: 7702
		public int valueC;
	}

	// Token: 0x0200029F RID: 671
	public struct s_ObjectDelete : NetworkMessage
	{
		// Token: 0x04001E17 RID: 7703
		public int playerID;

		// Token: 0x04001E18 RID: 7704
		public int objectID;
	}

	// Token: 0x020002A0 RID: 672
	public struct s_Object : NetworkMessage
	{
		// Token: 0x04001E19 RID: 7705
		public int playerID;

		// Token: 0x04001E1A RID: 7706
		public int objectID;

		// Token: 0x04001E1B RID: 7707
		public int typ;

		// Token: 0x04001E1C RID: 7708
		public float x;

		// Token: 0x04001E1D RID: 7709
		public float y;

		// Token: 0x04001E1E RID: 7710
		public float rot;
	}

	// Token: 0x020002A1 RID: 673
	public struct s_Map : NetworkMessage
	{
		// Token: 0x04001E1F RID: 7711
		public int playerID;

		// Token: 0x04001E20 RID: 7712
		public byte x;

		// Token: 0x04001E21 RID: 7713
		public byte y;

		// Token: 0x04001E22 RID: 7714
		public int id;

		// Token: 0x04001E23 RID: 7715
		public int typ;

		// Token: 0x04001E24 RID: 7716
		public int door;

		// Token: 0x04001E25 RID: 7717
		public byte window;
	}

	// Token: 0x020002A2 RID: 674
	public struct s_Office : NetworkMessage
	{
		// Token: 0x04001E26 RID: 7718
		public int office;
	}

	// Token: 0x020002A3 RID: 675
	public struct s_Difficulty : NetworkMessage
	{
		// Token: 0x04001E27 RID: 7719
		public int difficulty;
	}

	// Token: 0x020002A4 RID: 676
	public struct s_Startjahr : NetworkMessage
	{
		// Token: 0x04001E28 RID: 7720
		public int startjahr;
	}

	// Token: 0x020002A5 RID: 677
	public struct s_Spielgeschwindigkeit : NetworkMessage
	{
		// Token: 0x04001E29 RID: 7721
		public int gamespeed;
	}

	// Token: 0x020002A6 RID: 678
	public struct s_GlobalEvent : NetworkMessage
	{
		// Token: 0x04001E2A RID: 7722
		public int eventID;

		// Token: 0x04001E2B RID: 7723
		public int wochen;
	}

	// Token: 0x020002A7 RID: 679
	public struct s_EngineAbrechnung : NetworkMessage
	{
		// Token: 0x04001E2C RID: 7724
		public int toPlayerID;

		// Token: 0x04001E2D RID: 7725
		public int gameID;
	}

	// Token: 0x020002A8 RID: 680
	public struct s_Awards : NetworkMessage
	{
		// Token: 0x04001E2E RID: 7726
		public int bestGrafik;

		// Token: 0x04001E2F RID: 7727
		public int bestSound;

		// Token: 0x04001E30 RID: 7728
		public int bestStudio;

		// Token: 0x04001E31 RID: 7729
		public int bestPublisher;

		// Token: 0x04001E32 RID: 7730
		public int bestGame;

		// Token: 0x04001E33 RID: 7731
		public int badGame;
	}

	// Token: 0x020002A9 RID: 681
	public struct s_Payment : NetworkMessage
	{
		// Token: 0x04001E34 RID: 7732
		public int playerID;

		// Token: 0x04001E35 RID: 7733
		public int toPlayerID;

		// Token: 0x04001E36 RID: 7734
		public int what;

		// Token: 0x04001E37 RID: 7735
		public int money;
	}

	// Token: 0x020002AA RID: 682
	public struct s_Engine : NetworkMessage
	{
		// Token: 0x04001E38 RID: 7736
		public int engineID;

		// Token: 0x04001E39 RID: 7737
		public int ownerID;

		// Token: 0x04001E3A RID: 7738
		public bool isUnlocked;

		// Token: 0x04001E3B RID: 7739
		public bool gekauft;

		// Token: 0x04001E3C RID: 7740
		public string myName;

		// Token: 0x04001E3D RID: 7741
		public bool[] features;

		// Token: 0x04001E3E RID: 7742
		public int spezialgenre;

		// Token: 0x04001E3F RID: 7743
		public int spezialplatform;

		// Token: 0x04001E40 RID: 7744
		public bool sellEngine;

		// Token: 0x04001E41 RID: 7745
		public int preis;

		// Token: 0x04001E42 RID: 7746
		public int gewinnbeteiligung;
	}

	// Token: 0x020002AB RID: 683
	public struct s_Platform : NetworkMessage
	{
		// Token: 0x04001E43 RID: 7747
		public int myID;

		// Token: 0x04001E44 RID: 7748
		public int date_year;

		// Token: 0x04001E45 RID: 7749
		public int date_month;

		// Token: 0x04001E46 RID: 7750
		public int date_year_end;

		// Token: 0x04001E47 RID: 7751
		public int date_month_end;

		// Token: 0x04001E48 RID: 7752
		public int price;

		// Token: 0x04001E49 RID: 7753
		public int dev_costs;

		// Token: 0x04001E4A RID: 7754
		public int tech;

		// Token: 0x04001E4B RID: 7755
		public int typ;

		// Token: 0x04001E4C RID: 7756
		public float marktanteil;

		// Token: 0x04001E4D RID: 7757
		public int[] needFeatures;

		// Token: 0x04001E4E RID: 7758
		public int units;

		// Token: 0x04001E4F RID: 7759
		public int units_max;

		// Token: 0x04001E50 RID: 7760
		public string name_EN;

		// Token: 0x04001E51 RID: 7761
		public string name_GE;

		// Token: 0x04001E52 RID: 7762
		public string name_TU;

		// Token: 0x04001E53 RID: 7763
		public string name_CH;

		// Token: 0x04001E54 RID: 7764
		public string name_FR;

		// Token: 0x04001E55 RID: 7765
		public string name_HU;

		// Token: 0x04001E56 RID: 7766
		public string name_JA;

		// Token: 0x04001E57 RID: 7767
		public string manufacturer_EN;

		// Token: 0x04001E58 RID: 7768
		public string manufacturer_GE;

		// Token: 0x04001E59 RID: 7769
		public string manufacturer_TU;

		// Token: 0x04001E5A RID: 7770
		public string manufacturer_CH;

		// Token: 0x04001E5B RID: 7771
		public string manufacturer_FR;

		// Token: 0x04001E5C RID: 7772
		public string manufacturer_HU;

		// Token: 0x04001E5D RID: 7773
		public string manufacturer_JA;

		// Token: 0x04001E5E RID: 7774
		public string pic1_file;

		// Token: 0x04001E5F RID: 7775
		public string pic2_file;

		// Token: 0x04001E60 RID: 7776
		public int pic2_year;

		// Token: 0x04001E61 RID: 7777
		public int games;

		// Token: 0x04001E62 RID: 7778
		public int exklusivGames;

		// Token: 0x04001E63 RID: 7779
		public int erfahrung;

		// Token: 0x04001E64 RID: 7780
		public bool isUnlocked;

		// Token: 0x04001E65 RID: 7781
		public bool inBesitz;

		// Token: 0x04001E66 RID: 7782
		public bool vomMarktGenommen;

		// Token: 0x04001E67 RID: 7783
		public int complex;

		// Token: 0x04001E68 RID: 7784
		public bool internet;

		// Token: 0x04001E69 RID: 7785
		public float powerFromMarket;

		// Token: 0x04001E6A RID: 7786
		public string myName;

		// Token: 0x04001E6B RID: 7787
		public int ownerID;

		// Token: 0x04001E6C RID: 7788
		public int gameID;

		// Token: 0x04001E6D RID: 7789
		public int anzController;

		// Token: 0x04001E6E RID: 7790
		public float conHueShift;

		// Token: 0x04001E6F RID: 7791
		public float conSaturation;

		// Token: 0x04001E70 RID: 7792
		public int component_cpu;

		// Token: 0x04001E71 RID: 7793
		public int component_gfx;

		// Token: 0x04001E72 RID: 7794
		public int component_ram;

		// Token: 0x04001E73 RID: 7795
		public int component_hdd;

		// Token: 0x04001E74 RID: 7796
		public int component_sfx;

		// Token: 0x04001E75 RID: 7797
		public int component_cooling;

		// Token: 0x04001E76 RID: 7798
		public int component_disc;

		// Token: 0x04001E77 RID: 7799
		public int component_controller;

		// Token: 0x04001E78 RID: 7800
		public int component_case;

		// Token: 0x04001E79 RID: 7801
		public int component_monitor;

		// Token: 0x04001E7A RID: 7802
		public bool[] hwFeatures;

		// Token: 0x04001E7B RID: 7803
		public float devPoints;

		// Token: 0x04001E7C RID: 7804
		public float devPointsStart;

		// Token: 0x04001E7D RID: 7805
		public long entwicklungsKosten;

		// Token: 0x04001E7E RID: 7806
		public long einnahmen;

		// Token: 0x04001E7F RID: 7807
		public float hype;

		// Token: 0x04001E80 RID: 7808
		public int costs_marketing;

		// Token: 0x04001E81 RID: 7809
		public int costs_mitarbeiter;

		// Token: 0x04001E82 RID: 7810
		public int startProduktionskosten;

		// Token: 0x04001E83 RID: 7811
		public int verkaufspreis;

		// Token: 0x04001E84 RID: 7812
		public float kostenreduktion;

		// Token: 0x04001E85 RID: 7813
		public bool autoPreis;

		// Token: 0x04001E86 RID: 7814
		public bool thridPartyGames;

		// Token: 0x04001E87 RID: 7815
		public long umsatzTotal;

		// Token: 0x04001E88 RID: 7816
		public long costs_production;

		// Token: 0x04001E89 RID: 7817
		public int[] sellsPerWeek;

		// Token: 0x04001E8A RID: 7818
		public int weeksOnMarket;

		// Token: 0x04001E8B RID: 7819
		public float review;

		// Token: 0x04001E8C RID: 7820
		public int performancePoints;
	}

	// Token: 0x020002AC RID: 684
	public struct s_PlatformData : NetworkMessage
	{
		// Token: 0x04001E8D RID: 7821
		public int platformID;

		// Token: 0x04001E8E RID: 7822
		public float marktanteil;

		// Token: 0x04001E8F RID: 7823
		public int units;

		// Token: 0x04001E90 RID: 7824
		public int units_max;

		// Token: 0x04001E91 RID: 7825
		public int date_year_end;
	}

	// Token: 0x020002AD RID: 685
	public struct s_Chat : NetworkMessage
	{
		// Token: 0x04001E92 RID: 7826
		public int playerID;

		// Token: 0x04001E93 RID: 7827
		public string text;
	}

	// Token: 0x020002AE RID: 686
	public struct s_Money : NetworkMessage
	{
		// Token: 0x04001E94 RID: 7828
		public int playerID;

		// Token: 0x04001E95 RID: 7829
		public long money;

		// Token: 0x04001E96 RID: 7830
		public int fans;
	}

	// Token: 0x020002AF RID: 687
	public struct s_AutoPause : NetworkMessage
	{
		// Token: 0x04001E97 RID: 7831
		public int playerID;

		// Token: 0x04001E98 RID: 7832
		public bool pause;
	}

	// Token: 0x020002B0 RID: 688
	public struct s_Genres : NetworkMessage
	{
		// Token: 0x04001E99 RID: 7833
		public float[] genres_BELIEBTHEIT;

		// Token: 0x04001E9A RID: 7834
		public bool[] genres_BELIEBTHEIT_SOLL;

		// Token: 0x04001E9B RID: 7835
		public int[] genres_RES_POINTS;

		// Token: 0x04001E9C RID: 7836
		public float[] genres_RES_POINTS_LEFT;

		// Token: 0x04001E9D RID: 7837
		public int[] genres_PRICE;

		// Token: 0x04001E9E RID: 7838
		public int[] genres_DEV_COSTS;

		// Token: 0x04001E9F RID: 7839
		public int[] genres_DATE_YEAR;

		// Token: 0x04001EA0 RID: 7840
		public int[] genres_DATE_MONTH;

		// Token: 0x04001EA1 RID: 7841
		public int[] genres_LEVEL;

		// Token: 0x04001EA2 RID: 7842
		public bool[] genres_UNLOCK;

		// Token: 0x04001EA3 RID: 7843
		public bool[] genres_TARGETGROUP;

		// Token: 0x04001EA4 RID: 7844
		public float[] genres_GAMEPLAY;

		// Token: 0x04001EA5 RID: 7845
		public float[] genres_GRAPHIC;

		// Token: 0x04001EA6 RID: 7846
		public float[] genres_SOUND;

		// Token: 0x04001EA7 RID: 7847
		public float[] genres_CONTROL;

		// Token: 0x04001EA8 RID: 7848
		public bool[] genres_COMBINATION;

		// Token: 0x04001EA9 RID: 7849
		public int[] genres_FOCUS;

		// Token: 0x04001EAA RID: 7850
		public bool[] genres_FOCUS_KNOWN;

		// Token: 0x04001EAB RID: 7851
		public int[] genres_ALIGN;

		// Token: 0x04001EAC RID: 7852
		public bool[] genres_ALIGN_KNOWN;

		// Token: 0x04001EAD RID: 7853
		public string[] genres_ICONFILE;

		// Token: 0x04001EAE RID: 7854
		public string[] genres_NAME_EN;

		// Token: 0x04001EAF RID: 7855
		public string[] genres_NAME_GE;

		// Token: 0x04001EB0 RID: 7856
		public string[] genres_NAME_TU;

		// Token: 0x04001EB1 RID: 7857
		public string[] genres_NAME_CH;

		// Token: 0x04001EB2 RID: 7858
		public string[] genres_NAME_FR;

		// Token: 0x04001EB3 RID: 7859
		public string[] genres_NAME_PB;

		// Token: 0x04001EB4 RID: 7860
		public string[] genres_NAME_HU;

		// Token: 0x04001EB5 RID: 7861
		public string[] genres_NAME_CT;

		// Token: 0x04001EB6 RID: 7862
		public string[] genres_NAME_ES;

		// Token: 0x04001EB7 RID: 7863
		public string[] genres_NAME_PL;

		// Token: 0x04001EB8 RID: 7864
		public string[] genres_NAME_CZ;

		// Token: 0x04001EB9 RID: 7865
		public string[] genres_NAME_KO;

		// Token: 0x04001EBA RID: 7866
		public string[] genres_NAME_IT;

		// Token: 0x04001EBB RID: 7867
		public string[] genres_NAME_AR;

		// Token: 0x04001EBC RID: 7868
		public string[] genres_NAME_JA;

		// Token: 0x04001EBD RID: 7869
		public string[] genres_DESC_EN;

		// Token: 0x04001EBE RID: 7870
		public string[] genres_DESC_GE;

		// Token: 0x04001EBF RID: 7871
		public string[] genres_DESC_TU;

		// Token: 0x04001EC0 RID: 7872
		public string[] genres_DESC_CH;

		// Token: 0x04001EC1 RID: 7873
		public string[] genres_DESC_FR;

		// Token: 0x04001EC2 RID: 7874
		public string[] genres_DESC_PB;

		// Token: 0x04001EC3 RID: 7875
		public string[] genres_DESC_HU;

		// Token: 0x04001EC4 RID: 7876
		public string[] genres_DESC_CT;

		// Token: 0x04001EC5 RID: 7877
		public string[] genres_DESC_ES;

		// Token: 0x04001EC6 RID: 7878
		public string[] genres_DESC_PL;

		// Token: 0x04001EC7 RID: 7879
		public string[] genres_DESC_CZ;

		// Token: 0x04001EC8 RID: 7880
		public string[] genres_DESC_KO;

		// Token: 0x04001EC9 RID: 7881
		public string[] genres_DESC_IT;

		// Token: 0x04001ECA RID: 7882
		public string[] genres_DESC_AR;

		// Token: 0x04001ECB RID: 7883
		public string[] genres_DESC_JA;

		// Token: 0x04001ECC RID: 7884
		public int[] genres_FANS;

		// Token: 0x04001ECD RID: 7885
		public int[] genres_MARKT;
	}

	// Token: 0x020002B1 RID: 689
	public struct s_GameplayFeatures : NetworkMessage
	{
		// Token: 0x04001ECE RID: 7886
		public int[] gameplayFeatures_TYP;

		// Token: 0x04001ECF RID: 7887
		public int[] gameplayFeatures_RES_POINTS;

		// Token: 0x04001ED0 RID: 7888
		public float[] gameplayFeatures_RES_POINTS_LEFT;

		// Token: 0x04001ED1 RID: 7889
		public int[] gameplayFeatures_PRICE;

		// Token: 0x04001ED2 RID: 7890
		public int[] gameplayFeatures_DEV_COSTS;

		// Token: 0x04001ED3 RID: 7891
		public int[] gameplayFeatures_DATE_YEAR;

		// Token: 0x04001ED4 RID: 7892
		public int[] gameplayFeatures_DATE_MONTH;

		// Token: 0x04001ED5 RID: 7893
		public int[] gameplayFeatures_GAMEPLAY;

		// Token: 0x04001ED6 RID: 7894
		public int[] gameplayFeatures_GRAPHIC;

		// Token: 0x04001ED7 RID: 7895
		public int[] gameplayFeatures_SOUND;

		// Token: 0x04001ED8 RID: 7896
		public int[] gameplayFeatures_TECHNIK;

		// Token: 0x04001ED9 RID: 7897
		public int[] gameplayFeatures_LEVEL;

		// Token: 0x04001EDA RID: 7898
		public bool[] gameplayFeatures_UNLOCK;

		// Token: 0x04001EDB RID: 7899
		public string[] gameplayFeatures_ICONFILE;

		// Token: 0x04001EDC RID: 7900
		public bool[] gameplayFeatures_GOOD;

		// Token: 0x04001EDD RID: 7901
		public bool[] gameplayFeatures_BAD;

		// Token: 0x04001EDE RID: 7902
		public bool[] gameplayFeatures_LOCKPLATFORM;

		// Token: 0x04001EDF RID: 7903
		public string[] gameplayFeatures_NAME_EN;

		// Token: 0x04001EE0 RID: 7904
		public string[] gameplayFeatures_NAME_GE;

		// Token: 0x04001EE1 RID: 7905
		public string[] gameplayFeatures_NAME_TU;

		// Token: 0x04001EE2 RID: 7906
		public string[] gameplayFeatures_NAME_CH;

		// Token: 0x04001EE3 RID: 7907
		public string[] gameplayFeatures_NAME_FR;

		// Token: 0x04001EE4 RID: 7908
		public string[] gameplayFeatures_NAME_PB;

		// Token: 0x04001EE5 RID: 7909
		public string[] gameplayFeatures_NAME_CT;

		// Token: 0x04001EE6 RID: 7910
		public string[] gameplayFeatures_NAME_HU;

		// Token: 0x04001EE7 RID: 7911
		public string[] gameplayFeatures_NAME_ES;

		// Token: 0x04001EE8 RID: 7912
		public string[] gameplayFeatures_NAME_CZ;

		// Token: 0x04001EE9 RID: 7913
		public string[] gameplayFeatures_NAME_KO;

		// Token: 0x04001EEA RID: 7914
		public string[] gameplayFeatures_NAME_RU;

		// Token: 0x04001EEB RID: 7915
		public string[] gameplayFeatures_NAME_IT;

		// Token: 0x04001EEC RID: 7916
		public string[] gameplayFeatures_NAME_AR;

		// Token: 0x04001EED RID: 7917
		public string[] gameplayFeatures_NAME_JA;

		// Token: 0x04001EEE RID: 7918
		public string[] gameplayFeatures_NAME_PL;

		// Token: 0x04001EEF RID: 7919
		public string[] gameplayFeatures_DESC_EN;

		// Token: 0x04001EF0 RID: 7920
		public string[] gameplayFeatures_DESC_GE;

		// Token: 0x04001EF1 RID: 7921
		public string[] gameplayFeatures_DESC_TU;

		// Token: 0x04001EF2 RID: 7922
		public string[] gameplayFeatures_DESC_CH;

		// Token: 0x04001EF3 RID: 7923
		public string[] gameplayFeatures_DESC_FR;

		// Token: 0x04001EF4 RID: 7924
		public string[] gameplayFeatures_DESC_PB;

		// Token: 0x04001EF5 RID: 7925
		public string[] gameplayFeatures_DESC_CT;

		// Token: 0x04001EF6 RID: 7926
		public string[] gameplayFeatures_DESC_HU;

		// Token: 0x04001EF7 RID: 7927
		public string[] gameplayFeatures_DESC_ES;

		// Token: 0x04001EF8 RID: 7928
		public string[] gameplayFeatures_DESC_CZ;

		// Token: 0x04001EF9 RID: 7929
		public string[] gameplayFeatures_DESC_KO;

		// Token: 0x04001EFA RID: 7930
		public string[] gameplayFeatures_DESC_RU;

		// Token: 0x04001EFB RID: 7931
		public string[] gameplayFeatures_DESC_IT;

		// Token: 0x04001EFC RID: 7932
		public string[] gameplayFeatures_DESC_AR;

		// Token: 0x04001EFD RID: 7933
		public string[] gameplayFeatures_DESC_JA;

		// Token: 0x04001EFE RID: 7934
		public string[] gameplayFeatures_DESC_PL;
	}

	// Token: 0x020002B2 RID: 690
	public struct s_EngineFeatures : NetworkMessage
	{
		// Token: 0x04001EFF RID: 7935
		public int[] engineFeatures_TYP;

		// Token: 0x04001F00 RID: 7936
		public int[] engineFeatures_RES_POINTS;

		// Token: 0x04001F01 RID: 7937
		public float[] engineFeatures_RES_POINTS_LEFT;

		// Token: 0x04001F02 RID: 7938
		public int[] engineFeatures_PRICE;

		// Token: 0x04001F03 RID: 7939
		public int[] engineFeatures_DEV_COSTS;

		// Token: 0x04001F04 RID: 7940
		public int[] engineFeatures_TECH;

		// Token: 0x04001F05 RID: 7941
		public int[] engineFeatures_DATE_YEAR;

		// Token: 0x04001F06 RID: 7942
		public int[] engineFeatures_DATE_MONTH;

		// Token: 0x04001F07 RID: 7943
		public int[] engineFeatures_GAMEPLAY;

		// Token: 0x04001F08 RID: 7944
		public int[] engineFeatures_GRAPHIC;

		// Token: 0x04001F09 RID: 7945
		public int[] engineFeatures_SOUND;

		// Token: 0x04001F0A RID: 7946
		public int[] engineFeatures_TECHNIK;

		// Token: 0x04001F0B RID: 7947
		public int[] engineFeatures_LEVEL;

		// Token: 0x04001F0C RID: 7948
		public bool[] engineFeatures_UNLOCK;

		// Token: 0x04001F0D RID: 7949
		public string[] engineFeatures_ICONFILE;

		// Token: 0x04001F0E RID: 7950
		public string[] engineFeatures_NAME_EN;

		// Token: 0x04001F0F RID: 7951
		public string[] engineFeatures_NAME_GE;

		// Token: 0x04001F10 RID: 7952
		public string[] engineFeatures_NAME_TU;

		// Token: 0x04001F11 RID: 7953
		public string[] engineFeatures_NAME_CH;

		// Token: 0x04001F12 RID: 7954
		public string[] engineFeatures_NAME_FR;

		// Token: 0x04001F13 RID: 7955
		public string[] engineFeatures_NAME_PB;

		// Token: 0x04001F14 RID: 7956
		public string[] engineFeatures_NAME_CT;

		// Token: 0x04001F15 RID: 7957
		public string[] engineFeatures_NAME_HU;

		// Token: 0x04001F16 RID: 7958
		public string[] engineFeatures_NAME_ES;

		// Token: 0x04001F17 RID: 7959
		public string[] engineFeatures_NAME_CZ;

		// Token: 0x04001F18 RID: 7960
		public string[] engineFeatures_NAME_KO;

		// Token: 0x04001F19 RID: 7961
		public string[] engineFeatures_NAME_AR;

		// Token: 0x04001F1A RID: 7962
		public string[] engineFeatures_NAME_RU;

		// Token: 0x04001F1B RID: 7963
		public string[] engineFeatures_NAME_IT;

		// Token: 0x04001F1C RID: 7964
		public string[] engineFeatures_NAME_JA;

		// Token: 0x04001F1D RID: 7965
		public string[] engineFeatures_NAME_PL;

		// Token: 0x04001F1E RID: 7966
		public string[] engineFeatures_DESC_EN;

		// Token: 0x04001F1F RID: 7967
		public string[] engineFeatures_DESC_GE;

		// Token: 0x04001F20 RID: 7968
		public string[] engineFeatures_DESC_TU;

		// Token: 0x04001F21 RID: 7969
		public string[] engineFeatures_DESC_CH;

		// Token: 0x04001F22 RID: 7970
		public string[] engineFeatures_DESC_FR;

		// Token: 0x04001F23 RID: 7971
		public string[] engineFeatures_DESC_PB;

		// Token: 0x04001F24 RID: 7972
		public string[] engineFeatures_DESC_CT;

		// Token: 0x04001F25 RID: 7973
		public string[] engineFeatures_DESC_HU;

		// Token: 0x04001F26 RID: 7974
		public string[] engineFeatures_DESC_ES;

		// Token: 0x04001F27 RID: 7975
		public string[] engineFeatures_DESC_CZ;

		// Token: 0x04001F28 RID: 7976
		public string[] engineFeatures_DESC_KO;

		// Token: 0x04001F29 RID: 7977
		public string[] engineFeatures_DESC_AR;

		// Token: 0x04001F2A RID: 7978
		public string[] engineFeatures_DESC_RU;

		// Token: 0x04001F2B RID: 7979
		public string[] engineFeatures_DESC_IT;

		// Token: 0x04001F2C RID: 7980
		public string[] engineFeatures_DESC_JA;

		// Token: 0x04001F2D RID: 7981
		public string[] engineFeatures_DESC_PL;
	}

	// Token: 0x020002B3 RID: 691
	public struct s_HardwareFeatures : NetworkMessage
	{
		// Token: 0x04001F2E RID: 7982
		public string[] hardFeat_ICONFILE;

		// Token: 0x04001F2F RID: 7983
		public int[] hardFeat_RES_POINTS;

		// Token: 0x04001F30 RID: 7984
		public float[] hardFeat_RES_POINTS_LEFT;

		// Token: 0x04001F31 RID: 7985
		public int[] hardFeat_PRICE;

		// Token: 0x04001F32 RID: 7986
		public int[] hardFeat_DEV_COSTS;

		// Token: 0x04001F33 RID: 7987
		public int[] hardFeat_DATE_YEAR;

		// Token: 0x04001F34 RID: 7988
		public int[] hardFeat_DATE_MONTH;

		// Token: 0x04001F35 RID: 7989
		public bool[] hardFeat_UNLOCK;

		// Token: 0x04001F36 RID: 7990
		public bool[] hardFeat_ONLYSTATIONARY;

		// Token: 0x04001F37 RID: 7991
		public bool[] hardFeat_ONLYHANDHELD;

		// Token: 0x04001F38 RID: 7992
		public bool[] hardFeat_NEEDINTERNET;

		// Token: 0x04001F39 RID: 7993
		public float[] hardFeat_QUALITY;

		// Token: 0x04001F3A RID: 7994
		public string[] hardFeat_NAME_EN;

		// Token: 0x04001F3B RID: 7995
		public string[] hardFeat_NAME_GE;

		// Token: 0x04001F3C RID: 7996
		public string[] hardFeat_NAME_TU;

		// Token: 0x04001F3D RID: 7997
		public string[] hardFeat_NAME_CH;

		// Token: 0x04001F3E RID: 7998
		public string[] hardFeat_NAME_FR;

		// Token: 0x04001F3F RID: 7999
		public string[] hardFeat_NAME_PB;

		// Token: 0x04001F40 RID: 8000
		public string[] hardFeat_NAME_CT;

		// Token: 0x04001F41 RID: 8001
		public string[] hardFeat_NAME_HU;

		// Token: 0x04001F42 RID: 8002
		public string[] hardFeat_NAME_ES;

		// Token: 0x04001F43 RID: 8003
		public string[] hardFeat_NAME_CZ;

		// Token: 0x04001F44 RID: 8004
		public string[] hardFeat_NAME_KO;

		// Token: 0x04001F45 RID: 8005
		public string[] hardFeat_NAME_AR;

		// Token: 0x04001F46 RID: 8006
		public string[] hardFeat_NAME_RU;

		// Token: 0x04001F47 RID: 8007
		public string[] hardFeat_NAME_IT;

		// Token: 0x04001F48 RID: 8008
		public string[] hardFeat_NAME_JA;

		// Token: 0x04001F49 RID: 8009
		public string[] hardFeat_NAME_PL;

		// Token: 0x04001F4A RID: 8010
		public string[] hardFeat_DESC_EN;

		// Token: 0x04001F4B RID: 8011
		public string[] hardFeat_DESC_GE;

		// Token: 0x04001F4C RID: 8012
		public string[] hardFeat_DESC_TU;

		// Token: 0x04001F4D RID: 8013
		public string[] hardFeat_DESC_CH;

		// Token: 0x04001F4E RID: 8014
		public string[] hardFeat_DESC_FR;

		// Token: 0x04001F4F RID: 8015
		public string[] hardFeat_DESC_PB;

		// Token: 0x04001F50 RID: 8016
		public string[] hardFeat_DESC_CT;

		// Token: 0x04001F51 RID: 8017
		public string[] hardFeat_DESC_HU;

		// Token: 0x04001F52 RID: 8018
		public string[] hardFeat_DESC_ES;

		// Token: 0x04001F53 RID: 8019
		public string[] hardFeat_DESC_CZ;

		// Token: 0x04001F54 RID: 8020
		public string[] hardFeat_DESC_KO;

		// Token: 0x04001F55 RID: 8021
		public string[] hardFeat_DESC_AR;

		// Token: 0x04001F56 RID: 8022
		public string[] hardFeat_DESC_RU;

		// Token: 0x04001F57 RID: 8023
		public string[] hardFeat_DESC_IT;

		// Token: 0x04001F58 RID: 8024
		public string[] hardFeat_DESC_JA;

		// Token: 0x04001F59 RID: 8025
		public string[] hardFeat_DESC_PL;
	}

	// Token: 0x020002B4 RID: 692
	public struct s_Hardware : NetworkMessage
	{
		// Token: 0x04001F5A RID: 8026
		public string[] hardware_ICONFILE;

		// Token: 0x04001F5B RID: 8027
		public int[] hardware_TYP;

		// Token: 0x04001F5C RID: 8028
		public int[] hardware_RES_POINTS;

		// Token: 0x04001F5D RID: 8029
		public float[] hardware_RES_POINTS_LEFT;

		// Token: 0x04001F5E RID: 8030
		public int[] hardware_PRICE;

		// Token: 0x04001F5F RID: 8031
		public int[] hardware_DEV_COSTS;

		// Token: 0x04001F60 RID: 8032
		public int[] hardware_TECH;

		// Token: 0x04001F61 RID: 8033
		public int[] hardware_DATE_YEAR;

		// Token: 0x04001F62 RID: 8034
		public int[] hardware_DATE_MONTH;

		// Token: 0x04001F63 RID: 8035
		public bool[] hardware_UNLOCK;

		// Token: 0x04001F64 RID: 8036
		public bool[] hardware_ONLYSTATIONARY;

		// Token: 0x04001F65 RID: 8037
		public bool[] hardware_ONLYHANDHELD;

		// Token: 0x04001F66 RID: 8038
		public int[] hardware_NEED1;

		// Token: 0x04001F67 RID: 8039
		public int[] hardware_NEED2;

		// Token: 0x04001F68 RID: 8040
		public string[] hardware_NAME_EN;

		// Token: 0x04001F69 RID: 8041
		public string[] hardware_NAME_GE;

		// Token: 0x04001F6A RID: 8042
		public string[] hardware_NAME_TU;

		// Token: 0x04001F6B RID: 8043
		public string[] hardware_NAME_CH;

		// Token: 0x04001F6C RID: 8044
		public string[] hardware_NAME_FR;

		// Token: 0x04001F6D RID: 8045
		public string[] hardware_NAME_PB;

		// Token: 0x04001F6E RID: 8046
		public string[] hardware_NAME_CT;

		// Token: 0x04001F6F RID: 8047
		public string[] hardware_NAME_HU;

		// Token: 0x04001F70 RID: 8048
		public string[] hardware_NAME_ES;

		// Token: 0x04001F71 RID: 8049
		public string[] hardware_NAME_CZ;

		// Token: 0x04001F72 RID: 8050
		public string[] hardware_NAME_KO;

		// Token: 0x04001F73 RID: 8051
		public string[] hardware_NAME_AR;

		// Token: 0x04001F74 RID: 8052
		public string[] hardware_NAME_RU;

		// Token: 0x04001F75 RID: 8053
		public string[] hardware_NAME_IT;

		// Token: 0x04001F76 RID: 8054
		public string[] hardware_NAME_JA;

		// Token: 0x04001F77 RID: 8055
		public string[] hardware_NAME_PL;

		// Token: 0x04001F78 RID: 8056
		public string[] hardware_DESC_EN;

		// Token: 0x04001F79 RID: 8057
		public string[] hardware_DESC_GE;

		// Token: 0x04001F7A RID: 8058
		public string[] hardware_DESC_TU;

		// Token: 0x04001F7B RID: 8059
		public string[] hardware_DESC_CH;

		// Token: 0x04001F7C RID: 8060
		public string[] hardware_DESC_FR;

		// Token: 0x04001F7D RID: 8061
		public string[] hardware_DESC_PB;

		// Token: 0x04001F7E RID: 8062
		public string[] hardware_DESC_CT;

		// Token: 0x04001F7F RID: 8063
		public string[] hardware_DESC_HU;

		// Token: 0x04001F80 RID: 8064
		public string[] hardware_DESC_ES;

		// Token: 0x04001F81 RID: 8065
		public string[] hardware_DESC_CZ;

		// Token: 0x04001F82 RID: 8066
		public string[] hardware_DESC_KO;

		// Token: 0x04001F83 RID: 8067
		public string[] hardware_DESC_AR;

		// Token: 0x04001F84 RID: 8068
		public string[] hardware_DESC_RU;

		// Token: 0x04001F85 RID: 8069
		public string[] hardware_DESC_IT;

		// Token: 0x04001F86 RID: 8070
		public string[] hardware_DESC_JA;

		// Token: 0x04001F87 RID: 8071
		public string[] hardware_DESC_PL;
	}

	// Token: 0x020002B5 RID: 693
	public struct s_AntiCheat : NetworkMessage
	{
		// Token: 0x04001F88 RID: 8072
		public int myID;

		// Token: 0x04001F89 RID: 8073
		public int date_year;

		// Token: 0x04001F8A RID: 8074
		public int date_month;

		// Token: 0x04001F8B RID: 8075
		public int price;

		// Token: 0x04001F8C RID: 8076
		public int dev_costs;

		// Token: 0x04001F8D RID: 8077
		public string name_EN;

		// Token: 0x04001F8E RID: 8078
		public string name_GE;

		// Token: 0x04001F8F RID: 8079
		public string name_TU;

		// Token: 0x04001F90 RID: 8080
		public string name_CH;

		// Token: 0x04001F91 RID: 8081
		public string name_FR;

		// Token: 0x04001F92 RID: 8082
		public string name_CT;

		// Token: 0x04001F93 RID: 8083
		public string name_RU;

		// Token: 0x04001F94 RID: 8084
		public string name_IT;

		// Token: 0x04001F95 RID: 8085
		public string name_JA;

		// Token: 0x04001F96 RID: 8086
		public bool isUnlocked;

		// Token: 0x04001F97 RID: 8087
		public float effekt;

		// Token: 0x04001F98 RID: 8088
		public bool neverLooseEffect;
	}

	// Token: 0x020002B6 RID: 694
	public struct s_CopyProtect : NetworkMessage
	{
		// Token: 0x04001F99 RID: 8089
		public int myID;

		// Token: 0x04001F9A RID: 8090
		public int date_year;

		// Token: 0x04001F9B RID: 8091
		public int date_month;

		// Token: 0x04001F9C RID: 8092
		public int price;

		// Token: 0x04001F9D RID: 8093
		public int dev_costs;

		// Token: 0x04001F9E RID: 8094
		public string name_EN;

		// Token: 0x04001F9F RID: 8095
		public string name_GE;

		// Token: 0x04001FA0 RID: 8096
		public string name_TU;

		// Token: 0x04001FA1 RID: 8097
		public string name_CH;

		// Token: 0x04001FA2 RID: 8098
		public string name_FR;

		// Token: 0x04001FA3 RID: 8099
		public string name_CT;

		// Token: 0x04001FA4 RID: 8100
		public string name_RU;

		// Token: 0x04001FA5 RID: 8101
		public string name_IT;

		// Token: 0x04001FA6 RID: 8102
		public string name_JA;

		// Token: 0x04001FA7 RID: 8103
		public bool isUnlocked;

		// Token: 0x04001FA8 RID: 8104
		public float effekt;

		// Token: 0x04001FA9 RID: 8105
		public bool neverLooseEffect;
	}

	// Token: 0x020002B7 RID: 695
	public struct s_NpcEngine : NetworkMessage
	{
		// Token: 0x04001FAA RID: 8106
		public int myID;

		// Token: 0x04001FAB RID: 8107
		public int ownerID;

		// Token: 0x04001FAC RID: 8108
		public bool isUnlocked;

		// Token: 0x04001FAD RID: 8109
		public bool gekauft;

		// Token: 0x04001FAE RID: 8110
		public string myName;

		// Token: 0x04001FAF RID: 8111
		public int umsatz;

		// Token: 0x04001FB0 RID: 8112
		public string name_EN;

		// Token: 0x04001FB1 RID: 8113
		public string name_GE;

		// Token: 0x04001FB2 RID: 8114
		public string name_TU;

		// Token: 0x04001FB3 RID: 8115
		public string name_CH;

		// Token: 0x04001FB4 RID: 8116
		public string name_FR;

		// Token: 0x04001FB5 RID: 8117
		public string name_HU;

		// Token: 0x04001FB6 RID: 8118
		public string name_CT;

		// Token: 0x04001FB7 RID: 8119
		public string name_CZ;

		// Token: 0x04001FB8 RID: 8120
		public string name_PB;

		// Token: 0x04001FB9 RID: 8121
		public string name_IT;

		// Token: 0x04001FBA RID: 8122
		public string name_JA;

		// Token: 0x04001FBB RID: 8123
		public bool[] features;

		// Token: 0x04001FBC RID: 8124
		public bool[] featuresInDev;

		// Token: 0x04001FBD RID: 8125
		public int spezialgenre;

		// Token: 0x04001FBE RID: 8126
		public int spezialplatform;

		// Token: 0x04001FBF RID: 8127
		public bool sellEngine;

		// Token: 0x04001FC0 RID: 8128
		public int preis;

		// Token: 0x04001FC1 RID: 8129
		public int gewinnbeteiligung;

		// Token: 0x04001FC2 RID: 8130
		public bool updating;

		// Token: 0x04001FC3 RID: 8131
		public float devPoints;

		// Token: 0x04001FC4 RID: 8132
		public float devPointsStart;

		// Token: 0x04001FC5 RID: 8133
		public int date_year;

		// Token: 0x04001FC6 RID: 8134
		public int date_month;

		// Token: 0x04001FC7 RID: 8135
		public bool[] publisherBuyed;

		// Token: 0x04001FC8 RID: 8136
		public bool archiv_engine;
	}

	// Token: 0x020002B8 RID: 696
	public struct s_Firmenwert : NetworkMessage
	{
		// Token: 0x04001FC9 RID: 8137
		public int[] publisherID;

		// Token: 0x04001FCA RID: 8138
		public long[] firmenwert;
	}

	// Token: 0x020002B9 RID: 697
	public struct s_Publisher : NetworkMessage
	{
		// Token: 0x04001FCB RID: 8139
		public int myID;

		// Token: 0x04001FCC RID: 8140
		public bool isUnlocked;

		// Token: 0x04001FCD RID: 8141
		public string name_EN;

		// Token: 0x04001FCE RID: 8142
		public string name_GE;

		// Token: 0x04001FCF RID: 8143
		public string name_TU;

		// Token: 0x04001FD0 RID: 8144
		public string name_CH;

		// Token: 0x04001FD1 RID: 8145
		public string name_FR;

		// Token: 0x04001FD2 RID: 8146
		public string name_JA;

		// Token: 0x04001FD3 RID: 8147
		public int date_year;

		// Token: 0x04001FD4 RID: 8148
		public int date_month;

		// Token: 0x04001FD5 RID: 8149
		public float stars;

		// Token: 0x04001FD6 RID: 8150
		public int logoID;

		// Token: 0x04001FD7 RID: 8151
		public bool developer;

		// Token: 0x04001FD8 RID: 8152
		public bool publisher;

		// Token: 0x04001FD9 RID: 8153
		public bool onlyMobile;

		// Token: 0x04001FDA RID: 8154
		public float share;

		// Token: 0x04001FDB RID: 8155
		public int fanGenre;

		// Token: 0x04001FDC RID: 8156
		public long firmenwert;

		// Token: 0x04001FDD RID: 8157
		public bool notForSale;

		// Token: 0x04001FDE RID: 8158
		public int lockToBuy;

		// Token: 0x04001FDF RID: 8159
		public bool isPlayer;

		// Token: 0x04001FE0 RID: 8160
		public int ownerID;

		// Token: 0x04001FE1 RID: 8161
		public int country;

		// Token: 0x04001FE2 RID: 8162
		public int[] awards;
	}

	// Token: 0x020002BA RID: 698
	public struct s_exklusivKonsolenSells : NetworkMessage
	{
		// Token: 0x04001FE3 RID: 8163
		public int gameID;

		// Token: 0x04001FE4 RID: 8164
		public long exklusivKonsolenSells;
	}

	// Token: 0x020002BB RID: 699
	public struct s_GameData : NetworkMessage
	{
		// Token: 0x04001FE5 RID: 8165
		public int gameID;

		// Token: 0x04001FE6 RID: 8166
		public long sellsTotal;

		// Token: 0x04001FE7 RID: 8167
		public long umsatzTotal;

		// Token: 0x04001FE8 RID: 8168
		public bool isOnMarket;

		// Token: 0x04001FE9 RID: 8169
		public int weeksOnMarket;

		// Token: 0x04001FEA RID: 8170
		public int userPositiv;

		// Token: 0x04001FEB RID: 8171
		public int userNegativ;

		// Token: 0x04001FEC RID: 8172
		public long costs_entwicklung;

		// Token: 0x04001FED RID: 8173
		public long costs_mitarbeiter;

		// Token: 0x04001FEE RID: 8174
		public long costs_marketing;

		// Token: 0x04001FEF RID: 8175
		public long costs_enginegebuehren;

		// Token: 0x04001FF0 RID: 8176
		public long costs_server;

		// Token: 0x04001FF1 RID: 8177
		public long costs_production;

		// Token: 0x04001FF2 RID: 8178
		public long costs_updates;

		// Token: 0x04001FF3 RID: 8179
		public int[] sellsPerWeek;

		// Token: 0x04001FF4 RID: 8180
		public int abonnements;

		// Token: 0x04001FF5 RID: 8181
		public int abonnementsWoche;

		// Token: 0x04001FF6 RID: 8182
		public int bestAbonnements;

		// Token: 0x04001FF7 RID: 8183
		public int bestChartPosition;

		// Token: 0x04001FF8 RID: 8184
		public long exklusivKonsolenSells;

		// Token: 0x04001FF9 RID: 8185
		public float ipPunkte;

		// Token: 0x04001FFA RID: 8186
		public bool pubAngebot;

		// Token: 0x04001FFB RID: 8187
		public int pubAngebot_Weeks;

		// Token: 0x04001FFC RID: 8188
		public float pubAngebot_Verhandlung;

		// Token: 0x04001FFD RID: 8189
		public bool pubAngebot_Retail;

		// Token: 0x04001FFE RID: 8190
		public bool pubAngebot_Digital;

		// Token: 0x04001FFF RID: 8191
		public int pubAngebot_Garantiesumme;

		// Token: 0x04002000 RID: 8192
		public float pubAngebot_Gewinnbeteiligung;

		// Token: 0x04002001 RID: 8193
		public bool auftragsspiel;

		// Token: 0x04002002 RID: 8194
		public int auftragsspiel_gehalt;

		// Token: 0x04002003 RID: 8195
		public int auftragsspiel_bonus;

		// Token: 0x04002004 RID: 8196
		public int auftragsspiel_zeitInWochen;

		// Token: 0x04002005 RID: 8197
		public int auftragsspiel_wochenAlsAngebot;

		// Token: 0x04002006 RID: 8198
		public bool auftragsspiel_zeitAbgelaufen;

		// Token: 0x04002007 RID: 8199
		public int auftragsspiel_mindestbewertung;

		// Token: 0x04002008 RID: 8200
		public string ipName;

		// Token: 0x04002009 RID: 8201
		public int lastChartPosition;
	}

	// Token: 0x020002BC RID: 700
	public struct s_Game : NetworkMessage
	{
		// Token: 0x0400200A RID: 8202
		public int gameID;

		// Token: 0x0400200B RID: 8203
		public string myName;

		// Token: 0x0400200C RID: 8204
		public string ipName;

		// Token: 0x0400200D RID: 8205
		public bool playerGame;

		// Token: 0x0400200E RID: 8206
		public bool inDevelopment;

		// Token: 0x0400200F RID: 8207
		public int developerID;

		// Token: 0x04002010 RID: 8208
		public int publisherID;

		// Token: 0x04002011 RID: 8209
		public int ownerID;

		// Token: 0x04002012 RID: 8210
		public int engineID;

		// Token: 0x04002013 RID: 8211
		public float hype;

		// Token: 0x04002014 RID: 8212
		public bool isOnMarket;

		// Token: 0x04002015 RID: 8213
		public bool warBeiAwards;

		// Token: 0x04002016 RID: 8214
		public int weeksOnMarket;

		// Token: 0x04002017 RID: 8215
		public int usk;

		// Token: 0x04002018 RID: 8216
		public int freigabeBudget;

		// Token: 0x04002019 RID: 8217
		public int reviewGameplay;

		// Token: 0x0400201A RID: 8218
		public int reviewGrafik;

		// Token: 0x0400201B RID: 8219
		public int reviewSound;

		// Token: 0x0400201C RID: 8220
		public int reviewSteuerung;

		// Token: 0x0400201D RID: 8221
		public int reviewTotal;

		// Token: 0x0400201E RID: 8222
		public int reviewGameplayText;

		// Token: 0x0400201F RID: 8223
		public int reviewGrafikText;

		// Token: 0x04002020 RID: 8224
		public int reviewSoundText;

		// Token: 0x04002021 RID: 8225
		public int reviewSteuerungText;

		// Token: 0x04002022 RID: 8226
		public int reviewTotalText;

		// Token: 0x04002023 RID: 8227
		public int date_year;

		// Token: 0x04002024 RID: 8228
		public int date_month;

		// Token: 0x04002025 RID: 8229
		public int date_start_year;

		// Token: 0x04002026 RID: 8230
		public int date_start_month;

		// Token: 0x04002027 RID: 8231
		public long sellsTotal;

		// Token: 0x04002028 RID: 8232
		public long umsatzTotal;

		// Token: 0x04002029 RID: 8233
		public long costs_entwicklung;

		// Token: 0x0400202A RID: 8234
		public long costs_mitarbeiter;

		// Token: 0x0400202B RID: 8235
		public long costs_marketing;

		// Token: 0x0400202C RID: 8236
		public long costs_enginegebuehren;

		// Token: 0x0400202D RID: 8237
		public long costs_server;

		// Token: 0x0400202E RID: 8238
		public long costs_production;

		// Token: 0x0400202F RID: 8239
		public long costs_updates;

		// Token: 0x04002030 RID: 8240
		public bool typ_standard;

		// Token: 0x04002031 RID: 8241
		public bool typ_nachfolger;

		// Token: 0x04002032 RID: 8242
		public int originalIP;

		// Token: 0x04002033 RID: 8243
		public int teile;

		// Token: 0x04002034 RID: 8244
		public bool typ_contractGame;

		// Token: 0x04002035 RID: 8245
		public bool typ_remaster;

		// Token: 0x04002036 RID: 8246
		public bool typ_spinoff;

		// Token: 0x04002037 RID: 8247
		public bool typ_addon;

		// Token: 0x04002038 RID: 8248
		public bool typ_addonStandalone;

		// Token: 0x04002039 RID: 8249
		public bool typ_mmoaddon;

		// Token: 0x0400203A RID: 8250
		public bool typ_bundle;

		// Token: 0x0400203B RID: 8251
		public bool typ_budget;

		// Token: 0x0400203C RID: 8252
		public bool typ_bundleAddon;

		// Token: 0x0400203D RID: 8253
		public bool typ_goty;

		// Token: 0x0400203E RID: 8254
		public int originalGameID;

		// Token: 0x0400203F RID: 8255
		public int portID;

		// Token: 0x04002040 RID: 8256
		public int mainIP;

		// Token: 0x04002041 RID: 8257
		public float ipPunkte;

		// Token: 0x04002042 RID: 8258
		public bool exklusiv;

		// Token: 0x04002043 RID: 8259
		public bool herstellerExklusiv;

		// Token: 0x04002044 RID: 8260
		public bool retro;

		// Token: 0x04002045 RID: 8261
		public bool handy;

		// Token: 0x04002046 RID: 8262
		public bool arcade;

		// Token: 0x04002047 RID: 8263
		public bool goty;

		// Token: 0x04002048 RID: 8264
		public bool nachfolger_created;

		// Token: 0x04002049 RID: 8265
		public bool remaster_created;

		// Token: 0x0400204A RID: 8266
		public bool budget_created;

		// Token: 0x0400204B RID: 8267
		public bool goty_created;

		// Token: 0x0400204C RID: 8268
		public bool trendsetter;

		// Token: 0x0400204D RID: 8269
		public bool spielbericht;

		// Token: 0x0400204E RID: 8270
		public int amountUpdates;

		// Token: 0x0400204F RID: 8271
		public float bonusSellsUpdates;

		// Token: 0x04002050 RID: 8272
		public int amountAddons;

		// Token: 0x04002051 RID: 8273
		public float bonusSellsAddons;

		// Token: 0x04002052 RID: 8274
		public int amountMMOAddons;

		// Token: 0x04002053 RID: 8275
		public float bonusSellsMMOAddons;

		// Token: 0x04002054 RID: 8276
		public float addonQuality;

		// Token: 0x04002055 RID: 8277
		public int devAktFeature;

		// Token: 0x04002056 RID: 8278
		public float devPoints;

		// Token: 0x04002057 RID: 8279
		public float devPointsStart;

		// Token: 0x04002058 RID: 8280
		public float devPoints_Gesamt;

		// Token: 0x04002059 RID: 8281
		public float devPointsStart_Gesamt;

		// Token: 0x0400205A RID: 8282
		public float points_gameplay;

		// Token: 0x0400205B RID: 8283
		public float points_grafik;

		// Token: 0x0400205C RID: 8284
		public float points_sound;

		// Token: 0x0400205D RID: 8285
		public float points_technik;

		// Token: 0x0400205E RID: 8286
		public float points_bugs;

		// Token: 0x0400205F RID: 8287
		public string beschreibung;

		// Token: 0x04002060 RID: 8288
		public int gameTyp;

		// Token: 0x04002061 RID: 8289
		public int gameSize;

		// Token: 0x04002062 RID: 8290
		public int gameZielgruppe;

		// Token: 0x04002063 RID: 8291
		public int maingenre;

		// Token: 0x04002064 RID: 8292
		public int subgenre;

		// Token: 0x04002065 RID: 8293
		public int gameMainTheme;

		// Token: 0x04002066 RID: 8294
		public int gameSubTheme;

		// Token: 0x04002067 RID: 8295
		public int gameLicence;

		// Token: 0x04002068 RID: 8296
		public int gameCopyProtect;

		// Token: 0x04002069 RID: 8297
		public int gameAntiCheat;

		// Token: 0x0400206A RID: 8298
		public int gameAP_Gameplay;

		// Token: 0x0400206B RID: 8299
		public int gameAP_Grafik;

		// Token: 0x0400206C RID: 8300
		public int gameAP_Sound;

		// Token: 0x0400206D RID: 8301
		public int gameAP_Technik;

		// Token: 0x0400206E RID: 8302
		public bool[] gameLanguage;

		// Token: 0x0400206F RID: 8303
		public bool[] gameGameplayFeatures;

		// Token: 0x04002070 RID: 8304
		public int[] gamePlatform;

		// Token: 0x04002071 RID: 8305
		public int[] gameEngineFeature;

		// Token: 0x04002072 RID: 8306
		public bool[] gameplayFeatures_DevDone;

		// Token: 0x04002073 RID: 8307
		public bool[] engineFeature_DevDone;

		// Token: 0x04002074 RID: 8308
		public bool[] gameplayStudio;

		// Token: 0x04002075 RID: 8309
		public bool[] grafikStudio;

		// Token: 0x04002076 RID: 8310
		public bool[] soundStudio;

		// Token: 0x04002077 RID: 8311
		public bool[] motionCaptureStudio;

		// Token: 0x04002078 RID: 8312
		public int[] bundleID;

		// Token: 0x04002079 RID: 8313
		public bool[] portExist;

		// Token: 0x0400207A RID: 8314
		public int[] sellsPerWeek;

		// Token: 0x0400207B RID: 8315
		public int[] verkaufspreis;

		// Token: 0x0400207C RID: 8316
		public int releaseDate;

		// Token: 0x0400207D RID: 8317
		public int abonnements;

		// Token: 0x0400207E RID: 8318
		public int abonnementsWoche;

		// Token: 0x0400207F RID: 8319
		public int aboPreis;

		// Token: 0x04002080 RID: 8320
		public bool pubOffer;

		// Token: 0x04002081 RID: 8321
		public bool pubAngebot;

		// Token: 0x04002082 RID: 8322
		public int pubAngebot_Weeks;

		// Token: 0x04002083 RID: 8323
		public float pubAngebot_Verhandlung;

		// Token: 0x04002084 RID: 8324
		public bool pubAngebot_Retail;

		// Token: 0x04002085 RID: 8325
		public bool pubAngebot_Digital;

		// Token: 0x04002086 RID: 8326
		public int pubAngebot_Garantiesumme;

		// Token: 0x04002087 RID: 8327
		public float pubAngebot_Gewinnbeteiligung;

		// Token: 0x04002088 RID: 8328
		public bool auftragsspiel;

		// Token: 0x04002089 RID: 8329
		public int auftragsspiel_gehalt;

		// Token: 0x0400208A RID: 8330
		public int auftragsspiel_bonus;

		// Token: 0x0400208B RID: 8331
		public int auftragsspiel_zeitInWochen;

		// Token: 0x0400208C RID: 8332
		public int auftragsspiel_wochenAlsAngebot;

		// Token: 0x0400208D RID: 8333
		public bool auftragsspiel_zeitAbgelaufen;

		// Token: 0x0400208E RID: 8334
		public int auftragsspiel_mindestbewertung;

		// Token: 0x0400208F RID: 8335
		public bool f2pConverted;
	}

	// Token: 0x020002BD RID: 701
	public struct s_Lizenz : NetworkMessage
	{
		// Token: 0x04002090 RID: 8336
		public int lizenzID;

		// Token: 0x04002091 RID: 8337
		public int angebot;

		// Token: 0x04002092 RID: 8338
		public float quality;
	}

	// Token: 0x020002BE RID: 702
	public struct s_Trend : NetworkMessage
	{
		// Token: 0x04002093 RID: 8339
		public int trendWeeks;

		// Token: 0x04002094 RID: 8340
		public int trendTheme;

		// Token: 0x04002095 RID: 8341
		public int trendAntiTheme;

		// Token: 0x04002096 RID: 8342
		public int trendGenre;

		// Token: 0x04002097 RID: 8343
		public int trendAntiGenre;

		// Token: 0x04002098 RID: 8344
		public int trendNextGenre;

		// Token: 0x04002099 RID: 8345
		public int trendNextAntiGenre;

		// Token: 0x0400209A RID: 8346
		public int trendNextTheme;

		// Token: 0x0400209B RID: 8347
		public int trendNextAntiTheme;
	}

	// Token: 0x020002BF RID: 703
	public struct s_GameSpeed : NetworkMessage
	{
		// Token: 0x0400209C RID: 8348
		public int speed;
	}

	// Token: 0x020002C0 RID: 704
	public struct s_Command : NetworkMessage
	{
		// Token: 0x0400209D RID: 8349
		public int command;
	}

	// Token: 0x020002C1 RID: 705
	public struct s_Save : NetworkMessage
	{
		// Token: 0x0400209E RID: 8350
		public int saveID;
	}

	// Token: 0x020002C2 RID: 706
	public struct s_Load : NetworkMessage
	{
		// Token: 0x0400209F RID: 8351
		public int saveID;
	}

	// Token: 0x020002C3 RID: 707
	public struct s_PlayerID : NetworkMessage
	{
		// Token: 0x040020A0 RID: 8352
		public int id;

		// Token: 0x040020A1 RID: 8353
		public string version;
	}

	// Token: 0x020002C4 RID: 708
	public struct s_PlayerInfos : NetworkMessage
	{
		// Token: 0x040020A2 RID: 8354
		public int id;

		// Token: 0x040020A3 RID: 8355
		public string playerName;

		// Token: 0x040020A4 RID: 8356
		public bool ready;
	}

	// Token: 0x020002C5 RID: 709
	public struct s_DeleteArbeitsmarkt : NetworkMessage
	{
		// Token: 0x040020A5 RID: 8357
		public int objectID;

		// Token: 0x040020A6 RID: 8358
		public bool eingestellt;
	}

	// Token: 0x020002C6 RID: 710
	public struct s_CreateArbeitsmarkt : NetworkMessage
	{
		// Token: 0x040020A7 RID: 8359
		public int objectID;

		// Token: 0x040020A8 RID: 8360
		public bool male;

		// Token: 0x040020A9 RID: 8361
		public string myName;

		// Token: 0x040020AA RID: 8362
		public int wochenAmArbeitsmarkt;

		// Token: 0x040020AB RID: 8363
		public int legend;

		// Token: 0x040020AC RID: 8364
		public int beruf;

		// Token: 0x040020AD RID: 8365
		public float s_gamedesign;

		// Token: 0x040020AE RID: 8366
		public float s_programmieren;

		// Token: 0x040020AF RID: 8367
		public float s_grafik;

		// Token: 0x040020B0 RID: 8368
		public float s_sound;

		// Token: 0x040020B1 RID: 8369
		public float s_pr;

		// Token: 0x040020B2 RID: 8370
		public float s_gametests;

		// Token: 0x040020B3 RID: 8371
		public float s_technik;

		// Token: 0x040020B4 RID: 8372
		public float s_forschen;

		// Token: 0x040020B5 RID: 8373
		public bool[] perks;

		// Token: 0x040020B6 RID: 8374
		public int model_body;

		// Token: 0x040020B7 RID: 8375
		public int model_eyes;

		// Token: 0x040020B8 RID: 8376
		public int model_hair;

		// Token: 0x040020B9 RID: 8377
		public int model_beard;

		// Token: 0x040020BA RID: 8378
		public int model_skinColor;

		// Token: 0x040020BB RID: 8379
		public int model_hairColor;

		// Token: 0x040020BC RID: 8380
		public int model_beardColor;

		// Token: 0x040020BD RID: 8381
		public int model_HoseColor;

		// Token: 0x040020BE RID: 8382
		public int model_ShirtColor;

		// Token: 0x040020BF RID: 8383
		public int model_Add1Color;
	}
}
