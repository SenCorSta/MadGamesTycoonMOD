using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000122 RID: 290
public class Menu_DevGame_Size : MonoBehaviour
{
	// Token: 0x06000A0C RID: 2572 RVA: 0x0006DE28 File Offset: 0x0006C028
	private void Start()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0006DE38 File Offset: 0x0006C038
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

	// Token: 0x06000A0E RID: 2574 RVA: 0x0006DF44 File Offset: 0x0006C144
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0006DF4C File Offset: 0x0006C14C
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0006DF54 File Offset: 0x0006C154
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

	// Token: 0x06000A11 RID: 2577 RVA: 0x0006DFA0 File Offset: 0x0006C1A0
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

	// Token: 0x06000A12 RID: 2578 RVA: 0x0006E25A File Offset: 0x0006C45A
	public void BUTTON_GameSize(int i)
	{
		this.mDevGame_.SetGameSize(i);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0006E281 File Offset: 0x0006C481
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E56 RID: 3670
	public GameObject[] uiObjects;

	// Token: 0x04000E57 RID: 3671
	private GameObject main_;

	// Token: 0x04000E58 RID: 3672
	private mainScript mS_;

	// Token: 0x04000E59 RID: 3673
	private textScript tS_;

	// Token: 0x04000E5A RID: 3674
	private GUI_Main guiMain_;

	// Token: 0x04000E5B RID: 3675
	private sfxScript sfx_;

	// Token: 0x04000E5C RID: 3676
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E5D RID: 3677
	private unlockScript unlock_;

	// Token: 0x04000E5E RID: 3678
	private forschungSonstiges fS_;

	// Token: 0x04000E5F RID: 3679
	private float updateTimer;
}
