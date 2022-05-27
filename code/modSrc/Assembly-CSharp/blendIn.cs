using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D4 RID: 724
public class blendIn : MonoBehaviour
{
	// Token: 0x060019DE RID: 6622 RVA: 0x00011771 File Offset: 0x0000F971
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x0010E098 File Offset: 0x0010C298
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x00011779 File Offset: 0x0000F979
	private void OnEnable()
	{
		this.uiObjects[0].GetComponent<Image>().fillAmount = 1f;
	}

	// Token: 0x060019E1 RID: 6625 RVA: 0x0010E144 File Offset: 0x0010C344
	private void Update()
	{
		if (this.uiObjects[0].GetComponent<Image>().fillAmount > 0f)
		{
			this.uiObjects[0].GetComponent<Image>().fillAmount -= Time.deltaTime * 2f;
			if (this.uiObjects[0].GetComponent<Image>().fillAmount <= 0f)
			{
				this.uiObjects[0].GetComponent<Image>().fillAmount = 1f;
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04002118 RID: 8472
	public GameObject[] uiObjects;

	// Token: 0x04002119 RID: 8473
	private GameObject main_;

	// Token: 0x0400211A RID: 8474
	private mainScript mS_;

	// Token: 0x0400211B RID: 8475
	private textScript tS_;

	// Token: 0x0400211C RID: 8476
	private GUI_Main guiMain_;

	// Token: 0x0400211D RID: 8477
	private sfxScript sfx_;
}
