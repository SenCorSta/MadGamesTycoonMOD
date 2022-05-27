using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000102 RID: 258
public class Item_Tochterfirma_Theme : MonoBehaviour
{
	// Token: 0x0600084F RID: 2127 RVA: 0x000063EC File Offset: 0x000045EC
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0006C13C File Offset: 0x0006A33C
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetThemes(this.myID);
		if (this.themes_.themes_MGSR[this.myID] != 0)
		{
			this.uiObjects[1].GetComponent<Image>().sprite = this.mS_.games_.gamePEGI[this.themes_.themes_MGSR[this.myID]];
		}
		if (this.pS_.tf_gameTopic == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0006C1E4 File Offset: 0x0006A3E4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_gameTopic = this.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[399].GetComponent<Menu_Stats_TochterfirmaTopic>().BUTTON_Close();
	}

	// Token: 0x04000CA7 RID: 3239
	public int myID;

	// Token: 0x04000CA8 RID: 3240
	public GameObject[] uiObjects;

	// Token: 0x04000CA9 RID: 3241
	public mainScript mS_;

	// Token: 0x04000CAA RID: 3242
	public textScript tS_;

	// Token: 0x04000CAB RID: 3243
	public sfxScript sfx_;

	// Token: 0x04000CAC RID: 3244
	public themes themes_;

	// Token: 0x04000CAD RID: 3245
	public GUI_Main guiMain_;

	// Token: 0x04000CAE RID: 3246
	public tooltip tooltip_;

	// Token: 0x04000CAF RID: 3247
	public publisherScript pS_;
}
