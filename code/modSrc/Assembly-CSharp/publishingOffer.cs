using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class publishingOffer : MonoBehaviour
{
	// Token: 0x060003D4 RID: 980 RVA: 0x0000414F File Offset: 0x0000234F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00051B38 File Offset: 0x0004FD38
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

	// Token: 0x060003D6 RID: 982 RVA: 0x00004157 File Offset: 0x00002357
	public void Init()
	{
		base.name = "PUBOFFER_" + this.myID.ToString();
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00004174 File Offset: 0x00002374
	public void Delete()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00004181 File Offset: 0x00002381
	public string GetGameName()
	{
		return this.gameName;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00051BBC File Offset: 0x0004FDBC
	public string GetDeveloperName()
	{
		GameObject gameObject = GameObject.Find("PUB_" + this.developer.ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<publisherScript>().GetName();
		}
		return "<missing>";
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00051C00 File Offset: 0x0004FE00
	public Sprite GetDeveloperLogo()
	{
		GameObject gameObject = GameObject.Find("PUB_" + this.developer.ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<publisherScript>().GetLogo();
		}
		return null;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00004189 File Offset: 0x00002389
	public Sprite GetScreenshot()
	{
		return this.genres_.GetScreenshot(this.genre, Mathf.RoundToInt(this.points_grafik));
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00051C40 File Offset: 0x0004FE40
	public platformScript GetPlattformScript(int i)
	{
		GameObject gameObject = GameObject.Find("PLATFORM_" + this.gamePlatform[i].ToString());
		if (gameObject)
		{
			return gameObject.GetComponent<platformScript>();
		}
		return null;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00051C80 File Offset: 0x0004FE80
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

	// Token: 0x060003DE RID: 990 RVA: 0x00051D04 File Offset: 0x0004FF04
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

	// Token: 0x060003DF RID: 991 RVA: 0x0003D590 File Offset: 0x0003B790
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

	// Token: 0x060003E0 RID: 992 RVA: 0x0005200C File Offset: 0x0005020C
	public int GetGarantiesumme()
	{
		float num = this.verhandlungProzent;
		num *= 0.01f;
		return Mathf.RoundToInt((float)this.garantiesumme * num);
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00052038 File Offset: 0x00050238
	public int GetGewinnbeteiligung()
	{
		float num = this.verhandlungProzent;
		num *= 0.01f;
		return Mathf.RoundToInt(this.gewinnbeteiligung * num);
	}

	// Token: 0x04000741 RID: 1857
	public GameObject main_;

	// Token: 0x04000742 RID: 1858
	public mainScript mS_;

	// Token: 0x04000743 RID: 1859
	public textScript tS_;

	// Token: 0x04000744 RID: 1860
	public genres genres_;

	// Token: 0x04000745 RID: 1861
	public int myID;

	// Token: 0x04000746 RID: 1862
	public int garantiesumme;

	// Token: 0x04000747 RID: 1863
	public float gewinnbeteiligung;

	// Token: 0x04000748 RID: 1864
	public int developer = -1;

	// Token: 0x04000749 RID: 1865
	public int wochenAlsAngebot;

	// Token: 0x0400074A RID: 1866
	public float review;

	// Token: 0x0400074B RID: 1867
	public string gameName = "";

	// Token: 0x0400074C RID: 1868
	public int genre;

	// Token: 0x0400074D RID: 1869
	public int gameSize;

	// Token: 0x0400074E RID: 1870
	public int[] gamePlatform;

	// Token: 0x0400074F RID: 1871
	public float points_grafik;

	// Token: 0x04000750 RID: 1872
	public float verhandlung;

	// Token: 0x04000751 RID: 1873
	public float verhandlungProzent = 100f;

	// Token: 0x04000752 RID: 1874
	public float stimmung = 100f;

	// Token: 0x04000753 RID: 1875
	public int gameVorbild = -1;

	// Token: 0x04000754 RID: 1876
	public bool retail;

	// Token: 0x04000755 RID: 1877
	public bool digital;

	// Token: 0x04000756 RID: 1878
	public bool angebotWoche;

	// Token: 0x04000757 RID: 1879
	public bool nachfolger;
}
