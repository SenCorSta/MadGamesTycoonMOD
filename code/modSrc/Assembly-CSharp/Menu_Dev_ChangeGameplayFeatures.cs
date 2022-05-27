using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000130 RID: 304
public class Menu_Dev_ChangeGameplayFeatures : MonoBehaviour
{
	// Token: 0x06000ACB RID: 2763 RVA: 0x00074D56 File Offset: 0x00072F56
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x00074D60 File Offset: 0x00072F60
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

	// Token: 0x06000ACD RID: 2765 RVA: 0x00074F44 File Offset: 0x00073144
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

	// Token: 0x06000ACE RID: 2766 RVA: 0x00074FAE File Offset: 0x000731AE
	private void Update()
	{
		if (this.uiObjects[7].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[8].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00074FE0 File Offset: 0x000731E0
	private void CopyGameDate()
	{
		this.g_GameSize = this.gS_.gameSize;
		this.g_GameMainGenre = this.gS_.maingenre;
		this.g_GameGameplayFeatures = (bool[])this.gS_.gameGameplayFeatures.Clone();
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00075020 File Offset: 0x00073220
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

	// Token: 0x06000AD1 RID: 2769 RVA: 0x000750D2 File Offset: 0x000732D2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x000750F8 File Offset: 0x000732F8
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

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00075220 File Offset: 0x00073420
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

	// Token: 0x06000AD4 RID: 2772 RVA: 0x000752EC File Offset: 0x000734EC
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

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0007532C File Offset: 0x0007352C
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

	// Token: 0x06000AD6 RID: 2774 RVA: 0x000753A8 File Offset: 0x000735A8
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

	// Token: 0x06000AD7 RID: 2775 RVA: 0x00075488 File Offset: 0x00073688
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

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00075638 File Offset: 0x00073838
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

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00075774 File Offset: 0x00073974
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

	// Token: 0x06000ADA RID: 2778 RVA: 0x000757E8 File Offset: 0x000739E8
	public bool SetGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = !this.g_GameGameplayFeatures[i];
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00075813 File Offset: 0x00073A13
	public bool DisableGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = false;
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00075834 File Offset: 0x00073A34
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

	// Token: 0x06000ADD RID: 2781 RVA: 0x000758A8 File Offset: 0x00073AA8
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

	// Token: 0x06000ADE RID: 2782 RVA: 0x00075968 File Offset: 0x00073B68
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

	// Token: 0x04000F14 RID: 3860
	public GameObject[] uiObjects;

	// Token: 0x04000F15 RID: 3861
	public GameObject[] uiPrefabs;

	// Token: 0x04000F16 RID: 3862
	private GameObject main_;

	// Token: 0x04000F17 RID: 3863
	private mainScript mS_;

	// Token: 0x04000F18 RID: 3864
	private textScript tS_;

	// Token: 0x04000F19 RID: 3865
	private GUI_Main guiMain_;

	// Token: 0x04000F1A RID: 3866
	private sfxScript sfx_;

	// Token: 0x04000F1B RID: 3867
	private genres genres_;

	// Token: 0x04000F1C RID: 3868
	private themes themes_;

	// Token: 0x04000F1D RID: 3869
	private licences licences_;

	// Token: 0x04000F1E RID: 3870
	private engineFeatures eF_;

	// Token: 0x04000F1F RID: 3871
	private cameraMovementScript cmS_;

	// Token: 0x04000F20 RID: 3872
	private unlockScript unlock_;

	// Token: 0x04000F21 RID: 3873
	private gameplayFeatures gF_;

	// Token: 0x04000F22 RID: 3874
	private games games_;

	// Token: 0x04000F23 RID: 3875
	private platforms platforms_;

	// Token: 0x04000F24 RID: 3876
	private Menu_DevGame menuDevGame_;

	// Token: 0x04000F25 RID: 3877
	public gameScript gS_;

	// Token: 0x04000F26 RID: 3878
	public int g_GameSize;

	// Token: 0x04000F27 RID: 3879
	public int g_GameMainGenre;

	// Token: 0x04000F28 RID: 3880
	public bool[] g_GameGameplayFeatures;

	// Token: 0x04000F29 RID: 3881
	private string searchStringA = "";

	// Token: 0x04000F2A RID: 3882
	private bool allFeatures;
}
