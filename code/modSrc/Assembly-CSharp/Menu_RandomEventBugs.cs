using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D4 RID: 468
public class Menu_RandomEventBugs : MonoBehaviour
{
	// Token: 0x060011AF RID: 4527 RVA: 0x000BA584 File Offset: 0x000B8784
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x000BA58C File Offset: 0x000B878C
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

	// Token: 0x060011B1 RID: 4529 RVA: 0x000BA636 File Offset: 0x000B8836
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000BA654 File Offset: 0x000B8854
	public void Init(gameScript gS_)
	{
		if (!gS_)
		{
			this.BUTTON_Abbrechen();
		}
		this.FindScripts();
		this.sfx_.PlaySound(53, true);
		gS_.points_bugs += gS_.points_bugsInvis;
		gS_.points_bugsInvis = 0f;
		string text = this.tS_.GetText(1760);
		text = text.Replace("<NAME>", gS_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		if (this.mS_.settings_ && this.mS_.settings_.hideEvents)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000BA701 File Offset: 0x000B8901
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x000BA701 File Offset: 0x000B8901
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400162C RID: 5676
	public GameObject[] uiObjects;

	// Token: 0x0400162D RID: 5677
	private GameObject main_;

	// Token: 0x0400162E RID: 5678
	private mainScript mS_;

	// Token: 0x0400162F RID: 5679
	private textScript tS_;

	// Token: 0x04001630 RID: 5680
	private GUI_Main guiMain_;

	// Token: 0x04001631 RID: 5681
	private sfxScript sfx_;
}
