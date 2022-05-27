using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007C RID: 124
public class Item_DevEngine_Genre : MonoBehaviour
{
	// Token: 0x0600052A RID: 1322 RVA: 0x000479C6 File Offset: 0x00045BC6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x000479D0 File Offset: 0x00045BD0
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		this.tooltip_.c = this.genres_.GetTooltip(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[2], this.genres_.genres_LEVEL[this.myID]);
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00047A64 File Offset: 0x00045C64
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
