using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000051 RID: 81
public class forschungSonstiges : MonoBehaviour
{
	// Token: 0x060001F1 RID: 497 RVA: 0x0001B934 File Offset: 0x00019B34
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0001B93C File Offset: 0x00019B3C
	private void FindScripts()
	{
		if (!this.mS_)
		{
			this.mS_ = base.GetComponent<mainScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = base.GetComponent<textScript>();
		}
		if (!this.settings_)
		{
			this.settings_ = base.GetComponent<settingsScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = base.GetComponent<unlockScript>();
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0001B9B0 File Offset: 0x00019BB0
	public void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		this.FindScripts();
		if (this.IsErforscht(id_))
		{
			if (button_)
			{
				button_.GetComponent<Button>().interactable = true;
			}
			if (lock_)
			{
				lock_.SetActive(false);
				return;
			}
		}
		else
		{
			if (button_)
			{
				button_.GetComponent<Button>().interactable = false;
			}
			if (lock_)
			{
				lock_.SetActive(true);
			}
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0001BA13 File Offset: 0x00019C13
	public bool IsErforscht(int i)
	{
		return this.RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0001BA28 File Offset: 0x00019C28
	public string GetName(int i)
	{
		string result = "";
		switch (i)
		{
		case 0:
			result = this.tS_.GetText(330);
			break;
		case 1:
			result = this.tS_.GetText(331);
			break;
		case 2:
			result = this.tS_.GetText(332);
			break;
		case 3:
			result = this.tS_.GetText(333);
			break;
		case 4:
			result = this.tS_.GetText(571);
			break;
		case 5:
			result = this.tS_.GetText(572);
			break;
		case 6:
			result = this.tS_.GetText(573);
			break;
		case 7:
			result = this.tS_.GetText(574);
			break;
		case 8:
			result = this.tS_.GetText(575);
			break;
		case 9:
			result = this.tS_.GetText(576);
			break;
		case 10:
			result = this.tS_.GetText(577);
			break;
		case 11:
			result = this.tS_.GetText(578);
			break;
		case 12:
			result = this.tS_.GetText(579);
			break;
		case 13:
			result = this.tS_.GetText(580);
			break;
		case 14:
			result = this.tS_.GetText(581);
			break;
		case 15:
			result = this.tS_.GetText(582);
			break;
		case 16:
			result = this.tS_.GetText(583);
			break;
		case 17:
			result = this.tS_.GetText(584);
			break;
		case 18:
			result = this.tS_.GetText(585);
			break;
		case 19:
			result = this.tS_.GetText(586);
			break;
		case 20:
			result = this.tS_.GetText(587);
			break;
		case 21:
			result = this.tS_.GetText(588);
			break;
		case 22:
			result = this.tS_.GetText(589);
			break;
		case 23:
			result = this.tS_.GetText(590);
			break;
		case 24:
			result = this.tS_.GetText(591);
			break;
		case 25:
			result = this.tS_.GetText(592);
			break;
		case 26:
			result = this.tS_.GetText(593);
			break;
		case 27:
			result = this.tS_.GetText(594);
			break;
		case 28:
			result = this.tS_.GetText(21);
			break;
		case 29:
			result = this.tS_.GetText(25);
			break;
		case 30:
			result = this.tS_.GetText(24);
			break;
		case 31:
			result = this.tS_.GetText(22);
			break;
		case 32:
			result = this.tS_.GetText(23);
			break;
		case 33:
			result = this.tS_.GetText(32);
			break;
		case 34:
			result = this.tS_.GetText(31);
			break;
		case 35:
			result = this.tS_.GetText(344);
			break;
		case 36:
			result = this.tS_.GetText(353);
			break;
		case 37:
			result = this.tS_.GetText(381);
			break;
		case 38:
			result = this.tS_.GetText(1508);
			break;
		case 39:
			result = this.tS_.GetText(26);
			break;
		}
		return result;
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0001BE25 File Offset: 0x0001A025
	public int GetPrice(int i)
	{
		return this.RES_PRICE[i];
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0001BE2F File Offset: 0x0001A02F
	public Sprite GetPic(int i)
	{
		return this.RES_SPRITE[i];
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0001BE39 File Offset: 0x0001A039
	public float GetProzent(int i)
	{
		return 100f / this.RES_POINTS[i] * (this.RES_POINTS[i] - this.RES_POINTS_LEFT[i]);
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0001BE5B File Offset: 0x0001A05B
	public bool ForschungGestartet(int i)
	{
		return this.RES_POINTS_LEFT[i] != this.RES_POINTS[i];
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0001BE72 File Offset: 0x0001A072
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.RES_PRICE[i]))
			{
				return false;
			}
			this.mS_.Pay((long)this.RES_PRICE[i], 2);
		}
		return true;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0001BEAC File Offset: 0x0001A0AC
	public bool BereitsInAnderenRaumAktiv(int s)
	{
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component.typ == 2 && component.taskGameObject)
				{
					taskForschung component2 = component.taskGameObject.GetComponent<taskForschung>();
					if (component2 && component2.slot == s && component2.typ == 5)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0001BF38 File Offset: 0x0001A138
	public string GetTooltip(int i)
	{
		string result = "";
		switch (i)
		{
		case 0:
			result = this.tS_.GetText(328);
			break;
		case 1:
			result = this.tS_.GetText(328);
			break;
		case 2:
			result = this.tS_.GetText(328);
			break;
		case 3:
			result = this.tS_.GetText(328);
			break;
		default:
			switch (i)
			{
			case 28:
				result = this.tS_.GetText(36);
				break;
			case 29:
				result = this.tS_.GetText(40);
				break;
			case 30:
				result = this.tS_.GetText(39);
				break;
			case 31:
				result = this.tS_.GetText(37);
				break;
			case 32:
				result = this.tS_.GetText(38);
				break;
			case 33:
				result = this.tS_.GetText(47);
				break;
			case 34:
				result = this.tS_.GetText(46);
				break;
			case 38:
				result = this.tS_.GetText(1512);
				break;
			case 39:
				result = this.tS_.GetText(41);
				break;
			}
			break;
		}
		return result;
	}

	// Token: 0x04000423 RID: 1059
	private mainScript mS_;

	// Token: 0x04000424 RID: 1060
	private textScript tS_;

	// Token: 0x04000425 RID: 1061
	private settingsScript settings_;

	// Token: 0x04000426 RID: 1062
	private unlockScript unlock_;

	// Token: 0x04000427 RID: 1063
	public float[] RES_POINTS;

	// Token: 0x04000428 RID: 1064
	public float[] RES_POINTS_LEFT;

	// Token: 0x04000429 RID: 1065
	public int[] RES_PRICE;

	// Token: 0x0400042A RID: 1066
	public Sprite[] RES_SPRITE;

	// Token: 0x0400042B RID: 1067
	public const int gameSizeBplus = 0;

	// Token: 0x0400042C RID: 1068
	public const int gameSizeA = 1;

	// Token: 0x0400042D RID: 1069
	public const int gameSizeAA = 2;

	// Token: 0x0400042E RID: 1070
	public const int gameSizeAAA = 3;

	// Token: 0x0400042F RID: 1071
	public const int grafikStudio1 = 4;

	// Token: 0x04000430 RID: 1072
	public const int grafikStudio2 = 5;

	// Token: 0x04000431 RID: 1073
	public const int grafikStudio3 = 6;

	// Token: 0x04000432 RID: 1074
	public const int grafikStudio4 = 7;

	// Token: 0x04000433 RID: 1075
	public const int grafikStudio5 = 8;

	// Token: 0x04000434 RID: 1076
	public const int grafikStudio6 = 9;

	// Token: 0x04000435 RID: 1077
	public const int soundStudio1 = 10;

	// Token: 0x04000436 RID: 1078
	public const int soundStudio2 = 11;

	// Token: 0x04000437 RID: 1079
	public const int soundStudio3 = 12;

	// Token: 0x04000438 RID: 1080
	public const int soundStudio4 = 13;

	// Token: 0x04000439 RID: 1081
	public const int soundStudio5 = 14;

	// Token: 0x0400043A RID: 1082
	public const int soundStudio6 = 15;

	// Token: 0x0400043B RID: 1083
	public const int motionCapture1 = 16;

	// Token: 0x0400043C RID: 1084
	public const int motionCapture2 = 17;

	// Token: 0x0400043D RID: 1085
	public const int motionCapture3 = 18;

	// Token: 0x0400043E RID: 1086
	public const int motionCapture4 = 19;

	// Token: 0x0400043F RID: 1087
	public const int motionCapture5 = 20;

	// Token: 0x04000440 RID: 1088
	public const int motionCapture6 = 21;

	// Token: 0x04000441 RID: 1089
	public const int gameplayStudio1 = 22;

	// Token: 0x04000442 RID: 1090
	public const int gameplayStudio2 = 23;

	// Token: 0x04000443 RID: 1091
	public const int gameplayStudio3 = 24;

	// Token: 0x04000444 RID: 1092
	public const int gameplayStudio4 = 25;

	// Token: 0x04000445 RID: 1093
	public const int gameplayStudio5 = 26;

	// Token: 0x04000446 RID: 1094
	public const int gameplayStudio6 = 27;

	// Token: 0x04000447 RID: 1095
	public const int room_qa = 28;

	// Token: 0x04000448 RID: 1096
	public const int room_support = 29;

	// Token: 0x04000449 RID: 1097
	public const int room_marketing = 30;

	// Token: 0x0400044A RID: 1098
	public const int room_grafikstudio = 31;

	// Token: 0x0400044B RID: 1099
	public const int room_soundstudio = 32;

	// Token: 0x0400044C RID: 1100
	public const int room_produktion = 33;

	// Token: 0x0400044D RID: 1101
	public const int room_training = 34;

	// Token: 0x0400044E RID: 1102
	public const int subgenre = 35;

	// Token: 0x0400044F RID: 1103
	public const int subtheme = 36;

	// Token: 0x04000450 RID: 1104
	public const int copyProtect = 37;

	// Token: 0x04000451 RID: 1105
	public const int room_werkstatt = 38;

	// Token: 0x04000452 RID: 1106
	public const int room_hardware = 39;
}
