using System;
using UnityEngine;


public class roomDataScript : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public int GetPrice(int i)
	{
		return this.roomData_PRICE[i];
	}

	
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

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	public int[] roomData_PRICE;

	
	public Sprite[] roomData_SPRITE;

	
	public const int floor = 0;

	
	public const int entwicklung = 1;

	
	public const int forschung = 2;

	
	public const int qa = 3;

	
	public const int grafikstudio = 4;

	
	public const int soundstudio = 5;

	
	public const int marketing = 6;

	
	public const int support = 7;

	
	public const int hardware = 8;

	
	public const int lager = 9;

	
	public const int motion = 10;

	
	public const int wc = 11;

	
	public const int aufenthalt = 12;

	
	public const int training = 13;

	
	public const int produktion = 14;

	
	public const int server = 15;

	
	public const int free = 16;

	
	public const int werkstatt = 17;
}
