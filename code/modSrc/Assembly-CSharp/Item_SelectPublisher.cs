using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000099 RID: 153
public class Item_SelectPublisher : MonoBehaviour
{
	// Token: 0x060005E5 RID: 1509 RVA: 0x0004C456 File Offset: 0x0004A656
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0004C460 File Offset: 0x0004A660
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.pS_.GetLogo();
		this.uiObjects[2].GetComponent<Image>().sprite = this.genres_.GetPic(this.pS_.fanGenre);
		this.uiObjects[5].GetComponent<Text>().text = "$" + this.pS_.GetShare().ToString();
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(this.pS_.stars / 20f));
		this.guiMain_.DrawStarsColor(this.uiObjects[4], Mathf.RoundToInt(this.pS_.GetRelation() / 20f), this.guiMain_.colors[5]);
		this.tooltip_.c = this.pS_.GetTooltip();
		if (this.pS_.IsMyTochterfirma())
		{
			if (!this.uiObjects[8].activeSelf)
			{
				this.uiObjects[8].SetActive(true);
			}
		}
		else if (this.uiObjects[8].activeSelf)
		{
			this.uiObjects[8].SetActive(false);
		}
		if (this.pS_.isPlayer)
		{
			if (!this.uiObjects[7].activeSelf)
			{
				this.uiObjects[7].SetActive(true);
			}
		}
		else if (this.uiObjects[7].activeSelf)
		{
			this.uiObjects[7].SetActive(false);
		}
		if (!this.pS_.isPlayer && !this.pS_.IsMyTochterfirma())
		{
			if (!this.uiObjects[6].activeSelf)
			{
				this.uiObjects[6].SetActive(true);
				return;
			}
		}
		else if (this.uiObjects[6].activeSelf)
		{
			this.uiObjects[6].SetActive(false);
		}
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0004C65E File Offset: 0x0004A85E
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().SelectPublisher(this.pS_.myID);
	}

	// Token: 0x04000925 RID: 2341
	public GameObject[] uiObjects;

	// Token: 0x04000926 RID: 2342
	public mainScript mS_;

	// Token: 0x04000927 RID: 2343
	public textScript tS_;

	// Token: 0x04000928 RID: 2344
	public sfxScript sfx_;

	// Token: 0x04000929 RID: 2345
	public genres genres_;

	// Token: 0x0400092A RID: 2346
	public GUI_Main guiMain_;

	// Token: 0x0400092B RID: 2347
	public tooltip tooltip_;

	// Token: 0x0400092C RID: 2348
	public publisherScript pS_;
}
