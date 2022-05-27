using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D3 RID: 467
public class Menu_RandomEventBugs : MonoBehaviour
{
	// Token: 0x06001195 RID: 4501 RVA: 0x0000C50D File Offset: 0x0000A70D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x000C58C0 File Offset: 0x000C3AC0
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

	// Token: 0x06001197 RID: 4503 RVA: 0x0000C515 File Offset: 0x0000A715
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x000C596C File Offset: 0x000C3B6C
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

	// Token: 0x06001199 RID: 4505 RVA: 0x0000C530 File Offset: 0x0000A730
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x0000C530 File Offset: 0x0000A730
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001623 RID: 5667
	public GameObject[] uiObjects;

	// Token: 0x04001624 RID: 5668
	private GameObject main_;

	// Token: 0x04001625 RID: 5669
	private mainScript mS_;

	// Token: 0x04001626 RID: 5670
	private textScript tS_;

	// Token: 0x04001627 RID: 5671
	private GUI_Main guiMain_;

	// Token: 0x04001628 RID: 5672
	private sfxScript sfx_;
}
