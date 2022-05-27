using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C4 RID: 196
public class Item_MesseKonsole : MonoBehaviour
{
	// Token: 0x060006D8 RID: 1752 RVA: 0x000521EC File Offset: 0x000503EC
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x000521F4 File Offset: 0x000503F4
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		if (this.pS_.isUnlocked)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(528);
		}
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.pS_.GetHype()).ToString();
		Menu_MesseSelect component = this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>();
		if (component.konsolen[0] == this.pS_ || component.konsolen[1] == this.pS_)
		{
			base.GetComponent<Button>().interactable = false;
		}
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00052338 File Offset: 0x00050538
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[186].GetComponent<Menu_MesseSelect>().SetKonsole(this.slot, this.pS_);
		this.guiMain_.uiObjects[323].SetActive(false);
	}

	// Token: 0x04000A93 RID: 2707
	public GameObject[] uiObjects;

	// Token: 0x04000A94 RID: 2708
	public mainScript mS_;

	// Token: 0x04000A95 RID: 2709
	public textScript tS_;

	// Token: 0x04000A96 RID: 2710
	public sfxScript sfx_;

	// Token: 0x04000A97 RID: 2711
	public GUI_Main guiMain_;

	// Token: 0x04000A98 RID: 2712
	public tooltip tooltip_;

	// Token: 0x04000A99 RID: 2713
	public platformScript pS_;

	// Token: 0x04000A9A RID: 2714
	public int slot;
}
