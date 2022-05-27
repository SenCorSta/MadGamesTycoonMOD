using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F9 RID: 249
public class Item_Stats_Marktanalyse : MonoBehaviour
{
	// Token: 0x0600081A RID: 2074 RVA: 0x0006AD14 File Offset: 0x00068F14
	public void Init(string text_, string amountGames, Sprite pic, Sprite marktanalyse, int anzGames_, int typ_)
	{
		this.myName = text_;
		this.anzGames = anzGames_;
		this.typ = typ_;
		this.uiObjects[0].GetComponent<Text>().text = text_;
		this.uiObjects[1].GetComponent<Text>().text = amountGames;
		this.uiObjects[2].GetComponent<Image>().sprite = pic;
		this.uiObjects[3].GetComponent<Image>().sprite = marktanalyse;
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000C55 RID: 3157
	public GameObject[] uiObjects;

	// Token: 0x04000C56 RID: 3158
	public string myName;

	// Token: 0x04000C57 RID: 3159
	public int anzGames;

	// Token: 0x04000C58 RID: 3160
	public int typ;
}
