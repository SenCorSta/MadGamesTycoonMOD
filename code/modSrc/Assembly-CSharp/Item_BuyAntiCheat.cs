using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B4 RID: 180
public class Item_BuyAntiCheat : MonoBehaviour
{
	// Token: 0x06000676 RID: 1654 RVA: 0x00005B32 File Offset: 0x00003D32
	private void Start()
	{
		if (this.acS_.inBesitz)
		{
			base.GetComponent<Button>().interactable = false;
		}
		this.SetData();
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00063098 File Offset: 0x00061298
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.acS_.GetPrice(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.acS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.acS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.acS_.effekt);
		this.tooltip_.c = this.acS_.GetTooltip();
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x00063180 File Offset: 0x00061380
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

	// Token: 0x06000679 RID: 1657 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x000631F4 File Offset: 0x000613F4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[235]);
		this.guiMain_.uiObjects[235].GetComponent<Menu_W_BuyAntiCheat>().Init(this.acS_);
	}

	// Token: 0x04000A19 RID: 2585
	public antiCheatScript acS_;

	// Token: 0x04000A1A RID: 2586
	public GameObject[] uiObjects;

	// Token: 0x04000A1B RID: 2587
	public mainScript mS_;

	// Token: 0x04000A1C RID: 2588
	public textScript tS_;

	// Token: 0x04000A1D RID: 2589
	public sfxScript sfx_;

	// Token: 0x04000A1E RID: 2590
	public GUI_Main guiMain_;

	// Token: 0x04000A1F RID: 2591
	public tooltip tooltip_;
}
