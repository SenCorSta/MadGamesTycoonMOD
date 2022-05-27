using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D5 RID: 469
public class Menu_RandomEventCommercialFlop : MonoBehaviour
{
	// Token: 0x060011B6 RID: 4534 RVA: 0x000BA71B File Offset: 0x000B891B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x000BA724 File Offset: 0x000B8924
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

	// Token: 0x060011B8 RID: 4536 RVA: 0x000BA7CE File Offset: 0x000B89CE
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000BA7EC File Offset: 0x000B89EC
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

	// Token: 0x060011BA RID: 4538 RVA: 0x000BA87B File Offset: 0x000B8A7B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000BA87B File Offset: 0x000B8A7B
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001632 RID: 5682
	public GameObject[] uiObjects;

	// Token: 0x04001633 RID: 5683
	private GameObject main_;

	// Token: 0x04001634 RID: 5684
	private mainScript mS_;

	// Token: 0x04001635 RID: 5685
	private textScript tS_;

	// Token: 0x04001636 RID: 5686
	private GUI_Main guiMain_;

	// Token: 0x04001637 RID: 5687
	private sfxScript sfx_;
}
