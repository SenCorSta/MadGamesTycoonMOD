using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class themes : MonoBehaviour
{
	// Token: 0x060003F1 RID: 1009 RVA: 0x0003E39C File Offset: 0x0003C59C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0003E3A4 File Offset: 0x0003C5A4
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

	// Token: 0x060003F3 RID: 1011 RVA: 0x0003E415 File Offset: 0x0003C615
	public void Init()
	{
		this.FindScripts();
		this.InitArrays();
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0003E424 File Offset: 0x0003C624
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

	// Token: 0x060003F5 RID: 1013 RVA: 0x0003E4AC File Offset: 0x0003C6AC
	private string OpenFile(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0003E4E0 File Offset: 0x0003C6E0
	public void Load_THEMES_MGSR(string filename)
	{
		int num = this.genres_.genres_LEVEL.Length;
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

	// Token: 0x060003F7 RID: 1015 RVA: 0x0003E5B4 File Offset: 0x0003C7B4
	public void Load_FITGENRE(string filename)
	{
		int num = this.genres_.genres_LEVEL.Length;
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

	// Token: 0x060003F8 RID: 1016 RVA: 0x0003E647 File Offset: 0x0003C847
	public bool IsThemesFitWithGenre(int theme_, int genre_)
	{
		return theme_ >= 0 && genre_ >= 0 && this.themes_FITGENRE[theme_, genre_];
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0003E660 File Offset: 0x0003C860
	public int GetPrice(int i)
	{
		return this.PRICE;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0003E668 File Offset: 0x0003C868
	public bool IsErforscht(int i)
	{
		return this.themes_RES_POINTS_LEFT[i] <= 0f;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x0003E67C File Offset: 0x0003C87C
	public float GetProzent(int i)
	{
		return 100f / (float)this.RES_POINTS * ((float)this.RES_POINTS - this.themes_RES_POINTS_LEFT[i]);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0003E69C File Offset: 0x0003C89C
	public bool ForschungGestartet(int i)
	{
		return this.themes_RES_POINTS_LEFT[i] != (float)this.RES_POINTS;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x0003E6B2 File Offset: 0x0003C8B2
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

	// Token: 0x060003FE RID: 1022 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
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

	// Token: 0x060003FF RID: 1023 RVA: 0x0003E76E File Offset: 0x0003C96E
	public string GetTooltip(int i)
	{
		return "<b>" + this.tS_.GetThemes(i) + "</b>";
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0003E78C File Offset: 0x0003C98C
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

	// Token: 0x06000401 RID: 1025 RVA: 0x0003E7D8 File Offset: 0x0003C9D8
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
