using System;
using System.IO;
using System.Text;
using UnityEngine;


public class licences : MonoBehaviour
{
	
	private void Awake()
	{
		this.FindScripts();
	}

	
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

	
	public void LoadLicences(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.data = text.Split(new char[]
		{
			"\n"[0]
		});
		Debug.Log("Licence Amount: " + this.data.Length.ToString());
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

	
	public void Sell(int i)
	{
		this.mS_.Earn((long)this.GetSellPrice(i), 2);
		this.licence_GEKAUFT[i] = 0;
	}

	
	public string GetName(int i)
	{
		return this.licence_EN[i];
	}

	
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

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	public string[] licence_EN;

	
	public int[] licence_TYP;

	
	public float[] licence_QUALITY;

	
	public int[] licence_ANGEBOT;

	
	public int[] licence_GEKAUFT;

	
	public Sprite[] licenceSprites;

	
	private string[] data;
}
