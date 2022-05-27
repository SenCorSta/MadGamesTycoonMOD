using System;
using UnityEngine;


public class publishingOffer : MonoBehaviour
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
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	
	public void Init()
	{
		base.name = "PUBOFFER_" + this.myID.ToString();
	}

	
	public void Delete()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	
	public string GetGameName()
	{
		return this.gameName;
	}

	
	public string GetDeveloperName()
	{
		GameObject gameObject = GameObject.Find("PUB_" + this.developer.ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<publisherScript>().GetName();
		}
		return "<missing>";
	}

	
	public Sprite GetDeveloperLogo()
	{
		GameObject gameObject = GameObject.Find("PUB_" + this.developer.ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<publisherScript>().GetLogo();
		}
		return null;
	}

	
	public Sprite GetScreenshot()
	{
		return this.genres_.GetScreenshot(this.genre, Mathf.RoundToInt(this.points_grafik));
	}

	
	public platformScript GetPlattformScript(int i)
	{
		GameObject gameObject = GameObject.Find("PLATFORM_" + this.gamePlatform[i].ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<platformScript>();
		}
		return null;
	}

	
	public string GetRetailDigitalString()
	{
		if (this.tS_)
		{
			if (this.retail && this.digital)
			{
				return this.tS_.GetText(1746);
			}
			if (this.retail && !this.digital)
			{
				return this.tS_.GetText(1747);
			}
			if (!this.retail && this.digital)
			{
				return this.tS_.GetText(1748);
			}
		}
		return "<missing>";
	}

	
	public string GetTooltip()
	{
		string text = "<b>" + this.GetGameName() + "</b>";
		text = text + "\n<color=black>" + this.GetDeveloperName() + "</color>";
		text = text + "\n<color=magenta>" + this.GetRetailDigitalString() + "</color>";
		if (this.gamePlatform[0] > 0)
		{
			text = text + "\n<color=blue>" + this.GetPlattformScript(0).GetName() + "</color>";
		}
		if (this.gamePlatform[1] > 0)
		{
			text = text + "\n<color=blue>" + this.GetPlattformScript(1).GetName() + "</color>";
		}
		if (this.gamePlatform[2] > 0)
		{
			text = text + "\n<color=blue>" + this.GetPlattformScript(2).GetName() + "</color>";
		}
		if (this.gamePlatform[3] > 0)
		{
			text = text + "\n<color=blue>" + this.GetPlattformScript(3).GetName() + "</color>";
		}
		text += "\n\n";
		text = text + this.genres_.GetName(this.genre) + "\n";
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(327),
			": <color=blue>",
			this.tS_.GetText(330 + this.gameSize - 1),
			"</color>\n"
		});
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(1730),
			": <color=blue>",
			this.mS_.GetMoney((long)this.GetGarantiesumme(), true),
			"</color>\n"
		});
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(1731),
			": <color=blue>",
			this.GetGewinnbeteiligung().ToString(),
			"%</color>\n"
		});
		text += "\n";
		int i = Mathf.RoundToInt(this.review / 20f);
		text = string.Concat(new string[]
		{
			text,
			this.tS_.GetText(1732),
			"\n<size=21>",
			this.GetQualitatStars(i),
			"</size>\n\n"
		});
		text = text + this.tS_.GetText(1736) + "\n";
		if (this.stimmung < 33f)
		{
			text = text + "<color=red><b>" + this.tS_.GetText(1740) + "</b></color>";
		}
		if (this.stimmung > 33f && this.stimmung < 66f)
		{
			text = text + "<color=orange><b>" + this.tS_.GetText(1741) + "</b></color>";
		}
		if (this.stimmung > 66f)
		{
			text = text + "<color=green><b>" + this.tS_.GetText(1742) + "</b></color>";
		}
		return text;
	}

	
	private string GetQualitatStars(int i)
	{
		string result;
		switch (i)
		{
		case 0:
			result = "☆☆☆☆☆";
			break;
		case 1:
			result = "★☆☆☆☆";
			break;
		case 2:
			result = "★★☆☆☆";
			break;
		case 3:
			result = "★★★☆☆";
			break;
		case 4:
			result = "★★★★☆";
			break;
		case 5:
			result = "★★★★★";
			break;
		default:
			result = "☆☆☆☆☆";
			break;
		}
		return result;
	}

	
	public int GetGarantiesumme()
	{
		float num = this.verhandlungProzent;
		num *= 0.01f;
		return Mathf.RoundToInt((float)this.garantiesumme * num);
	}

	
	public int GetGewinnbeteiligung()
	{
		float num = this.verhandlungProzent;
		num *= 0.01f;
		return Mathf.RoundToInt(this.gewinnbeteiligung * num);
	}

	
	public GameObject main_;

	
	public mainScript mS_;

	
	public textScript tS_;

	
	public genres genres_;

	
	public int myID;

	
	public int garantiesumme;

	
	public float gewinnbeteiligung;

	
	public int developer = -1;

	
	public int wochenAlsAngebot;

	
	public float review;

	
	public string gameName = "";

	
	public int genre;

	
	public int gameSize;

	
	public int[] gamePlatform;

	
	public float points_grafik;

	
	public float verhandlung;

	
	public float verhandlungProzent = 100f;

	
	public float stimmung = 100f;

	
	public int gameVorbild = -1;

	
	public bool retail;

	
	public bool digital;

	
	public bool angebotWoche;

	
	public bool nachfolger;
}
