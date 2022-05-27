using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000091 RID: 145
public class Item_DevGame_XP : MonoBehaviour
{
	// Token: 0x060005B2 RID: 1458 RVA: 0x0004B22A File Offset: 0x0004942A
	public void SetData(string text_, Sprite sprite_, int stars_)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Image>().sprite = sprite_;
		this.guiMain_.DrawStars(this.uiObjects[2], stars_);
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040008E3 RID: 2275
	public GameObject[] uiObjects;

	// Token: 0x040008E4 RID: 2276
	public GUI_Main guiMain_;
}
