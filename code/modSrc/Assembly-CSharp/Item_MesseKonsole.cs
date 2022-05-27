using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C4 RID: 196
public class Item_MesseKonsole : MonoBehaviour
{
	// Token: 0x060006CF RID: 1743 RVA: 0x00005D16 File Offset: 0x00003F16
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x0006489C File Offset: 0x00062A9C
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

	// Token: 0x060006D1 RID: 1745 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x000649E0 File Offset: 0x00062BE0
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
