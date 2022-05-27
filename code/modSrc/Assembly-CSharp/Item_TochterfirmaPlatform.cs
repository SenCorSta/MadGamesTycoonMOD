using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000101 RID: 257
public class Item_TochterfirmaPlatform : MonoBehaviour
{
	// Token: 0x0600084A RID: 2122 RVA: 0x000063E4 File Offset: 0x000045E4
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0006BEC4 File Offset: 0x0006A0C4
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.pS_.SetPic(this.uiObjects[1]);
		this.uiObjects[2].GetComponent<Text>().text = this.pS_.GetDateString();
		this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(220) + ": " + this.pS_.GetGames().ToString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(219) + ": " + this.pS_.GetMarktanteilString();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetPrice(), true);
		this.uiObjects[6].GetComponent<Text>().text = this.pS_.tech.ToString();
		if (this.pS_.internet)
		{
			this.uiObjects[10].SetActive(true);
		}
		else
		{
			this.uiObjects[10].SetActive(false);
		}
		this.guiMain_.DrawStars(this.uiObjects[7], this.pS_.erfahrung);
		this.uiObjects[9].GetComponent<Image>().sprite = this.pS_.GetComplexSprite();
		this.tooltip_.c = this.pS_.GetTooltip();
		this.uiObjects[11].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[11].GetComponent<tooltip>().c = this.pS_.GetTypString();
		if (this.pS_.vomMarktGenommen)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[2];
			return;
		}
		this.uiObjects[8].SetActive(false);
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0006C0D4 File Offset: 0x0006A2D4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pubS_.tf_platformFocus[this.slot] = this.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[402].GetComponent<Menu_Stats_TochterfirmaPlatform>().BUTTON_Close();
	}

	// Token: 0x04000C9D RID: 3229
	public int myID;

	// Token: 0x04000C9E RID: 3230
	public GameObject[] uiObjects;

	// Token: 0x04000C9F RID: 3231
	public mainScript mS_;

	// Token: 0x04000CA0 RID: 3232
	public textScript tS_;

	// Token: 0x04000CA1 RID: 3233
	public sfxScript sfx_;

	// Token: 0x04000CA2 RID: 3234
	public GUI_Main guiMain_;

	// Token: 0x04000CA3 RID: 3235
	public tooltip tooltip_;

	// Token: 0x04000CA4 RID: 3236
	public platformScript pS_;

	// Token: 0x04000CA5 RID: 3237
	public publisherScript pubS_;

	// Token: 0x04000CA6 RID: 3238
	public int slot;
}
