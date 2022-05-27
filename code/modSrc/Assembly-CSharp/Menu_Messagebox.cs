using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B5 RID: 437
public class Menu_Messagebox : MonoBehaviour
{
	// Token: 0x06001085 RID: 4229 RVA: 0x000AF2DF File Offset: 0x000AD4DF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000AF2E8 File Offset: 0x000AD4E8
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

	// Token: 0x06001087 RID: 4231 RVA: 0x000AF392 File Offset: 0x000AD592
	public void Init(string c, bool close)
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = c;
		this.closeMenu = close;
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x000AF3B4 File Offset: 0x000AD5B4
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x000AF3CF File Offset: 0x000AD5CF
	public void BUTTON_Yes()
	{
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040014F5 RID: 5365
	public GameObject[] uiObjects;

	// Token: 0x040014F6 RID: 5366
	private GameObject main_;

	// Token: 0x040014F7 RID: 5367
	private mainScript mS_;

	// Token: 0x040014F8 RID: 5368
	private textScript tS_;

	// Token: 0x040014F9 RID: 5369
	private GUI_Main guiMain_;

	// Token: 0x040014FA RID: 5370
	private sfxScript sfx_;

	// Token: 0x040014FB RID: 5371
	private bool closeMenu;
}
