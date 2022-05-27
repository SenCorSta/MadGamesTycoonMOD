using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000193 RID: 403
public class Menu_Firmenname : MonoBehaviour
{
	// Token: 0x06000F42 RID: 3906 RVA: 0x0000AD60 File Offset: 0x00008F60
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x000AF284 File Offset: 0x000AD484
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

	// Token: 0x06000F44 RID: 3908 RVA: 0x0000AD68 File Offset: 0x00008F68
	private void OnEnable()
	{
		this.Init();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x0000AD7C File Offset: 0x00008F7C
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0000AD9E File Offset: 0x00008F9E
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<InputField>().text = this.mS_.companyName;
		this.SetLogo(this.mS_.logo);
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0000ADD4 File Offset: 0x00008FD4
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x0000ADFC File Offset: 0x00008FFC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000AF360 File Offset: 0x000AD560
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

	// Token: 0x06000F4A RID: 3914 RVA: 0x0000AE22 File Offset: 0x00009022
	public void SetLogo(int i)
	{
		this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	// Token: 0x04001397 RID: 5015
	public GameObject[] uiObjects;

	// Token: 0x04001398 RID: 5016
	private GameObject main_;

	// Token: 0x04001399 RID: 5017
	private mainScript mS_;

	// Token: 0x0400139A RID: 5018
	private textScript tS_;

	// Token: 0x0400139B RID: 5019
	private GUI_Main guiMain_;

	// Token: 0x0400139C RID: 5020
	private sfxScript sfx_;

	// Token: 0x0400139D RID: 5021
	private cameraMovementScript cmS_;

	// Token: 0x0400139E RID: 5022
	private int logo = -1;
}
