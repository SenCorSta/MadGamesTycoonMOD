using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D6 RID: 470
public class Menu_RandomEventCommercialHit : MonoBehaviour
{
	// Token: 0x060011BD RID: 4541 RVA: 0x000BA8AD File Offset: 0x000B8AAD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x000BA8B8 File Offset: 0x000B8AB8
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

	// Token: 0x060011BF RID: 4543 RVA: 0x000BA962 File Offset: 0x000B8B62
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x000BA980 File Offset: 0x000B8B80
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

	// Token: 0x060011C1 RID: 4545 RVA: 0x000BAA0F File Offset: 0x000B8C0F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x000BAA0F File Offset: 0x000B8C0F
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001638 RID: 5688
	public GameObject[] uiObjects;

	// Token: 0x04001639 RID: 5689
	private GameObject main_;

	// Token: 0x0400163A RID: 5690
	private mainScript mS_;

	// Token: 0x0400163B RID: 5691
	private textScript tS_;

	// Token: 0x0400163C RID: 5692
	private GUI_Main guiMain_;

	// Token: 0x0400163D RID: 5693
	private sfxScript sfx_;
}
