using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CB RID: 715
public class Fill_Window : MonoBehaviour
{
	// Token: 0x060019B9 RID: 6585 RVA: 0x000115CD File Offset: 0x0000F7CD
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x0010D4A0 File Offset: 0x0010B6A0
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x000115DB File Offset: 0x0000F7DB
	private void Init()
	{
		base.GetComponent<Image>().material = this.guiMain_.matFill_Window;
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x040020E6 RID: 8422
	private mainScript mS_;

	// Token: 0x040020E7 RID: 8423
	private GameObject main_;

	// Token: 0x040020E8 RID: 8424
	private GUI_Main guiMain_;
}
