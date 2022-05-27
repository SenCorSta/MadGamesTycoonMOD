using System;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class roomDataScript : MonoBehaviour
{
	// Token: 0x06001DA6 RID: 7590 RVA: 0x000141F9 File Offset: 0x000123F9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x0012C394 File Offset: 0x0012A594
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

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00014201 File Offset: 0x00012401
	public int GetPrice(int i)
	{
		return this.roomData_PRICE[i];
	}

	// Token: 0x06001DA9 RID: 7593 RVA: 0x0012C3E4 File Offset: 0x0012A5E4
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

	// Token: 0x06001DAA RID: 7594 RVA: 0x0012C52C File Offset: 0x0012A72C
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

	// Token: 0x040025AB RID: 9643
	private GameObject main_;

	// Token: 0x040025AC RID: 9644
	private mainScript mS_;

	// Token: 0x040025AD RID: 9645
	private textScript tS_;

	// Token: 0x040025AE RID: 9646
	public int[] roomData_PRICE;

	// Token: 0x040025AF RID: 9647
	public Sprite[] roomData_SPRITE;

	// Token: 0x040025B0 RID: 9648
	public const int floor = 0;

	// Token: 0x040025B1 RID: 9649
	public const int entwicklung = 1;

	// Token: 0x040025B2 RID: 9650
	public const int forschung = 2;

	// Token: 0x040025B3 RID: 9651
	public const int qa = 3;

	// Token: 0x040025B4 RID: 9652
	public const int grafikstudio = 4;

	// Token: 0x040025B5 RID: 9653
	public const int soundstudio = 5;

	// Token: 0x040025B6 RID: 9654
	public const int marketing = 6;

	// Token: 0x040025B7 RID: 9655
	public const int support = 7;

	// Token: 0x040025B8 RID: 9656
	public const int hardware = 8;

	// Token: 0x040025B9 RID: 9657
	public const int lager = 9;

	// Token: 0x040025BA RID: 9658
	public const int motion = 10;

	// Token: 0x040025BB RID: 9659
	public const int wc = 11;

	// Token: 0x040025BC RID: 9660
	public const int aufenthalt = 12;

	// Token: 0x040025BD RID: 9661
	public const int training = 13;

	// Token: 0x040025BE RID: 9662
	public const int produktion = 14;

	// Token: 0x040025BF RID: 9663
	public const int server = 15;

	// Token: 0x040025C0 RID: 9664
	public const int free = 16;

	// Token: 0x040025C1 RID: 9665
	public const int werkstatt = 17;
}
