using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class Item_Firmenlogo : MonoBehaviour
{
	// Token: 0x0600069D RID: 1693 RVA: 0x00051085 File Offset: 0x0004F285
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00051090 File Offset: 0x0004F290
	public void SetData()
	{
		if (this.guiMain_.uiObjects[47].activeSelf && this.mS_.GetCompanyLogoID() == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		if (this.guiMain_.uiObjects[159].activeSelf && this.guiMain_.uiObjects[159].GetComponent<Menu_NewGame>().logo == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		if (this.guiMain_.uiObjects[201].activeSelf && this.mS_.GetCompanyLogoID() == this.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			this.uiObjects[0].GetComponent<Animation>().Play();
		}
		this.uiObjects[0].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.myID);
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x000511DC File Offset: 0x0004F3DC
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
