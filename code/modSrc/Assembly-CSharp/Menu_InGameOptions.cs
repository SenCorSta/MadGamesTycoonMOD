using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000173 RID: 371
public class Menu_InGameOptions : MonoBehaviour
{
	// Token: 0x06000DB5 RID: 3509 RVA: 0x00009879 File Offset: 0x00007A79
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x000A3500 File Offset: 0x000A1700
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

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00009881 File Offset: 0x00007A81
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.guiMain_.BUTTON_GameSpeed(0f);
		}
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x000098BE File Offset: 0x00007ABE
	private void OnDisable()
	{
		this.camMove_.disableMovement = false;
		this.guiMain_.CloseMenu();
		this.guiMain_.ShowInGameUI(true);
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x000A35FC File Offset: 0x000A17FC
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

	// Token: 0x06000DBA RID: 3514 RVA: 0x000098E3 File Offset: 0x00007AE3
	public void BUTTON_ExitToOS()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[148]);
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x0000990E File Offset: 0x00007B0E
	public void BUTTON_ExitToMainMenu()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[154]);
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00009939 File Offset: 0x00007B39
	public void BUTTON_LoadGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x00009964 File Offset: 0x00007B64
	public void BUTTON_SaveGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[156]);
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0000998F File Offset: 0x00007B8F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x000099AA File Offset: 0x00007BAA
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[169]);
	}

	// Token: 0x04001264 RID: 4708
	public GameObject[] uiObjects;

	// Token: 0x04001265 RID: 4709
	private GameObject main_;

	// Token: 0x04001266 RID: 4710
	private mainScript mS_;

	// Token: 0x04001267 RID: 4711
	private textScript tS_;

	// Token: 0x04001268 RID: 4712
	private GUI_Main guiMain_;

	// Token: 0x04001269 RID: 4713
	private sfxScript sfx_;

	// Token: 0x0400126A RID: 4714
	private cameraMovementScript camMove_;

	// Token: 0x0400126B RID: 4715
	private mpCalls mpCalls_;
}
