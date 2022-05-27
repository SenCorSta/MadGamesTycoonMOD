using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000100 RID: 256
public class Item_TochterfirmaIP : MonoBehaviour
{
	// Token: 0x06000844 RID: 2116 RVA: 0x000063AF File Offset: 0x000045AF
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x000063B7 File Offset: 0x000045B7
	public void Update()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0006BD68 File Offset: 0x00069F68
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && !this.game_.playerGame && this.game_.multiplayerSlot != -1 && this.game_.multiplayerSlot != this.mS_.GetMyMultiplayerID())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
		this.guiMain_.DrawIpBekanntheit(this.uiObjects[1], this.game_);
		this.tooltip_.c = this.game_.GetTooltipIP();
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0006BE54 File Offset: 0x0006A054
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_ipFocus[this.slot] = this.game_.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[400].GetComponent<Menu_Stats_TochterfirmaIP>().BUTTON_Close();
	}

	// Token: 0x04000C93 RID: 3219
	public GameObject[] uiObjects;

	// Token: 0x04000C94 RID: 3220
	public mainScript mS_;

	// Token: 0x04000C95 RID: 3221
	public textScript tS_;

	// Token: 0x04000C96 RID: 3222
	public sfxScript sfx_;

	// Token: 0x04000C97 RID: 3223
	public GUI_Main guiMain_;

	// Token: 0x04000C98 RID: 3224
	public tooltip tooltip_;

	// Token: 0x04000C99 RID: 3225
	public gameScript game_;

	// Token: 0x04000C9A RID: 3226
	public genres genres_;

	// Token: 0x04000C9B RID: 3227
	public publisherScript pS_;

	// Token: 0x04000C9C RID: 3228
	public int slot;
}
