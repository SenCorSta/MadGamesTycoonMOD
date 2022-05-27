using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000181 RID: 385
public class Menu_Start : MonoBehaviour
{
	// Token: 0x06000E74 RID: 3700 RVA: 0x0009BD75 File Offset: 0x00099F75
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0009BD80 File Offset: 0x00099F80
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

	// Token: 0x06000E76 RID: 3702 RVA: 0x0009BE5A File Offset: 0x0009A05A
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0009BE68 File Offset: 0x0009A068
	private void OnDisable()
	{
		this.camMove_.disableMovement = false;
		this.guiMain_.CloseMenu();
		this.guiMain_.ShowInGameUI(true);
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x0009BE90 File Offset: 0x0009A090
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

	// Token: 0x06000E79 RID: 3705 RVA: 0x00002715 File Offset: 0x00000915
	private void Update()
	{
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0009BF14 File Offset: 0x0009A114
	public void BUTTON_NewGame()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		this.mS_.LoadContent();
		this.mS_.myID = 100000;
		this.mS_.myPubS_ = null;
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[159]);
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x0009BF80 File Offset: 0x0009A180
	public void BUTTON_Fortsetzen()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		int @int = PlayerPrefs.GetInt("ResumeGame");
		this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().BUTTON_LoadGame(@int);
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0009BFCD File Offset: 0x0009A1CD
	public void BUTTON_CloseGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[148]);
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x0009BFF8 File Offset: 0x0009A1F8
	public void BUTTON_Credits()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[149]);
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0009C023 File Offset: 0x0009A223
	public void BUTTON_LoadGame()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0009C05A File Offset: 0x0009A25A
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[169]);
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0009C088 File Offset: 0x0009A288
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

	// Token: 0x040012EB RID: 4843
	public GameObject[] uiObjects;

	// Token: 0x040012EC RID: 4844
	private GameObject main_;

	// Token: 0x040012ED RID: 4845
	private mainScript mS_;

	// Token: 0x040012EE RID: 4846
	private textScript tS_;

	// Token: 0x040012EF RID: 4847
	private GUI_Main guiMain_;

	// Token: 0x040012F0 RID: 4848
	private sfxScript sfx_;

	// Token: 0x040012F1 RID: 4849
	private cameraMovementScript camMove_;
}
