using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000141 RID: 321
public class Menu_Dev_PortHype : MonoBehaviour
{
	// Token: 0x06000BBB RID: 3003 RVA: 0x0007EB29 File Offset: 0x0007CD29
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x0007EB34 File Offset: 0x0007CD34
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

	// Token: 0x06000BBD RID: 3005 RVA: 0x0007EBE0 File Offset: 0x0007CDE0
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

	// Token: 0x06000BBE RID: 3006 RVA: 0x0007ECA9 File Offset: 0x0007CEA9
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0007ECC4 File Offset: 0x0007CEC4
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x0007ECEA File Offset: 0x0007CEEA
	public void BUTTON_Yes()
	{
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001002 RID: 4098
	public GameObject[] uiObjects;

	// Token: 0x04001003 RID: 4099
	private GameObject main_;

	// Token: 0x04001004 RID: 4100
	private mainScript mS_;

	// Token: 0x04001005 RID: 4101
	private textScript tS_;

	// Token: 0x04001006 RID: 4102
	private GUI_Main guiMain_;

	// Token: 0x04001007 RID: 4103
	private sfxScript sfx_;
}
