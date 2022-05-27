using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008C RID: 140
public class Item_DevGame_PublisherBeziehung : MonoBehaviour
{
	// Token: 0x0600058B RID: 1419 RVA: 0x0005D7C0 File Offset: 0x0005B9C0
	public void SetData(string text_, Sprite sprite_, int stars_)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Image>().sprite = sprite_;
		this.guiMain_.DrawStarsColor(this.uiObjects[2], stars_, Color.red);
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040008B8 RID: 2232
	public GameObject[] uiObjects;

	// Token: 0x040008B9 RID: 2233
	public GUI_Main guiMain_;
}
