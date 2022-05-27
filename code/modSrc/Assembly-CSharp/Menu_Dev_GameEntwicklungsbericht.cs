using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000137 RID: 311
public class Menu_Dev_GameEntwicklungsbericht : MonoBehaviour
{
	// Token: 0x06000B27 RID: 2855 RVA: 0x00078517 File Offset: 0x00076717
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00078520 File Offset: 0x00076720
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
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
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x000786CC File Offset: 0x000768CC
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x000786E0 File Offset: 0x000768E0
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x000786F4 File Offset: 0x000768F4
	public void Init(gameScript game_, roomScript room_)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.rS_ = room_;
		this.SetLeitenderDesigner(this.GetLeitenderEntwickler(), false);
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
		this.uiObjects[0].GetComponent<InputField>().text = text;
		this.uiObjects[22].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[0].SetActive(true);
		this.uiObjects[23].SetActive(false);
		this.uiObjects[20].GetComponent<Image>().sprite = this.games_.gameSizeSprites[this.gS_.gameSize];
		string text2 = this.genres_.GetName(this.gS_.maingenre);
		if (this.gS_.subgenre != -1)
		{
			text2 = text2 + " / " + this.genres_.GetName(this.gS_.subgenre);
		}
		text2 = text2 + "\n" + this.tS_.GetThemes(this.gS_.gameMainTheme);
		if (this.gS_.gameSubTheme != -1)
		{
			text2 = text2 + " / " + this.tS_.GetThemes(this.gS_.gameSubTheme);
		}
		this.uiObjects[1].GetComponent<Text>().text = text2;
		this.uiObjects[2].GetComponent<Image>().fillAmount = this.gS_.GetProzentGesamt() * 0.01f;
		this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(450) + " " + this.mS_.Round(this.gS_.GetProzentGesamt(), 1).ToString() + "%";
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(6) + " <color=red>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
		text2 = this.tS_.GetText(1775);
		text2 = text2.Replace("<NUM>", this.gS_.weeksInDevelopment.ToString());
		this.uiObjects[31].GetComponent<Text>().text = text2 + " <color=grey> [" + this.gS_.GetEntwicklungsbeginnDateString() + "]</color>";
		this.gS_.CalcReview(true);
		int num = this.gS_.reviewTotal - 10;
		int num2 = this.gS_.reviewTotal + 10;
		num = num / 10 * 10;
		num2 = num2 / 10 * 10;
		if (num < 1)
		{
			num = 1;
		}
		if (num2 > 100)
		{
			num2 = 100;
		}
		text2 = string.Concat(new string[]
		{
			" ",
			num.ToString(),
			"% - ",
			num2.ToString(),
			"%"
		});
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(452) + "<color=blue>" + text2 + "</color>";
		this.gS_.ClearReview();
		this.uiObjects[6].GetComponent<Image>().sprite = this.gS_.GetScreenshot();
		this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_gameplay).ToString();
		this.uiObjects[8].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_grafik).ToString();
		this.uiObjects[9].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_sound).ToString();
		this.uiObjects[10].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_technik).ToString();
		this.uiObjects[11].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_bugs).ToString();
		this.uiObjects[12].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.hype).ToString();
		for (int i = 0; i < this.gS_.gamePlatform.Length; i++)
		{
			if (this.gS_.gamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.gS_.gamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					this.uiObjects[13 + i].SetActive(true);
					component.SetPic(this.uiObjects[13 + i]);
					this.uiObjects[13 + i].GetComponent<tooltip>().c = component.GetTooltip();
				}
			}
			else
			{
				this.uiObjects[13 + i].SetActive(false);
			}
		}
		this.uiObjects[17].GetComponent<Component_Aufwertungen>().Init(this.gS_);
		this.uiObjects[18].GetComponent<Image>().sprite = this.gS_.GetTypSprite();
		this.uiObjects[24].GetComponent<tooltip>().c = this.gS_.GetTypString();
		this.uiObjects[19].GetComponent<Image>().sprite = this.gS_.GetPlatformTypSprite();
		this.uiObjects[25].GetComponent<tooltip>().c = this.gS_.GetPlatformTypString();
		if (this.gS_.typ_contractGame)
		{
			this.uiObjects[0].GetComponent<InputField>().interactable = false;
		}
		else
		{
			this.uiObjects[0].GetComponent<InputField>().interactable = true;
		}
		this.ShowContractDaten();
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00078E04 File Offset: 0x00077004
	private void ShowContractDaten()
	{
		if (this.gS_.typ_contractGame)
		{
			this.uiObjects[26].SetActive(true);
			string text = this.tS_.GetText(605);
			text = text.Replace("<NUM>", this.gS_.auftragsspiel_zeitInWochen.ToString());
			this.uiObjects[27].GetComponent<Text>().text = text;
			text = this.tS_.GetText(626);
			text = text.Replace("<NUM>", this.gS_.auftragsspiel_mindestbewertung.ToString());
			this.uiObjects[28].GetComponent<Text>().text = text;
			this.uiObjects[29].GetComponent<Text>().text = this.tS_.GetText(600) + ": " + this.mS_.GetMoney((long)this.gS_.auftragsspiel_gehalt, true);
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(627) + ": " + this.mS_.GetMoney((long)this.gS_.auftragsspiel_bonus, true);
			return;
		}
		this.uiObjects[26].SetActive(false);
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00078F4C File Offset: 0x0007714C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00078F74 File Offset: 0x00077174
	public void BUTTON_Yes()
	{
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(412), false);
			return;
		}
		if (!this.gS_.typ_contractGame && this.gS_.portID == -1)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					gameScript component = array[i].GetComponent<gameScript>();
					if (component && this.gS_.myID != component.myID && component.GetNameSimple() == this.uiObjects[0].GetComponent<InputField>().text)
					{
						this.guiMain_.MessageBox(this.tS_.GetText(618), false);
						return;
					}
				}
			}
		}
		if (this.uiObjects[0].activeSelf && this.uiObjects[0].GetComponent<InputField>().text.Length > 0 && this.uiObjects[0].GetComponent<InputField>().interactable)
		{
			this.gS_.SetMyName(this.uiObjects[0].GetComponent<InputField>().text);
		}
		this.BUTTON_Close();
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000790B4 File Offset: 0x000772B4
	public void SetLeitenderDesigner(characterScript charS_, bool manuellSelectet)
	{
		taskGame taskGame = null;
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
		if (gameObject)
		{
			taskGame = gameObject.GetComponent<taskGame>();
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
			this.uiObjects[21].GetComponent<Text>().text = "---";
			taskGame.leitenderDesignerID = -1;
			taskGame.designer_ = null;
			this.rS_.leitenderGamedesigner = -1;
			return;
		}
		this.uiObjects[21].GetComponent<Text>().text = charS_.myName;
		taskGame.leitenderDesignerID = charS_.myID;
		taskGame.designer_ = charS_;
		if (this.rS_.leitenderGamedesigner != charS_.myID)
		{
			this.rS_.leitenderGamedesigner = -1;
		}
		if (manuellSelectet)
		{
			this.rS_.leitenderGamedesigner = charS_.myID;
		}
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00079220 File Offset: 0x00077420
	public characterScript GetLeitenderEntwickler()
	{
		GameObject gameObject = GameObject.Find("Task_" + this.rS_.taskID.ToString());
		if (gameObject)
		{
			taskGame component = gameObject.GetComponent<taskGame>();
			if (component)
			{
				return component.designer_;
			}
		}
		return null;
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x0007926C File Offset: 0x0007746C
	public string GetBeschreibung()
	{
		return this.gS_.beschreibung;
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00079279 File Offset: 0x00077479
	public void SetBeschreibung(string c)
	{
		this.gS_.beschreibung = c;
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00079288 File Offset: 0x00077488
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[196]);
		this.guiMain_.uiObjects[196].GetComponent<Menu_Dev_LeitenderDesigner>().Init(this.rS_);
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x000792DF File Offset: 0x000774DF
	public void BUTTON_Spielbeschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[198]);
	}

	// Token: 0x04000F79 RID: 3961
	public GameObject[] uiObjects;

	// Token: 0x04000F7A RID: 3962
	private GameObject main_;

	// Token: 0x04000F7B RID: 3963
	private mainScript mS_;

	// Token: 0x04000F7C RID: 3964
	private textScript tS_;

	// Token: 0x04000F7D RID: 3965
	private GUI_Main guiMain_;

	// Token: 0x04000F7E RID: 3966
	private sfxScript sfx_;

	// Token: 0x04000F7F RID: 3967
	private genres genres_;

	// Token: 0x04000F80 RID: 3968
	private themes themes_;

	// Token: 0x04000F81 RID: 3969
	private licences licences_;

	// Token: 0x04000F82 RID: 3970
	private engineFeatures eF_;

	// Token: 0x04000F83 RID: 3971
	private cameraMovementScript cmS_;

	// Token: 0x04000F84 RID: 3972
	private unlockScript unlock_;

	// Token: 0x04000F85 RID: 3973
	private gameplayFeatures gF_;

	// Token: 0x04000F86 RID: 3974
	private games games_;

	// Token: 0x04000F87 RID: 3975
	private gameScript gS_;

	// Token: 0x04000F88 RID: 3976
	private roomScript rS_;
}
