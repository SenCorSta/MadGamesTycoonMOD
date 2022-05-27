using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000125 RID: 293
public class Menu_Dev_AddonDo : MonoBehaviour
{
	// Token: 0x06000A27 RID: 2599 RVA: 0x00007549 File Offset: 0x00005749
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0007FD44 File Offset: 0x0007DF44
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

	// Token: 0x06000A29 RID: 2601 RVA: 0x00007551 File Offset: 0x00005751
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00007565 File Offset: 0x00005765
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00007579 File Offset: 0x00005779
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0007FF28 File Offset: 0x0007E128
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
		this.forschungSonstiges_.Unlock(35, this.uiObjects[76], null);
		this.UpdateGUI();
		this.OpenSide(0);
		this.DROPDOWN_AddonTyp();
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00080678 File Offset: 0x0007E878
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

	// Token: 0x06000A2E RID: 2606 RVA: 0x000806CC File Offset: 0x0007E8CC
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

	// Token: 0x06000A2F RID: 2607 RVA: 0x000807CC File Offset: 0x0007E9CC
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

	// Token: 0x06000A30 RID: 2608 RVA: 0x00080808 File Offset: 0x0007EA08
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

	// Token: 0x06000A31 RID: 2609 RVA: 0x00007594 File Offset: 0x00005794
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00080940 File Offset: 0x0007EB40
	public void BUTTON_Adds(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.buttonAdds[i] = !this.buttonAdds[i];
		if (this.uiObjects[63].GetComponent<Dropdown>().value == 1)
		{
			this.buttonAdds[7] = true;
		}
		this.UpdateGUI();
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x000075AF File Offset: 0x000057AF
	public void BUTTON_Sprache(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.sprachen[i] = !this.sprachen[i];
		this.UpdateGUI();
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00080994 File Offset: 0x0007EB94
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

	// Token: 0x06000A35 RID: 2613 RVA: 0x000809E4 File Offset: 0x0007EBE4
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

	// Token: 0x06000A36 RID: 2614 RVA: 0x000075D7 File Offset: 0x000057D7
	public void BUTTON_CopyProtect()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[194]);
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x00007602 File Offset: 0x00005802
	public void BUTTON_CopyProtectKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[49]);
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0000762A File Offset: 0x0000582A
	public void BUTTON_AntiCheat()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[240]);
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00007655 File Offset: 0x00005855
	public void BUTTON_AntiCheatKaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[234]);
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00007680 File Offset: 0x00005880
	public void SetBeschreibung(string c)
	{
		this.g_Beschreibung = c;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00080A34 File Offset: 0x0007EC34
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

	// Token: 0x06000A3C RID: 2620 RVA: 0x00080AB8 File Offset: 0x0007ECB8
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

	// Token: 0x06000A3D RID: 2621 RVA: 0x00080B3C File Offset: 0x0007ED3C
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

	// Token: 0x06000A3E RID: 2622 RVA: 0x00080CD4 File Offset: 0x0007EED4
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

	// Token: 0x06000A3F RID: 2623 RVA: 0x00080E6C File Offset: 0x0007F06C
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

	// Token: 0x06000A40 RID: 2624 RVA: 0x00080EE0 File Offset: 0x0007F0E0
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
		bool flag = false;
		for (int i = 0; i < this.buttonAdds.Length; i++)
		{
			if (this.buttonAdds[i])
			{
				flag = true;
			}
		}
		if (!flag)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(972), false);
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
		int num = Mathf.RoundToInt((float)this.GetDevCosts());
		this.mS_.Pay((long)num, 18);
		gameScript gameScript = this.games_.CreateNewGame(false, true);
		gameScript.ownerID = this.mS_.myID;
		gameScript.mainIP = this.gS_.mainIP;
		gameScript.costs_entwicklung = (long)num;
		gameScript.playerGame = true;
		if (this.mS_.multiplayer)
		{
			gameScript.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
		gameScript.inDevelopment = true;
		gameScript.SetMyName(this.uiObjects[12].GetComponent<InputField>().text);
		gameScript.originalIP = this.gS_.myID;
		gameScript.developerID = -1;
		gameScript.addonQuality = this.GetAddonQuality();
		gameScript.beschreibung = this.g_Beschreibung;
		if (this.uiObjects[63].GetComponent<Dropdown>().value == 1)
		{
			gameScript.typ_addonStandalone = true;
		}
		else
		{
			gameScript.typ_addon = true;
		}
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
		gameScript.gameTyp = this.gS_.gameTyp;
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
		for (int num2 = 0; num2 < this.gS_.gamePlatform.Length; num2++)
		{
			gameScript.gamePlatform[num2] = this.gS_.gamePlatform[num2];
		}
		for (int num3 = 0; num3 < this.gS_.gameEngineFeature.Length; num3++)
		{
			gameScript.gameEngineFeature[num3] = this.gS_.gameEngineFeature[num3];
			gameScript.engineFeature_DevDone[num3] = this.gS_.engineFeature_DevDone[num3];
		}
		for (int num4 = 0; num4 < this.gS_.gameplayStudio.Length; num4++)
		{
			gameScript.gameplayStudio[num4] = this.gS_.gameplayStudio[num4];
		}
		for (int num5 = 0; num5 < this.gS_.grafikStudio.Length; num5++)
		{
			gameScript.grafikStudio[num5] = this.gS_.grafikStudio[num5];
		}
		for (int num6 = 0; num6 < this.gS_.soundStudio.Length; num6++)
		{
			gameScript.soundStudio[num6] = this.gS_.soundStudio[num6];
		}
		for (int num7 = 0; num7 < this.gS_.motionCaptureStudio.Length; num7++)
		{
			gameScript.motionCaptureStudio[num7] = this.gS_.motionCaptureStudio[num7];
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
		this.guiMain_.uiObjects[189].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x000816B4 File Offset: 0x0007F8B4
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

	// Token: 0x06000A42 RID: 2626 RVA: 0x000816E8 File Offset: 0x0007F8E8
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

	// Token: 0x06000A43 RID: 2627 RVA: 0x0008173C File Offset: 0x0007F93C
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

	// Token: 0x06000A44 RID: 2628 RVA: 0x00081820 File Offset: 0x0007FA20
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[34].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(6));
		list.Add(this.tS_.GetText(413));
		list.Add(this.tS_.GetText(1438));
		this.uiObjects[34].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[34].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[34].GetComponent<Dropdown>().value = @int;
		@int = PlayerPrefs.GetInt(this.uiObjects[63].name);
		list.Clear();
		list.Add(this.tS_.GetText(963));
		list.Add(this.tS_.GetText(979));
		this.uiObjects[63].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[63].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[63].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x00081958 File Offset: 0x0007FB58
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

	// Token: 0x06000A46 RID: 2630 RVA: 0x00081AC0 File Offset: 0x0007FCC0
	public void DROPDOWN_AddonTyp()
	{
		int value = this.uiObjects[63].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[63].name, value);
		if (this.uiObjects[63].GetComponent<Dropdown>().value == 1)
		{
			this.buttonAdds[7] = true;
			this.UpdateGUI();
		}
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x00007689 File Offset: 0x00005889
	private IEnumerator iDROPDOWN_SortGameplayFeatures()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.DROPDOWN_SortGameplayFeatures();
		yield break;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x00081B1C File Offset: 0x0007FD1C
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

	// Token: 0x06000A49 RID: 2633 RVA: 0x00007698 File Offset: 0x00005898
	public bool SetGameplayFeature(int i)
	{
		this.g_GameGameplayFeatures[i] = !this.g_GameGameplayFeatures[i];
		this.GetDevCosts();
		this.UpdateGesamtGameplayFeatures();
		this.UpdateGUI();
		return this.g_GameGameplayFeatures[i];
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x00081C5C File Offset: 0x0007FE5C
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

	// Token: 0x06000A4B RID: 2635 RVA: 0x00081D9C File Offset: 0x0007FF9C
	public void BUTTON_AllGameplayFeatures()
	{
		this.allFeatures = !this.allFeatures;
		if (!this.allFeatures)
		{
			for (int i = 0; i < this.g_GameGameplayFeatures.Length; i++)
			{
				this.g_GameGameplayFeatures[i] = false;
			}
		}
		for (int j = 0; j < this.uiObjects[35].transform.childCount; j++)
		{
			GameObject gameObject = this.uiObjects[35].transform.GetChild(j).gameObject;
			if (gameObject)
			{
				gameObject.GetComponent<Item_DevAddon_GameplayFeature>().BUTTON_Click();
			}
		}
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x00081E28 File Offset: 0x00080028
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

	// Token: 0x06000A4D RID: 2637 RVA: 0x00081EF8 File Offset: 0x000800F8
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

	// Token: 0x06000A4E RID: 2638 RVA: 0x00081F44 File Offset: 0x00080144
	public void SetAP_Gameplay()
	{
		this.g_GameAP_Gameplay = Mathf.RoundToInt(this.uiObjects[42].GetComponent<Slider>().value);
		this.uiObjects[38].GetComponent<Text>().text = (this.g_GameAP_Gameplay * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00081FA4 File Offset: 0x000801A4
	public void SetAP_Grafik()
	{
		this.g_GameAP_Grafik = Mathf.RoundToInt(this.uiObjects[43].GetComponent<Slider>().value);
		this.uiObjects[39].GetComponent<Text>().text = (this.g_GameAP_Grafik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00082004 File Offset: 0x00080204
	public void SetAP_Sound()
	{
		this.g_GameAP_Sound = Mathf.RoundToInt(this.uiObjects[44].GetComponent<Slider>().value);
		this.uiObjects[40].GetComponent<Text>().text = (this.g_GameAP_Sound * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00082064 File Offset: 0x00080264
	public void SetAP_Technik()
	{
		this.g_GameAP_Technik = Mathf.RoundToInt(this.uiObjects[45].GetComponent<Slider>().value);
		this.uiObjects[41].GetComponent<Text>().text = (this.g_GameAP_Technik * 5).ToString() + "%";
		this.UpdateGesamtArbeitsprioritaet();
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x000820C4 File Offset: 0x000802C4
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

	// Token: 0x06000A53 RID: 2643 RVA: 0x000821C8 File Offset: 0x000803C8
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

	// Token: 0x06000A54 RID: 2644 RVA: 0x0008223C File Offset: 0x0008043C
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

	// Token: 0x06000A55 RID: 2645 RVA: 0x000076C9 File Offset: 0x000058C9
	public void BUTTON_Spielkonzepte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[109]);
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00082290 File Offset: 0x00080490
	public void BUTTON_Spielberichte()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[182]);
		this.guiMain_.uiObjects[182].GetComponent<Menu_QA_ShowSpielberichtSelectGame>().Init();
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x000076F1 File Offset: 0x000058F1
	public void BUTTON_Fanbriefe()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[111]);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00007719 File Offset: 0x00005919
	public void BUTTON_Beschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[198]);
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x000822E4 File Offset: 0x000804E4
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[196]);
		this.guiMain_.uiObjects[196].GetComponent<Menu_Dev_LeitenderDesigner>().Init(this.rS_);
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0008233C File Offset: 0x0008053C
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

	// Token: 0x06000A5B RID: 2651 RVA: 0x00082470 File Offset: 0x00080670
	public void BUTTON_Thema(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[62]);
		this.guiMain_.uiObjects[62].GetComponent<Menu_DevGame_Theme>().Init(i);
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x000824BC File Offset: 0x000806BC
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

	// Token: 0x04000E77 RID: 3703
	public GameObject[] uiPrefabs;

	// Token: 0x04000E78 RID: 3704
	public GameObject[] uiObjects;

	// Token: 0x04000E79 RID: 3705
	public GameObject[] uiSides;

	// Token: 0x04000E7A RID: 3706
	public float[] devCostsPercent;

	// Token: 0x04000E7B RID: 3707
	public float[] devPointsPercent;

	// Token: 0x04000E7C RID: 3708
	private bool[] buttonAdds = new bool[8];

	// Token: 0x04000E7D RID: 3709
	private bool[] sprachen = new bool[11];

	// Token: 0x04000E7E RID: 3710
	private int seite;

	// Token: 0x04000E7F RID: 3711
	public bool[] g_GameGameplayFeatures;

	// Token: 0x04000E80 RID: 3712
	public bool[] g_GameLanguage;

	// Token: 0x04000E81 RID: 3713
	public characterScript g_leitenderDesigner;

	// Token: 0x04000E82 RID: 3714
	public string g_Beschreibung;

	// Token: 0x04000E83 RID: 3715
	public int g_GameCopyProtect = -1;

	// Token: 0x04000E84 RID: 3716
	public int g_GameAntiCheat = -1;

	// Token: 0x04000E85 RID: 3717
	public int g_GameAP_Gameplay;

	// Token: 0x04000E86 RID: 3718
	public int g_GameAP_Grafik;

	// Token: 0x04000E87 RID: 3719
	public int g_GameAP_Sound;

	// Token: 0x04000E88 RID: 3720
	public int g_GameAP_Technik;

	// Token: 0x04000E89 RID: 3721
	public int g_GameSubTheme = -1;

	// Token: 0x04000E8A RID: 3722
	public copyProtectScript g_GameCopyProtectScript_;

	// Token: 0x04000E8B RID: 3723
	public antiCheatScript g_GameAntiCheatScript_;

	// Token: 0x04000E8C RID: 3724
	private GameObject main_;

	// Token: 0x04000E8D RID: 3725
	private mainScript mS_;

	// Token: 0x04000E8E RID: 3726
	private textScript tS_;

	// Token: 0x04000E8F RID: 3727
	private GUI_Main guiMain_;

	// Token: 0x04000E90 RID: 3728
	private sfxScript sfx_;

	// Token: 0x04000E91 RID: 3729
	private genres genres_;

	// Token: 0x04000E92 RID: 3730
	private themes themes_;

	// Token: 0x04000E93 RID: 3731
	private licences licences_;

	// Token: 0x04000E94 RID: 3732
	private engineFeatures eF_;

	// Token: 0x04000E95 RID: 3733
	private cameraMovementScript cmS_;

	// Token: 0x04000E96 RID: 3734
	private unlockScript unlock_;

	// Token: 0x04000E97 RID: 3735
	private gameplayFeatures gF_;

	// Token: 0x04000E98 RID: 3736
	private games games_;

	// Token: 0x04000E99 RID: 3737
	private Menu_DevGame menuDevGame_;

	// Token: 0x04000E9A RID: 3738
	private forschungSonstiges forschungSonstiges_;

	// Token: 0x04000E9B RID: 3739
	public gameScript gS_;

	// Token: 0x04000E9C RID: 3740
	private roomScript rS_;

	// Token: 0x04000E9D RID: 3741
	private bool allSprachen;

	// Token: 0x04000E9E RID: 3742
	private bool allAdds;

	// Token: 0x04000E9F RID: 3743
	private bool allFeatures;
}
