using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000174 RID: 372
public class Menu_InGameOptions : MonoBehaviour
{
	// Token: 0x06000DCD RID: 3533 RVA: 0x00095117 File Offset: 0x00093317
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00095120 File Offset: 0x00093320
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

	// Token: 0x06000DCF RID: 3535 RVA: 0x0009521C File Offset: 0x0009341C
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
		{
			this.guiMain_.BUTTON_GameSpeed(0f);
		}
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x00095259 File Offset: 0x00093459
	private void OnDisable()
	{
		this.camMove_.disableMovement = false;
		this.guiMain_.CloseMenu();
		this.guiMain_.ShowInGameUI(true);
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x00095280 File Offset: 0x00093480
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

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0009573B File Offset: 0x0009393B
	public void BUTTON_ExitToOS()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[148]);
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x00095766 File Offset: 0x00093966
	public void BUTTON_ExitToMainMenu()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[154]);
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x00095791 File Offset: 0x00093991
	public void BUTTON_LoadGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000957BC File Offset: 0x000939BC
	public void BUTTON_SaveGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[156]);
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000957E7 File Offset: 0x000939E7
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00095802 File Offset: 0x00093A02
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[169]);
	}

	// Token: 0x0400126C RID: 4716
	public GameObject[] uiObjects;

	// Token: 0x0400126D RID: 4717
	private GameObject main_;

	// Token: 0x0400126E RID: 4718
	private mainScript mS_;

	// Token: 0x0400126F RID: 4719
	private textScript tS_;

	// Token: 0x04001270 RID: 4720
	private GUI_Main guiMain_;

	// Token: 0x04001271 RID: 4721
	private sfxScript sfx_;

	// Token: 0x04001272 RID: 4722
	private cameraMovementScript camMove_;

	// Token: 0x04001273 RID: 4723
	private mpCalls mpCalls_;
}
