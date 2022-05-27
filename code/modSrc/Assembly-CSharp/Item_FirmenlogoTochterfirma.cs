using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BB RID: 187
public class Item_FirmenlogoTochterfirma : MonoBehaviour
{
	// Token: 0x06000699 RID: 1689 RVA: 0x00005BEA File Offset: 0x00003DEA
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00063AEC File Offset: 0x00061CEC
	public void SetData()
	{
		if (this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().pS_.logoID == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.myID);
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x00063B74 File Offset: 0x00061D74
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().SetLogo(this.myID);
		this.guiMain_.uiObjects[392].SetActive(false);
	}

	// Token: 0x04000A47 RID: 2631
	public int myID = -1;

	// Token: 0x04000A48 RID: 2632
	public GameObject[] uiObjects;

	// Token: 0x04000A49 RID: 2633
	public mainScript mS_;

	// Token: 0x04000A4A RID: 2634
	public textScript tS_;

	// Token: 0x04000A4B RID: 2635
	public sfxScript sfx_;

	// Token: 0x04000A4C RID: 2636
	public GUI_Main guiMain_;
}
