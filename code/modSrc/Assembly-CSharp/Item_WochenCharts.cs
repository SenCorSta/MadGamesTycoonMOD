using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FD RID: 253
public class Item_WochenCharts : MonoBehaviour
{
	// Token: 0x0600083B RID: 2107 RVA: 0x0005980E File Offset: 0x00057A0E
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00059818 File Offset: 0x00057A18
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && this.game_.GameFromMitspieler())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.sellsPerWeek[0], false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00059960 File Offset: 0x00057B60
	private void Update()
	{
		int siblingIndex = base.gameObject.transform.GetSiblingIndex();
		this.uiObjects[1].GetComponent<Text>().text = (siblingIndex + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.sellsPerWeek[0], false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
		if (this.game_.lastChartPosition < siblingIndex)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=red>▼</color>";
		}
		if (this.game_.lastChartPosition > siblingIndex)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=green>▲</color>";
		}
		if (this.game_.lastChartPosition == siblingIndex)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=black>●</color>";
		}
		if (this.game_.lastChartPosition == -1)
		{
			this.uiObjects[4].GetComponent<Text>().text = "<color=blue>◆</color>";
		}
		if (!this.mS_.multiplayer)
		{
			return;
		}
		if (!this.game_.isOnMarket)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00059A9C File Offset: 0x00057C9C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000C78 RID: 3192
	public GameObject[] uiObjects;

	// Token: 0x04000C79 RID: 3193
	public mainScript mS_;

	// Token: 0x04000C7A RID: 3194
	public textScript tS_;

	// Token: 0x04000C7B RID: 3195
	public sfxScript sfx_;

	// Token: 0x04000C7C RID: 3196
	public GUI_Main guiMain_;

	// Token: 0x04000C7D RID: 3197
	public tooltip tooltip_;

	// Token: 0x04000C7E RID: 3198
	public gameScript game_;

	// Token: 0x04000C7F RID: 3199
	public genres genres_;
}
