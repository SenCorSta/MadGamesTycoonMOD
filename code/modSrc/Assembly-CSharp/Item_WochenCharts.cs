using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FD RID: 253
public class Item_WochenCharts : MonoBehaviour
{
	// Token: 0x06000832 RID: 2098 RVA: 0x00006397 File Offset: 0x00004597
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0006B6F8 File Offset: 0x000698F8
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.playerGame)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && !this.game_.playerGame && this.game_.multiplayerSlot != -1 && this.game_.multiplayerSlot != this.mS_.GetMyMultiplayerID())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.sellsPerWeek[0], false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0006B840 File Offset: 0x00069A40
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

	// Token: 0x06000835 RID: 2101 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0006B97C File Offset: 0x00069B7C
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
