using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F8 RID: 248
public class Item_Stats_GenreBeliebtheit : MonoBehaviour
{
	// Token: 0x06000817 RID: 2071 RVA: 0x0006ABD0 File Offset: 0x00068DD0
	public void Init(string text_, float prozent, Sprite pic)
	{
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(prozent) + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = pic;
		this.uiObjects[3].GetComponent<Image>().fillAmount = prozent * 0.01f;
		if (prozent >= 100f)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1381);
		}
		if (prozent <= 20f)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1382);
		}
		if (prozent <= 50f)
		{
			this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colorsBalken[0];
			return;
		}
		if (prozent < 70f)
		{
			this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colorsBalken[1];
			return;
		}
		this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colorsBalken[2];
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000C52 RID: 3154
	public GameObject[] uiObjects;

	// Token: 0x04000C53 RID: 3155
	public GUI_Main guiMain_;

	// Token: 0x04000C54 RID: 3156
	public textScript tS_;
}
