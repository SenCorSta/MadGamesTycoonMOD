using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Firmenname : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	
	private void OnEnable()
	{
		this.Init();
		this.cmS_.disableMovement = true;
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<InputField>().text = this.mS_.companyName;
		this.SetLogo(this.mS_.logo);
	}

	
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(814), false);
			return;
		}
		this.mS_.logo = this.logo;
		this.mS_.companyName = this.uiObjects[0].GetComponent<InputField>().text;
		this.guiMain_.SetMainGuiData();
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				player_mp player_mp = this.mS_.mpCalls_.FindPlayer(this.mS_.mpCalls_.myID);
				if (player_mp != null)
				{
					player_mp.companyName = this.mS_.companyName;
					player_mp.companyLogo = this.mS_.logo;
				}
				this.mS_.mpCalls_.SERVER_Send_PlayerInfos();
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_PlayerInfos();
			}
		}
		this.BUTTON_Abbrechen();
	}

	
	public void SetLogo(int i)
	{
		this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript cmS_;

	
	private int logo = -1;
}
