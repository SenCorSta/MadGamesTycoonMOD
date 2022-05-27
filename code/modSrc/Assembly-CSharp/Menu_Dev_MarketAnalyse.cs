using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013A RID: 314
public class Menu_Dev_MarketAnalyse : MonoBehaviour
{
	// Token: 0x06000B6D RID: 2925 RVA: 0x000081CA File Offset: 0x000063CA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x0008CCE8 File Offset: 0x0008AEE8
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
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x000081D2 File Offset: 0x000063D2
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(16);
		}
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0008CE88 File Offset: 0x0008B088
	public void Init(gameScript game_)
	{
		this.FindScripts();
		this.gS_ = game_;
		string text = this.tS_.GetText(441);
		text = text.Replace("<GENRE>", this.genres_.GetName(this.gS_.maingenre));
		text = text.Replace("<THEME>", this.tS_.GetThemes(this.gS_.gameMainTheme));
		this.uiObjects[10].GetComponent<Text>().text = text;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.gS_.maingenre);
		for (int i = 0; i < this.gS_.gamePlatform.Length; i++)
		{
			if (this.gS_.gamePlatform[i] != -1)
			{
				GameObject gameObject = GameObject.Find("PLATFORM_" + this.gS_.gamePlatform[i].ToString());
				if (gameObject)
				{
					platformScript component = gameObject.GetComponent<platformScript>();
					this.uiObjects[6 + i].SetActive(true);
					component.SetPic(this.uiObjects[6 + i]);
					this.uiObjects[6 + i].GetComponent<tooltip>().c = component.GetTooltip();
				}
			}
			else
			{
				this.uiObjects[6 + i].SetActive(false);
			}
		}
		Vector4 amountGamesWithGenreAndTopic = this.games_.GetAmountGamesWithGenreAndTopic(this.gS_);
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.x).ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.y).ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.z).ToString();
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(amountGamesWithGenreAndTopic.w).ToString();
		if (this.gS_.typ_contractGame)
		{
			this.BUTTON_Close();
		}
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0008D0C0 File Offset: 0x0008B2C0
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[195]);
		this.guiMain_.uiObjects[195].GetComponent<Menu_Dev_USK>().Init(this.gS_);
	}

	// Token: 0x04000FBC RID: 4028
	public GameObject[] uiObjects;

	// Token: 0x04000FBD RID: 4029
	private GameObject main_;

	// Token: 0x04000FBE RID: 4030
	private mainScript mS_;

	// Token: 0x04000FBF RID: 4031
	private textScript tS_;

	// Token: 0x04000FC0 RID: 4032
	private GUI_Main guiMain_;

	// Token: 0x04000FC1 RID: 4033
	private sfxScript sfx_;

	// Token: 0x04000FC2 RID: 4034
	private genres genres_;

	// Token: 0x04000FC3 RID: 4035
	private themes themes_;

	// Token: 0x04000FC4 RID: 4036
	private licences licences_;

	// Token: 0x04000FC5 RID: 4037
	private engineFeatures eF_;

	// Token: 0x04000FC6 RID: 4038
	private cameraMovementScript cmS_;

	// Token: 0x04000FC7 RID: 4039
	private unlockScript unlock_;

	// Token: 0x04000FC8 RID: 4040
	private gameplayFeatures gF_;

	// Token: 0x04000FC9 RID: 4041
	private games games_;

	// Token: 0x04000FCA RID: 4042
	private gameScript gS_;
}
