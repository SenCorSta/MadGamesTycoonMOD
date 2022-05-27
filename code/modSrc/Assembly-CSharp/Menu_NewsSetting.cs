using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D3 RID: 467
public class Menu_NewsSetting : MonoBehaviour
{
	// Token: 0x060011A8 RID: 4520 RVA: 0x000BA1F0 File Offset: 0x000B83F0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x000BA1F8 File Offset: 0x000B83F8
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.main_)
		{
			return;
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

	// Token: 0x060011AA RID: 4522 RVA: 0x000BA2B0 File Offset: 0x000B84B0
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x000BA2C0 File Offset: 0x000B84C0
	private void Init()
	{
		this.uiObjects[0].GetComponent<Toggle>().isOn = this.mS_.newsSetting[0];
		this.uiObjects[1].GetComponent<Toggle>().isOn = this.mS_.newsSetting[1];
		this.uiObjects[2].GetComponent<Toggle>().isOn = this.mS_.newsSetting[2];
		this.uiObjects[3].GetComponent<Toggle>().isOn = this.mS_.newsSetting[3];
		this.uiObjects[4].GetComponent<Toggle>().isOn = this.mS_.newsSetting[4];
		this.uiObjects[5].GetComponent<Toggle>().isOn = this.mS_.newsSetting[5];
		this.uiObjects[6].GetComponent<Toggle>().isOn = this.mS_.newsSetting[6];
		this.uiObjects[7].GetComponent<Toggle>().isOn = this.mS_.newsSetting[7];
		this.uiObjects[8].GetComponent<Toggle>().isOn = this.mS_.newsSetting[8];
		this.uiObjects[9].GetComponent<Toggle>().isOn = this.mS_.newsSetting[9];
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x000BA405 File Offset: 0x000B8605
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x000BA42C File Offset: 0x000B862C
	public void BUTTON_OK()
	{
		this.mS_.newsSetting[0] = this.uiObjects[0].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[1] = this.uiObjects[1].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[2] = this.uiObjects[2].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[3] = this.uiObjects[3].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[4] = this.uiObjects[4].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[5] = this.uiObjects[5].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[6] = this.uiObjects[6].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[7] = this.uiObjects[7].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[8] = this.uiObjects[8].GetComponent<Toggle>().isOn;
		this.mS_.newsSetting[9] = this.uiObjects[9].GetComponent<Toggle>().isOn;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001626 RID: 5670
	public GameObject[] uiObjects;

	// Token: 0x04001627 RID: 5671
	private GameObject main_;

	// Token: 0x04001628 RID: 5672
	private mainScript mS_;

	// Token: 0x04001629 RID: 5673
	private textScript tS_;

	// Token: 0x0400162A RID: 5674
	private GUI_Main guiMain_;

	// Token: 0x0400162B RID: 5675
	private sfxScript sfx_;
}
