using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Result_MarketingSpezial : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
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

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private bool closeMenu;
}
