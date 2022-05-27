using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_GOTYGamename : MonoBehaviour
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
		this.BUTTON_Name(this.lastName);
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Name(int i)
	{
		this.lastName = i;
		this.sfx_.PlaySound(3, true);
		if (!this.game_)
		{
			return;
		}
		if (i != 0)
		{
			if (i == 1)
			{
				this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag() + " <color=orange><i>" + this.tS_.GetText(1361) + "</i></color>";
			}
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag() + " <color=orange><i>" + this.tS_.GetText(1359) + "</i></color>";
		}
		for (int j = 0; j < this.uiObjects[1].transform.childCount; j++)
		{
			this.uiObjects[1].transform.GetChild(j).GetComponent<Button>().interactable = true;
		}
		this.uiObjects[1].transform.GetChild(i).GetComponent<Button>().interactable = false;
	}

	
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component)
				{
					if (component.GetNameSimple() == this.uiObjects[0].GetComponent<Text>().text)
					{
						this.guiMain_.MessageBox(this.tS_.GetText(618), false);
						return;
					}
					if (component.isOnMarket && component.typ_budget && component.originalGameID == this.game_.myID)
					{
						this.guiMain_.MessageBox(this.tS_.GetText(1404), false);
						return;
					}
				}
			}
		}
		this.CreateGotyGame();
	}

	
	private void CreateGotyGame()
	{
		this.game_.goty_created = true;
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.game_.gameObject).GetComponent<gameScript>();
		this.games_.InitGotyGame(component);
		component.originalGameID = this.game_.myID;
		if (this.mS_.multiplayer)
		{
			component.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
		component.SetMyName(this.uiObjects[0].GetComponent<Text>().text);
		component.typ_standard = false;
		component.typ_goty = true;
		component.typ_nachfolger = false;
		component.typ_remaster = false;
		component.typ_budget = false;
		component.typ_addon = false;
		component.typ_addonStandalone = false;
		component.typ_bundle = false;
		component.typ_mmoaddon = false;
		component.typ_bundleAddon = false;
		component.spielbericht = false;
		component.spielbericht_favorit = false;
		component.hype = 0f;
		component.warBeiAwards = true;
		component.weeksOnMarket = 0;
		component.releaseDate = 0;
		component.vorbestellungen = 0;
		component.date_year = this.mS_.year;
		component.date_month = this.mS_.month;
		component.sellsTotalStandard = 0L;
		component.sellsTotalDeluxe = 0L;
		component.sellsTotalCollectors = 0L;
		component.sellsTotalOnline = 0L;
		component.sellsTotal = 0L;
		component.umsatzTotal = 0L;
		component.costs_entwicklung = 0L;
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
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[218]);
		this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(component, null, true, true);
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
		this.guiMain_.uiObjects[274].SetActive(false);
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private games games_;

	
	private gameScript game_;

	
	private int lastName;
}
