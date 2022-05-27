using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000179 RID: 377
public class Menu_NewGameCEO : MonoBehaviour
{
	// Token: 0x06000E03 RID: 3587 RVA: 0x00096784 File Offset: 0x00094984
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x0009678C File Offset: 0x0009498C
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

	// Token: 0x06000E05 RID: 3589 RVA: 0x00096880 File Offset: 0x00094A80
	private void Update()
	{
		if (this.s_skills > 50f)
		{
			this.s_skills = 50f;
			this.s_gamedesign = 15f;
			this.s_programmieren = 15f;
			this.s_grafik = 15f;
			this.s_sound = 15f;
			this.s_pr = 15f;
			this.s_gametests = 15f;
			this.s_technik = 15f;
			this.s_forschen = 15f;
		}
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x000968FD File Offset: 0x00094AFD
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x0009690C File Offset: 0x00094B0C
	public void Init()
	{
		this.InitDropdowns();
		if (this.uiObjects[12].GetComponent<InputField>().text.Length <= 0)
		{
			this.s_skills = 50f;
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

	// Token: 0x06000E08 RID: 3592 RVA: 0x00096AB0 File Offset: 0x00094CB0
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

	// Token: 0x06000E09 RID: 3593 RVA: 0x00096BBC File Offset: 0x00094DBC
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

	// Token: 0x06000E0A RID: 3594 RVA: 0x00096CC0 File Offset: 0x00094EC0
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

	// Token: 0x06000E0B RID: 3595 RVA: 0x000972FC File Offset: 0x000954FC
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

	// Token: 0x06000E0C RID: 3596 RVA: 0x00097448 File Offset: 0x00095648
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

	// Token: 0x06000E0D RID: 3597 RVA: 0x000974C4 File Offset: 0x000956C4
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

	// Token: 0x06000E0E RID: 3598 RVA: 0x000975B8 File Offset: 0x000957B8
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

	// Token: 0x06000E0F RID: 3599 RVA: 0x0009762C File Offset: 0x0009582C
	public void SLIDER_Body()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.body = Mathf.RoundToInt(this.uiObjects[0].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x0009765A File Offset: 0x0009585A
	public void SLIDER_Hair()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.hair = Mathf.RoundToInt(this.uiObjects[1].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x00097688 File Offset: 0x00095888
	public void SLIDER_Eyes()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.eyes = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x000976B6 File Offset: 0x000958B6
	public void SLIDER_Beard()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.beard = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x000976E4 File Offset: 0x000958E4
	public void SLIDER_ColorSkin()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorSkin = Mathf.RoundToInt(this.uiObjects[4].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00097712 File Offset: 0x00095912
	public void SLIDER_ColorHair()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorHair = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00097740 File Offset: 0x00095940
	public void SLIDER_ColorShirt()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorShirt = Mathf.RoundToInt(this.uiObjects[6].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0009776E File Offset: 0x0009596E
	public void SLIDER_ColorHose()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorHose = Mathf.RoundToInt(this.uiObjects[7].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x0009779C File Offset: 0x0009599C
	public void SLIDER_ColorAdd1()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorAdd1 = Mathf.RoundToInt(this.uiObjects[8].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000977CC File Offset: 0x000959CC
	public void SLIDER_CamRotate()
	{
		this.camRotate = this.uiObjects[11].GetComponent<Slider>().value;
		if (this.character)
		{
			this.character.transform.eulerAngles = new Vector3(0f, this.camRotate, 0f);
		}
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x00097824 File Offset: 0x00095A24
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().BUTTON_Close();
		}
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x00097874 File Offset: 0x00095A74
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.uiObjects[12].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(824), false);
			return;
		}
		if (this.s_skills > 0f)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(831), false);
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

	// Token: 0x06000E1B RID: 3611 RVA: 0x0009797C File Offset: 0x00095B7C
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

	// Token: 0x06000E1C RID: 3612 RVA: 0x00097B44 File Offset: 0x00095D44
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

	// Token: 0x06000E1D RID: 3613 RVA: 0x00097D34 File Offset: 0x00095F34
	public void BUTTON_Male()
	{
		this.sfx_.PlaySound(3, true);
		this.male = true;
		this.InitSlider();
		this.CreateChar();
		this.uiObjects[9].GetComponent<Image>().color = this.guiMain_.colors[0];
		this.uiObjects[10].GetComponent<Image>().color = Color.white;
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00097DA0 File Offset: 0x00095FA0
	public void BUTTON_Female()
	{
		this.sfx_.PlaySound(3, true);
		this.male = false;
		this.InitSlider();
		this.CreateChar();
		this.uiObjects[9].GetComponent<Image>().color = Color.white;
		this.uiObjects[10].GetComponent<Image>().color = this.guiMain_.colors[0];
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00097E0C File Offset: 0x0009600C
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

	// Token: 0x06000E20 RID: 3616 RVA: 0x00097E83 File Offset: 0x00096083
	private IEnumerator iAddStats(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_AddStats(i);
		}
		yield break;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x00097E9C File Offset: 0x0009609C
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
			if (this.s_gamedesign < 100f && this.beruf == 0)
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
			if (this.s_programmieren < 100f && this.beruf == 1)
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
			if (this.s_grafik < 100f && this.beruf == 2)
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
			if (this.s_sound < 100f && this.beruf == 3)
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
			if (this.s_pr < 100f && this.beruf == 4)
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
			if (this.s_gametests < 100f && this.beruf == 5)
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
			if (this.s_technik < 100f && this.beruf == 6)
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
			if (this.s_forschen < 100f && this.beruf == 7)
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

	// Token: 0x06000E22 RID: 3618 RVA: 0x000982F3 File Offset: 0x000964F3
	private IEnumerator iMinusStats(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusStats(i);
		}
		yield break;
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x0009830C File Offset: 0x0009650C
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

	// Token: 0x06000E24 RID: 3620 RVA: 0x00002715 File Offset: 0x00000915
	public void BUTTON_RandomPerks()
	{
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0009851C File Offset: 0x0009671C
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
		if (num >= 5)
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

	// Token: 0x06000E26 RID: 3622 RVA: 0x000986FB File Offset: 0x000968FB
	private float GetSkillCap()
	{
		if (!this.perks[15])
		{
			return 50f;
		}
		return 60f;
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x00098714 File Offset: 0x00096914
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

	// Token: 0x04001290 RID: 4752
	public GameObject[] uiObjects;

	// Token: 0x04001291 RID: 4753
	private GameObject main_;

	// Token: 0x04001292 RID: 4754
	private mainScript mS_;

	// Token: 0x04001293 RID: 4755
	private textScript tS_;

	// Token: 0x04001294 RID: 4756
	private GUI_Main guiMain_;

	// Token: 0x04001295 RID: 4757
	private sfxScript sfx_;

	// Token: 0x04001296 RID: 4758
	private characterScript cS_;

	// Token: 0x04001297 RID: 4759
	private GameObject character;

	// Token: 0x04001298 RID: 4760
	private createCharScript cCS_;

	// Token: 0x04001299 RID: 4761
	private clothScript cloth_;

	// Token: 0x0400129A RID: 4762
	private bool sliderEvent = true;

	// Token: 0x0400129B RID: 4763
	private float camRotate;

	// Token: 0x0400129C RID: 4764
	public bool male = true;

	// Token: 0x0400129D RID: 4765
	public int body = -2;

	// Token: 0x0400129E RID: 4766
	public int hair = -2;

	// Token: 0x0400129F RID: 4767
	public int eyes = -2;

	// Token: 0x040012A0 RID: 4768
	public int beard = -2;

	// Token: 0x040012A1 RID: 4769
	public int colorSkin = -2;

	// Token: 0x040012A2 RID: 4770
	public int colorHair = -2;

	// Token: 0x040012A3 RID: 4771
	public int colorShirt = -2;

	// Token: 0x040012A4 RID: 4772
	public int colorHose = -2;

	// Token: 0x040012A5 RID: 4773
	public int colorAdd1 = -2;

	// Token: 0x040012A6 RID: 4774
	public int beruf;

	// Token: 0x040012A7 RID: 4775
	public float s_skills;

	// Token: 0x040012A8 RID: 4776
	public float s_gamedesign;

	// Token: 0x040012A9 RID: 4777
	public float s_programmieren;

	// Token: 0x040012AA RID: 4778
	public float s_grafik;

	// Token: 0x040012AB RID: 4779
	public float s_sound;

	// Token: 0x040012AC RID: 4780
	public float s_pr;

	// Token: 0x040012AD RID: 4781
	public float s_gametests;

	// Token: 0x040012AE RID: 4782
	public float s_technik;

	// Token: 0x040012AF RID: 4783
	public float s_forschen;

	// Token: 0x040012B0 RID: 4784
	public bool[] perks;
}
