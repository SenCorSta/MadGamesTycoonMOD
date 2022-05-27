using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AE RID: 174
public class Item_MM_SpezialGenre : MonoBehaviour
{
	// Token: 0x06000662 RID: 1634 RVA: 0x000501ED File Offset: 0x0004E3ED
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x000501F8 File Offset: 0x0004E3F8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		this.tooltip_.c = this.genres_.GetTooltip(this.myID);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00050268 File Offset: 0x0004E468
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().SetGenre(this.myID);
		this.guiMain_.uiObjects[298].SetActive(false);
	}

	// Token: 0x040009ED RID: 2541
	public int myID;

	// Token: 0x040009EE RID: 2542
	public GameObject[] uiObjects;

	// Token: 0x040009EF RID: 2543
	public mainScript mS_;

	// Token: 0x040009F0 RID: 2544
	public textScript tS_;

	// Token: 0x040009F1 RID: 2545
	public sfxScript sfx_;

	// Token: 0x040009F2 RID: 2546
	public genres genres_;

	// Token: 0x040009F3 RID: 2547
	public GUI_Main guiMain_;

	// Token: 0x040009F4 RID: 2548
	public tooltip tooltip_;
}
