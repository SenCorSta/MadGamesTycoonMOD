using System;
using Mirror;
using Steamworks;
using UnityEngine;


public class SteamLobby : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
		this.lobbyCreated = Callback<LobbyCreated_t>.Create(new Callback<LobbyCreated_t>.DispatchDelegate(this.OnLobbyCreated));
		this.gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnGameLobbyJoinRequested));
		this.lobbyEntered = Callback<LobbyEnter_t>.Create(new Callback<LobbyEnter_t>.DispatchDelegate(this.OnLobbyEntered));
	}

	
	private void FindScripts()
	{
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.Menu_Multiplayer_Main)
		{
			this.Menu_Multiplayer_Main = this.guiMain_.uiObjects[201];
		}
		this.networkManager = base.GetComponent<NetworkManager>();
	}

	
	public void HostLobby()
	{
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, this.networkManager.maxConnections);
	}

	
	public void LockLobby(bool b)
	{
		SteamMatchmaking.SetLobbyJoinable(new CSteamID(this.lobbyID), b);
	}

	
	public void LeaveLobby()
	{
		SteamMatchmaking.LeaveLobby(new CSteamID(this.lobbyID));
	}

	
	private void OnLobbyCreated(LobbyCreated_t callback)
	{
		if (callback.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		this.FindScripts();
		this.Menu_Multiplayer_Main.GetComponent<mpMain>().StartHost();
		SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAdress", SteamUser.GetSteamID().ToString());
		this.lobbyID = callback.m_ulSteamIDLobby;
	}

	
	private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
	{
		SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
	}

	
	private void OnLobbyEntered(LobbyEnter_t callback)
	{
		if (NetworkServer.active)
		{
			return;
		}
		this.FindScripts();
		this.mS_.multiplayer = true;
		this.mS_.mpCalls_.myID = -1;
		this.RemoveContentFromClient();
		this.mS_.LoadContent_MultiplayerClient();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[201]);
		string lobbyData = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAdress");
		this.Menu_Multiplayer_Main.GetComponent<mpMain>().StartClient_Steam();
		this.networkManager.networkAddress = lobbyData;
		this.networkManager.StartClient();
	}

	
	private void RemoveContentFromClient()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				UnityEngine.Object.Destroy(array[i]);
			}
		}
	}

	
	[SerializeField]
	private GameObject Menu_Multiplayer_Main;

	
	protected Callback<LobbyCreated_t> lobbyCreated;

	
	protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

	
	protected Callback<LobbyEnter_t> lobbyEntered;

	
	private const string HostAdressKey = "HostAdress";

	
	private NetworkManager networkManager;

	
	private ulong lobbyID;

	
	public mainScript mS_;

	
	public GUI_Main guiMain_;

	
	public GameObject main_;
}
