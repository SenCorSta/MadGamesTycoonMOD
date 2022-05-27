using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000178 RID: 376
public class Menu_NewGameCEO : MonoBehaviour
{
	// Token: 0x06000DEB RID: 3563 RVA: 0x00009C08 File Offset: 0x00007E08
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x000A47E4 File Offset: 0x000A29E4
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

	// Token: 0x06000DED RID: 3565 RVA: 0x00009C10 File Offset: 0x00007E10
	private void Update()
	{
		this.s_skills = 100f;
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00009C1D File Offset: 0x00007E1D
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x000A48D8 File Offset: 0x000A2AD8
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

	// Token: 0x06000DF0 RID: 3568 RVA: 0x000A4A7C File Offset: 0x000A2C7C
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

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000A4B88 File Offset: 0x000A2D88
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

	// Token: 0x06000DF2 RID: 3570 RVA: 0x000A4C8C File Offset: 0x000A2E8C
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

	// Token: 0x06000DF3 RID: 3571 RVA: 0x000A52C8 File Offset: 0x000A34C8
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

	// Token: 0x06000DF4 RID: 3572 RVA: 0x000A5414 File Offset: 0x000A3614
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

	// Token: 0x06000DF5 RID: 3573 RVA: 0x000A5490 File Offset: 0x000A3690
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

	// Token: 0x06000DF6 RID: 3574 RVA: 0x000A5584 File Offset: 0x000A3784
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

	// Token: 0x06000DF7 RID: 3575 RVA: 0x00009C2B File Offset: 0x00007E2B
	public void SLIDER_Body()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.body = Mathf.RoundToInt(this.uiObjects[0].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x00009C59 File Offset: 0x00007E59
	public void SLIDER_Hair()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.hair = Mathf.RoundToInt(this.uiObjects[1].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00009C87 File Offset: 0x00007E87
	public void SLIDER_Eyes()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.eyes = Mathf.RoundToInt(this.uiObjects[2].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00009CB5 File Offset: 0x00007EB5
	public void SLIDER_Beard()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.beard = Mathf.RoundToInt(this.uiObjects[3].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x00009CE3 File Offset: 0x00007EE3
	public void SLIDER_ColorSkin()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorSkin = Mathf.RoundToInt(this.uiObjects[4].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x00009D11 File Offset: 0x00007F11
	public void SLIDER_ColorHair()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorHair = Mathf.RoundToInt(this.uiObjects[5].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x00009D3F File Offset: 0x00007F3F
	public void SLIDER_ColorShirt()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorShirt = Mathf.RoundToInt(this.uiObjects[6].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00009D6D File Offset: 0x00007F6D
	public void SLIDER_ColorHose()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorHose = Mathf.RoundToInt(this.uiObjects[7].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x00009D9B File Offset: 0x00007F9B
	public void SLIDER_ColorAdd1()
	{
		if (!this.sliderEvent)
		{
			return;
		}
		this.colorAdd1 = Mathf.RoundToInt(this.uiObjects[8].GetComponent<Slider>().value);
		this.CreateChar();
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x000A55F8 File Offset: 0x000A37F8
	public void SLIDER_CamRotate()
	{
		this.camRotate = this.uiObjects[11].GetComponent<Slider>().value;
		if (this.character)
		{
			this.character.transform.eulerAngles = new Vector3(0f, this.camRotate, 0f);
		}
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x000A5650 File Offset: 0x000A3850
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.mS_.multiplayer)
		{
			this.guiMain_.uiObjects[201].GetComponent<mpMain>().BUTTON_Close();
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x000A56A0 File Offset: 0x000A38A0
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

	// Token: 0x06000E03 RID: 3587 RVA: 0x000A577C File Offset: 0x000A397C
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

	// Token: 0x06000E04 RID: 3588 RVA: 0x000A5944 File Offset: 0x000A3B44
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

	// Token: 0x06000E05 RID: 3589 RVA: 0x000A5B34 File Offset: 0x000A3D34
	public void BUTTON_Male()
	{
		this.sfx_.PlaySound(3, true);
		this.male = true;
		this.InitSlider();
		this.CreateChar();
		this.uiObjects[9].GetComponent<Image>().color = this.guiMain_.colors[0];
		this.uiObjects[10].GetComponent<Image>().color = Color.white;
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x000A5BA0 File Offset: 0x000A3DA0
	public void BUTTON_Female()
	{
		this.sfx_.PlaySound(3, true);
		this.male = false;
		this.InitSlider();
		this.CreateChar();
		this.uiObjects[9].GetComponent<Image>().color = Color.white;
		this.uiObjects[10].GetComponent<Image>().color = this.guiMain_.colors[0];
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x000A5C0C File Offset: 0x000A3E0C
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

	// Token: 0x06000E08 RID: 3592 RVA: 0x00009DC9 File Offset: 0x00007FC9
	private IEnumerator iAddStats(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_AddStats(i);
		}
		yield break;
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x000A5C84 File Offset: 0x000A3E84
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

	// Token: 0x06000E0A RID: 3594 RVA: 0x00009DDF File Offset: 0x00007FDF
	private IEnumerator iMinusStats(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusStats(i);
		}
		yield break;
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x000A60DC File Offset: 0x000A42DC
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

	// Token: 0x06000E0C RID: 3596 RVA: 0x00002098 File Offset: 0x00000298
	public void BUTTON_RandomPerks()
	{
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x000A62EC File Offset: 0x000A44EC
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

	// Token: 0x06000E0E RID: 3598 RVA: 0x00009DF5 File Offset: 0x00007FF5
	private float GetSkillCap()
	{
		if (!this.perks[15])
		{
			return 200f;
		}
		return 255f;
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x000A64CC File Offset: 0x000A46CC
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

	// Token: 0x04001288 RID: 4744
	public GameObject[] uiObjects;

	// Token: 0x04001289 RID: 4745
	private GameObject main_;

	// Token: 0x0400128A RID: 4746
	private mainScript mS_;

	// Token: 0x0400128B RID: 4747
	private textScript tS_;

	// Token: 0x0400128C RID: 4748
	private GUI_Main guiMain_;

	// Token: 0x0400128D RID: 4749
	private sfxScript sfx_;

	// Token: 0x0400128E RID: 4750
	private characterScript cS_;

	// Token: 0x0400128F RID: 4751
	private GameObject character;

	// Token: 0x04001290 RID: 4752
	private createCharScript cCS_;

	// Token: 0x04001291 RID: 4753
	private clothScript cloth_;

	// Token: 0x04001292 RID: 4754
	private bool sliderEvent = true;

	// Token: 0x04001293 RID: 4755
	private float camRotate;

	// Token: 0x04001294 RID: 4756
	public bool male = true;

	// Token: 0x04001295 RID: 4757
	public int body = -2;

	// Token: 0x04001296 RID: 4758
	public int hair = -2;

	// Token: 0x04001297 RID: 4759
	public int eyes = -2;

	// Token: 0x04001298 RID: 4760
	public int beard = -2;

	// Token: 0x04001299 RID: 4761
	public int colorSkin = -2;

	// Token: 0x0400129A RID: 4762
	public int colorHair = -2;

	// Token: 0x0400129B RID: 4763
	public int colorShirt = -2;

	// Token: 0x0400129C RID: 4764
	public int colorHose = -2;

	// Token: 0x0400129D RID: 4765
	public int colorAdd1 = -2;

	// Token: 0x0400129E RID: 4766
	public int beruf;

	// Token: 0x0400129F RID: 4767
	public float s_skills;

	// Token: 0x040012A0 RID: 4768
	public float s_gamedesign;

	// Token: 0x040012A1 RID: 4769
	public float s_programmieren;

	// Token: 0x040012A2 RID: 4770
	public float s_grafik;

	// Token: 0x040012A3 RID: 4771
	public float s_sound;

	// Token: 0x040012A4 RID: 4772
	public float s_pr;

	// Token: 0x040012A5 RID: 4773
	public float s_gametests;

	// Token: 0x040012A6 RID: 4774
	public float s_technik;

	// Token: 0x040012A7 RID: 4775
	public float s_forschen;

	// Token: 0x040012A8 RID: 4776
	public bool[] perks;
}
