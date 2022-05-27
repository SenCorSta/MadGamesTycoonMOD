using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007B RID: 123
public class Item_DevEngine_Feature : MonoBehaviour
{
	// Token: 0x06000524 RID: 1316 RVA: 0x00047799 File Offset: 0x00045999
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x000477A4 File Offset: 0x000459A4
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

	// Token: 0x06000526 RID: 1318 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x000478E4 File Offset: 0x00045AE4
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

	// Token: 0x06000528 RID: 1320 RVA: 0x00047948 File Offset: 0x00045B48
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
