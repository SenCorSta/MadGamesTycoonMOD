using System;
using Mirror;
using Steamworks;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class SteamLobby : MonoBehaviour
{
	// Token: 0x06001AEA RID: 6890 RVA: 0x0010E2A4 File Offset: 0x0010C4A4
	private void Start()
	{
		this.FindScripts();
		this.lobbyCreated = Callback<LobbyCreated_t>.Create(new Callback<LobbyCreated_t>.DispatchDelegate(this.OnLobbyCreated));
		this.gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnGameLobbyJoinRequested));
		this.lobbyEntered = Callback<LobbyEnter_t>.Create(new Callback<LobbyEnter_t>.DispatchDelegate(this.OnLobbyEntered));
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0010E2FC File Offset: 0x0010C4FC
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

	// Token: 0x06001AEC RID: 6892 RVA: 0x0010E396 File Offset: 0x0010C596
	public void HostLobby()
	{
		Debug.Log("3. HostLobby()");
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, this.networkManager.maxConnections);
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x0010E3B4 File Offset: 0x0010C5B4
	public void LockLobby(bool b)
	{
		SteamMatchmaking.SetLobbyJoinable(new CSteamID(this.lobbyID), b);
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x0010E3C8 File Offset: 0x0010C5C8
	public void LeaveLobby()
	{
		SteamMatchmaking.LeaveLobby(new CSteamID(this.lobbyID));
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0010E3DC File Offset: 0x0010C5DC
	private void OnLobbyCreated(LobbyCreated_t callback)
	{
		Debug.Log("4. OnLobbyCreated()");
		if (callback.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		this.FindScripts();
		this.Menu_Multiplayer_Main.GetComponent<mpMain>().StartHost();
		SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAdress", SteamUser.GetSteamID().ToString());
		this.lobbyID = callback.m_ulSteamIDLobby;
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x0010E448 File Offset: 0x0010C648
	private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
	{
		SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x0010E458 File Offset: 0x0010C658
	private void OnLobbyEntered(LobbyEnter_t callback)
	{
		if (NetworkServer.active)
		{
			return;
		}
		this.FindScripts();
		this.mS_.multiplayer = true;
		this.mS_.myID = -1;
		this.RemoveContentFromClient();
		this.mS_.LoadContent_MultiplayerClient();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[201]);
		string lobbyData = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAdress");
		this.Menu_Multiplayer_Main.GetComponent<mpMain>().StartClient_Steam();
		this.networkManager.networkAddress = lobbyData;
		this.networkManager.StartClient();
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0010E4F8 File Offset: 0x0010C6F8
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

	// Token: 0x0400220D RID: 8717
	[SerializeField]
	private GameObject Menu_Multiplayer_Main;

	// Token: 0x0400220E RID: 8718
	protected Callback<LobbyCreated_t> lobbyCreated;

	// Token: 0x0400220F RID: 8719
	protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;

	// Token: 0x04002210 RID: 8720
	protected Callback<LobbyEnter_t> lobbyEntered;

	// Token: 0x04002211 RID: 8721
	private const string HostAdressKey = "HostAdress";

	// Token: 0x04002212 RID: 8722
	private NetworkManager networkManager;

	// Token: 0x04002213 RID: 8723
	private ulong lobbyID;

	// Token: 0x04002214 RID: 8724
	public mainScript mS_;

	// Token: 0x04002215 RID: 8725
	public GUI_Main guiMain_;

	// Token: 0x04002216 RID: 8726
	public GameObject main_;
}
