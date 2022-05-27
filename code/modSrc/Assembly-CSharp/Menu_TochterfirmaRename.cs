using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000261 RID: 609
public class Menu_TochterfirmaRename : MonoBehaviour
{
	// Token: 0x0600179E RID: 6046 RVA: 0x00010718 File Offset: 0x0000E918
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x000F4538 File Offset: 0x000F2738
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

	// Token: 0x060017A0 RID: 6048 RVA: 0x00010720 File Offset: 0x0000E920
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x00010734 File Offset: 0x0000E934
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x00010756 File Offset: 0x0000E956
	public void Init(publisherScript script_)
	{
		this.FindScripts();
		this.pS_ = script_;
		this.uiObjects[0].GetComponent<InputField>().text = this.pS_.GetName();
		this.SetLogo(this.pS_.logoID);
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x000F4614 File Offset: 0x000F2814
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[392]);
		this.guiMain_.uiObjects[392].GetComponent<Menu_TochterfirmaLogo>().Init(this.pS_);
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x00010793 File Offset: 0x0000E993
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000F466C File Offset: 0x000F286C
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

	// Token: 0x060017A6 RID: 6054 RVA: 0x000107AE File Offset: 0x0000E9AE
	public void SetLogo(int i)
	{
		this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	// Token: 0x04001B79 RID: 7033
	public GameObject[] uiObjects;

	// Token: 0x04001B7A RID: 7034
	private GameObject main_;

	// Token: 0x04001B7B RID: 7035
	private mainScript mS_;

	// Token: 0x04001B7C RID: 7036
	private textScript tS_;

	// Token: 0x04001B7D RID: 7037
	private GUI_Main guiMain_;

	// Token: 0x04001B7E RID: 7038
	private sfxScript sfx_;

	// Token: 0x04001B7F RID: 7039
	private cameraMovementScript cmS_;

	// Token: 0x04001B80 RID: 7040
	private int logo = -1;

	// Token: 0x04001B81 RID: 7041
	public publisherScript pS_;
}
