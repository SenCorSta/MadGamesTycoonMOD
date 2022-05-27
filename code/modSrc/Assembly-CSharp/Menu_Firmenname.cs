using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000194 RID: 404
public class Menu_Firmenname : MonoBehaviour
{
	// Token: 0x06000F5A RID: 3930 RVA: 0x000A24F6 File Offset: 0x000A06F6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000A2500 File Offset: 0x000A0700
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

	// Token: 0x06000F5C RID: 3932 RVA: 0x000A25DA File Offset: 0x000A07DA
	private void OnEnable()
	{
		this.Init();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x000A25EE File Offset: 0x000A07EE
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x000A2610 File Offset: 0x000A0810
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<InputField>().text = this.mS_.GetCompanyName();
		this.SetLogo(this.mS_.GetCompanyLogoID());
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000A2646 File Offset: 0x000A0846
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000A266E File Offset: 0x000A086E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x000A2694 File Offset: 0x000A0894
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(814), false);
			return;
		}
		this.mS_.SetCompanyName(this.uiObjects[0].GetComponent<InputField>().text);
		this.mS_.SetCompanyLogoID(this.logo);
		this.guiMain_.SetMainGuiData();
		this.BUTTON_Abbrechen();
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000A2717 File Offset: 0x000A0917
	public void SetLogo(int i)
	{
		this.uiObjects[1].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	// Token: 0x040013A0 RID: 5024
	public GameObject[] uiObjects;

	// Token: 0x040013A1 RID: 5025
	private GameObject main_;

	// Token: 0x040013A2 RID: 5026
	private mainScript mS_;

	// Token: 0x040013A3 RID: 5027
	private textScript tS_;

	// Token: 0x040013A4 RID: 5028
	private GUI_Main guiMain_;

	// Token: 0x040013A5 RID: 5029
	private sfxScript sfx_;

	// Token: 0x040013A6 RID: 5030
	private cameraMovementScript cmS_;

	// Token: 0x040013A7 RID: 5031
	private int logo = -1;
}
