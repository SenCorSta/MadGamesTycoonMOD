﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
public class Item_DevGame_Genre : MonoBehaviour
{
	// Token: 0x0600056E RID: 1390 RVA: 0x0004958C File Offset: 0x0004778C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00049594 File Offset: 0x00047794
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

	// Token: 0x06000570 RID: 1392 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x000497A0 File Offset: 0x000479A0
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
