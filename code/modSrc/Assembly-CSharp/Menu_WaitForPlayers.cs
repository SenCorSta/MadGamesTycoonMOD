using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000283 RID: 643
public class Menu_WaitForPlayers : MonoBehaviour
{
	// Token: 0x06001920 RID: 6432 RVA: 0x000F95C4 File Offset: 0x000F77C4
	private void Awake()
	{
		this.manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x000F95F0 File Offset: 0x000F77F0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x000F95F8 File Offset: 0x000F77F8
	private void OnEnable()
	{
		if (this.mpCalls_.isClient)
		{
			this.mpCalls_.CLIENT_Send_Command(1);
		}
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x000F9614 File Offset: 0x000F7814
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

	// Token: 0x06001924 RID: 6436 RVA: 0x000F96C0 File Offset: 0x000F78C0
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

	// Token: 0x06001925 RID: 6437 RVA: 0x000F9754 File Offset: 0x000F7954
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

	// Token: 0x04001C84 RID: 7300
	private NetworkManager manager;

	// Token: 0x04001C85 RID: 7301
	private mpCalls mpCalls_;

	// Token: 0x04001C86 RID: 7302
	public GameObject[] uiObjects;

	// Token: 0x04001C87 RID: 7303
	private GameObject main_;

	// Token: 0x04001C88 RID: 7304
	private mainScript mS_;

	// Token: 0x04001C89 RID: 7305
	private textScript tS_;

	// Token: 0x04001C8A RID: 7306
	private GUI_Main guiMain_;

	// Token: 0x04001C8B RID: 7307
	private sfxScript sfx_;

	// Token: 0x04001C8C RID: 7308
	private float sendTimer;
}
