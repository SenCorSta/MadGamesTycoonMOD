using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D4 RID: 468
public class Menu_RandomEventCommercialFlop : MonoBehaviour
{
	// Token: 0x0600119C RID: 4508 RVA: 0x0000C556 File Offset: 0x0000A756
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000C5A1C File Offset: 0x000C3C1C
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

	// Token: 0x0600119E RID: 4510 RVA: 0x0000C55E File Offset: 0x0000A75E
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x000C5AC8 File Offset: 0x000C3CC8
	public void Init(gameScript gS_)
	{
		if (!gS_)
		{
			this.BUTTON_Abbrechen();
		}
		this.FindScripts();
		this.sfx_.PlaySound(53, true);
		string text = this.tS_.GetText(1758);
		text = text.Replace("<NAME>", gS_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		if (this.mS_.settings_ && this.mS_.settings_.hideEvents)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0000C579 File Offset: 0x0000A779
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0000C579 File Offset: 0x0000A779
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001629 RID: 5673
	public GameObject[] uiObjects;

	// Token: 0x0400162A RID: 5674
	private GameObject main_;

	// Token: 0x0400162B RID: 5675
	private mainScript mS_;

	// Token: 0x0400162C RID: 5676
	private textScript tS_;

	// Token: 0x0400162D RID: 5677
	private GUI_Main guiMain_;

	// Token: 0x0400162E RID: 5678
	private sfxScript sfx_;
}
