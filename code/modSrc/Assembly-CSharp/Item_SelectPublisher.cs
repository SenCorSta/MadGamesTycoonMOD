using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000099 RID: 153
public class Item_SelectPublisher : MonoBehaviour
{
	// Token: 0x060005DC RID: 1500 RVA: 0x00005672 File Offset: 0x00003872
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0005F348 File Offset: 0x0005D548
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[2].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[5].GetComponent<Text>().text = "$" + this.pS_.GetShare().ToString();
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		this.guiMain_.DrawStarsColor(this.uiObjects[4], Mathf.RoundToInt(this.pS_.GetRelation() / 20f), this.guiMain_.colors[5]);
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.IsMyTochterfirma())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x0000567A File Offset: 0x0000387A
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().SelectPublisher(this.pS_.myID);
	}

	// Token: 0x04000925 RID: 2341
	public GameObject[] uiObjects;

	// Token: 0x04000926 RID: 2342
	public mainScript mS_;

	// Token: 0x04000927 RID: 2343
	public textScript tS_;

	// Token: 0x04000928 RID: 2344
	public sfxScript sfx_;

	// Token: 0x04000929 RID: 2345
	public genres genres_;

	// Token: 0x0400092A RID: 2346
	public GUI_Main guiMain_;

	// Token: 0x0400092B RID: 2347
	public tooltip tooltip_;

	// Token: 0x0400092C RID: 2348
	public publisherScript pS_;
}
