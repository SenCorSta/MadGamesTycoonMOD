using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D2 RID: 466
public class Menu_NewsSetting : MonoBehaviour
{
	// Token: 0x0600118E RID: 4494 RVA: 0x0000C4D1 File Offset: 0x0000A6D1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x000C5568 File Offset: 0x000C3768
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

	// Token: 0x06001190 RID: 4496 RVA: 0x0000C4D9 File Offset: 0x0000A6D9
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x000C5620 File Offset: 0x000C3820
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

	// Token: 0x06001192 RID: 4498 RVA: 0x0000C4E7 File Offset: 0x0000A6E7
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x000C5768 File Offset: 0x000C3968
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

	// Token: 0x0400161D RID: 5661
	public GameObject[] uiObjects;

	// Token: 0x0400161E RID: 5662
	private GameObject main_;

	// Token: 0x0400161F RID: 5663
	private mainScript mS_;

	// Token: 0x04001620 RID: 5664
	private textScript tS_;

	// Token: 0x04001621 RID: 5665
	private GUI_Main guiMain_;

	// Token: 0x04001622 RID: 5666
	private sfxScript sfx_;
}
