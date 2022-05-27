using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000140 RID: 320
public class Menu_Dev_PortHype : MonoBehaviour
{
	// Token: 0x06000BA7 RID: 2983 RVA: 0x000083F2 File Offset: 0x000065F2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x0008E61C File Offset: 0x0008C81C
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

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0008E6C8 File Offset: 0x0008C8C8
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		if (!gS_)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("GAME_" + gS_.portID.ToString());
		if (gameObject)
		{
			gameScript component = gameObject.GetComponent<gameScript>();
			this.uiObjects[0].GetComponent<Text>().text = "+" + Mathf.RoundToInt(component.GetHype()).ToString();
			string text = this.tS_.GetText(1550);
			text = text.Replace("<NAME>", "<color=blue>" + component.GetNameWithTag() + "</color>");
			this.uiObjects[1].GetComponent<Text>().text = text;
			gS_.hype = component.GetHype();
		}
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x000083FA File Offset: 0x000065FA
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00008415 File Offset: 0x00006615
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0000843B File Offset: 0x0000663B
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04000FFA RID: 4090
	public GameObject[] uiObjects;

	// Token: 0x04000FFB RID: 4091
	private GameObject main_;

	// Token: 0x04000FFC RID: 4092
	private mainScript mS_;

	// Token: 0x04000FFD RID: 4093
	private textScript tS_;

	// Token: 0x04000FFE RID: 4094
	private GUI_Main guiMain_;

	// Token: 0x04000FFF RID: 4095
	private sfxScript sfx_;
}
