using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CC RID: 716
public class OptionenForschung : MonoBehaviour
{
	// Token: 0x060019BD RID: 6589 RVA: 0x000115F9 File Offset: 0x0000F7F9
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x000115F9 File Offset: 0x0000F7F9
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x0010D50C File Offset: 0x0010B70C
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

	// Token: 0x060019C0 RID: 6592 RVA: 0x0010D5B8 File Offset: 0x0010B7B8
	private void Init()
	{
		base.gameObject.GetComponent<Text>().text = "[" + this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>().GetAmountForschung(this.forschungTyp, false).ToString() + "] ";
	}

	// Token: 0x040020E9 RID: 8425
	public int forschungTyp;

	// Token: 0x040020EA RID: 8426
	private mainScript mS_;

	// Token: 0x040020EB RID: 8427
	private GameObject main_;

	// Token: 0x040020EC RID: 8428
	private GUI_Main guiMain_;

	// Token: 0x040020ED RID: 8429
	private sfxScript sfx_;

	// Token: 0x040020EE RID: 8430
	private textScript tS_;
}
