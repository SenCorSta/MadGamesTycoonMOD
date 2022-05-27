using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CE RID: 462
public class Menu_MesseSelect : MonoBehaviour
{
	// Token: 0x06001177 RID: 4471 RVA: 0x000B8B0D File Offset: 0x000B6D0D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x000B8B18 File Offset: 0x000B6D18
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
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x000B8BC4 File Offset: 0x000B6DC4
	public void Init(int standgroesse)
	{
		this.standGroesse = standgroesse;
		for (int i = 0; i < this.games.Length; i++)
		{
			this.games[i] = null;
		}
		this.FindScripts();
		for (int j = 0; j < this.games.Length; j++)
		{
			this.SetGame(j, null);
		}
		for (int k = 0; k < this.konsolen.Length; k++)
		{
			this.SetKonsole(k, null);
		}
		switch (standgroesse)
		{
		case 0:
			this.uiObjects[5].GetComponent<Button>().interactable = false;
			this.uiObjects[6].GetComponent<Button>().interactable = false;
			this.uiObjects[7].GetComponent<Button>().interactable = false;
			this.uiObjects[8].GetComponent<Button>().interactable = false;
			return;
		case 1:
			this.uiObjects[5].GetComponent<Button>().interactable = true;
			this.uiObjects[6].GetComponent<Button>().interactable = false;
			this.uiObjects[7].GetComponent<Button>().interactable = true;
			this.uiObjects[8].GetComponent<Button>().interactable = false;
			return;
		case 2:
			this.uiObjects[5].GetComponent<Button>().interactable = true;
			this.uiObjects[6].GetComponent<Button>().interactable = true;
			this.uiObjects[7].GetComponent<Button>().interactable = true;
			this.uiObjects[8].GetComponent<Button>().interactable = true;
			return;
		default:
			return;
		}
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x000B8D29 File Offset: 0x000B6F29
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x000B8D44 File Offset: 0x000B6F44
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x000B8D60 File Offset: 0x000B6F60
	public void BUTTON_SelectGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[187]);
		this.guiMain_.uiObjects[187].GetComponent<Menu_MesseSelectGame>().Init(i);
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x000B8DB4 File Offset: 0x000B6FB4
	public void BUTTON_SelectKonsole(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[323]);
		this.guiMain_.uiObjects[323].GetComponent<Menu_MesseSelectKonsole>().Init(i);
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x000B8E08 File Offset: 0x000B7008
	public void SetGame(int slot_, gameScript game_)
	{
		this.games[slot_] = game_;
		if (this.games[0])
		{
			this.uiObjects[0].GetComponent<Text>().text = "<b>" + this.games[0].GetNameWithTag() + "</b>";
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(948);
		}
		if (this.games[1])
		{
			this.uiObjects[1].GetComponent<Text>().text = "<b>" + this.games[1].GetNameWithTag() + "</b>";
		}
		else
		{
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(948);
		}
		if (this.games[2])
		{
			this.uiObjects[2].GetComponent<Text>().text = "<b>" + this.games[2].GetNameWithTag() + "</b>";
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(948);
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x000B8F40 File Offset: 0x000B7140
	public void SetKonsole(int slot_, platformScript script_)
	{
		this.konsolen[slot_] = script_;
		if (this.konsolen[0])
		{
			this.uiObjects[3].GetComponent<Text>().text = "<b>" + this.konsolen[0].GetName() + "</b>";
		}
		else
		{
			this.uiObjects[3].GetComponent<Text>().text = this.tS_.GetText(949);
		}
		if (this.konsolen[1])
		{
			this.uiObjects[9].GetComponent<Text>().text = "<b>" + this.konsolen[1].GetName() + "</b>";
			return;
		}
		this.uiObjects[9].GetComponent<Text>().text = this.tS_.GetText(949);
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x000B901C File Offset: 0x000B721C
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		if (this.games[0] == null && this.games[1] == null && this.games[2] == null && this.konsolen[0] == null && this.konsolen[1] == null)
		{
			this.guiMain_.MessageBox(this.tS_.GetText(951), false);
			return;
		}
		Menu_Messe component = this.guiMain_.uiObjects[185].GetComponent<Menu_Messe>();
		if (component)
		{
			this.mS_.Pay((long)component.GetPrice(this.standGroesse), 17);
		}
		if (component)
		{
			int num = component.GetPrice(this.standGroesse);
			int num2 = 0;
			for (int i = 0; i < this.games.Length; i++)
			{
				if (this.games[i])
				{
					num2++;
				}
			}
			if (this.konsolen[0])
			{
				num2++;
			}
			if (num2 > 0)
			{
				num /= num2;
			}
			for (int j = 0; j < this.games.Length; j++)
			{
				if (this.games[j])
				{
					this.games[j].GetComponent<gameScript>().costs_marketing += (long)num;
				}
			}
			if (this.konsolen[0])
			{
				this.konsolen[0].GetComponent<platformScript>().costs_marketing += num;
			}
		}
		this.guiMain_.uiObjects[185].SetActive(false);
		this.guiMain_.uiObjects[188].SetActive(true);
		this.guiMain_.uiObjects[188].GetComponent<Menu_MesseErgebnis>().Init();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040015F7 RID: 5623
	public GameObject[] uiObjects;

	// Token: 0x040015F8 RID: 5624
	private GameObject main_;

	// Token: 0x040015F9 RID: 5625
	private mainScript mS_;

	// Token: 0x040015FA RID: 5626
	private textScript tS_;

	// Token: 0x040015FB RID: 5627
	private GUI_Main guiMain_;

	// Token: 0x040015FC RID: 5628
	private sfxScript sfx_;

	// Token: 0x040015FD RID: 5629
	public int standGroesse;

	// Token: 0x040015FE RID: 5630
	public gameScript[] games;

	// Token: 0x040015FF RID: 5631
	public platformScript[] konsolen;
}
