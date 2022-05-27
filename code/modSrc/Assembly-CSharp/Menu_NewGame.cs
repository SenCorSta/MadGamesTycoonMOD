using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000176 RID: 374
public class Menu_NewGame : MonoBehaviour
{
	// Token: 0x06000DD3 RID: 3539 RVA: 0x00009A8B File Offset: 0x00007C8B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x000A3FD4 File Offset: 0x000A21D4
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

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000A40CC File Offset: 0x000A22CC
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

	// Token: 0x06000DD6 RID: 3542 RVA: 0x00009A93 File Offset: 0x00007C93
	public void Init()
	{
		this.InitDropdowns();
		this.SetLogo(this.logo);
		this.SetCountry(this.country);
		this.SetGenre(this.genre);
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x00009ABF File Offset: 0x00007CBF
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x000A4184 File Offset: 0x000A2384
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

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00009AE1 File Offset: 0x00007CE1
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

	// Token: 0x06000DDA RID: 3546 RVA: 0x00009AF0 File Offset: 0x00007CF0
	public void SetLogo(int i)
	{
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x000A44C4 File Offset: 0x000A26C4
	public void SetCountry(int i)
	{
		this.country = i;
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetCountry(i);
		this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.flagSprites[i];
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000A4518 File Offset: 0x000A2718
	public void SetGenre(int i)
	{
		this.genre = i;
		this.uiObjects[10].GetComponent<Text>().text = this.genres_.GetName(this.genre);
		this.uiObjects[11].GetComponent<Image>().sprite = this.genres_.GetPic(this.genre);
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x00009B17 File Offset: 0x00007D17
	public void BUTTON_Abbrechen()
	{
		this.SaveOptions();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00009B38 File Offset: 0x00007D38
	public void BUTTON_RandomCompanyName()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[0].GetComponent<InputField>().text = this.tS_.GetRandomCompanyName();
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x00009B64 File Offset: 0x00007D64
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00009B8C File Offset: 0x00007D8C
	public void BUTTON_Standort()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[161]);
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x00009BB7 File Offset: 0x00007DB7
	public void BUTTON_Genre()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[298]);
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000A4574 File Offset: 0x000A2774
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

	// Token: 0x06000DE3 RID: 3555 RVA: 0x000A4600 File Offset: 0x000A2800
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

	// Token: 0x0400127A RID: 4730
	public GameObject[] uiObjects;

	// Token: 0x0400127B RID: 4731
	private GameObject main_;

	// Token: 0x0400127C RID: 4732
	private mainScript mS_;

	// Token: 0x0400127D RID: 4733
	private textScript tS_;

	// Token: 0x0400127E RID: 4734
	private GUI_Main guiMain_;

	// Token: 0x0400127F RID: 4735
	private sfxScript sfx_;

	// Token: 0x04001280 RID: 4736
	private genres genres_;

	// Token: 0x04001281 RID: 4737
	private cameraMovementScript cmS_;

	// Token: 0x04001282 RID: 4738
	public int logo = -1;

	// Token: 0x04001283 RID: 4739
	public int country;

	// Token: 0x04001284 RID: 4740
	public int genre;
}
