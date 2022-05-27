using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F2 RID: 242
public class Item_MyKonsolen_AllTimeCharts : MonoBehaviour
{
	// Token: 0x060007FB RID: 2043 RVA: 0x00057E41 File Offset: 0x00056041
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00057E4C File Offset: 0x0005604C
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.units, false);
		base.gameObject.name = this.pS_.units.ToString();
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00057ECC File Offset: 0x000560CC
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		if (this.pS_.ownerID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		else
		{
			base.GetComponent<Button>().interactable = false;
		}
		if (this.mS_.multiplayer && this.pS_.PlatformFromMitspieler())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.units, false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00058008 File Offset: 0x00056208
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[333]);
		this.guiMain_.uiObjects[333].GetComponent<Menu_Umsatz_Konsole>().Init(this.pS_);
	}

	// Token: 0x04000C20 RID: 3104
	public GameObject[] uiObjects;

	// Token: 0x04000C21 RID: 3105
	public mainScript mS_;

	// Token: 0x04000C22 RID: 3106
	public textScript tS_;

	// Token: 0x04000C23 RID: 3107
	public sfxScript sfx_;

	// Token: 0x04000C24 RID: 3108
	public GUI_Main guiMain_;

	// Token: 0x04000C25 RID: 3109
	public tooltip tooltip_;

	// Token: 0x04000C26 RID: 3110
	public platformScript pS_;
}
