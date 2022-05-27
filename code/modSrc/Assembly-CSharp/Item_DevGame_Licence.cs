using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000087 RID: 135
public class Item_DevGame_Licence : MonoBehaviour
{
	// Token: 0x0600056A RID: 1386 RVA: 0x00005451 File Offset: 0x00003651
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0005C934 File Offset: 0x0005AB34
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.licences_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.licences_.GetSellPrice(this.myID), true);
		this.uiObjects[5].GetComponent<Image>().sprite = this.licences_.licenceSprites[this.licences_.licence_TYP[this.myID]];
		this.guiMain_.DrawStars(this.uiObjects[2], Mathf.RoundToInt(this.licences_.licence_QUALITY[this.myID] / 20f));
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.licences_.licence_GEKAUFT[this.myID].ToString());
		this.uiObjects[3].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.licences_.GetTypString(this.myID);
		this.tooltip_.c = this.licences_.GetTooltip(this.myID);
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0005CA80 File Offset: 0x0005AC80
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetLicence(this.myID);
		this.guiMain_.uiObjects[63].GetComponent<Menu_GameDev_Licence>().BUTTON_Close();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[64]);
	}

	// Token: 0x04000887 RID: 2183
	public int myID = -1;

	// Token: 0x04000888 RID: 2184
	public licences licences_;

	// Token: 0x04000889 RID: 2185
	public GameObject[] uiObjects;

	// Token: 0x0400088A RID: 2186
	public mainScript mS_;

	// Token: 0x0400088B RID: 2187
	public textScript tS_;

	// Token: 0x0400088C RID: 2188
	public sfxScript sfx_;

	// Token: 0x0400088D RID: 2189
	public GUI_Main guiMain_;

	// Token: 0x0400088E RID: 2190
	public tooltip tooltip_;
}
