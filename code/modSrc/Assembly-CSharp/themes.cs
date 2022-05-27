using System;
using System.IO;
using System.Text;
using UnityEngine;


public class themes : MonoBehaviour
{
	
	private void Start()
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
		if (!this.genres_)
		{
			this.genres_ = base.GetComponent<genres>();
		}
	}

	
	public void Init()
	{
		this.FindScripts();
		this.InitArrays();
	}

	
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

	
	private string OpenFile(string filename)
	{
		StreamReader streamReader = new StreamReader(Application.dataPath + "/Extern/Text/" + filename, Encoding.Unicode);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		return result;
	}

	
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

	
	public bool IsThemesFitWithGenre(int theme_, int genre_)
	{
		return theme_ >= 0 && genre_ >= 0 && this.themes_FITGENRE[theme_, genre_];
	}

	
	public int GetPrice(int i)
	{
		return this.PRICE;
	}

	
	public bool IsErforscht(int i)
	{
		return this.themes_RES_POINTS_LEFT[i] <= 0f;
	}

	
	public float GetProzent(int i)
	{
		return 100f / (float)this.RES_POINTS * ((float)this.RES_POINTS - this.themes_RES_POINTS_LEFT[i]);
	}

	
	public bool ForschungGestartet(int i)
	{
		return this.themes_RES_POINTS_LEFT[i] != (float)this.RES_POINTS;
	}

	
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

	
	public string GetTooltip(int i)
	{
		return "<b>" + this.tS_.GetThemes(i) + "</b>";
	}

	
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

	
	public void UnlockAll()
	{
		for (int i = 0; i < this.themes_RES_POINTS_LEFT.Length; i++)
		{
			this.themes_RES_POINTS_LEFT[i] = 0f;
		}
	}

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private settingsScript settings_;

	
	private genres genres_;

	
	public Sprite icon;

	
	public int RES_POINTS;

	
	public int PRICE;

	
	public Sprite[] spriteMarkt;

	
	public float[] themes_RES_POINTS_LEFT;

	
	public int[] themes_LEVEL;

	
	public bool[,] themes_FITGENRE;

	
	public int[] themes_MGSR;

	
	public int[] themes_MARKT;
}
