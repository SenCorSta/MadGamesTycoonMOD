using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000121 RID: 289
public class Menu_DevGame_Size : MonoBehaviour
{
	// Token: 0x060009FD RID: 2557 RVA: 0x00007378 File Offset: 0x00005578
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0007EA24 File Offset: 0x0007CC24
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
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
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00007386 File Offset: 0x00005586
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0000738E File Offset: 0x0000558E
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0007EB30 File Offset: 0x0007CD30
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.Init();
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0007EB7C File Offset: 0x0007CD7C
	private void Init()
	{
		this.FindScripts();
		this.fS_.Unlock(0, this.uiObjects[14], this.uiObjects[10]);
		this.fS_.Unlock(1, this.uiObjects[15], this.uiObjects[11]);
		this.fS_.Unlock(2, this.uiObjects[16], this.uiObjects[12]);
		this.fS_.Unlock(3, this.uiObjects[17], this.uiObjects[13]);
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mDevGame_.costs_gameSize[0], true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[1] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[2] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[3] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameSize[4] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[0];
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[1];
		this.uiObjects[7].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[2];
		this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(334) + ": " + this.mDevGame_.maxFeatures_gameSize[3];
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(335);
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00007396 File Offset: 0x00005596
	public void BUTTON_GameSize(int i)
	{
		this.mDevGame_.SetGameSize(i);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x000073BD File Offset: 0x000055BD
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E4E RID: 3662
	public GameObject[] uiObjects;

	// Token: 0x04000E4F RID: 3663
	private GameObject main_;

	// Token: 0x04000E50 RID: 3664
	private mainScript mS_;

	// Token: 0x04000E51 RID: 3665
	private textScript tS_;

	// Token: 0x04000E52 RID: 3666
	private GUI_Main guiMain_;

	// Token: 0x04000E53 RID: 3667
	private sfxScript sfx_;

	// Token: 0x04000E54 RID: 3668
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E55 RID: 3669
	private unlockScript unlock_;

	// Token: 0x04000E56 RID: 3670
	private forschungSonstiges fS_;

	// Token: 0x04000E57 RID: 3671
	private float updateTimer;
}
