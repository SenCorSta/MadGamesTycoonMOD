using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000278 RID: 632
public class setText : MonoBehaviour
{
	// Token: 0x060018B6 RID: 6326 RVA: 0x00002098 File Offset: 0x00000298
	private void Start()
	{
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x000FD9E0 File Offset: 0x000FBBE0
	private void OnEnable()
	{
		this.FindScripts();
		if (this.textArray.Length > 0 && this.textID > -1)
		{
			this.c = this.tS_.GetText(this.textID);
		}
		base.GetComponent<Text>().text = this.c;
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x000FDA34 File Offset: 0x000FBC34
	private void SetText()
	{
		if (this.textArray.Length > 0 && this.textID > -1)
		{
			this.c = this.tS_.GetText(this.textID);
		}
		base.GetComponent<Text>().text = this.c;
	}

	// Token: 0x060018B9 RID: 6329 RVA: 0x000FDA80 File Offset: 0x000FBC80
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

	// Token: 0x04001C3E RID: 7230
	private GameObject main_;

	// Token: 0x04001C3F RID: 7231
	private textScript tS_;

	// Token: 0x04001C40 RID: 7232
	private settingsScript settings_;

	// Token: 0x04001C41 RID: 7233
	public int textID = -1;

	// Token: 0x04001C42 RID: 7234
	public string textArray = "";

	// Token: 0x04001C43 RID: 7235
	public string c = "";
}
