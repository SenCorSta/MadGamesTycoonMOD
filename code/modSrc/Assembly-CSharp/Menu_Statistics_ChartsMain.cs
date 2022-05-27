using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class Menu_Statistics_ChartsMain : MonoBehaviour
{
	// Token: 0x0600152F RID: 5423 RVA: 0x000D9581 File Offset: 0x000D7781
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000D958C File Offset: 0x000D778C
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

	// Token: 0x06001531 RID: 5425 RVA: 0x000D9636 File Offset: 0x000D7836
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000D965C File Offset: 0x000D785C
	public void BUTTON_AllTimeCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[115]);
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000D9684 File Offset: 0x000D7884
	public void BUTTON_UmsdatzCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[375]);
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000D96AF File Offset: 0x000D78AF
	public void BUTTON_AllTimeChartsHandy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[303]);
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000D96DA File Offset: 0x000D78DA
	public void BUTTON_AllTimeChartsArcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[305]);
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000D9705 File Offset: 0x000D7905
	public void BUTTON_WochenCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[116]);
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000D972D File Offset: 0x000D792D
	public void BUTTON_AllTimeAddons()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[191]);
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000D9758 File Offset: 0x000D7958
	public void BUTTON_AllTimeBundles()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[192]);
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000D9783 File Offset: 0x000D7983
	public void BUTTON_AllTimeBudget()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[190]);
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x000D97AE File Offset: 0x000D79AE
	public void BUTTON_F2P()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[284]);
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000D97D9 File Offset: 0x000D79D9
	public void BUTTON_MMOAbos()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[243]);
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000D9804 File Offset: 0x000D7A04
	public void BUTTON_BestGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[232]);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000D982F File Offset: 0x000D7A2F
	public void BUTTON_AllTimeKonsolen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[343]);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000D985A File Offset: 0x000D7A5A
	public void BUTTON_BestIPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[355]);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000D9885 File Offset: 0x000D7A85
	public void BUTTON_MostPlayedF2P()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[363]);
	}

	// Token: 0x0400192C RID: 6444
	public GameObject[] uiObjects;

	// Token: 0x0400192D RID: 6445
	private roomScript rS_;

	// Token: 0x0400192E RID: 6446
	private GameObject main_;

	// Token: 0x0400192F RID: 6447
	private mainScript mS_;

	// Token: 0x04001930 RID: 6448
	private textScript tS_;

	// Token: 0x04001931 RID: 6449
	private GUI_Main guiMain_;

	// Token: 0x04001932 RID: 6450
	private sfxScript sfx_;
}
