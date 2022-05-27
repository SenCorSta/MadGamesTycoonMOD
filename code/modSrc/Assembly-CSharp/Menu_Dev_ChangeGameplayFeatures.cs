using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200012F RID: 303
public class Menu_Dev_ChangeGameplayFeatures : MonoBehaviour
{
	// Token: 0x06000ABA RID: 2746 RVA: 0x00007AA8 File Offset: 0x00005CA8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x000851E0 File Offset: 0x000833E0
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

	// Token: 0x06000ABC RID: 2748 RVA: 0x000853C4 File Offset: 0x000835C4
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

	// Token: 0x06000ABD RID: 2749 RVA: 0x00007AB0 File Offset: 0x00005CB0
	private void Update()
	{
		if (this.uiObjects[7].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[8].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x00007AE2 File Offset: 0x00005CE2
	private void CopyGameDate()
	{
		this.g_GameSize = this.gS_.gameSize;
		this.g_GameMainGenre = this.gS_.maingenre;
		this.g_GameGameplayFeatures = (bool[])this.gS_.gameGameplayFeatures.Clone();
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x00085430 File Offset: 0x00083630
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

	// Token: 0x06000AC0 RID: 2752 RVA: 0x00007B21 File Offset: 0x00005D21
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x000854E4 File Offset: 0x000836E4
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

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0008560C File Offset: 0x0008380C
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

	// Token: 0x06000AC3 RID: 2755 RVA: 0x000856D8 File Offset: 0x000838D8
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

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00085718 File Offset: 0x00083918
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

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00085794 File Offset: 0x00083994
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

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00085874 File Offset: 0x00083A74
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

	// Token: 0x06000AC7 RID: 2759 RVA: 0x00085A24 File Offset: 0x00083C24
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

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00085B60 File Offset: 0x00083D60
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

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00007B47 File Offset: 0x00005D47
	public bool SetGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = !this.g_GameGameplayFeatures[i];
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x00007B72 File Offset: 0x00005D72
	public bool DisableGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = false;
		this.CalcDevCosts();
		this.UpdateGesamtGameplayFeatures();
		return this.g_GameGameplayFeatures[i];
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x00085BD4 File Offset: 0x00083DD4
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

	// Token: 0x06000ACC RID: 2764 RVA: 0x00085C48 File Offset: 0x00083E48
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

	// Token: 0x06000ACD RID: 2765 RVA: 0x00085D08 File Offset: 0x00083F08
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

	// Token: 0x04000F0C RID: 3852
	public GameObject[] uiObjects;

	// Token: 0x04000F0D RID: 3853
	public GameObject[] uiPrefabs;

	// Token: 0x04000F0E RID: 3854
	private GameObject main_;

	// Token: 0x04000F0F RID: 3855
	private mainScript mS_;

	// Token: 0x04000F10 RID: 3856
	private textScript tS_;

	// Token: 0x04000F11 RID: 3857
	private GUI_Main guiMain_;

	// Token: 0x04000F12 RID: 3858
	private sfxScript sfx_;

	// Token: 0x04000F13 RID: 3859
	private genres genres_;

	// Token: 0x04000F14 RID: 3860
	private themes themes_;

	// Token: 0x04000F15 RID: 3861
	private licences licences_;

	// Token: 0x04000F16 RID: 3862
	private engineFeatures eF_;

	// Token: 0x04000F17 RID: 3863
	private cameraMovementScript cmS_;

	// Token: 0x04000F18 RID: 3864
	private unlockScript unlock_;

	// Token: 0x04000F19 RID: 3865
	private gameplayFeatures gF_;

	// Token: 0x04000F1A RID: 3866
	private games games_;

	// Token: 0x04000F1B RID: 3867
	private platforms platforms_;

	// Token: 0x04000F1C RID: 3868
	private Menu_DevGame menuDevGame_;

	// Token: 0x04000F1D RID: 3869
	public gameScript gS_;

	// Token: 0x04000F1E RID: 3870
	public int g_GameSize;

	// Token: 0x04000F1F RID: 3871
	public int g_GameMainGenre;

	// Token: 0x04000F20 RID: 3872
	public bool[] g_GameGameplayFeatures;

	// Token: 0x04000F21 RID: 3873
	private string searchStringA = "";

	// Token: 0x04000F22 RID: 3874
	private bool allFeatures;
}
