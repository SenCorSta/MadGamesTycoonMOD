using System;
using System.Collections.Generic;
using System.Net;
using Mirror;
using UnityEngine;
using UnityEngine.UI;


public class mpMain : MonoBehaviour
{
	
	private void Awake()
	{
		this.manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
	}

	
	private void Start()
	{
		this.FindScripts();
		if (PlayerPrefs.HasKey("MP_IP"))
		{
			this.uiObjects[0].GetComponent<InputField>().text = PlayerPrefs.GetString("MP_IP");
			return;
		}
		this.uiObjects[0].GetComponent<InputField>().text = "127.0.0.1";
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
		if (!this.manager)
		{
			this.manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetLogo(0);
		this.uiObjects[12].GetComponent<InputField>().text = "";
		this.INPUT_PlayerName();
		this.INPUT_CompanyName();
		this.mpCalls_.isServer = false;
		this.mpCalls_.isClient = false;
		this.mpCalls_.playersMP.Clear();
		this.uiObjects[1].SetActive(false);
		this.uiObjects[2].SetActive(false);
		this.uiObjects[3].SetActive(true);
		this.uiObjects[4].SetActive(false);
		this.uiObjects[5].SetActive(false);
		this.uiObjects[37].GetComponent<InputField>().readOnly = false;
		this.uiObjects[37].GetComponent<InputField>().text = new WebClient().DownloadString("https://ipv4.icanhazip.com/");
		this.uiObjects[37].GetComponent<InputField>().readOnly = true;
		this.uiObjects[35].GetComponent<Text>().text = this.tS_.GetText(1228);
	}

	
	public void InitDropdowns()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.tS_.country_GE.Length; i++)
		{
			list.Add(this.tS_.GetCountry(i));
		}
		this.uiObjects[28].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[28].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[28].GetComponent<Dropdown>().value = 0;
		list = new List<string>();
		list.Add(this.tS_.GetText(802));
		list.Add(this.tS_.GetText(803));
		list.Add(this.tS_.GetText(804));
		list.Add(this.tS_.GetText(805));
		list.Add(this.tS_.GetText(1685));
		list.Add(this.tS_.GetText(806));
		this.uiObjects[31].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[31].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[31].GetComponent<Dropdown>().value = 2;
		this.mS_.difficulty = 2;
		list = new List<string>();
		list.Add("1976");
		list.Add("1985");
		list.Add("1995");
		list.Add("2005");
		list.Add("2015");
		this.uiObjects[32].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[32].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[32].GetComponent<Dropdown>().value = 0;
		list = new List<string>();
		list.Add(this.tS_.GetText(1770));
		list.Add(this.tS_.GetText(1771));
		list.Add(this.tS_.GetText(2012));
		list.Add(this.tS_.GetText(1773));
		this.uiObjects[33].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[33].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[33].GetComponent<Dropdown>().value = 0;
		this.mS_.office = 3;
		list = new List<string>();
		list.Add(this.tS_.GetText(1335));
		list.Add(this.tS_.GetText(807));
		list.Add(this.tS_.GetText(808));
		list.Add(this.tS_.GetText(809));
		list.Add(this.tS_.GetText(810));
		this.uiObjects[44].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[44].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[44].GetComponent<Dropdown>().value = 2;
		list = new List<string>();
		for (int j = 0; j < this.genres_.genres_UNLOCK.Length; j++)
		{
			list.Add(this.genres_.GetName(j));
		}
		this.uiObjects[46].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[46].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[46].GetComponent<Dropdown>().value = 0;
	}

	
	private void UpdatePlayerInfos()
	{
		this.timerPlayerInfos += Time.deltaTime;
		if (this.timerPlayerInfos > 1f)
		{
			this.timerPlayerInfos = 0f;
			if (this.mpCalls_.isClient && NetworkClient.isConnected)
			{
				this.mpCalls_.CLIENT_Send_PlayerInfos();
			}
			if (this.mpCalls_.isServer)
			{
				player_mp player_mp = this.mS_.mpCalls_.FindPlayer(this.mS_.myID);
				if (player_mp != null)
				{
					player_mp.playerName = this.mS_.playerName;
					player_mp.ready = true;
				}
				if (this.manager.numPlayers > 1)
				{
					this.mpCalls_.SERVER_Send_Difficulty();
					this.mpCalls_.SERVER_Send_Startjahr();
					this.mpCalls_.SERVER_Send_Office();
					this.mpCalls_.SERVER_Send_Spielgeschwindigkeit();
				}
			}
			if (this.mS_.myPubS_)
			{
				if (this.mpCalls_.isClient)
				{
					this.mpCalls_.CLIENT_Send_Publisher(this.mS_.myPubS_);
				}
				if (this.mpCalls_.isServer)
				{
					this.mpCalls_.SERVER_Send_Publisher(this.mS_.myPubS_);
				}
			}
		}
	}

	
	private void Update()
	{
		if (NetworkServer.active)
		{
			this.uiObjects[1].SetActive(true);
			this.uiObjects[1].GetComponent<Text>().text = string.Concat(new object[]
			{
				"Server: active. Transport: ",
				Transport.activeTransport,
				", Players: ",
				this.manager.numPlayers.ToString()
			});
		}
		if (NetworkClient.isConnected)
		{
			this.uiObjects[2].SetActive(true);
			this.uiObjects[2].GetComponent<Text>().text = "Client: address=" + this.manager.networkAddress;
			if (!this.uiObjects[4].activeSelf)
			{
				this.uiObjects[5].SetActive(false);
				this.uiObjects[4].SetActive(true);
			}
		}
		if (this.mpCalls_.isClient && this.uiObjects[4].activeSelf && !NetworkClient.isConnected)
		{
			this.BUTTON_Close();
			this.guiMain_.uiObjects[162].SetActive(false);
			this.guiMain_.MessageBox(this.tS_.GetText(1039), false);
		}
		for (int i = 0; i < 4; i++)
		{
			this.uiObjects[7 + i].GetComponent<Text>().text = "";
			this.uiObjects[13 + i].GetComponent<Text>().text = "";
			this.uiObjects[18 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			this.uiObjects[22 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			this.uiObjects[47 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
		}
		if (this.mpCalls_.isClient)
		{
			if (this.mS_.GetCompanyName().Length <= 0 || this.mS_.playerName.Length <= 0)
			{
				this.uiObjects[51].GetComponent<Toggle>().interactable = false;
				this.uiObjects[51].GetComponent<Toggle>().isOn = false;
			}
			else
			{
				this.uiObjects[51].GetComponent<Toggle>().interactable = true;
			}
		}
		this.UpdatePlayerInfos();
		bool flag = false;
		for (int j = 0; j < this.mpCalls_.playersMP.Count; j++)
		{
			int playerID = this.mpCalls_.playersMP[j].playerID;
			this.uiObjects[7 + j].GetComponent<Text>().text = this.mpCalls_.GetPlayerName(playerID);
			this.uiObjects[13 + j].GetComponent<Text>().text = this.mpCalls_.GetCompanyName(playerID);
			if (this.mpCalls_.GetLogo(playerID) != -1)
			{
				this.uiObjects[18 + j].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.mpCalls_.GetLogo(playerID));
			}
			else
			{
				this.uiObjects[18 + j].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
			if (this.mpCalls_.GetCountry(playerID) != -1)
			{
				this.uiObjects[22 + j].GetComponent<Image>().sprite = this.guiMain_.flagSprites[this.mpCalls_.GetCountry(playerID)];
			}
			else
			{
				this.uiObjects[22 + j].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
			if (this.mpCalls_.GetReady(playerID))
			{
				this.uiObjects[47 + j].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
			}
			else
			{
				flag = true;
				this.uiObjects[47 + j].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
		}
		if (this.mpCalls_.isClient)
		{
			this.uiObjects[31].GetComponent<Dropdown>().interactable = false;
			this.uiObjects[32].GetComponent<Dropdown>().interactable = false;
			this.uiObjects[33].GetComponent<Dropdown>().interactable = false;
			this.uiObjects[36].GetComponent<Toggle>().interactable = false;
			this.uiObjects[40].GetComponent<Toggle>().interactable = false;
			this.uiObjects[41].GetComponent<Toggle>().interactable = false;
			this.uiObjects[42].GetComponent<Toggle>().interactable = false;
			this.uiObjects[43].GetComponent<Toggle>().interactable = false;
			this.uiObjects[44].GetComponent<Dropdown>().interactable = false;
			this.uiObjects[45].GetComponent<Toggle>().interactable = false;
			this.uiObjects[52].GetComponent<Toggle>().interactable = false;
			this.uiObjects[53].GetComponent<Toggle>().interactable = false;
		}
		else
		{
			this.uiObjects[31].GetComponent<Dropdown>().interactable = true;
			this.uiObjects[32].GetComponent<Dropdown>().interactable = true;
			this.uiObjects[33].GetComponent<Dropdown>().interactable = true;
			this.uiObjects[36].GetComponent<Toggle>().interactable = true;
			this.uiObjects[40].GetComponent<Toggle>().interactable = true;
			this.uiObjects[41].GetComponent<Toggle>().interactable = true;
			this.uiObjects[42].GetComponent<Toggle>().interactable = true;
			this.uiObjects[43].GetComponent<Toggle>().interactable = true;
			this.uiObjects[44].GetComponent<Dropdown>().interactable = true;
			this.uiObjects[45].GetComponent<Toggle>().interactable = true;
			this.uiObjects[52].GetComponent<Toggle>().interactable = true;
			if (!this.uiObjects[40].GetComponent<Toggle>().isOn)
			{
				this.uiObjects[53].GetComponent<Toggle>().interactable = true;
			}
		}
		if (this.mpCalls_.isServer)
		{
			if (this.manager.numPlayers <= 1 || flag)
			{
				this.uiObjects[17].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[17].GetComponent<Button>().interactable = true;
			}
			if (this.manager.numPlayers <= 1)
			{
				this.uiObjects[34].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[34].GetComponent<Button>().interactable = true;
			}
			if (this.manager.numPlayers < 4)
			{
				this.uiObjects[27].GetComponent<Text>().text = this.tS_.GetText(1026);
			}
			else
			{
				this.uiObjects[27].GetComponent<Text>().text = this.tS_.GetText(1030);
			}
		}
		else
		{
			this.uiObjects[27].GetComponent<Text>().text = this.tS_.GetText(1031);
		}
		if (this.mpCalls_.isServer && (this.mS_.GetCompanyName().Length <= 0 || this.mS_.playerName.Length <= 0))
		{
			this.uiObjects[17].GetComponent<Button>().interactable = false;
		}
		if (this.mS_.achScript_)
		{
			if (this.mpCalls_.playersMP.Count >= 2)
			{
				this.mS_.achScript_.SetAchivement(55);
			}
			if (this.mpCalls_.playersMP.Count >= 4)
			{
				this.mS_.achScript_.SetAchivement(56);
			}
		}
	}

	
	public void StartHost()
	{
		Debug.Log("5. StartHost()");
		this.FindScripts();
		this.mS_.mpLobbyOpen = true;
		this.mpCalls_.SetupServer();
		this.mpCalls_.isServer = true;
		this.manager.StartHost();
		this.uiObjects[3].SetActive(false);
		this.uiObjects[17].SetActive(true);
		this.uiObjects[26].SetActive(false);
		this.uiObjects[34].SetActive(true);
		this.uiObjects[51].SetActive(false);
		this.uiObjects[51].GetComponent<Toggle>().isOn = true;
	}

	
	public void BUTTON_StartHost()
	{
		Debug.Log("2. BUTTON_StartHost()");
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		this.manager.GetComponent<SteamLobby>().HostLobby();
	}

	
	public void BUTTON_LoadMultiplayerSavegame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	
	public void BUTTON_StartClient()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mS_.playerName.Length <= 0 || this.mS_.GetCompanyName().Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1033), false);
			return;
		}
		if (this.mS_.playerName.Length <= 0)
		{
			this.mS_.playerName = "<Missing Player Name>";
		}
		if (this.mS_.GetCompanyName().Length <= 0)
		{
			this.mS_.SetCompanyName("<Missing Company Name>");
		}
		PlayerPrefs.SetString("PlayerName", this.uiObjects[11].GetComponent<InputField>().text);
		PlayerPrefs.SetString("CompanyName", this.uiObjects[12].GetComponent<InputField>().text);
		PlayerPrefs.SetString("MP_IP", this.uiObjects[0].GetComponent<InputField>().text);
		this.mpCalls_.SetupClient();
		this.mS_.myID = -1;
		this.mpCalls_.isClient = true;
		this.manager.networkAddress = this.uiObjects[0].GetComponent<InputField>().text;
		this.manager.StartClient();
		this.uiObjects[3].SetActive(false);
		this.uiObjects[5].SetActive(true);
		this.uiObjects[17].SetActive(false);
		this.uiObjects[26].SetActive(true);
		this.uiObjects[34].SetActive(false);
		this.uiObjects[51].SetActive(true);
		this.uiObjects[6].GetComponent<Text>().text = "Connecting:\n" + this.manager.networkAddress;
	}

	
	public void StartClient_Steam()
	{
		PlayerPrefs.SetString("PlayerName", this.uiObjects[11].GetComponent<InputField>().text);
		PlayerPrefs.SetString("CompanyName", this.uiObjects[12].GetComponent<InputField>().text);
		PlayerPrefs.SetString("MP_IP", this.uiObjects[0].GetComponent<InputField>().text);
		this.mpCalls_.SetupClient();
		this.mS_.myID = -1;
		this.mpCalls_.isClient = true;
		this.uiObjects[3].SetActive(false);
		this.uiObjects[5].SetActive(true);
		this.uiObjects[17].SetActive(false);
		this.uiObjects[26].SetActive(true);
		this.uiObjects[34].SetActive(false);
		this.uiObjects[51].SetActive(true);
		this.uiObjects[6].GetComponent<Text>().text = "Connecting...";
	}

	
	public void StopNetwork()
	{
		this.FindScripts();
		this.manager.GetComponent<SteamLobby>().LeaveLobby();
		this.mS_.myID = -1;
		this.mS_.myPubS_ = null;
		this.mpCalls_.isServer = false;
		this.mpCalls_.isClient = false;
		for (int i = 0; i < 8; i++)
		{
			GameObject gameObject = GameObject.Find("PUB_" + (100000 + i).ToString());
			if (gameObject)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		if (NetworkServer.active && NetworkClient.isConnected)
		{
			this.manager.StopHost();
			Debug.Log("StopHost()");
		}
		else if (NetworkClient.isConnected)
		{
			this.manager.StopClient();
			Debug.Log("StopClient()");
		}
		else if (NetworkServer.active)
		{
			this.manager.StopServer();
			Debug.Log("StopServer()");
		}
		if (!NetworkClient.active)
		{
			this.manager.StopClient();
			Debug.Log("StopClient()");
		}
	}

	
	public void BUTTON_Close()
	{
		this.StopNetwork();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void INPUT_PlayerName()
	{
		this.mS_.playerName = this.uiObjects[11].GetComponent<InputField>().text;
	}

	
	public void INPUT_CompanyName()
	{
		this.mS_.SetCompanyName(this.uiObjects[12].GetComponent<InputField>().text);
	}

	
	public void DROPDOWN_Country()
	{
		this.mS_.SetCountryID(this.uiObjects[28].GetComponent<Dropdown>().value);
	}

	
	public void DROPDOWN_Genre()
	{
		this.mS_.SetFanGenreID(this.uiObjects[46].GetComponent<Dropdown>().value);
	}

	
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	
	public void TOGGLE_AutoPause()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		this.mS_.settings_autoPauseForMultiplayer = this.uiObjects[36].GetComponent<Toggle>().isOn;
		if (this.uiObjects[36].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(7);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(6);
	}

	
	public void TOGGLE_RandomEventsOff()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		this.mS_.settings_RandomEventsOff = this.uiObjects[40].GetComponent<Toggle>().isOn;
		if (this.uiObjects[40].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(9);
			this.uiObjects[53].GetComponent<Toggle>().isOn = false;
			this.uiObjects[53].GetComponent<Toggle>().interactable = false;
			Debug.Log("KKK" + UnityEngine.Random.Range(0, 10000));
			return;
		}
		this.mpCalls_.SERVER_Send_Command(8);
		this.uiObjects[53].GetComponent<Toggle>().interactable = true;
	}

	
	public void TOGGLE_RandomReviews()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		this.mS_.settings_RandomReviews = this.uiObjects[41].GetComponent<Toggle>().isOn;
		if (this.uiObjects[41].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(11);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(10);
	}

	
	public void TOGGLE_History()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		this.mS_.settings_history = this.uiObjects[53].GetComponent<Toggle>().isOn;
		if (this.uiObjects[53].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(21);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(20);
	}

	
	public void TOGGLE_RandomPlatformPop()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		if (this.uiObjects[42].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(13);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(12);
	}

	
	public void TOGGLE_RandomGenreCombination()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		if (this.uiObjects[52].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(19);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(18);
	}

	
	public void TOGGLE_RandomGameConcept()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		if (this.uiObjects[43].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(15);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(14);
	}

	
	public void TOGGLE_SpeedAnpassen()
	{
		if (!this.mpCalls_.isServer)
		{
			return;
		}
		if (this.uiObjects[45].GetComponent<Toggle>().isOn)
		{
			this.mpCalls_.SERVER_Send_Command(17);
			return;
		}
		this.mpCalls_.SERVER_Send_Command(16);
	}

	
	public void SetLogo(int i)
	{
		this.uiObjects[29].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.mS_.SetCompanyLogoID(i);
	}

	
	public void BUTTON_StartGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[162].SetActive(true);
		this.mS_.mpLobbyOpen = false;
		this.mpCalls_.SERVER_Send_Command(1);
		this.manager.GetComponent<SteamLobby>().LockLobby(false);
	}

	
	public void DROPDOWN_Difficulty()
	{
		this.mS_.difficulty = this.uiObjects[31].GetComponent<Dropdown>().value;
		if (this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Difficulty();
		}
	}

	
	public void DROPDOWN_Office()
	{
		this.mS_.office = this.mS_.GetMapIDfromDropdown(this.uiObjects[33]);
		if (this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Office();
		}
	}

	
	public void DROPDOWN_Startjahr()
	{
		if (this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Startjahr();
		}
	}

	
	public void DROPDOWN_Speed()
	{
		if (this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Spielgeschwindigkeit();
		}
	}

	
	private NetworkManager manager;

	
	private mpCalls mpCalls_;

	
	public GameObject[] uiObjects;

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	private float timerPlayerInfos;
}
