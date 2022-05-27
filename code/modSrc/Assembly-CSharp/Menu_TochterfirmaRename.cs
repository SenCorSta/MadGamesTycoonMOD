using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000265 RID: 613
public class Menu_TochterfirmaRename : MonoBehaviour
{
	// Token: 0x060017E1 RID: 6113 RVA: 0x000EED56 File Offset: 0x000ECF56
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x000EED60 File Offset: 0x000ECF60
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

	// Token: 0x060017E3 RID: 6115 RVA: 0x000EEE3A File Offset: 0x000ED03A
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x000EEE4E File Offset: 0x000ED04E
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x000EEE70 File Offset: 0x000ED070
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<InputField>().text = this.pS_.GetName();
		this.SetLogo(this.pS_.logoID);
	}

	// Token: 0x060017E6 RID: 6118 RVA: 0x000EEEB0 File Offset: 0x000ED0B0
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[392]);
		this.guiMain_.uiObjects[392].GetComponent<Menu_TochterfirmaLogo>().Init(this.pS_);
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000EEF07 File Offset: 0x000ED107
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x000EEF24 File Offset: 0x000ED124
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1941), false);
			return;
		}
		this.pS_.logoID = this.logo;
		this.pS_.SetOwnName(this.uiObjects[0].GetComponent<InputField>().text);
		if (this.guiMain_.uiObjects[387].activeSelf)
		{
			this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().UpdateData();
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x000EEFCF File Offset: 0x000ED1CF
	public void SetLogo(int i)
	{
		this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	// Token: 0x04001B93 RID: 7059
	public GameObject[] uiObjects;

	// Token: 0x04001B94 RID: 7060
	private GameObject main_;

	// Token: 0x04001B95 RID: 7061
	private mainScript mS_;

	// Token: 0x04001B96 RID: 7062
	private textScript tS_;

	// Token: 0x04001B97 RID: 7063
	private GUI_Main guiMain_;

	// Token: 0x04001B98 RID: 7064
	private sfxScript sfx_;

	// Token: 0x04001B99 RID: 7065
	private cameraMovementScript cmS_;

	// Token: 0x04001B9A RID: 7066
	private int logo = -1;

	// Token: 0x04001B9B RID: 7067
	public publisherScript pS_;
}
