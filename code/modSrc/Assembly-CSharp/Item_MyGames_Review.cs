using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EC RID: 236
public class Item_MyGames_Review : MonoBehaviour
{
	// Token: 0x060007CE RID: 1998 RVA: 0x000062B6 File Offset: 0x000044B6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x00069274 File Offset: 0x00067474
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		base.gameObject.name = this.game_.reviewTotal.ToString();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x00069364 File Offset: 0x00067564
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.pubOffer)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[23];
		}
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.guiMain_.uiObjects[387].activeSelf && !this.game_.isOnMarket)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[26];
		}
	}

	// Token: 0x060007D1 RID: 2001 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007D2 RID: 2002 RVA: 0x000694D0 File Offset: 0x000676D0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000BEE RID: 3054
	public GameObject[] uiObjects;

	// Token: 0x04000BEF RID: 3055
	public mainScript mS_;

	// Token: 0x04000BF0 RID: 3056
	public textScript tS_;

	// Token: 0x04000BF1 RID: 3057
	public sfxScript sfx_;

	// Token: 0x04000BF2 RID: 3058
	public GUI_Main guiMain_;

	// Token: 0x04000BF3 RID: 3059
	public tooltip tooltip_;

	// Token: 0x04000BF4 RID: 3060
	public gameScript game_;

	// Token: 0x04000BF5 RID: 3061
	public genres genres_;
}
