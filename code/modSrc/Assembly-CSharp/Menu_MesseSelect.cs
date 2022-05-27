using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CD RID: 461
public class Menu_MesseSelect : MonoBehaviour
{
	// Token: 0x0600115D RID: 4445 RVA: 0x0000C233 File Offset: 0x0000A433
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x000C416C File Offset: 0x000C236C
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

	// Token: 0x0600115F RID: 4447 RVA: 0x000C4218 File Offset: 0x000C2418
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

	// Token: 0x06001160 RID: 4448 RVA: 0x0000C23B File Offset: 0x0000A43B
	private void Update()
	{
		if (!this.guiMain_.menuOpen)
		{
			this.guiMain_.OpenMenu(false);
		}
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0000C256 File Offset: 0x0000A456
	public void BUTTON_Abbrechen()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000C4380 File Offset: 0x000C2580
	public void BUTTON_SelectGame(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[187]);
		this.guiMain_.uiObjects[187].GetComponent<Menu_MesseSelectGame>().Init(i);
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000C43D4 File Offset: 0x000C25D4
	public void BUTTON_SelectKonsole(int i)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[323]);
		this.guiMain_.uiObjects[323].GetComponent<Menu_MesseSelectKonsole>().Init(i);
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000C4428 File Offset: 0x000C2628
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

	// Token: 0x06001165 RID: 4453 RVA: 0x000C4560 File Offset: 0x000C2760
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

	// Token: 0x06001166 RID: 4454 RVA: 0x000C463C File Offset: 0x000C283C
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

	// Token: 0x040015EE RID: 5614
	public GameObject[] uiObjects;

	// Token: 0x040015EF RID: 5615
	private GameObject main_;

	// Token: 0x040015F0 RID: 5616
	private mainScript mS_;

	// Token: 0x040015F1 RID: 5617
	private textScript tS_;

	// Token: 0x040015F2 RID: 5618
	private GUI_Main guiMain_;

	// Token: 0x040015F3 RID: 5619
	private sfxScript sfx_;

	// Token: 0x040015F4 RID: 5620
	public int standGroesse;

	// Token: 0x040015F5 RID: 5621
	public gameScript[] games;

	// Token: 0x040015F6 RID: 5622
	public platformScript[] konsolen;
}
