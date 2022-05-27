using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B5 RID: 181
public class Item_BuyCopyProtect : MonoBehaviour
{
	// Token: 0x06000685 RID: 1669 RVA: 0x0005096B File Offset: 0x0004EB6B
	private void Start()
	{
		if (this.cpS_.inBesitz)
		{
			base.GetComponent<Button>().interactable = false;
		}
		this.SetData();
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0005098C File Offset: 0x0004EB8C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cpS_.GetPrice(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.cpS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.cpS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.cpS_.effekt);
		this.tooltip_.c = this.cpS_.GetTooltip();
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00050A74 File Offset: 0x0004EC74
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x00050AE8 File Offset: 0x0004ECE8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[50]);
		this.guiMain_.uiObjects[50].GetComponent<Menu_W_BuyCopyProtect>().Init(this.cpS_);
	}

	// Token: 0x04000A20 RID: 2592
	public copyProtectScript cpS_;

	// Token: 0x04000A21 RID: 2593
	public GameObject[] uiObjects;

	// Token: 0x04000A22 RID: 2594
	public mainScript mS_;

	// Token: 0x04000A23 RID: 2595
	public textScript tS_;

	// Token: 0x04000A24 RID: 2596
	public sfxScript sfx_;

	// Token: 0x04000A25 RID: 2597
	public GUI_Main guiMain_;

	// Token: 0x04000A26 RID: 2598
	public tooltip tooltip_;
}
