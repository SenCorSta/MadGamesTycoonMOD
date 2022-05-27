using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000281 RID: 641
public class Menu_MultiplayerLobbyInGame : MonoBehaviour
{
	// Token: 0x06001914 RID: 6420 RVA: 0x000F919B File Offset: 0x000F739B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x000F91A4 File Offset: 0x000F73A4
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

	// Token: 0x06001916 RID: 6422 RVA: 0x000F9294 File Offset: 0x000F7494
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

	// Token: 0x06001917 RID: 6423 RVA: 0x000F92F8 File Offset: 0x000F74F8
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

	// Token: 0x04001C72 RID: 7282
	public GameObject[] uiObjects;

	// Token: 0x04001C73 RID: 7283
	public int[] price;

	// Token: 0x04001C74 RID: 7284
	private GameObject main_;

	// Token: 0x04001C75 RID: 7285
	private mainScript mS_;

	// Token: 0x04001C76 RID: 7286
	private textScript tS_;

	// Token: 0x04001C77 RID: 7287
	private GUI_Main guiMain_;

	// Token: 0x04001C78 RID: 7288
	private sfxScript sfx_;

	// Token: 0x04001C79 RID: 7289
	private mpCalls mpCalls_;

	// Token: 0x04001C7A RID: 7290
	private NetworkManager manager;
}
