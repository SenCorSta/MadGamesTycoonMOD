using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000131 RID: 305
public class Menu_Dev_ChangePlatform : MonoBehaviour
{
	// Token: 0x06000AE0 RID: 2784 RVA: 0x000759DD File Offset: 0x00073BDD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x000759E8 File Offset: 0x00073BE8
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

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00075BA4 File Offset: 0x00073DA4
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

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00075D81 File Offset: 0x00073F81
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.CloseMenu();
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00075DA8 File Offset: 0x00073FA8
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

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00076024 File Offset: 0x00074224
	public void BUTTON_PlatformKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[33]);
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0007604C File Offset: 0x0007424C
	public void BUTTON_Platform(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[66]);
		this.guiMain_.uiObjects[66].GetComponent<Menu_DevGame_Platform>().Init(i);
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x00076098 File Offset: 0x00074298
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

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0007666C File Offset: 0x0007486C
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

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00076708 File Offset: 0x00074908
	public int GetEngineTechLevel()
	{
		this.gS_.FindMyEngineNew();
		if (this.gS_.engineS_)
		{
			return this.gS_.engineS_.GetTechLevel();
		}
		return 0;
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x0007673C File Offset: 0x0007493C
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

	// Token: 0x04000F2B RID: 3883
	public GameObject[] uiObjects;

	// Token: 0x04000F2C RID: 3884
	public int[] g_GamePlatform;

	// Token: 0x04000F2D RID: 3885
	private GameObject main_;

	// Token: 0x04000F2E RID: 3886
	private mainScript mS_;

	// Token: 0x04000F2F RID: 3887
	private textScript tS_;

	// Token: 0x04000F30 RID: 3888
	private GUI_Main guiMain_;

	// Token: 0x04000F31 RID: 3889
	private sfxScript sfx_;

	// Token: 0x04000F32 RID: 3890
	private genres genres_;

	// Token: 0x04000F33 RID: 3891
	private themes themes_;

	// Token: 0x04000F34 RID: 3892
	private licences licences_;

	// Token: 0x04000F35 RID: 3893
	private engineFeatures eF_;

	// Token: 0x04000F36 RID: 3894
	private cameraMovementScript cmS_;

	// Token: 0x04000F37 RID: 3895
	private unlockScript unlock_;

	// Token: 0x04000F38 RID: 3896
	private gameplayFeatures gF_;

	// Token: 0x04000F39 RID: 3897
	private games games_;

	// Token: 0x04000F3A RID: 3898
	private platforms platforms_;

	// Token: 0x04000F3B RID: 3899
	public gameScript gS_;
}
