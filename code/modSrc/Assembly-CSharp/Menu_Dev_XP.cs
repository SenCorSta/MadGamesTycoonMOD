using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Dev_XP : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(15);
		}
	}

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
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

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private themes themes_;

	
	private Menu_DevGame mDevGame_;

	
	private genres genres_;

	
	private engineFeatures eF_;

	
	private gameplayFeatures gF_;

	
	private gameScript gS_;

	
	private float time_ = 0.1f;

	
	private bool disableOkButton = true;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;
}
