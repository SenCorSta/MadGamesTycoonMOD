using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000258 RID: 600
public class Menu_Stats_TochterfirmaSettings : MonoBehaviour
{
	// Token: 0x06001746 RID: 5958 RVA: 0x00010490 File Offset: 0x0000E690
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000F1850 File Offset: 0x000EFA50
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
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000F1938 File Offset: 0x000EFB38
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		if (this.uiObjects[11].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[15].GetComponent<Toggle>().isOn = true;
			this.uiObjects[16].GetComponent<Toggle>().isOn = true;
			this.uiObjects[17].GetComponent<Toggle>().isOn = true;
			this.uiObjects[18].GetComponent<Toggle>().isOn = true;
			this.uiObjects[15].GetComponent<Toggle>().interactable = false;
			this.uiObjects[16].GetComponent<Toggle>().interactable = false;
			this.uiObjects[17].GetComponent<Toggle>().interactable = false;
			this.uiObjects[18].GetComponent<Toggle>().interactable = false;
			this.uiObjects[33].GetComponent<Button>().interactable = false;
			this.uiObjects[34].GetComponent<Button>().interactable = false;
			this.uiObjects[35].GetComponent<Button>().interactable = false;
			this.uiObjects[36].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[15].GetComponent<Toggle>().interactable = true;
			this.uiObjects[16].GetComponent<Toggle>().interactable = true;
			this.uiObjects[17].GetComponent<Toggle>().interactable = true;
			this.uiObjects[18].GetComponent<Toggle>().interactable = true;
			this.uiObjects[33].GetComponent<Button>().interactable = true;
			this.uiObjects[34].GetComponent<Button>().interactable = true;
			this.uiObjects[35].GetComponent<Button>().interactable = true;
			this.uiObjects[36].GetComponent<Button>().interactable = true;
		}
		if (this.uiObjects[10].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[32].GetComponent<Dropdown>().interactable = true;
			return;
		}
		this.uiObjects[32].GetComponent<Dropdown>().interactable = false;
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000F1B60 File Offset: 0x000EFD60
	public void InitDropdowns()
	{
		this.FindScripts();
		List<string> list = new List<string>();
		if (this.pS_.tf_publisher)
		{
			list.Add(this.tS_.GetText(432));
		}
		else
		{
			list.Add("<color=red>" + this.tS_.GetText(432) + "</color>");
		}
		if (this.pS_.tf_developer)
		{
			list.Add(this.tS_.GetText(274));
		}
		else
		{
			list.Add("<color=red>" + this.tS_.GetText(274) + "</color>");
		}
		if (this.pS_.tf_publisher && this.pS_.tf_developer)
		{
			list.Add(this.tS_.GetText(432) + " & " + this.tS_.GetText(274));
		}
		else
		{
			list.Add(string.Concat(new string[]
			{
				"<color=red>",
				this.tS_.GetText(432),
				" & ",
				this.tS_.GetText(274),
				"</color>"
			}));
		}
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		list = new List<string>();
		list.Add(this.tS_.GetText(1963));
		list.Add(this.tS_.GetText(1964));
		list.Add(this.tS_.GetText(1965));
		this.uiObjects[6].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[6].GetComponent<Dropdown>().AddOptions(list);
		list = new List<string>();
		list.Add(this.tS_.GetText(1966));
		for (int i = 0; i < this.games_.gameSizeSprites.Length; i++)
		{
			list.Add(this.tS_.GetText(329 + i));
		}
		this.uiObjects[7].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[7].GetComponent<Dropdown>().AddOptions(list);
		list = new List<string>();
		list.Add(this.tS_.GetText(1966));
		for (int j = 0; j < this.genres_.genres_LEVEL.Length; j++)
		{
			if (this.genres_.genres_UNLOCK[j])
			{
				list.Add(this.genres_.GetName(j));
			}
			else
			{
				list.Add("<color=red>" + this.genres_.GetName(j) + "</color>");
			}
		}
		this.uiObjects[8].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[8].GetComponent<Dropdown>().AddOptions(list);
		list = new List<string>();
		list.Add(this.tS_.GetText(1966));
		list.Add("< 10%");
		list.Add("< 20%");
		list.Add("< 30%");
		list.Add("< 40%");
		list.Add("< 50%");
		list.Add("< 60%");
		list.Add("< 70%");
		list.Add("< 80%");
		list.Add("< 90%");
		this.uiObjects[32].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[32].GetComponent<Dropdown>().AddOptions(list);
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x00010498 File Offset: 0x0000E698
	public void Init(publisherScript pubS_)
	{
		this.pS_ = pubS_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000F1EF0 File Offset: 0x000F00F0
	private void SetData()
	{
		if (this.pS_.publisher && !this.pS_.developer)
		{
			this.uiObjects[5].GetComponent<Dropdown>().value = 0;
		}
		if (!this.pS_.publisher && this.pS_.developer)
		{
			this.uiObjects[5].GetComponent<Dropdown>().value = 1;
		}
		if (this.pS_.publisher && this.pS_.developer)
		{
			this.uiObjects[5].GetComponent<Dropdown>().value = 2;
		}
		this.uiObjects[6].GetComponent<Dropdown>().value = this.pS_.tf_entwicklungsdauer;
		this.uiObjects[7].GetComponent<Dropdown>().value = this.pS_.tf_gameSize;
		this.uiObjects[8].GetComponent<Dropdown>().value = this.pS_.tf_gameGenre;
		this.uiObjects[10].GetComponent<Toggle>().isOn = this.pS_.tf_autoRelease;
		this.uiObjects[11].GetComponent<Toggle>().isOn = this.pS_.tf_onlyPlayerConsole;
		this.uiObjects[12].GetComponent<Toggle>().isOn = this.pS_.tf_allowMMO;
		this.uiObjects[13].GetComponent<Toggle>().isOn = this.pS_.tf_allowF2P;
		this.uiObjects[14].GetComponent<Toggle>().isOn = this.pS_.tf_allowAddon;
		this.uiObjects[15].GetComponent<Toggle>().isOn = this.pS_.tf_noArcade;
		this.uiObjects[16].GetComponent<Toggle>().isOn = this.pS_.tf_noHandy;
		this.uiObjects[17].GetComponent<Toggle>().isOn = this.pS_.tf_noRetro;
		this.uiObjects[18].GetComponent<Toggle>().isOn = this.pS_.tf_noPorts;
		this.uiObjects[19].GetComponent<Toggle>().isOn = this.pS_.tf_noBudget;
		this.uiObjects[20].GetComponent<Toggle>().isOn = this.pS_.tf_noGOTY;
		this.uiObjects[21].GetComponent<Toggle>().isOn = this.pS_.tf_noRemaster;
		this.uiObjects[22].GetComponent<Toggle>().isOn = this.pS_.tf_noSpinoffs;
		this.uiObjects[23].GetComponent<Toggle>().isOn = this.pS_.tf_ownPublisher;
		this.uiObjects[32].GetComponent<Dropdown>().value = this.pS_.tf_autoReleaseVal;
		this.UpdateData();
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000F21A4 File Offset: 0x000F03A4
	public void UpdateData()
	{
		if (this.pS_.tf_gameTopic != -1)
		{
			this.uiObjects[24].GetComponent<Text>().text = "<b>" + this.tS_.GetThemes(this.pS_.tf_gameTopic) + "</b>";
		}
		else
		{
			this.uiObjects[24].GetComponent<Text>().text = this.tS_.GetText(1966);
		}
		for (int i = 0; i < this.pS_.tf_ipFocus.Length; i++)
		{
			if (this.pS_.tf_ipFocus[i] != -1)
			{
				GameObject gameObject = GameObject.Find("GAME_" + this.pS_.tf_ipFocus[i].ToString());
				if (gameObject)
				{
					this.uiObjects[25 + i].GetComponent<Text>().text = "<b>" + gameObject.GetComponent<gameScript>().GetIpName() + "</b>";
				}
				else
				{
					this.pS_.tf_ipFocus[i] = -1;
					this.uiObjects[25 + i].GetComponent<Text>().text = this.tS_.GetText(1966);
				}
			}
			else
			{
				this.uiObjects[25 + i].GetComponent<Text>().text = this.tS_.GetText(1966);
			}
		}
		if (this.pS_.tf_engine != -1 && this.pS_.tf_engine != 0)
		{
			GameObject gameObject2 = GameObject.Find("ENGINE_" + this.pS_.tf_engine.ToString());
			if (gameObject2)
			{
				engineScript component = gameObject2.GetComponent<engineScript>();
				if (component)
				{
					this.uiObjects[31].GetComponent<Text>().text = "<b>" + component.GetName() + "</b>";
				}
			}
		}
		else
		{
			this.uiObjects[31].GetComponent<Text>().text = this.tS_.GetText(1966);
		}
		for (int j = 0; j < this.pS_.tf_platformFocus.Length; j++)
		{
			if (this.pS_.tf_platformFocus[j] != -1)
			{
				GameObject gameObject3 = GameObject.Find("PLATFORM_" + this.pS_.tf_platformFocus[j].ToString());
				if (gameObject3)
				{
					platformScript component2 = gameObject3.GetComponent<platformScript>();
					if (component2)
					{
						if (!component2.vomMarktGenommen)
						{
							this.uiObjects[37 + j].GetComponent<Text>().text = "<b>" + component2.GetName() + "</b>";
						}
						else
						{
							this.uiObjects[37 + j].GetComponent<Text>().text = "<b><color=red>" + component2.GetName() + "</color></b>";
						}
					}
				}
				else
				{
					this.pS_.tf_platformFocus[j] = -1;
					this.uiObjects[37 + j].GetComponent<Text>().text = this.tS_.GetText(1966);
				}
			}
			else
			{
				this.uiObjects[37 + j].GetComponent<Text>().text = this.tS_.GetText(1966);
			}
		}
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000104B3 File Offset: 0x0000E6B3
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000F24E8 File Offset: 0x000F06E8
	public void BUTTON_Topic()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[399]);
		this.guiMain_.uiObjects[399].GetComponent<Menu_Stats_TochterfirmaTopic>().Init(this.pS_);
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000F2540 File Offset: 0x000F0740
	public void BUTTON_IP(int slot)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[400]);
		this.guiMain_.uiObjects[400].GetComponent<Menu_Stats_TochterfirmaIP>().Init(this.pS_, slot);
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000F2598 File Offset: 0x000F0798
	public void BUTTON_Engine()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[401]);
		this.guiMain_.uiObjects[401].GetComponent<Menu_Stats_TochterfirmaEngine>().Init(this.pS_);
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000F25F0 File Offset: 0x000F07F0
	public void BUTTON_Platform(int slot)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[402]);
		this.guiMain_.uiObjects[402].GetComponent<Menu_Stats_TochterfirmaPlatform>().Init(this.pS_, slot);
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000F2648 File Offset: 0x000F0848
	public void BUTTON_SettingsForAll()
	{
		this.sfx_.PlaySound(3, true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.IsMyTochterfirma())
				{
					this.SetSettings(component, true);
					this.guiMain_.MessageBox(this.tS_.GetText(1972), false);
				}
			}
		}
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000F26BC File Offset: 0x000F08BC
	public void BUTTON_Ok()
	{
		this.sfx_.PlaySound(3, true);
		this.SetSettings(this.pS_, false);
		if (this.guiMain_.uiObjects[387].activeSelf)
		{
			this.guiMain_.uiObjects[387].GetComponent<Menu_Stats_Tochterfirma_Main>().UpdateData();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000F2724 File Offset: 0x000F0924
	public void SetSettings(publisherScript script_, bool allTochterfirmen)
	{
		if (script_)
		{
			if (this.uiObjects[5].GetComponent<Dropdown>().value == 0 && script_.tf_publisher)
			{
				script_.publisher = true;
				script_.developer = false;
			}
			if (this.uiObjects[5].GetComponent<Dropdown>().value == 1 && script_.tf_developer)
			{
				script_.publisher = false;
				script_.developer = true;
			}
			if (this.uiObjects[5].GetComponent<Dropdown>().value == 2 && script_.tf_publisher && script_.tf_developer)
			{
				script_.publisher = true;
				script_.developer = true;
			}
			script_.tf_entwicklungsdauer = this.uiObjects[6].GetComponent<Dropdown>().value;
			script_.tf_gameSize = this.uiObjects[7].GetComponent<Dropdown>().value;
			if (!allTochterfirmen)
			{
				if (this.uiObjects[8].GetComponent<Dropdown>().value > 0)
				{
					if (this.genres_.genres_UNLOCK[this.uiObjects[8].GetComponent<Dropdown>().value - 1])
					{
						script_.tf_gameGenre = this.uiObjects[8].GetComponent<Dropdown>().value;
					}
				}
				else
				{
					script_.tf_gameGenre = this.uiObjects[8].GetComponent<Dropdown>().value;
				}
			}
			script_.tf_autoRelease = this.uiObjects[10].GetComponent<Toggle>().isOn;
			script_.tf_onlyPlayerConsole = this.uiObjects[11].GetComponent<Toggle>().isOn;
			script_.tf_allowMMO = this.uiObjects[12].GetComponent<Toggle>().isOn;
			script_.tf_allowF2P = this.uiObjects[13].GetComponent<Toggle>().isOn;
			script_.tf_allowAddon = this.uiObjects[14].GetComponent<Toggle>().isOn;
			script_.tf_noArcade = this.uiObjects[15].GetComponent<Toggle>().isOn;
			script_.tf_noHandy = this.uiObjects[16].GetComponent<Toggle>().isOn;
			script_.tf_noRetro = this.uiObjects[17].GetComponent<Toggle>().isOn;
			script_.tf_noPorts = this.uiObjects[18].GetComponent<Toggle>().isOn;
			script_.tf_noBudget = this.uiObjects[19].GetComponent<Toggle>().isOn;
			script_.tf_noGOTY = this.uiObjects[20].GetComponent<Toggle>().isOn;
			script_.tf_noRemaster = this.uiObjects[21].GetComponent<Toggle>().isOn;
			script_.tf_noSpinoffs = this.uiObjects[22].GetComponent<Toggle>().isOn;
			script_.tf_ownPublisher = this.uiObjects[23].GetComponent<Toggle>().isOn;
			script_.tf_autoReleaseVal = this.uiObjects[32].GetComponent<Dropdown>().value;
			if (allTochterfirmen)
			{
				for (int i = 0; i < this.pS_.tf_platformFocus.Length; i++)
				{
					script_.tf_platformFocus[i] = this.pS_.tf_platformFocus[i];
				}
			}
		}
	}

	// Token: 0x04001B2F RID: 6959
	private mainScript mS_;

	// Token: 0x04001B30 RID: 6960
	private GameObject main_;

	// Token: 0x04001B31 RID: 6961
	private GUI_Main guiMain_;

	// Token: 0x04001B32 RID: 6962
	private sfxScript sfx_;

	// Token: 0x04001B33 RID: 6963
	private textScript tS_;

	// Token: 0x04001B34 RID: 6964
	private genres genres_;

	// Token: 0x04001B35 RID: 6965
	private games games_;

	// Token: 0x04001B36 RID: 6966
	public GameObject[] uiPrefabs;

	// Token: 0x04001B37 RID: 6967
	public GameObject[] uiObjects;

	// Token: 0x04001B38 RID: 6968
	public publisherScript pS_;
}
