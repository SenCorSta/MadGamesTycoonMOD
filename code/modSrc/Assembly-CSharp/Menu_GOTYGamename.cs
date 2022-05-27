using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F6 RID: 502
public class Menu_GOTYGamename : MonoBehaviour
{
	// Token: 0x06001316 RID: 4886 RVA: 0x000CA2AA File Offset: 0x000C84AA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x000CA2B4 File Offset: 0x000C84B4
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

	// Token: 0x06001318 RID: 4888 RVA: 0x000CA37C File Offset: 0x000C857C
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		this.game_ = gS_;
		this.BUTTON_Name(this.lastName);
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000CA397 File Offset: 0x000C8597
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000CA3B4 File Offset: 0x000C85B4
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
				this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameSimple() + " <color=orange><i>" + this.tS_.GetText(1361) + "</i></color>";
			}
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameSimple() + " <color=orange><i>" + this.tS_.GetText(1359) + "</i></color>";
		}
		for (int j = 0; j < this.uiObjects[1].transform.childCount; j++)
		{
			this.uiObjects[1].transform.GetChild(j).GetComponent<Button>().interactable = true;
		}
		this.uiObjects[1].transform.GetChild(i).GetComponent<Button>().interactable = false;
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000CA4C0 File Offset: 0x000C86C0
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

	// Token: 0x0600131C RID: 4892 RVA: 0x000CA594 File Offset: 0x000C8794
	private void CreateGotyGame()
	{
		this.game_.goty_created = true;
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.game_.gameObject).GetComponent<gameScript>();
		this.games_.InitGotyGame(component);
		component.originalGameID = this.game_.myID;
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
		component.umsatzInApp = 0L;
		component.umsatzAbos = 0L;
		component.tw_gewinnanteil = 0L;
		component.costs_entwicklung = 0L;
		component.costs_mitarbeiter = 0L;
		component.costs_marketing = 0L;
		component.costs_enginegebuehren = 0L;
		component.costs_server = 0L;
		component.costs_production = 0L;
		component.costs_updates = 0L;
		component.bestChartPosition = 0;
		component.lastChartPosition = 0;
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

	// Token: 0x0400175D RID: 5981
	public GameObject[] uiObjects;

	// Token: 0x0400175E RID: 5982
	private GameObject main_;

	// Token: 0x0400175F RID: 5983
	private mainScript mS_;

	// Token: 0x04001760 RID: 5984
	private textScript tS_;

	// Token: 0x04001761 RID: 5985
	private GUI_Main guiMain_;

	// Token: 0x04001762 RID: 5986
	private sfxScript sfx_;

	// Token: 0x04001763 RID: 5987
	private games games_;

	// Token: 0x04001764 RID: 5988
	private gameScript game_;

	// Token: 0x04001765 RID: 5989
	private int lastName;
}
