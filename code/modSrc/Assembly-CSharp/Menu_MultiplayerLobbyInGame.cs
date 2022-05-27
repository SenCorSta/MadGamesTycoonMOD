using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027B RID: 635
public class Menu_MultiplayerLobbyInGame : MonoBehaviour
{
	// Token: 0x060018C5 RID: 6341 RVA: 0x00010FB1 File Offset: 0x0000F1B1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x000FDE84 File Offset: 0x000FC084
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

	// Token: 0x060018C7 RID: 6343 RVA: 0x000FDF74 File Offset: 0x000FC174
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

	// Token: 0x060018C8 RID: 6344 RVA: 0x000FDFD8 File Offset: 0x000FC1D8
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

	// Token: 0x04001C54 RID: 7252
	public GameObject[] uiObjects;

	// Token: 0x04001C55 RID: 7253
	public int[] price;

	// Token: 0x04001C56 RID: 7254
	private GameObject main_;

	// Token: 0x04001C57 RID: 7255
	private mainScript mS_;

	// Token: 0x04001C58 RID: 7256
	private textScript tS_;

	// Token: 0x04001C59 RID: 7257
	private GUI_Main guiMain_;

	// Token: 0x04001C5A RID: 7258
	private sfxScript sfx_;

	// Token: 0x04001C5B RID: 7259
	private mpCalls mpCalls_;

	// Token: 0x04001C5C RID: 7260
	private NetworkManager manager;
}
