using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;


public class Menu_MultiplayerLobbyInGame : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
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

	
	public void OnEnable()
	{
		this.FindScripts();
		if (this.mS_.multiplayer)
		{
			if (this.mpCalls_.isServer)
			{
				this.mS_.SetGameSpeed(0f);
				this.mpCalls_.SetPlayersUnready();
			}
			if (this.mpCalls_.isClient)
			{
				this.mpCalls_.CLIENT_Send_Command(1);
			}
		}
	}

	
	private void Update()
	{
		for (int i = 0; i < this.mpCalls_.playersMP.Count; i++)
		{
			int playerID = this.mpCalls_.playersMP[i].playerID;
			this.uiObjects[i].GetComponent<Text>().text = this.mpCalls_.GetPlayerName(playerID);
			this.uiObjects[4 + i].GetComponent<Text>().text = this.mpCalls_.GetCompanyName(playerID);
			if (this.mpCalls_.GetLogo(playerID) != -1)
			{
				this.uiObjects[8 + i].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.mpCalls_.GetLogo(playerID));
			}
			else
			{
				this.uiObjects[8 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
			if (this.mpCalls_.GetCountry(playerID) != -1)
			{
				this.uiObjects[12 + i].GetComponent<Image>().sprite = this.guiMain_.flagSprites[this.mpCalls_.GetCountry(playerID)];
			}
			else
			{
				this.uiObjects[12 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			}
		}
	}

	
	public GameObject[] uiObjects;

	
	public int[] price;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private mpCalls mpCalls_;

	
	private NetworkManager manager;
}
