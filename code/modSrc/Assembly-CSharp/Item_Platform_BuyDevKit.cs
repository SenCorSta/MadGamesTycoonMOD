using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CA RID: 202
public class Item_Platform_BuyDevKit : MonoBehaviour
{
	// Token: 0x060006F4 RID: 1780 RVA: 0x00005DAC File Offset: 0x00003FAC
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0006558C File Offset: 0x0006378C
	private void SetData()
	{
		if (this.pS_.inBesitz)
		{
			base.gameObject.GetComponent<Button>().interactable = false;
		}
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

	// Token: 0x060006F6 RID: 1782 RVA: 0x00005DB4 File Offset: 0x00003FB4
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x000657BC File Offset: 0x000639BC
	public void BUTTON_Click()
	{
		if (!this.pS_.inBesitz)
		{
			this.sfx_.PlaySound(3, false);
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[35]);
			this.guiMain_.uiObjects[35].GetComponent<Menu_W_BuyDevKit>().Init(this.pS_);
		}
	}

	// Token: 0x04000AC2 RID: 2754
	public int myID;

	// Token: 0x04000AC3 RID: 2755
	public GameObject[] uiObjects;

	// Token: 0x04000AC4 RID: 2756
	public mainScript mS_;

	// Token: 0x04000AC5 RID: 2757
	public textScript tS_;

	// Token: 0x04000AC6 RID: 2758
	public sfxScript sfx_;

	// Token: 0x04000AC7 RID: 2759
	public GUI_Main guiMain_;

	// Token: 0x04000AC8 RID: 2760
	public tooltip tooltip_;

	// Token: 0x04000AC9 RID: 2761
	public platformScript pS_;

	// Token: 0x04000ACA RID: 2762
	private float updateTimer;
}
