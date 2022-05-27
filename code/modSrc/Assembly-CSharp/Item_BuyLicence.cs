using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B7 RID: 183
public class Item_BuyLicence : MonoBehaviour
{
	// Token: 0x06000688 RID: 1672 RVA: 0x00005B7C File Offset: 0x00003D7C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00005B84 File Offset: 0x00003D84
	private void Update()
	{
		if (this.licences_.licence_ANGEBOT[this.myID] <= 0 && this.licences_.licence_GEKAUFT[this.myID] <= 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x000635F8 File Offset: 0x000617F8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.licences_.GetPrice(this.myID), true);
		this.uiObjects[5].GetComponent<Image>().sprite = this.licences_.licenceSprites[this.licences_.licence_TYP[this.myID]];
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.licences_.licence_QUALITY[this.myID] / 20f));
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.licences_.licence_ANGEBOT[this.myID].ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.licences_.GetTypString(this.myID);
		this.tooltip_.c = this.licences_.GetTooltip(this.myID);
		if (this.licences_.licence_GEKAUFT[this.myID] > 0)
		{
			base.GetComponent<Button>().interactable = false;
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(307);
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00063788 File Offset: 0x00061988
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[53]);
		this.guiMain_.uiObjects[53].GetComponent<Menu_W_BuyLicence>().Init(this.myID);
	}

	// Token: 0x04000A31 RID: 2609
	public int myID = -1;

	// Token: 0x04000A32 RID: 2610
	public licences licences_;

	// Token: 0x04000A33 RID: 2611
	public GameObject[] uiObjects;

	// Token: 0x04000A34 RID: 2612
	public mainScript mS_;

	// Token: 0x04000A35 RID: 2613
	public textScript tS_;

	// Token: 0x04000A36 RID: 2614
	public sfxScript sfx_;

	// Token: 0x04000A37 RID: 2615
	public GUI_Main guiMain_;

	// Token: 0x04000A38 RID: 2616
	public tooltip tooltip_;
}
