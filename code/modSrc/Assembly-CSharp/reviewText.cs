using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class reviewText : MonoBehaviour
{
	
	private void Awake()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		GameObject gameObject = GameObject.Find("Main");
		this.mS_ = gameObject.GetComponent<mainScript>();
		this.tS_ = gameObject.GetComponent<textScript>();
		this.settings_ = gameObject.GetComponent<settingsScript>();
	}

	
	public string GetReviewText(gameScript game_)
	{
		string str;
		switch (this.settings_.language)
		{
		case 0:
			str = "EN/Review_EN.txt";
			break;
		case 1:
			str = "GE/Review_GE.txt";
			break;
		case 2:
			str = "TU/Review_TU.txt";
			break;
		case 3:
			str = "CH/Review_CH.txt";
			break;
		case 4:
			str = "FR/Review_FR.txt";
			break;
		case 5:
			str = "ES/Review_ES.txt";
			break;
		case 6:
			str = "KO/Review_KO.txt";
			break;
		case 7:
			str = "PB/Review_PB.txt";
			break;
		case 8:
			str = "HU/Review_HU.txt";
			break;
		case 9:
			str = "RU/Review_RU.txt";
			break;
		case 10:
			str = "CT/Review_CT.txt";
			break;
		case 11:
			str = "PL/Review_PL.txt";
			break;
		case 12:
			str = "CZ/Review_CZ.txt";
			break;
		case 13:
			str = "AR/Review_AR.txt";
			break;
		case 14:
			str = "IT/Review_IT.txt";
			break;
		case 15:
			str = "RO/Review_RO.txt";
			break;
		case 16:
			str = "JA/Review_JA.txt";
			break;
		default:
			str = "EN/Review_EN.txt";
			break;
		}
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + str, Encoding.Unicode);
		string text = streamReader.ReadToEnd();
		streamReader.Close();
		this.data = text.Split(new char[]
		{
			"\n"[0]
		});
		int num = game_.reviewGameplay / 10 * 10 + 10;
		int num2 = game_.reviewGrafik / 10 * 10 + 10;
		int num3 = game_.reviewSound / 10 * 10 + 10;
		int num4 = game_.reviewSteuerung / 10 * 10 + 10;
		int num5 = game_.reviewTotal / 10 * 10 + 10;
		if (num > 100)
		{
			num = 100;
		}
		if (num2 > 100)
		{
			num2 = 100;
		}
		if (num3 > 100)
		{
			num3 = 100;
		}
		if (num4 > 100)
		{
			num4 = 100;
		}
		if (num5 > 100)
		{
			num5 = 100;
		}
		this.gameplayList.Clear();
		this.grafikList.Clear();
		this.soundList.Clear();
		this.steuerungList.Clear();
		this.totalList.Clear();
		for (int i = 0; i < this.data.Length; i++)
		{
			if (this.ParseData("[GAMEPLAY_" + num.ToString() + "]", i))
			{
				this.GetStrings(i, 0);
			}
			if (this.ParseData("[GRAPHIC_" + num2.ToString() + "]", i))
			{
				this.GetStrings(i, 1);
			}
			if (this.ParseData("[SOUND_" + num3.ToString() + "]", i))
			{
				this.GetStrings(i, 2);
			}
			if (this.ParseData("[CONTROL_" + num4.ToString() + "]", i))
			{
				this.GetStrings(i, 3);
			}
			if (this.ParseData("[TOTAL_" + num5.ToString() + "]", i))
			{
				this.GetStrings(i, 4);
			}
		}
		if (game_.reviewTotalText == -1)
		{
			game_.reviewGameplayText = UnityEngine.Random.Range(0, this.gameplayList.Count);
			game_.reviewGrafikText = UnityEngine.Random.Range(0, this.grafikList.Count);
			game_.reviewSoundText = UnityEngine.Random.Range(0, this.soundList.Count);
			game_.reviewSteuerungText = UnityEngine.Random.Range(0, this.steuerungList.Count);
			game_.reviewTotalText = UnityEngine.Random.Range(0, this.totalList.Count);
		}
		string text2 = "";
		if (game_.exklusiv)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + game_.gamePlatform[0].ToString());
			if (gameObject)
			{
				platformScript component = gameObject.GetComponent<platformScript>();
				if (component)
				{
					text2 = this.tS_.GetText(909) + " ";
					text2 = text2.Replace("<NAME>", component.GetName());
				}
			}
		}
		if (game_.retro)
		{
			GameObject gameObject2 = GameObject.Find("PLATFORM_" + game_.gamePlatform[0].ToString());
			if (gameObject2)
			{
				platformScript component2 = gameObject2.GetComponent<platformScript>();
				if (component2)
				{
					text2 = this.tS_.GetText(910) + " ";
					text2 = text2.Replace("<NAME>", component2.GetName());
				}
			}
		}
		if (game_.typ_nachfolger)
		{
			GameObject gameObject3 = GameObject.Find("GAME_" + game_.originalIP.ToString());
			if (gameObject3)
			{
				gameScript component3 = gameObject3.GetComponent<gameScript>();
				if (component3)
				{
					text2 = this.tS_.GetText(1539) + " ";
					text2 = text2.Replace("<NAME>", component3.GetNameWithTag());
				}
			}
		}
		if (game_.typ_spinoff)
		{
			GameObject gameObject4 = GameObject.Find("GAME_" + game_.originalIP.ToString());
			if (gameObject4)
			{
				gameScript component4 = gameObject4.GetComponent<gameScript>();
				if (component4)
				{
					text2 = this.tS_.GetText(1540) + " ";
					text2 = text2.Replace("<NAME>", component4.GetNameWithTag());
				}
			}
		}
		if (this.gameplayList.Count >= game_.reviewGameplayText)
		{
			text2 += this.gameplayList[game_.reviewGameplayText];
		}
		if (this.grafikList.Count >= game_.reviewGrafikText)
		{
			text2 = text2 + " " + this.grafikList[game_.reviewGrafikText];
		}
		if (this.soundList.Count >= game_.reviewSoundText)
		{
			text2 = text2 + " " + this.soundList[game_.reviewSoundText];
		}
		if (this.steuerungList.Count >= game_.reviewSteuerungText)
		{
			text2 = text2 + " " + this.steuerungList[game_.reviewSteuerungText];
		}
		if (game_.finanzierung_Grundkosten < 75 || game_.finanzierung_Kontent < 75 || game_.finanzierung_Technology < 75)
		{
			text2 = text2 + " " + this.tS_.GetText(1193);
		}
		return text2 + " " + this.totalList[game_.reviewTotalText];
	}

	
	private void GetStrings(int i, int what)
	{
		int num = i + 1;
		while (num < i + 100 && !this.ParseData("[END]", num))
		{
			switch (what)
			{
			case 0:
				this.gameplayList.Add(this.data[num]);
				break;
			case 1:
				this.grafikList.Add(this.data[num]);
				break;
			case 2:
				this.soundList.Add(this.data[num]);
				break;
			case 3:
				this.steuerungList.Add(this.data[num]);
				break;
			case 4:
				this.totalList.Add(this.data[num]);
				break;
			}
			num++;
		}
	}

	
	private bool ParseData(string c, int i)
	{
		return this.data[i].Contains(c);
	}

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private List<string> gameplayList = new List<string>();

	
	private List<string> grafikList = new List<string>();

	
	private List<string> soundList = new List<string>();

	
	private List<string> steuerungList = new List<string>();

	
	private List<string> totalList = new List<string>();

	
	private string[] data;
}
