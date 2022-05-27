using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AD RID: 429
public class Menu_Result_MarketingSpezial : MonoBehaviour
{
	// Token: 0x06001026 RID: 4134 RVA: 0x0000B683 File Offset: 0x00009883
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x000B8000 File Offset: 0x000B6200
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0000B68B File Offset: 0x0000988B
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x000B80C8 File Offset: 0x000B62C8
	public void Init(gameScript gS_, int kampagne)
	{
		if (!gS_)
		{
			this.BUTTON_Abbrechen();
		}
		this.FindScripts();
		this.sfx_.PlaySound(54, true);
		this.uiObjects[1].GetComponent<Text>().text = gS_.GetNameWithTag();
		if (kampagne < 100)
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiObjects[294].GetComponent<Menu_MarketingSpezial>().sprites[kampagne];
			gS_.specialMarketing[kampagne] = -1;
		}
		else
		{
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
		}
		switch (kampagne)
		{
		case 0:
			if ((!gS_.inDevelopment && !gS_.schublade) || gS_.reviewTotal > 0)
			{
				this.BUTTON_Abbrechen();
				return;
			}
			gS_.CalcReview(true);
			if (gS_.reviewTotal < 40)
			{
				int num = -1 - UnityEngine.Random.Range(0, gS_.reviewTotal) / 5;
				gS_.AddHype((float)num);
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1426);
				this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt((float)num).ToString();
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[33];
				gS_.specialMarketing[kampagne] = -1;
			}
			else
			{
				int num = 1 + UnityEngine.Random.Range(0, gS_.reviewTotal) / 5;
				gS_.AddHype((float)num);
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1427);
				this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt((float)num).ToString();
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[33];
				gS_.specialMarketing[kampagne] = -1;
			}
			gS_.ClearReview();
			return;
		case 1:
			if ((!gS_.inDevelopment && !gS_.schublade) || gS_.reviewTotal > 0)
			{
				this.BUTTON_Abbrechen();
				return;
			}
			gS_.CalcReview(true);
			if (gS_.reviewTotal < 50)
			{
				int num = -3;
				gS_.AddHype((float)num);
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1428);
				this.uiObjects[2].GetComponent<Text>().text = "-3%";
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[30];
				gS_.specialMarketing[kampagne] = num;
			}
			else
			{
				int num = 3;
				gS_.AddHype((float)num);
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1429);
				this.uiObjects[2].GetComponent<Text>().text = "+3%";
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[30];
				gS_.specialMarketing[kampagne] = num;
			}
			gS_.ClearReview();
			return;
		case 2:
			if (!gS_.inDevelopment && !gS_.schublade)
			{
				this.BUTTON_Abbrechen();
				return;
			}
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1430);
				this.uiObjects[2].GetComponent<Text>().text = "+0";
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[33];
				gS_.specialMarketing[kampagne] = -1;
				return;
			}
			if (this.mS_.achScript_)
			{
				this.mS_.achScript_.SetAchivement(41);
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1431);
			this.uiObjects[2].GetComponent<Text>().text = "200";
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[33];
			gS_.hype = 200f;
			gS_.specialMarketing[kampagne] = -1;
			return;
		case 3:
		{
			int num = UnityEngine.Random.Range((gS_.userPositiv + gS_.userNegativ) / 10 + 50, gS_.userPositiv + gS_.userNegativ + 100);
			if (UnityEngine.Random.Range(0, 100) > 50)
			{
				int num2 = 500 + UnityEngine.Random.Range(this.genres_.GetAmountFans() / 20, this.genres_.GetAmountFans() / 10);
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1432);
				this.uiObjects[2].GetComponent<Text>().text = "-" + num2.ToString();
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[34];
				gS_.specialMarketing[kampagne] = -1;
				this.mS_.AddFans(-num2, -1);
				return;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1433);
			this.uiObjects[2].GetComponent<Text>().text = "+" + num.ToString();
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[35];
			gS_.userPositiv += num;
			gS_.specialMarketing[kampagne] = -1;
			return;
		}
		case 4:
		{
			int num;
			if (gS_.reviewTotal < 50)
			{
				num = -1 - UnityEngine.Random.Range(0, gS_.reviewTotal) / 5;
				gS_.AddHype((float)num);
				this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1435);
				this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt((float)num).ToString();
				this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[33];
				gS_.specialMarketing[kampagne] = -1;
				return;
			}
			num = 1 + UnityEngine.Random.Range(0, gS_.reviewTotal) / 5;
			gS_.AddHype((float)num);
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1436);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt((float)num).ToString();
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[33];
			gS_.specialMarketing[kampagne] = -1;
			return;
		}
		default:
		{
			if (kampagne != 100)
			{
				return;
			}
			int num2 = 2000 + UnityEngine.Random.Range(0, 3000);
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1437);
			this.uiObjects[2].GetComponent<Text>().text = "-" + num2.ToString();
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[34];
			gS_.hype = 100f;
			this.mS_.AddFans(-num2, -1);
			return;
		}
		}
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0000B6A6 File Offset: 0x000098A6
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040014AC RID: 5292
	public GameObject[] uiObjects;

	// Token: 0x040014AD RID: 5293
	private GameObject main_;

	// Token: 0x040014AE RID: 5294
	private mainScript mS_;

	// Token: 0x040014AF RID: 5295
	private textScript tS_;

	// Token: 0x040014B0 RID: 5296
	private GUI_Main guiMain_;

	// Token: 0x040014B1 RID: 5297
	private sfxScript sfx_;

	// Token: 0x040014B2 RID: 5298
	private genres genres_;

	// Token: 0x040014B3 RID: 5299
	private bool closeMenu;
}
