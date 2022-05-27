using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CD RID: 717
public class AmountContracts : MonoBehaviour
{
	// Token: 0x060019FE RID: 6654 RVA: 0x00108FF8 File Offset: 0x001071F8
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x00108FF8 File Offset: 0x001071F8
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x00109008 File Offset: 0x00107208
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x001090B4 File Offset: 0x001072B4
	private void Init()
	{
		base.gameObject.GetComponent<Text>().text = "[" + this.mS_.GetAmountContracts(this.contractTyp).ToString() + "] ";
	}

	// Token: 0x040020FA RID: 8442
	public int contractTyp;

	// Token: 0x040020FB RID: 8443
	private mainScript mS_;

	// Token: 0x040020FC RID: 8444
	private GameObject main_;

	// Token: 0x040020FD RID: 8445
	private GUI_Main guiMain_;

	// Token: 0x040020FE RID: 8446
	private sfxScript sfx_;

	// Token: 0x040020FF RID: 8447
	private textScript tS_;
}
