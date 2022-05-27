using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200014E RID: 334
public class Menu_Dev_XP : MonoBehaviour
{
	// Token: 0x06000C43 RID: 3139 RVA: 0x00008975 File Offset: 0x00006B75
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00094054 File Offset: 0x00092254
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0000897D File Offset: 0x00006B7D
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(15);
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0000899F File Offset: 0x00006B9F
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0009419C File Offset: 0x0009239C
	public void Init(gameScript game_)
	{
		this.FindScripts();
		if (game_.gameTyp == 1 && game_.aboPreis <= 0)
		{
			this.guiMain_.uiObjects[242].SetActive(true);
			this.guiMain_.uiObjects[242].GetComponent<Menu_Abo_Preis>().Init(game_);
			base.gameObject.SetActive(false);
			return;
		}
		if (game_.pubOffer)
		{
			this.disableOkButton = false;
			this.gS_ = game_;
			this.BUTTON_Close();
			return;
		}
		this.disableOkButton = true;
		this.gS_ = game_;
		base.StartCoroutine(this.CreateItems());
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x000089D1 File Offset: 0x00006BD1
	private IEnumerator CreateItems()
	{
		if (this.gS_.publisherID != -1)
		{
			this.gS_.FindMyPublisher();
			if (this.gS_.pS_ && this.gS_.pS_.GetRelation() < 100f && !this.gS_.pS_.IsMyTochterfirma() && this.gS_.reviewTotal >= 20 && ((!this.gS_.typ_addon && !this.gS_.typ_addonStandalone && !this.gS_.typ_mmoaddon) || UnityEngine.Random.Range(0, 100) < 30) && UnityEngine.Random.Range(0, 80) < this.gS_.reviewTotal)
			{
				this.gS_.pS_.relation += 20f;
				Item_DevGame_PublisherBeziehung component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_PublisherBeziehung>();
				component.guiMain_ = this.guiMain_;
				component.SetData(this.gS_.pS_.GetName(), this.gS_.pS_.GetLogo(), Mathf.RoundToInt(this.gS_.pS_.GetRelation() / 20f));
				if (this.gS_.pS_.GetRelation() >= 100f)
				{
					this.gS_.pS_.relation = 100f;
					if (this.mS_.achScript_)
					{
						this.mS_.achScript_.SetAchivement(47);
					}
				}
				if (this.time_ > 0f)
				{
					this.sfx_.PlaySound(38, true);
					yield return new WaitForSeconds(this.time_);
				}
			}
		}
		if (UnityEngine.Random.Range(0, 100) > 90)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j])
				{
					publisherScript component2 = array[j].GetComponent<publisherScript>();
					if (component2 && component2.myID != this.gS_.publisherID && !component2.IsMyTochterfirma() && component2.GetRelation() > 0f)
					{
						component2.relation -= 20f;
						Item_DevGame_PublisherBeziehung component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[2], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_PublisherBeziehung>();
						component3.guiMain_ = this.guiMain_;
						component3.SetData(component2.GetName(), component2.GetLogo(), Mathf.RoundToInt(component2.GetRelation() / 20f));
						if (this.time_ > 0f)
						{
							this.sfx_.PlaySound(24, true);
							yield return new WaitForSeconds(this.time_);
							break;
						}
						break;
					}
				}
			}
		}
		if (this.genres_.genres_LEVEL[this.gS_.maingenre] < 5)
		{
			int maingenre = this.gS_.maingenre;
			this.genres_.genres_LEVEL[maingenre]++;
			Item_DevGame_XP component4 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
			component4.guiMain_ = this.guiMain_;
			component4.SetData(this.genres_.GetName(maingenre), this.genres_.GetPic(maingenre), this.genres_.genres_LEVEL[maingenre]);
			if (this.time_ > 0f)
			{
				this.sfx_.PlaySound(38, true);
				yield return new WaitForSeconds(this.time_);
			}
		}
		if (this.gS_.subgenre != -1 && this.genres_.genres_LEVEL[this.gS_.subgenre] < 5)
		{
			int subgenre = this.gS_.subgenre;
			this.genres_.genres_LEVEL[subgenre]++;
			Item_DevGame_XP component5 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
			component5.guiMain_ = this.guiMain_;
			component5.SetData(this.genres_.GetName(subgenre), this.genres_.GetPic(subgenre), this.genres_.genres_LEVEL[subgenre]);
			if (this.time_ > 0f)
			{
				this.sfx_.PlaySound(38, true);
				yield return new WaitForSeconds(this.time_);
			}
		}
		if (this.themes_.themes_LEVEL[this.gS_.gameMainTheme] < 5)
		{
			int gameMainTheme = this.gS_.gameMainTheme;
			this.themes_.themes_LEVEL[gameMainTheme]++;
			Item_DevGame_XP component6 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
			component6.guiMain_ = this.guiMain_;
			component6.SetData(this.tS_.GetThemes(gameMainTheme), this.guiMain_.uiSprites[6], this.themes_.themes_LEVEL[gameMainTheme]);
			if (this.time_ > 0f)
			{
				this.sfx_.PlaySound(38, true);
				yield return new WaitForSeconds(this.time_);
			}
		}
		if (this.gS_.gameSubTheme != -1 && this.themes_.themes_LEVEL[this.gS_.gameSubTheme] < 5)
		{
			int gameSubTheme = this.gS_.gameSubTheme;
			this.themes_.themes_LEVEL[gameSubTheme]++;
			Item_DevGame_XP component7 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
			component7.guiMain_ = this.guiMain_;
			component7.SetData(this.tS_.GetThemes(gameSubTheme), this.guiMain_.uiSprites[6], this.themes_.themes_LEVEL[gameSubTheme]);
			if (this.time_ > 0f)
			{
				this.sfx_.PlaySound(38, true);
				yield return new WaitForSeconds(this.time_);
			}
		}
		int num;
		for (int i = 0; i < this.gS_.gamePlatform.Length; i = num + 1)
		{
			if (this.gS_.gamePlatform[i] != -1)
			{
				platformScript component8 = GameObject.Find("PLATFORM_" + this.gS_.gamePlatform[i].ToString()).GetComponent<platformScript>();
				if (component8.erfahrung < 5)
				{
					component8.erfahrung++;
					Item_DevGame_XP component9 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
					component9.guiMain_ = this.guiMain_;
					component9.SetData(component8.GetName(), null, component8.erfahrung);
					component8.SetPic(component9.uiObjects[1]);
					if (this.time_ > 0f)
					{
						this.sfx_.PlaySound(38, true);
						yield return new WaitForSeconds(this.time_);
					}
				}
			}
			num = i;
		}
		for (int i = 0; i < this.gS_.gameEngineFeature.Length; i = num + 1)
		{
			if (this.gS_.gameEngineFeature[i] != -1)
			{
				int num2 = this.gS_.gameEngineFeature[i];
				if (this.eF_.engineFeatures_LEVEL[num2] < 5)
				{
					this.eF_.engineFeatures_LEVEL[num2]++;
					Item_DevGame_XP component10 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
					component10.guiMain_ = this.guiMain_;
					component10.SetData(this.eF_.GetName(num2), this.eF_.GetTypPic(num2), this.eF_.engineFeatures_LEVEL[num2]);
					if (this.time_ > 0f)
					{
						this.sfx_.PlaySound(38, true);
						yield return new WaitForSeconds(this.time_);
					}
				}
			}
			num = i;
		}
		for (int i = 0; i < this.gS_.gameGameplayFeatures.Length; i = num + 1)
		{
			if (this.gS_.gameGameplayFeatures[i] && this.gF_.gameplayFeatures_LEVEL[i] < 5)
			{
				this.gF_.gameplayFeatures_LEVEL[i]++;
				Item_DevGame_XP component11 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_XP>();
				component11.guiMain_ = this.guiMain_;
				component11.SetData(this.gF_.GetName(i), this.gF_.GetTypSprite(i), this.gF_.gameplayFeatures_LEVEL[i]);
				if (this.time_ > 0f)
				{
					this.sfx_.PlaySound(38, true);
					yield return new WaitForSeconds(this.time_);
				}
			}
			num = i;
		}
		this.disableOkButton = false;
		this.time_ = 0.1f;
		yield break;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00094254 File Offset: 0x00092454
	public void BUTTON_Close()
	{
		if (this.disableOkButton)
		{
			this.time_ = 0f;
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[72]);
		this.guiMain_.uiObjects[72].GetComponent<Menu_Dev_MarketAnalyse>().Init(this.gS_);
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400109D RID: 4253
	private mainScript mS_;

	// Token: 0x0400109E RID: 4254
	private GameObject main_;

	// Token: 0x0400109F RID: 4255
	private GUI_Main guiMain_;

	// Token: 0x040010A0 RID: 4256
	private sfxScript sfx_;

	// Token: 0x040010A1 RID: 4257
	private textScript tS_;

	// Token: 0x040010A2 RID: 4258
	private themes themes_;

	// Token: 0x040010A3 RID: 4259
	private Menu_DevGame mDevGame_;

	// Token: 0x040010A4 RID: 4260
	private genres genres_;

	// Token: 0x040010A5 RID: 4261
	private engineFeatures eF_;

	// Token: 0x040010A6 RID: 4262
	private gameplayFeatures gF_;

	// Token: 0x040010A7 RID: 4263
	private gameScript gS_;

	// Token: 0x040010A8 RID: 4264
	private float time_ = 0.1f;

	// Token: 0x040010A9 RID: 4265
	private bool disableOkButton = true;

	// Token: 0x040010AA RID: 4266
	public GameObject[] uiPrefabs;

	// Token: 0x040010AB RID: 4267
	public GameObject[] uiObjects;
}
