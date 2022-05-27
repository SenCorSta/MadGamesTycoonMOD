using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200016D RID: 365
public class Menu_BackToMainMenu : MonoBehaviour
{
	// Token: 0x06000D93 RID: 3475 RVA: 0x000096A4 File Offset: 0x000078A4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x000A2D6C File Offset: 0x000A0F6C
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

	// Token: 0x06000D95 RID: 3477 RVA: 0x000096AC File Offset: 0x000078AC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x000096C7 File Offset: 0x000078C7
	public void BUTTON_Yes()
	{
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().StopNetwork();
		}
		this.guiMain_.RemoveVectrocity();
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x04001238 RID: 4664
	public GameObject[] uiObjects;

	// Token: 0x04001239 RID: 4665
	private GameObject main_;

	// Token: 0x0400123A RID: 4666
	private mainScript mS_;

	// Token: 0x0400123B RID: 4667
	private textScript tS_;

	// Token: 0x0400123C RID: 4668
	private GUI_Main guiMain_;

	// Token: 0x0400123D RID: 4669
	private sfxScript sfx_;
}
