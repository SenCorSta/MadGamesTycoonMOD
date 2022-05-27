using System;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class Menu_Statistics_ChartsMain : MonoBehaviour
{
	// Token: 0x06001511 RID: 5393 RVA: 0x0000E57C File Offset: 0x0000C77C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x000E23AC File Offset: 0x000E05AC
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

	// Token: 0x06001513 RID: 5395 RVA: 0x0000E584 File Offset: 0x0000C784
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x0000E5AA File Offset: 0x0000C7AA
	public void BUTTON_AllTimeCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[115]);
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x0000E5D2 File Offset: 0x0000C7D2
	public void BUTTON_UmsdatzCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[375]);
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x0000E5FD File Offset: 0x0000C7FD
	public void BUTTON_AllTimeChartsHandy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[303]);
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x0000E628 File Offset: 0x0000C828
	public void BUTTON_AllTimeChartsArcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[305]);
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x0000E653 File Offset: 0x0000C853
	public void BUTTON_WochenCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[116]);
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x0000E67B File Offset: 0x0000C87B
	public void BUTTON_AllTimeAddons()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[191]);
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x0000E6A6 File Offset: 0x0000C8A6
	public void BUTTON_AllTimeBundles()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[192]);
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x0000E6D1 File Offset: 0x0000C8D1
	public void BUTTON_AllTimeBudget()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[190]);
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x0000E6FC File Offset: 0x0000C8FC
	public void BUTTON_F2P()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[284]);
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x0000E727 File Offset: 0x0000C927
	public void BUTTON_MMOAbos()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[243]);
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x0000E752 File Offset: 0x0000C952
	public void BUTTON_BestGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[232]);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x0000E77D File Offset: 0x0000C97D
	public void BUTTON_AllTimeKonsolen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[343]);
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
	public void BUTTON_BestIPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[355]);
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
	public void BUTTON_MostPlayedF2P()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[363]);
	}

	// Token: 0x04001925 RID: 6437
	public GameObject[] uiObjects;

	// Token: 0x04001926 RID: 6438
	private roomScript rS_;

	// Token: 0x04001927 RID: 6439
	private GameObject main_;

	// Token: 0x04001928 RID: 6440
	private mainScript mS_;

	// Token: 0x04001929 RID: 6441
	private textScript tS_;

	// Token: 0x0400192A RID: 6442
	private GUI_Main guiMain_;

	// Token: 0x0400192B RID: 6443
	private sfxScript sfx_;
}
