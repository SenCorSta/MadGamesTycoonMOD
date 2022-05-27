using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_ChangeGameplayFeatures : MonoBehaviour
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.platforms_)
		{
			this.platforms_ = this.main_.GetComponent<platforms>();
		}
		if (!this.menuDevGame_)
		{
			this.menuDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	
	public void Init(gameScript game_)
	{
		this.allFeatures = false;
		this.FindScripts();
		this.gS_ = game_;
		this.CopyGameDate();
		this.InitDropdowns_GameplayFeatures();
		this.Init_GameplayFeatures();
		this.uiObjects[0].GetComponent<Image>().sprite = this.games_.gameSizeSprites[this.gS_.gameSize];
		this.UpdateGesamtGameplayFeatures();
		this.CalcDevCosts();
	}

	
	private void Update()
	{
		if (this.uiObjects[7].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[8].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
	private void CopyGameDate()
	{
		this.g_GameSize = this.gS_.gameSize;
		this.g_GameMainGenre = this.gS_.maingenre;
		this.g_GameGameplayFeatures = (bool[])this.gS_.gameGameplayFeatures.Clone();
	}

	
	public void InitDropdowns_GameplayFeatures()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[2].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(413));
		list.Add(this.tS_.GetText(1438));
		this.uiObjects[2].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[2].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[2].GetComponent<Dropdown>().value = @int;
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public void BUTTON_Ok()
	{
		if (this.UpdateGesamtGameplayFeatures() > this.menuDevGame_.maxFeatures_gameSize[this.g_GameSize])
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1724), false);
			return;
		}
		int num = this.CalcDevCosts();
		this.mS_.Pay((long)num, 10);
		this.gS_.costs_entwicklung += (long)num;
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (!this.gS_.gameplayFeatures_DevDone[i] && !this.gS_.gameGameplayFeatures[i] && this.g_GameGameplayFeatures[i])
			{
				this.gS_.devPointsStart_Gesamt += (float)this.gF_.GetDevPoints(i);
				this.gS_.devPoints_Gesamt += (float)this.gF_.GetDevPoints(i);
			}
		}
		this.gS_.gameGameplayFeatures = (bool[])this.g_GameGameplayFeatures.Clone();
		if (this.gS_.devPoints <= 0f)
		{
			this.gS_.FindNextFeatureForDevelopment();
		}
		this.BUTTON_Close();
	}

	
	private int CalcDevCosts()
	{
		int num = 0;
		num += this.CalcDevCosts_Kontent();
		float num2 = this.GetPreisnachlass() * 100f;
		if (num2 > 0f)
		{
			this.tS_.GetText(624).Replace("<NUM>", this.mS_.GetMoney((long)num, true));
			this.uiObjects[6].GetComponent<Text>().text = string.Concat(new object[]
			{
				this.mS_.GetMoney((long)num, true),
				" (",
				Mathf.RoundToInt(100f - num2),
				"%)"
			});
			return num;
		}
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)num, true);
		return num;
	}

	
	private float GetPreisnachlass()
	{
		float num = 0f;
		if (this.gS_.typ_remaster)
		{
			num += 0.2f;
		}
		if (this.gS_.handy)
		{
			num += 0.25f;
		}
		return num;
	}

	
	private int CalcDevCosts_Kontent()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (this.g_GameGameplayFeatures[i] && !this.gS_.gameplayFeatures_DevDone[i] && !this.gS_.gameGameplayFeatures[i])
			{
				num += this.gF_.GetDevCosts(i);
			}
		}
		if (this.gS_.typ_remaster)
		{
			num /= 2;
		}
		if (this.gS_.handy)
		{
			num /= 4;
		}
		return num;
	}

	
	private int UpdateGesamtGameplayFeatures()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (this.g_GameGameplayFeatures[i])
			{
				num++;
			}
		}
		this.uiObjects[1].GetComponent<Text>().text = string.Concat(new string[]
		{
			this.tS_.GetText(410),
			": ",
			num.ToString(),
			" / ",
			this.menuDevGame_.maxFeatures_gameSize[this.g_GameSize].ToString()
		});
		if (num > this.menuDevGame_.maxFeatures_gameSize[this.g_GameSize])
		{
			this.uiObjects[1].GetComponent<Text>().color = Color.red;
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().color = Color.black;
		}
		return num;
	}

	
	private void Init_GameplayFeatures()
	{
		this.FindScripts();
		if (this.g_GameGameplayFeatures.Length == 0)
		{
			this.g_GameGameplayFeatures = new bool[this.gF_.gameplayFeatures_LEVEL.Length];
		}
		for (int i = 0; i < this.uiObjects[3].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[3].transform.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.gF_.gameplayFeatures_LEVEL.Length; j++)
		{
			if (this.gF_.IsErforscht(j))
			{
				string text = this.gF_.GetName(j);
				this.searchStringA = this.searchStringA.ToLower();
				text = text.ToLower();
				if (this.uiObjects[4].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA))
				{
					Item_DevGame_ChangeGameplayFeature component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[3].transform).GetComponent<Item_DevGame_ChangeGameplayFeature>();
					component.myID = j;
					component.gS_ = this.gS_;
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.sfx_ = this.sfx_;
					component.guiMain_ = this.guiMain_;
					component.gF_ = this.gF_;
					component.menu_ = this;
					component.SetGoodBadIcon();
					component.BUTTON_Click();
					component.BUTTON_Click();
				}
			}
		}
		this.DROPDOWN_SortGameplayFeatures();
		this.guiMain_.KeinEintrag(this.uiObjects[3], this.uiObjects[5]);
	}

	
	public void DROPDOWN_SortGameplayFeatures()
	{
		int value = this.uiObjects[2].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[2].name, value);
		int childCount = this.uiObjects[3].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[3].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevGame_ChangeGameplayFeature component = gameObject.GetComponent<Item_DevGame_ChangeGameplayFeature>();
				switch (value)
				{
				case 0:
					gameObject.name = this.gF_.GetName(component.myID);
					break;
				case 1:
					gameObject.name = this.gF_.GetDevCosts(component.myID).ToString();
					break;
				case 2:
					gameObject.name = this.gF_.gameplayFeatures_TYP[component.myID].ToString();
					break;
				case 3:
					gameObject.name = component.goodBad.ToString();
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[3]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[3]);
	}

	
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[3].transform.childCount; i++)
		{
			this.uiObjects[3].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[4].GetComponent<InputField>().text;
		this.Init_GameplayFeatures();
	}

	
	public bool SetGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = !this.g_GameGameplayFeatures[i];
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	
	public bool DisableGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = false;
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	
	public void BUTTON_AllGameplayFeatures()
	{
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			this.DisableAllGameplayFeatures();
			return;
		}
		for (int i = 0; i < this.uiObjects[3].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[3].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.GetComponent<Item_DevGame_ChangeGameplayFeature>().BUTTON_Click();
			}
		}
	}

	
	public void BUTTON_AllPassendenGameplayFeatures()
	{
		if (this.g_GameMainGenre < 0)
		{
			return;
		}
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			this.DisableAllGameplayFeatures();
			return;
		}
		for (int i = 0; i < this.uiObjects[3].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[3].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevGame_ChangeGameplayFeature component = gameObject.GetComponent<Item_DevGame_ChangeGameplayFeature>();
				if (this.gF_.gameplayFeatures_GOOD[component.myID, this.g_GameMainGenre] || !this.gF_.gameplayFeatures_BAD[component.myID, this.g_GameMainGenre])
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	
	public void DisableAllGameplayFeatures()
	{
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (!this.gS_.gameplayFeatures_DevDone[i] && !this.gS_.gameGameplayFeatures[i])
			{
				this.g_GameGameplayFeatures[i] = false;
			}
		}
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		this.sfx_.PlaySound(3, true);
	}

	
	public GameObject[] uiObjects;

	
	public GameObject[] uiPrefabs;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private themes themes_;

	
	private licences licences_;

	
	private engineFeatures eF_;

	
	private cameraMovementScript cmS_;

	
	private unlockScript unlock_;

	
	private gameplayFeatures gF_;

	
	private games games_;

	
	private platforms platforms_;

	
	private Menu_DevGame menuDevGame_;

	
	public gameScript gS_;

	
	public int g_GameSize;

	
	public int g_GameMainGenre;

	
	public bool[] g_GameGameplayFeatures;

	
	private string searchStringA = "";

	
	private bool allFeatures;
}
