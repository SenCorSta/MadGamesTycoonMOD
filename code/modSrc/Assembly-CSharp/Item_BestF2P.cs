using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E2 RID: 226
public class Item_BestF2P : MonoBehaviour
{
	// Token: 0x0600079E RID: 1950 RVA: 0x00055D4D File Offset: 0x00053F4D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00055D58 File Offset: 0x00053F58
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.ownerID == this.mS_.myID || this.game_.publisherID == this.mS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		if (this.mS_.multiplayer && this.game_.GameFromMitspieler())
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[8];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00055EA8 File Offset: 0x000540A8
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00055F34 File Offset: 0x00054134
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000B99 RID: 2969
	public GameObject[] uiObjects;

	// Token: 0x04000B9A RID: 2970
	public mainScript mS_;

	// Token: 0x04000B9B RID: 2971
	public textScript tS_;

	// Token: 0x04000B9C RID: 2972
	public sfxScript sfx_;

	// Token: 0x04000B9D RID: 2973
	public GUI_Main guiMain_;

	// Token: 0x04000B9E RID: 2974
	public tooltip tooltip_;

	// Token: 0x04000B9F RID: 2975
	public gameScript game_;

	// Token: 0x04000BA0 RID: 2976
	public genres genres_;
}
