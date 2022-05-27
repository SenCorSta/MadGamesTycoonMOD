using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000180 RID: 384
public class Menu_Start : MonoBehaviour
{
	// Token: 0x06000E5C RID: 3676 RVA: 0x0000A0C1 File Offset: 0x000082C1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x000A9838 File Offset: 0x000A7A38
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

	// Token: 0x06000E5E RID: 3678 RVA: 0x0000A0C9 File Offset: 0x000082C9
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0000A0D7 File Offset: 0x000082D7
	private void OnDisable()
	{
		this.camMove_.disableMovement = false;
		this.guiMain_.CloseMenu();
		this.guiMain_.ShowInGameUI(true);
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x000A9914 File Offset: 0x000A7B14
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

	// Token: 0x06000E61 RID: 3681 RVA: 0x00002098 File Offset: 0x00000298
	private void Update()
	{
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x000A9998 File Offset: 0x000A7B98
	public void BUTTON_NewGame()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		this.mS_.LoadContent();
		this.mS_.mpCalls_.myID = 0;
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[159]);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x000A99F8 File Offset: 0x000A7BF8
	public void BUTTON_Fortsetzen()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		int @int = PlayerPrefs.GetInt("ResumeGame");
		this.guiMain_.uiObjects[150].GetComponent<Menu_LoadGame>().BUTTON_LoadGame(@int);
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0000A0FC File Offset: 0x000082FC
	public void BUTTON_CloseGame()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[148]);
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x0000A127 File Offset: 0x00008327
	public void BUTTON_Credits()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[149]);
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x0000A152 File Offset: 0x00008352
	public void BUTTON_LoadGame()
	{
		this.mS_.multiplayer = false;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[150]);
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0000A189 File Offset: 0x00008389
	public void BUTTON_Options()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[169]);
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x000A9A48 File Offset: 0x000A7C48
	public void BUTTON_Multiplayer()
	{
		this.mS_.multiplayer = true;
		this.mS_.mpCalls_.myID = -1;
		this.mS_.LoadContent();
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[201]);
		this.guiMain_.uiObjects[201].GetComponent<mpMain>().BUTTON_StartHost();
	}

	// Token: 0x040012E2 RID: 4834
	public GameObject[] uiObjects;

	// Token: 0x040012E3 RID: 4835
	private GameObject main_;

	// Token: 0x040012E4 RID: 4836
	private mainScript mS_;

	// Token: 0x040012E5 RID: 4837
	private textScript tS_;

	// Token: 0x040012E6 RID: 4838
	private GUI_Main guiMain_;

	// Token: 0x040012E7 RID: 4839
	private sfxScript sfx_;

	// Token: 0x040012E8 RID: 4840
	private cameraMovementScript camMove_;
}
