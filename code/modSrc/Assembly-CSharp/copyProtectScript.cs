using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class copyProtectScript : MonoBehaviour
{
	// Token: 0x060001AE RID: 430 RVA: 0x00002FBC File Offset: 0x000011BC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0002F070 File Offset: 0x0002D270
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
		if (!this.settings_)
		{
			this.settings_ = this.main_.GetComponent<settingsScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00002FC4 File Offset: 0x000011C4
	public void Init()
	{
		base.name = "COPYPROTECT_" + this.myID.ToString();
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
	public string GetName()
	{
		string text;
		switch (this.settings_.language)
		{
		case 0:
			text = this.name_EN;
			goto IL_B6;
		case 1:
			text = this.name_GE;
			goto IL_B6;
		case 2:
			text = this.name_TU;
			goto IL_B6;
		case 3:
			text = this.name_CH;
			goto IL_B6;
		case 4:
			text = this.name_FR;
			goto IL_B6;
		case 9:
			text = this.name_RU;
			goto IL_B6;
		case 10:
			text = this.name_CT;
			goto IL_B6;
		case 14:
			text = this.name_IT;
			goto IL_B6;
		case 16:
			text = this.name_JA;
			goto IL_B6;
		}
		text = this.name_EN;
		IL_B6:
		if (text == null)
		{
			return this.name_EN;
		}
		if (text.Length <= 0)
		{
			return this.name_EN;
		}
		return text;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00002FE1 File Offset: 0x000011E1
	public int GetPrice()
	{
		return this.price;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00002FE9 File Offset: 0x000011E9
	public int GetDevCosts()
	{
		return this.dev_costs;
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00002FF1 File Offset: 0x000011F1
	public string GetDateString()
	{
		return this.date_year.ToString() + " " + this.tS_.GetText(this.date_month + 220);
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0002F1D4 File Offset: 0x0002D3D4
	public string GetTooltip()
	{
		string text = "<b>" + this.GetName() + "</b>";
		text = string.Concat(new object[]
		{
			text,
			"\n<color=magenta>",
			this.tS_.GetText(286),
			": ",
			this.mS_.Round(this.effekt, 2),
			"%</color>"
		});
		text += "\n";
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(218),
			": ",
			this.mS_.GetMoney((long)this.GetPrice(), true)
		});
		return string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(6),
			": ",
			this.mS_.GetMoney((long)this.GetDevCosts(), true)
		});
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000301F File Offset: 0x0000121F
	public bool IsVerfuegbar()
	{
		return this.isUnlocked;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000302C File Offset: 0x0000122C
	public void EffektVerschlechtern()
	{
		if (this.IsVerfuegbar() && !this.neverLooseEffect)
		{
			this.effekt -= 0.2f;
			if (this.effekt < 0f)
			{
				this.effekt = 0f;
			}
		}
	}

	// Token: 0x040003A8 RID: 936
	public GameObject main_;

	// Token: 0x040003A9 RID: 937
	public mainScript mS_;

	// Token: 0x040003AA RID: 938
	public settingsScript settings_;

	// Token: 0x040003AB RID: 939
	public textScript tS_;

	// Token: 0x040003AC RID: 940
	public int myID;

	// Token: 0x040003AD RID: 941
	public int date_year;

	// Token: 0x040003AE RID: 942
	public int date_month;

	// Token: 0x040003AF RID: 943
	public int price;

	// Token: 0x040003B0 RID: 944
	public int dev_costs;

	// Token: 0x040003B1 RID: 945
	public string name_EN = "";

	// Token: 0x040003B2 RID: 946
	public string name_GE = "";

	// Token: 0x040003B3 RID: 947
	public string name_TU = "";

	// Token: 0x040003B4 RID: 948
	public string name_CH = "";

	// Token: 0x040003B5 RID: 949
	public string name_FR = "";

	// Token: 0x040003B6 RID: 950
	public string name_CT = "";

	// Token: 0x040003B7 RID: 951
	public string name_RU = "";

	// Token: 0x040003B8 RID: 952
	public string name_IT = "";

	// Token: 0x040003B9 RID: 953
	public string name_JA = "";

	// Token: 0x040003BA RID: 954
	public bool isUnlocked;

	// Token: 0x040003BB RID: 955
	public bool inBesitz;

	// Token: 0x040003BC RID: 956
	public float effekt = 100f;

	// Token: 0x040003BD RID: 957
	public bool neverLooseEffect;
}
