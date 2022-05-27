using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C2 RID: 450
public class Menu_GameTabFilter : MonoBehaviour
{
	// Token: 0x060010F2 RID: 4338 RVA: 0x000B4324 File Offset: 0x000B2524
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x000B432C File Offset: 0x000B252C
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

	// Token: 0x060010F4 RID: 4340 RVA: 0x000B43E4 File Offset: 0x000B25E4
	private void OnEnable()
	{
		this.FindScripts();
		this.SetToggles();
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000B43F2 File Offset: 0x000B25F2
	public void Init(bool isMenuOpen_)
	{
		this.isMenuOpen = isMenuOpen_;
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000B43FC File Offset: 0x000B25FC
	private void SetToggles()
	{
		for (int i = 0; i < this.mS_.gameTabFilter.Length; i++)
		{
			this.uiObjects[i].GetComponent<Toggle>().isOn = this.mS_.gameTabFilter[i];
		}
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000B4440 File Offset: 0x000B2640
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.isMenuOpen)
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x000B4470 File Offset: 0x000B2670
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

	// Token: 0x0400157B RID: 5499
	public GameObject gameTabContent;

	// Token: 0x0400157C RID: 5500
	public GameObject[] uiObjects;

	// Token: 0x0400157D RID: 5501
	private GameObject main_;

	// Token: 0x0400157E RID: 5502
	private mainScript mS_;

	// Token: 0x0400157F RID: 5503
	private textScript tS_;

	// Token: 0x04001580 RID: 5504
	private GUI_Main guiMain_;

	// Token: 0x04001581 RID: 5505
	private sfxScript sfx_;

	// Token: 0x04001582 RID: 5506
	private bool isMenuOpen;
}
