using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001B4 RID: 436
public class Menu_Messagebox : MonoBehaviour
{
	// Token: 0x0600106B RID: 4203 RVA: 0x0000B9C8 File Offset: 0x00009BC8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x000BB2C0 File Offset: 0x000B94C0
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

	// Token: 0x0600106D RID: 4205 RVA: 0x0000B9D0 File Offset: 0x00009BD0
	public void Init(string c, bool close)
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = c;
		this.closeMenu = close;
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x0000B9F2 File Offset: 0x00009BF2
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0000BA0D File Offset: 0x00009C0D
	public void BUTTON_Yes()
	{
		if (this.closeMenu)
		{
			this.guiMain_.CloseMenu();
		}
		this.BUTTON_Abbrechen();
	}

	// Token: 0x040014EA RID: 5354
	public GameObject[] uiObjects;

	// Token: 0x040014EB RID: 5355
	private GameObject main_;

	// Token: 0x040014EC RID: 5356
	private mainScript mS_;

	// Token: 0x040014ED RID: 5357
	private textScript tS_;

	// Token: 0x040014EE RID: 5358
	private GUI_Main guiMain_;

	// Token: 0x040014EF RID: 5359
	private sfxScript sfx_;

	// Token: 0x040014F0 RID: 5360
	private bool closeMenu;
}
