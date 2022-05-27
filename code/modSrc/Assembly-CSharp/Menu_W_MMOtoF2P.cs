using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000211 RID: 529
public class Menu_W_MMOtoF2P : MonoBehaviour
{
	// Token: 0x06001459 RID: 5209 RVA: 0x000D3992 File Offset: 0x000D1B92
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600145A RID: 5210 RVA: 0x000D399C File Offset: 0x000D1B9C
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

	// Token: 0x0600145B RID: 5211 RVA: 0x000D3A64 File Offset: 0x000D1C64
	public void Init(gameScript gS_)
	{
		this.FindScripts();
		this.game_ = gS_;
		this.uiObjects[0].GetComponent<Text>().text = gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.GetPrice(), true);
	}

	// Token: 0x0600145C RID: 5212 RVA: 0x000D3ABB File Offset: 0x000D1CBB
	private int GetPrice()
	{
		return this.game_.GetGesamtDevPoints() * 500;
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x000D3ACE File Offset: 0x000D1CCE
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x000D3AEC File Offset: 0x000D1CEC
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

	// Token: 0x0600145F RID: 5215 RVA: 0x000D3BC8 File Offset: 0x000D1DC8
	private void CreateGame()
	{
		this.game_.mmoTOf2p_created = true;
		gameScript component = UnityEngine.Object.Instantiate<GameObject>(this.game_.gameObject).GetComponent<gameScript>();
		this.games_.InitMMOtoF2PGame(component);
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

	// Token: 0x04001864 RID: 6244
	public GameObject[] uiObjects;

	// Token: 0x04001865 RID: 6245
	private GameObject main_;

	// Token: 0x04001866 RID: 6246
	private mainScript mS_;

	// Token: 0x04001867 RID: 6247
	private textScript tS_;

	// Token: 0x04001868 RID: 6248
	private GUI_Main guiMain_;

	// Token: 0x04001869 RID: 6249
	private sfxScript sfx_;

	// Token: 0x0400186A RID: 6250
	private gameScript game_;

	// Token: 0x0400186B RID: 6251
	private games games_;
}
