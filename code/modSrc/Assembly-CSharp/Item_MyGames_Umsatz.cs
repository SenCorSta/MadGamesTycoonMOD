using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F0 RID: 240
public class Item_MyGames_Umsatz : MonoBehaviour
{
	// Token: 0x060007EF RID: 2031 RVA: 0x00057A24 File Offset: 0x00055C24
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00057A24 File Offset: 0x00055C24
	private void Update()
	{
		this.SetData();
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00057A2C File Offset: 0x00055C2C
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.pubOffer)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[23];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		if (this.tooltip_.c.Length <= 0)
		{
			this.tooltip_.c = this.game_.GetTooltip();
		}
		long num = 0L;
		switch (this.menu_.uiObjects[4].GetComponent<Dropdown>().value)
		{
		case 0:
			num = this.game_.GetGesamtGewinn();
			break;
		case 1:
			num = this.game_.umsatzTotal;
			break;
		case 2:
			num = this.game_.umsatzAbos;
			break;
		case 3:
			num = this.game_.umsatzInApp;
			break;
		case 4:
			num = this.game_.GetEntwicklungskosten();
			break;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(num, true);
		if (num < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			return;
		}
		this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[15];
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00057BF0 File Offset: 0x00055DF0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[91]);
		this.guiMain_.uiObjects[91].GetComponent<Menu_Game_Umsatz>().Init(this.game_);
	}

	// Token: 0x04000C0F RID: 3087
	public GameObject[] uiObjects;

	// Token: 0x04000C10 RID: 3088
	public mainScript mS_;

	// Token: 0x04000C11 RID: 3089
	public textScript tS_;

	// Token: 0x04000C12 RID: 3090
	public sfxScript sfx_;

	// Token: 0x04000C13 RID: 3091
	public GUI_Main guiMain_;

	// Token: 0x04000C14 RID: 3092
	public tooltip tooltip_;

	// Token: 0x04000C15 RID: 3093
	public gameScript game_;

	// Token: 0x04000C16 RID: 3094
	public genres genres_;

	// Token: 0x04000C17 RID: 3095
	public Menu_Stats_MyGames_Umsatz menu_;
}
