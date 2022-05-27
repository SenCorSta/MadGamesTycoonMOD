using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F1 RID: 497
public class Menu_BudgetGamename : MonoBehaviour
{
	// Token: 0x060012E0 RID: 4832 RVA: 0x000C8323 File Offset: 0x000C6523
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000C832C File Offset: 0x000C652C
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

	// Token: 0x060012E2 RID: 4834 RVA: 0x000C83F4 File Offset: 0x000C65F4
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		this.game_ = gS_;
		this.BUTTON_Name(this.lastName);
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000C840F File Offset: 0x000C660F
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x000C842C File Offset: 0x000C662C
	public void BUTTON_Name(int i)
	{
		this.lastName = i;
		this.sfx_.PlaySound(3, true);
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameSimple() + " <color=grey><i>" + this.tS_.GetText(1154 + i) + "</i></color>";
		for (int j = 0; j < this.uiObjects[1].transform.childCount; j++)
		{
			this.uiObjects[1].transform.GetChild(j).GetComponent<Button>().interactable = true;
		}
		this.uiObjects[1].transform.GetChild(i).GetComponent<Button>().interactable = false;
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x000C84F4 File Offset: 0x000C66F4
	public void BUTTON_Yes()
	{
		this.sfx_.PlaySound(3, true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.GetNameSimple() == this.uiObjects[0].GetComponent<Text>().text)
				{
					this.guiMain_.MessageBox(this.tS_.GetText(618), false);
					return;
				}
			}
		}
		this.CreateBudgetGame();
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000C8580 File Offset: 0x000C6780
	private void CreateBudgetGame()
	{
		this.game_.budget_created = true;
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.game_.gameObject).GetComponent<gameScript>();
		this.games_.InitBudgetGame(component);
		component.originalGameID = this.game_.myID;
		component.SetMyName(this.uiObjects[0].GetComponent<Text>().text);
		component.typ_standard = false;
		component.typ_budget = true;
		component.typ_addon = false;
		component.typ_addonStandalone = false;
		component.typ_bundle = false;
		component.typ_mmoaddon = false;
		component.typ_bundleAddon = false;
		component.warBeiAwards = true;
		component.weeksOnMarket = 0;
		component.releaseDate = 0;
		component.vorbestellungen = 0;
		component.date_year = this.mS_.year;
		component.date_month = this.mS_.month;
		component.spielbericht = false;
		component.spielbericht_favorit = false;
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
		this.game_.lagerbestand[0] = 0;
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
		this.guiMain_.uiObjects[227].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400172C RID: 5932
	public GameObject[] uiObjects;

	// Token: 0x0400172D RID: 5933
	private GameObject main_;

	// Token: 0x0400172E RID: 5934
	private mainScript mS_;

	// Token: 0x0400172F RID: 5935
	private textScript tS_;

	// Token: 0x04001730 RID: 5936
	private GUI_Main guiMain_;

	// Token: 0x04001731 RID: 5937
	private sfxScript sfx_;

	// Token: 0x04001732 RID: 5938
	private games games_;

	// Token: 0x04001733 RID: 5939
	private gameScript game_;

	// Token: 0x04001734 RID: 5940
	private int lastName;
}
