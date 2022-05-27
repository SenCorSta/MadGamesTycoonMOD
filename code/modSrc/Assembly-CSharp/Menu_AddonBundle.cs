using System;
using UnityEngine;
using UnityEngine.UI;


public class Menu_AddonBundle : MonoBehaviour
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
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

	
	private void OnEnable()
	{
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.games.Length; i++)
		{
			this.SetGame(i, this.games[i]);
		}
	}

	
	public void SetGame(int slot, gameScript script_)
	{
		if (slot == 0)
		{
			this.BUTTON_Remove(1);
			this.BUTTON_Remove(2);
			this.BUTTON_Remove(3);
			this.BUTTON_Remove(4);
			if (script_)
			{
				string text = script_.GetNameSimple();
				text = text.Replace("<color=green>[P]</color>", string.Empty);
				text = text.Replace("<color=green>", string.Empty);
				text = text.Replace("[P]", string.Empty);
				text = text.Replace("</color>", string.Empty);
				text = text.Replace("\n", string.Empty);
				text = text.Replace("\r", string.Empty);
				text = text.Replace("\t", string.Empty);
				this.uiObjects[0].GetComponent<InputField>().text = text + " - " + this.tS_.GetText(1358);
			}
		}
		this.games[slot] = script_;
		if (!script_)
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = this.tS_.GetText(1344);
			this.uiObjects[2 + slot].GetComponent<Text>().text = this.tS_.GetText(1345);
			this.uiObjects[7 + slot].GetComponent<Text>().text = this.tS_.GetText(1344);
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[27 + slot].GetComponent<Text>().text = "";
		}
		else
		{
			this.uiObjects[22 + slot].GetComponent<tooltip>().c = script_.GetTooltip();
			this.uiObjects[2 + slot].GetComponent<Text>().text = "<b>" + script_.GetNameWithTag() + "</b>";
			this.uiObjects[7 + slot].GetComponent<Text>().text = script_.GetReleaseDateString();
			this.uiObjects[12 + slot].GetComponent<Image>().sprite = script_.GetTypSprite();
			this.uiObjects[27 + slot].GetComponent<Text>().text = Mathf.RoundToInt((float)script_.reviewTotal).ToString() + "%";
		}
		this.guiMain_.DrawStarsColor(this.uiObjects[1], Mathf.RoundToInt(this.GetQuality()), Color.white);
	}

	
	public float GetQuality()
	{
		return this.GetTotalReview() / 20f;
	}

	
	public float GetTotalReview()
	{
		float num = 0f;
		for (int i = 0; i < this.games.Length; i++)
		{
			if (this.games[i])
			{
				num += (float)this.games[i].reviewTotal;
			}
		}
		if (num > 0f)
		{
			num /= 3f;
		}
		if (num > 90f)
		{
			num = 90f;
		}
		return num;
	}

	
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	
	public void BUTTON_Game(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (i != 0 && !this.games[0])
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1355), false);
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[272]);
		this.guiMain_.uiObjects[272].GetComponent<Menu_AddonBundleSelect>().Init(i);
	}

	
	public void BUTTON_Remove(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.SetGame(i, null);
	}

	
	public void BUTTON_OK()
	{
		int num = 0;
		for (int i = 0; i < this.games.Length; i++)
		{
			if (this.games[i])
			{
				num++;
			}
		}
		if (num <= 1)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1357), false);
			return;
		}
		if (this.uiObjects[0].GetComponent<InputField>().text.Length <= 0)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1346), false);
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int j = 0; j < array.Length; j++)
			{
				gameScript component = array[j].GetComponent<gameScript>();
				if (component && component.GetNameSimple() == this.uiObjects[0].GetComponent<InputField>().text)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(618), false);
					return;
				}
			}
		}
		this.CreateAddonBundleGame();
		this.uiObjects[0].GetComponent<InputField>().text = "";
		for (int k = 0; k < this.games.Length; k++)
		{
			this.games[k] = null;
		}
	}

	
	private void CreateAddonBundleGame()
	{
		for (int i = 1; i < this.games.Length; i++)
		{
			if (this.games[i])
			{
				this.games[i].bundle_created = true;
			}
		}
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.games[0].gameObject).GetComponent<gameScript>();
		this.games_.InitAddonBundle(component);
		if (this.mS_.multiplayer)
		{
			component.multiplayerSlot = this.mS_.mpCalls_.myID;
		}
		component.SetMyName(this.uiObjects[0].GetComponent<InputField>().text);
		component.developerID = -1;
		component.publisherID = -1;
		component.typ_standard = false;
		component.typ_nachfolger = false;
		component.typ_remaster = false;
		component.typ_budget = false;
		component.typ_addon = false;
		component.typ_addonStandalone = false;
		component.typ_bundle = false;
		component.typ_mmoaddon = false;
		component.typ_bundleAddon = true;
		component.warBeiAwards = true;
		component.weeksOnMarket = 0;
		component.releaseDate = 0;
		component.vorbestellungen = 0;
		component.date_year = this.mS_.year;
		component.date_month = this.mS_.month;
		component.spielbericht = false;
		component.spielbericht_favorit = false;
		component.userPositiv = 0;
		component.userNegativ = 0;
		component.hype = 0f;
		component.reviewGameplayText = 0;
		component.reviewGrafikText = 0;
		component.reviewSoundText = 0;
		component.reviewSteuerungText = 0;
		component.reviewTotalText = 0;
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
		for (int j = 0; j < component.sellsPerWeek.Length; j++)
		{
			component.sellsPerWeek[j] = 0;
		}
		component.lagerbestand[0] = 0;
		component.lagerbestand[1] = 0;
		component.lagerbestand[2] = 0;
		for (int k = 0; k < this.games.Length; k++)
		{
			if (this.games[k])
			{
				component.bundleID[k] = this.games[k].myID;
			}
		}
		component.reviewTotal -= 16;
		if (this.games[1])
		{
			component.reviewTotal += 4;
		}
		if (this.games[2])
		{
			component.reviewTotal += 4;
		}
		if (this.games[3])
		{
			component.reviewTotal += 4;
		}
		if (this.games[4])
		{
			component.reviewTotal += 4;
		}
		component.reviewTotal -= (this.mS_.year - component.date_start_year) * 2;
		if (component.reviewTotal <= 0)
		{
			component.reviewTotal = 1;
		}
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
		base.gameObject.SetActive(false);
	}

	
	public GameObject[] uiObjects;

	
	private roomScript rS_;

	
	private GameObject main_;

	
	private mainScript mS_;

	
	private textScript tS_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private genres genres_;

	
	private games games_;

	
	public gameScript[] games;
}
