using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BE RID: 190
public class Item_PublisherExklusiv : MonoBehaviour
{
	// Token: 0x060006B4 RID: 1716 RVA: 0x0005175A File Offset: 0x0004F95A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00051764 File Offset: 0x0004F964
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetShareExklusiv(), true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.GetMoneyExklusiv(), true);
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		string text = this.tS_.GetText(1048);
		text = text.Replace("<NUM>", this.pS_.exklusivLaufzeit.ToString());
		this.uiObjects[7].GetComponent<Text>().text = text;
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0005187E File Offset: 0x0004FA7E
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[200].GetComponent<Menu_PublisherExklusiv>().SelectPublisher(this.pS_);
	}

	// Token: 0x04000A60 RID: 2656
	public GameObject[] uiObjects;

	// Token: 0x04000A61 RID: 2657
	public mainScript mS_;

	// Token: 0x04000A62 RID: 2658
	public textScript tS_;

	// Token: 0x04000A63 RID: 2659
	public sfxScript sfx_;

	// Token: 0x04000A64 RID: 2660
	public genres genres_;

	// Token: 0x04000A65 RID: 2661
	public GUI_Main guiMain_;

	// Token: 0x04000A66 RID: 2662
	public tooltip tooltip_;

	// Token: 0x04000A67 RID: 2663
	public publisherScript pS_;
}
