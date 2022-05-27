using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class Menu_CloseGame : MonoBehaviour
{
	// Token: 0x06000DB0 RID: 3504 RVA: 0x000948B8 File Offset: 0x00092AB8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x000948C0 File Offset: 0x00092AC0
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0009496A File Offset: 0x00092B6A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00094985 File Offset: 0x00092B85
	public void BUTTON_Yes()
	{
		Application.Quit();
	}

	// Token: 0x04001246 RID: 4678
	public GameObject[] uiObjects;

	// Token: 0x04001247 RID: 4679
	private GameObject main_;

	// Token: 0x04001248 RID: 4680
	private mainScript mS_;

	// Token: 0x04001249 RID: 4681
	private textScript tS_;

	// Token: 0x0400124A RID: 4682
	private GUI_Main guiMain_;

	// Token: 0x0400124B RID: 4683
	private sfxScript sfx_;
}
