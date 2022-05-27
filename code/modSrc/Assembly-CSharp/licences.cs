﻿using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class licences : MonoBehaviour
{
	// Token: 0x06000335 RID: 821 RVA: 0x0003100D File Offset: 0x0002F20D
	private void Awake()
	{
		this.FindScripts();
	}

	// Token: 0x06000336 RID: 822 RVA: 0x00031018 File Offset: 0x0002F218
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
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00031070 File Offset: 0x0002F270
	public void LoadLicences(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.data = text.Split(new char[]
		{
			"\n"[0]
		});
		int num = this.data.Length;
		this.licence_EN = new string[num];
		this.licence_TYP = new int[num];
		this.licence_QUALITY = new float[num];
		this.licence_ANGEBOT = new int[num];
		this.licence_GEKAUFT = new int[num];
		for (int i = 0; i < this.data.Length; i++)
		{
			this.licence_QUALITY[i] = UnityEngine.Random.Range(10f, 100f);
			if (this.ParseData("[MOVIE]", i))
			{
				this.licence_TYP[i] = 0;
			}
			if (this.ParseData("[BOOK]", i))
			{
				this.licence_TYP[i] = 1;
			}
			if (this.ParseData("[SPORT]", i))
			{
				this.licence_TYP[i] = 2;
			}
			if (this.ParseData("[1]", i))
			{
				this.licence_QUALITY[i] = 10f;
			}
			if (this.ParseData("[2]", i))
			{
				this.licence_QUALITY[i] = 20f;
			}
			if (this.ParseData("[3]", i))
			{
				this.licence_QUALITY[i] = 30f;
			}
			if (this.ParseData("[4]", i))
			{
				this.licence_QUALITY[i] = 40f;
			}
			if (this.ParseData("[5]", i))
			{
				this.licence_QUALITY[i] = 50f;
			}
			if (this.ParseData("[6]", i))
			{
				this.licence_QUALITY[i] = 60f;
			}
			if (this.ParseData("[7]", i))
			{
				this.licence_QUALITY[i] = 70f;
			}
			if (this.ParseData("[8]", i))
			{
				this.licence_QUALITY[i] = 80f;
			}
			if (this.ParseData("[9]", i))
			{
				this.licence_QUALITY[i] = 90f;
			}
			if (this.ParseData("[10]", i))
			{
				this.licence_QUALITY[i] = 100f;
			}
			this.licence_EN[i] = this.data[i];
		}
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0003129C File Offset: 0x0002F49C
	private bool ParseData(string c, int i)
	{
		if (this.data[i].Contains(c))
		{
			this.data[i] = this.data[i].Replace("\n", string.Empty);
			this.data[i] = this.data[i].Replace("\r", string.Empty);
			this.data[i] = this.data[i].Replace("\t", string.Empty);
			this.data[i] = this.data[i].Replace(c, "");
			string text = this.data[i];
			if (text[text.Length - 1] == ' ')
			{
				text = text.Remove(text.Length - 1);
				this.data[i] = text;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0003136C File Offset: 0x0002F56C
	public void Buy(int i)
	{
		this.mS_.Pay((long)this.GetPrice(i), 7);
		this.licence_GEKAUFT[i] = this.licence_ANGEBOT[i];
		this.licence_ANGEBOT[i] = 0;
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_BuyLizenz(i);
				return;
			}
			this.mS_.mpCalls_.SERVER_Send_Lizenz(i);
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x000313E8 File Offset: 0x0002F5E8
	public void Sell(int i)
	{
		this.mS_.Earn((long)this.GetSellPrice(i), 2);
		this.licence_GEKAUFT[i] = 0;
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00031407 File Offset: 0x0002F607
	public string GetName(int i)
	{
		return this.licence_EN[i];
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00031414 File Offset: 0x0002F614
	public int GetPrice(int i)
	{
		int num = 0;
		if (this.licence_QUALITY[i] >= 0f && this.licence_QUALITY[i] < 33f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 1500f * (float)this.licence_ANGEBOT[i]);
		}
		if (this.licence_QUALITY[i] >= 33f && this.licence_QUALITY[i] < 66f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 4000f * (float)this.licence_ANGEBOT[i]);
		}
		if (this.licence_QUALITY[i] >= 66f && this.licence_QUALITY[i] < 80f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 7000f * (float)this.licence_ANGEBOT[i]);
		}
		if (this.licence_QUALITY[i] >= 80f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 11000f * (float)this.licence_ANGEBOT[i]);
		}
		return num / 500 * 500;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00031514 File Offset: 0x0002F714
	public int GetSellPrice(int i)
	{
		int num = 0;
		if (this.licence_QUALITY[i] >= 0f && this.licence_QUALITY[i] < 33f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 1000f * (float)this.licence_GEKAUFT[i]);
		}
		if (this.licence_QUALITY[i] >= 33f && this.licence_QUALITY[i] < 66f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 3000f * (float)this.licence_GEKAUFT[i]);
		}
		if (this.licence_QUALITY[i] >= 66f && this.licence_QUALITY[i] < 80f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 6000f * (float)this.licence_GEKAUFT[i]);
		}
		if (this.licence_QUALITY[i] >= 80f)
		{
			num = Mathf.RoundToInt(this.licence_QUALITY[i] * 8000f * (float)this.licence_GEKAUFT[i]);
		}
		return num / 500 * 500;
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00031614 File Offset: 0x0002F814
	public string GetTypString(int i)
	{
		string result = "";
		switch (this.licence_TYP[i])
		{
		case 0:
			result = this.tS_.GetText(298);
			break;
		case 1:
			result = this.tS_.GetText(299);
			break;
		case 2:
			result = this.tS_.GetText(300);
			break;
		}
		return result;
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0003167C File Offset: 0x0002F87C
	public string GetTooltip(int i)
	{
		string text = "<b>" + this.GetName(i) + "</b>";
		text = text + "\n<color=magenta>" + this.GetTypString(i) + "</color>\n";
		if (this.GetPrice(i) > 0)
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(218),
				": ",
				this.mS_.GetMoney((long)this.GetPrice(i), true)
			});
		}
		else
		{
			text = string.Concat(new string[]
			{
				text,
				"\n",
				this.tS_.GetText(88),
				": ",
				this.mS_.GetMoney((long)this.GetSellPrice(i), true)
			});
		}
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(302),
			": ",
			Mathf.RoundToInt(this.licence_QUALITY[i]).ToString(),
			"%"
		});
		string text2 = this.tS_.GetText(301);
		if (this.licence_GEKAUFT[i] == 0)
		{
			text2 = text2.Replace("<NUM>", this.licence_ANGEBOT[i].ToString());
		}
		else
		{
			text2 = text2.Replace("<NUM>", this.licence_GEKAUFT[i].ToString());
		}
		return text + "\n\n<b>" + text2 + "</b>";
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00031814 File Offset: 0x0002FA14
	public void LizenzenUpdaten()
	{
		if (this.mS_.multiplayer && this.mS_.mpCalls_.isClient)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < this.licence_ANGEBOT.Length; i++)
		{
			if (this.licence_ANGEBOT[i] > 0)
			{
				switch (this.licence_TYP[i])
				{
				case 0:
					num++;
					break;
				case 1:
					num2++;
					break;
				case 2:
					num3++;
					break;
				}
				if (UnityEngine.Random.Range(0, 10) == 1)
				{
					this.licence_ANGEBOT[i] = 0;
					switch (this.licence_TYP[i])
					{
					case 0:
						num--;
						break;
					case 1:
						num2--;
						break;
					case 2:
						num3--;
						break;
					}
					if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
					{
						this.mS_.mpCalls_.SERVER_Send_Lizenz(i);
					}
				}
			}
		}
		if (this.mS_.globalEvent == 10)
		{
			return;
		}
		if (num < 20)
		{
			for (int j = 0; j < this.licence_ANGEBOT.Length; j++)
			{
				if (this.licence_TYP[j] == 0 && this.licence_ANGEBOT[j] == 0 && this.licence_GEKAUFT[j] == 0 && UnityEngine.Random.Range(0, 100) == 1)
				{
					this.licence_ANGEBOT[j] = UnityEngine.Random.Range(1, 6);
					num++;
					if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
					{
						this.mS_.mpCalls_.SERVER_Send_Lizenz(j);
					}
				}
				if (num >= 20)
				{
					break;
				}
			}
		}
		if (num2 < 5)
		{
			for (int k = 0; k < this.licence_ANGEBOT.Length; k++)
			{
				if (this.licence_TYP[k] == 1 && this.licence_ANGEBOT[k] == 0 && this.licence_GEKAUFT[k] == 0 && UnityEngine.Random.Range(0, 100) <= 2)
				{
					this.licence_ANGEBOT[k] = UnityEngine.Random.Range(1, 6);
					num2++;
					if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
					{
						this.mS_.mpCalls_.SERVER_Send_Lizenz(k);
					}
				}
				if (num2 >= 5)
				{
					break;
				}
			}
		}
		if (num3 < 5)
		{
			for (int l = 0; l < this.licence_ANGEBOT.Length; l++)
			{
				if (this.licence_TYP[l] == 2 && this.licence_ANGEBOT[l] == 0 && this.licence_GEKAUFT[l] == 0 && UnityEngine.Random.Range(0, 100) <= 2)
				{
					this.licence_ANGEBOT[l] = UnityEngine.Random.Range(1, 6);
					num3++;
					if (this.mS_.multiplayer && this.mS_.mpCalls_.isServer)
					{
						this.mS_.mpCalls_.SERVER_Send_Lizenz(l);
					}
				}
				if (num3 >= 5)
				{
					break;
				}
			}
		}
	}

	// Token: 0x0400065E RID: 1630
	private mainScript mS_;

	// Token: 0x0400065F RID: 1631
	private textScript tS_;

	// Token: 0x04000660 RID: 1632
	private settingsScript settings_;

	// Token: 0x04000661 RID: 1633
	public string[] licence_EN;

	// Token: 0x04000662 RID: 1634
	public int[] licence_TYP;

	// Token: 0x04000663 RID: 1635
	public float[] licence_QUALITY;

	// Token: 0x04000664 RID: 1636
	public int[] licence_ANGEBOT;

	// Token: 0x04000665 RID: 1637
	public int[] licence_GEKAUFT;

	// Token: 0x04000666 RID: 1638
	public Sprite[] licenceSprites;

	// Token: 0x04000667 RID: 1639
	private string[] data;
}
