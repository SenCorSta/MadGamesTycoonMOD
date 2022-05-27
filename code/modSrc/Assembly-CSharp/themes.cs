using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class themes : MonoBehaviour
{
	// Token: 0x060003E9 RID: 1001 RVA: 0x0000423A File Offset: 0x0000243A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000527B0 File Offset: 0x000509B0
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
		if (!this.genres_)
		{
			this.genres_ = base.GetComponent<genres>();
		}
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00004242 File Offset: 0x00002442
	public void Init()
	{
		this.FindScripts();
		this.InitArrays();
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00052824 File Offset: 0x00050A24
	private void InitArrays()
	{
		this.FindScripts();
		this.themes_RES_POINTS_LEFT = new float[this.tS_.themes_EN.Length];
		this.themes_LEVEL = new int[this.tS_.themes_EN.Length];
		this.themes_MARKT = new int[this.tS_.themes_EN.Length];
		for (int i = 0; i < this.themes_RES_POINTS_LEFT.Length; i++)
		{
			this.themes_RES_POINTS_LEFT[i] = (float)this.RES_POINTS;
			this.themes_LEVEL[i] = 0;
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000528AC File Offset: 0x00050AAC
	private string OpenFile(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000528E0 File Offset: 0x00050AE0
	public void Load_THEMES_MGSR(string filename)
	{
		Debug.Log("Load_THEMES_MGSR() genreAmount: " + this.genres_.genres_LEVEL.Length.ToString());
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		this.themes_MGSR = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains("<M1>"))
			{
				this.themes_MGSR[i] = 1;
			}
			if (array[i].Contains("<M2>"))
			{
				this.themes_MGSR[i] = 2;
			}
			if (array[i].Contains("<M3>"))
			{
				this.themes_MGSR[i] = 3;
			}
			if (array[i].Contains("<M4>"))
			{
				this.themes_MGSR[i] = 4;
			}
			if (array[i].Contains("<M5>"))
			{
				this.themes_MGSR[i] = 5;
			}
		}
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000529CC File Offset: 0x00050BCC
	public void Load_FITGENRE(string filename)
	{
		int num = this.genres_.genres_LEVEL.Length;
		Debug.Log("Load_FITGENRE() genreAmount: " + num.ToString());
		string[] array = this.OpenFile(filename).Split(new char[]
		{
			"\n"[0]
		});
		this.themes_FITGENRE = new bool[array.Length, num];
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < num; j++)
			{
				if (array[i].Contains("<" + j.ToString() + ">"))
				{
					this.themes_FITGENRE[i, j] = true;
				}
			}
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00004250 File Offset: 0x00002450
	public bool IsThemesFitWithGenre(int theme_, int genre_)
	{
		return theme_ >= 0 && genre_ >= 0 && this.themes_FITGENRE[theme_, genre_];
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00004269 File Offset: 0x00002469
	public int GetPrice(int i)
	{
		return this.PRICE;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00004271 File Offset: 0x00002471
	public bool IsErforscht(int i)
	{
		return this.themes_RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00004285 File Offset: 0x00002485
	public float GetProzent(int i)
	{
		return 100f / (float)this.RES_POINTS * ((float)this.RES_POINTS - this.themes_RES_POINTS_LEFT[i]);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000042A5 File Offset: 0x000024A5
	public bool ForschungGestartet(int i)
	{
		return this.themes_RES_POINTS_LEFT[i] != (float)this.RES_POINTS;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000042BB File Offset: 0x000024BB
	public bool Pay(int i)
	{
		if (!this.ForschungGestartet(i))
		{
			if (this.mS_.NotEnoughMoney(this.PRICE))
			{
				return false;
			}
			this.mS_.Pay((long)this.PRICE, 2);
		}
		return true;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00052A78 File Offset: 0x00050C78
	public bool BereitsInAnderenRaumAktiv(int s)
	{
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component.typ == 2 && component.taskGameObject)
				{
					taskForschung taskForschung = component.GetTaskForschung();
					if (taskForschung && taskForschung.slot == s && taskForschung.typ == 1)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x000042EF File Offset: 0x000024EF
	public string GetTooltip(int i)
	{
		return "<b>" + this.tS_.GetThemes(i) + "</b>";
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00052B00 File Offset: 0x00050D00
	public Sprite GetSpriteMarkt(int i)
	{
		int num = this.themes_MARKT[i];
		if (num <= 2)
		{
			return this.spriteMarkt[0];
		}
		if (num > 2 && num <= 4)
		{
			return this.spriteMarkt[1];
		}
		if (num > 4)
		{
			return this.spriteMarkt[2];
		}
		return this.spriteMarkt[0];
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00052B4C File Offset: 0x00050D4C
	public void UnlockAll()
	{
		for (int i = 0; i < this.themes_RES_POINTS_LEFT.Length; i++)
		{
			this.themes_RES_POINTS_LEFT[i] = 0f;
		}
	}

	// Token: 0x04000761 RID: 1889
	private mainScript mS_;

	// Token: 0x04000762 RID: 1890
	private textScript tS_;

	// Token: 0x04000763 RID: 1891
	private settingsScript settings_;

	// Token: 0x04000764 RID: 1892
	private genres genres_;

	// Token: 0x04000765 RID: 1893
	public Sprite icon;

	// Token: 0x04000766 RID: 1894
	public int RES_POINTS;

	// Token: 0x04000767 RID: 1895
	public int PRICE;

	// Token: 0x04000768 RID: 1896
	public Sprite[] spriteMarkt;

	// Token: 0x04000769 RID: 1897
	public float[] themes_RES_POINTS_LEFT;

	// Token: 0x0400076A RID: 1898
	public int[] themes_LEVEL;

	// Token: 0x0400076B RID: 1899
	public bool[,] themes_FITGENRE;

	// Token: 0x0400076C RID: 1900
	public int[] themes_MGSR;

	// Token: 0x0400076D RID: 1901
	public int[] themes_MARKT;
}
