using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FE RID: 254
public class Item_Fanshop : MonoBehaviour
{
	// Token: 0x06000841 RID: 2113 RVA: 0x00059AE8 File Offset: 0x00057CE8
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00059AF0 File Offset: 0x00057CF0
	public void Update()
	{
		if (!this.game_)
		{
			return;
		}
		if (this.game_.merchKeinVerkauf)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[13];
			this.uiObjects[3].GetComponent<tooltip>().c = this.tS_.GetText(1853);
			return;
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[14];
		this.uiObjects[3].GetComponent<tooltip>().c = this.tS_.GetText(1854);
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00059BA0 File Offset: 0x00057DA0
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
		this.guiMain_.DrawIpBekanntheit(this.uiObjects[1], this.game_);
		this.tooltip_.c = this.game_.GetTooltipIP();
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00059C08 File Offset: 0x00057E08
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[367].SetActive(true);
		this.guiMain_.uiObjects[367].GetComponent<Menu_Fanshop>().Init(this.game_);
	}

	// Token: 0x04000C80 RID: 3200
	public GameObject[] uiObjects;

	// Token: 0x04000C81 RID: 3201
	public mainScript mS_;

	// Token: 0x04000C82 RID: 3202
	public textScript tS_;

	// Token: 0x04000C83 RID: 3203
	public sfxScript sfx_;

	// Token: 0x04000C84 RID: 3204
	public GUI_Main guiMain_;

	// Token: 0x04000C85 RID: 3205
	public tooltip tooltip_;

	// Token: 0x04000C86 RID: 3206
	public gameScript game_;

	// Token: 0x04000C87 RID: 3207
	public genres genres_;
}
