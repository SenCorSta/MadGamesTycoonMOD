using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008C RID: 140
public class Item_DevGame_PublisherBeziehung : MonoBehaviour
{
	// Token: 0x06000594 RID: 1428 RVA: 0x0004A768 File Offset: 0x00048968
	public void SetData(string text_, Sprite sprite_, int stars_)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Image>().sprite = sprite_;
		this.guiMain_.DrawStarsColor(this.uiObjects[2], stars_, Color.red);
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040008B8 RID: 2232
	public GameObject[] uiObjects;

	// Token: 0x040008B9 RID: 2233
	public GUI_Main guiMain_;
}
