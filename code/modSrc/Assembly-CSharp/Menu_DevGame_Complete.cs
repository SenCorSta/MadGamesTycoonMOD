using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_DevGame_Complete : MonoBehaviour
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
	}

	
	private void OnEnable()
	{
		this.FindScripts();
		if (!this.mS_.settings_TutorialOff)
		{
			this.guiMain_.SetTutorialStep(14);
		}
	}

	
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	
	public void Init(gameScript s1_, taskGame s2_)
	{
		this.FindScripts();
		this.gS_ = s1_;
		this.task_ = s2_;
		this.uiObjects[0].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_gameplay).ToString();
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_grafik).ToString();
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_sound).ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_technik).ToString();
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.points_bugs).ToString();
		this.uiObjects[13].GetComponent<Text>().text = Mathf.RoundToInt(this.gS_.GetHype()).ToString();
		this.uiObjects[6].GetComponent<Image>().sprite = this.gS_.GetScreenshot();
		this.uiObjects[31].GetComponent<Text>().text = this.tS_.GetText(6) + " <color=red>" + this.mS_.GetMoney(this.gS_.GetGesamtGewinn(), true) + "</color>";
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
		string str = string.Concat(new string[]
		{
			" ",
			num.ToString(),
			"% - ",
			num2.ToString(),
			"%"
		});
		this.uiObjects[32].GetComponent<Text>().text = this.tS_.GetText(452) + "<color=blue>" + str + "</color>";
		this.gS_.ClearReview();
		if (this.mS_.record_Gameplay < this.gS_.points_gameplay)
		{
			this.mS_.record_Gameplay = this.gS_.points_gameplay;
			this.uiObjects[7].SetActive(true);
		}
		else
		{
			this.uiObjects[7].SetActive(false);
		}
		if (this.mS_.record_Grafik < this.gS_.points_grafik)
		{
			this.mS_.record_Grafik = this.gS_.points_grafik;
			this.uiObjects[8].SetActive(true);
		}
		else
		{
			this.uiObjects[8].SetActive(false);
		}
		if (this.mS_.record_Sound < this.gS_.points_sound)
		{
			this.mS_.record_Sound = this.gS_.points_sound;
			this.uiObjects[9].SetActive(true);
		}
		else
		{
			this.uiObjects[9].SetActive(false);
		}
		if (this.mS_.record_Technik < this.gS_.points_technik)
		{
			this.mS_.record_Technik = this.gS_.points_technik;
			this.uiObjects[10].SetActive(true);
		}
		else
		{
			this.uiObjects[10].SetActive(false);
		}
		this.uiObjects[14].GetComponent<Component_Aufwertungen>().Init(this.gS_);
		this.uiObjects[15].GetComponent<Image>().sprite = this.gS_.GetTypSprite();
		this.uiObjects[15].GetComponent<tooltip>().c = this.gS_.GetTypString();
		this.uiObjects[20].GetComponent<Image>().sprite = this.gS_.GetPlatformTypSprite();
		this.uiObjects[20].GetComponent<tooltip>().c = this.gS_.GetPlatformTypString();
		this.uiObjects[11].GetComponent<Button>().interactable = true;
		this.forschungSonstiges_.Unlock(33, this.uiObjects[12], this.uiObjects[11]);
		if (this.mS_.exklusivVertrag_ID == -1)
		{
			this.uiObjects[16].transform.GetChild(1).GetComponent<Text>().text = this.tS_.GetText(420);
		}
		else
		{
			string text = this.tS_.GetText(1052);
			text = text.Replace("<NAME>", this.mS_.GetExklusivPublisher().GetName());
			this.uiObjects[16].transform.GetChild(1).GetComponent<Text>().text = text;
		}
		if (this.gS_.typ_standard || this.gS_.typ_remaster || this.gS_.typ_nachfolger || this.gS_.typ_spinoff)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.tS_.GetText(417);
			this.uiObjects[19].GetComponent<Text>().text = this.tS_.GetText(418);
			this.uiObjects[11].SetActive(true);
			this.uiObjects[16].SetActive(true);
			this.uiObjects[17].SetActive(true);
			this.uiObjects[18].SetActive(false);
			this.uiObjects[22].SetActive(false);
			this.uiObjects[23].SetActive(false);
			if (this.mS_.exklusivVertrag_ID != -1)
			{
				this.uiObjects[11].GetComponent<Button>().interactable = false;
			}
		}
		if (this.gS_.arcade)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.tS_.GetText(417);
			this.uiObjects[19].GetComponent<Text>().text = this.tS_.GetText(418);
			this.uiObjects[16].SetActive(false);
			this.uiObjects[11].SetActive(true);
			this.uiObjects[11].GetComponent<Button>().interactable = true;
			this.uiObjects[12].SetActive(false);
		}
		if (this.gS_.gameTyp == 2 || this.gS_.handy)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.tS_.GetText(417);
			this.uiObjects[19].GetComponent<Text>().text = this.tS_.GetText(418);
			this.uiObjects[11].SetActive(false);
			this.uiObjects[16].SetActive(false);
			this.uiObjects[23].SetActive(true);
		}
		if (this.gS_.typ_addon || this.gS_.typ_addonStandalone || this.gS_.typ_mmoaddon)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.tS_.GetText(981);
			this.uiObjects[19].GetComponent<Text>().text = this.tS_.GetText(982);
			this.uiObjects[11].SetActive(true);
			this.uiObjects[16].SetActive(false);
			this.uiObjects[17].SetActive(true);
			this.uiObjects[18].SetActive(false);
			this.uiObjects[22].SetActive(true);
			this.uiObjects[23].SetActive(false);
			gameScript gameScript = this.gS_.FindVorgaengerScript();
			if (gameScript && gameScript.publisherID != this.mS_.myID)
			{
				this.uiObjects[11].GetComponent<Button>().interactable = false;
			}
			if (this.mS_.exklusivVertrag_ID != -1)
			{
				this.uiObjects[11].GetComponent<Button>().interactable = false;
			}
		}
		if (this.gS_.typ_contractGame)
		{
			this.uiObjects[21].GetComponent<Text>().text = this.tS_.GetText(417);
			this.uiObjects[19].GetComponent<Text>().text = this.tS_.GetText(629);
			this.uiObjects[11].SetActive(false);
			this.uiObjects[16].SetActive(false);
			this.uiObjects[17].SetActive(false);
			this.uiObjects[18].SetActive(true);
			this.uiObjects[22].SetActive(false);
			this.uiObjects[23].SetActive(false);
			this.uiObjects[24].SetActive(false);
			this.uiObjects[26].SetActive(true);
			this.uiObjects[25].SetActive(true);
			this.ShowContractDaten();
		}
		else
		{
			this.uiObjects[26].SetActive(false);
		}
		if (!this.gS_.typ_contractGame)
		{
			if (this.gS_.schublade)
			{
				this.uiObjects[24].SetActive(false);
				this.uiObjects[25].SetActive(false);
				return;
			}
			this.uiObjects[24].SetActive(true);
			this.uiObjects[25].SetActive(true);
		}
	}

	
	private void ShowContractDaten()
	{
		if (this.gS_.typ_contractGame)
		{
			string text = this.tS_.GetText(605);
			text = text.Replace("<NUM>", this.gS_.auftragsspiel_zeitInWochen.ToString());
			this.uiObjects[27].GetComponent<Text>().text = text;
			text = this.tS_.GetText(626);
			text = text.Replace("<NUM>", this.gS_.auftragsspiel_mindestbewertung.ToString());
			this.uiObjects[28].GetComponent<Text>().text = text;
			this.uiObjects[29].GetComponent<Text>().text = this.tS_.GetText(600) + ": " + this.mS_.GetMoney((long)this.gS_.auftragsspiel_gehalt, true);
			this.uiObjects[30].GetComponent<Text>().text = this.tS_.GetText(627) + ": " + this.mS_.GetMoney((long)this.gS_.auftragsspiel_bonus, true);
		}
	}

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Schublade()
	{
		this.sfx_.PlaySound(3, true);
		if (this.HasNoMainPlatform())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1660), false);
			return;
		}
		if (!this.IsAddon_IsMainGameReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1504), false);
			return;
		}
		if (this.IsSpezialMarketingInBearbeitung())
		{
			return;
		}
		this.gS_.schublade = true;
		this.gS_.schubladeTaskID = this.task_.myID;
		this.gS_.inDevelopment = false;
		this.gS_.InitUI();
		GameObject[] array = GameObject.FindGameObjectsWithTag("Room");
		for (int i = 0; i < array.Length; i++)
		{
			roomScript component = array[i].GetComponent<roomScript>();
			if (component && component.taskID == this.task_.myID)
			{
				component.taskID = -1;
				component.taskGameObject = null;
				break;
			}
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_PublisherSuchen()
	{
		this.sfx_.PlaySound(3, true);
		if (this.HasNoMainPlatform())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1660), false);
			return;
		}
		if (!this.gS_.AllePlattformenReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1634) + "\n\n<color=red>" + this.gS_.GetUnreleasedPlatformsString() + "</color>", false);
			return;
		}
		if (!this.IsAddon_IsMainGameReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1504), false);
			return;
		}
		if (this.IsSpezialMarketingInBearbeitung())
		{
			return;
		}
		this.gS_.CalcReview(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[70]);
		this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().Init(this.gS_, this.task_);
		if (this.mS_.exklusivVertrag_ID != -1)
		{
			this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().SelectPublisher(this.mS_.exklusivVertrag_ID);
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_AddonPublisher()
	{
		this.sfx_.PlaySound(3, true);
		if (this.HasNoMainPlatform())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1660), false);
			return;
		}
		if (!this.gS_.AllePlattformenReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1634) + "\n\n<color=red>" + this.gS_.GetUnreleasedPlatformsString() + "</color>", false);
			return;
		}
		if (!this.IsAddon_IsMainGameReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1504), false);
			return;
		}
		if (this.IsSpezialMarketingInBearbeitung())
		{
			return;
		}
		this.gS_.CalcReview(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[70]);
		this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().Init(this.gS_, this.task_);
		gameScript gameScript = this.gS_.FindVorgaengerScript();
		if (gameScript && gameScript.publisherID != this.mS_.myID)
		{
			this.guiMain_.uiObjects[70].GetComponent<Menu_Dev_SelectPublisher>().SelectPublisher(gameScript.publisherID);
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_OnlineVertreiben()
	{
		this.sfx_.PlaySound(3, true);
		if (this.HasNoMainPlatform())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1660), false);
			return;
		}
		if (!this.gS_.AllePlattformenReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1634) + "\n\n<color=red>" + this.gS_.GetUnreleasedPlatformsString() + "</color>", false);
			return;
		}
		if (!this.IsAddon_IsMainGameReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1504), false);
			return;
		}
		if (this.IsSpezialMarketingInBearbeitung())
		{
			return;
		}
		this.gS_.CalcReview(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[283]);
		this.guiMain_.uiObjects[283].GetComponent<Menu_ReleaseDate_F2P>().Init(this.gS_, this.task_);
	}

	
	public void BUTTON_SelfPublish()
	{
		this.sfx_.PlaySound(3, true);
		if (this.HasNoMainPlatform())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1660), false);
			return;
		}
		if (!this.gS_.AllePlattformenReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1634) + "\n\n<color=red>" + this.gS_.GetUnreleasedPlatformsString() + "</color>", false);
			return;
		}
		if (!this.IsAddon_IsMainGameReleased())
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1504), false);
			return;
		}
		if (this.IsSpezialMarketingInBearbeitung())
		{
			return;
		}
		this.gS_.CalcReview(false);
		if (this.gS_.arcade)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[307]);
			this.guiMain_.uiObjects[307].GetComponent<Menu_ArcadePreis>().Init(this.gS_, this.task_);
		}
		else
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[218]);
			this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(this.gS_, this.task_, true, false);
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Verwerfen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[93]);
		this.guiMain_.uiObjects[93].GetComponent<Menu_W_GameVerwerfen>().Init(this.gS_, this.task_);
	}

	
	public void BUTTON_ContractGame()
	{
		this.sfx_.PlaySound(3, true);
		this.gS_.CalcReview(false);
		UnityEngine.Object.Destroy(this.task_.gameObject);
		this.gS_.SetOnMarket();
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[71]);
		this.guiMain_.uiObjects[71].GetComponent<Menu_Dev_XP>().Init(this.gS_);
		base.gameObject.SetActive(false);
	}

	
	private bool IsAddon_IsMainGameReleased()
	{
		if ((this.gS_.typ_addon || this.gS_.typ_addonStandalone || this.gS_.typ_mmoaddon) && this.gS_.originalIP != -1)
		{
			GameObject gameObject = GameObject.Find("GAME_" + this.gS_.originalIP.ToString());
			if (gameObject)
			{
				return gameObject.GetComponent<gameScript>().releaseDate <= 0;
			}
		}
		return true;
	}

	
	private bool IsSpezialMarketingInBearbeitung()
	{
		return false;
	}

	
	private bool HasNoMainPlatform()
	{
		if (this.gS_.gamePlatform[0] == -1)
		{
			return true;
		}
		this.gS_.FindMyPlatforms();
		return !this.gS_.gamePlatformScript[0];
	}

	
	public GameObject[] uiObjects;

	
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

	
	private forschungSonstiges forschungSonstiges_;

	
	private gameScript gS_;

	
	private taskGame task_;
}
