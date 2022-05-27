using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_NewGameCEO : MonoBehaviour
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
		if (!this.cCS_)
		{
			this.cCS_ = this.main_.GetComponent<createCharScript>();
		}
		if (!this.cloth_)
		{
			this.cloth_ = this.main_.GetComponent<clothScript>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	private void Update()
	{
		this.s_skills = 100f;
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	
	public void Init()
	{
		this.InitDropdowns();
		if (this.uiObjects[12].GetComponent<InputField>().text.Length <= 0)
		{
			this.s_skills = 5000f;
			this.s_gamedesign = 15f;
			this.s_programmieren = 15f;
			this.s_grafik = 15f;
			this.s_sound = 15f;
			this.s_pr = 15f;
			this.s_gametests = 15f;
			this.s_technik = 15f;
			this.s_forschen = 15f;
			this.LoadData();
			this.uiObjects[23].GetComponent<Dropdown>().value = this.beruf;
			this.InitSkills();
			this.InitSlider();
			this.CreateChar();
			this.BUTTON_Perk(0);
		}
		if (this.male)
		{
			this.uiObjects[9].GetComponent<Image>().color = this.guiMain_.colors[0];
			this.uiObjects[10].GetComponent<Image>().color = Color.white;
		}
		else
		{
			this.uiObjects[9].GetComponent<Image>().color = Color.white;
			this.uiObjects[10].GetComponent<Image>().color = this.guiMain_.colors[0];
		}
		if (this.mS_.multiplayer)
		{
			this.uiObjects[12].GetComponent<InputField>().interactable = false;
			this.uiObjects[12].GetComponent<InputField>().text = this.mS_.playerName;
			return;
		}
		this.uiObjects[12].GetComponent<InputField>().interactable = true;
	}

	
	public void InitDropdowns()
	{
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(137));
		list.Add(this.tS_.GetText(138));
		list.Add(this.tS_.GetText(139));
		list.Add(this.tS_.GetText(140));
		list.Add(this.tS_.GetText(141));
		list.Add(this.tS_.GetText(142));
		list.Add(this.tS_.GetText(143));
		list.Add(this.tS_.GetText(144));
		this.uiObjects[23].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[23].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[23].GetComponent<Dropdown>().value = this.beruf;
	}

	
	private void InitSkills()
	{
		this.SetBalken(this.uiObjects[13], this.s_gamedesign, 0);
		this.SetBalken(this.uiObjects[14], this.s_programmieren, 1);
		this.SetBalken(this.uiObjects[15], this.s_grafik, 2);
		this.SetBalken(this.uiObjects[16], this.s_sound, 3);
		this.SetBalken(this.uiObjects[17], this.s_pr, 4);
		this.SetBalken(this.uiObjects[18], this.s_gametests, 5);
		this.SetBalken(this.uiObjects[19], this.s_technik, 6);
		this.SetBalken(this.uiObjects[20], this.s_forschen, 7);
		string text = this.tS_.GetText(830);
		text = text.Replace("<NUM>", Mathf.RoundToInt(this.s_skills).ToString());
		this.uiObjects[21].GetComponent<Text>().text = text;
	}

	
	private void InitSlider()
	{
		this.sliderEvent = false;
		if (this.male)
		{
			this.uiObjects[0].GetComponent<Slider>().maxValue = (float)(this.cCS_.charGfxMales.Length - 1);
		}
		else
		{
			this.uiObjects[0].GetComponent<Slider>().maxValue = (float)(this.cCS_.charGfxFemales.Length - 1);
		}
		if (this.body == -2)
		{
			this.body = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[0].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[0].GetComponent<Slider>().value = (float)this.body;
		this.body = Mathf.RoundToInt(this.uiObjects[0].GetComponent<Slider>().value);
		if (this.male)
		{
			this.uiObjects[1].GetComponent<Slider>().maxValue = (float)(this.cloth_.prefabMaleHairs.Length - 1);
		}
		else
		{
			this.uiObjects[1].GetComponent<Slider>().maxValue = (float)(this.cloth_.prefabFemaleHairs.Length - 1);
		}
		if (this.hair == -2)
		{
			this.hair = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[1].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[1].GetComponent<Slider>().value = (float)this.hair;
		this.hair = Mathf.RoundToInt(this.uiObjects[1].GetComponent<Slider>().value);
		if (this.male)
		{
			this.uiObjects[2].GetComponent<Slider>().maxValue = (float)(this.cloth_.prefabMaleEyes.Length - 1);
		}
		else
		{
			this.uiObjects[2].GetComponent<Slider>().maxValue = (float)(this.cloth_.prefabFemaleEyes.Length - 1);
		}
		if (this.eyes == -2)
		{
			this.eyes = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[2].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[2].GetComponent<Slider>().value = (float)this.eyes;
		this.eyes = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		if (this.male)
		{
			this.uiObjects[3].GetComponent<Slider>().interactable = true;
			this.uiObjects[3].GetComponent<Slider>().maxValue = (float)(this.cloth_.prefabBeards.Length - 1);
			if (this.beard == -2)
			{
				this.beard = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[3].GetComponent<Slider>().maxValue - 1f));
			}
			this.uiObjects[3].GetComponent<Slider>().value = (float)this.beard;
		}
		else
		{
			this.uiObjects[3].GetComponent<Slider>().interactable = false;
			this.uiObjects[3].GetComponent<Slider>().maxValue = 0f;
			this.beard = 0;
			this.uiObjects[3].GetComponent<Slider>().value = 0f;
		}
		this.uiObjects[4].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_Skin.Length - 1);
		if (this.colorSkin == -2)
		{
			this.colorSkin = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[4].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[4].GetComponent<Slider>().value = (float)this.colorSkin;
		if (this.male)
		{
			this.uiObjects[5].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_MaleHair.Length - 1);
		}
		else
		{
			this.uiObjects[5].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_FemaleHair.Length - 1);
		}
		if (this.colorHair == -2)
		{
			this.colorHair = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[5].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[5].GetComponent<Slider>().value = (float)this.colorHair;
		this.colorHair = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		if (this.male)
		{
			this.uiObjects[6].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_MaleShirt.Length - 1);
		}
		else
		{
			this.uiObjects[6].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_FemaleShirt.Length - 1);
		}
		if (this.colorShirt == -2)
		{
			this.colorShirt = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[6].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[6].GetComponent<Slider>().value = (float)this.colorShirt;
		this.colorShirt = Mathf.RoundToInt(this.uiObjects[6].GetComponent<Slider>().value);
		if (this.male)
		{
			this.uiObjects[7].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_MaleHose.Length - 1);
		}
		else
		{
			this.uiObjects[7].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_FemaleHose.Length - 1);
		}
		if (this.colorHose == -2)
		{
			this.colorHose = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[7].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[7].GetComponent<Slider>().value = (float)this.colorHose;
		this.colorHose = Mathf.RoundToInt(this.uiObjects[7].GetComponent<Slider>().value);
		this.uiObjects[8].GetComponent<Slider>().maxValue = (float)(this.cloth_.matColor_AllColors.Length - 1);
		if (this.colorAdd1 == -2)
		{
			this.colorAdd1 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, this.uiObjects[8].GetComponent<Slider>().maxValue - 1f));
		}
		this.uiObjects[8].GetComponent<Slider>().value = (float)this.colorAdd1;
		this.sliderEvent = true;
	}

	
	private void CreateChar()
	{
		if (this.character)
		{
			UnityEngine.Object.Destroy(this.character);
		}
		this.cS_ = this.mS_.CreatePlayer(this.male, this.body, this.eyes, this.hair, this.beard, this.colorSkin, this.colorHair, this.colorHair, this.colorHose, this.colorShirt, this.colorAdd1);
		this.character = this.cS_.gameObject.transform.GetChild(0).gameObject;
		this.character.name = "CHARNEWGAME";
		this.character.transform.SetParent(null);
		this.character.transform.position = new Vector3(0f, 0f, 0f);
		this.character.transform.eulerAngles = new Vector3(0f, this.camRotate, 0f);
		this.SetLayer(4, this.character.transform);
		this.character.GetComponent<Animator>().CrossFade("idle", 0.1f, 0, 0f, 0.4f);
		UnityEngine.Object.Destroy(this.cS_.gameObject);
	}

	
	private void SetLayer(int newLayer, Transform trans)
	{
		trans.gameObject.layer = newLayer;
		foreach (object obj in trans)
		{
			Transform transform = (Transform)obj;
			transform.gameObject.layer = newLayer;
			if (transform.childCount > 0)
			{
				this.SetLayer(newLayer, transform.transform);
			}
		}
	}

	
	public void SetBalken(GameObject go, float val, int beruf_)
	{
		go.transform.Find("Value").GetComponent<Text>().text = this.mS_.Round(val, 1).ToString();
		go.transform.Find("Fill").GetComponent<Image>().fillAmount = val * 0.01f;
		go.transform.Find("Fill").GetComponent<Image>().color = this.GetValColor(val);
		if (this.beruf == beruf_)
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 1f;
			return;
		}
		if (this.perks[15])
		{
			go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.6f;
			return;
		}
		go.transform.Find("FillMax").GetComponent<Image>().fillAmount = 0.5f;
	}

	
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	
	public void SLIDER_Body()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.body = Mathf.RoundToInt(this.uiObjects[0].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_Hair()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.hair = Mathf.RoundToInt(this.uiObjects[1].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_Eyes()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.eyes = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_Beard()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.beard = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_ColorSkin()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorSkin = Mathf.RoundToInt(this.uiObjects[4].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_ColorHair()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorHair = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_ColorShirt()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorShirt = Mathf.RoundToInt(this.uiObjects[6].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_ColorHose()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorHose = Mathf.RoundToInt(this.uiObjects[7].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_ColorAdd1()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorAdd1 = Mathf.RoundToInt(this.uiObjects[8].GetComponent<Slider>().value);
		this.CreateChar();
	}

	
	public void SLIDER_CamRotate()
	{
		this.camRotate = this.uiObjects[11].GetComponent<Slider>().value;
		if (this.character)
		{
			this.character.transform.eulerAngles = new Vector3(0f, this.camRotate, 0f);
		}
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().BUTTON_Close();
		}
	}

	
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.uiObjects[12].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(824), false);
			return;
		}
		int num = 0;
		for (int i = 0; i < this.perks.Length; i++)
		{
			if (this.perks[i])
			{
				num++;
			}
		}
		if (num < 5)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1056), false);
			return;
		}
		this.SaveData();
		this.guiMain_.uiObjects[163].SetActive(true);
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[163].GetComponent<Menu_NewGameSettings>().BUTTON_OK();
		}
	}

	
	private void SaveData()
	{
		PlayerPrefs.SetString("PlayerName", this.uiObjects[12].GetComponent<InputField>().text);
		PlayerPrefs.SetInt("setup_beruf", this.beruf);
		PlayerPrefs.SetFloat("setup_s_skills", this.s_skills);
		PlayerPrefs.SetFloat("setup_s_gamedesign", this.s_gamedesign);
		PlayerPrefs.SetFloat("setup_s_programmieren", this.s_programmieren);
		PlayerPrefs.SetFloat("setup_s_grafik", this.s_grafik);
		PlayerPrefs.SetFloat("setup_s_sound", this.s_sound);
		PlayerPrefs.SetFloat("setup_s_pr", this.s_pr);
		PlayerPrefs.SetFloat("setup_s_gametests", this.s_gametests);
		PlayerPrefs.SetFloat("setup_s_technik", this.s_technik);
		PlayerPrefs.SetFloat("setup_s_forschen", this.s_forschen);
		for (int i = 0; i < this.perks.Length; i++)
		{
			if (this.perks[i])
			{
				PlayerPrefs.SetInt("setup_s_perks_" + i.ToString(), 1);
			}
			else
			{
				PlayerPrefs.SetInt("setup_s_perks_" + i.ToString(), 0);
			}
		}
		if (this.male)
		{
			PlayerPrefs.SetInt("setup_male", 1);
		}
		else
		{
			PlayerPrefs.SetInt("setup_male", 0);
		}
		PlayerPrefs.SetInt("setup_body", this.body);
		PlayerPrefs.SetInt("setup_hair", this.hair);
		PlayerPrefs.SetInt("setup_eyes", this.eyes);
		PlayerPrefs.SetInt("setup_beard", this.beard);
		PlayerPrefs.SetInt("setup_colorSkin", this.colorSkin);
		PlayerPrefs.SetInt("setup_colorHair", this.colorHair);
		PlayerPrefs.SetInt("setup_colorShirt", this.colorShirt);
		PlayerPrefs.SetInt("setup_colorHose", this.colorHose);
		PlayerPrefs.SetInt("setup_colorAdd1", this.colorAdd1);
	}

	
	private void LoadData()
	{
		this.uiObjects[12].GetComponent<InputField>().text = PlayerPrefs.GetString("PlayerName");
		if (PlayerPrefs.HasKey("setup_beruf"))
		{
			this.beruf = PlayerPrefs.GetInt("setup_beruf");
		}
		if (PlayerPrefs.HasKey("setup_s_skills"))
		{
			this.s_skills = PlayerPrefs.GetFloat("setup_s_skills");
			this.s_gamedesign = PlayerPrefs.GetFloat("setup_s_gamedesign");
			this.s_programmieren = PlayerPrefs.GetFloat("setup_s_programmieren");
			this.s_grafik = PlayerPrefs.GetFloat("setup_s_grafik");
			this.s_sound = PlayerPrefs.GetFloat("setup_s_sound");
			this.s_pr = PlayerPrefs.GetFloat("setup_s_pr");
			this.s_gametests = PlayerPrefs.GetFloat("setup_s_gametests");
			this.s_technik = PlayerPrefs.GetFloat("setup_s_technik");
			this.s_forschen = PlayerPrefs.GetFloat("setup_s_forschen");
		}
		if (PlayerPrefs.HasKey("setup_s_perks_0"))
		{
			for (int i = 0; i < this.perks.Length; i++)
			{
				if (PlayerPrefs.GetInt("setup_s_perks_" + i.ToString()) > 0)
				{
					this.perks[i] = true;
				}
				else
				{
					this.perks[i] = false;
				}
			}
		}
		if (PlayerPrefs.HasKey("setup_male"))
		{
			if (PlayerPrefs.GetInt("setup_male") == 1)
			{
				this.male = true;
			}
			else
			{
				this.male = false;
			}
			this.body = PlayerPrefs.GetInt("setup_body");
			this.hair = PlayerPrefs.GetInt("setup_hair");
			this.eyes = PlayerPrefs.GetInt("setup_eyes");
			this.beard = PlayerPrefs.GetInt("setup_beard");
			this.colorSkin = PlayerPrefs.GetInt("setup_colorSkin");
			this.colorHair = PlayerPrefs.GetInt("setup_colorHair");
			this.colorShirt = PlayerPrefs.GetInt("setup_colorShirt");
			this.colorHose = PlayerPrefs.GetInt("setup_colorHose");
			this.colorAdd1 = PlayerPrefs.GetInt("setup_colorAdd1");
		}
	}

	
	public void BUTTON_Male()
	{
		this.sfx_.PlaySound(3, true);
		this.male = true;
		this.InitSlider();
		this.CreateChar();
		this.uiObjects[9].GetComponent<Image>().color = this.guiMain_.colors[0];
		this.uiObjects[10].GetComponent<Image>().color = Color.white;
	}

	
	public void BUTTON_Female()
	{
		this.sfx_.PlaySound(3, true);
		this.male = false;
		this.InitSlider();
		this.CreateChar();
		this.uiObjects[9].GetComponent<Image>().color = Color.white;
		this.uiObjects[10].GetComponent<Image>().color = this.guiMain_.colors[0];
	}

	
	public void BUTTON_Random()
	{
		this.sfx_.PlaySound(3, true);
		this.body = -2;
		this.hair = -2;
		this.eyes = -2;
		this.beard = -2;
		this.colorSkin = -2;
		this.colorHair = -2;
		this.colorShirt = -2;
		this.colorHose = -2;
		this.colorAdd1 = -2;
		if (this.male)
		{
			this.BUTTON_Male();
			return;
		}
		this.BUTTON_Female();
	}

	
	private IEnumerator iAddStats(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_AddStats(i);
		}
		yield break;
	}

	
	public void BUTTON_AddStats(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (this.s_skills <= 0f)
		{
			return;
		}
		switch (i)
		{
		case 0:
			if (this.s_gamedesign < 255f && this.beruf == 0)
			{
				this.s_gamedesign += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_gamedesign < this.GetSkillCap() && this.beruf != 0)
			{
				this.s_gamedesign += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 1:
			if (this.s_programmieren < 255f && this.beruf == 1)
			{
				this.s_programmieren += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_programmieren < this.GetSkillCap() && this.beruf != 1)
			{
				this.s_programmieren += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 2:
			if (this.s_grafik < 255f && this.beruf == 2)
			{
				this.s_grafik += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_grafik < this.GetSkillCap() && this.beruf != 2)
			{
				this.s_grafik += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 3:
			if (this.s_sound < 255f && this.beruf == 3)
			{
				this.s_sound += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_sound < this.GetSkillCap() && this.beruf != 3)
			{
				this.s_sound += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 4:
			if (this.s_pr < 255f && this.beruf == 4)
			{
				this.s_pr += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_pr < this.GetSkillCap() && this.beruf != 4)
			{
				this.s_pr += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 5:
			if (this.s_gametests < 255f && this.beruf == 5)
			{
				this.s_gametests += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_gametests < this.GetSkillCap() && this.beruf != 5)
			{
				this.s_gametests += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 6:
			if (this.s_technik < 255f && this.beruf == 6)
			{
				this.s_technik += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_technik < this.GetSkillCap() && this.beruf != 6)
			{
				this.s_technik += 5f;
				this.s_skills -= 5f;
			}
			break;
		case 7:
			if (this.s_forschen < 255f && this.beruf == 7)
			{
				this.s_forschen += 5f;
				this.s_skills -= 5f;
			}
			if (this.s_forschen < this.GetSkillCap() && this.beruf != 7)
			{
				this.s_forschen += 5f;
				this.s_skills -= 5f;
			}
			break;
		}
		this.InitSkills();
		base.StartCoroutine(this.iAddStats(i));
	}

	
	private IEnumerator iMinusStats(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusStats(i);
		}
		yield break;
	}

	
	public void BUTTON_MinusStats(int i)
	{
		this.sfx_.PlaySound(3, true);
		switch (i)
		{
		case 0:
			if (this.s_gamedesign > 15f)
			{
				this.s_gamedesign -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 1:
			if (this.s_programmieren > 15f)
			{
				this.s_programmieren -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 2:
			if (this.s_grafik > 15f)
			{
				this.s_grafik -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 3:
			if (this.s_sound > 15f)
			{
				this.s_sound -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 4:
			if (this.s_pr > 15f)
			{
				this.s_pr -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 5:
			if (this.s_gametests > 15f)
			{
				this.s_gametests -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 6:
			if (this.s_technik > 15f)
			{
				this.s_technik -= 5f;
				this.s_skills += 5f;
			}
			break;
		case 7:
			if (this.s_forschen > 15f)
			{
				this.s_forschen -= 5f;
				this.s_skills += 5f;
			}
			break;
		}
		this.InitSkills();
		base.StartCoroutine(this.iMinusStats(i));
	}

	
	public void BUTTON_RandomPerks()
	{
	}

	
	public void BUTTON_Perk(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.perks[i] = !this.perks[i];
		this.perks[0] = true;
		int num = 0;
		for (int j = 0; j < this.perks.Length; j++)
		{
			if (this.perks[j])
			{
				if (this.uiObjects[24].transform.childCount > j)
				{
					this.uiObjects[24].transform.GetChild(j).GetComponent<Image>().color = this.guiMain_.colors[0];
					num++;
				}
			}
			else if (this.uiObjects[24].transform.childCount > j)
			{
				this.uiObjects[24].transform.GetChild(j).GetComponent<Image>().color = Color.white;
			}
		}
		string text = this.tS_.GetText(1682);
		text = text.Replace("<NUM>", (5 - num).ToString());
		this.uiObjects[25].GetComponent<Text>().text = text;
		if (num >= 18)
		{
			for (int k = 0; k < this.perks.Length; k++)
			{
				if (this.uiObjects[24].transform.childCount > k && !this.perks[k])
				{
					this.uiObjects[24].transform.GetChild(k).GetComponent<Button>().interactable = false;
				}
			}
		}
		else
		{
			for (int l = 0; l < this.uiObjects[24].transform.childCount; l++)
			{
				if (this.uiObjects[24].transform.childCount > l)
				{
					this.uiObjects[24].transform.GetChild(l).GetComponent<Button>().interactable = true;
				}
			}
		}
		this.DROPDOWN_Beruf();
		this.InitSkills();
	}

	
	private float GetSkillCap()
	{
		if (!this.perks[15])
		{
			return 200f;
		}
		return 255f;
	}

	
	public void DROPDOWN_Beruf()
	{
		this.sfx_.PlaySound(3, true);
		this.beruf = this.uiObjects[23].GetComponent<Dropdown>().value;
		if (this.s_gamedesign > this.GetSkillCap() && this.beruf != 0)
		{
			float f = this.s_gamedesign - this.GetSkillCap();
			this.s_gamedesign = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f);
		}
		if (this.s_programmieren > this.GetSkillCap() && this.beruf != 1)
		{
			float f2 = this.s_programmieren - this.GetSkillCap();
			this.s_programmieren = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f2);
		}
		if (this.s_grafik > this.GetSkillCap() && this.beruf != 2)
		{
			float f3 = this.s_grafik - this.GetSkillCap();
			this.s_grafik = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f3);
		}
		if (this.s_sound > this.GetSkillCap() && this.beruf != 3)
		{
			float f4 = this.s_sound - this.GetSkillCap();
			this.s_sound = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f4);
		}
		if (this.s_pr > this.GetSkillCap() && this.beruf != 4)
		{
			float f5 = this.s_pr - this.GetSkillCap();
			this.s_pr = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f5);
		}
		if (this.s_gametests > this.GetSkillCap() && this.beruf != 5)
		{
			float f6 = this.s_gametests - this.GetSkillCap();
			this.s_gametests = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f6);
		}
		if (this.s_technik > this.GetSkillCap() && this.beruf != 6)
		{
			float f7 = this.s_technik - this.GetSkillCap();
			this.s_technik = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f7);
		}
		if (this.s_forschen > this.GetSkillCap() && this.beruf != 7)
		{
			float f8 = this.s_forschen - this.GetSkillCap();
			this.s_forschen = this.GetSkillCap();
			this.s_skills += (float)Mathf.RoundToInt(f8);
		}
		this.InitSkills();
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private characterScript cS_;

	
	private GameObject character;

	
	private createCharScript cCS_;

	
	private clothScript cloth_;

	
	private bool sliderEvent = true;

	
	private float camRotate;

	
	public bool male = true;

	
	public int body = -2;

	
	public int hair = -2;

	
	public int eyes = -2;

	
	public int beard = -2;

	
	public int colorSkin = -2;

	
	public int colorHair = -2;

	
	public int colorShirt = -2;

	
	public int colorHose = -2;

	
	public int colorAdd1 = -2;

	
	public int beruf;

	
	public float s_skills;

	
	public float s_gamedesign;

	
	public float s_programmieren;

	
	public float s_grafik;

	
	public float s_sound;

	
	public float s_pr;

	
	public float s_gametests;

	
	public float s_technik;

	
	public float s_forschen;

	
	public bool[] perks;
}
