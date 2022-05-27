using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D7 RID: 727
public class blendIn : MonoBehaviour
{
	// Token: 0x06001A28 RID: 6696 RVA: 0x00109E68 File Offset: 0x00108068
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x00109E70 File Offset: 0x00108070
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

	// Token: 0x06001A2A RID: 6698 RVA: 0x00109F1A File Offset: 0x0010811A
	private void OnEnable()
	{
		this.uiObjects[0].GetComponent<Image>().fillAmount = 1f;
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x00109F34 File Offset: 0x00108134
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

	// Token: 0x04002132 RID: 8498
	public GameObject[] uiObjects;

	// Token: 0x04002133 RID: 8499
	private GameObject main_;

	// Token: 0x04002134 RID: 8500
	private mainScript mS_;

	// Token: 0x04002135 RID: 8501
	private textScript tS_;

	// Token: 0x04002136 RID: 8502
	private GUI_Main guiMain_;

	// Token: 0x04002137 RID: 8503
	private sfxScript sfx_;
}
