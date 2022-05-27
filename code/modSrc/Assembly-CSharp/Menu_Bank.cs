using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000215 RID: 533
public class Menu_Bank : MonoBehaviour
{
	// Token: 0x06001474 RID: 5236 RVA: 0x0000DE82 File Offset: 0x0000C082
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000DF060 File Offset: 0x000DD260
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

	// Token: 0x06001476 RID: 5238 RVA: 0x0000DE8A File Offset: 0x0000C08A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x0000DE92 File Offset: 0x0000C092
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06001478 RID: 5240 RVA: 0x000DF10C File Offset: 0x000DD30C
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

	// Token: 0x06001479 RID: 5241 RVA: 0x000DF158 File Offset: 0x000DD358
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

	// Token: 0x0600147A RID: 5242 RVA: 0x0000DE9A File Offset: 0x0000C09A
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000DF30C File Offset: 0x000DD50C
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

	// Token: 0x0600147C RID: 5244 RVA: 0x000DF3FC File Offset: 0x000DD5FC
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

	// Token: 0x0400189B RID: 6299
	public GameObject[] uiObjects;

	// Token: 0x0400189C RID: 6300
	private roomScript rS_;

	// Token: 0x0400189D RID: 6301
	private GameObject main_;

	// Token: 0x0400189E RID: 6302
	private mainScript mS_;

	// Token: 0x0400189F RID: 6303
	private textScript tS_;

	// Token: 0x040018A0 RID: 6304
	private GUI_Main guiMain_;

	// Token: 0x040018A1 RID: 6305
	private sfxScript sfx_;

	// Token: 0x040018A2 RID: 6306
	private float updateTimer;
}
