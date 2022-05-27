using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F4 RID: 244
public class Item_MyKonsolen_Sells : MonoBehaviour
{
	// Token: 0x06000807 RID: 2055 RVA: 0x00058246 File Offset: 0x00056446
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00058250 File Offset: 0x00056450
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.units, false);
		base.gameObject.name = this.pS_.units.ToString();
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x000582DC File Offset: 0x000564DC
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.units, false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x000583A0 File Offset: 0x000565A0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[333]);
		this.guiMain_.uiObjects[333].GetComponent<Menu_Umsatz_Konsole>().Init(this.pS_);
	}

	// Token: 0x04000C2E RID: 3118
	public GameObject[] uiObjects;

	// Token: 0x04000C2F RID: 3119
	public mainScript mS_;

	// Token: 0x04000C30 RID: 3120
	public textScript tS_;

	// Token: 0x04000C31 RID: 3121
	public sfxScript sfx_;

	// Token: 0x04000C32 RID: 3122
	public GUI_Main guiMain_;

	// Token: 0x04000C33 RID: 3123
	public tooltip tooltip_;

	// Token: 0x04000C34 RID: 3124
	public platformScript pS_;
}
