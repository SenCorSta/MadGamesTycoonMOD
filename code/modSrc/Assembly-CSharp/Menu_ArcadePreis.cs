using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000112 RID: 274
public class Menu_ArcadePreis : MonoBehaviour
{
	// Token: 0x060008EB RID: 2283 RVA: 0x000069B0 File Offset: 0x00004BB0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x000727BC File Offset: 0x000709BC
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x0007295C File Offset: 0x00070B5C
	public void Init(gameScript game_, taskGame t_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.task_ = t_;
		this.orgPreis = game_.verkaufspreis[0];
		if (this.task_)
		{
			this.uiObjects[19].SetActive(true);
			this.uiObjects[20].SetActive(true);
			this.uiObjects[21].SetActive(true);
			this.uiObjects[22].SetActive(true);
			this.uiObjects[23].SetActive(true);
			this.uiObjects[24].SetActive(true);
			this.uiObjects[25].SetActive(true);
			this.uiObjects[26].SetActive(true);
		}
		else
		{
			this.uiObjects[19].SetActive(false);
			this.uiObjects[20].SetActive(false);
			this.uiObjects[21].SetActive(false);
			this.uiObjects[22].SetActive(false);
			this.uiObjects[23].SetActive(false);
			this.uiObjects[24].SetActive(false);
			this.uiObjects[25].SetActive(false);
			this.uiObjects[26].SetActive(false);
			this.setCase = this.gS_.arcadeCase;
			this.setMonitor = this.gS_.arcadeMonitor;
			this.setJoystick = this.gS_.arcadeJoystick;
			this.setSound = this.gS_.arcadeSound;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		if (this.gS_.verkaufspreis[0] <= 0)
		{
			this.uiObjects[13].GetComponent<Slider>().value = 1000f;
		}
		else
		{
			this.uiObjects[13].GetComponent<Slider>().value = (float)this.gS_.verkaufspreis[0];
		}
		this.SLIDER_Preis();
		this.SetData();
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00072B48 File Offset: 0x00070D48
	private void SetData()
	{
		this.uiObjects[15].GetComponent<Text>().text = this.mS_.GetMoney((long)this.prodCostsCase[this.setCase], true);
		this.uiObjects[16].GetComponent<Text>().text = this.mS_.GetMoney((long)this.prodCostsMonitor[this.setMonitor], true);
		this.uiObjects[17].GetComponent<Text>().text = this.mS_.GetMoney((long)this.prodCostsJoystick[this.setJoystick], true);
		this.uiObjects[18].GetComponent<Text>().text = this.mS_.GetMoney((long)this.prodCostsSound[this.setSound], true);
		this.guiMain_.DrawStars(this.uiObjects[6], this.setCase + 1);
		this.guiMain_.DrawStars(this.uiObjects[7], this.setMonitor + 1);
		this.guiMain_.DrawStars(this.uiObjects[8], this.setJoystick + 1);
		this.guiMain_.DrawStars(this.uiObjects[9], this.setSound + 1);
		int num = Mathf.RoundToInt(this.uiObjects[13].GetComponent<Slider>().value);
		if (this.gS_.vorbestellungen > 0 && num != this.orgPreis)
		{
			num = this.orgPreis;
			this.uiObjects[13].GetComponent<Slider>().value = (float)this.orgPreis;
			this.guiMain_.MessageBox(this.tS_.GetText(1579), false);
		}
		this.uiObjects[11].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		int num2 = this.CalcProdCosts();
		this.uiObjects[10].GetComponent<Text>().text = this.mS_.GetMoney((long)num2, true);
		int num3 = num - num2;
		this.uiObjects[12].GetComponent<Text>().text = this.mS_.GetMoney((long)num3, true);
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x000069B8 File Offset: 0x00004BB8
	public int CalcProdCosts()
	{
		return 0 + this.prodCostsCase[this.setCase] + this.prodCostsMonitor[this.setMonitor] + this.prodCostsJoystick[this.setJoystick] + this.prodCostsSound[this.setSound];
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x00072D54 File Offset: 0x00070F54
	public void BUTTON_Minus_Komponent(int i)
	{
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
			this.setCase--;
			if (this.setCase < 0)
			{
				this.setCase = 0;
			}
			break;
		case 1:
			this.setMonitor--;
			if (this.setMonitor < 0)
			{
				this.setMonitor = 0;
			}
			break;
		case 2:
			this.setJoystick--;
			if (this.setJoystick < 0)
			{
				this.setJoystick = 0;
			}
			break;
		case 3:
			this.setSound--;
			if (this.setSound < 0)
			{
				this.setSound = 0;
			}
			break;
		}
		this.SetData();
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00072E0C File Offset: 0x0007100C
	public void BUTTON_Plus_Komponent(int i)
	{
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
			this.setCase++;
			if (this.setCase > 4)
			{
				this.setCase = 4;
			}
			break;
		case 1:
			this.setMonitor++;
			if (this.setMonitor > 4)
			{
				this.setMonitor = 4;
			}
			break;
		case 2:
			this.setJoystick++;
			if (this.setJoystick > 4)
			{
				this.setJoystick = 4;
			}
			break;
		case 3:
			this.setSound++;
			if (this.setSound > 4)
			{
				this.setSound = 4;
			}
			break;
		}
		this.SetData();
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x000069F3 File Offset: 0x00004BF3
	public void SLIDER_Preis()
	{
		this.SetData();
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00072EC4 File Offset: 0x000710C4
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.task_)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[69]);
			this.guiMain_.uiObjects[69].GetComponent<Menu_DevGame_Complete>().Init(this.gS_, this.task_);
			this.guiMain_.OpenMenu(false);
		}
		this.task_ = null;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00072F48 File Offset: 0x00071148
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		if (Mathf.RoundToInt(this.uiObjects[13].GetComponent<Slider>().value) - this.CalcProdCosts() < 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1534), false);
			return;
		}
		if (this.task_)
		{
			this.gS_.SetPublisher(-1);
			this.gS_.SetOnMarket();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
			this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		}
		if (this.task_)
		{
			UnityEngine.Object.Destroy(this.task_.gameObject);
		}
		this.gS_.verkaufspreis[0] = Mathf.RoundToInt(this.uiObjects[13].GetComponent<Slider>().value);
		this.gS_.arcadeProdCosts = this.CalcProdCosts();
		this.gS_.arcadeCase = this.setCase;
		this.gS_.arcadeMonitor = this.setMonitor;
		this.gS_.arcadeJoystick = this.setJoystick;
		this.gS_.arcadeSound = this.setSound;
		base.gameObject.SetActive(false);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x000069FB File Offset: 0x00004BFB
	private IEnumerator iMinusPreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusPreis(i);
		}
		yield break;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x000730A0 File Offset: 0x000712A0
	public void BUTTON_MinusPreis(int i)
	{
		int num = Mathf.RoundToInt(this.uiObjects[13].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num -= i;
		if (num < 500)
		{
			num = 500;
		}
		this.uiObjects[13].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iMinusPreis(i));
		this.SetData();
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00006A11 File Offset: 0x00004C11
	private IEnumerator iPlusPreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusPreis(i);
		}
		yield break;
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00073110 File Offset: 0x00071310
	public void BUTTON_PlusPreis(int i)
	{
		int num = Mathf.RoundToInt(this.uiObjects[13].GetComponent<Slider>().value);
		this.sfx_.PlaySound(3, true);
		num += i;
		if (num > 1500)
		{
			num = 1500;
		}
		this.uiObjects[13].GetComponent<Slider>().value = (float)num;
		base.StartCoroutine(this.iPlusPreis(i));
		this.SetData();
	}

	// Token: 0x04000D72 RID: 3442
	public GameObject[] uiObjects;

	// Token: 0x04000D73 RID: 3443
	public int[] prodCostsCase;

	// Token: 0x04000D74 RID: 3444
	public int[] prodCostsMonitor;

	// Token: 0x04000D75 RID: 3445
	public int[] prodCostsJoystick;

	// Token: 0x04000D76 RID: 3446
	public int[] prodCostsSound;

	// Token: 0x04000D77 RID: 3447
	public int setCase;

	// Token: 0x04000D78 RID: 3448
	public int setMonitor;

	// Token: 0x04000D79 RID: 3449
	public int setJoystick;

	// Token: 0x04000D7A RID: 3450
	public int setSound;

	// Token: 0x04000D7B RID: 3451
	public int orgPreis;

	// Token: 0x04000D7C RID: 3452
	private GameObject main_;

	// Token: 0x04000D7D RID: 3453
	private mainScript mS_;

	// Token: 0x04000D7E RID: 3454
	private textScript tS_;

	// Token: 0x04000D7F RID: 3455
	private GUI_Main guiMain_;

	// Token: 0x04000D80 RID: 3456
	private sfxScript sfx_;

	// Token: 0x04000D81 RID: 3457
	private genres genres_;

	// Token: 0x04000D82 RID: 3458
	private themes themes_;

	// Token: 0x04000D83 RID: 3459
	private licences licences_;

	// Token: 0x04000D84 RID: 3460
	private engineFeatures eF_;

	// Token: 0x04000D85 RID: 3461
	private cameraMovementScript cmS_;

	// Token: 0x04000D86 RID: 3462
	private unlockScript unlock_;

	// Token: 0x04000D87 RID: 3463
	private gameplayFeatures gF_;

	// Token: 0x04000D88 RID: 3464
	private games games_;

	// Token: 0x04000D89 RID: 3465
	private gameScript gS_;

	// Token: 0x04000D8A RID: 3466
	private taskGame task_;
}
