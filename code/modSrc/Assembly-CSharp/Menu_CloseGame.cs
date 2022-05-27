using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class Menu_CloseGame : MonoBehaviour
{
	// Token: 0x06000D98 RID: 3480 RVA: 0x00009706 File Offset: 0x00007906
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x000A2E18 File Offset: 0x000A1018
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

	// Token: 0x06000D9A RID: 3482 RVA: 0x0000970E File Offset: 0x0000790E
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x00009729 File Offset: 0x00007929
	public void BUTTON_Yes()
	{
		Application.Quit();
	}

	// Token: 0x0400123E RID: 4670
	public GameObject[] uiObjects;

	// Token: 0x0400123F RID: 4671
	private GameObject main_;

	// Token: 0x04001240 RID: 4672
	private mainScript mS_;

	// Token: 0x04001241 RID: 4673
	private textScript tS_;

	// Token: 0x04001242 RID: 4674
	private GUI_Main guiMain_;

	// Token: 0x04001243 RID: 4675
	private sfxScript sfx_;
}
