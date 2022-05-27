using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007C RID: 124
public class Item_DevEngine_Genre : MonoBehaviour
{
	// Token: 0x06000521 RID: 1313 RVA: 0x00005324 File Offset: 0x00003524
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0005AC1C File Offset: 0x00058E1C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		this.tooltip_.c = this.genres_.GetTooltip(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[2], this.genres_.genres_LEVEL[this.myID]);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0005ACB0 File Offset: 0x00058EB0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().SetSpezialgenre(this.myID);
		this.guiMain_.uiObjects[38].GetComponent<Menu_Dev_Engine_Genre>().BUTTON_Close();
	}

	// Token: 0x0400081D RID: 2077
	public int myID;

	// Token: 0x0400081E RID: 2078
	public GameObject[] uiObjects;

	// Token: 0x0400081F RID: 2079
	public mainScript mS_;

	// Token: 0x04000820 RID: 2080
	public textScript tS_;

	// Token: 0x04000821 RID: 2081
	public sfxScript sfx_;

	// Token: 0x04000822 RID: 2082
	public genres genres_;

	// Token: 0x04000823 RID: 2083
	public GUI_Main guiMain_;

	// Token: 0x04000824 RID: 2084
	public tooltip tooltip_;
}
