using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000103 RID: 259
public class Item_Tochterfirma_Theme : MonoBehaviour
{
	// Token: 0x0600085E RID: 2142 RVA: 0x0005A5C8 File Offset: 0x000587C8
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0005A5D0 File Offset: 0x000587D0
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

	// Token: 0x06000860 RID: 2144 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0005A678 File Offset: 0x00058878
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_gameTopic = this.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[399].GetComponent<Menu_Stats_TochterfirmaTopic>().BUTTON_Close();
	}

	// Token: 0x04000CAF RID: 3247
	public int myID;

	// Token: 0x04000CB0 RID: 3248
	public GameObject[] uiObjects;

	// Token: 0x04000CB1 RID: 3249
	public mainScript mS_;

	// Token: 0x04000CB2 RID: 3250
	public textScript tS_;

	// Token: 0x04000CB3 RID: 3251
	public sfxScript sfx_;

	// Token: 0x04000CB4 RID: 3252
	public themes themes_;

	// Token: 0x04000CB5 RID: 3253
	public GUI_Main guiMain_;

	// Token: 0x04000CB6 RID: 3254
	public tooltip tooltip_;

	// Token: 0x04000CB7 RID: 3255
	public publisherScript pS_;
}
