using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F5 RID: 245
public class Item_MyKonsolen_Umsatz : MonoBehaviour
{
	// Token: 0x06000804 RID: 2052 RVA: 0x00006306 File Offset: 0x00004506
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00006306 File Offset: 0x00004506
	private void Update()
	{
		this.SetData();
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0006A480 File Offset: 0x00068680
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

	// Token: 0x06000807 RID: 2055 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0006A5C8 File Offset: 0x000687C8
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
