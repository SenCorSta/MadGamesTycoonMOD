using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C1 RID: 449
public class Menu_GameTabFilter : MonoBehaviour
{
	// Token: 0x060010D8 RID: 4312 RVA: 0x0000BE12 File Offset: 0x0000A012
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x000BFDF8 File Offset: 0x000BDFF8
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

	// Token: 0x060010DA RID: 4314 RVA: 0x0000BE1A File Offset: 0x0000A01A
	private void OnEnable()
	{
		this.FindScripts();
		this.SetToggles();
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0000BE28 File Offset: 0x0000A028
	public void Init(bool isMenuOpen_)
	{
		this.isMenuOpen = isMenuOpen_;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x000BFEB0 File Offset: 0x000BE0B0
	private void SetToggles()
	{
		for (int i = 0; i < this.mS_.gameTabFilter.Length; i++)
		{
			this.uiObjects[i].GetComponent<Toggle>().isOn = this.mS_.gameTabFilter[i];
		}
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0000BE31 File Offset: 0x0000A031
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.isMenuOpen)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x000BFEF4 File Offset: 0x000BE0F4
	public void BUTTON_OK()
	{
		for (int i = 0; i < this.mS_.gameTabFilter.Length; i++)
		{
			this.mS_.gameTabFilter[i] = this.uiObjects[i].GetComponent<Toggle>().isOn;
		}
		this.gameTabContent.GetComponent<GamesGroupContent>().timer = 10f;
		this.sfx_.PlaySound(3, true);
		this.BUTTON_Abbrechen();
	}

	// Token: 0x04001572 RID: 5490
	public GameObject gameTabContent;

	// Token: 0x04001573 RID: 5491
	public GameObject[] uiObjects;

	// Token: 0x04001574 RID: 5492
	private GameObject main_;

	// Token: 0x04001575 RID: 5493
	private mainScript mS_;

	// Token: 0x04001576 RID: 5494
	private textScript tS_;

	// Token: 0x04001577 RID: 5495
	private GUI_Main guiMain_;

	// Token: 0x04001578 RID: 5496
	private sfxScript sfx_;

	// Token: 0x04001579 RID: 5497
	private bool isMenuOpen;
}
