using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000216 RID: 534
public class Menu_Bank : MonoBehaviour
{
	// Token: 0x06001492 RID: 5266 RVA: 0x000D591F File Offset: 0x000D3B1F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000D5928 File Offset: 0x000D3B28
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

	// Token: 0x06001494 RID: 5268 RVA: 0x000D59D2 File Offset: 0x000D3BD2
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000D59DA File Offset: 0x000D3BDA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x000D59E4 File Offset: 0x000D3BE4
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

	// Token: 0x06001497 RID: 5271 RVA: 0x000D5A30 File Offset: 0x000D3C30
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.GetKreditlimit(), true);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.GetKredit(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.mS_.GetKreditZinsen(), true);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(25000L, true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney(25000L, true);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney(50000L, true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney(50000L, true);
		this.uiObjects[7].GetComponent<Text>().text = this.mS_.GetMoney(100000L, true);
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.GetMoney(100000L, true);
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.GetMoney(250000L, true);
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney(250000L, true);
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000D5BE1 File Offset: 0x000D3DE1
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000D5C08 File Offset: 0x000D3E08
	public void BUTTON_KreditAufnehmen(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (i == 0)
		{
			long num = this.mS_.GetKreditlimit() - this.mS_.kredit;
			this.mS_.kredit += num;
			this.mS_.money += num;
			this.Init();
			return;
		}
		if (this.mS_.kredit + (long)i <= this.mS_.GetKreditlimit())
		{
			this.mS_.kredit += (long)i;
			this.mS_.money += (long)i;
			this.Init();
			return;
		}
		long num2 = this.mS_.GetKreditlimit() - this.mS_.kredit;
		this.mS_.kredit += num2;
		this.mS_.money += num2;
		this.Init();
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000D5CF8 File Offset: 0x000D3EF8
	public void BUTTON_KreditAbzahlen(int i)
	{
		if (i == 0)
		{
			for (;;)
			{
				this.BUTTON_KreditAbzahlen(25000);
				if (this.mS_.money < 25000L)
				{
					break;
				}
				if (this.mS_.kredit <= 0L)
				{
					return;
				}
			}
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (this.mS_.money >= (long)i)
		{
			this.mS_.kredit -= (long)i;
			this.mS_.money -= (long)i;
			if (this.mS_.kredit < 0L)
			{
				this.mS_.money += this.mS_.kredit * -1L;
				this.mS_.kredit = 0L;
			}
			this.Init();
		}
	}

	// Token: 0x040018A2 RID: 6306
	public GameObject[] uiObjects;

	// Token: 0x040018A3 RID: 6307
	private roomScript rS_;

	// Token: 0x040018A4 RID: 6308
	private GameObject main_;

	// Token: 0x040018A5 RID: 6309
	private mainScript mS_;

	// Token: 0x040018A6 RID: 6310
	private textScript tS_;

	// Token: 0x040018A7 RID: 6311
	private GUI_Main guiMain_;

	// Token: 0x040018A8 RID: 6312
	private sfxScript sfx_;

	// Token: 0x040018A9 RID: 6313
	private float updateTimer;
}
