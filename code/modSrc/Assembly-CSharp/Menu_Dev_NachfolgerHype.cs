using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013C RID: 316
public class Menu_Dev_NachfolgerHype : MonoBehaviour
{
	// Token: 0x06000B86 RID: 2950 RVA: 0x0007D42B File Offset: 0x0007B62B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0007D434 File Offset: 0x0007B634
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

	// Token: 0x06000B88 RID: 2952 RVA: 0x0007D4E0 File Offset: 0x0007B6E0
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

	// Token: 0x06000B89 RID: 2953 RVA: 0x0007D5A5 File Offset: 0x0007B7A5
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0007D5C0 File Offset: 0x0007B7C0
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0007D5E6 File Offset: 0x0007B7E6
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04000FD3 RID: 4051
	public GameObject[] uiObjects;

	// Token: 0x04000FD4 RID: 4052
	private GameObject main_;

	// Token: 0x04000FD5 RID: 4053
	private mainScript mS_;

	// Token: 0x04000FD6 RID: 4054
	private textScript tS_;

	// Token: 0x04000FD7 RID: 4055
	private GUI_Main guiMain_;

	// Token: 0x04000FD8 RID: 4056
	private sfxScript sfx_;
}
