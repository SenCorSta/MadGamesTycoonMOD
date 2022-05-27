using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;


public class mpCalls : MonoBehaviour
{
	
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

	
	public string GetPlayerName(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return "---";
		}
		return player_mp.playerName;
	}

	
	public bool GetReady(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		return player_mp != null && player_mp.ready;
	}

	
	public long GetMoney(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return 0L;
		}
		return player_mp.money;
	}

	
	public int GetFans(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		if (player_mp == null)
		{
			return 0;
		}
		return player_mp.fans;
	}

	
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

	
	public bool GetPause(int id_)
	{
		player_mp player_mp = this.FindPlayer(id_);
		return player_mp != null && player_mp.playerPause;
	}

	
	public void SetPause(bool b)
	{
		player_mp player_mp = this.FindPlayer(this.mS_.myID);
		if (player_mp == null)
		{
			return;
		}
		player_mp.playerPause = b;
	}

	
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

	
	public void SetPlayersUnready()
	{
		for (int i = 1; i < this.playersMP.Count; i++)
		{
			this.playersMP[i].playerReady = false;
		}
	}

	
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

	
	public void CLIENT_Send_ObjectDelete(int id_)
	{
		Debug.Log("CLIENT_Send_Object_Delete()");
		NetworkClient.Send<mpCalls.c_ObjectDelete>(new mpCalls.c_ObjectDelete
		{
			playerID = this.mS_.myID,
			objectID = id_
		}, 0);
	}

	
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

	
	public void CLIENT_Send_Chat(string c)
	{
		Debug.Log("CLIENT_Send_Chat()");
		NetworkClient.Send<mpCalls.c_Chat>(new mpCalls.c_Chat
		{
			playerID = this.mS_.myID,
			text = c
		}, 0);
	}

	
	public void CLIENT_Get_Chat(NetworkConnection conn, mpCalls.c_Chat msg)
	{
		Debug.Log("CLIENT_Get_Chat()");
		this.guiMain_.AddChat(msg.playerID, msg.text);
		this.SERVER_Send_Chat(msg.playerID, msg.text);
	}

	
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

	
	public void CLIENT_Send_ExklusivKonsolenSells(gameScript script_, long i)
	{
		Debug.Log("CLIENT_Send_ExklusivKonsolenSells()");
		NetworkClient.Send<mpCalls.c_exklusivKonsolenSells>(new mpCalls.c_exklusivKonsolenSells
		{
			gameID = script_.myID,
			exklusivKonsolenSells = i
		}, 0);
	}

	
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

	
	public void CLIENT_Send_BuyLizenz(int objectID_)
	{
		NetworkClient.Send<mpCalls.c_BuyLizenz>(new mpCalls.c_BuyLizenz
		{
			playerID = this.mS_.myID,
			objectID = objectID_
		}, 0);
	}

	
	public void CLIENT_Get_BuyLizenz(NetworkConnection conn, mpCalls.c_BuyLizenz msg)
	{
		Debug.Log("CLIENT_Get_BuyLizenz");
		this.licences_.licence_ANGEBOT[msg.objectID] = 0;
		this.SERVER_Send_Lizenz(msg.objectID);
	}

	
	public void CLIENT_Send_DeleteArbeitsmarkt(int objectID_, bool eingestellt)
	{
		NetworkClient.Send<mpCalls.c_DeleteArbeitsmarkt>(new mpCalls.c_DeleteArbeitsmarkt
		{
			playerID = this.mS_.myID,
			objectID = objectID_,
			eingestellt = eingestellt
		}, 0);
	}

	
	public void CLIENT_Get_DeleteArbeitsmarkt(NetworkConnection conn, mpCalls.c_DeleteArbeitsmarkt msg)
	{
		Debug.Log("CLIENT_Get_DeleteArbeitsmarkt");
		GameObject gameObject = GameObject.Find("AA_" + msg.objectID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<charArbeitsmarkt>().RemoveFromArbeitsmarkt(msg.eingestellt);
		}
	}

	
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

	
	public void SERVER_Send_PlayerLeave(int playerID_)
	{
		Debug.Log("SERVER_Send_PlayerLeave()");
		NetworkServer.SendToAll<mpCalls.s_PlayerLeave>(new mpCalls.s_PlayerLeave
		{
			playerID = playerID_
		}, 0, false);
	}

	
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

	
	public void SERVER_Send_ObjectDelete(int objectID_)
	{
		Debug.Log("SERVER_Send_ObjectDelete()");
		NetworkServer.SendToAll<mpCalls.s_ObjectDelete>(new mpCalls.s_ObjectDelete
		{
			playerID = this.mS_.myID,
			objectID = objectID_
		}, 0, false);
	}

	
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

	
	public void SERVER_Send_GlobalEvent(int eventID, int wochen)
	{
		Debug.Log("SERVER_Send_GlobalEvent()");
		NetworkServer.SendToAll<mpCalls.s_GlobalEvent>(new mpCalls.s_GlobalEvent
		{
			eventID = eventID,
			wochen = wochen
		}, 0, false);
	}

	
	public void SERVER_Get_GlobalEvent(NetworkConnection conn, mpCalls.s_GlobalEvent msg)
	{
		Debug.Log("SERVER_Get_GlobalEvent()");
		this.FindScripts();
		this.mS_.SetGlobalEvent(msg.eventID);
		this.mS_.globalEventWeeks = msg.wochen;
		base.StartCoroutine(this.iSERVER_Get_GlobalEvent(msg.eventID, msg.wochen));
	}

	
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

	
	public void SERVER_Send_EngineAbrechnung(int toPlayer, int gameID)
	{
		Debug.Log("SERVER_Send_EngineAbrechnung()");
		NetworkServer.SendToAll<mpCalls.s_EngineAbrechnung>(new mpCalls.s_EngineAbrechnung
		{
			toPlayerID = toPlayer,
			gameID = gameID
		}, 0, false);
	}

	
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

	
	public void SERVER_Get_Awards(NetworkConnection conn, mpCalls.s_Awards msg)
	{
		Debug.Log("SERVER_Get_Awards()");
		Menu_Awards component = this.guiMain_.uiObjects[143].GetComponent<Menu_Awards>();
		component.gameObject.SetActive(true);
		component.Multiplayer_FindWinners(msg.bestGrafik, msg.bestSound, msg.bestStudio, msg.bestPublisher, msg.bestGame, msg.badGame);
		this.mS_.MadGamesAward(true);
	}

	
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

	
	public void SERVER_Send_GenreBeliebtheit()
	{
		Debug.Log("SERVER_Send_GenreBeliebtheit()");
		NetworkServer.SendToAll<mpCalls.s_GenreBeliebtheit>(new mpCalls.s_GenreBeliebtheit
		{
			genreBeliebtheit = (float[])this.genres_.genres_BELIEBTHEIT.Clone()
		}, 0, false);
	}

	
	public void SERVER_Get_GenreBeliebtheit(NetworkConnection conn, mpCalls.s_GenreBeliebtheit msg)
	{
		Debug.Log("SERVER_GenreBeliebtheit()");
		this.genres_.genres_BELIEBTHEIT = (float[])msg.genreBeliebtheit.Clone();
	}

	
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

	
	public void SERVER_Send_Chat(int playerID_, string c)
	{
		Debug.Log("SERVER_Send_Chat()");
		NetworkServer.SendToAll<mpCalls.s_Chat>(new mpCalls.s_Chat
		{
			playerID = playerID_,
			text = c
		}, 0, false);
	}

	
	public void SERVER_Get_Chat(NetworkConnection conn, mpCalls.s_Chat msg)
	{
		Debug.Log("SERVER_Get_Chat()");
		this.guiMain_.AddChat(msg.playerID, msg.text);
	}

	
	public void SERVER_Send_ExklusivKonsolenSells(gameScript script_, long i)
	{
		Debug.Log("SERVER_Send_ExklusivKonsolenSells()");
		NetworkServer.SendToAll<mpCalls.s_exklusivKonsolenSells>(new mpCalls.s_exklusivKonsolenSells
		{
			gameID = script_.myID,
			exklusivKonsolenSells = i
		}, 0, false);
	}

	
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

	
	public void SERVER_Get_Lizenz(NetworkConnection conn, mpCalls.s_Lizenz msg)
	{
		Debug.Log("SERVER_Get_Lizenz()");
		if (this.licences_.licence_ANGEBOT.Length >= msg.lizenzID)
		{
			this.licences_.licence_ANGEBOT[msg.lizenzID] = msg.angebot;
			this.licences_.licence_QUALITY[msg.lizenzID] = msg.quality;
		}
	}

	
	public void SERVER_Send_Difficulty()
	{
		Debug.Log("SERVER_Send_Difficulty()");
		NetworkServer.SendToAll<mpCalls.s_Difficulty>(new mpCalls.s_Difficulty
		{
			difficulty = this.mS_.difficulty
		}, 0, false);
	}

	
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

	
	public void SERVER_Send_Office()
	{
		Debug.Log("SERVER_Send_Office()");
		NetworkServer.SendToAll<mpCalls.s_Office>(new mpCalls.s_Office
		{
			office = this.mS_.office
		}, 0, false);
	}

	
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

	
	public void SERVER_Send_Startjahr()
	{
		Debug.Log("SERVER_Send_Startjahr()");
		NetworkServer.SendToAll<mpCalls.s_Startjahr>(new mpCalls.s_Startjahr
		{
			startjahr = this.mpMain_.uiObjects[32].GetComponent<Dropdown>().value
		}, 0, false);
	}

	
	public void SERVER_Get_Startjahr(NetworkConnection conn, mpCalls.s_Startjahr msg)
	{
		Debug.Log("SERVER_Get_Startjahr()");
		this.FindScripts();
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[32].GetComponent<Dropdown>().value = msg.startjahr;
		}
	}

	
	public void SERVER_Send_Spielgeschwindigkeit()
	{
		Debug.Log("SERVER_Send_Spielgeschwindigkeit()");
		NetworkServer.SendToAll<mpCalls.s_Spielgeschwindigkeit>(new mpCalls.s_Spielgeschwindigkeit
		{
			gamespeed = this.mpMain_.uiObjects[44].GetComponent<Dropdown>().value
		}, 0, false);
	}

	
	public void SERVER_Get_Spielgeschwindigkeit(NetworkConnection conn, mpCalls.s_Spielgeschwindigkeit msg)
	{
		Debug.Log("SERVER_Send_Spielgeschwindigkeit()");
		this.FindScripts();
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().uiObjects[44].GetComponent<Dropdown>().value = msg.gamespeed;
		}
	}

	
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

	
	public void SERVER_Send_DeleteArbeitsmarkt(int objectID_, bool eingestellt)
	{
		Debug.Log("SERVER_Send_DeleteArbeitsmarkt()");
		NetworkServer.SendToAll<mpCalls.s_DeleteArbeitsmarkt>(new mpCalls.s_DeleteArbeitsmarkt
		{
			objectID = objectID_,
			eingestellt = eingestellt
		}, 0, false);
	}

	
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

	
	public void SERVER_Send_GameSpeed(int speed)
	{
		Debug.Log("SERVER_Send_GameSpeed()");
		NetworkServer.SendToAll<mpCalls.s_GameSpeed>(new mpCalls.s_GameSpeed
		{
			speed = speed
		}, 0, false);
	}

	
	public void SERVER_Get_GameSpeed(NetworkConnection conn, mpCalls.s_GameSpeed msg)
	{
		Debug.Log("SERVER_Get_GameSpeed()");
		this.mS_.SetGameSpeed((float)msg.speed);
	}

	
	public void SERVER_Send_Command(int command)
	{
		Debug.Log("SERVER_Send_Command() " + command.ToString());
		NetworkServer.SendToAll<mpCalls.s_Command>(new mpCalls.s_Command
		{
			command = command
		}, 0, false);
	}

	
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

	
	public void SERVER_Send_Load(int saveID)
	{
		Debug.Log("SERVER_Send_Load() " + saveID.ToString());
		NetworkServer.SendToAll<mpCalls.s_Load>(new mpCalls.s_Load
		{
			saveID = saveID
		}, 0, false);
	}

	
	public void SERVER_Get_Load(NetworkConnection conn, mpCalls.s_Load msg)
	{
		Debug.Log("SERVER_Get_Load()");
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[238]);
		this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().BUTTON_LoadGame(msg.saveID);
	}

	
	public void SERVER_Send_Save(int saveID)
	{
		Debug.Log("SERVER_Send_Save() " + saveID.ToString());
		NetworkServer.SendToAll<mpCalls.s_Save>(new mpCalls.s_Save
		{
			saveID = saveID
		}, 0, false);
	}

	
	public void SERVER_Get_Save(NetworkConnection conn, mpCalls.s_Save msg)
	{
		Debug.Log("SERVER_Get_Save()");
		this.save_.Save(msg.saveID);
	}

	
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

	
	public bool isServer;

	
	public bool isClient;

	
	public bool disableSend;

	
	public List<player_mp> playersMP = new List<player_mp>();

	
	public mpPlayer[] players = new mpPlayer[4];

	
	public float timer;

	
	public float timer10Secs;

	
	public mainScript mS_;

	
	public GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private mpMain mpMain_;

	
	private arbeitsmarkt arbeitsmarkt_;

	
	private licences licences_;

	
	private games games_;

	
	private engineFeatures eF_;

	
	private genres genres_;

	
	private savegameScript save_;

	
	private mapScript mapScript_;

	
	private gameplayFeatures gF_;

	
	private hardware hardware_;

	
	private hardwareFeatures hardwareFeatures_;

	
	private forschungSonstiges fS_;

	
	private themes themes_;

	
	private platforms platforms_;

	
	private publisher publisher_;

	
	private copyProtect copyProtect_;

	
	private anitCheat antiCheat_;

	
	public struct c_Publisher : NetworkMessage
	{
		
		public int myID;

		
		public bool isUnlocked;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_JA;

		
		public int date_year;

		
		public int date_month;

		
		public float stars;

		
		public int logoID;

		
		public bool developer;

		
		public bool publisher;

		
		public bool onlyMobile;

		
		public float share;

		
		public int fanGenre;

		
		public long firmenwert;

		
		public bool notForSale;

		
		public int lockToBuy;

		
		public bool isPlayer;

		
		public int ownerID;

		
		public int country;

		
		public int[] awards;
	}

	
	public struct c_Forschung : NetworkMessage
	{
		
		public int playerID;

		
		public bool[] forschungSonstiges;

		
		public bool[] genres;

		
		public bool[] themes;

		
		public bool[] engineFeatures;

		
		public bool[] gameplayFeatures;

		
		public bool[] hardware;

		
		public bool[] hardwareFeatures;
	}

	
	public struct c_Help : NetworkMessage
	{
		
		public int playerID;

		
		public int toPlayerID;

		
		public int what;

		
		public int valueA;

		
		public int valueB;

		
		public int valueC;
	}

	
	public struct c_ObjectDelete : NetworkMessage
	{
		
		public int playerID;

		
		public int objectID;
	}

	
	public struct c_Object : NetworkMessage
	{
		
		public int playerID;

		
		public int objectID;

		
		public int typ;

		
		public float x;

		
		public float y;

		
		public float rot;
	}

	
	public struct c_Map : NetworkMessage
	{
		
		public int playerID;

		
		public byte x;

		
		public byte y;

		
		public int id;

		
		public int typ;

		
		public int door;

		
		public byte window;
	}

	
	public struct c_Trend : NetworkMessage
	{
		
		public int trendWeeks;

		
		public int trendTheme;

		
		public int trendAntiTheme;

		
		public int trendGenre;

		
		public int trendAntiGenre;
	}

	
	public struct c_Payment : NetworkMessage
	{
		
		public int playerID;

		
		public int toPlayerID;

		
		public int what;

		
		public int money;
	}

	
	public struct c_Engine : NetworkMessage
	{
		
		public int myID;

		
		public int ownerID;

		
		public bool isUnlocked;

		
		public bool gekauft;

		
		public string myName;

		
		public bool[] features;

		
		public int spezialgenre;

		
		public int spezialplatform;

		
		public bool sellEngine;

		
		public int preis;

		
		public int gewinnbeteiligung;
	}

	
	public struct c_Platform : NetworkMessage
	{
		
		public int myID;

		
		public int date_year;

		
		public int date_month;

		
		public int date_year_end;

		
		public int date_month_end;

		
		public int price;

		
		public int dev_costs;

		
		public int tech;

		
		public int typ;

		
		public float marktanteil;

		
		public int[] needFeatures;

		
		public int units;

		
		public int units_max;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_HU;

		
		public string name_JA;

		
		public string manufacturer_EN;

		
		public string manufacturer_GE;

		
		public string manufacturer_TU;

		
		public string manufacturer_CH;

		
		public string manufacturer_FR;

		
		public string manufacturer_HU;

		
		public string manufacturer_JA;

		
		public string pic1_file;

		
		public string pic2_file;

		
		public int pic2_year;

		
		public int games;

		
		public int exklusivGames;

		
		public int erfahrung;

		
		public bool isUnlocked;

		
		public bool inBesitz;

		
		public bool vomMarktGenommen;

		
		public int complex;

		
		public bool internet;

		
		public float powerFromMarket;

		
		public string myName;

		
		public int ownerID;

		
		public int gameID;

		
		public int anzController;

		
		public float conHueShift;

		
		public float conSaturation;

		
		public int component_cpu;

		
		public int component_gfx;

		
		public int component_ram;

		
		public int component_hdd;

		
		public int component_sfx;

		
		public int component_cooling;

		
		public int component_disc;

		
		public int component_controller;

		
		public int component_case;

		
		public int component_monitor;

		
		public bool[] hwFeatures;

		
		public float devPoints;

		
		public float devPointsStart;

		
		public long entwicklungsKosten;

		
		public long einnahmen;

		
		public float hype;

		
		public int costs_marketing;

		
		public int costs_mitarbeiter;

		
		public int startProduktionskosten;

		
		public int verkaufspreis;

		
		public float kostenreduktion;

		
		public bool autoPreis;

		
		public bool thridPartyGames;

		
		public long umsatzTotal;

		
		public long costs_production;

		
		public int[] sellsPerWeek;

		
		public int weeksOnMarket;

		
		public float review;

		
		public int performancePoints;
	}

	
	public struct c_Chat : NetworkMessage
	{
		
		public int playerID;

		
		public string text;
	}

	
	public struct c_Command : NetworkMessage
	{
		
		public int playerID;

		
		public int command;
	}

	
	public struct c_Money : NetworkMessage
	{
		
		public int playerID;

		
		public long money;

		
		public int fans;
	}

	
	public struct c_PlayerInfos : NetworkMessage
	{
		
		public int playerID;

		
		public string playerName;

		
		public bool ready;
	}

	
	public struct c_DeleteArbeitsmarkt : NetworkMessage
	{
		
		public int playerID;

		
		public int objectID;

		
		public bool eingestellt;
	}

	
	public struct c_BuyLizenz : NetworkMessage
	{
		
		public int playerID;

		
		public int objectID;
	}

	
	public struct c_exklusivKonsolenSells : NetworkMessage
	{
		
		public int gameID;

		
		public long exklusivKonsolenSells;
	}

	
	public struct c_GameData : NetworkMessage
	{
		
		public int gameID;

		
		public long sellsTotal;

		
		public long umsatzTotal;

		
		public bool isOnMarket;

		
		public int weeksOnMarket;

		
		public int userPositiv;

		
		public int userNegativ;

		
		public long costs_entwicklung;

		
		public long costs_mitarbeiter;

		
		public long costs_marketing;

		
		public long costs_enginegebuehren;

		
		public long costs_server;

		
		public long costs_production;

		
		public long costs_updates;

		
		public int[] sellsPerWeek;

		
		public int abonnements;

		
		public int abonnementsWoche;

		
		public int bestAbonnements;

		
		public int bestChartPosition;

		
		public long exklusivKonsolenSells;

		
		public float ipPunkte;

		
		public bool pubAngebot;

		
		public int pubAngebot_Weeks;

		
		public float pubAngebot_Verhandlung;

		
		public bool pubAngebot_Retail;

		
		public bool pubAngebot_Digital;

		
		public int pubAngebot_Garantiesumme;

		
		public float pubAngebot_Gewinnbeteiligung;

		
		public bool auftragsspiel;

		
		public int auftragsspiel_gehalt;

		
		public int auftragsspiel_bonus;

		
		public int auftragsspiel_zeitInWochen;

		
		public int auftragsspiel_wochenAlsAngebot;

		
		public bool auftragsspiel_zeitAbgelaufen;

		
		public int auftragsspiel_mindestbewertung;

		
		public string ipName;
	}

	
	public struct c_Game : NetworkMessage
	{
		
		public int gameID;

		
		public string myName;

		
		public string ipName;

		
		public bool playerGame;

		
		public bool inDevelopment;

		
		public int developerID;

		
		public int publisherID;

		
		public int ownerID;

		
		public int engineID;

		
		public float hype;

		
		public bool isOnMarket;

		
		public bool warBeiAwards;

		
		public int weeksOnMarket;

		
		public int usk;

		
		public int freigabeBudget;

		
		public int reviewGameplay;

		
		public int reviewGrafik;

		
		public int reviewSound;

		
		public int reviewSteuerung;

		
		public int reviewTotal;

		
		public int reviewGameplayText;

		
		public int reviewGrafikText;

		
		public int reviewSoundText;

		
		public int reviewSteuerungText;

		
		public int reviewTotalText;

		
		public int date_year;

		
		public int date_month;

		
		public int date_start_year;

		
		public int date_start_month;

		
		public long sellsTotal;

		
		public long umsatzTotal;

		
		public long costs_entwicklung;

		
		public long costs_mitarbeiter;

		
		public long costs_marketing;

		
		public long costs_enginegebuehren;

		
		public long costs_server;

		
		public long costs_production;

		
		public long costs_updates;

		
		public bool typ_standard;

		
		public bool typ_nachfolger;

		
		public int originalIP;

		
		public int teile;

		
		public bool typ_contractGame;

		
		public bool typ_remaster;

		
		public bool typ_spinoff;

		
		public bool typ_addon;

		
		public bool typ_addonStandalone;

		
		public bool typ_mmoaddon;

		
		public bool typ_bundle;

		
		public bool typ_budget;

		
		public bool typ_bundleAddon;

		
		public bool typ_goty;

		
		public int originalGameID;

		
		public int portID;

		
		public int mainIP;

		
		public float ipPunkte;

		
		public bool exklusiv;

		
		public bool herstellerExklusiv;

		
		public bool retro;

		
		public bool handy;

		
		public bool arcade;

		
		public bool goty;

		
		public bool nachfolger_created;

		
		public bool remaster_created;

		
		public bool budget_created;

		
		public bool goty_created;

		
		public bool trendsetter;

		
		public bool spielbericht;

		
		public int amountUpdates;

		
		public float bonusSellsUpdates;

		
		public int amountAddons;

		
		public float bonusSellsAddons;

		
		public int amountMMOAddons;

		
		public float bonusSellsMMOAddons;

		
		public float addonQuality;

		
		public int devAktFeature;

		
		public float devPoints;

		
		public float devPointsStart;

		
		public float devPoints_Gesamt;

		
		public float devPointsStart_Gesamt;

		
		public float points_gameplay;

		
		public float points_grafik;

		
		public float points_sound;

		
		public float points_technik;

		
		public float points_bugs;

		
		public string beschreibung;

		
		public int gameTyp;

		
		public int gameSize;

		
		public int gameZielgruppe;

		
		public int maingenre;

		
		public int subgenre;

		
		public int gameMainTheme;

		
		public int gameSubTheme;

		
		public int gameLicence;

		
		public int gameCopyProtect;

		
		public int gameAntiCheat;

		
		public int gameAP_Gameplay;

		
		public int gameAP_Grafik;

		
		public int gameAP_Sound;

		
		public int gameAP_Technik;

		
		public bool[] gameLanguage;

		
		public bool[] gameGameplayFeatures;

		
		public int[] gamePlatform;

		
		public int[] gameEngineFeature;

		
		public bool[] gameplayFeatures_DevDone;

		
		public bool[] engineFeature_DevDone;

		
		public bool[] gameplayStudio;

		
		public bool[] grafikStudio;

		
		public bool[] soundStudio;

		
		public bool[] motionCaptureStudio;

		
		public int[] bundleID;

		
		public bool[] portExist;

		
		public int[] sellsPerWeek;

		
		public int[] verkaufspreis;

		
		public int releaseDate;

		
		public int abonnements;

		
		public int abonnementsWoche;

		
		public int aboPreis;

		
		public bool pubOffer;

		
		public bool pubAngebot;

		
		public int pubAngebot_Weeks;

		
		public float pubAngebot_Verhandlung;

		
		public bool pubAngebot_Retail;

		
		public bool pubAngebot_Digital;

		
		public int pubAngebot_Garantiesumme;

		
		public float pubAngebot_Gewinnbeteiligung;

		
		public bool auftragsspiel;

		
		public int auftragsspiel_gehalt;

		
		public int auftragsspiel_bonus;

		
		public int auftragsspiel_zeitInWochen;

		
		public int auftragsspiel_wochenAlsAngebot;

		
		public bool auftragsspiel_zeitAbgelaufen;

		
		public int auftragsspiel_mindestbewertung;

		
		public bool f2pConverted;
	}

	
	public struct s_AddPlayer : NetworkMessage
	{
		
		public int playerID;
	}

	
	public struct s_Forschung : NetworkMessage
	{
		
		public int playerID;

		
		public bool[] forschungSonstiges;

		
		public bool[] genres;

		
		public bool[] themes;

		
		public bool[] engineFeatures;

		
		public bool[] gameplayFeatures;

		
		public bool[] hardware;

		
		public bool[] hardwareFeatures;
	}

	
	public struct s_PlayerLeave : NetworkMessage
	{
		
		public int playerID;
	}

	
	public struct s_GenreBeliebtheit : NetworkMessage
	{
		
		public float[] genreBeliebtheit;
	}

	
	public struct s_GenreCombination : NetworkMessage
	{
		
		public int genreSlot;

		
		public bool[] genres_COMBINATION;
	}

	
	public struct s_GenreDesign : NetworkMessage
	{
		
		public int genreSlot;

		
		public int genres_focus0;

		
		public int genres_focus1;

		
		public int genres_focus2;

		
		public int genres_focus3;

		
		public int genres_focus4;

		
		public int genres_focus5;

		
		public int genres_focus6;

		
		public int genres_focus7;

		
		public int genres_align0;

		
		public int genres_align1;

		
		public int genres_align2;
	}

	
	public struct s_Help : NetworkMessage
	{
		
		public int playerID;

		
		public int toPlayerID;

		
		public int what;

		
		public int valueA;

		
		public int valueB;

		
		public int valueC;
	}

	
	public struct s_ObjectDelete : NetworkMessage
	{
		
		public int playerID;

		
		public int objectID;
	}

	
	public struct s_Object : NetworkMessage
	{
		
		public int playerID;

		
		public int objectID;

		
		public int typ;

		
		public float x;

		
		public float y;

		
		public float rot;
	}

	
	public struct s_Map : NetworkMessage
	{
		
		public int playerID;

		
		public byte x;

		
		public byte y;

		
		public int id;

		
		public int typ;

		
		public int door;

		
		public byte window;
	}

	
	public struct s_Office : NetworkMessage
	{
		
		public int office;
	}

	
	public struct s_Difficulty : NetworkMessage
	{
		
		public int difficulty;
	}

	
	public struct s_Startjahr : NetworkMessage
	{
		
		public int startjahr;
	}

	
	public struct s_Spielgeschwindigkeit : NetworkMessage
	{
		
		public int gamespeed;
	}

	
	public struct s_GlobalEvent : NetworkMessage
	{
		
		public int eventID;

		
		public int wochen;
	}

	
	public struct s_EngineAbrechnung : NetworkMessage
	{
		
		public int toPlayerID;

		
		public int gameID;
	}

	
	public struct s_Awards : NetworkMessage
	{
		
		public int bestGrafik;

		
		public int bestSound;

		
		public int bestStudio;

		
		public int bestPublisher;

		
		public int bestGame;

		
		public int badGame;
	}

	
	public struct s_Payment : NetworkMessage
	{
		
		public int playerID;

		
		public int toPlayerID;

		
		public int what;

		
		public int money;
	}

	
	public struct s_Engine : NetworkMessage
	{
		
		public int engineID;

		
		public int ownerID;

		
		public bool isUnlocked;

		
		public bool gekauft;

		
		public string myName;

		
		public bool[] features;

		
		public int spezialgenre;

		
		public int spezialplatform;

		
		public bool sellEngine;

		
		public int preis;

		
		public int gewinnbeteiligung;
	}

	
	public struct s_Platform : NetworkMessage
	{
		
		public int myID;

		
		public int date_year;

		
		public int date_month;

		
		public int date_year_end;

		
		public int date_month_end;

		
		public int price;

		
		public int dev_costs;

		
		public int tech;

		
		public int typ;

		
		public float marktanteil;

		
		public int[] needFeatures;

		
		public int units;

		
		public int units_max;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_HU;

		
		public string name_JA;

		
		public string manufacturer_EN;

		
		public string manufacturer_GE;

		
		public string manufacturer_TU;

		
		public string manufacturer_CH;

		
		public string manufacturer_FR;

		
		public string manufacturer_HU;

		
		public string manufacturer_JA;

		
		public string pic1_file;

		
		public string pic2_file;

		
		public int pic2_year;

		
		public int games;

		
		public int exklusivGames;

		
		public int erfahrung;

		
		public bool isUnlocked;

		
		public bool inBesitz;

		
		public bool vomMarktGenommen;

		
		public int complex;

		
		public bool internet;

		
		public float powerFromMarket;

		
		public string myName;

		
		public int ownerID;

		
		public int gameID;

		
		public int anzController;

		
		public float conHueShift;

		
		public float conSaturation;

		
		public int component_cpu;

		
		public int component_gfx;

		
		public int component_ram;

		
		public int component_hdd;

		
		public int component_sfx;

		
		public int component_cooling;

		
		public int component_disc;

		
		public int component_controller;

		
		public int component_case;

		
		public int component_monitor;

		
		public bool[] hwFeatures;

		
		public float devPoints;

		
		public float devPointsStart;

		
		public long entwicklungsKosten;

		
		public long einnahmen;

		
		public float hype;

		
		public int costs_marketing;

		
		public int costs_mitarbeiter;

		
		public int startProduktionskosten;

		
		public int verkaufspreis;

		
		public float kostenreduktion;

		
		public bool autoPreis;

		
		public bool thridPartyGames;

		
		public long umsatzTotal;

		
		public long costs_production;

		
		public int[] sellsPerWeek;

		
		public int weeksOnMarket;

		
		public float review;

		
		public int performancePoints;
	}

	
	public struct s_PlatformData : NetworkMessage
	{
		
		public int platformID;

		
		public float marktanteil;

		
		public int units;

		
		public int units_max;

		
		public int date_year_end;
	}

	
	public struct s_Chat : NetworkMessage
	{
		
		public int playerID;

		
		public string text;
	}

	
	public struct s_Money : NetworkMessage
	{
		
		public int playerID;

		
		public long money;

		
		public int fans;
	}

	
	public struct s_AutoPause : NetworkMessage
	{
		
		public int playerID;

		
		public bool pause;
	}

	
	public struct s_Genres : NetworkMessage
	{
		
		public float[] genres_BELIEBTHEIT;

		
		public bool[] genres_BELIEBTHEIT_SOLL;

		
		public int[] genres_RES_POINTS;

		
		public float[] genres_RES_POINTS_LEFT;

		
		public int[] genres_PRICE;

		
		public int[] genres_DEV_COSTS;

		
		public int[] genres_DATE_YEAR;

		
		public int[] genres_DATE_MONTH;

		
		public int[] genres_LEVEL;

		
		public bool[] genres_UNLOCK;

		
		public bool[] genres_TARGETGROUP;

		
		public float[] genres_GAMEPLAY;

		
		public float[] genres_GRAPHIC;

		
		public float[] genres_SOUND;

		
		public float[] genres_CONTROL;

		
		public bool[] genres_COMBINATION;

		
		public int[] genres_FOCUS;

		
		public bool[] genres_FOCUS_KNOWN;

		
		public int[] genres_ALIGN;

		
		public bool[] genres_ALIGN_KNOWN;

		
		public string[] genres_ICONFILE;

		
		public string[] genres_NAME_EN;

		
		public string[] genres_NAME_GE;

		
		public string[] genres_NAME_TU;

		
		public string[] genres_NAME_CH;

		
		public string[] genres_NAME_FR;

		
		public string[] genres_NAME_PB;

		
		public string[] genres_NAME_HU;

		
		public string[] genres_NAME_CT;

		
		public string[] genres_NAME_ES;

		
		public string[] genres_NAME_PL;

		
		public string[] genres_NAME_CZ;

		
		public string[] genres_NAME_KO;

		
		public string[] genres_NAME_IT;

		
		public string[] genres_NAME_AR;

		
		public string[] genres_NAME_JA;

		
		public string[] genres_DESC_EN;

		
		public string[] genres_DESC_GE;

		
		public string[] genres_DESC_TU;

		
		public string[] genres_DESC_CH;

		
		public string[] genres_DESC_FR;

		
		public string[] genres_DESC_PB;

		
		public string[] genres_DESC_HU;

		
		public string[] genres_DESC_CT;

		
		public string[] genres_DESC_ES;

		
		public string[] genres_DESC_PL;

		
		public string[] genres_DESC_CZ;

		
		public string[] genres_DESC_KO;

		
		public string[] genres_DESC_IT;

		
		public string[] genres_DESC_AR;

		
		public string[] genres_DESC_JA;

		
		public int[] genres_FANS;

		
		public int[] genres_MARKT;
	}

	
	public struct s_GameplayFeatures : NetworkMessage
	{
		
		public int[] gameplayFeatures_TYP;

		
		public int[] gameplayFeatures_RES_POINTS;

		
		public float[] gameplayFeatures_RES_POINTS_LEFT;

		
		public int[] gameplayFeatures_PRICE;

		
		public int[] gameplayFeatures_DEV_COSTS;

		
		public int[] gameplayFeatures_DATE_YEAR;

		
		public int[] gameplayFeatures_DATE_MONTH;

		
		public int[] gameplayFeatures_GAMEPLAY;

		
		public int[] gameplayFeatures_GRAPHIC;

		
		public int[] gameplayFeatures_SOUND;

		
		public int[] gameplayFeatures_TECHNIK;

		
		public int[] gameplayFeatures_LEVEL;

		
		public bool[] gameplayFeatures_UNLOCK;

		
		public string[] gameplayFeatures_ICONFILE;

		
		public bool[] gameplayFeatures_GOOD;

		
		public bool[] gameplayFeatures_BAD;

		
		public bool[] gameplayFeatures_LOCKPLATFORM;

		
		public string[] gameplayFeatures_NAME_EN;

		
		public string[] gameplayFeatures_NAME_GE;

		
		public string[] gameplayFeatures_NAME_TU;

		
		public string[] gameplayFeatures_NAME_CH;

		
		public string[] gameplayFeatures_NAME_FR;

		
		public string[] gameplayFeatures_NAME_PB;

		
		public string[] gameplayFeatures_NAME_CT;

		
		public string[] gameplayFeatures_NAME_HU;

		
		public string[] gameplayFeatures_NAME_ES;

		
		public string[] gameplayFeatures_NAME_CZ;

		
		public string[] gameplayFeatures_NAME_KO;

		
		public string[] gameplayFeatures_NAME_RU;

		
		public string[] gameplayFeatures_NAME_IT;

		
		public string[] gameplayFeatures_NAME_AR;

		
		public string[] gameplayFeatures_NAME_JA;

		
		public string[] gameplayFeatures_NAME_PL;

		
		public string[] gameplayFeatures_DESC_EN;

		
		public string[] gameplayFeatures_DESC_GE;

		
		public string[] gameplayFeatures_DESC_TU;

		
		public string[] gameplayFeatures_DESC_CH;

		
		public string[] gameplayFeatures_DESC_FR;

		
		public string[] gameplayFeatures_DESC_PB;

		
		public string[] gameplayFeatures_DESC_CT;

		
		public string[] gameplayFeatures_DESC_HU;

		
		public string[] gameplayFeatures_DESC_ES;

		
		public string[] gameplayFeatures_DESC_CZ;

		
		public string[] gameplayFeatures_DESC_KO;

		
		public string[] gameplayFeatures_DESC_RU;

		
		public string[] gameplayFeatures_DESC_IT;

		
		public string[] gameplayFeatures_DESC_AR;

		
		public string[] gameplayFeatures_DESC_JA;

		
		public string[] gameplayFeatures_DESC_PL;
	}

	
	public struct s_EngineFeatures : NetworkMessage
	{
		
		public int[] engineFeatures_TYP;

		
		public int[] engineFeatures_RES_POINTS;

		
		public float[] engineFeatures_RES_POINTS_LEFT;

		
		public int[] engineFeatures_PRICE;

		
		public int[] engineFeatures_DEV_COSTS;

		
		public int[] engineFeatures_TECH;

		
		public int[] engineFeatures_DATE_YEAR;

		
		public int[] engineFeatures_DATE_MONTH;

		
		public int[] engineFeatures_GAMEPLAY;

		
		public int[] engineFeatures_GRAPHIC;

		
		public int[] engineFeatures_SOUND;

		
		public int[] engineFeatures_TECHNIK;

		
		public int[] engineFeatures_LEVEL;

		
		public bool[] engineFeatures_UNLOCK;

		
		public string[] engineFeatures_ICONFILE;

		
		public string[] engineFeatures_NAME_EN;

		
		public string[] engineFeatures_NAME_GE;

		
		public string[] engineFeatures_NAME_TU;

		
		public string[] engineFeatures_NAME_CH;

		
		public string[] engineFeatures_NAME_FR;

		
		public string[] engineFeatures_NAME_PB;

		
		public string[] engineFeatures_NAME_CT;

		
		public string[] engineFeatures_NAME_HU;

		
		public string[] engineFeatures_NAME_ES;

		
		public string[] engineFeatures_NAME_CZ;

		
		public string[] engineFeatures_NAME_KO;

		
		public string[] engineFeatures_NAME_AR;

		
		public string[] engineFeatures_NAME_RU;

		
		public string[] engineFeatures_NAME_IT;

		
		public string[] engineFeatures_NAME_JA;

		
		public string[] engineFeatures_NAME_PL;

		
		public string[] engineFeatures_DESC_EN;

		
		public string[] engineFeatures_DESC_GE;

		
		public string[] engineFeatures_DESC_TU;

		
		public string[] engineFeatures_DESC_CH;

		
		public string[] engineFeatures_DESC_FR;

		
		public string[] engineFeatures_DESC_PB;

		
		public string[] engineFeatures_DESC_CT;

		
		public string[] engineFeatures_DESC_HU;

		
		public string[] engineFeatures_DESC_ES;

		
		public string[] engineFeatures_DESC_CZ;

		
		public string[] engineFeatures_DESC_KO;

		
		public string[] engineFeatures_DESC_AR;

		
		public string[] engineFeatures_DESC_RU;

		
		public string[] engineFeatures_DESC_IT;

		
		public string[] engineFeatures_DESC_JA;

		
		public string[] engineFeatures_DESC_PL;
	}

	
	public struct s_HardwareFeatures : NetworkMessage
	{
		
		public string[] hardFeat_ICONFILE;

		
		public int[] hardFeat_RES_POINTS;

		
		public float[] hardFeat_RES_POINTS_LEFT;

		
		public int[] hardFeat_PRICE;

		
		public int[] hardFeat_DEV_COSTS;

		
		public int[] hardFeat_DATE_YEAR;

		
		public int[] hardFeat_DATE_MONTH;

		
		public bool[] hardFeat_UNLOCK;

		
		public bool[] hardFeat_ONLYSTATIONARY;

		
		public bool[] hardFeat_ONLYHANDHELD;

		
		public bool[] hardFeat_NEEDINTERNET;

		
		public float[] hardFeat_QUALITY;

		
		public string[] hardFeat_NAME_EN;

		
		public string[] hardFeat_NAME_GE;

		
		public string[] hardFeat_NAME_TU;

		
		public string[] hardFeat_NAME_CH;

		
		public string[] hardFeat_NAME_FR;

		
		public string[] hardFeat_NAME_PB;

		
		public string[] hardFeat_NAME_CT;

		
		public string[] hardFeat_NAME_HU;

		
		public string[] hardFeat_NAME_ES;

		
		public string[] hardFeat_NAME_CZ;

		
		public string[] hardFeat_NAME_KO;

		
		public string[] hardFeat_NAME_AR;

		
		public string[] hardFeat_NAME_RU;

		
		public string[] hardFeat_NAME_IT;

		
		public string[] hardFeat_NAME_JA;

		
		public string[] hardFeat_NAME_PL;

		
		public string[] hardFeat_DESC_EN;

		
		public string[] hardFeat_DESC_GE;

		
		public string[] hardFeat_DESC_TU;

		
		public string[] hardFeat_DESC_CH;

		
		public string[] hardFeat_DESC_FR;

		
		public string[] hardFeat_DESC_PB;

		
		public string[] hardFeat_DESC_CT;

		
		public string[] hardFeat_DESC_HU;

		
		public string[] hardFeat_DESC_ES;

		
		public string[] hardFeat_DESC_CZ;

		
		public string[] hardFeat_DESC_KO;

		
		public string[] hardFeat_DESC_AR;

		
		public string[] hardFeat_DESC_RU;

		
		public string[] hardFeat_DESC_IT;

		
		public string[] hardFeat_DESC_JA;

		
		public string[] hardFeat_DESC_PL;
	}

	
	public struct s_Hardware : NetworkMessage
	{
		
		public string[] hardware_ICONFILE;

		
		public int[] hardware_TYP;

		
		public int[] hardware_RES_POINTS;

		
		public float[] hardware_RES_POINTS_LEFT;

		
		public int[] hardware_PRICE;

		
		public int[] hardware_DEV_COSTS;

		
		public int[] hardware_TECH;

		
		public int[] hardware_DATE_YEAR;

		
		public int[] hardware_DATE_MONTH;

		
		public bool[] hardware_UNLOCK;

		
		public bool[] hardware_ONLYSTATIONARY;

		
		public bool[] hardware_ONLYHANDHELD;

		
		public int[] hardware_NEED1;

		
		public int[] hardware_NEED2;

		
		public string[] hardware_NAME_EN;

		
		public string[] hardware_NAME_GE;

		
		public string[] hardware_NAME_TU;

		
		public string[] hardware_NAME_CH;

		
		public string[] hardware_NAME_FR;

		
		public string[] hardware_NAME_PB;

		
		public string[] hardware_NAME_CT;

		
		public string[] hardware_NAME_HU;

		
		public string[] hardware_NAME_ES;

		
		public string[] hardware_NAME_CZ;

		
		public string[] hardware_NAME_KO;

		
		public string[] hardware_NAME_AR;

		
		public string[] hardware_NAME_RU;

		
		public string[] hardware_NAME_IT;

		
		public string[] hardware_NAME_JA;

		
		public string[] hardware_NAME_PL;

		
		public string[] hardware_DESC_EN;

		
		public string[] hardware_DESC_GE;

		
		public string[] hardware_DESC_TU;

		
		public string[] hardware_DESC_CH;

		
		public string[] hardware_DESC_FR;

		
		public string[] hardware_DESC_PB;

		
		public string[] hardware_DESC_CT;

		
		public string[] hardware_DESC_HU;

		
		public string[] hardware_DESC_ES;

		
		public string[] hardware_DESC_CZ;

		
		public string[] hardware_DESC_KO;

		
		public string[] hardware_DESC_AR;

		
		public string[] hardware_DESC_RU;

		
		public string[] hardware_DESC_IT;

		
		public string[] hardware_DESC_JA;

		
		public string[] hardware_DESC_PL;
	}

	
	public struct s_AntiCheat : NetworkMessage
	{
		
		public int myID;

		
		public int date_year;

		
		public int date_month;

		
		public int price;

		
		public int dev_costs;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_CT;

		
		public string name_RU;

		
		public string name_IT;

		
		public string name_JA;

		
		public bool isUnlocked;

		
		public float effekt;

		
		public bool neverLooseEffect;
	}

	
	public struct s_CopyProtect : NetworkMessage
	{
		
		public int myID;

		
		public int date_year;

		
		public int date_month;

		
		public int price;

		
		public int dev_costs;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_CT;

		
		public string name_RU;

		
		public string name_IT;

		
		public string name_JA;

		
		public bool isUnlocked;

		
		public float effekt;

		
		public bool neverLooseEffect;
	}

	
	public struct s_NpcEngine : NetworkMessage
	{
		
		public int myID;

		
		public int ownerID;

		
		public bool isUnlocked;

		
		public bool gekauft;

		
		public string myName;

		
		public int umsatz;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_HU;

		
		public string name_CT;

		
		public string name_CZ;

		
		public string name_PB;

		
		public string name_IT;

		
		public string name_JA;

		
		public bool[] features;

		
		public bool[] featuresInDev;

		
		public int spezialgenre;

		
		public int spezialplatform;

		
		public bool sellEngine;

		
		public int preis;

		
		public int gewinnbeteiligung;

		
		public bool updating;

		
		public float devPoints;

		
		public float devPointsStart;

		
		public int date_year;

		
		public int date_month;

		
		public bool[] publisherBuyed;

		
		public bool archiv_engine;
	}

	
	public struct s_Firmenwert : NetworkMessage
	{
		
		public int[] publisherID;

		
		public long[] firmenwert;
	}

	
	public struct s_Publisher : NetworkMessage
	{
		
		public int myID;

		
		public bool isUnlocked;

		
		public string name_EN;

		
		public string name_GE;

		
		public string name_TU;

		
		public string name_CH;

		
		public string name_FR;

		
		public string name_JA;

		
		public int date_year;

		
		public int date_month;

		
		public float stars;

		
		public int logoID;

		
		public bool developer;

		
		public bool publisher;

		
		public bool onlyMobile;

		
		public float share;

		
		public int fanGenre;

		
		public long firmenwert;

		
		public bool notForSale;

		
		public int lockToBuy;

		
		public bool isPlayer;

		
		public int ownerID;

		
		public int country;

		
		public int[] awards;
	}

	
	public struct s_exklusivKonsolenSells : NetworkMessage
	{
		
		public int gameID;

		
		public long exklusivKonsolenSells;
	}

	
	public struct s_GameData : NetworkMessage
	{
		
		public int gameID;

		
		public long sellsTotal;

		
		public long umsatzTotal;

		
		public bool isOnMarket;

		
		public int weeksOnMarket;

		
		public int userPositiv;

		
		public int userNegativ;

		
		public long costs_entwicklung;

		
		public long costs_mitarbeiter;

		
		public long costs_marketing;

		
		public long costs_enginegebuehren;

		
		public long costs_server;

		
		public long costs_production;

		
		public long costs_updates;

		
		public int[] sellsPerWeek;

		
		public int abonnements;

		
		public int abonnementsWoche;

		
		public int bestAbonnements;

		
		public int bestChartPosition;

		
		public long exklusivKonsolenSells;

		
		public float ipPunkte;

		
		public bool pubAngebot;

		
		public int pubAngebot_Weeks;

		
		public float pubAngebot_Verhandlung;

		
		public bool pubAngebot_Retail;

		
		public bool pubAngebot_Digital;

		
		public int pubAngebot_Garantiesumme;

		
		public float pubAngebot_Gewinnbeteiligung;

		
		public bool auftragsspiel;

		
		public int auftragsspiel_gehalt;

		
		public int auftragsspiel_bonus;

		
		public int auftragsspiel_zeitInWochen;

		
		public int auftragsspiel_wochenAlsAngebot;

		
		public bool auftragsspiel_zeitAbgelaufen;

		
		public int auftragsspiel_mindestbewertung;

		
		public string ipName;

		
		public int lastChartPosition;
	}

	
	public struct s_Game : NetworkMessage
	{
		
		public int gameID;

		
		public string myName;

		
		public string ipName;

		
		public bool playerGame;

		
		public bool inDevelopment;

		
		public int developerID;

		
		public int publisherID;

		
		public int ownerID;

		
		public int engineID;

		
		public float hype;

		
		public bool isOnMarket;

		
		public bool warBeiAwards;

		
		public int weeksOnMarket;

		
		public int usk;

		
		public int freigabeBudget;

		
		public int reviewGameplay;

		
		public int reviewGrafik;

		
		public int reviewSound;

		
		public int reviewSteuerung;

		
		public int reviewTotal;

		
		public int reviewGameplayText;

		
		public int reviewGrafikText;

		
		public int reviewSoundText;

		
		public int reviewSteuerungText;

		
		public int reviewTotalText;

		
		public int date_year;

		
		public int date_month;

		
		public int date_start_year;

		
		public int date_start_month;

		
		public long sellsTotal;

		
		public long umsatzTotal;

		
		public long costs_entwicklung;

		
		public long costs_mitarbeiter;

		
		public long costs_marketing;

		
		public long costs_enginegebuehren;

		
		public long costs_server;

		
		public long costs_production;

		
		public long costs_updates;

		
		public bool typ_standard;

		
		public bool typ_nachfolger;

		
		public int originalIP;

		
		public int teile;

		
		public bool typ_contractGame;

		
		public bool typ_remaster;

		
		public bool typ_spinoff;

		
		public bool typ_addon;

		
		public bool typ_addonStandalone;

		
		public bool typ_mmoaddon;

		
		public bool typ_bundle;

		
		public bool typ_budget;

		
		public bool typ_bundleAddon;

		
		public bool typ_goty;

		
		public int originalGameID;

		
		public int portID;

		
		public int mainIP;

		
		public float ipPunkte;

		
		public bool exklusiv;

		
		public bool herstellerExklusiv;

		
		public bool retro;

		
		public bool handy;

		
		public bool arcade;

		
		public bool goty;

		
		public bool nachfolger_created;

		
		public bool remaster_created;

		
		public bool budget_created;

		
		public bool goty_created;

		
		public bool trendsetter;

		
		public bool spielbericht;

		
		public int amountUpdates;

		
		public float bonusSellsUpdates;

		
		public int amountAddons;

		
		public float bonusSellsAddons;

		
		public int amountMMOAddons;

		
		public float bonusSellsMMOAddons;

		
		public float addonQuality;

		
		public int devAktFeature;

		
		public float devPoints;

		
		public float devPointsStart;

		
		public float devPoints_Gesamt;

		
		public float devPointsStart_Gesamt;

		
		public float points_gameplay;

		
		public float points_grafik;

		
		public float points_sound;

		
		public float points_technik;

		
		public float points_bugs;

		
		public string beschreibung;

		
		public int gameTyp;

		
		public int gameSize;

		
		public int gameZielgruppe;

		
		public int maingenre;

		
		public int subgenre;

		
		public int gameMainTheme;

		
		public int gameSubTheme;

		
		public int gameLicence;

		
		public int gameCopyProtect;

		
		public int gameAntiCheat;

		
		public int gameAP_Gameplay;

		
		public int gameAP_Grafik;

		
		public int gameAP_Sound;

		
		public int gameAP_Technik;

		
		public bool[] gameLanguage;

		
		public bool[] gameGameplayFeatures;

		
		public int[] gamePlatform;

		
		public int[] gameEngineFeature;

		
		public bool[] gameplayFeatures_DevDone;

		
		public bool[] engineFeature_DevDone;

		
		public bool[] gameplayStudio;

		
		public bool[] grafikStudio;

		
		public bool[] soundStudio;

		
		public bool[] motionCaptureStudio;

		
		public int[] bundleID;

		
		public bool[] portExist;

		
		public int[] sellsPerWeek;

		
		public int[] verkaufspreis;

		
		public int releaseDate;

		
		public int abonnements;

		
		public int abonnementsWoche;

		
		public int aboPreis;

		
		public bool pubOffer;

		
		public bool pubAngebot;

		
		public int pubAngebot_Weeks;

		
		public float pubAngebot_Verhandlung;

		
		public bool pubAngebot_Retail;

		
		public bool pubAngebot_Digital;

		
		public int pubAngebot_Garantiesumme;

		
		public float pubAngebot_Gewinnbeteiligung;

		
		public bool auftragsspiel;

		
		public int auftragsspiel_gehalt;

		
		public int auftragsspiel_bonus;

		
		public int auftragsspiel_zeitInWochen;

		
		public int auftragsspiel_wochenAlsAngebot;

		
		public bool auftragsspiel_zeitAbgelaufen;

		
		public int auftragsspiel_mindestbewertung;

		
		public bool f2pConverted;
	}

	
	public struct s_Lizenz : NetworkMessage
	{
		
		public int lizenzID;

		
		public int angebot;

		
		public float quality;
	}

	
	public struct s_Trend : NetworkMessage
	{
		
		public int trendWeeks;

		
		public int trendTheme;

		
		public int trendAntiTheme;

		
		public int trendGenre;

		
		public int trendAntiGenre;

		
		public int trendNextGenre;

		
		public int trendNextAntiGenre;

		
		public int trendNextTheme;

		
		public int trendNextAntiTheme;
	}

	
	public struct s_GameSpeed : NetworkMessage
	{
		
		public int speed;
	}

	
	public struct s_Command : NetworkMessage
	{
		
		public int command;
	}

	
	public struct s_Save : NetworkMessage
	{
		
		public int saveID;
	}

	
	public struct s_Load : NetworkMessage
	{
		
		public int saveID;
	}

	
	public struct s_PlayerID : NetworkMessage
	{
		
		public int id;

		
		public string version;
	}

	
	public struct s_PlayerInfos : NetworkMessage
	{
		
		public int id;

		
		public string playerName;

		
		public bool ready;
	}

	
	public struct s_DeleteArbeitsmarkt : NetworkMessage
	{
		
		public int objectID;

		
		public bool eingestellt;
	}

	
	public struct s_CreateArbeitsmarkt : NetworkMessage
	{
		
		public int objectID;

		
		public bool male;

		
		public string myName;

		
		public int wochenAmArbeitsmarkt;

		
		public int legend;

		
		public int beruf;

		
		public float s_gamedesign;

		
		public float s_programmieren;

		
		public float s_grafik;

		
		public float s_sound;

		
		public float s_pr;

		
		public float s_gametests;

		
		public float s_technik;

		
		public float s_forschen;

		
		public bool[] perks;

		
		public int model_body;

		
		public int model_eyes;

		
		public int model_hair;

		
		public int model_beard;

		
		public int model_skinColor;

		
		public int model_hairColor;

		
		public int model_beardColor;

		
		public int model_HoseColor;

		
		public int model_ShirtColor;

		
		public int model_Add1Color;
	}
}
