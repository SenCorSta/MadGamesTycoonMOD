﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000177 RID: 375
public class Menu_NewGame : MonoBehaviour
{
	// Token: 0x06000DEB RID: 3563 RVA: 0x00095DDF File Offset: 0x00093FDF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00095DE8 File Offset: 0x00093FE8
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
		}
		if (!this.main_)
		{
			return;
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
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.cmS_)
		{
			this.cmS_ = GameObject.Find("CamMovement").GetComponent<cameraMovementScript>();
		}
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00095EE0 File Offset: 0x000940E0
	private void OnEnable()
	{
		this.FindScripts();
		if (this.logo == -1)
		{
			this.logo = UnityEngine.Random.Range(0, 30);
			if (PlayerPrefs.HasKey("optLogo"))
			{
				this.logo = PlayerPrefs.GetInt("optLogo");
			}
		}
		if (PlayerPrefs.HasKey("optCountry"))
		{
			this.country = PlayerPrefs.GetInt("optCountry");
		}
		if (PlayerPrefs.HasKey("optGenre"))
		{
			this.genre = PlayerPrefs.GetInt("optGenre");
		}
		if (PlayerPrefs.HasKey("CompanyName"))
		{
			this.uiObjects[0].GetComponent<InputField>().text = PlayerPrefs.GetString("CompanyName");
		}
		this.Init();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00095F98 File Offset: 0x00094198
	public void Init()
	{
		this.InitDropdowns();
		this.SetLogo(this.logo);
		this.SetCountry(this.country);
		this.SetGenre(this.genre);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00095FC4 File Offset: 0x000941C4
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00095FE8 File Offset: 0x000941E8
	public void InitDropdowns()
	{
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(802));
		list.Add(this.tS_.GetText(803));
		list.Add(this.tS_.GetText(804));
		list.Add(this.tS_.GetText(805));
		list.Add(this.tS_.GetText(1685));
		list.Add(this.tS_.GetText(806));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = 2;
		if (PlayerPrefs.HasKey("optDifficulty"))
		{
			this.uiObjects[1].GetComponent<Dropdown>().value = PlayerPrefs.GetInt("optDifficulty");
		}
		list = new List<string>();
		list.Add("1976");
		list.Add("1985");
		list.Add("1995");
		list.Add("2005");
		list.Add("2015");
		this.uiObjects[2].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[2].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[2].GetComponent<Dropdown>().value = 0;
		if (PlayerPrefs.HasKey("optYear"))
		{
			this.uiObjects[2].GetComponent<Dropdown>().value = PlayerPrefs.GetInt("optYear");
		}
		list = new List<string>();
		list.Add(this.tS_.GetText(1335));
		list.Add(this.tS_.GetText(807));
		list.Add(this.tS_.GetText(808));
		list.Add(this.tS_.GetText(809));
		list.Add(this.tS_.GetText(810));
		this.uiObjects[3].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[3].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[3].GetComponent<Dropdown>().value = 2;
		if (PlayerPrefs.HasKey("optSpeed"))
		{
			this.uiObjects[3].GetComponent<Dropdown>().value = PlayerPrefs.GetInt("optSpeed");
		}
		list = new List<string>();
		list.Add(this.tS_.GetText(1770) + " " + this.tS_.GetText(1772));
		list.Add(this.tS_.GetText(1771));
		list.Add(this.tS_.GetText(2012));
		list.Add(this.tS_.GetText(1773));
		this.uiObjects[8].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[8].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[8].GetComponent<Dropdown>().value = 0;
		if (PlayerPrefs.HasKey("optMap"))
		{
			this.uiObjects[8].GetComponent<Dropdown>().value = PlayerPrefs.GetInt("optMap");
		}
		base.StartCoroutine(this.DropdownAfterOneFrame());
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x0009633B File Offset: 0x0009453B
	public IEnumerator DropdownAfterOneFrame()
	{
		yield return new WaitForEndOfFrame();
		List<string> list = new List<string>();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		list = new List<string>();
		list.Add("20");
		list.Add("40");
		list.Add("60");
		list.Add("80");
		list.Add(array.Length.ToString());
		this.uiObjects[7].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[7].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[7].GetComponent<Dropdown>().value = 4;
		if (PlayerPrefs.HasKey("optOpponent"))
		{
			this.uiObjects[7].GetComponent<Dropdown>().value = PlayerPrefs.GetInt("optOpponent");
		}
		yield break;
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x0009634A File Offset: 0x0009454A
	public void SetLogo(int i)
	{
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00096374 File Offset: 0x00094574
	public void SetCountry(int i)
	{
		this.country = i;
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetCountry(i);
		this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.flagSprites[i];
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x000963C8 File Offset: 0x000945C8
	public void SetGenre(int i)
	{
		this.genre = i;
		this.uiObjects[10].GetComponent<Text>().text = this.genres_.GetName(this.genre);
		this.uiObjects[11].GetComponent<Image>().sprite = this.genres_.GetPic(this.genre);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x00096424 File Offset: 0x00094624
	public void BUTTON_Abbrechen()
	{
		this.SaveOptions();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x00096445 File Offset: 0x00094645
	public void BUTTON_RandomCompanyName()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[0].GetComponent<InputField>().text = this.tS_.GetRandomCompanyName();
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00096471 File Offset: 0x00094671
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x00096499 File Offset: 0x00094699
	public void BUTTON_Standort()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[161]);
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x000964C4 File Offset: 0x000946C4
	public void BUTTON_Genre()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[298]);
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x000964F0 File Offset: 0x000946F0
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(814), false);
			return;
		}
		this.mS_.settings_arbeitsgeschwindigkeitAnpassen = this.uiObjects[9].GetComponent<Toggle>().isOn;
		this.SaveOptions();
		this.guiMain_.uiObjects[162].SetActive(true);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x0009657C File Offset: 0x0009477C
	private void SaveOptions()
	{
		PlayerPrefs.SetInt("optDifficulty", this.uiObjects[1].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("optYear", this.uiObjects[2].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("optSpeed", this.uiObjects[3].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("optMap", this.uiObjects[8].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("optOpponent", this.uiObjects[7].GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("optLogo", this.logo);
		PlayerPrefs.SetInt("optCountry", this.country);
		PlayerPrefs.SetInt("optGenre", this.genre);
		PlayerPrefs.SetString("CompanyName", this.uiObjects[0].GetComponent<InputField>().text);
	}

	// Token: 0x04001282 RID: 4738
	public GameObject[] uiObjects;

	// Token: 0x04001283 RID: 4739
	private GameObject main_;

	// Token: 0x04001284 RID: 4740
	private mainScript mS_;

	// Token: 0x04001285 RID: 4741
	private textScript tS_;

	// Token: 0x04001286 RID: 4742
	private GUI_Main guiMain_;

	// Token: 0x04001287 RID: 4743
	private sfxScript sfx_;

	// Token: 0x04001288 RID: 4744
	private genres genres_;

	// Token: 0x04001289 RID: 4745
	private cameraMovementScript cmS_;

	// Token: 0x0400128A RID: 4746
	public int logo = -1;

	// Token: 0x0400128B RID: 4747
	public int country;

	// Token: 0x0400128C RID: 4748
	public int genre;
}
