using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Start : MonoBehaviour
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
		if (!this.main_)
		{
			return;
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
		if (!this.camMove_)
		{
			this.camMove_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	
	private void OnDisable()
	{
		this.camMove_.disableMovement = false;
		this.guiMain_.CloseMenu();
		this.guiMain_.ShowInGameUI(true);
	}

	
	public void Init()
	{
		this.camMove_.disableMovement = true;
		this.guiMain_.OpenMenu(false);
		this.guiMain_.ShowInGameUI(false);
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.buildVersion;
		if (PlayerPrefs.HasKey("ResumeGame"))
		{
			this.uiObjects[1].GetComponent<Button>().interactable = true;
			return;
		}
		this.uiObjects[1].GetComponent<Button>().interactable = false;
	}

	
	private void Update()
	{
	}

	
	public void BUTTON_NewGame()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		this.mS_.LoadContent();
		this.mS_.myID = 100000;
		this.mS_.myPubS_ = null;
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[159]);
	}

	
	public void BUTTON_Fortsetzen()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		int @int = PlayerPrefs.GetInt("ResumeGame");
		this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().BUTTON_LoadGame(@int);
	}

	
	public void BUTTON_CloseGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[148]);
	}

	
	public void BUTTON_Credits()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[149]);
	}

	
	public void BUTTON_LoadGame()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[169]);
	}

	
	public void BUTTON_Multiplayer()
	{
		Debug.Log("1. BUTTON_Multiplayer()");
		this.mS_.multiplayer = true;
		this.mS_.myID = -1;
		this.mS_.myPubS_ = null;
		this.mS_.LoadContent();
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[201]);
		this.guiMain_.uiObjects[201].GetComponent<mpMain>().BUTTON_StartHost();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript camMove_;
}
