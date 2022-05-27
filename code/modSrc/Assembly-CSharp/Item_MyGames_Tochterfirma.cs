using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EF RID: 239
public class Item_MyGames_Tochterfirma : MonoBehaviour
{
	// Token: 0x060007EA RID: 2026 RVA: 0x000578AA File Offset: 0x00055AAA
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000578B4 File Offset: 0x00055AB4
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

	// Token: 0x060007EC RID: 2028 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000579D8 File Offset: 0x00055BD8
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
