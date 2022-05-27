using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025C RID: 604
public class Menu_Stats_TochterfirmaSettings : MonoBehaviour
{
	// Token: 0x06001786 RID: 6022 RVA: 0x000EBC67 File Offset: 0x000E9E67
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000EBC70 File Offset: 0x000E9E70
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

	// Token: 0x06001788 RID: 6024 RVA: 0x000EBD58 File Offset: 0x000E9F58
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

	// Token: 0x06001789 RID: 6025 RVA: 0x000EBF80 File Offset: 0x000EA180
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

	// Token: 0x0600178A RID: 6026 RVA: 0x000EC30E File Offset: 0x000EA50E
	public void Init(publisherScript pubS_)
	{
		this.pS_ = pubS_;
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000EC32C File Offset: 0x000EA52C
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

	// Token: 0x0600178C RID: 6028 RVA: 0x000EC5E0 File Offset: 0x000EA7E0
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

	// Token: 0x0600178D RID: 6029 RVA: 0x000EC922 File Offset: 0x000EAB22
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000EC940 File Offset: 0x000EAB40
	public void BUTTON_Topic()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[399]);
		this.guiMain_.uiObjects[399].GetComponent<Menu_Stats_TochterfirmaTopic>().Init(this.pS_);
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000EC998 File Offset: 0x000EAB98
	public void BUTTON_IP(int slot)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[400]);
		this.guiMain_.uiObjects[400].GetComponent<Menu_Stats_TochterfirmaIP>().Init(this.pS_, slot);
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000EC9F0 File Offset: 0x000EABF0
	public void BUTTON_Engine()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[401]);
		this.guiMain_.uiObjects[401].GetComponent<Menu_Stats_TochterfirmaEngine>().Init(this.pS_);
	}

	// Token: 0x06001791 RID: 6033 RVA: 0x000ECA48 File Offset: 0x000EAC48
	public void BUTTON_Platform(int slot)
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[402]);
		this.guiMain_.uiObjects[402].GetComponent<Menu_Stats_TochterfirmaPlatform>().Init(this.pS_, slot);
	}

	// Token: 0x06001792 RID: 6034 RVA: 0x000ECAA0 File Offset: 0x000EACA0
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

	// Token: 0x06001793 RID: 6035 RVA: 0x000ECB14 File Offset: 0x000EAD14
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

	// Token: 0x06001794 RID: 6036 RVA: 0x000ECB7C File Offset: 0x000EAD7C
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

	// Token: 0x04001B49 RID: 6985
	private mainScript mS_;

	// Token: 0x04001B4A RID: 6986
	private GameObject main_;

	// Token: 0x04001B4B RID: 6987
	private GUI_Main guiMain_;

	// Token: 0x04001B4C RID: 6988
	private sfxScript sfx_;

	// Token: 0x04001B4D RID: 6989
	private textScript tS_;

	// Token: 0x04001B4E RID: 6990
	private genres genres_;

	// Token: 0x04001B4F RID: 6991
	private games games_;

	// Token: 0x04001B50 RID: 6992
	public GameObject[] uiPrefabs;

	// Token: 0x04001B51 RID: 6993
	public GameObject[] uiObjects;

	// Token: 0x04001B52 RID: 6994
	public publisherScript pS_;
}
