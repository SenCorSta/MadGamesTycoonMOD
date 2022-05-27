using System;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_WaitForPlayers : MonoBehaviour
{
	
	private void Awake()
	{
		this.manager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
	}

	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void OnEnable()
	{
		if (this.mpCalls_.isClient)
		{
			this.mpCalls_.CLIENT_Send_Command(1);
		}
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
	}

	
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

	
	private NetworkManager manager;

	
	private mpCalls mpCalls_;

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private float sendTimer;
}
