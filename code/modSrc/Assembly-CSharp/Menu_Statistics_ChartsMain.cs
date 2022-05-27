using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
public class Menu_Statistics_ChartsMain : MonoBehaviour
{
	// Token: 0x0600152F RID: 5423 RVA: 0x000D95AD File Offset: 0x000D77AD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000D95B8 File Offset: 0x000D77B8
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

	// Token: 0x06001531 RID: 5425 RVA: 0x000D9662 File Offset: 0x000D7862
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000D9688 File Offset: 0x000D7888
	public void BUTTON_AllTimeCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[115]);
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000D96B0 File Offset: 0x000D78B0
	public void BUTTON_UmsdatzCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[375]);
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000D96DB File Offset: 0x000D78DB
	public void BUTTON_AllTimeChartsHandy()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[303]);
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000D9706 File Offset: 0x000D7906
	public void BUTTON_AllTimeChartsArcade()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[305]);
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000D9731 File Offset: 0x000D7931
	public void BUTTON_WochenCharts()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[116]);
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000D9759 File Offset: 0x000D7959
	public void BUTTON_AllTimeAddons()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[191]);
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000D9784 File Offset: 0x000D7984
	public void BUTTON_AllTimeBundles()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[192]);
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000D97AF File Offset: 0x000D79AF
	public void BUTTON_AllTimeBudget()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[190]);
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x000D97DA File Offset: 0x000D79DA
	public void BUTTON_F2P()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[284]);
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000D9805 File Offset: 0x000D7A05
	public void BUTTON_MMOAbos()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[243]);
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000D9830 File Offset: 0x000D7A30
	public void BUTTON_BestGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[232]);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000D985B File Offset: 0x000D7A5B
	public void BUTTON_AllTimeKonsolen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[343]);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000D9886 File Offset: 0x000D7A86
	public void BUTTON_BestIPs()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[355]);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000D98B1 File Offset: 0x000D7AB1
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
