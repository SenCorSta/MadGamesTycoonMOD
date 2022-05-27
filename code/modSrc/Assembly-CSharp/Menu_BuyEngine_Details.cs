using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_BuyEngine_Details : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.Find("Main");
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
	}

	
	public void Init(engineScript s)
	{
		this.FindScripts();
		this.eS_ = s;
		this.SetData();
	}

	
	private void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(4) + " " + this.eS_.GetTechLevel();
		this.uiObjects[2].GetComponent<Text>().text = this.genres_.GetName(this.eS_.spezialgenre);
		this.uiObjects[4].GetComponent<Text>().text = this.eS_.GetGamesAmount().ToString() + " " + this.tS_.GetText(271);
		this.uiObjects[5].GetComponent<Text>().text = this.eS_.GetFeaturesAmount().ToString() + " " + this.tS_.GetText(272);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.preis, true);
		this.uiObjects[7].GetComponent<Text>().text = this.eS_.gewinnbeteiligung.ToString() + "%";
		this.uiObjects[10].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.guiMain_.DrawStars(this.uiObjects[3], this.genres_.genres_LEVEL[this.eS_.spezialgenre]);
		platformScript spezialPlatformScript = this.eS_.GetSpezialPlatformScript();
		if (spezialPlatformScript)
		{
			this.uiObjects[13].GetComponent<Text>().text = spezialPlatformScript.GetName();
			spezialPlatformScript.SetPic(this.uiObjects[11]);
			this.guiMain_.DrawStars(this.uiObjects[12], spezialPlatformScript.erfahrung);
		}
		if (!this.eS_.playerEngine && !this.eS_.gekauft)
		{
			this.uiObjects[8].SetActive(true);
			this.uiObjects[9].SetActive(false);
			return;
		}
		this.uiObjects[8].SetActive(false);
		this.uiObjects[9].SetActive(true);
	}

	
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.SetData();
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Kaufen()
	{
		this.sfx_.PlaySound(3, true);
		this.mS_.Pay((long)this.eS_.preis, 5);
		this.eS_.gekauft = true;
		this.guiMain_.uiObjects[42].GetComponent<Menu_BuyEngine>().OnEnable();
		if (this.eS_.multiplayerSlot != -1)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_Payment(this.mS_.mpCalls_.myID, this.eS_.multiplayerSlot, 1, this.eS_.preis);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_Payment(this.eS_.multiplayerSlot, 1, this.eS_.preis);
			}
		}
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_ShowFeatures()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[44]);
		this.guiMain_.uiObjects[44].GetComponent<Menu_Engine_ShowFeatures>().Init(this.eS_);
	}

	
	public void BUTTON_ShowGames()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[45]);
		this.guiMain_.uiObjects[45].GetComponent<Menu_Engine_ShowGames>().Init(this.eS_);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private engineFeatures eF_;

	
	private engineScript eS_;
}
