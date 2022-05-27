using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C3 RID: 195
public class Item_MesseGame : MonoBehaviour
{
	// Token: 0x060006D3 RID: 1747 RVA: 0x0005201A File Offset: 0x0005021A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00052024 File Offset: 0x00050224
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.isOnMarket)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(528);
		}
		this.uiObjects[2].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.game_.GetHype()).ToString();
		Menu_MesseSelect component = this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>();
		if (component.games[0] == this.game_ || component.games[1] == this.game_ || component.games[2] == this.game_)
		{
			base.GetComponent<Button>().interactable = false;
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00052194 File Offset: 0x00050394
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetGame(this.slot, this.game_);
		this.guiMain_.uiObjects[187].SetActive(false);
	}

	// Token: 0x04000A8A RID: 2698
	public GameObject[] uiObjects;

	// Token: 0x04000A8B RID: 2699
	public mainScript mS_;

	// Token: 0x04000A8C RID: 2700
	public textScript tS_;

	// Token: 0x04000A8D RID: 2701
	public sfxScript sfx_;

	// Token: 0x04000A8E RID: 2702
	public GUI_Main guiMain_;

	// Token: 0x04000A8F RID: 2703
	public tooltip tooltip_;

	// Token: 0x04000A90 RID: 2704
	public gameScript game_;

	// Token: 0x04000A91 RID: 2705
	public genres genres_;

	// Token: 0x04000A92 RID: 2706
	public int slot;
}
