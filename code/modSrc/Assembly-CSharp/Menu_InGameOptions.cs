using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_InGameOptions : MonoBehaviour
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
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.guiMain_.BUTTON_GameSpeed(0f);
		}
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
		switch (this.mS_.difficulty)
		{
		case 0:
		{
			Text component = this.uiObjects[0].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n",
				this.tS_.GetText(798),
				": ",
				this.tS_.GetText(802)
			});
			break;
		}
		case 1:
		{
			Text component = this.uiObjects[0].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n",
				this.tS_.GetText(798),
				": ",
				this.tS_.GetText(803)
			});
			break;
		}
		case 2:
		{
			Text component = this.uiObjects[0].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n",
				this.tS_.GetText(798),
				": ",
				this.tS_.GetText(804)
			});
			break;
		}
		case 3:
		{
			Text component = this.uiObjects[0].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n",
				this.tS_.GetText(798),
				": ",
				this.tS_.GetText(805)
			});
			break;
		}
		case 4:
		{
			Text component = this.uiObjects[0].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n",
				this.tS_.GetText(798),
				": ",
				this.tS_.GetText(1685)
			});
			break;
		}
		case 5:
		{
			Text component = this.uiObjects[0].GetComponent<Text>();
			component.text = string.Concat(new string[]
			{
				component.text,
				"\n",
				this.tS_.GetText(798),
				": ",
				this.tS_.GetText(806)
			});
			break;
		}
		}
		Text component2 = this.uiObjects[0].GetComponent<Text>();
		component2.text = component2.text + "\n" + this.tS_.GetText(800) + ": ";
		for (int i = 0; i < this.mS_.gameSpeeds.Length; i++)
		{
			if (this.mS_.gameSpeeds[i] == this.mS_.speedSetting)
			{
				switch (i)
				{
				case 0:
				{
					Text component3 = this.uiObjects[0].GetComponent<Text>();
					component3.text += this.tS_.GetText(1335);
					break;
				}
				case 1:
				{
					Text component4 = this.uiObjects[0].GetComponent<Text>();
					component4.text += this.tS_.GetText(807);
					break;
				}
				case 2:
				{
					Text component5 = this.uiObjects[0].GetComponent<Text>();
					component5.text += this.tS_.GetText(808);
					break;
				}
				case 3:
				{
					Text component6 = this.uiObjects[0].GetComponent<Text>();
					component6.text += this.tS_.GetText(809);
					break;
				}
				case 4:
				{
					Text component7 = this.uiObjects[0].GetComponent<Text>();
					component7.text += this.tS_.GetText(810);
					break;
				}
				}
			}
		}
		if (this.mS_.multiplayer)
		{
			if (this.mpCalls_.isClient)
			{
				this.uiObjects[1].GetComponent<Button>().interactable = false;
			}
			if (this.mpCalls_.isClient)
			{
				this.uiObjects[2].GetComponent<Button>().interactable = false;
				return;
			}
		}
		else
		{
			this.uiObjects[1].GetComponent<Button>().interactable = true;
			this.uiObjects[2].GetComponent<Button>().interactable = true;
		}
	}

	
	public void BUTTON_ExitToOS()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[148]);
	}

	
	public void BUTTON_ExitToMainMenu()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[154]);
	}

	
	public void BUTTON_LoadGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	
	public void BUTTON_SaveGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[156]);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[169]);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private cameraMovementScript camMove_;

	
	private mpCalls mpCalls_;
}
