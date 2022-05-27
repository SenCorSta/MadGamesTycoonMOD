using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000100 RID: 256
public class Item_TochterfirmaIP : MonoBehaviour
{
	// Token: 0x0600084D RID: 2125 RVA: 0x00059E96 File Offset: 0x00058096
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x00059E9E File Offset: 0x0005809E
	public void Update()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x00059ECC File Offset: 0x000580CC
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

	// Token: 0x06000850 RID: 2128 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00059FB8 File Offset: 0x000581B8
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
