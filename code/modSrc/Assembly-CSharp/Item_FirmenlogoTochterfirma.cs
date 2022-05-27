using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BB RID: 187
public class Item_FirmenlogoTochterfirma : MonoBehaviour
{
	// Token: 0x060006A2 RID: 1698 RVA: 0x000512E9 File Offset: 0x0004F4E9
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x000512F4 File Offset: 0x0004F4F4
	public void SetData()
	{
		if (this.guiMain_.uiObjects[391].GetComponent<Menu_TochterfirmaRename>().pS_.logoID == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.myID);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0005137C File Offset: 0x0004F57C
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
