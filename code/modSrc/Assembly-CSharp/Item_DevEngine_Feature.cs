using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007B RID: 123
public class Item_DevEngine_Feature : MonoBehaviour
{
	// Token: 0x0600051B RID: 1307 RVA: 0x0000531C File Offset: 0x0000351C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0005A9F8 File Offset: 0x00058BF8
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.engineFeatures_PICTYP[this.eF_.engineFeatures_TYP[this.myID]];
		this.uiObjects[3].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[this.myID].ToString();
		this.guiMain_.DrawStars(this.uiObjects[4], this.eF_.engineFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.eF_.GetTooltip(this.myID);
		this.SetButtonColor();
		if (!this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().featuresLock[this.myID])
		{
			this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eF_.GetDevCostsForEngine(this.myID), true);
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = "$0";
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0005AB38 File Offset: 0x00058D38
	public void BUTTON_Click()
	{
		Menu_Dev_Engine component = this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>();
		if (!component.featuresLock[this.myID])
		{
			this.activ = !this.activ;
			this.sfx_.PlaySound(3, true);
			component.SetFeature(this.myID, this.activ);
			this.SetButtonColor();
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0005AB9C File Offset: 0x00058D9C
	private void SetButtonColor()
	{
		if (this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().featuresLock[this.myID])
		{
			base.GetComponent<Button>().interactable = false;
			return;
		}
		if (this.activ)
		{
			this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		this.uiObjects[5].GetComponent<Image>().color = Color.white;
	}

	// Token: 0x04000814 RID: 2068
	public int myID;

	// Token: 0x04000815 RID: 2069
	public GameObject[] uiObjects;

	// Token: 0x04000816 RID: 2070
	public mainScript mS_;

	// Token: 0x04000817 RID: 2071
	public textScript tS_;

	// Token: 0x04000818 RID: 2072
	public sfxScript sfx_;

	// Token: 0x04000819 RID: 2073
	public engineFeatures eF_;

	// Token: 0x0400081A RID: 2074
	public GUI_Main guiMain_;

	// Token: 0x0400081B RID: 2075
	public tooltip tooltip_;

	// Token: 0x0400081C RID: 2076
	public bool activ;
}
