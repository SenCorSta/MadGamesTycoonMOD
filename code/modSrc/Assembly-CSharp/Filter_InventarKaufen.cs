using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009A RID: 154
public class Filter_InventarKaufen : MonoBehaviour
{
	// Token: 0x060005E1 RID: 1505 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x00002098 File Offset: 0x00000298
	private void Update()
	{
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0005F488 File Offset: 0x0005D688
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.show = !this.show;
		this.guiMain_.uiObjects[20].GetComponent<Menu_BuyInventar>().filter[this.filterArrayID] = !this.show;
		if (this.show)
		{
			this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.uiSprites[60];
			base.GetComponent<Image>().color = this.colors[0];
		}
		else
		{
			this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.uiSprites[59];
			base.GetComponent<Image>().color = this.colors[1];
		}
		for (int i = base.transform.GetSiblingIndex() + 1; i < base.transform.parent.childCount; i++)
		{
			Transform child = base.transform.parent.GetChild(i);
			if (child)
			{
				if (!child.GetComponent<Item_InventarKaufen>())
				{
					return;
				}
				child.gameObject.SetActive(!child.gameObject.activeSelf);
			}
		}
	}

	// Token: 0x0400092D RID: 2349
	public GameObject[] uiObjects;

	// Token: 0x0400092E RID: 2350
	public int filterArrayID;

	// Token: 0x0400092F RID: 2351
	public Color[] colors;

	// Token: 0x04000930 RID: 2352
	public mainScript mS_;

	// Token: 0x04000931 RID: 2353
	public textScript tS_;

	// Token: 0x04000932 RID: 2354
	public mapScript mapS_;

	// Token: 0x04000933 RID: 2355
	public GUI_Main guiMain_;

	// Token: 0x04000934 RID: 2356
	public sfxScript sfx_;

	// Token: 0x04000935 RID: 2357
	public bool show = true;
}
