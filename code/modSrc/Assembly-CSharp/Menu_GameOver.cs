using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001C1 RID: 449
public class Menu_GameOver : MonoBehaviour
{
	// Token: 0x060010ED RID: 4333 RVA: 0x000B4217 File Offset: 0x000B2417
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000B4220 File Offset: 0x000B2420
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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

	// Token: 0x060010EF RID: 4335 RVA: 0x000B42CA File Offset: 0x000B24CA
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x000B42E5 File Offset: 0x000B24E5
	public void BUTTON_Yes()
	{
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().StopNetwork();
		}
		this.guiMain_.RemoveVectrocity();
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x04001575 RID: 5493
	public GameObject[] uiObjects;

	// Token: 0x04001576 RID: 5494
	private GameObject main_;

	// Token: 0x04001577 RID: 5495
	private mainScript mS_;

	// Token: 0x04001578 RID: 5496
	private textScript tS_;

	// Token: 0x04001579 RID: 5497
	private GUI_Main guiMain_;

	// Token: 0x0400157A RID: 5498
	private sfxScript sfx_;
}
