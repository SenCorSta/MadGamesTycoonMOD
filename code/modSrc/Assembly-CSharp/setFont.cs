using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027B RID: 635
public class setFont : MonoBehaviour
{
	// Token: 0x060018F8 RID: 6392 RVA: 0x000F8BAF File Offset: 0x000F6DAF
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
	}

	// Token: 0x060018F9 RID: 6393 RVA: 0x000F8BEC File Offset: 0x000F6DEC
	private void OnEnable()
	{
		this.FindScripts();
		if (this.settings_.language == 3 || this.settings_.language == 10)
		{
			base.GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
	}

	// Token: 0x04001C57 RID: 7255
	private GameObject main_;

	// Token: 0x04001C58 RID: 7256
	private settingsScript settings_;
}
