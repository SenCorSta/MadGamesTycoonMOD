using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_NewGame : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	public void Init()
	{
		this.InitDropdowns();
		this.SetLogo(this.logo);
		this.SetCountry(this.country);
		this.SetGenre(this.genre);
	}

	
	private void OnDisable()
	{
		this.FindScripts();
		if (!this.cmS_)
		{
			return;
		}
		this.cmS_.disableMovement = false;
	}

	
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

	
	public void SetLogo(int i)
	{
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(i);
		this.logo = i;
	}

	
	public void SetCountry(int i)
	{
		this.country = i;
		this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetCountry(i);
		this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.flagSprites[i];
	}

	
	public void SetGenre(int i)
	{
		this.genre = i;
		this.uiObjects[10].GetComponent<Text>().text = this.genres_.GetName(this.genre);
		this.uiObjects[11].GetComponent<Image>().sprite = this.genres_.GetPic(this.genre);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.SaveOptions();
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_RandomCompanyName()
	{
		this.sfx_.PlaySound(3, true);
		this.uiObjects[0].GetComponent<InputField>().text = this.tS_.GetRandomCompanyName();
	}

	
	public void BUTTON_Firmenlogo()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[48]);
	}

	
	public void BUTTON_Standort()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[161]);
	}

	
	public void BUTTON_Genre()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[298]);
	}

	
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

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private cameraMovementScript cmS_;

	
	public int logo = -1;

	
	public int country;

	
	public int genre;
}
