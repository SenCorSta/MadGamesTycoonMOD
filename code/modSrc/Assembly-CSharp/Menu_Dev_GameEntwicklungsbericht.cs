using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000136 RID: 310
public class Menu_Dev_GameEntwicklungsbericht : MonoBehaviour
{
	// Token: 0x06000B14 RID: 2836 RVA: 0x00007E4A File Offset: 0x0000604A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0008859C File Offset: 0x0008679C
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

	// Token: 0x06000B16 RID: 2838 RVA: 0x00007E52 File Offset: 0x00006052
	private void OnEnable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = true;
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00007E66 File Offset: 0x00006066
	private void OnDisable()
	{
		this.FindScripts();
		this.cmS_.disableMovement = false;
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00088748 File Offset: 0x00086948
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

	// Token: 0x06000B19 RID: 2841 RVA: 0x00088E58 File Offset: 0x00087058
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

	// Token: 0x06000B1A RID: 2842 RVA: 0x00007E7A File Offset: 0x0000607A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00088FA0 File Offset: 0x000871A0
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

	// Token: 0x06000B1C RID: 2844 RVA: 0x000890E0 File Offset: 0x000872E0
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

	// Token: 0x06000B1D RID: 2845 RVA: 0x0008924C File Offset: 0x0008744C
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

	// Token: 0x06000B1E RID: 2846 RVA: 0x00007EA0 File Offset: 0x000060A0
	public string GetBeschreibung()
	{
		return this.gS_.beschreibung;
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00007EAD File Offset: 0x000060AD
	public void SetBeschreibung(string c)
	{
		this.gS_.beschreibung = c;
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00089298 File Offset: 0x00087498
	public void BUTTON_LeitenderEntwickler()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[196]);
		this.guiMain_.uiObjects[196].GetComponent<Menu_Dev_LeitenderDesigner>().Init(this.rS_);
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00007EBB File Offset: 0x000060BB
	public void BUTTON_Spielbeschreibung()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[198]);
	}

	// Token: 0x04000F71 RID: 3953
	public GameObject[] uiObjects;

	// Token: 0x04000F72 RID: 3954
	private GameObject main_;

	// Token: 0x04000F73 RID: 3955
	private mainScript mS_;

	// Token: 0x04000F74 RID: 3956
	private textScript tS_;

	// Token: 0x04000F75 RID: 3957
	private GUI_Main guiMain_;

	// Token: 0x04000F76 RID: 3958
	private sfxScript sfx_;

	// Token: 0x04000F77 RID: 3959
	private genres genres_;

	// Token: 0x04000F78 RID: 3960
	private themes themes_;

	// Token: 0x04000F79 RID: 3961
	private licences licences_;

	// Token: 0x04000F7A RID: 3962
	private engineFeatures eF_;

	// Token: 0x04000F7B RID: 3963
	private cameraMovementScript cmS_;

	// Token: 0x04000F7C RID: 3964
	private unlockScript unlock_;

	// Token: 0x04000F7D RID: 3965
	private gameplayFeatures gF_;

	// Token: 0x04000F7E RID: 3966
	private games games_;

	// Token: 0x04000F7F RID: 3967
	private gameScript gS_;

	// Token: 0x04000F80 RID: 3968
	private roomScript rS_;
}
