using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CF RID: 719
public class OptionenForschung : MonoBehaviour
{
	// Token: 0x06001A07 RID: 6663 RVA: 0x00109190 File Offset: 0x00107390
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001A08 RID: 6664 RVA: 0x00109190 File Offset: 0x00107390
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x001091A0 File Offset: 0x001073A0
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

	// Token: 0x06001A0A RID: 6666 RVA: 0x0010924C File Offset: 0x0010744C
	private void Init()
	{
		base.gameObject.GetComponent<Text>().text = "[" + this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>().GetAmountForschung(this.forschungTyp, false).ToString() + "] ";
	}

	// Token: 0x04002103 RID: 8451
	public int forschungTyp;

	// Token: 0x04002104 RID: 8452
	private mainScript mS_;

	// Token: 0x04002105 RID: 8453
	private GameObject main_;

	// Token: 0x04002106 RID: 8454
	private GUI_Main guiMain_;

	// Token: 0x04002107 RID: 8455
	private sfxScript sfx_;

	// Token: 0x04002108 RID: 8456
	private textScript tS_;
}
