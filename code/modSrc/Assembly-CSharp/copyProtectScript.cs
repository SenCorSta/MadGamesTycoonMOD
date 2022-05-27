using System;
using UnityEngine;


public class copyProtectScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void Init()
	{
		base.name = "COPYPROTECT_" + this.myID.ToString();
	}

	
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

	
	public int GetPrice()
	{
		return this.price;
	}

	
	public int GetDevCosts()
	{
		return this.dev_costs;
	}

	
	public string GetDateString()
	{
		return this.date_year.ToString() + " " + this.tS_.GetText(this.date_month + 220);
	}

	
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

	
	public bool IsVerfuegbar()
	{
		return this.isUnlocked;
	}

	
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

	
	public GameObject main_;

	
	public mainScript mS_;

	
	public settingsScript settings_;

	
	public textScript tS_;

	
	public int myID;

	
	public int date_year;

	
	public int date_month;

	
	public int price;

	
	public int dev_costs;

	
	public string name_EN = "";

	
	public string name_GE = "";

	
	public string name_TU = "";

	
	public string name_CH = "";

	
	public string name_FR = "";

	
	public string name_CT = "";

	
	public string name_RU = "";

	
	public string name_IT = "";

	
	public string name_JA = "";

	
	public bool isUnlocked;

	
	public bool inBesitz;

	
	public float effekt = 100f;

	
	public bool neverLooseEffect;
}
