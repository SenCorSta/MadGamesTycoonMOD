using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000148 RID: 328
public class Menu_Dev_SpinoffHype : MonoBehaviour
{
	// Token: 0x06000BF8 RID: 3064 RVA: 0x00008682 File Offset: 0x00006882
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x000916DC File Offset: 0x0008F8DC
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

	// Token: 0x06000BFA RID: 3066 RVA: 0x00091788 File Offset: 0x0008F988
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
			this.uiObjects[0].GetComponent<Text>().text = "+" + component.GetHypeSpinoff().ToString();
			string text = this.tS_.GetText(1542);
			text = text.Replace("<NAME>", "<color=blue>" + component.GetNameWithTag() + "</color>");
			this.uiObjects[1].GetComponent<Text>().text = text;
			gS_.hype = (float)component.GetHypeSpinoff();
		}
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0000868A File Offset: 0x0000688A
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x000086A5 File Offset: 0x000068A5
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x000086CB File Offset: 0x000068CB
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x0400104A RID: 4170
	public GameObject[] uiObjects;

	// Token: 0x0400104B RID: 4171
	private GameObject main_;

	// Token: 0x0400104C RID: 4172
	private mainScript mS_;

	// Token: 0x0400104D RID: 4173
	private textScript tS_;

	// Token: 0x0400104E RID: 4174
	private GUI_Main guiMain_;

	// Token: 0x0400104F RID: 4175
	private sfxScript sfx_;
}
