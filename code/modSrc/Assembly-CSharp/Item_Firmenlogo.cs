using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class Item_Firmenlogo : MonoBehaviour
{
	// Token: 0x06000694 RID: 1684 RVA: 0x00005BD3 File Offset: 0x00003DD3
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x000638A0 File Offset: 0x00061AA0
	public void SetData()
	{
		if (this.guiMain_.uiObjects[47].activeSelf && this.mS_.logo == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		if (this.guiMain_.uiObjects[159].activeSelf && this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().logo == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		if (this.guiMain_.uiObjects[201].activeSelf && this.mS_.logo == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.myID);
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x000639EC File Offset: 0x00061BEC
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[47].activeSelf)
		{
			this.guiMain_.uiObjects[47].GetComponent<Menu_Firmenname>().SetLogo(this.myID);
			this.guiMain_.uiObjects[48].SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[159].activeSelf)
		{
			this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().SetLogo(this.myID);
			this.guiMain_.uiObjects[48].SetActive(false);
			return;
		}
		if (this.guiMain_.uiObjects[201].activeSelf)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().SetLogo(this.myID);
			this.guiMain_.uiObjects[48].SetActive(false);
			return;
		}
	}

	// Token: 0x04000A41 RID: 2625
	public int myID = -1;

	// Token: 0x04000A42 RID: 2626
	public GameObject[] uiObjects;

	// Token: 0x04000A43 RID: 2627
	public mainScript mS_;

	// Token: 0x04000A44 RID: 2628
	public textScript tS_;

	// Token: 0x04000A45 RID: 2629
	public sfxScript sfx_;

	// Token: 0x04000A46 RID: 2630
	public GUI_Main guiMain_;
}
