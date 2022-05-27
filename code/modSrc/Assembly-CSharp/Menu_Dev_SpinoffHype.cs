using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000149 RID: 329
public class Menu_Dev_SpinoffHype : MonoBehaviour
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x00081EB3 File Offset: 0x000800B3
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x00081EBC File Offset: 0x000800BC
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

	// Token: 0x06000C0F RID: 3087 RVA: 0x00081F68 File Offset: 0x00080168
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

	// Token: 0x06000C10 RID: 3088 RVA: 0x0008202D File Offset: 0x0008022D
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x00082048 File Offset: 0x00080248
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x0008206E File Offset: 0x0008026E
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001052 RID: 4178
	public GameObject[] uiObjects;

	// Token: 0x04001053 RID: 4179
	private GameObject main_;

	// Token: 0x04001054 RID: 4180
	private mainScript mS_;

	// Token: 0x04001055 RID: 4181
	private textScript tS_;

	// Token: 0x04001056 RID: 4182
	private GUI_Main guiMain_;

	// Token: 0x04001057 RID: 4183
	private sfxScript sfx_;
}
