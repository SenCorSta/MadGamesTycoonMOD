using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000102 RID: 258
public class Item_TochterfirmaPlatform : MonoBehaviour
{
	// Token: 0x06000859 RID: 2137 RVA: 0x0005A348 File Offset: 0x00058548
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0005A350 File Offset: 0x00058550
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

	// Token: 0x0600085B RID: 2139 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0005A560 File Offset: 0x00058760
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pubS_.tf_platformFocus[this.slot] = this.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[402].GetComponent<Menu_Stats_TochterfirmaPlatform>().BUTTON_Close();
	}

	// Token: 0x04000CA5 RID: 3237
	public int myID;

	// Token: 0x04000CA6 RID: 3238
	public GameObject[] uiObjects;

	// Token: 0x04000CA7 RID: 3239
	public mainScript mS_;

	// Token: 0x04000CA8 RID: 3240
	public textScript tS_;

	// Token: 0x04000CA9 RID: 3241
	public sfxScript sfx_;

	// Token: 0x04000CAA RID: 3242
	public GUI_Main guiMain_;

	// Token: 0x04000CAB RID: 3243
	public tooltip tooltip_;

	// Token: 0x04000CAC RID: 3244
	public platformScript pS_;

	// Token: 0x04000CAD RID: 3245
	public publisherScript pubS_;

	// Token: 0x04000CAE RID: 3246
	public int slot;
}
