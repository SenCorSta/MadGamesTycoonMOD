using System;
using Mirror;
using Steamworks;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class SteamLobby : MonoBehaviour
{
	// Token: 0x06001AA0 RID: 6816 RVA: 0x00111E48 File Offset: 0x00110048
	private void Start()
	{
		this.FindScripts();
		this.lobbyCreated = Callback<LobbyCreated_t>.Create(new Callback<LobbyCreated_t>.DispatchDelegate(this.OnLobbyCreated));
		this.gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnGameLobbyJoinRequested));
		this.lobbyEntered = Callback<LobbyEnter_t>.Create(new Callback<LobbyEnter_t>.DispatchDelegate(this.OnLobbyEntered));
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x00111EA0 File Offset: 0x001100A0
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

	// Token: 0x06001AA2 RID: 6818 RVA: 0x00011E2E File Offset: 0x0001002E
	public void HostLobby()
	{
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, this.networkManager.maxConnections);
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x00011E42 File Offset: 0x00010042
	public void LockLobby(bool b)
	{
		SteamMatchmaking.SetLobbyJoinable(new CSteamID(this.lobbyID), b);
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x00011E56 File Offset: 0x00010056
	public void LeaveLobby()
	{
		SteamMatchmaking.LeaveLobby(new CSteamID(this.lobbyID));
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x00111F3C File Offset: 0x0011013C
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

	// Token: 0x06001AA6 RID: 6822 RVA: 0x00011E68 File Offset: 0x00010068
	private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
	{
		SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x00111FA0 File Offset: 0x001101A0
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

	// Token: 0x06001AA8 RID: 6824 RVA: 0x00112044 File Offset: 0x00110244
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

	// Token: 0x040021F3 RID: 8691
	[SerializeField]
	private GameObject Menu_Multiplayer_Main;

	// Token: 0x040021F4 RID: 8692
	protected Callback<LobbyCreated_t> lobbyCreated;

	// Token: 0x040021F5 RID: 8693
	protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

	// Token: 0x040021F6 RID: 8694
	protected Callback<LobbyEnter_t> lobbyEntered;

	// Token: 0x040021F7 RID: 8695
	private const string HostAdressKey = "HostAdress";

	// Token: 0x040021F8 RID: 8696
	private NetworkManager networkManager;

	// Token: 0x040021F9 RID: 8697
	private ulong lobbyID;

	// Token: 0x040021FA RID: 8698
	public mainScript mS_;

	// Token: 0x040021FB RID: 8699
	public GUI_Main guiMain_;

	// Token: 0x040021FC RID: 8700
	public GameObject main_;
}
