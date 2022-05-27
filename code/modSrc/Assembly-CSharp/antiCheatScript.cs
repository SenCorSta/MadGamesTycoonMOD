using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class antiCheatScript : MonoBehaviour
{
	// Token: 0x06000184 RID: 388 RVA: 0x00002DBB File Offset: 0x00000FBB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0002E73C File Offset: 0x0002C93C
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

	// Token: 0x06000186 RID: 390 RVA: 0x00002DC3 File Offset: 0x00000FC3
	public void Init()
	{
		base.name = "ANTICHEAT_" + this.myID.ToString();
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
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

	// Token: 0x06000188 RID: 392 RVA: 0x00002DE0 File Offset: 0x00000FE0
	public int GetPrice()
	{
		return this.price;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00002DE8 File Offset: 0x00000FE8
	public int GetDevCosts()
	{
		return this.dev_costs;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00002DF0 File Offset: 0x00000FF0
	public string GetDateString()
	{
		return this.date_year.ToString() + " " + this.tS_.GetText(this.date_month + 220);
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
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

	// Token: 0x0600018C RID: 396 RVA: 0x00002E1E File Offset: 0x0000101E
	public bool IsVerfuegbar()
	{
		return this.isUnlocked;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00002E2B File Offset: 0x0000102B
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

	// Token: 0x04000370 RID: 880
	public GameObject main_;

	// Token: 0x04000371 RID: 881
	public mainScript mS_;

	// Token: 0x04000372 RID: 882
	public settingsScript settings_;

	// Token: 0x04000373 RID: 883
	public textScript tS_;

	// Token: 0x04000374 RID: 884
	public int myID;

	// Token: 0x04000375 RID: 885
	public int date_year;

	// Token: 0x04000376 RID: 886
	public int date_month;

	// Token: 0x04000377 RID: 887
	public int price;

	// Token: 0x04000378 RID: 888
	public int dev_costs;

	// Token: 0x04000379 RID: 889
	public string name_EN = "";

	// Token: 0x0400037A RID: 890
	public string name_GE = "";

	// Token: 0x0400037B RID: 891
	public string name_TU = "";

	// Token: 0x0400037C RID: 892
	public string name_CH = "";

	// Token: 0x0400037D RID: 893
	public string name_FR = "";

	// Token: 0x0400037E RID: 894
	public string name_CT = "";

	// Token: 0x0400037F RID: 895
	public string name_RU = "";

	// Token: 0x04000380 RID: 896
	public string name_IT = "";

	// Token: 0x04000381 RID: 897
	public string name_JA = "";

	// Token: 0x04000382 RID: 898
	public bool isUnlocked;

	// Token: 0x04000383 RID: 899
	public bool inBesitz;

	// Token: 0x04000384 RID: 900
	public float effekt = 100f;

	// Token: 0x04000385 RID: 901
	public bool neverLooseEffect;
}
