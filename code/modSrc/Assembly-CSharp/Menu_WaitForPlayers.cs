using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200027D RID: 637
public class Menu_WaitForPlayers : MonoBehaviour
{
	// Token: 0x060018D1 RID: 6353 RVA: 0x00010FF8 File Offset: 0x0000F1F8
	private void Awake()
	{
		this.manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x00011024 File Offset: 0x0000F224
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x0001102C File Offset: 0x0000F22C
	private void OnEnable()
	{
		if (this.mpCalls_.isClient)
		{
			this.mpCalls_.CLIENT_Send_Command(1);
		}
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x000FE238 File Offset: 0x000FC438
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
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x000FE2E4 File Offset: 0x000FC4E4
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		if (!this.mpCalls_.isServer)
		{
			this.sendTimer += Time.deltaTime;
			if ((double)this.sendTimer >= 1.0)
			{
				this.sendTimer = 0f;
				this.mpCalls_.CLIENT_Send_Command(1);
			}
			return;
		}
		if (!this.mpCalls_.GetAllPlayersReady())
		{
			return;
		}
		this.mpCalls_.SERVER_Send_Command(2);
		base.gameObject.SetActive(false);
		Debug.Log("WaitForPlayers() CLOSE");
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x000FE378 File Offset: 0x000FC578
	public void BUTTON_Close()
	{
		this.FindScripts();
		this.sfx_.PlaySound(3, true);
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].SetActive(true);
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().StopNetwork();
			this.guiMain_.uiObjects[201].SetActive(false);
		}
		this.guiMain_.RemoveVectrocity();
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x04001C66 RID: 7270
	private NetworkManager manager;

	// Token: 0x04001C67 RID: 7271
	private mpCalls mpCalls_;

	// Token: 0x04001C68 RID: 7272
	public GameObject[] uiObjects;

	// Token: 0x04001C69 RID: 7273
	private GameObject main_;

	// Token: 0x04001C6A RID: 7274
	private mainScript mS_;

	// Token: 0x04001C6B RID: 7275
	private textScript tS_;

	// Token: 0x04001C6C RID: 7276
	private GUI_Main guiMain_;

	// Token: 0x04001C6D RID: 7277
	private sfxScript sfx_;

	// Token: 0x04001C6E RID: 7278
	private float sendTimer;
}
