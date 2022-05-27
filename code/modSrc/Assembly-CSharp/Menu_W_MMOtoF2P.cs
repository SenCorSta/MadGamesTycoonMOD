using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_W_MMOtoF2P : MonoBehaviour
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
	}

	
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		this.game_ = gS_;
		this.uiObjects[0].GetComponent<Text>().text = gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(), true);
	}

	
	private int GetPrice()
	{
		return this.game_.GetGesamtDevPoints() * 500;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_)
		{
			this.mS_.Pay((long)this.GetPrice(), 10);
			this.CreateGame();
		}
		if (this.game_.isOnMarket)
		{
			this.game_.RemoveFromMarket();
			if (this.mS_.multiplayer)
			{
				if (this.mS_.mpCalls_.isServer)
				{
					this.mS_.mpCalls_.SERVER_Send_GameData(this.game_);
				}
				if (this.mS_.mpCalls_.isClient)
				{
					this.mS_.mpCalls_.CLIENT_Send_GameData(this.game_);
				}
			}
		}
		this.guiMain_.uiObjects[285].SetActive(false);
		base.gameObject.SetActive(false);
	}

	
	private void CreateGame()
	{
		this.game_.mmoTOf2p_created = true;
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.game_.gameObject).GetComponent<gameScript>();
		this.games_.InitMMOtoF2PGame(component);
		if (this.mS_.multiplayer)
		{
			component.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
		component.SetMyName(this.game_.GetNameSimple());
		component.gameTyp = 2;
		component.f2pConverted = true;
		component.publisherID = -1;
		component.warBeiAwards = true;
		component.weeksOnMarket = 0;
		component.releaseDate = 0;
		component.vorbestellungen = 0;
		component.date_year = this.mS_.year;
		component.date_month = this.mS_.month;
		component.spielbericht_favorit = false;
		component.abonnements = this.game_.abonnements;
		component.sellsTotal = (long)this.game_.abonnements;
		component.sellsTotalOnline = 0L;
		component.abonnementsWoche = 0;
		component.sellsTotalStandard = 0L;
		component.sellsTotalDeluxe = 0L;
		component.sellsTotalCollectors = 0L;
		component.umsatzTotal = 0L;
		component.umsatzInApp = 0L;
		component.umsatzAbos = 0L;
		component.inAppPurchaseWeek = 0;
		component.costs_entwicklung = (long)this.GetPrice();
		component.costs_mitarbeiter = 0L;
		component.costs_marketing = 0L;
		component.costs_enginegebuehren = 0L;
		component.costs_server = 0L;
		component.costs_production = 0L;
		for (int i = 0; i < component.sellsPerWeek.Length; i++)
		{
			component.sellsPerWeek[i] = 0;
		}
		component.lagerbestand[0] = 0;
		component.lagerbestand[1] = 0;
		component.lagerbestand[2] = 0;
		component.SetOnMarket();
		this.guiMain_.uiObjects[278].SetActive(true);
		this.guiMain_.uiObjects[278].GetComponent<Menu_InAppPurchases>().Init(component, true);
		if (this.mS_.multiplayer)
		{
			if (this.mS_.mpCalls_.isServer)
			{
				this.mS_.mpCalls_.SERVER_Send_GameData(component);
			}
			if (this.mS_.mpCalls_.isClient)
			{
				this.mS_.mpCalls_.CLIENT_Send_GameData(component);
			}
		}
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private gameScript game_;

	
	private games games_;
}
