using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F5 RID: 501
public class Menu_GOTYGamename : MonoBehaviour
{
	// Token: 0x060012FB RID: 4859 RVA: 0x0000D041 File Offset: 0x0000B241
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x000D49CC File Offset: 0x000D2BCC
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

	// Token: 0x060012FD RID: 4861 RVA: 0x0000D049 File Offset: 0x0000B249
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		this.game_ = gS_;
		this.BUTTON_Name(this.lastName);
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x0000D064 File Offset: 0x0000B264
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x000D4A94 File Offset: 0x000D2C94
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

	// Token: 0x06001300 RID: 4864 RVA: 0x000D4BA0 File Offset: 0x000D2DA0
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

	// Token: 0x06001301 RID: 4865 RVA: 0x000D4C74 File Offset: 0x000D2E74
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

	// Token: 0x04001754 RID: 5972
	public GameObject[] uiObjects;

	// Token: 0x04001755 RID: 5973
	private GameObject main_;

	// Token: 0x04001756 RID: 5974
	private mainScript mS_;

	// Token: 0x04001757 RID: 5975
	private textScript tS_;

	// Token: 0x04001758 RID: 5976
	private GUI_Main guiMain_;

	// Token: 0x04001759 RID: 5977
	private sfxScript sfx_;

	// Token: 0x0400175A RID: 5978
	private games games_;

	// Token: 0x0400175B RID: 5979
	private gameScript game_;

	// Token: 0x0400175C RID: 5980
	private int lastName;
}
