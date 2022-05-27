using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D5 RID: 469
public class Menu_RandomEventCommercialHit : MonoBehaviour
{
	// Token: 0x060011A3 RID: 4515 RVA: 0x0000C59F File Offset: 0x0000A79F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000C5B58 File Offset: 0x000C3D58
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

	// Token: 0x060011A5 RID: 4517 RVA: 0x0000C5A7 File Offset: 0x0000A7A7
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x000C5C04 File Offset: 0x000C3E04
	public void Init(gameScript gS_)
	{
		if (!gS_)
		{
			this.BUTTON_Abbrechen();
		}
		this.FindScripts();
		this.sfx_.PlaySound(54, true);
		string text = this.tS_.GetText(1763);
		text = text.Replace("<NAME>", gS_.GetNameWithTag());
		this.uiObjects[0].GetComponent<Text>().text = text;
		if (this.mS_.settings_ && this.mS_.settings_.hideEvents)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400162F RID: 5679
	public GameObject[] uiObjects;

	// Token: 0x04001630 RID: 5680
	private GameObject main_;

	// Token: 0x04001631 RID: 5681
	private mainScript mS_;

	// Token: 0x04001632 RID: 5682
	private textScript tS_;

	// Token: 0x04001633 RID: 5683
	private GUI_Main guiMain_;

	// Token: 0x04001634 RID: 5684
	private sfxScript sfx_;
}
