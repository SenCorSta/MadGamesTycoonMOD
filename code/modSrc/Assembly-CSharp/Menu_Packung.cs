﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000153 RID: 339
public class Menu_Packung : MonoBehaviour
{
	// Token: 0x06000C7B RID: 3195 RVA: 0x0008637A File Offset: 0x0008457A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00086384 File Offset: 0x00084584
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindWithTag("Main");
		}
		if (!this.mS_)
		{
			this.mS_ = this.main_.GetComponent<mainScript>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.sfx_)
		{
			this.sfx_ = GameObject.Find("SFX").GetComponent<sfxScript>();
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x000864CC File Offset: 0x000846CC
	private void Update()
	{
		if (!this.gS_)
		{
			return;
		}
		this.UpdatePackung();
		this.uiObjects[22].GetComponent<Text>().text = this.GetMoneyString(this.GetProduktionskosten(0));
		this.uiObjects[23].GetComponent<Text>().text = this.GetMoneyString(this.GetProduktionskosten(1));
		this.uiObjects[24].GetComponent<Text>().text = this.GetMoneyString(this.GetProduktionskosten(2));
		this.uiObjects[25].GetComponent<Text>().text = "-";
		this.uiObjects[26].GetComponent<Text>().text = this.mS_.GetMoney((long)this.verkaufspreis[0], true);
		this.uiObjects[27].GetComponent<Text>().text = this.mS_.GetMoney((long)this.verkaufspreis[1], true);
		this.uiObjects[28].GetComponent<Text>().text = this.mS_.GetMoney((long)this.verkaufspreis[2], true);
		this.uiObjects[29].GetComponent<Text>().text = this.mS_.GetMoney((long)this.verkaufspreis[3], true);
		this.uiObjects[34].GetComponent<Text>().text = "$" + Mathf.RoundToInt((float)this.verkaufspreis[0] * 1.4f).ToString() + ".99";
		this.uiObjects[35].GetComponent<Text>().text = "$" + Mathf.RoundToInt((float)this.verkaufspreis[1] * 1.4f).ToString() + ".99";
		this.uiObjects[36].GetComponent<Text>().text = "$" + Mathf.RoundToInt((float)this.verkaufspreis[2] * 1.4f).ToString() + ".99";
		this.uiObjects[37].GetComponent<Text>().text = "$" + Mathf.RoundToInt((float)this.verkaufspreis[3] * 1.4f).ToString() + ".99";
		this.verkaufspreis[3] = this.verkaufspreis[0];
		if (this.verkaufspreis[1] <= this.verkaufspreis[0] + 10)
		{
			this.verkaufspreis[1] = this.verkaufspreis[0] + 10;
		}
		if (this.verkaufspreis[2] <= this.verkaufspreis[1] + 10)
		{
			this.verkaufspreis[2] = this.verkaufspreis[1] + 10;
		}
		this.uiObjects[30].GetComponent<Text>().text = this.GetMoneyString(this.GetGewinn(0));
		this.uiObjects[31].GetComponent<Text>().text = this.GetMoneyString(this.GetGewinn(1));
		this.uiObjects[32].GetComponent<Text>().text = this.GetMoneyString(this.GetGewinn(2));
		this.uiObjects[33].GetComponent<Text>().text = this.GetMoneyString(this.GetGewinn(3));
		if (this.gS_.pubOffer)
		{
			Text component = this.uiObjects[30].GetComponent<Text>();
			component.text = component.text + "<color=red> (-" + Mathf.RoundToInt((float)this.gS_.PUBOFFER_GetGewinnbeteiligung()).ToString() + "%)</color>";
			Text component2 = this.uiObjects[31].GetComponent<Text>();
			component2.text = component2.text + "<color=red> (-" + Mathf.RoundToInt((float)this.gS_.PUBOFFER_GetGewinnbeteiligung()).ToString() + "%)</color>";
			Text component3 = this.uiObjects[32].GetComponent<Text>();
			component3.text = component3.text + "<color=red> (-" + Mathf.RoundToInt((float)this.gS_.PUBOFFER_GetGewinnbeteiligung()).ToString() + "%)</color>";
			Text component4 = this.uiObjects[33].GetComponent<Text>();
			component4.text = component4.text + "<color=red> (-" + Mathf.RoundToInt((float)this.gS_.PUBOFFER_GetGewinnbeteiligung()).ToString() + "%)</color>";
		}
		if (this.GetGewinn(0) >= 0f)
		{
			this.uiObjects[30].GetComponent<Text>().color = this.guiMain_.colors[13];
		}
		else
		{
			this.uiObjects[30].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
		if (this.GetGewinn(1) >= 0f)
		{
			this.uiObjects[31].GetComponent<Text>().color = this.guiMain_.colors[13];
		}
		else
		{
			this.uiObjects[31].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
		if (this.GetGewinn(2) >= 0f)
		{
			this.uiObjects[32].GetComponent<Text>().color = this.guiMain_.colors[13];
		}
		else
		{
			this.uiObjects[32].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
		if (this.GetGewinn(3) >= 0f)
		{
			this.uiObjects[33].GetComponent<Text>().color = this.guiMain_.colors[13];
		}
		else
		{
			this.uiObjects[33].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
		if (this.verkaufspreis[0] <= 5)
		{
			this.uiObjects[45].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[45].GetComponent<Button>().interactable = true;
		}
		if (this.verkaufspreis[0] >= 79)
		{
			this.uiObjects[48].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[48].GetComponent<Button>().interactable = true;
		}
		if (!this.gS_.typ_budget && !this.gS_.typ_bundle && !this.gS_.typ_goty && !this.gS_.typ_addon && !this.gS_.typ_mmoaddon && !this.gS_.typ_addonStandalone)
		{
			if (this.verkaufspreis[1] <= this.verkaufspreis[0] + 10)
			{
				this.uiObjects[46].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[46].GetComponent<Button>().interactable = true;
			}
			if (this.verkaufspreis[1] >= 89)
			{
				this.uiObjects[49].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[49].GetComponent<Button>().interactable = true;
			}
			if (this.verkaufspreis[2] <= this.verkaufspreis[1] + 10)
			{
				this.uiObjects[47].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[47].GetComponent<Button>().interactable = true;
			}
			if (this.verkaufspreis[2] >= 99)
			{
				this.uiObjects[50].GetComponent<Button>().interactable = false;
			}
			else
			{
				this.uiObjects[50].GetComponent<Button>().interactable = true;
			}
		}
		if (this.verkaufspreis[3] <= 5)
		{
			this.uiObjects[53].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[53].GetComponent<Button>().interactable = true;
		}
		if (this.verkaufspreis[3] >= 79)
		{
			this.uiObjects[54].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[54].GetComponent<Button>().interactable = true;
		}
		if (this.gS_.typ_budget || this.gS_.typ_bundle || this.gS_.typ_goty || this.gS_.typ_addon || this.gS_.typ_mmoaddon || this.gS_.typ_addonStandalone)
		{
			this.uiObjects[46].GetComponent<Button>().interactable = false;
			this.uiObjects[49].GetComponent<Button>().interactable = false;
			this.uiObjects[47].GetComponent<Button>().interactable = false;
			this.uiObjects[50].GetComponent<Button>().interactable = false;
			this.uiObjects[23].GetComponent<Text>().text = "-";
			this.uiObjects[27].GetComponent<Text>().text = "-";
			this.uiObjects[35].GetComponent<Text>().text = "-";
			this.uiObjects[31].GetComponent<Text>().text = "-";
			this.uiObjects[24].GetComponent<Text>().text = "-";
			this.uiObjects[28].GetComponent<Text>().text = "-";
			this.uiObjects[36].GetComponent<Text>().text = "-";
			this.uiObjects[32].GetComponent<Text>().text = "-";
		}
		if (this.uiObjects[39].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[51].GetComponent<Image>().color = Color.white;
			this.uiObjects[52].GetComponent<Image>().color = Color.white;
			return;
		}
		this.uiObjects[51].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		this.uiObjects[52].GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00086E74 File Offset: 0x00085074
	private float GetGewinn(int i)
	{
		float num = (float)this.verkaufspreis[i] - this.GetProduktionskosten(i);
		if (this.gS_.pubOffer && this.gS_.PUBOFFER_GetGewinnbeteiligung() > 0)
		{
			num = (float)this.gS_.SubGewinnbeteiligung(Mathf.RoundToInt(num));
		}
		return num;
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x00086EC4 File Offset: 0x000850C4
	public void Init(gameScript game_, taskGame t_, bool newGame, bool hideClose)
	{
		this.FindScripts();
		this.gS_ = game_;
		this.task_ = t_;
		if (hideClose)
		{
			this.uiObjects[56].SetActive(false);
		}
		else
		{
			this.uiObjects[56].SetActive(true);
		}
		this.InitDropdowns();
		this.uiObjects[39].GetComponent<Toggle>().interactable = true;
		this.uiObjects[42].GetComponent<Toggle>().interactable = true;
		this.Unlock(59, this.uiObjects[38], this.uiObjects[42]);
		this.uiObjects[43].GetComponent<Text>().text = this.gS_.GetNameWithTag();
		this.uiObjects[51].GetComponent<Image>().sprite = this.guiMain_.uiSprites[27];
		this.uiObjects[52].GetComponent<Image>().sprite = this.guiMain_.uiSprites[27];
		if (game_.typ_budget || game_.typ_bundle || this.gS_.typ_goty || this.gS_.typ_addon || this.gS_.typ_mmoaddon || this.gS_.typ_addonStandalone)
		{
			this.uiObjects[51].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
			this.uiObjects[52].GetComponent<Image>().sprite = this.guiMain_.uiSprites[3];
		}
		if (newGame)
		{
			this.uiObjects[39].GetComponent<Toggle>().isOn = true;
			if (this.uiObjects[42].GetComponent<Toggle>().interactable)
			{
				this.uiObjects[42].GetComponent<Toggle>().isOn = true;
			}
			this.verkaufspreis[0] = 29;
			this.verkaufspreis[1] = 39;
			this.verkaufspreis[2] = 49;
			this.verkaufspreis[3] = 29;
			for (int i = 0; i < this.standard_edition.Length; i++)
			{
				this.standard_edition[i] = false;
				this.deluxe_edition[i] = false;
				this.collectors_edition[i] = false;
			}
			this.standard_edition[0] = true;
			this.deluxe_edition[1] = true;
			this.deluxe_edition[2] = true;
			this.deluxe_edition[3] = true;
			this.deluxe_edition[4] = true;
			this.collectors_edition[1] = true;
			this.collectors_edition[2] = true;
			this.collectors_edition[3] = true;
			this.collectors_edition[4] = true;
			this.collectors_edition[5] = true;
			this.collectors_edition[6] = true;
			this.collectors_edition[7] = true;
			this.collectors_edition[8] = true;
			this.standard_edition[0] = true;
			this.deluxe_edition[0] = true;
			this.collectors_edition[0] = true;
			if (game_.typ_budget)
			{
				this.verkaufspreis[0] = 9;
				this.verkaufspreis[1] = 0;
				this.verkaufspreis[2] = 0;
				this.verkaufspreis[3] = 9;
				for (int j = 0; j < this.standard_edition.Length; j++)
				{
					this.deluxe_edition[j] = false;
					this.collectors_edition[j] = false;
				}
			}
			if (game_.typ_bundle)
			{
				this.verkaufspreis[0] = 29;
				this.verkaufspreis[1] = 0;
				this.verkaufspreis[2] = 0;
				this.verkaufspreis[3] = 29;
				for (int k = 0; k < this.standard_edition.Length; k++)
				{
					this.deluxe_edition[k] = false;
					this.collectors_edition[k] = false;
				}
			}
			if (game_.typ_bundleAddon)
			{
				this.verkaufspreis[0] = 29;
				this.verkaufspreis[1] = 39;
				this.verkaufspreis[2] = 49;
				this.verkaufspreis[3] = 29;
			}
			if (game_.typ_goty || game_.typ_addon || game_.typ_addonStandalone || game_.typ_mmoaddon)
			{
				this.verkaufspreis[0] = 19;
				this.verkaufspreis[1] = 0;
				this.verkaufspreis[2] = 0;
				this.verkaufspreis[3] = 19;
				for (int l = 0; l < this.standard_edition.Length; l++)
				{
					this.deluxe_edition[l] = false;
					this.collectors_edition[l] = false;
				}
			}
			this.LoadData();
		}
		else
		{
			this.uiObjects[39].GetComponent<Toggle>().isOn = game_.retailVersion;
			this.uiObjects[42].GetComponent<Toggle>().isOn = game_.digitalVersion;
			this.standard_edition = (bool[])this.gS_.standard_edition.Clone();
			this.deluxe_edition = (bool[])this.gS_.deluxe_edition.Clone();
			this.collectors_edition = (bool[])this.gS_.collectors_edition.Clone();
			this.verkaufspreis = (int[])this.gS_.verkaufspreis.Clone();
			this.uiObjects[58].GetComponent<Toggle>().isOn = this.gS_.autoPreis;
		}
		this.uiObjects[21].GetComponent<Text>().text = this.GetMoneyString(this.games_.GetGrundkosten());
		this.uiObjects[0].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[0]);
		this.uiObjects[1].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[1]);
		this.uiObjects[2].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[2]);
		this.uiObjects[3].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[3]);
		this.uiObjects[4].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[4]);
		this.uiObjects[5].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[5]);
		this.uiObjects[6].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[6]);
		this.uiObjects[7].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[7]);
		this.uiObjects[8].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[8]);
		this.uiObjects[9].GetComponent<Text>().text = this.GetMoneyString(this.games_.preise_inhalt[9]);
		this.uiObjects[10].GetComponent<Toggle>().isOn = this.standard_edition[0];
		this.uiObjects[11].GetComponent<Toggle>().isOn = this.standard_edition[1];
		this.uiObjects[12].GetComponent<Toggle>().isOn = this.standard_edition[2];
		this.uiObjects[13].GetComponent<Toggle>().isOn = this.standard_edition[3];
		this.uiObjects[14].GetComponent<Toggle>().isOn = this.standard_edition[4];
		this.uiObjects[15].GetComponent<Toggle>().isOn = this.standard_edition[5];
		this.uiObjects[16].GetComponent<Toggle>().isOn = this.standard_edition[6];
		this.uiObjects[17].GetComponent<Toggle>().isOn = this.standard_edition[7];
		this.uiObjects[18].GetComponent<Toggle>().isOn = this.standard_edition[8];
		this.uiObjects[19].GetComponent<Toggle>().isOn = this.standard_edition[9];
		this.uiObjects[59].SetActive(false);
		this.uiObjects[60].SetActive(false);
		if (this.gS_.pubOffer)
		{
			if (!this.gS_.pubAngebot_Retail)
			{
				this.uiObjects[59].SetActive(true);
				this.uiObjects[39].GetComponent<Toggle>().isOn = false;
				this.uiObjects[39].GetComponent<Toggle>().interactable = false;
			}
			if (!this.gS_.pubAngebot_Digital)
			{
				this.uiObjects[60].SetActive(true);
				this.uiObjects[38].SetActive(false);
				this.uiObjects[42].GetComponent<Toggle>().isOn = false;
				this.uiObjects[42].GetComponent<Toggle>().interactable = false;
			}
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x000876D2 File Offset: 0x000858D2
	private void Unlock(int id_, GameObject lock_, GameObject toggle_)
	{
		if (this.unlock_.unlock[id_])
		{
			toggle_.GetComponent<Toggle>().interactable = true;
			lock_.SetActive(false);
			return;
		}
		toggle_.GetComponent<Toggle>().interactable = false;
		lock_.SetActive(true);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0008770C File Offset: 0x0008590C
	private string GetMoneyString(float f)
	{
		string text = "$" + this.mS_.Round(f, 2);
		text = text.Replace(",", ".");
		if (text.Length == 2)
		{
			text += ".00";
		}
		if (text[text.Length - 2] == '.')
		{
			text += "0";
		}
		return text;
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0008777C File Offset: 0x0008597C
	private float GetProduktionskosten(int edition)
	{
		float num = 0f;
		switch (edition)
		{
		case 0:
			for (int i = 0; i < this.standard_edition.Length; i++)
			{
				if (this.standard_edition[i])
				{
					num += this.games_.preise_inhalt[i];
				}
			}
			break;
		case 1:
			for (int j = 0; j < this.deluxe_edition.Length; j++)
			{
				if (this.deluxe_edition[j])
				{
					num += this.games_.preise_inhalt[j];
				}
			}
			break;
		case 2:
			for (int k = 0; k < this.collectors_edition.Length; k++)
			{
				if (this.collectors_edition[k])
				{
					num += this.games_.preise_inhalt[k];
				}
			}
			break;
		case 3:
			return 0f;
		}
		return num + this.games_.GetGrundkosten();
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0008784C File Offset: 0x00085A4C
	public void InitDropdowns()
	{
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(1103));
		if (!this.gS_.typ_budget && !this.gS_.typ_bundle && !this.gS_.typ_goty && !this.gS_.typ_addon && !this.gS_.typ_mmoaddon && !this.gS_.typ_addonStandalone)
		{
			list.Add(this.tS_.GetText(1104));
			list.Add(this.tS_.GetText(1105));
		}
		this.uiObjects[20].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[20].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[20].GetComponent<Dropdown>().value = 0;
		this.DROPDOWN_Edition();
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x00087930 File Offset: 0x00085B30
	public void TOGGLE_ManualSW()
	{
		if (this.uiObjects[11].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[11].GetComponent<Toggle>().isOn = false;
		}
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0008795B File Offset: 0x00085B5B
	public void TOGGLE_ManualColor()
	{
		if (this.uiObjects[10].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[10].GetComponent<Toggle>().isOn = false;
		}
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00087988 File Offset: 0x00085B88
	private void UpdatePackung()
	{
		if (this.packObjects[0].activeSelf != this.uiObjects[10].GetComponent<Toggle>().isOn)
		{
			this.packObjects[0].SetActive(this.uiObjects[10].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[1].activeSelf != this.uiObjects[11].GetComponent<Toggle>().isOn)
		{
			this.packObjects[1].SetActive(this.uiObjects[11].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[2].activeSelf != this.uiObjects[12].GetComponent<Toggle>().isOn)
		{
			this.packObjects[2].SetActive(this.uiObjects[12].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[3].activeSelf != this.uiObjects[13].GetComponent<Toggle>().isOn)
		{
			this.packObjects[3].SetActive(this.uiObjects[13].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[4].activeSelf != this.uiObjects[14].GetComponent<Toggle>().isOn)
		{
			this.packObjects[4].SetActive(this.uiObjects[14].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[5].activeSelf != this.uiObjects[15].GetComponent<Toggle>().isOn)
		{
			this.packObjects[5].SetActive(this.uiObjects[15].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[6].activeSelf != this.uiObjects[16].GetComponent<Toggle>().isOn)
		{
			this.packObjects[6].SetActive(this.uiObjects[16].GetComponent<Toggle>().isOn);
		}
		if (this.packObjects[7].activeSelf != this.uiObjects[17].GetComponent<Toggle>().isOn)
		{
			this.packObjects[7].SetActive(this.uiObjects[17].GetComponent<Toggle>().isOn);
		}
		if (this.mS_.year <= 1990)
		{
			if (this.packObjects[8].activeSelf != this.uiObjects[18].GetComponent<Toggle>().isOn)
			{
				this.packObjects[8].SetActive(this.uiObjects[18].GetComponent<Toggle>().isOn);
				this.packObjects[9].SetActive(false);
			}
		}
		else if (this.packObjects[9].activeSelf != this.uiObjects[18].GetComponent<Toggle>().isOn)
		{
			this.packObjects[9].SetActive(this.uiObjects[18].GetComponent<Toggle>().isOn);
			this.packObjects[8].SetActive(false);
		}
		if (this.packObjects[10].activeSelf != this.uiObjects[19].GetComponent<Toggle>().isOn)
		{
			this.packObjects[10].SetActive(this.uiObjects[19].GetComponent<Toggle>().isOn);
		}
		switch (this.uiObjects[20].GetComponent<Dropdown>().value)
		{
		case 0:
			this.standard_edition[0] = this.uiObjects[10].GetComponent<Toggle>().isOn;
			this.standard_edition[1] = this.uiObjects[11].GetComponent<Toggle>().isOn;
			this.standard_edition[2] = this.uiObjects[12].GetComponent<Toggle>().isOn;
			this.standard_edition[3] = this.uiObjects[13].GetComponent<Toggle>().isOn;
			this.standard_edition[4] = this.uiObjects[14].GetComponent<Toggle>().isOn;
			this.standard_edition[5] = this.uiObjects[15].GetComponent<Toggle>().isOn;
			this.standard_edition[6] = this.uiObjects[16].GetComponent<Toggle>().isOn;
			this.standard_edition[7] = this.uiObjects[17].GetComponent<Toggle>().isOn;
			this.standard_edition[8] = this.uiObjects[18].GetComponent<Toggle>().isOn;
			this.standard_edition[9] = this.uiObjects[19].GetComponent<Toggle>().isOn;
			break;
		case 1:
			this.deluxe_edition[0] = this.uiObjects[10].GetComponent<Toggle>().isOn;
			this.deluxe_edition[1] = this.uiObjects[11].GetComponent<Toggle>().isOn;
			this.deluxe_edition[2] = this.uiObjects[12].GetComponent<Toggle>().isOn;
			this.deluxe_edition[3] = this.uiObjects[13].GetComponent<Toggle>().isOn;
			this.deluxe_edition[4] = this.uiObjects[14].GetComponent<Toggle>().isOn;
			this.deluxe_edition[5] = this.uiObjects[15].GetComponent<Toggle>().isOn;
			this.deluxe_edition[6] = this.uiObjects[16].GetComponent<Toggle>().isOn;
			this.deluxe_edition[7] = this.uiObjects[17].GetComponent<Toggle>().isOn;
			this.deluxe_edition[8] = this.uiObjects[18].GetComponent<Toggle>().isOn;
			this.deluxe_edition[9] = this.uiObjects[19].GetComponent<Toggle>().isOn;
			break;
		case 2:
			this.collectors_edition[0] = this.uiObjects[10].GetComponent<Toggle>().isOn;
			this.collectors_edition[1] = this.uiObjects[11].GetComponent<Toggle>().isOn;
			this.collectors_edition[2] = this.uiObjects[12].GetComponent<Toggle>().isOn;
			this.collectors_edition[3] = this.uiObjects[13].GetComponent<Toggle>().isOn;
			this.collectors_edition[4] = this.uiObjects[14].GetComponent<Toggle>().isOn;
			this.collectors_edition[5] = this.uiObjects[15].GetComponent<Toggle>().isOn;
			this.collectors_edition[6] = this.uiObjects[16].GetComponent<Toggle>().isOn;
			this.collectors_edition[7] = this.uiObjects[17].GetComponent<Toggle>().isOn;
			this.collectors_edition[8] = this.uiObjects[18].GetComponent<Toggle>().isOn;
			this.collectors_edition[9] = this.uiObjects[19].GetComponent<Toggle>().isOn;
			break;
		}
		for (int i = 2; i < this.standard_edition.Length; i++)
		{
			if (this.standard_edition[i])
			{
				this.deluxe_edition[i] = true;
			}
			if (this.deluxe_edition[i])
			{
				this.collectors_edition[i] = true;
			}
		}
		if (this.standard_edition[1])
		{
			this.deluxe_edition[0] = false;
			this.deluxe_edition[1] = true;
		}
		if (this.deluxe_edition[1])
		{
			this.collectors_edition[0] = false;
			this.collectors_edition[1] = true;
		}
		if (this.gS_.lagerbestand[0] > 0 || this.gS_.lagerbestand[1] > 0 || this.gS_.lagerbestand[2] > 0 || this.gS_.typ_budget || this.gS_.typ_bundle)
		{
			if (!this.uiObjects[55].activeSelf)
			{
				this.uiObjects[55].SetActive(true);
			}
			this.uiObjects[57].GetComponent<Text>().text = this.tS_.GetText(1140);
			if (this.gS_.typ_budget)
			{
				this.uiObjects[57].GetComponent<Text>().text = this.tS_.GetText(1159);
			}
			if (this.gS_.typ_bundle)
			{
				this.uiObjects[57].GetComponent<Text>().text = this.tS_.GetText(1347);
			}
			for (int j = 0; j < this.standard_edition.Length; j++)
			{
				this.uiObjects[10 + j].GetComponent<Toggle>().interactable = false;
			}
			return;
		}
		if (this.uiObjects[55].activeSelf)
		{
			this.uiObjects[55].SetActive(false);
		}
		for (int k = 1; k < this.standard_edition.Length; k++)
		{
			switch (this.uiObjects[20].GetComponent<Dropdown>().value)
			{
			case 0:
				this.uiObjects[10 + k].GetComponent<Toggle>().interactable = true;
				break;
			case 1:
				if (this.standard_edition[k])
				{
					this.uiObjects[10 + k].GetComponent<Toggle>().interactable = false;
				}
				else
				{
					this.uiObjects[10 + k].GetComponent<Toggle>().interactable = true;
				}
				break;
			case 2:
				if (this.standard_edition[k] || this.deluxe_edition[k])
				{
					this.uiObjects[10 + k].GetComponent<Toggle>().interactable = false;
				}
				else
				{
					this.uiObjects[10 + k].GetComponent<Toggle>().interactable = true;
				}
				break;
			}
		}
		switch (this.uiObjects[20].GetComponent<Dropdown>().value)
		{
		case 0:
			this.uiObjects[10].GetComponent<Toggle>().interactable = true;
			return;
		case 1:
			if (this.standard_edition[1])
			{
				this.uiObjects[10].GetComponent<Toggle>().interactable = false;
				return;
			}
			this.uiObjects[10].GetComponent<Toggle>().interactable = true;
			return;
		case 2:
			if (this.deluxe_edition[1])
			{
				this.uiObjects[10].GetComponent<Toggle>().interactable = false;
				return;
			}
			this.uiObjects[10].GetComponent<Toggle>().interactable = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0008832C File Offset: 0x0008652C
	public void DROPDOWN_Edition()
	{
		switch (this.uiObjects[20].GetComponent<Dropdown>().value)
		{
		case 0:
			this.uiObjects[10].GetComponent<Toggle>().isOn = this.standard_edition[0];
			this.uiObjects[11].GetComponent<Toggle>().isOn = this.standard_edition[1];
			this.uiObjects[12].GetComponent<Toggle>().isOn = this.standard_edition[2];
			this.uiObjects[13].GetComponent<Toggle>().isOn = this.standard_edition[3];
			this.uiObjects[14].GetComponent<Toggle>().isOn = this.standard_edition[4];
			this.uiObjects[15].GetComponent<Toggle>().isOn = this.standard_edition[5];
			this.uiObjects[16].GetComponent<Toggle>().isOn = this.standard_edition[6];
			this.uiObjects[17].GetComponent<Toggle>().isOn = this.standard_edition[7];
			this.uiObjects[18].GetComponent<Toggle>().isOn = this.standard_edition[8];
			this.uiObjects[19].GetComponent<Toggle>().isOn = this.standard_edition[9];
			this.uiObjects[18].SetActive(false);
			this.uiObjects[19].SetActive(false);
			return;
		case 1:
			this.uiObjects[10].GetComponent<Toggle>().isOn = this.deluxe_edition[0];
			this.uiObjects[11].GetComponent<Toggle>().isOn = this.deluxe_edition[1];
			this.uiObjects[12].GetComponent<Toggle>().isOn = this.deluxe_edition[2];
			this.uiObjects[13].GetComponent<Toggle>().isOn = this.deluxe_edition[3];
			this.uiObjects[14].GetComponent<Toggle>().isOn = this.deluxe_edition[4];
			this.uiObjects[15].GetComponent<Toggle>().isOn = this.deluxe_edition[5];
			this.uiObjects[16].GetComponent<Toggle>().isOn = this.deluxe_edition[6];
			this.uiObjects[17].GetComponent<Toggle>().isOn = this.deluxe_edition[7];
			this.uiObjects[18].GetComponent<Toggle>().isOn = this.deluxe_edition[8];
			this.uiObjects[19].GetComponent<Toggle>().isOn = this.deluxe_edition[9];
			this.uiObjects[18].SetActive(true);
			this.uiObjects[19].SetActive(false);
			return;
		case 2:
			this.uiObjects[10].GetComponent<Toggle>().isOn = this.collectors_edition[0];
			this.uiObjects[11].GetComponent<Toggle>().isOn = this.collectors_edition[1];
			this.uiObjects[12].GetComponent<Toggle>().isOn = this.collectors_edition[2];
			this.uiObjects[13].GetComponent<Toggle>().isOn = this.collectors_edition[3];
			this.uiObjects[14].GetComponent<Toggle>().isOn = this.collectors_edition[4];
			this.uiObjects[15].GetComponent<Toggle>().isOn = this.collectors_edition[5];
			this.uiObjects[16].GetComponent<Toggle>().isOn = this.collectors_edition[6];
			this.uiObjects[17].GetComponent<Toggle>().isOn = this.collectors_edition[7];
			this.uiObjects[18].GetComponent<Toggle>().isOn = this.collectors_edition[8];
			this.uiObjects[19].GetComponent<Toggle>().isOn = this.collectors_edition[9];
			this.uiObjects[18].SetActive(true);
			this.uiObjects[19].SetActive(true);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x000886E9 File Offset: 0x000868E9
	private IEnumerator iMinusPreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_MinusPreis(i);
		}
		yield break;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x00088700 File Offset: 0x00086900
	public void BUTTON_MinusPreis(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			this.verkaufspreis[i] -= 10;
		}
		else
		{
			this.verkaufspreis[i]--;
		}
		if (this.verkaufspreis[0] < 5)
		{
			this.verkaufspreis[0] = 5;
		}
		if (this.verkaufspreis[1] < 6)
		{
			this.verkaufspreis[1] = 6;
		}
		if (this.verkaufspreis[2] < 7)
		{
			this.verkaufspreis[2] = 7;
		}
		if (this.verkaufspreis[3] < 5)
		{
			this.verkaufspreis[3] = 5;
		}
		base.StartCoroutine(this.iMinusPreis(i));
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x000887B5 File Offset: 0x000869B5
	private IEnumerator iPlusPreis(int i)
	{
		yield return new WaitForSeconds(0.2f);
		if (Input.GetMouseButton(0))
		{
			this.BUTTON_PlusPreis(i);
		}
		yield break;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x000887CC File Offset: 0x000869CC
	public void BUTTON_PlusPreis(int i)
	{
		this.sfx_.PlaySound(3, true);
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			this.verkaufspreis[i] += 10;
		}
		else
		{
			this.verkaufspreis[i]++;
		}
		if (this.gS_.typ_budget)
		{
			if (this.verkaufspreis[0] > 10)
			{
				this.verkaufspreis[0] = 10;
			}
			if (this.verkaufspreis[3] > 10)
			{
				this.verkaufspreis[3] = 10;
			}
			base.StartCoroutine(this.iPlusPreis(i));
			return;
		}
		if (this.gS_.typ_goty)
		{
			if (this.verkaufspreis[0] > 19)
			{
				this.verkaufspreis[0] = 19;
			}
			if (this.verkaufspreis[1] > 29)
			{
				this.verkaufspreis[1] = 29;
			}
			if (this.verkaufspreis[2] > 39)
			{
				this.verkaufspreis[2] = 39;
			}
			if (this.verkaufspreis[3] > 19)
			{
				this.verkaufspreis[3] = 19;
			}
			base.StartCoroutine(this.iPlusPreis(i));
			return;
		}
		if (this.gS_.typ_addon || this.gS_.typ_addonStandalone || this.gS_.typ_mmoaddon)
		{
			if (this.verkaufspreis[0] > 29)
			{
				this.verkaufspreis[0] = 29;
			}
			if (this.verkaufspreis[1] > 39)
			{
				this.verkaufspreis[1] = 39;
			}
			if (this.verkaufspreis[2] > 49)
			{
				this.verkaufspreis[2] = 49;
			}
			if (this.verkaufspreis[3] > 29)
			{
				this.verkaufspreis[3] = 29;
			}
			base.StartCoroutine(this.iPlusPreis(i));
			return;
		}
		if (this.verkaufspreis[0] > 79)
		{
			this.verkaufspreis[0] = 79;
		}
		if (this.verkaufspreis[1] > 89)
		{
			this.verkaufspreis[1] = 89;
		}
		if (this.verkaufspreis[2] > 99)
		{
			this.verkaufspreis[2] = 99;
		}
		if (this.verkaufspreis[3] > 79)
		{
			this.verkaufspreis[3] = 79;
		}
		base.StartCoroutine(this.iPlusPreis(i));
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x000889D3 File Offset: 0x00086BD3
	private float GetQualityPackung(int i)
	{
		return (this.GetProduktionskosten(i) - this.games_.GetGrundkosten()) * 10f;
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x000889F0 File Offset: 0x00086BF0
	public void BUTTON_Close()
	{
		if (!this.guiMain_.uiObjects[220].activeSelf)
		{
			this.gS_.ClearReview();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[69]);
			this.guiMain_.uiObjects[69].GetComponent<Menu_DevGame_Complete>().Init(this.gS_, this.task_);
		}
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x00088A78 File Offset: 0x00086C78
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.uiObjects[39].GetComponent<Toggle>().isOn && !this.uiObjects[42].GetComponent<Toggle>().isOn)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(1119), false);
			return;
		}
		if (!this.gS_.typ_budget && !this.gS_.typ_bundle && !this.gS_.typ_goty && !this.gS_.typ_addon && !this.gS_.typ_mmoaddon && !this.gS_.typ_addonStandalone && this.uiObjects[39].GetComponent<Toggle>().isOn)
		{
			if (this.GetProduktionskosten(1) <= this.GetProduktionskosten(0))
			{
				this.guiMain_.MessageBox(this.tS_.GetText(1120), false);
				return;
			}
			if (this.GetProduktionskosten(2) <= this.GetProduktionskosten(0))
			{
				this.guiMain_.MessageBox(this.tS_.GetText(1121), false);
				return;
			}
			if (this.GetProduktionskosten(2) <= this.GetProduktionskosten(1))
			{
				this.guiMain_.MessageBox(this.tS_.GetText(1121), false);
				return;
			}
		}
		int num = 0;
		for (int i = 0; i < this.gS_.verkaufspreis.Length; i++)
		{
			num += this.verkaufspreis[i] - this.gS_.verkaufspreis[i];
		}
		this.gS_.standard_edition = (bool[])this.standard_edition.Clone();
		this.gS_.deluxe_edition = (bool[])this.deluxe_edition.Clone();
		this.gS_.collectors_edition = (bool[])this.collectors_edition.Clone();
		this.gS_.verkaufspreis = (int[])this.verkaufspreis.Clone();
		this.gS_.digitalVersion = this.uiObjects[42].GetComponent<Toggle>().isOn;
		this.gS_.retailVersion = this.uiObjects[39].GetComponent<Toggle>().isOn;
		this.gS_.autoPreis = this.uiObjects[58].GetComponent<Toggle>().isOn;
		if (!this.guiMain_.uiObjects[220].activeSelf)
		{
			this.SaveData();
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[219]);
			this.guiMain_.uiObjects[219].GetComponent<Menu_ReleaseDate>().Init(this.gS_, this.task_);
			return;
		}
		base.gameObject.SetActive(false);
		if (this.gS_.vorbestellungen > 0 && num > 0)
		{
			int num2 = this.gS_.vorbestellungen / 100 * num;
			if (num2 > this.gS_.vorbestellungen)
			{
				num2 = this.gS_.vorbestellungen;
			}
			if (num2 > 0)
			{
				this.gS_.vorbestellungen -= num2;
				if (this.gS_.vorbestellungen < 0)
				{
					this.gS_.vorbestellungen = 0;
				}
				string text = this.tS_.GetText(2013);
				text = text.Replace("<NUM>", "<color=red>" + this.mS_.GetMoney((long)num2, false) + "</color>");
				this.guiMain_.MessageBox(text, false);
			}
		}
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00088DFC File Offset: 0x00086FFC
	private void SaveData()
	{
		if (this.gS_.typ_bundle)
		{
			this.verkaufspreis_default_bundle = (int[])this.verkaufspreis.Clone();
			this.standard_default_bundle = (bool[])this.standard_edition.Clone();
			this.deluxe_default_bundle = (bool[])this.deluxe_edition.Clone();
			this.collectors_default_bundle = (bool[])this.collectors_edition.Clone();
			return;
		}
		if (this.gS_.typ_bundleAddon)
		{
			this.verkaufspreis_default_bundleAddon = (int[])this.verkaufspreis.Clone();
			this.standard_default_bundleAddon = (bool[])this.standard_edition.Clone();
			this.deluxe_default_bundleAddon = (bool[])this.deluxe_edition.Clone();
			this.collectors_default_bundleAddon = (bool[])this.collectors_edition.Clone();
			return;
		}
		if (this.gS_.typ_addon || this.gS_.typ_addonStandalone || this.gS_.typ_mmoaddon)
		{
			this.verkaufspreis_default_addon = (int[])this.verkaufspreis.Clone();
			this.standard_default_addon = (bool[])this.standard_edition.Clone();
			this.deluxe_default_addon = (bool[])this.deluxe_edition.Clone();
			this.collectors_default_addon = (bool[])this.collectors_edition.Clone();
			return;
		}
		if (this.gS_.typ_budget)
		{
			this.verkaufspreis_default_budget = (int[])this.verkaufspreis.Clone();
			this.standard_default_budget = (bool[])this.standard_edition.Clone();
			this.deluxe_default_budget = (bool[])this.deluxe_edition.Clone();
			this.collectors_default_budget = (bool[])this.collectors_edition.Clone();
			return;
		}
		if (this.gS_.typ_goty)
		{
			this.verkaufspreis_default_goty = (int[])this.verkaufspreis.Clone();
			this.standard_default_goty = (bool[])this.standard_edition.Clone();
			this.deluxe_default_goty = (bool[])this.deluxe_edition.Clone();
			this.collectors_default_goty = (bool[])this.collectors_edition.Clone();
			return;
		}
		this.verkaufspreis_default_standard = (int[])this.verkaufspreis.Clone();
		this.standard_default_standard = (bool[])this.standard_edition.Clone();
		this.deluxe_default_standard = (bool[])this.deluxe_edition.Clone();
		this.collectors_default_standard = (bool[])this.collectors_edition.Clone();
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0008907C File Offset: 0x0008727C
	private void LoadData()
	{
		if (this.gS_.typ_bundle)
		{
			if (this.verkaufspreis_default_bundle[0] != 0)
			{
				this.verkaufspreis = (int[])this.verkaufspreis_default_bundle.Clone();
				this.standard_edition = (bool[])this.standard_default_bundle.Clone();
				this.deluxe_edition = (bool[])this.deluxe_default_bundle.Clone();
				this.collectors_edition = (bool[])this.collectors_default_bundle.Clone();
			}
			return;
		}
		if (this.gS_.typ_bundleAddon)
		{
			if (this.verkaufspreis_default_bundleAddon[0] != 0)
			{
				this.verkaufspreis = (int[])this.verkaufspreis_default_bundleAddon.Clone();
				this.standard_edition = (bool[])this.standard_default_bundleAddon.Clone();
				this.deluxe_edition = (bool[])this.deluxe_default_bundleAddon.Clone();
				this.collectors_edition = (bool[])this.collectors_default_bundleAddon.Clone();
			}
			return;
		}
		if (this.gS_.typ_addon || this.gS_.typ_addonStandalone || this.gS_.typ_mmoaddon)
		{
			if (this.verkaufspreis_default_addon[0] != 0)
			{
				this.verkaufspreis = (int[])this.verkaufspreis_default_addon.Clone();
				this.standard_edition = (bool[])this.standard_default_addon.Clone();
				this.deluxe_edition = (bool[])this.deluxe_default_addon.Clone();
				this.collectors_edition = (bool[])this.collectors_default_addon.Clone();
			}
			return;
		}
		if (this.gS_.typ_budget)
		{
			if (this.verkaufspreis_default_budget[0] != 0)
			{
				this.verkaufspreis = (int[])this.verkaufspreis_default_budget.Clone();
				this.standard_edition = (bool[])this.standard_default_budget.Clone();
				this.deluxe_edition = (bool[])this.deluxe_default_budget.Clone();
				this.collectors_edition = (bool[])this.collectors_default_budget.Clone();
			}
			return;
		}
		if (this.gS_.typ_goty)
		{
			if (this.verkaufspreis_default_goty[0] != 0)
			{
				this.verkaufspreis = (int[])this.verkaufspreis_default_goty.Clone();
				this.standard_edition = (bool[])this.standard_default_goty.Clone();
				this.deluxe_edition = (bool[])this.deluxe_default_goty.Clone();
				this.collectors_edition = (bool[])this.collectors_default_goty.Clone();
			}
			return;
		}
		if (this.verkaufspreis_default_standard[0] != 0)
		{
			this.verkaufspreis = (int[])this.verkaufspreis_default_standard.Clone();
			this.standard_edition = (bool[])this.standard_default_standard.Clone();
			this.deluxe_edition = (bool[])this.deluxe_default_standard.Clone();
			this.collectors_edition = (bool[])this.collectors_default_standard.Clone();
		}
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x00089335 File Offset: 0x00087535
	public void TOGGLE_Autopreis()
	{
		if (this.uiObjects[58].GetComponent<Toggle>().isOn)
		{
			this.gS_.UpdateAutoPreis();
			this.verkaufspreis = (int[])this.gS_.verkaufspreis.Clone();
		}
	}

	// Token: 0x040010D0 RID: 4304
	private mainScript mS_;

	// Token: 0x040010D1 RID: 4305
	private GameObject main_;

	// Token: 0x040010D2 RID: 4306
	private GUI_Main guiMain_;

	// Token: 0x040010D3 RID: 4307
	private sfxScript sfx_;

	// Token: 0x040010D4 RID: 4308
	private textScript tS_;

	// Token: 0x040010D5 RID: 4309
	private themes themes_;

	// Token: 0x040010D6 RID: 4310
	private Menu_DevGame mDevGame_;

	// Token: 0x040010D7 RID: 4311
	private genres genres_;

	// Token: 0x040010D8 RID: 4312
	private games games_;

	// Token: 0x040010D9 RID: 4313
	private unlockScript unlock_;

	// Token: 0x040010DA RID: 4314
	private gameScript gS_;

	// Token: 0x040010DB RID: 4315
	private taskGame task_;

	// Token: 0x040010DC RID: 4316
	public GameObject[] uiPrefabs;

	// Token: 0x040010DD RID: 4317
	public GameObject[] uiObjects;

	// Token: 0x040010DE RID: 4318
	public GameObject[] packObjects;

	// Token: 0x040010DF RID: 4319
	public bool[] standard_edition;

	// Token: 0x040010E0 RID: 4320
	public bool[] deluxe_edition;

	// Token: 0x040010E1 RID: 4321
	public bool[] collectors_edition;

	// Token: 0x040010E2 RID: 4322
	public int[] verkaufspreis;

	// Token: 0x040010E3 RID: 4323
	public int[] verkaufspreis_default_bundle;

	// Token: 0x040010E4 RID: 4324
	public int[] verkaufspreis_default_bundleAddon;

	// Token: 0x040010E5 RID: 4325
	public int[] verkaufspreis_default_addon;

	// Token: 0x040010E6 RID: 4326
	public int[] verkaufspreis_default_budget;

	// Token: 0x040010E7 RID: 4327
	public int[] verkaufspreis_default_goty;

	// Token: 0x040010E8 RID: 4328
	public int[] verkaufspreis_default_standard;

	// Token: 0x040010E9 RID: 4329
	public bool[] standard_default_bundleAddon;

	// Token: 0x040010EA RID: 4330
	public bool[] deluxe_default_bundleAddon;

	// Token: 0x040010EB RID: 4331
	public bool[] collectors_default_bundleAddon;

	// Token: 0x040010EC RID: 4332
	public bool[] standard_default_bundle;

	// Token: 0x040010ED RID: 4333
	public bool[] deluxe_default_bundle;

	// Token: 0x040010EE RID: 4334
	public bool[] collectors_default_bundle;

	// Token: 0x040010EF RID: 4335
	public bool[] standard_default_addon;

	// Token: 0x040010F0 RID: 4336
	public bool[] deluxe_default_addon;

	// Token: 0x040010F1 RID: 4337
	public bool[] collectors_default_addon;

	// Token: 0x040010F2 RID: 4338
	public bool[] standard_default_budget;

	// Token: 0x040010F3 RID: 4339
	public bool[] deluxe_default_budget;

	// Token: 0x040010F4 RID: 4340
	public bool[] collectors_default_budget;

	// Token: 0x040010F5 RID: 4341
	public bool[] standard_default_goty;

	// Token: 0x040010F6 RID: 4342
	public bool[] deluxe_default_goty;

	// Token: 0x040010F7 RID: 4343
	public bool[] collectors_default_goty;

	// Token: 0x040010F8 RID: 4344
	public bool[] standard_default_standard;

	// Token: 0x040010F9 RID: 4345
	public bool[] deluxe_default_standard;

	// Token: 0x040010FA RID: 4346
	public bool[] collectors_default_standard;
}
