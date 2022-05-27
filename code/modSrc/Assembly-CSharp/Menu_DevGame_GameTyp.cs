using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011D RID: 285
public class Menu_DevGame_GameTyp : MonoBehaviour
{
	// Token: 0x060009D1 RID: 2513 RVA: 0x00007144 File Offset: 0x00005344
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0007D428 File Offset: 0x0007B628
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
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0000714C File Offset: 0x0000534C
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0000715A File Offset: 0x0000535A
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0007D518 File Offset: 0x0007B718
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

	// Token: 0x060009D6 RID: 2518 RVA: 0x0007D564 File Offset: 0x0007B764
	private void Init()
	{
		this.Unlock(21, this.uiObjects[3], this.uiObjects[5]);
		this.Unlock(22, this.uiObjects[4], this.uiObjects[6]);
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mDevGame_.costs_gameTyp[0], true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameTyp[1] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameTyp[2] * (this.mS_.difficulty + 1)), true);
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00007162 File Offset: 0x00005362
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			lock_.SetActive(false);
			return;
		}
		button_.GetComponent<Button>().interactable = false;
		lock_.SetActive(true);
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0000719A File Offset: 0x0000539A
	public void BUTTON_GameTyp(int i)
	{
		this.mDevGame_.SetGameTyp(i);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x000071C1 File Offset: 0x000053C1
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E29 RID: 3625
	public GameObject[] uiObjects;

	// Token: 0x04000E2A RID: 3626
	private GameObject main_;

	// Token: 0x04000E2B RID: 3627
	private mainScript mS_;

	// Token: 0x04000E2C RID: 3628
	private textScript tS_;

	// Token: 0x04000E2D RID: 3629
	private GUI_Main guiMain_;

	// Token: 0x04000E2E RID: 3630
	private sfxScript sfx_;

	// Token: 0x04000E2F RID: 3631
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E30 RID: 3632
	private unlockScript unlock_;

	// Token: 0x04000E31 RID: 3633
	private float updateTimer;
}
