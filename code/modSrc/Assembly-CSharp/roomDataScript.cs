using System;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class roomDataScript : MonoBehaviour
{
	// Token: 0x06001DFD RID: 7677 RVA: 0x0012B0FE File Offset: 0x001292FE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x0012B108 File Offset: 0x00129308
	private void FindScripts()
	{
		if (this.main_)
		{
			return;
		}
		this.main_ = GameObject.Find("Main");
		this.mS_ = this.main_.GetComponent<mainScript>();
		this.tS_ = this.main_.GetComponent<textScript>();
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x0012B155 File Offset: 0x00129355
	public int GetPrice(int i)
	{
		return this.roomData_PRICE[i];
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x0012B160 File Offset: 0x00129360
	public string GetName(int i)
	{
		switch (i)
		{
		case 1:
			return this.tS_.GetText(19);
		case 2:
			return this.tS_.GetText(20);
		case 3:
			return this.tS_.GetText(21);
		case 4:
			return this.tS_.GetText(22);
		case 5:
			return this.tS_.GetText(23);
		case 6:
			return this.tS_.GetText(24);
		case 7:
			return this.tS_.GetText(25);
		case 8:
			return this.tS_.GetText(26);
		case 9:
			return this.tS_.GetText(27);
		case 10:
			return this.tS_.GetText(28);
		case 11:
			return this.tS_.GetText(29);
		case 12:
			return this.tS_.GetText(30);
		case 13:
			return this.tS_.GetText(31);
		case 14:
			return this.tS_.GetText(32);
		case 15:
			return this.tS_.GetText(33);
		case 17:
			return this.tS_.GetText(1508);
		}
		return "<Missing>";
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x0012B2A8 File Offset: 0x001294A8
	public bool KeineMitarbeiter(int roomTyp)
	{
		bool result = false;
		if (roomTyp != 0)
		{
			switch (roomTyp)
			{
			case 9:
				result = true;
				break;
			case 11:
				result = true;
				break;
			case 12:
				result = true;
				break;
			case 14:
				result = true;
				break;
			case 15:
				result = true;
				break;
			case 16:
				result = true;
				break;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	// Token: 0x040025C2 RID: 9666
	private GameObject main_;

	// Token: 0x040025C3 RID: 9667
	private mainScript mS_;

	// Token: 0x040025C4 RID: 9668
	private textScript tS_;

	// Token: 0x040025C5 RID: 9669
	public int[] roomData_PRICE;

	// Token: 0x040025C6 RID: 9670
	public Sprite[] roomData_SPRITE;

	// Token: 0x040025C7 RID: 9671
	public const int floor = 0;

	// Token: 0x040025C8 RID: 9672
	public const int entwicklung = 1;

	// Token: 0x040025C9 RID: 9673
	public const int forschung = 2;

	// Token: 0x040025CA RID: 9674
	public const int qa = 3;

	// Token: 0x040025CB RID: 9675
	public const int grafikstudio = 4;

	// Token: 0x040025CC RID: 9676
	public const int soundstudio = 5;

	// Token: 0x040025CD RID: 9677
	public const int marketing = 6;

	// Token: 0x040025CE RID: 9678
	public const int support = 7;

	// Token: 0x040025CF RID: 9679
	public const int hardware = 8;

	// Token: 0x040025D0 RID: 9680
	public const int lager = 9;

	// Token: 0x040025D1 RID: 9681
	public const int motion = 10;

	// Token: 0x040025D2 RID: 9682
	public const int wc = 11;

	// Token: 0x040025D3 RID: 9683
	public const int aufenthalt = 12;

	// Token: 0x040025D4 RID: 9684
	public const int training = 13;

	// Token: 0x040025D5 RID: 9685
	public const int produktion = 14;

	// Token: 0x040025D6 RID: 9686
	public const int server = 15;

	// Token: 0x040025D7 RID: 9687
	public const int free = 16;

	// Token: 0x040025D8 RID: 9688
	public const int werkstatt = 17;
}
