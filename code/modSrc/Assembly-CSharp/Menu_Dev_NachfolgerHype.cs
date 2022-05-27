using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013B RID: 315
public class Menu_Dev_NachfolgerHype : MonoBehaviour
{
	// Token: 0x06000B73 RID: 2931 RVA: 0x000081F4 File Offset: 0x000063F4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x0008D124 File Offset: 0x0008B324
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

	// Token: 0x06000B75 RID: 2933 RVA: 0x0008D1D0 File Offset: 0x0008B3D0
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		if (!gS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GAME_" + gS_.originalIP.ToString());
		if (gameObject)
		{
			gameScript component = gameObject.GetComponent<gameScript>();
			this.uiObjects[0].GetComponent<Text>().text = "+" + component.GetHypeNachfolger().ToString();
			string text = this.tS_.GetText(1195);
			text = text.Replace("<NAME>", "<color=blue>" + component.GetNameWithTag() + "</color>");
			this.uiObjects[1].GetComponent<Text>().text = text;
			gS_.hype = (float)component.GetHypeNachfolger();
		}
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x000081FC File Offset: 0x000063FC
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00008217 File Offset: 0x00006417
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0000823D File Offset: 0x0000643D
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04000FCB RID: 4043
	public GameObject[] uiObjects;

	// Token: 0x04000FCC RID: 4044
	private GameObject main_;

	// Token: 0x04000FCD RID: 4045
	private mainScript mS_;

	// Token: 0x04000FCE RID: 4046
	private textScript tS_;

	// Token: 0x04000FCF RID: 4047
	private GUI_Main guiMain_;

	// Token: 0x04000FD0 RID: 4048
	private sfxScript sfx_;
}
