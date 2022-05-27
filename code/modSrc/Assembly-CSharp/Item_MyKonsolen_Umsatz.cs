using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F5 RID: 245
public class Item_MyKonsolen_Umsatz : MonoBehaviour
{
	// Token: 0x0600080D RID: 2061 RVA: 0x000583F7 File Offset: 0x000565F7
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x000583F7 File Offset: 0x000565F7
	private void Update()
	{
		this.SetData();
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00058400 File Offset: 0x00056600
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		if (this.tooltip_.c.Length <= 0)
		{
			this.tooltip_.c = this.pS_.GetTooltip();
		}
		long num = 0L;
		switch (this.menu_.uiObjects[4].GetComponent<Dropdown>().value)
		{
		case 0:
			num = this.pS_.GetGesamtGewinn();
			break;
		case 1:
			num = this.pS_.umsatzTotal;
			break;
		case 2:
			num = this.pS_.GetEntwicklungskosten();
			break;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(num, true);
		if (num < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00058548 File Offset: 0x00056748
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[333]);
		this.guiMain_.uiObjects[333].GetComponent<Menu_Umsatz_Konsole>().Init(this.pS_);
	}

	// Token: 0x04000C35 RID: 3125
	public GameObject[] uiObjects;

	// Token: 0x04000C36 RID: 3126
	public mainScript mS_;

	// Token: 0x04000C37 RID: 3127
	public textScript tS_;

	// Token: 0x04000C38 RID: 3128
	public sfxScript sfx_;

	// Token: 0x04000C39 RID: 3129
	public GUI_Main guiMain_;

	// Token: 0x04000C3A RID: 3130
	public tooltip tooltip_;

	// Token: 0x04000C3B RID: 3131
	public genres genres_;

	// Token: 0x04000C3C RID: 3132
	public Menu_Stats_MyKonsolen_Umsatz menu_;

	// Token: 0x04000C3D RID: 3133
	public platformScript pS_;
}
