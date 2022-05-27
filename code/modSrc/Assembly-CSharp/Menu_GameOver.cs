using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001C0 RID: 448
public class Menu_GameOver : MonoBehaviour
{
	// Token: 0x060010D3 RID: 4307 RVA: 0x0000BDB0 File Offset: 0x00009FB0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x000BFD4C File Offset: 0x000BDF4C
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

	// Token: 0x060010D5 RID: 4309 RVA: 0x0000BDB8 File Offset: 0x00009FB8
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x0000BDD3 File Offset: 0x00009FD3
	public void BUTTON_Yes()
	{
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().StopNetwork();
		}
		this.guiMain_.RemoveVectrocity();
		SceneManager.LoadScene("scene01");
	}

	// Token: 0x0400156C RID: 5484
	public GameObject[] uiObjects;

	// Token: 0x0400156D RID: 5485
	private GameObject main_;

	// Token: 0x0400156E RID: 5486
	private mainScript mS_;

	// Token: 0x0400156F RID: 5487
	private textScript tS_;

	// Token: 0x04001570 RID: 5488
	private GUI_Main guiMain_;

	// Token: 0x04001571 RID: 5489
	private sfxScript sfx_;
}
