﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D6 RID: 470
public class Menu_RandomEventDev : MonoBehaviour
{
	// Token: 0x060011AA RID: 4522 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x000C5C94 File Offset: 0x000C3E94
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

	// Token: 0x060011AC RID: 4524 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x000C5D40 File Offset: 0x000C3F40
	public void Init(gameScript gS_)
	{
		if (!gS_)
		{
			this.BUTTON_Abbrechen();
		}
		this.FindScripts();
		this.uiObjects[1].GetComponent<Text>().text = gS_.GetNameWithTag();
		switch (UnityEngine.Random.Range(0, 14))
		{
		case 0:
		{
			float num = gS_.points_grafik * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_grafik -= num;
			if (gS_.points_grafik < 0f)
			{
				gS_.points_grafik = 0f;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1066);
			this.uiObjects[2].GetComponent<Text>().text = "-" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[21];
			this.sfx_.PlaySound(53, true);
			break;
		}
		case 1:
		{
			float num = gS_.points_sound * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_sound += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1067);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[22];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 2:
		{
			float num = (float)UnityEngine.Random.Range(25, 35);
			gS_.points_bugs += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1068);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[24];
			this.sfx_.PlaySound(53, true);
			break;
		}
		case 3:
		{
			float num = gS_.points_technik * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_technik += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1069);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[23];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 4:
		{
			float num = gS_.points_gameplay * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_gameplay += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1070);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[20];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 5:
		{
			float num = (float)UnityEngine.Random.Range(10, 30);
			gS_.AddHype(-num);
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1071);
			this.uiObjects[2].GetComponent<Text>().text = "-" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[25];
			this.sfx_.PlaySound(53, true);
			break;
		}
		case 6:
		{
			float num = (float)UnityEngine.Random.Range(10, 30);
			gS_.AddHype(num);
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1072);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[25];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 7:
		{
			float num = gS_.points_sound * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_sound -= num;
			if (gS_.points_sound < 0f)
			{
				gS_.points_sound = 0f;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1073);
			this.uiObjects[2].GetComponent<Text>().text = "-" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[22];
			this.sfx_.PlaySound(53, true);
			break;
		}
		case 8:
		{
			float num = gS_.points_technik * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_technik -= num;
			if (gS_.points_technik < 0f)
			{
				gS_.points_technik = 0f;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1074);
			this.uiObjects[2].GetComponent<Text>().text = "-" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[23];
			this.sfx_.PlaySound(53, true);
			break;
		}
		case 9:
		{
			float num = gS_.points_gameplay * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_gameplay -= num;
			if (gS_.points_gameplay < 0f)
			{
				gS_.points_gameplay = 0f;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1075);
			this.uiObjects[2].GetComponent<Text>().text = "-" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[20];
			this.sfx_.PlaySound(53, true);
			break;
		}
		case 10:
		{
			float num = (float)UnityEngine.Random.Range(25, 35);
			gS_.points_bugs -= num;
			if (gS_.points_bugs < 0f)
			{
				gS_.points_bugs = 0f;
			}
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1076);
			this.uiObjects[2].GetComponent<Text>().text = "-" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[24];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 11:
		{
			float num = gS_.points_grafik * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_grafik += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1077);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[21];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 12:
		{
			float num = gS_.points_sound * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_sound += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1078);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[22];
			this.sfx_.PlaySound(54, true);
			break;
		}
		case 13:
		{
			float num = gS_.points_technik * UnityEngine.Random.Range(0.01f, 0.1f) + (float)UnityEngine.Random.Range(5, 10);
			gS_.points_technik += num;
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1079);
			this.uiObjects[2].GetComponent<Text>().text = "+" + Mathf.RoundToInt(num);
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[7];
			this.uiObjects[3].GetComponent<Image>().sprite = this.guiMain_.uiSprites[23];
			this.sfx_.PlaySound(54, true);
			break;
		}
		}
		if (this.mS_.settings_ && this.mS_.settings_.hideEvents)
		{
			this.BUTTON_Abbrechen();
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x0000C60B File Offset: 0x0000A80B
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x0000C60B File Offset: 0x0000A80B
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001635 RID: 5685
	public GameObject[] uiObjects;

	// Token: 0x04001636 RID: 5686
	private GameObject main_;

	// Token: 0x04001637 RID: 5687
	private mainScript mS_;

	// Token: 0x04001638 RID: 5688
	private textScript tS_;

	// Token: 0x04001639 RID: 5689
	private GUI_Main guiMain_;

	// Token: 0x0400163A RID: 5690
	private sfxScript sfx_;

	// Token: 0x0400163B RID: 5691
	private bool closeMenu;
}
