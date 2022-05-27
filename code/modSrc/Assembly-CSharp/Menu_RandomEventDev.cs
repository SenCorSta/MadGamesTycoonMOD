using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001D7 RID: 471
public class Menu_RandomEventDev : MonoBehaviour
{
	// Token: 0x060011C4 RID: 4548 RVA: 0x000BAA35 File Offset: 0x000B8C35
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x000BAA40 File Offset: 0x000B8C40
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

	// Token: 0x060011C6 RID: 4550 RVA: 0x000BAAEA File Offset: 0x000B8CEA
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x000BAB08 File Offset: 0x000B8D08
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

	// Token: 0x060011C8 RID: 4552 RVA: 0x000BB70F File Offset: 0x000B990F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x000BB70F File Offset: 0x000B990F
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400163E RID: 5694
	public GameObject[] uiObjects;

	// Token: 0x0400163F RID: 5695
	private GameObject main_;

	// Token: 0x04001640 RID: 5696
	private mainScript mS_;

	// Token: 0x04001641 RID: 5697
	private textScript tS_;

	// Token: 0x04001642 RID: 5698
	private GUI_Main guiMain_;

	// Token: 0x04001643 RID: 5699
	private sfxScript sfx_;

	// Token: 0x04001644 RID: 5700
	private bool closeMenu;
}
