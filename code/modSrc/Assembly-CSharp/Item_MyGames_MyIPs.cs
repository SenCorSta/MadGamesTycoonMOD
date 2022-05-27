using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EB RID: 235
public class Item_MyGames_MyIPs : MonoBehaviour
{
	// Token: 0x060007D1 RID: 2001 RVA: 0x00056FA4 File Offset: 0x000551A4
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x00056FAC File Offset: 0x000551AC
	public void Update()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00056FDC File Offset: 0x000551DC
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && this.game_.GameFromMitspieler())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
		this.guiMain_.DrawIpBekanntheit(this.uiObjects[1], this.game_);
		this.tooltip_.c = this.game_.GetTooltipIP();
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x000570C8 File Offset: 0x000552C8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[315].activeSelf)
		{
			this.guiMain_.uiObjects[316].SetActive(true);
			this.guiMain_.uiObjects[316].GetComponent<Menu_Stats_ShowMyIPs>().Init(this.game_);
			return;
		}
		if (this.guiMain_.uiObjects[355].activeSelf || this.guiMain_.uiObjects[361].activeSelf)
		{
			this.guiMain_.uiObjects[356].SetActive(true);
			this.guiMain_.uiObjects[356].GetComponent<Menu_Stats_ShowBestIPs>().Init(this.game_);
			return;
		}
	}

	// Token: 0x04000BE6 RID: 3046
	public GameObject[] uiObjects;

	// Token: 0x04000BE7 RID: 3047
	public mainScript mS_;

	// Token: 0x04000BE8 RID: 3048
	public textScript tS_;

	// Token: 0x04000BE9 RID: 3049
	public sfxScript sfx_;

	// Token: 0x04000BEA RID: 3050
	public GUI_Main guiMain_;

	// Token: 0x04000BEB RID: 3051
	public tooltip tooltip_;

	// Token: 0x04000BEC RID: 3052
	public gameScript game_;

	// Token: 0x04000BED RID: 3053
	public genres genres_;
}
