using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FE RID: 254
public class Item_Fanshop : MonoBehaviour
{
	// Token: 0x06000838 RID: 2104 RVA: 0x0000639F File Offset: 0x0000459F
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0006B9C8 File Offset: 0x00069BC8
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

	// Token: 0x0600083A RID: 2106 RVA: 0x0006BA78 File Offset: 0x00069C78
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

	// Token: 0x0600083B RID: 2107 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0006BAE0 File Offset: 0x00069CE0
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
