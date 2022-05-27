using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CE RID: 718
public class Fill_Window : MonoBehaviour
{
	// Token: 0x06001A03 RID: 6659 RVA: 0x001090C9 File Offset: 0x001072C9
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x001090D8 File Offset: 0x001072D8
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

	// Token: 0x06001A05 RID: 6661 RVA: 0x00109142 File Offset: 0x00107342
	private void Init()
	{
		base.GetComponent<Image>().material = this.guiMain_.matFill_Window;
		UnityEngine.Object.Destroy(this);
	}

	// Token: 0x04002100 RID: 8448
	private mainScript mS_;

	// Token: 0x04002101 RID: 8449
	private GameObject main_;

	// Token: 0x04002102 RID: 8450
	private GUI_Main guiMain_;
}
