using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000277 RID: 631
public class setFont : MonoBehaviour
{
	// Token: 0x060018B3 RID: 6323 RVA: 0x00010ED7 File Offset: 0x0000F0D7
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

	// Token: 0x060018B4 RID: 6324 RVA: 0x00010F14 File Offset: 0x0000F114
	private void OnEnable()
	{
		this.FindScripts();
		if (this.settings_.language == 3 || this.settings_.language == 10)
		{
			base.GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
	}

	// Token: 0x04001C3C RID: 7228
	private GameObject main_;

	// Token: 0x04001C3D RID: 7229
	private settingsScript settings_;
}
