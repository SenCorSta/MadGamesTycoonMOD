using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000091 RID: 145
public class Item_DevGame_XP : MonoBehaviour
{
	// Token: 0x060005A9 RID: 1449 RVA: 0x00005599 File Offset: 0x00003799
	public void SetData(string text_, Sprite sprite_, int stars_)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Image>().sprite = sprite_;
		this.guiMain_.DrawStars(this.uiObjects[2], stars_);
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040008E3 RID: 2275
	public GameObject[] uiObjects;

	// Token: 0x040008E4 RID: 2276
	public GUI_Main guiMain_;
}
