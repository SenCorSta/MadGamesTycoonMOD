using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BF RID: 191
public class Item_SellLicence : MonoBehaviour
{
	// Token: 0x060006B9 RID: 1721 RVA: 0x000518AE File Offset: 0x0004FAAE
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x000518B8 File Offset: 0x0004FAB8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.licences_.GetSellPrice(this.myID), true);
		this.uiObjects[5].GetComponent<Image>().sprite = this.licences_.licenceSprites[this.licences_.licence_TYP[this.myID]];
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.licences_.licence_QUALITY[this.myID] / 20f));
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.licences_.licence_GEKAUFT[this.myID].ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.licences_.GetTypString(this.myID);
		this.tooltip_.c = this.licences_.GetTooltip(this.myID);
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00051A04 File Offset: 0x0004FC04
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[55]);
		this.guiMain_.uiObjects[55].GetComponent<Menu_W_SellLicence>().Init(this.myID);
	}

	// Token: 0x04000A68 RID: 2664
	public int myID = -1;

	// Token: 0x04000A69 RID: 2665
	public licences licences_;

	// Token: 0x04000A6A RID: 2666
	public GameObject[] uiObjects;

	// Token: 0x04000A6B RID: 2667
	public mainScript mS_;

	// Token: 0x04000A6C RID: 2668
	public textScript tS_;

	// Token: 0x04000A6D RID: 2669
	public sfxScript sfx_;

	// Token: 0x04000A6E RID: 2670
	public GUI_Main guiMain_;

	// Token: 0x04000A6F RID: 2671
	public tooltip tooltip_;
}
