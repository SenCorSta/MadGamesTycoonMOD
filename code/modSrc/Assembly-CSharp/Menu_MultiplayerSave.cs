using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027C RID: 636
public class Menu_MultiplayerSave : MonoBehaviour
{
	// Token: 0x060018CA RID: 6346 RVA: 0x00010FB9 File Offset: 0x0000F1B9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060018CB RID: 6347 RVA: 0x000FE11C File Offset: 0x000FC31C
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
		if (!this.save_)
		{
			this.save_ = this.main_.GetComponent<savegameScript>();
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

	// Token: 0x060018CC RID: 6348 RVA: 0x00010FB9 File Offset: 0x0000F1B9
	public void OnEnable()
	{
		this.FindScripts();
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x00010FC1 File Offset: 0x0000F1C1
	public void Init(int saveID_)
	{
		this.saveID = saveID_;
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x000FE1E4 File Offset: 0x000FC3E4
	private void Update()
	{
		if (this.uiObjects[1].GetComponent<Button>().interactable)
		{
			this.timer += Time.deltaTime;
			if (this.timer > 1f)
			{
				this.BUTTON_Yes();
				return;
			}
		}
		else
		{
			this.timer = 0f;
		}
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x00010FCA File Offset: 0x0000F1CA
	public void BUTTON_Yes()
	{
		this.save_.Save(this.saveID);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001C5D RID: 7261
	public GameObject[] uiObjects;

	// Token: 0x04001C5E RID: 7262
	private GameObject main_;

	// Token: 0x04001C5F RID: 7263
	private mainScript mS_;

	// Token: 0x04001C60 RID: 7264
	private textScript tS_;

	// Token: 0x04001C61 RID: 7265
	private GUI_Main guiMain_;

	// Token: 0x04001C62 RID: 7266
	private sfxScript sfx_;

	// Token: 0x04001C63 RID: 7267
	private savegameScript save_;

	// Token: 0x04001C64 RID: 7268
	private int saveID = -1;

	// Token: 0x04001C65 RID: 7269
	private float timer;
}
