using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
public class Item_DevGame_Genre : MonoBehaviour
{
	// Token: 0x06000565 RID: 1381 RVA: 0x00005449 File Offset: 0x00003649
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0005C6B0 File Offset: 0x0005A8B0
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.genres_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.myID);
		this.uiObjects[3].GetComponent<Text>().text = this.genres_.GetStringBeliebtheit(this.myID, true);
		if (this.mS_.trendGenre == this.myID)
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[31];
		}
		if (this.mS_.trendAntiGenre == this.myID)
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[32];
		}
		this.guiMain_.DrawStars(this.uiObjects[2], this.genres_.genres_LEVEL[this.myID]);
		this.uiObjects[5].GetComponent<Image>().sprite = this.genres_.GetSpriteMarkt(this.myID);
		this.tooltip_.c = this.genres_.GetTooltip(this.myID) + "\n";
		tooltip tooltip = this.tooltip_;
		tooltip.c = string.Concat(new string[]
		{
			tooltip.c,
			"\n",
			this.tS_.GetText(1380),
			": <color=blue>",
			this.genres_.GetStringBeliebtheit(this.myID, false),
			"</color>"
		});
		tooltip = this.tooltip_;
		tooltip.c = string.Concat(new string[]
		{
			tooltip.c,
			"\n",
			this.tS_.GetText(1665),
			": <color=blue>",
			this.genres_.GetStringMarktsaettigung(this.myID),
			"</color>"
		});
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0005C8BC File Offset: 0x0005AABC
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.genreArt == 0)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetMainGenre(this.myID);
		}
		else
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetSubGenre(this.myID);
		}
		this.guiMain_.uiObjects[61].GetComponent<Menu_DevGame_Genre>().BUTTON_Close();
	}

	// Token: 0x0400087E RID: 2174
	public int genreArt;

	// Token: 0x0400087F RID: 2175
	public int myID;

	// Token: 0x04000880 RID: 2176
	public GameObject[] uiObjects;

	// Token: 0x04000881 RID: 2177
	public mainScript mS_;

	// Token: 0x04000882 RID: 2178
	public textScript tS_;

	// Token: 0x04000883 RID: 2179
	public sfxScript sfx_;

	// Token: 0x04000884 RID: 2180
	public genres genres_;

	// Token: 0x04000885 RID: 2181
	public GUI_Main guiMain_;

	// Token: 0x04000886 RID: 2182
	public tooltip tooltip_;
}
