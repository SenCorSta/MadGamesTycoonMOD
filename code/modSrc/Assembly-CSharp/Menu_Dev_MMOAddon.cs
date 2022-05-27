using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000138 RID: 312
public class Menu_Dev_MMOAddon : MonoBehaviour
{
	// Token: 0x06000B31 RID: 2865 RVA: 0x00007F5A File Offset: 0x0000615A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x0008A5A8 File Offset: 0x000887A8
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
		if (!this.forschungSonstiges_)
		{
			this.forschungSonstiges_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.menuDevGame_)
		{
			this.menuDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00007F62 File Offset: 0x00006162
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00007F76 File Offset: 0x00006176
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00007F8A File Offset: 0x0000618A
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x0008A78C File Offset: 0x0008898C
	public void Init(roomScript roomScript_, gameScript gameScript_)
	{
		this.FindScripts();
		this.rS_ = roomScript_;
		this.gS_ = gameScript_;
		this.InitDropdowns();
		this.Init_GameplayFeatures();
		this.SetLeitenderDesigner(null, false);
		this.allAdds = false;
		this.allFeatures = true;
		this.SetCopyProtect(-1);
		this.SetAutomaticBestCopyProtect();
		this.SetAntiCheat(-1);
		this.SetAutomaticBestAntiCheat();
		this.g_GameAP_Gameplay = this.gS_.gameAP_Gameplay;
		this.g_GameAP_Grafik = this.gS_.gameAP_Grafik;
		this.g_GameAP_Sound = this.gS_.gameAP_Sound;
		this.g_GameAP_Technik = this.gS_.gameAP_Technik;
		this.uiObjects[42].GetComponent<Slider>().value = (float)this.g_GameAP_Gameplay;
		this.uiObjects[43].GetComponent<Slider>().value = (float)this.g_GameAP_Grafik;
		this.uiObjects[44].GetComponent<Slider>().value = (float)this.g_GameAP_Sound;
		this.uiObjects[45].GetComponent<Slider>().value = (float)this.g_GameAP_Technik;
		this.SetAP_Gameplay();
		this.SetAP_Grafik();
		this.SetAP_Sound();
		this.SetAP_Technik();
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			this.g_GameGameplayFeatures[i] = false;
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			this.sprachen[j] = this.gS_.gameLanguage[j];
		}
		for (int k = 0; k < this.buttonAdds.Length; k++)
		{
			this.buttonAdds[k] = false;
		}
		string text = this.gS_.GetNameSimple();
		text = text.Replace(" <color=green>" + this.tS_.GetText(1549) + "</color>", string.Empty);
		text = text.Replace(" <color=green>" + this.tS_.GetText(1896) + "</color>", string.Empty);
		text = text.Replace("<color=green>[A]</color>", string.Empty);
		text = text.Replace("<color=green>[P]</color>", string.Empty);
		text = text.Replace("<color=green>", string.Empty);
		text = text.Replace("[P]", string.Empty);
		text = text.Replace("[A]", string.Empty);
		text = text.Replace("</color>", string.Empty);
		text = text.Replace("\n", string.Empty);
		text = text.Replace("\r", string.Empty);
		text = text.Replace("\t", string.Empty);
		text = text.Replace(this.tS_.GetText(1896), string.Empty);
		text = text.Replace(this.tS_.GetText(1549), string.Empty);
		this.uiObjects[12].GetComponent<InputField>().text = text + " - " + this.tS_.GetText(963);
		this.g_Beschreibung = this.gS_.beschreibung;
		this.g_GameSubTheme = this.gS_.gameSubTheme;
		this.uiObjects[13].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[0]), true);
		this.uiObjects[14].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[1]), true);
		this.uiObjects[15].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[2]), true);
		this.uiObjects[16].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[3]), true);
		this.uiObjects[17].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[4]), true);
		this.uiObjects[18].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[5]), true);
		this.uiObjects[19].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[6]), true);
		this.uiObjects[20].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[7]), true);
		this.uiObjects[51].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.maingenre);
		this.uiObjects[52].GetComponent<Text>().text = this.genres_.GetName(this.gS_.maingenre);
		this.guiMain_.DrawStars(this.uiObjects[53], this.genres_.genres_LEVEL[this.gS_.maingenre]);
		if (this.gS_.subgenre != -1)
		{
			this.uiObjects[54].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.subgenre);
			this.uiObjects[55].GetComponent<Text>().text = this.genres_.GetName(this.gS_.subgenre);
			this.guiMain_.DrawStars(this.uiObjects[56], this.genres_.genres_LEVEL[this.gS_.subgenre]);
		}
		else
		{
			this.uiObjects[54].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[55].GetComponent<Text>().text = "---";
			this.guiMain_.DrawStars(this.uiObjects[56], 0);
		}
		this.uiObjects[57].GetComponent<Text>().text = this.tS_.GetThemes(this.gS_.gameMainTheme);
		this.guiMain_.DrawStars(this.uiObjects[58], this.themes_.themes_LEVEL[this.gS_.gameMainTheme]);
		this.SetSubTheme(this.gS_.gameSubTheme);
		this.uiObjects[62].GetComponent<Image>().sprite = this.gS_.GetSizeSprite();
		this.Unlock(31, this.uiObjects[49], this.uiObjects[48]);
		this.Unlock(31, null, this.uiObjects[50]);
		this.Unlock(64, this.uiObjects[67], this.uiObjects[66]);
		this.Unlock(64, null, this.uiObjects[68]);
		this.forschungSonstiges_.Unlock(36, this.uiObjects[75], this.uiObjects[74]);
		this.UpdateGUI();
		this.OpenSide(0);
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x0008AEC0 File Offset: 0x000890C0
	private void Unlock(int id_, GameObject lock_, GameObject button_)
	{
		if (this.unlock_.unlock[id_])
		{
			button_.GetComponent<Button>().interactable = true;
			if (lock_)
			{
				lock_.SetActive(false);
				return;
			}
		}
		else
		{
			button_.GetComponent<Button>().interactable = false;
			if (lock_)
			{
				lock_.SetActive(true);
			}
		}
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x0008AF14 File Offset: 0x00089114
	private void UpdateGUI()
	{
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				this.uiObjects[21 + i].GetComponent<Image>().color = this.guiMain_.colors[7];
			}
			else
			{
				this.uiObjects[21 + i].GetComponent<Image>().color = Color.white;
			}
		}
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			if (this.sprachen[j])
			{
				this.uiObjects[1 + j].GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.uiObjects[1 + j].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f);
			}
		}
		this.uiObjects[29].GetComponent<Text>().text = this.mS_.GetMoney(this.GetDevCosts(), true);
		this.GetGesamtDevPoints();
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0008B014 File Offset: 0x00089214
	private float GetAddonQuality()
	{
		float num = 0f;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += this.devPointsPercent[i];
			}
		}
		return num;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0008B050 File Offset: 0x00089250
	private long GetDevCosts()
	{
		long num = 0L;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += (long)Mathf.RoundToInt((float)this.gS_.costs_entwicklung * this.devCostsPercent[i]);
			}
		}
		int num2 = 0;
		for (int j = 0; j < this.sprachen.Length; j++)
		{
			if (this.sprachen[j] && !this.mS_.Muttersprache(j))
			{
				num2 += this.gS_.GetGesamtDevPoints() * 5;
				num += (long)(this.gS_.GetGesamtDevPoints() * 5);
			}
		}
		this.uiObjects[73].GetComponent<Text>().text = this.mS_.GetMoney((long)num2, true);
		if (this.g_GameCopyProtectScript_)
		{
			num += (long)this.g_GameCopyProtectScript_.GetDevCosts();
		}
		if (this.g_GameAntiCheatScript_)
		{
			num += (long)this.g_GameAntiCheatScript_.GetDevCosts();
		}
		for (int k = 0; k < this.g_GameGameplayFeatures.Length; k++)
		{
			if (this.g_GameGameplayFeatures[k] && !this.gS_.gameplayFeatures_DevDone[k])
			{
				num += (long)this.gF_.GetDevCosts(k);
			}
		}
		return num;
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00007FA5 File Offset: 0x000061A5
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00007FC0 File Offset: 0x000061C0
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		this.UpdateGUI();
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00007FE8 File Offset: 0x000061E8
	public void BUTTON_Sprache(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.sprachen[i] = !this.sprachen[i];
		this.UpdateGUI();
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x0008B188 File Offset: 0x00089388
	public void BUTTON_AlleSprache()
	{
		this.sfx_.PlaySound(3, true);
		this.allSprachen = !this.allSprachen;
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			this.sprachen[i] = this.allSprachen;
		}
		this.UpdateGUI();
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x0008B1D8 File Offset: 0x000893D8
	public void BUTTON_AlleAdds()
	{
		this.sfx_.PlaySound(3, true);
		this.allAdds = !this.allAdds;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			this.buttonAdds[i] = this.allAdds;
		}
		this.UpdateGUI();
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00008010 File Offset: 0x00006210
	public void BUTTON_CopyProtect()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[194]);
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0000803B File Offset: 0x0000623B
	public void BUTTON_CopyProtectKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[49]);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00008063 File Offset: 0x00006263
	public void BUTTON_AntiCheat()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[240]);
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x0000808E File Offset: 0x0000628E
	public void BUTTON_AntiCheatKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[234]);
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000080B9 File Offset: 0x000062B9
	public void SetBeschreibung(string c)
	{
		this.g_Beschreibung = c;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0008B228 File Offset: 0x00089428
	private void SetAutomaticBestCopyProtect()
	{
		if (this.g_GameCopyProtect == -1)
		{
			float num = 0f;
			int num2 = -1;
			GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					copyProtectScript component = array[i].GetComponent<copyProtectScript>();
					if (component && component.inBesitz && component.effekt > num)
					{
						num2 = component.myID;
						num = component.effekt;
					}
				}
			}
			if (num2 != -1)
			{
				this.SetCopyProtect(num2);
			}
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x0008B2AC File Offset: 0x000894AC
	public void SetCopyProtect(int i)
	{
		this.g_GameCopyProtect = i;
		if (i >= 0)
		{
			GameObject gameObject = GameObject.Find("COPYPROTECT_" + i.ToString());
			if (gameObject)
			{
				copyProtectScript component = gameObject.GetComponent<copyProtectScript>();
				this.g_GameCopyProtectScript_ = component;
				this.uiObjects[30].GetComponent<Text>().text = component.GetName();
				this.uiObjects[31].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetDevCosts(), true);
				this.uiObjects[32].GetComponent<Image>().fillAmount = component.effekt * 0.01f;
				this.uiObjects[33].GetComponent<Text>().text = this.mS_.Round(component.effekt, 2) + "%";
				this.uiObjects[32].GetComponent<Image>().color = this.GetValColor(component.effekt);
			}
		}
		else
		{
			this.g_GameCopyProtectScript_ = null;
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(383);
			this.uiObjects[31].GetComponent<Text>().text = "";
			this.uiObjects[32].GetComponent<Image>().fillAmount = 0f;
			this.uiObjects[33].GetComponent<Text>().text = "0.0%";
			this.uiObjects[32].GetComponent<Image>().color = this.GetValColor(0f);
		}
		this.UpdateGUI();
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0008B444 File Offset: 0x00089644
	private void SetAutomaticBestAntiCheat()
	{
		if (this.g_GameAntiCheat == -1)
		{
			float num = 0f;
			int num2 = -1;
			GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					antiCheatScript component = array[i].GetComponent<antiCheatScript>();
					if (component && component.inBesitz && component.effekt > num)
					{
						num2 = component.myID;
						num = component.effekt;
					}
				}
			}
			if (num2 != -1)
			{
				this.SetAntiCheat(num2);
			}
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0008B4C8 File Offset: 0x000896C8
	public void SetAntiCheat(int i)
	{
		this.g_GameAntiCheat = i;
		if (i >= 0)
		{
			GameObject gameObject = GameObject.Find("ANTICHEAT_" + i.ToString());
			if (gameObject)
			{
				antiCheatScript component = gameObject.GetComponent<antiCheatScript>();
				this.g_GameAntiCheatScript_ = component;
				this.uiObjects[69].GetComponent<Text>().text = component.GetName();
				this.uiObjects[70].GetComponent<Text>().text = this.mS_.GetMoney((long)component.GetDevCosts(), true);
				this.uiObjects[71].GetComponent<Image>().fillAmount = component.effekt * 0.01f;
				this.uiObjects[72].GetComponent<Text>().text = this.mS_.Round(component.effekt, 2) + "%";
				this.uiObjects[71].GetComponent<Image>().color = this.GetValColor(component.effekt);
			}
		}
		else
		{
			this.g_GameAntiCheatScript_ = null;
			this.uiObjects[69].GetComponent<Text>().text = this.tS_.GetText(1213);
			this.uiObjects[70].GetComponent<Text>().text = "";
			this.uiObjects[71].GetComponent<Image>().fillAmount = 0f;
			this.uiObjects[72].GetComponent<Text>().text = "0.0%";
			this.uiObjects[71].GetComponent<Image>().color = this.GetValColor(0f);
		}
		this.UpdateGUI();
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0008B660 File Offset: 0x00089860
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

	// Token: 0x06000B4A RID: 2890 RVA: 0x0008B6D4 File Offset: 0x000898D4
	public void BUTTON_Start()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num++;
			}
		}
		if (num < 3)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1258), false);
			this.OpenSide(0);
			return;
		}
		if (this.uiObjects[12].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(973), false);
			this.OpenSide(0);
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int j = 0; j < array.Length; j++)
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && component.GetNameSimple() == this.uiObjects[12].GetComponent<InputField>().text)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(618), false);
					this.OpenSide(0);
					return;
				}
			}
		}
		if (this.UpdateGesamtArbeitsprioritaet() > 100)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(400), false);
			this.OpenSide(1);
			return;
		}
		if (this.UpdateGesamtArbeitsprioritaet() < 100)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(416), false);
			this.OpenSide(1);
			return;
		}
		if (this.AnzahlLanguages() <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(404), false);
			this.OpenSide(1);
			return;
		}
		if (this.UpdateGesamtGameplayFeatures() > this.menuDevGame_.maxFeatures_gameSize[this.gS_.gameSize])
		{
			this.guiMain_.MessageBox(this.tS_.GetText(974), false);
			this.OpenSide(2);
			return;
		}
		int num2 = Mathf.RoundToInt((float)this.GetDevCosts());
		this.mS_.Pay((long)num2, 18);
		gameScript gameScript = this.games_.CreateNewGame(false, true);
		if (this.mS_.multiplayer)
		{
			gameScript.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
		gameScript.ownerID = this.mS_.myID;
		gameScript.mainIP = this.gS_.mainIP;
		gameScript.costs_entwicklung = (long)num2;
		gameScript.playerGame = true;
		gameScript.inDevelopment = true;
		gameScript.SetMyName(this.uiObjects[12].GetComponent<InputField>().text);
		gameScript.originalIP = this.gS_.myID;
		gameScript.developerID = -1;
		gameScript.addonQuality = this.GetAddonQuality();
		gameScript.beschreibung = this.g_Beschreibung;
		gameScript.typ_mmoaddon = true;
		gameScript.gameCopyProtect = this.g_GameCopyProtect;
		gameScript.gameAntiCheat = this.g_GameAntiCheat;
		gameScript.gameAP_Gameplay = this.g_GameAP_Gameplay;
		gameScript.gameAP_Grafik = this.g_GameAP_Grafik;
		gameScript.gameAP_Sound = this.g_GameAP_Sound;
		gameScript.gameAP_Technik = this.g_GameAP_Technik;
		gameScript.hype = (float)this.gS_.reviewTotal * 0.25f;
		for (int k = 0; k < this.g_GameGameplayFeatures.Length; k++)
		{
			gameScript.gameGameplayFeatures[k] = this.g_GameGameplayFeatures[k];
			if (this.gS_.gameplayFeatures_DevDone[k])
			{
				gameScript.gameGameplayFeatures[k] = true;
				gameScript.gameplayFeatures_DevDone[k] = true;
			}
		}
		for (int l = 0; l < this.sprachen.Length; l++)
		{
			gameScript.gameLanguage[l] = this.sprachen[l];
		}
		gameScript.usk = this.gS_.usk;
		gameScript.engineID = this.gS_.engineID;
		gameScript.exklusiv = this.gS_.exklusiv;
		gameScript.herstellerExklusiv = this.gS_.herstellerExklusiv;
		gameScript.retro = this.gS_.retro;
		gameScript.points_gameplay = this.gS_.points_gameplay;
		gameScript.points_grafik = this.gS_.points_grafik;
		gameScript.points_sound = this.gS_.points_sound;
		gameScript.points_technik = this.gS_.points_technik;
		gameScript.points_bugs = this.gS_.points_bugs;
		gameScript.gameTyp = 0;
		gameScript.gameSize = this.gS_.gameSize;
		gameScript.gameZielgruppe = this.gS_.gameZielgruppe;
		gameScript.maingenre = this.gS_.maingenre;
		gameScript.subgenre = this.gS_.subgenre;
		gameScript.gameMainTheme = this.gS_.gameMainTheme;
		gameScript.gameSubTheme = this.g_GameSubTheme;
		gameScript.gameLicence = this.gS_.gameLicence;
		for (int m = 0; m < this.gS_.Designausrichtung.Length; m++)
		{
			gameScript.Designausrichtung[m] = this.gS_.Designausrichtung[m];
		}
		for (int n = 0; n < this.gS_.Designschwerpunkt.Length; n++)
		{
			gameScript.Designschwerpunkt[n] = this.gS_.Designschwerpunkt[n];
		}
		gameScript.finanzierung_Grundkosten = this.gS_.finanzierung_Grundkosten;
		gameScript.finanzierung_Kontent = this.gS_.finanzierung_Kontent;
		gameScript.finanzierung_Technology = this.gS_.finanzierung_Technology;
		for (int num3 = 0; num3 < this.gS_.gamePlatform.Length; num3++)
		{
			gameScript.gamePlatform[num3] = this.gS_.gamePlatform[num3];
		}
		for (int num4 = 0; num4 < this.gS_.gameEngineFeature.Length; num4++)
		{
			gameScript.gameEngineFeature[num4] = this.gS_.gameEngineFeature[num4];
			gameScript.engineFeature_DevDone[num4] = this.gS_.engineFeature_DevDone[num4];
		}
		for (int num5 = 0; num5 < this.gS_.gameplayStudio.Length; num5++)
		{
			gameScript.gameplayStudio[num5] = this.gS_.gameplayStudio[num5];
		}
		for (int num6 = 0; num6 < this.gS_.grafikStudio.Length; num6++)
		{
			gameScript.grafikStudio[num6] = this.gS_.grafikStudio[num6];
		}
		for (int num7 = 0; num7 < this.gS_.soundStudio.Length; num7++)
		{
			gameScript.soundStudio[num7] = this.gS_.soundStudio[num7];
		}
		for (int num8 = 0; num8 < this.gS_.motionCaptureStudio.Length; num8++)
		{
			gameScript.motionCaptureStudio[num8] = this.gS_.motionCaptureStudio[num8];
		}
		int devPointsContent = this.GetDevPointsContent();
		gameScript.devPointsStart_Gesamt = (float)(gameScript.GetGesamtDevPointsAddon() + devPointsContent);
		gameScript.devPoints_Gesamt = gameScript.devPointsStart_Gesamt;
		gameScript.devAktFeature = -5;
		gameScript.devPoints = (float)devPointsContent;
		gameScript.devPointsStart = gameScript.devPoints;
		taskGame taskGame = this.guiMain_.AddTask_Game();
		taskGame.Init(false);
		taskGame.gameID = gameScript.myID;
		if (this.g_leitenderDesigner && this.g_leitenderDesigner.myID != -1)
		{
			taskGame.leitenderDesignerID = this.g_leitenderDesigner.myID;
			taskGame.designer_ = this.g_leitenderDesigner;
		}
		this.rS_.taskID = taskGame.myID;
		this.guiMain_.CloseMenu();
		this.guiMain_.uiObjects[104].SetActive(false);
		this.guiMain_.uiObjects[246].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0008BE80 File Offset: 0x0008A080
	public int AnzahlLanguages()
	{
		int num = 0;
		for (int i = 0; i < this.sprachen.Length; i++)
		{
			if (this.sprachen[i])
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x0008BEB4 File Offset: 0x0008A0B4
	public void NextSide(int i)
	{
		this.seite += i;
		if (this.seite < 0)
		{
			this.seite = 0;
		}
		if (this.seite > 2)
		{
			this.seite = 2;
		}
		this.OpenSide(this.seite);
		this.sfx_.PlaySound(3, true);
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x0008BF08 File Offset: 0x0008A108
	public void OpenSide(int i)
	{
		this.sfx_.PlaySound(3, false);
		for (int j = 0; j < this.uiSides.Length; j++)
		{
			if (this.uiSides[j].activeSelf && j != i)
			{
				this.uiSides[j].SetActive(false);
			}
		}
		this.seite = i;
		for (int k = 0; k < this.uiObjects[0].transform.childCount; k++)
		{
			this.uiObjects[0].transform.GetChild(k).GetComponent<Image>().color = Color.white;
		}
		this.uiObjects[0].transform.GetChild(i).GetComponent<Image>().color = Color.grey;
		if (!this.uiSides[i].activeSelf)
		{
			this.uiSides[i].SetActive(true);
		}
		if (i == 2)
		{
			base.StartCoroutine(this.iDROPDOWN_SortGameplayFeatures());
		}
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x0008BFEC File Offset: 0x0008A1EC
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[34].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(413));
		list.Add(this.tS_.GetText(1438));
		this.uiObjects[34].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[34].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[34].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x0008C0A8 File Offset: 0x0008A2A8
	private void Init_GameplayFeatures()
	{
		this.FindScripts();
		if (this.g_GameGameplayFeatures.Length == 0)
		{
			this.g_GameGameplayFeatures = new bool[this.gF_.gameplayFeatures_LEVEL.Length];
		}
		for (int i = 0; i < this.uiObjects[35].transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(this.uiObjects[35].transform.GetChild(i).gameObject);
		}
		for (int j = 0; j < this.gF_.gameplayFeatures_LEVEL.Length; j++)
		{
			if (this.gF_.IsErforscht(j))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[35].transform);
				Item_DevAddon_GameplayFeature component = gameObject.GetComponent<Item_DevAddon_GameplayFeature>();
				component.myID = j;
				component.gF_ = this.gF_;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.BUTTON_Click();
				component.BUTTON_Click();
				if (this.gS_.gameplayFeatures_DevDone[j])
				{
					gameObject.GetComponent<Button>().interactable = false;
				}
			}
		}
		this.DROPDOWN_SortGameplayFeatures();
		this.guiMain_.KeinEintrag(this.uiObjects[35], this.uiObjects[36]);
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x000080C2 File Offset: 0x000062C2
	private IEnumerator iDROPDOWN_SortGameplayFeatures()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.DROPDOWN_SortGameplayFeatures();
		yield break;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x0008C210 File Offset: 0x0008A410
	public void DROPDOWN_SortGameplayFeatures()
	{
		int value = this.uiObjects[34].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[34].name, value);
		int childCount = this.uiObjects[35].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[35].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevAddon_GameplayFeature component = gameObject.GetComponent<Item_DevAddon_GameplayFeature>();
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
			this.mS_.SortChildrenByName(this.uiObjects[35]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[35]);
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x000080D1 File Offset: 0x000062D1
	public bool SetGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = !this.g_GameGameplayFeatures[i];
		this.GetDevCosts();
		this.UpdateGesamtGameplayFeatures();
		this.UpdateGUI();
		return this.g_GameGameplayFeatures[i];
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0008C350 File Offset: 0x0008A550
	private int UpdateGesamtGameplayFeatures()
	{
		int num = 0;
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (this.g_GameGameplayFeatures[i] || this.gS_.gameplayFeatures_DevDone[i])
			{
				num++;
			}
		}
		if (this.gS_.gameSize < 4)
		{
			this.uiObjects[37].GetComponent<Text>().text = string.Concat(new string[]
			{
				this.tS_.GetText(410),
				": ",
				num.ToString(),
				" / ",
				this.menuDevGame_.maxFeatures_gameSize[this.gS_.gameSize].ToString()
			});
		}
		else
		{
			this.uiObjects[37].GetComponent<Text>().text = this.tS_.GetText(410) + ": " + num.ToString();
		}
		if (num > this.menuDevGame_.maxFeatures_gameSize[this.gS_.gameSize])
		{
			this.uiObjects[37].GetComponent<Text>().color = Color.red;
		}
		else
		{
			this.uiObjects[37].GetComponent<Text>().color = Color.black;
		}
		return num;
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x0008C490 File Offset: 0x0008A690
	public void BUTTON_AllGameplayFeatures()
	{
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			int num = 0;
			if (num < this.g_GameGameplayFeatures.Length)
			{
				this.DisableAllGameplayFeatures();
				return;
			}
		}
		for (int i = 0; i < this.uiObjects[35].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[35].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				gameObject.GetComponent<Item_DevAddon_GameplayFeature>().BUTTON_Click();
			}
		}
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x0008C518 File Offset: 0x0008A718
	public void BUTTON_AllPassendenGameplayFeatures()
	{
		if (this.gS_.maingenre < 0)
		{
			return;
		}
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			this.DisableAllGameplayFeatures();
			return;
		}
		for (int i = 0; i < this.uiObjects[35].transform.childCount; i++)
		{
			GameObject gameObject = this.uiObjects[35].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_DevAddon_GameplayFeature component = gameObject.GetComponent<Item_DevAddon_GameplayFeature>();
				if (this.gF_.gameplayFeatures_GOOD[component.myID, this.gS_.maingenre] || !this.gF_.gameplayFeatures_BAD[component.myID, this.gS_.maingenre])
				{
					component.BUTTON_Click();
				}
			}
		}
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x0008C5E8 File Offset: 0x0008A7E8
	public void DisableAllGameplayFeatures()
	{
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			this.g_GameGameplayFeatures[i] = false;
		}
		this.GetDevCosts();
		this.UpdateGesamtGameplayFeatures();
		this.UpdateGUI();
		this.sfx_.PlaySound(3, true);
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x0008C634 File Offset: 0x0008A834
	public void SetAP_Gameplay()
	{
		this.g_GameAP_Gameplay = Mathf.RoundToInt(this.uiObjects[42].GetComponent<Slider>().value);
		this.uiObjects[38].GetComponent<Text>().text = (this.g_GameAP_Gameplay * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x0008C694 File Offset: 0x0008A894
	public void SetAP_Grafik()
	{
		this.g_GameAP_Grafik = Mathf.RoundToInt(this.uiObjects[43].GetComponent<Slider>().value);
		this.uiObjects[39].GetComponent<Text>().text = (this.g_GameAP_Grafik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0008C6F4 File Offset: 0x0008A8F4
	public void SetAP_Sound()
	{
		this.g_GameAP_Sound = Mathf.RoundToInt(this.uiObjects[44].GetComponent<Slider>().value);
		this.uiObjects[40].GetComponent<Text>().text = (this.g_GameAP_Sound * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x0008C754 File Offset: 0x0008A954
	public void SetAP_Technik()
	{
		this.g_GameAP_Technik = Mathf.RoundToInt(this.uiObjects[45].GetComponent<Slider>().value);
		this.uiObjects[41].GetComponent<Text>().text = (this.g_GameAP_Technik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0008C7B4 File Offset: 0x0008A9B4
	private int UpdateGesamtArbeitsprioritaet()
	{
		float num = this.uiObjects[42].GetComponent<Slider>().value;
		num += this.uiObjects[43].GetComponent<Slider>().value;
		num += this.uiObjects[44].GetComponent<Slider>().value;
		num += this.uiObjects[45].GetComponent<Slider>().value;
		num *= 5f;
		this.uiObjects[46].GetComponent<Text>().text = Mathf.RoundToInt(num).ToString() + "%";
		if (Mathf.RoundToInt(num) > 100)
		{
			this.uiObjects[46].GetComponent<Text>().color = Color.red;
		}
		else
		{
			this.uiObjects[46].GetComponent<Text>().color = Color.white;
		}
		float num2 = num;
		num2 *= 0.01f;
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		this.uiObjects[47].GetComponent<Image>().fillAmount = num2;
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x0008C8B8 File Offset: 0x0008AAB8
	public int GetGesamtDevPoints()
	{
		int num = this.GetDevPointsContent();
		for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
		{
			if (this.g_GameGameplayFeatures[i] && !this.gS_.gameplayFeatures_DevDone[i])
			{
				num += this.gF_.GetDevPoints(i);
			}
		}
		this.uiObjects[64].GetComponent<Text>().text = this.mS_.GetMoney((long)num, false);
		return num;
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x0008C92C File Offset: 0x0008AB2C
	public int GetDevPointsContent()
	{
		float num = 0f;
		float num2 = (float)this.gS_.GetGesamtDevPoints();
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				num += this.devPointsPercent[i];
			}
		}
		num *= num2;
		return Mathf.RoundToInt(num);
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x00008102 File Offset: 0x00006302
	public void BUTTON_Spielkonzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[109]);
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x0008C980 File Offset: 0x0008AB80
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0000812A File Offset: 0x0000632A
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[111]);
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00008152 File Offset: 0x00006352
	public void BUTTON_Beschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[198]);
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x0008C9D4 File Offset: 0x0008ABD4
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[196]);
		this.guiMain_.uiObjects[196].GetComponent<Menu_Dev_LeitenderDesigner>().Init(this.rS_);
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0008CA2C File Offset: 0x0008AC2C
	public void SetLeitenderDesigner(characterScript charS_, bool manuellSelectet)
	{
		if (charS_ && charS_.roomID != this.rS_.myID)
		{
			charS_ = null;
		}
		if (!charS_)
		{
			float num = 0f;
			GameObject[] array = GameObject.FindGameObjectsWithTag("Character");
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					characterScript component = array[i].GetComponent<characterScript>();
					if (component && component.roomID == this.rS_.myID)
					{
						if (component.s_gamedesign > num)
						{
							num = component.s_gamedesign;
							charS_ = component;
						}
						if (this.rS_.leitenderGamedesigner == component.myID)
						{
							charS_ = component;
							break;
						}
					}
				}
			}
		}
		if (!charS_)
		{
			this.uiObjects[65].GetComponent<Text>().text = "---";
			this.g_leitenderDesigner = null;
			this.rS_.leitenderGamedesigner = -1;
			return;
		}
		this.g_leitenderDesigner = charS_;
		if (this.rS_.leitenderGamedesigner != charS_.myID)
		{
			this.rS_.leitenderGamedesigner = -1;
		}
		if (manuellSelectet)
		{
			this.rS_.leitenderGamedesigner = charS_.myID;
		}
		this.uiObjects[65].GetComponent<Text>().text = charS_.myName;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0008CB60 File Offset: 0x0008AD60
	public void BUTTON_Thema(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[62]);
		this.guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>().Init(i);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0008CBAC File Offset: 0x0008ADAC
	public void SetSubTheme(int i)
	{
		this.g_GameSubTheme = i;
		if (i >= 0)
		{
			this.uiObjects[60].GetComponent<Text>().text = this.tS_.GetThemes(i);
			this.guiMain_.DrawStars(this.uiObjects[61], this.themes_.themes_LEVEL[i]);
			this.uiObjects[59].GetComponent<Image>().sprite = this.guiMain_.uiSprites[6];
			return;
		}
		this.uiObjects[60].GetComponent<Text>().text = "---";
		this.guiMain_.DrawStars(this.uiObjects[61], 0);
		this.uiObjects[59].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
	}

	// Token: 0x04000F91 RID: 3985
	public GameObject[] uiPrefabs;

	// Token: 0x04000F92 RID: 3986
	public GameObject[] uiObjects;

	// Token: 0x04000F93 RID: 3987
	public GameObject[] uiSides;

	// Token: 0x04000F94 RID: 3988
	public float[] devCostsPercent;

	// Token: 0x04000F95 RID: 3989
	public float[] devPointsPercent;

	// Token: 0x04000F96 RID: 3990
	private bool[] buttonAdds = new bool[8];

	// Token: 0x04000F97 RID: 3991
	private bool[] sprachen = new bool[11];

	// Token: 0x04000F98 RID: 3992
	private int seite;

	// Token: 0x04000F99 RID: 3993
	public bool[] g_GameGameplayFeatures;

	// Token: 0x04000F9A RID: 3994
	public characterScript g_leitenderDesigner;

	// Token: 0x04000F9B RID: 3995
	public string g_Beschreibung;

	// Token: 0x04000F9C RID: 3996
	public int g_GameCopyProtect = -1;

	// Token: 0x04000F9D RID: 3997
	public int g_GameAntiCheat = -1;

	// Token: 0x04000F9E RID: 3998
	public int g_GameAP_Gameplay;

	// Token: 0x04000F9F RID: 3999
	public int g_GameAP_Grafik;

	// Token: 0x04000FA0 RID: 4000
	public int g_GameAP_Sound;

	// Token: 0x04000FA1 RID: 4001
	public int g_GameAP_Technik;

	// Token: 0x04000FA2 RID: 4002
	public int g_GameSubTheme = -1;

	// Token: 0x04000FA3 RID: 4003
	public copyProtectScript g_GameCopyProtectScript_;

	// Token: 0x04000FA4 RID: 4004
	public antiCheatScript g_GameAntiCheatScript_;

	// Token: 0x04000FA5 RID: 4005
	private GameObject main_;

	// Token: 0x04000FA6 RID: 4006
	private mainScript mS_;

	// Token: 0x04000FA7 RID: 4007
	private textScript tS_;

	// Token: 0x04000FA8 RID: 4008
	private GUI_Main guiMain_;

	// Token: 0x04000FA9 RID: 4009
	private sfxScript sfx_;

	// Token: 0x04000FAA RID: 4010
	private genres genres_;

	// Token: 0x04000FAB RID: 4011
	private themes themes_;

	// Token: 0x04000FAC RID: 4012
	private licences licences_;

	// Token: 0x04000FAD RID: 4013
	private engineFeatures eF_;

	// Token: 0x04000FAE RID: 4014
	private cameraMovementScript cmS_;

	// Token: 0x04000FAF RID: 4015
	private unlockScript unlock_;

	// Token: 0x04000FB0 RID: 4016
	private gameplayFeatures gF_;

	// Token: 0x04000FB1 RID: 4017
	private games games_;

	// Token: 0x04000FB2 RID: 4018
	private Menu_DevGame menuDevGame_;

	// Token: 0x04000FB3 RID: 4019
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000FB4 RID: 4020
	public gameScript gS_;

	// Token: 0x04000FB5 RID: 4021
	private roomScript rS_;

	// Token: 0x04000FB6 RID: 4022
	private bool allSprachen;

	// Token: 0x04000FB7 RID: 4023
	private bool allAdds;

	// Token: 0x04000FB8 RID: 4024
	private bool allFeatures;
}
