using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001EE RID: 494
public class Menu_AddonBundle : MonoBehaviour
{
	// Token: 0x060012B8 RID: 4792 RVA: 0x000C6B78 File Offset: 0x000C4D78
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012B9 RID: 4793 RVA: 0x000C6B80 File Offset: 0x000C4D80
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

	// Token: 0x060012BA RID: 4794 RVA: 0x000C6C66 File Offset: 0x000C4E66
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x000C6C70 File Offset: 0x000C4E70
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.games.Length; i++)
		{
			this.SetGame(i, this.games[i]);
		}
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x000C6CA8 File Offset: 0x000C4EA8
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

	// Token: 0x060012BD RID: 4797 RVA: 0x000C6F1E File Offset: 0x000C511E
	public float GetQuality()
	{
		return this.GetTotalReview() / 20f;
	}

	// Token: 0x060012BE RID: 4798 RVA: 0x000C6F2C File Offset: 0x000C512C
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

	// Token: 0x060012BF RID: 4799 RVA: 0x000C6F91 File Offset: 0x000C5191
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x000C6FB8 File Offset: 0x000C51B8
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

	// Token: 0x060012C1 RID: 4801 RVA: 0x000C7039 File Offset: 0x000C5239
	public void BUTTON_Remove(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.SetGame(i, null);
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x000C7050 File Offset: 0x000C5250
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

	// Token: 0x060012C3 RID: 4803 RVA: 0x000C7188 File Offset: 0x000C5388
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

	// Token: 0x0400170E RID: 5902
	public GameObject[] uiObjects;

	// Token: 0x0400170F RID: 5903
	private roomScript rS_;

	// Token: 0x04001710 RID: 5904
	private GameObject main_;

	// Token: 0x04001711 RID: 5905
	private mainScript mS_;

	// Token: 0x04001712 RID: 5906
	private textScript tS_;

	// Token: 0x04001713 RID: 5907
	private GUI_Main guiMain_;

	// Token: 0x04001714 RID: 5908
	private sfxScript sfx_;

	// Token: 0x04001715 RID: 5909
	private genres genres_;

	// Token: 0x04001716 RID: 5910
	private games games_;

	// Token: 0x04001717 RID: 5911
	public gameScript[] games;
}
