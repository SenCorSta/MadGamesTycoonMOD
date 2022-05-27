using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011E RID: 286
public class Menu_DevGame_GameTyp : MonoBehaviour
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x0006C617 File Offset: 0x0006A817
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0006C620 File Offset: 0x0006A820
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

	// Token: 0x060009E2 RID: 2530 RVA: 0x0006C70E File Offset: 0x0006A90E
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0006C71C File Offset: 0x0006A91C
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0006C724 File Offset: 0x0006A924
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

	// Token: 0x060009E5 RID: 2533 RVA: 0x0006C770 File Offset: 0x0006A970
	private void Init()
	{
		this.Unlock(21, this.uiObjects[3], this.uiObjects[5]);
		this.Unlock(22, this.uiObjects[4], this.uiObjects[6]);
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mDevGame_.costs_gameTyp[0], true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameTyp[1] * (this.mS_.difficulty + 1)), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.mDevGame_.costs_gameTyp[2] * (this.mS_.difficulty + 1)), true);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0006C84D File Offset: 0x0006AA4D
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

	// Token: 0x060009E7 RID: 2535 RVA: 0x0006C885 File Offset: 0x0006AA85
	public void BUTTON_GameTyp(int i)
	{
		this.mDevGame_.SetGameTyp(i);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0006C8AC File Offset: 0x0006AAAC
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000E31 RID: 3633
	public GameObject[] uiObjects;

	// Token: 0x04000E32 RID: 3634
	private GameObject main_;

	// Token: 0x04000E33 RID: 3635
	private mainScript mS_;

	// Token: 0x04000E34 RID: 3636
	private textScript tS_;

	// Token: 0x04000E35 RID: 3637
	private GUI_Main guiMain_;

	// Token: 0x04000E36 RID: 3638
	private sfxScript sfx_;

	// Token: 0x04000E37 RID: 3639
	private Menu_DevGame mDevGame_;

	// Token: 0x04000E38 RID: 3640
	private unlockScript unlock_;

	// Token: 0x04000E39 RID: 3641
	private float updateTimer;
}
