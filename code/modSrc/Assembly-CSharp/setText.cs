using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027C RID: 636
public class setText : MonoBehaviour
{
	// Token: 0x060018FB RID: 6395 RVA: 0x00002715 File Offset: 0x00000915
	private void Start()
	{
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x000F8BF4 File Offset: 0x000F6DF4
	private void OnEnable()
	{
		this.FindScripts();
		if (this.textArray.Length > 0 && this.textID > -1)
		{
			this.c = this.tS_.GetText(this.textID);
		}
		base.GetComponent<Text>().text = this.c;
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x000F8C48 File Offset: 0x000F6E48
	private void SetText()
	{
		if (this.textArray.Length > 0 && this.textID > -1)
		{
			this.c = this.tS_.GetText(this.textID);
		}
		base.GetComponent<Text>().text = this.c;
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x000F8C94 File Offset: 0x000F6E94
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
	}

	// Token: 0x04001C59 RID: 7257
	private GameObject main_;

	// Token: 0x04001C5A RID: 7258
	private textScript tS_;

	// Token: 0x04001C5B RID: 7259
	private settingsScript settings_;

	// Token: 0x04001C5C RID: 7260
	public int textID = -1;

	// Token: 0x04001C5D RID: 7261
	public string textArray = "";

	// Token: 0x04001C5E RID: 7262
	public string c = "";
}
