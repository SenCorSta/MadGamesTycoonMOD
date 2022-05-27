using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000090 RID: 144
public class Item_DevGame_Theme : MonoBehaviour
{
	// Token: 0x060005AD RID: 1453 RVA: 0x0004AFEF File Offset: 0x000491EF
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0004AFF8 File Offset: 0x000491F8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetThemes(this.myID);
		if (this.themes_.themes_MGSR[this.myID] != 0)
		{
			this.uiObjects[1].GetComponent<Image>().sprite = this.mS_.games_.gamePEGI[this.themes_.themes_MGSR[this.myID]];
		}
		this.uiObjects[3].GetComponent<Image>().sprite = this.themes_.GetSpriteMarkt(this.myID);
		switch (this.fitGenre)
		{
		case -1:
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
			break;
		case 1:
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
			break;
		}
		this.guiMain_.DrawStars(this.uiObjects[2], this.themes_.themes_LEVEL[this.myID]);
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0004B110 File Offset: 0x00049310
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.themeArt == 0)
		{
			if (this.guiMain_.uiObjects[56].activeSelf)
			{
				this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMainTheme(this.myID);
			}
		}
		else
		{
			if (this.guiMain_.uiObjects[56].activeSelf)
			{
				this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetSubTheme(this.myID);
			}
			if (this.guiMain_.uiObjects[193].activeSelf)
			{
				this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetSubTheme(this.myID);
			}
			if (this.guiMain_.uiObjects[247].activeSelf)
			{
				this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetSubTheme(this.myID);
			}
		}
		this.guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>().BUTTON_Close();
	}

	// Token: 0x040008D8 RID: 2264
	public int themeArt;

	// Token: 0x040008D9 RID: 2265
	public int myID;

	// Token: 0x040008DA RID: 2266
	public int fitGenre;

	// Token: 0x040008DB RID: 2267
	public GameObject[] uiObjects;

	// Token: 0x040008DC RID: 2268
	public mainScript mS_;

	// Token: 0x040008DD RID: 2269
	public textScript tS_;

	// Token: 0x040008DE RID: 2270
	public sfxScript sfx_;

	// Token: 0x040008DF RID: 2271
	public themes themes_;

	// Token: 0x040008E0 RID: 2272
	public GUI_Main guiMain_;

	// Token: 0x040008E1 RID: 2273
	public tooltip tooltip_;

	// Token: 0x040008E2 RID: 2274
	public bool debug;
}
