using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EF RID: 239
public class Item_MyGames_Tochterfirma : MonoBehaviour
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x000062D6 File Offset: 0x000044D6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00069964 File Offset: 0x00067B64
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(1291),
			": ",
			this.game_.GetUserReviewPercent().ToString(),
			"%  ",
			this.tS_.GetText(1292),
			": ",
			this.game_.reviewTotal.ToString(),
			"%"
		});
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetDeveloperLogo();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00069A88 File Offset: 0x00067C88
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[46].SetActive(true);
		this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
	}

	// Token: 0x04000C07 RID: 3079
	public GameObject[] uiObjects;

	// Token: 0x04000C08 RID: 3080
	public mainScript mS_;

	// Token: 0x04000C09 RID: 3081
	public textScript tS_;

	// Token: 0x04000C0A RID: 3082
	public sfxScript sfx_;

	// Token: 0x04000C0B RID: 3083
	public GUI_Main guiMain_;

	// Token: 0x04000C0C RID: 3084
	public tooltip tooltip_;

	// Token: 0x04000C0D RID: 3085
	public gameScript game_;

	// Token: 0x04000C0E RID: 3086
	public genres genres_;
}
