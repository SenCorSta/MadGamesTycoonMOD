using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_ChangePlatform : MonoBehaviour
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
	}

	
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.uiObjects[28].GetComponent<Text>().text = this.tS_.GetText(635);
		if (this.gS_.retro)
		{
			this.uiObjects[28].GetComponent<Text>().text = "<color=red>" + this.tS_.GetText(907) + "</color>";
		}
		if (this.gS_.exklusiv)
		{
			this.uiObjects[28].GetComponent<Text>().text = "<color=red>" + this.tS_.GetText(1919) + "</color>";
		}
		this.uiObjects[20].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[26].GetComponent<Text>().text = this.tS_.GetText(376) + ": " + this.GetEngineTechLevel().ToString();
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			this.SetPlatform(i, game_.gamePlatform[i], true);
		}
		this.uiObjects[27].GetComponent<Text>().text = this.gS_.GetPlatformTypString();
		this.uiObjects[29].GetComponent<Image>().sprite = this.gS_.GetPlatformTypSprite();
		if (this.gS_.retro || this.gS_.exklusiv)
		{
			this.uiObjects[21].GetComponent<Button>().interactable = false;
			this.uiObjects[22].GetComponent<Button>().interactable = false;
			this.uiObjects[23].GetComponent<Button>().interactable = false;
			this.uiObjects[24].GetComponent<Button>().interactable = false;
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	
	public void BUTTON_Ok()
	{
		string text = "";
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					if (component)
					{
						if (component.needFeatures[0] != -1 && !this.gS_.gameGameplayFeatures[component.needFeatures[0]])
						{
							text = string.Concat(new string[]
							{
								text,
								component.GetName(),
								": ",
								this.gF_.GetName(component.needFeatures[0]),
								"\n"
							});
						}
						if (component.needFeatures[1] != -1 && !this.gS_.gameGameplayFeatures[component.needFeatures[1]])
						{
							text = string.Concat(new string[]
							{
								text,
								component.GetName(),
								": ",
								this.gF_.GetName(component.needFeatures[1]),
								"\n"
							});
						}
						if (component.needFeatures[2] != -1 && !this.gS_.gameGameplayFeatures[component.needFeatures[2]])
						{
							text = string.Concat(new string[]
							{
								text,
								component.GetName(),
								": ",
								this.gF_.GetName(component.needFeatures[2]),
								"\n"
							});
						}
						if ((this.gS_.gameTyp == 1 || this.gS_.gameTyp == 2) && !component.internet)
						{
							this.guiMain_.MessageBox(this.tS_.GetText(1262), false);
							return;
						}
					}
				}
			}
		}
		if (text.Length > 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1020) + "\n<color=blue>" + text + "</color>", false);
			return;
		}
		int num = this.CalcDevCosts();
		this.mS_.Pay((long)num, 10);
		this.gS_.costs_entwicklung += (long)num;
		for (int j = 0; j < this.g_GamePlatform.Length; j++)
		{
			this.gS_.gamePlatform[j] = this.g_GamePlatform[j];
		}
		this.BUTTON_Close();
	}

	
	public void BUTTON_PlatformKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[33]);
	}

	
	public void BUTTON_Platform(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[66]);
		this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().Init(i);
	}

	
	public void SetPlatform(int slot, int platform_, bool init)
	{
		this.g_GamePlatform[slot] = platform_;
		if (platform_ >= 0)
		{
			GameObject gameObject = GameObject.Find("PLATFORM_" + platform_.ToString());
			if (gameObject)
			{
				platformScript component = gameObject.GetComponent<platformScript>();
				this.uiObjects[slot].GetComponent<Text>().text = component.GetName();
				component.SetPic(this.uiObjects[4 + slot]);
				this.uiObjects[4 + slot].SetActive(true);
				this.guiMain_.DrawStars(this.uiObjects[8 + slot], component.erfahrung);
				this.uiObjects[12 + slot].GetComponent<Text>().text = component.tech.ToString();
				this.uiObjects[16 + slot].GetComponent<Text>().text = component.GetMarktanteilString();
				this.uiObjects[30 + slot].GetComponent<Image>().sprite = component.GetComplexSprite();
				this.uiObjects[38 + slot].GetComponent<Image>().sprite = component.GetTypSprite();
				this.uiObjects[38 + slot].GetComponent<tooltip>().c = component.GetTypString();
				if (component.internet)
				{
					this.uiObjects[34 + slot].SetActive(true);
				}
				else
				{
					this.uiObjects[34 + slot].SetActive(false);
				}
				this.uiObjects[4 + slot].GetComponent<tooltip>().c = component.GetTooltip();
				if (init)
				{
					if (!component.vomMarktGenommen)
					{
						this.uiObjects[21 + slot].GetComponent<Button>().interactable = false;
					}
					else
					{
						this.uiObjects[21 + slot].GetComponent<Button>().interactable = true;
					}
				}
			}
			else
			{
				this.uiObjects[slot].GetComponent<Text>().text = this.tS_.GetText(360 + slot);
				this.uiObjects[4 + slot].GetComponent<Image>().sprite = null;
				this.uiObjects[4 + slot].SetActive(false);
				this.guiMain_.DrawStars(this.uiObjects[8 + slot], 0);
				this.uiObjects[12 + slot].GetComponent<Text>().text = "-";
				this.uiObjects[16 + slot].GetComponent<Text>().text = "";
				this.uiObjects[30 + slot].GetComponent<Image>().sprite = this.platforms_.complexSprites[0];
				this.uiObjects[38 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
				this.uiObjects[38 + slot].GetComponent<tooltip>().c = "";
				this.uiObjects[34 + slot].SetActive(false);
				this.uiObjects[4 + slot].GetComponent<tooltip>().c = "";
				if (init)
				{
					this.uiObjects[21 + slot].GetComponent<Button>().interactable = true;
				}
			}
		}
		else
		{
			this.uiObjects[slot].GetComponent<Text>().text = this.tS_.GetText(360 + slot);
			this.uiObjects[4 + slot].GetComponent<Image>().sprite = null;
			this.uiObjects[4 + slot].SetActive(false);
			this.guiMain_.DrawStars(this.uiObjects[8 + slot], 0);
			this.uiObjects[12 + slot].GetComponent<Text>().text = "-";
			this.uiObjects[16 + slot].GetComponent<Text>().text = "";
			this.uiObjects[30 + slot].GetComponent<Image>().sprite = this.platforms_.complexSprites[0];
			this.uiObjects[38 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[38 + slot].GetComponent<tooltip>().c = "";
			this.uiObjects[34 + slot].SetActive(false);
			if (init)
			{
				this.uiObjects[21 + slot].GetComponent<Button>().interactable = false;
			}
			if (slot == 0)
			{
				this.uiObjects[21 + slot].GetComponent<Button>().interactable = true;
			}
		}
		this.uiObjects[42].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(0), 1).ToString() + "%";
		this.uiObjects[43].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(1), 1).ToString() + "%";
		this.uiObjects[44].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(2), 1).ToString() + "%";
		this.uiObjects[45].GetComponent<Text>().text = this.mS_.Round(this.GetGesamtMarktanteil(3), 1).ToString() + "%";
		long num = 0L;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.g_GamePlatform[i] >= 0)
			{
				GameObject gameObject2 = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
				if (gameObject2)
				{
					platformScript component2 = gameObject2.GetComponent<platformScript>();
					num += (long)component2.GetAktiveNutzer();
				}
			}
		}
		this.uiObjects[46].GetComponent<Text>().text = this.mS_.Round((float)num / 1000000f, 1).ToString() + " " + this.tS_.GetText(1483);
		this.uiObjects[25].GetComponent<Text>().text = this.mS_.GetMoney((long)this.CalcDevCosts(), true);
	}

	
	public float GetGesamtMarktanteil(int platformTyp)
	{
		if (!this.gS_)
		{
			return 0f;
		}
		this.FindScripts();
		float num = 0f;
		for (int i = 0; i < this.gS_.gamePlatform.Length; i++)
		{
			if (this.gS_.gamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.gS_.gamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					if (component.typ == platformTyp)
					{
						num += component.GetMarktanteil();
					}
				}
			}
		}
		return num;
	}

	
	public int GetEngineTechLevel()
	{
		this.gS_.FindMyEngineNew();
		if (this.gS_.engineS_)
		{
			return this.gS_.engineS_.GetTechLevel();
		}
		return 0;
	}

	
	private int CalcDevCosts()
	{
		int num = 0;
		for (int i = 0; i < this.g_GamePlatform.Length; i++)
		{
			if (this.gS_.gamePlatform[i] != this.g_GamePlatform[i] && this.g_GamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.g_GamePlatform[i].ToString());
				if (gameObject)
				{
					num += gameObject.GetComponent<platformScript>().GetDevCosts();
				}
			}
		}
		return num;
	}

	
	public GameObject[] uiObjects;

	
	public int[] g_GamePlatform;

	
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

	
	public gameScript gS_;
}
