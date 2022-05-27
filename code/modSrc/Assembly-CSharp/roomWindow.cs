using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200027A RID: 634
public class roomWindow : MonoBehaviour
{
	// Token: 0x060018D6 RID: 6358 RVA: 0x000F66C0 File Offset: 0x000F48C0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x000F66C8 File Offset: 0x000F48C8
	private void OnEnable()
	{
		if (this.uiObjects[1])
		{
			this.uiObjects[1].GetComponent<Image>().fillAmount = 0.1f;
		}
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x000F66F0 File Offset: 0x000F48F0
	private void FindScripts()
	{
		if (!this.main_)
		{
			this.main_ = GameObject.FindGameObjectWithTag("Main");
		}
		if (!this.tS_)
		{
			this.tS_ = this.main_.GetComponent<textScript>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
		if (!this.contractWorkMain_)
		{
			this.contractWorkMain_ = this.main_.GetComponent<contractWorkMain>();
		}
		if (!this.guiMain_)
		{
			this.guiMain_ = GameObject.Find("CanvasInGameMenu").GetComponent<GUI_Main>();
		}
		if (!this.scriptMarketing_)
		{
			this.scriptMarketing_ = this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>();
		}
	}

	// Token: 0x060018D9 RID: 6361 RVA: 0x000F67F8 File Offset: 0x000F49F8
	public void Window_Konsole(taskKonsole task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.pS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.pS_.GetName();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(task_.pS_.GetHype()).ToString();
		task_.pS_.SetPic(this.uiObjects[5]);
		this.uiObjects[6].GetComponent<Text>().text = task_.pS_.tech.ToString();
		if (task_.leitenderTechnikerID != -1)
		{
			if (this.uiObjects[7].activeSelf)
			{
				this.uiObjects[7].SetActive(false);
			}
			return;
		}
		if (!this.uiObjects[7].activeSelf)
		{
			this.uiObjects[7].SetActive(true);
			return;
		}
		this.uiObjects[7].transform.GetChild(0).GetComponent<Text>().text = this.tS_.GetText(1777);
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x000F69B0 File Offset: 0x000F4BB0
	public void Window_ArcadeProduction(taskArcadeProduction task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = string.Concat(new string[]
		{
			"(",
			task_.mS_.GetMoney((long)task_.gS_.vorbestellungen, false),
			") ",
			this.Round(task_.GetProzent(), 1).ToString(),
			"%"
		});
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x000F6AB0 File Offset: 0x000F4CB0
	public void Window_Update(string name_, float prozent, bool automatic)
	{
		this.uiObjects[0].GetComponent<Text>().text = name_;
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
		if (this.uiObjects[4].activeSelf != automatic)
		{
			this.uiObjects[4].SetActive(automatic);
		}
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x000F6B50 File Offset: 0x000F4D50
	public void Window_F2PUpdate(taskF2PUpdate task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		if (this.uiObjects[4].activeSelf != task_.automatic)
		{
			this.uiObjects[4].SetActive(task_.automatic);
		}
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x000F6C34 File Offset: 0x000F4E34
	public void Window_Wait(taskWait task_)
	{
		if (!task_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		if (task_.art == 0)
		{
			this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(1699);
			this.uiObjects[1].GetComponent<Image>().sprite = task_.GetPic();
		}
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x000F6C9C File Offset: 0x000F4E9C
	public void Window_ContractWorkWait(taskContractWait task_)
	{
		task_;
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x000F6CA8 File Offset: 0x000F4EA8
	public void Window_ContractWork(taskContractWork task_, roomScript rS_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (rS_.typ != 14)
		{
			this.uiObjects[0].GetComponent<Text>().text = task_.GetName();
			float prozent = task_.GetProzent();
			this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
			this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
		}
		if (rS_.typ == 14)
		{
			this.uiObjects[0].GetComponent<Text>().text = task_.GetName();
			float prozent2 = task_.GetProzent();
			this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent2 * 0.01f, 0.2f);
			this.uiObjects[2].GetComponent<Text>().text = task_.mS_.GetMoney((long)Mathf.RoundToInt(task_.pointsLeft), false);
		}
		this.uiObjects[5].GetComponent<Text>().text = task_.contract_.GetWochen().ToString();
		if (this.uiObjects[6].activeSelf != task_.automatic)
		{
			this.uiObjects[6].SetActive(task_.automatic);
		}
		if (task_.automatic)
		{
			int typ = rS_.typ;
			switch (typ)
			{
			case 1:
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzDEV.ToString();
				return;
			case 2:
			case 6:
			case 7:
			case 9:
				break;
			case 3:
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzQA.ToString();
				return;
			case 4:
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzGFX.ToString();
				return;
			case 5:
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzSFX.ToString();
				return;
			case 8:
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzHardware.ToString();
				break;
			case 10:
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzMotion.ToString();
				return;
			default:
				if (typ == 14)
				{
					this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzProduction.ToString();
					return;
				}
				if (typ != 17)
				{
					return;
				}
				this.uiObjects[7].GetComponent<Text>().text = this.contractWorkMain_.anzWerkstatt.ToString();
				return;
			}
		}
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x000F6F98 File Offset: 0x000F5198
	public void Window_Training(string name_, float prozent, Sprite icon, bool automatic)
	{
		this.uiObjects[0].GetComponent<Text>().text = name_;
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = icon;
		if (this.uiObjects[4].activeSelf != automatic)
		{
			this.uiObjects[4].SetActive(automatic);
		}
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x000F704C File Offset: 0x000F524C
	public void Window_Anrufe(taskSupport task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(746);
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x000F710C File Offset: 0x000F530C
	public void Window_Fanshop(taskFanshop task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		string text = this.tS_.GetText(200);
		text = text.Replace("<NUM>", task_.mS_.GetMoney((long)task_.verdienst, true));
		this.uiObjects[2].GetComponent<Text>().text = text;
		float num = (float)(task_.mS_.week - 1);
		this.uiObjects[1].GetComponent<Image>().fillAmount = num * 0.25f + task_.mS_.dayTimer * 0.25f;
		for (int i = 0; i < task_.bestellungen.Length; i++)
		{
			if (task_.bestellungen[i] <= 0)
			{
				if (this.uiObjects[4 + i].activeSelf)
				{
					this.uiObjects[4 + i].SetActive(false);
				}
			}
			else
			{
				if (!this.uiObjects[4 + i].activeSelf)
				{
					this.uiObjects[4 + i].SetActive(true);
				}
				this.uiObjects[12 + i].GetComponent<Text>().text = task_.mS_.GetMoney((long)task_.bestellungen[i], false);
			}
		}
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x000F724C File Offset: 0x000F544C
	public void Window_Bugfixing(taskBugfixing task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = task_.mS_.GetMoney((long)Mathf.RoundToInt(task_.gS_.points_bugs), false) + " " + this.tS_.GetText(424);
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x000F732C File Offset: 0x000F552C
	public void Window_Polishing(taskPolishing task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
	}

	// Token: 0x060018E5 RID: 6373 RVA: 0x000F73F4 File Offset: 0x000F55F4
	public void Window_Lagerhaus(roomScript rS_)
	{
		if (!rS_)
		{
			return;
		}
		if (!rS_.mS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		int lagerplatz = rS_.GetLagerplatz();
		this.uiObjects[0].GetComponent<Text>().text = rS_.mS_.GetMoney((long)rS_.lagerplatzUsed, false) + " / " + rS_.mS_.GetMoney((long)lagerplatz, false);
		if (rS_.lagerplatzUsed >= lagerplatz)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[8];
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().color = Color.white;
		}
		float num = (float)lagerplatz;
		if (lagerplatz > 0)
		{
			num *= 0.01f;
			num = (float)rS_.lagerplatzUsed / num;
		}
		else
		{
			num = 0f;
		}
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, num * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(num).ToString() + "%";
	}

	// Token: 0x060018E6 RID: 6374 RVA: 0x000F7530 File Offset: 0x000F5730
	public void Window_Serverraum(roomScript rS_)
	{
		if (!rS_)
		{
			return;
		}
		if (!rS_.mS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		int serverplatz = rS_.GetServerplatz();
		this.uiObjects[0].GetComponent<Text>().text = rS_.mS_.GetMoney((long)rS_.serverplatzUsed, false) + " / " + rS_.mS_.GetMoney((long)serverplatz, false);
		if (rS_.serverplatzUsed >= serverplatz || rS_.serverDown)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[8];
		}
		else
		{
			this.uiObjects[0].GetComponent<Text>().color = Color.white;
		}
		if (rS_.serverDown)
		{
			if (this.tS_)
			{
				this.uiObjects[1].GetComponent<Image>().fillAmount = 0f;
				this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(1242);
				if (!this.uiObjects[3].activeSelf)
				{
					this.uiObjects[3].SetActive(true);
				}
				if (this.uiObjects[5].activeSelf)
				{
					this.uiObjects[5].SetActive(false);
				}
			}
			return;
		}
		if (this.uiObjects[3].activeSelf)
		{
			this.uiObjects[3].SetActive(false);
		}
		float num = (float)serverplatz;
		if (serverplatz > 0)
		{
			num *= 0.01f;
			num = (float)rS_.serverplatzUsed / num;
		}
		else
		{
			num = 0f;
		}
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, num * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = Mathf.RoundToInt(num).ToString() + "%";
		bool serverOverheat = rS_.serverOverheat;
		if (this.uiObjects[5].activeSelf != serverOverheat)
		{
			this.uiObjects[5].SetActive(serverOverheat);
			if (this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].transform.GetChild(0).GetComponent<Text>().text = this.tS_.GetText(1260);
			}
		}
	}

	// Token: 0x060018E7 RID: 6375 RVA: 0x000F7780 File Offset: 0x000F5980
	public void Window_Production(taskProduction task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		if (task_.WaitForAutomatic())
		{
			this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(476);
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().text = task_.mS_.GetMoney((long)task_.GetAmount(), false);
		}
		if (this.uiObjects[4].activeSelf != task_.automatic)
		{
			this.uiObjects[4].SetActive(task_.automatic);
		}
		bool flag = true;
		if (this.games_.GetFreeLagerplatz() > 0)
		{
			flag = false;
		}
		if (this.uiObjects[5].activeSelf != flag)
		{
			this.uiObjects[5].SetActive(flag);
			if (this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].transform.GetChild(0).GetComponent<Text>().text = this.tS_.GetText(1147);
			}
		}
	}

	// Token: 0x060018E8 RID: 6376 RVA: 0x000F78FC File Offset: 0x000F5AFC
	public void Window_Spielbericht(taskSpielbericht task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		if (this.uiObjects[6].activeSelf != task_.automatic)
		{
			this.uiObjects[6].SetActive(task_.automatic);
		}
		if (task_.automatic)
		{
			this.uiObjects[7].GetComponent<Text>().text = this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().GetNumSpielberichteCanCreate().ToString();
		}
	}

	// Token: 0x060018E9 RID: 6377 RVA: 0x000F7A2C File Offset: 0x000F5C2C
	public void Window_GameplayVerbessern(taskGameplayVerbessern task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.games_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.GetPic();
		int num = 0;
		for (int i = 0; i < task_.adds.Length; i++)
		{
			this.uiObjects[4 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			if (task_.adds[i] && task_.aktuellerAdd != i)
			{
				this.uiObjects[4 + num].GetComponent<Image>().sprite = this.games_.gameAdds[i];
				num++;
			}
		}
	}

	// Token: 0x060018EA RID: 6378 RVA: 0x000F7B8C File Offset: 0x000F5D8C
	public void Window_GrafikVerbessern(taskGrafikVerbessern task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.games_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.GetPic();
		int num = 0;
		for (int i = 0; i < task_.adds.Length; i++)
		{
			this.uiObjects[4 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			if (task_.adds[i] && task_.aktuellerAdd != i)
			{
				this.uiObjects[4 + num].GetComponent<Image>().sprite = this.games_.gameAdds[i + 6];
				num++;
			}
		}
	}

	// Token: 0x060018EB RID: 6379 RVA: 0x000F7CEC File Offset: 0x000F5EEC
	public void Window_SoundVerbessern(taskSoundVerbessern task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.games_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.GetPic();
		int num = 0;
		for (int i = 0; i < task_.adds.Length; i++)
		{
			this.uiObjects[4 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			if (task_.adds[i] && task_.aktuellerAdd != i)
			{
				this.uiObjects[4 + num].GetComponent<Image>().sprite = this.games_.gameAdds[i + 12];
				num++;
			}
		}
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x000F7E4C File Offset: 0x000F604C
	public void Window_AnimationVerbessern(taskAnimationVerbessern task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.games_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.GetPic();
		int num = 0;
		for (int i = 0; i < task_.adds.Length; i++)
		{
			this.uiObjects[4 + i].GetComponent<Image>().sprite = this.guiMain_.uiSprites[19];
			if (task_.adds[i] && task_.aktuellerAdd != i)
			{
				this.uiObjects[4 + num].GetComponent<Image>().sprite = this.games_.gameAdds[i + 18];
				num++;
			}
		}
	}

	// Token: 0x060018ED RID: 6381 RVA: 0x000F7FAC File Offset: 0x000F61AC
	public void Window_Fankampagne(taskFankampagne task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(task_.kampagne + 740);
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.GetPic();
		if (this.uiObjects[4].activeSelf != task_.automatic)
		{
			this.uiObjects[4].SetActive(task_.automatic);
		}
	}

	// Token: 0x060018EE RID: 6382 RVA: 0x000F80B4 File Offset: 0x000F62B4
	public void Window_Mitarbeitersuche(taskMitarbeitersuche task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.tS_.GetText(137 + task_.beruf);
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[5].GetComponent<Image>().sprite = this.guiMain_.Get3Stars(task_.berufserfahrung);
		if (this.uiObjects[4].activeSelf != task_.automatic)
		{
			this.uiObjects[4].SetActive(task_.automatic);
		}
	}

	// Token: 0x060018EF RID: 6383 RVA: 0x000F81C8 File Offset: 0x000F63C8
	public void Window_Marktforschung(taskMarktforschung task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
	}

	// Token: 0x060018F0 RID: 6384 RVA: 0x000F8258 File Offset: 0x000F6458
	public void Window_MarketingSpezial(taskMarketingSpezial task_)
	{
		if (!task_)
		{
			return;
		}
		if (!task_.mS_)
		{
			return;
		}
		if (!task_.gS_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = task_.gS_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, task_.GetProzent() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(task_.GetProzent(), 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = task_.GetPic();
	}

	// Token: 0x060018F1 RID: 6385 RVA: 0x000F8338 File Offset: 0x000F6538
	public void Window_Marketing(string name_, float prozent, Sprite icon, taskMarketing taskMarketing_)
	{
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = name_;
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = icon;
		if (taskMarketing_.typ == 0)
		{
			if (taskMarketing_.gS_)
			{
				this.uiObjects[6].GetComponent<Image>().sprite = taskMarketing_.gS_.GetTypSprite();
			}
		}
		else if (taskMarketing_.pS_)
		{
			this.uiObjects[6].GetComponent<Image>().sprite = taskMarketing_.pS_.GetTypSprite();
		}
		if (this.uiObjects[4].activeSelf != taskMarketing_.automatic)
		{
			this.uiObjects[4].SetActive(taskMarketing_.automatic);
		}
		bool flag = taskMarketing_.WaitForMinimumHype();
		if (this.uiObjects[5].activeSelf != flag)
		{
			this.uiObjects[5].SetActive(flag);
			if (this.uiObjects[5].activeSelf)
			{
				string text = this.tS_.GetText(726);
				text = text.Replace("<NUM>", "<color=blue>" + (this.scriptMarketing_.maxHype[taskMarketing_.kampagne] - this.scriptMarketing_.hypeProKampagne[taskMarketing_.kampagne]).ToString() + "</color>");
				this.uiObjects[5].transform.GetChild(0).GetComponent<Text>().text = text;
			}
		}
	}

	// Token: 0x060018F2 RID: 6386 RVA: 0x000F8518 File Offset: 0x000F6718
	public void Window_Forschung(string name_, float prozent, Sprite icon, taskForschung task_)
	{
		if (!this.guiMain_)
		{
			return;
		}
		if (!task_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = name_;
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = icon;
		if (this.uiObjects[6].activeSelf != task_.automatic)
		{
			this.uiObjects[6].SetActive(task_.automatic);
		}
		if (task_.automatic)
		{
			this.window_ForschungTimer += Time.deltaTime;
			if (this.window_ForschungTimer < 1f)
			{
				return;
			}
			this.window_ForschungTimer = 0f;
			this.uiObjects[7].GetComponent<Text>().text = this.guiMain_.uiObjects[21].GetComponent<Menu_Forschung>().GetAmountForschung(task_.typ, false).ToString();
		}
	}

	// Token: 0x060018F3 RID: 6387 RVA: 0x000F866C File Offset: 0x000F686C
	public void Window_DevEngine(string name_, float prozent, Sprite icon)
	{
		this.uiObjects[0].GetComponent<Text>().text = name_;
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = icon;
	}

	// Token: 0x060018F4 RID: 6388 RVA: 0x000F8700 File Offset: 0x000F6900
	public void Window_DevGame(gameScript gS_, taskGame task_)
	{
		if (!this.main_)
		{
			return;
		}
		if (!this.tS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = gS_.GetNameWithTag();
		this.uiObjects[11].GetComponent<Image>().sprite = gS_.GetTypSprite();
		this.uiObjects[14].GetComponent<Image>().sprite = gS_.GetPlatformTypSprite();
		this.uiObjects[10].GetComponent<Text>().text = Mathf.RoundToInt(gS_.GetHype()).ToString();
		this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, gS_.GetProzentGesamt() * 0.01f, 0.2f);
		this.uiObjects[2].GetComponent<Text>().text = this.Round(gS_.GetProzentGesamt(), 1).ToString() + "%";
		this.uiObjects[9].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[9].GetComponent<Image>().fillAmount, gS_.GetProzentFeature() * 0.01f, 0.2f);
		this.uiObjects[3].GetComponent<Text>().text = Mathf.RoundToInt(gS_.points_gameplay).ToString();
		this.uiObjects[4].GetComponent<Text>().text = Mathf.RoundToInt(gS_.points_grafik).ToString();
		this.uiObjects[5].GetComponent<Text>().text = Mathf.RoundToInt(gS_.points_sound).ToString();
		this.uiObjects[6].GetComponent<Text>().text = Mathf.RoundToInt(gS_.points_technik).ToString();
		this.uiObjects[7].GetComponent<Text>().text = Mathf.RoundToInt(gS_.points_bugs).ToString();
		if (gS_.devPoints_Gesamt > 0f)
		{
			if (gS_.devAktFeature != -5)
			{
				if (gS_.devAktFeature < 0)
				{
					this.uiObjects[8].GetComponent<Text>().text = this.eF_.GetName(gS_.gameEngineFeature[gS_.devAktFeature + 4]);
				}
				else
				{
					this.uiObjects[8].GetComponent<Text>().text = this.gF_.GetName(gS_.devAktFeature);
				}
			}
			if (gS_.devAktFeature == -5)
			{
				this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(976);
			}
		}
		else
		{
			this.uiObjects[8].GetComponent<Text>().text = this.tS_.GetText(1046);
			this.uiObjects[9].GetComponent<Image>().fillAmount = 1f;
		}
		if (gS_.typ_contractGame)
		{
			if (!this.uiObjects[12].activeSelf)
			{
				this.uiObjects[12].SetActive(true);
			}
			this.uiObjects[13].GetComponent<Text>().text = gS_.GetContractGameAbgabetermin().ToString();
		}
		else if (this.uiObjects[12].activeSelf)
		{
			this.uiObjects[12].SetActive(false);
		}
		if (task_.leitenderDesignerID != -1)
		{
			if (this.uiObjects[15].activeSelf)
			{
				this.uiObjects[15].SetActive(false);
			}
			return;
		}
		if (!this.uiObjects[15].activeSelf)
		{
			this.uiObjects[15].SetActive(true);
			return;
		}
		this.uiObjects[15].transform.GetChild(0).GetComponent<Text>().text = this.tS_.GetText(1000);
	}

	// Token: 0x060018F5 RID: 6389 RVA: 0x000F8AB8 File Offset: 0x000F6CB8
	public void Window_Unterstuetzen(string roomName, float prozent)
	{
		if (!this.tS_)
		{
			return;
		}
		if (prozent >= 0f)
		{
			this.uiObjects[1].GetComponent<Image>().fillAmount = Mathf.Lerp(this.uiObjects[1].GetComponent<Image>().fillAmount, prozent * 0.01f, 0.2f);
			this.uiObjects[2].GetComponent<Text>().text = this.Round(prozent, 1).ToString() + "%";
			return;
		}
		this.uiObjects[1].GetComponent<Image>().fillAmount = 0f;
		this.uiObjects[2].GetComponent<Text>().text = this.tS_.GetText(476);
	}

	// Token: 0x060018F6 RID: 6390 RVA: 0x000F8B78 File Offset: 0x000F6D78
	private float Round(float value, int digits)
	{
		float num = Mathf.Pow(10f, (float)digits);
		return Mathf.Round(value * num) / num;
	}

	// Token: 0x04001C4D RID: 7245
	public GameObject[] uiObjects;

	// Token: 0x04001C4E RID: 7246
	public GameObject main_;

	// Token: 0x04001C4F RID: 7247
	public textScript tS_;

	// Token: 0x04001C50 RID: 7248
	public engineFeatures eF_;

	// Token: 0x04001C51 RID: 7249
	public gameplayFeatures gF_;

	// Token: 0x04001C52 RID: 7250
	public games games_;

	// Token: 0x04001C53 RID: 7251
	public GUI_Main guiMain_;

	// Token: 0x04001C54 RID: 7252
	private Menu_Marketing_GameKampagne scriptMarketing_;

	// Token: 0x04001C55 RID: 7253
	private contractWorkMain contractWorkMain_;

	// Token: 0x04001C56 RID: 7254
	private float window_ForschungTimer = 2f;
}
