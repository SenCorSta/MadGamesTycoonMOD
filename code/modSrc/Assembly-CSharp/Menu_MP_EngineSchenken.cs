using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C3 RID: 451
public class Menu_MP_EngineSchenken : MonoBehaviour
{
	// Token: 0x060010EB RID: 4331 RVA: 0x0000BED9 File Offset: 0x0000A0D9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x000C03FC File Offset: 0x000BE5FC
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0000BEE1 File Offset: 0x0000A0E1
	private void OnEnable()
	{
		this.selectedEngine = null;
		this.selectedPlayer = -1;
		this.InitDropdowns();
		this.Init();
		this.InitPlayerButtons();
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x000C0504 File Offset: 0x000BE704
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(4));
		list.Add(this.tS_.GetText(245));
		list.Add(this.tS_.GetText(160));
		list.Add(this.tS_.GetText(261));
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(260));
		list.Add(this.tS_.GetText(268));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x000C0614 File Offset: 0x000BE814
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		if (!this.selectedEngine)
		{
			this.uiObjects[5].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[5].GetComponent<Button>().interactable = true;
		}
		this.UpdatePlayerButtons();
		this.MultiplayerUpdate();
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x000C0694 File Offset: 0x000BE894
	public void UpdatePlayerButtons()
	{
		for (int i = 0; i < 4; i++)
		{
			if (this.uiPlayerButtons[i].activeSelf)
			{
				if (this.selectedPlayer == i)
				{
					this.uiPlayerButtons[i].GetComponent<Image>().color = this.guiMain_.colors[20];
				}
				else
				{
					this.uiPlayerButtons[i].GetComponent<Image>().color = Color.white;
				}
			}
		}
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x000C0704 File Offset: 0x000BE904
	public void InitPlayerButtons()
	{
		for (int i = 0; i < 4; i++)
		{
			if (this.uiPlayerButtons[i].activeSelf)
			{
				this.uiPlayerButtons[i].SetActive(false);
			}
		}
		for (int j = 0; j < this.mpCalls_.playersMP.Count; j++)
		{
			int playerID = this.mpCalls_.playersMP[j].playerID;
			if (playerID == this.mpCalls_.myID)
			{
				if (this.uiPlayerButtons[j].activeSelf)
				{
					this.uiPlayerButtons[j].SetActive(false);
				}
			}
			else
			{
				if (!this.uiPlayerButtons[j].activeSelf)
				{
					this.uiPlayerButtons[j].SetActive(true);
				}
				if (this.selectedPlayer == -1)
				{
					this.selectedPlayer = j;
				}
				this.uiPlayerButtons[j].transform.GetChild(1).GetComponent<Image>().sprite = this.guiMain_.GetCompanyLogo(this.mpCalls_.GetLogo(playerID));
				this.uiPlayerButtons[j].transform.GetChild(2).GetComponent<Text>().text = this.mpCalls_.GetCompanyName(playerID);
			}
		}
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0000BF03 File Offset: 0x0000A103
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x000C0830 File Offset: 0x000BEA30
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x000C087C File Offset: 0x000BEA7C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_EngineSchenken>().eS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000C08D8 File Offset: 0x000BEAD8
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000C0928 File Offset: 0x000BEB28
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.myID != 0 && component.playerEngine && component.Complete() && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_EngineSchenken component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_EngineSchenken>();
					component2.eS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.eF_ = this.eF_;
					component2.genres_ = this.genres_;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x000C0A60 File Offset: 0x000BEC60
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_EngineSchenken component = gameObject.GetComponent<Item_EngineSchenken>();
				switch (value)
				{
				case 0:
					gameObject.name = component.eS_.GetName();
					break;
				case 1:
					gameObject.name = component.eS_.GetTechLevel().ToString();
					break;
				case 2:
					gameObject.name = component.eS_.spezialgenre.ToString();
					break;
				case 3:
					gameObject.name = component.eS_.GetFeaturesAmount().ToString();
					break;
				case 4:
					gameObject.name = component.eS_.GetGamesAmount().ToString();
					break;
				case 5:
					gameObject.name = component.eS_.preis.ToString();
					break;
				case 6:
					gameObject.name = component.eS_.gewinnbeteiligung.ToString();
					break;
				case 7:
					gameObject.name = component.eS_.GetVerkaufteLizenzen().ToString();
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0000BF1A File Offset: 0x0000A11A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x000C0C14 File Offset: 0x000BEE14
	public void BUTTON_Ok()
	{
		if (!this.selectedEngine)
		{
			return;
		}
		if (this.selectedPlayer == -1)
		{
			return;
		}
		this.sfx_.PlaySound(3, true);
		if (this.mpCalls_.isServer)
		{
			this.mpCalls_.SERVER_Send_Help(this.mpCalls_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 1, this.selectedEngine.myID, 0, 0);
		}
		else
		{
			this.mpCalls_.CLIENT_Send_Help(this.mpCalls_.playersMP[this.selectedPlayer].playerID, 1, this.selectedEngine.myID, 0, 0);
		}
		string text = this.tS_.GetText(1329);
		text = text.Replace("<NAME1>", this.mpCalls_.GetCompanyName(this.mpCalls_.playersMP[this.selectedPlayer].playerID));
		text = text.Replace("<NAME2>", this.selectedEngine.GetName());
		this.guiMain_.MessageBox(text, false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001585 RID: 5509
	private mainScript mS_;

	// Token: 0x04001586 RID: 5510
	private GameObject main_;

	// Token: 0x04001587 RID: 5511
	private GUI_Main guiMain_;

	// Token: 0x04001588 RID: 5512
	private sfxScript sfx_;

	// Token: 0x04001589 RID: 5513
	private textScript tS_;

	// Token: 0x0400158A RID: 5514
	private engineFeatures eF_;

	// Token: 0x0400158B RID: 5515
	private genres genres_;

	// Token: 0x0400158C RID: 5516
	private mpCalls mpCalls_;

	// Token: 0x0400158D RID: 5517
	public GameObject[] uiPrefabs;

	// Token: 0x0400158E RID: 5518
	public GameObject[] uiObjects;

	// Token: 0x0400158F RID: 5519
	public GameObject[] uiPlayerButtons;

	// Token: 0x04001590 RID: 5520
	public int selectedPlayer = -1;

	// Token: 0x04001591 RID: 5521
	public engineScript selectedEngine;

	// Token: 0x04001592 RID: 5522
	private float updateTimer;
}
