using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CA RID: 714
public class AmountContracts : MonoBehaviour
{
	// Token: 0x060019B4 RID: 6580 RVA: 0x000115BF File Offset: 0x0000F7BF
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x000115BF File Offset: 0x0000F7BF
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x0010D3AC File Offset: 0x0010B5AC
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

	// Token: 0x060019B7 RID: 6583 RVA: 0x0010D458 File Offset: 0x0010B658
	private void Init()
	{
		base.gameObject.GetComponent<Text>().text = "[" + this.mS_.GetAmountContracts(this.contractTyp).ToString() + "] ";
	}

	// Token: 0x040020E0 RID: 8416
	public int contractTyp;

	// Token: 0x040020E1 RID: 8417
	private mainScript mS_;

	// Token: 0x040020E2 RID: 8418
	private GameObject main_;

	// Token: 0x040020E3 RID: 8419
	private GUI_Main guiMain_;

	// Token: 0x040020E4 RID: 8420
	private sfxScript sfx_;

	// Token: 0x040020E5 RID: 8421
	private textScript tS_;
}
