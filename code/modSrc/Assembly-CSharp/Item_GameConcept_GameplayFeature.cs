using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000096 RID: 150
public class Item_GameConcept_GameplayFeature : MonoBehaviour
{
	// Token: 0x060005D3 RID: 1491 RVA: 0x0004BB1C File Offset: 0x00049D1C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0004BB24 File Offset: 0x00049D24
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.gS_.maingenre);
		this.SetGoodBadIcon();
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0004BBFC File Offset: 0x00049DFC
	private void SetGoodBadIcon()
	{
		if (this.gS_.maingenre != -1)
		{
			if (this.gF_.gameplayFeatures_GOOD[this.myID, this.gS_.maingenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
				return;
			}
			if (this.gF_.gameplayFeatures_BAD[this.myID, this.gS_.maingenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
				return;
			}
		}
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
	}

	// Token: 0x04000908 RID: 2312
	public int myID;

	// Token: 0x04000909 RID: 2313
	public GameObject[] uiObjects;

	// Token: 0x0400090A RID: 2314
	public mainScript mS_;

	// Token: 0x0400090B RID: 2315
	public textScript tS_;

	// Token: 0x0400090C RID: 2316
	public sfxScript sfx_;

	// Token: 0x0400090D RID: 2317
	public gameplayFeatures gF_;

	// Token: 0x0400090E RID: 2318
	public GUI_Main guiMain_;

	// Token: 0x0400090F RID: 2319
	public tooltip tooltip_;

	// Token: 0x04000910 RID: 2320
	public gameScript gS_;
}
