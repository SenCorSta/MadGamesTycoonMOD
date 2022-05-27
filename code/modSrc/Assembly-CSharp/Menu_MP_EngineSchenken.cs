using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C4 RID: 452
public class Menu_MP_EngineSchenken : MonoBehaviour
{
	// Token: 0x06001105 RID: 4357 RVA: 0x000B4A2B File Offset: 0x000B2C2B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x000B4A34 File Offset: 0x000B2C34
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

	// Token: 0x06001107 RID: 4359 RVA: 0x000B4B3C File Offset: 0x000B2D3C
	private void OnEnable()
	{
		this.selectedEngine = null;
		this.selectedPlayer = -1;
		this.InitDropdowns();
		this.Init();
		this.InitPlayerButtons();
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x000B4B60 File Offset: 0x000B2D60
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

	// Token: 0x06001109 RID: 4361 RVA: 0x000B4C70 File Offset: 0x000B2E70
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

	// Token: 0x0600110A RID: 4362 RVA: 0x000B4CF0 File Offset: 0x000B2EF0
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

	// Token: 0x0600110B RID: 4363 RVA: 0x000B4D60 File Offset: 0x000B2F60
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
			if (playerID == this.mS_.myID)
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

	// Token: 0x0600110C RID: 4364 RVA: 0x000B4E8B File Offset: 0x000B308B
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x000B4EA4 File Offset: 0x000B30A4
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

	// Token: 0x0600110E RID: 4366 RVA: 0x000B4EF0 File Offset: 0x000B30F0
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

	// Token: 0x0600110F RID: 4367 RVA: 0x000B4F4C File Offset: 0x000B314C
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000B4F9C File Offset: 0x000B319C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Engine");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				engineScript component = array[i].GetComponent<engineScript>();
				if (component && component.myID != 0 && component.ownerID == this.mS_.myID && component.Complete() && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06001111 RID: 4369 RVA: 0x000B50DC File Offset: 0x000B32DC
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

	// Token: 0x06001112 RID: 4370 RVA: 0x000B528F File Offset: 0x000B348F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000B52AC File Offset: 0x000B34AC
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
			this.mpCalls_.SERVER_Send_Help(this.mS_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 1, this.selectedEngine.myID, 0, 0);
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

	// Token: 0x0400158E RID: 5518
	private mainScript mS_;

	// Token: 0x0400158F RID: 5519
	private GameObject main_;

	// Token: 0x04001590 RID: 5520
	private GUI_Main guiMain_;

	// Token: 0x04001591 RID: 5521
	private sfxScript sfx_;

	// Token: 0x04001592 RID: 5522
	private textScript tS_;

	// Token: 0x04001593 RID: 5523
	private engineFeatures eF_;

	// Token: 0x04001594 RID: 5524
	private genres genres_;

	// Token: 0x04001595 RID: 5525
	private mpCalls mpCalls_;

	// Token: 0x04001596 RID: 5526
	public GameObject[] uiPrefabs;

	// Token: 0x04001597 RID: 5527
	public GameObject[] uiObjects;

	// Token: 0x04001598 RID: 5528
	public GameObject[] uiPlayerButtons;

	// Token: 0x04001599 RID: 5529
	public int selectedPlayer = -1;

	// Token: 0x0400159A RID: 5530
	public engineScript selectedEngine;

	// Token: 0x0400159B RID: 5531
	private float updateTimer;
}
