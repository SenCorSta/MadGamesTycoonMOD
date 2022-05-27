using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C9 RID: 457
public class Menu_MP_LizenzSchenken : MonoBehaviour
{
	// Token: 0x06001135 RID: 4405 RVA: 0x0000C107 File Offset: 0x0000A307
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000C2E74 File Offset: 0x000C1074
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
		if (!this.licences_)
		{
			this.licences_ = this.main_.GetComponent<licences>();
		}
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0000C10F File Offset: 0x0000A30F
	private void OnEnable()
	{
		this.selectedLizenz = -1;
		this.selectedPlayer = -1;
		this.InitDropdowns();
		this.Init();
		this.InitPlayerButtons();
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x000C2F9C File Offset: 0x000C119C
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(302));
		list.Add(this.tS_.GetText(304));
		list.Add(this.tS_.GetText(305));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000C306C File Offset: 0x000C126C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		if (this.selectedLizenz <= -1)
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

	// Token: 0x0600113A RID: 4410 RVA: 0x000C30E8 File Offset: 0x000C12E8
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

	// Token: 0x0600113B RID: 4411 RVA: 0x000C3158 File Offset: 0x000C1358
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

	// Token: 0x0600113C RID: 4412 RVA: 0x0000C131 File Offset: 0x0000A331
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x000C3284 File Offset: 0x000C1484
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

	// Token: 0x0600113E RID: 4414 RVA: 0x000C32D0 File Offset: 0x000C14D0
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_LizenzVerschenken>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x000C3328 File Offset: 0x000C1528
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x000C3378 File Offset: 0x000C1578
	private void SetData()
	{
		for (int i = 0; i < this.licences_.licence_GEKAUFT.Length; i++)
		{
			if (this.licences_.licence_GEKAUFT[i] > 0 && !this.Exists(this.uiObjects[0], i))
			{
				Item_LizenzVerschenken component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_LizenzVerschenken>();
				component.myID = i;
				component.licences_ = this.licences_;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.menu_ = this;
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000C346C File Offset: 0x000C166C
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
				Item_LizenzVerschenken component = gameObject.GetComponent<Item_LizenzVerschenken>();
				switch (value)
				{
				case 0:
					gameObject.name = this.licences_.GetName(component.myID);
					break;
				case 1:
					gameObject.name = this.licences_.GetSellPrice(component.myID).ToString();
					break;
				case 2:
					gameObject.name = this.licences_.licence_QUALITY[component.myID].ToString();
					break;
				case 3:
					gameObject.name = this.licences_.licence_TYP[component.myID].ToString();
					break;
				case 4:
					gameObject.name = this.licences_.licence_GEKAUFT[component.myID].ToString();
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

	// Token: 0x06001142 RID: 4418 RVA: 0x0000C148 File Offset: 0x0000A348
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x000C35E4 File Offset: 0x000C17E4
	public void BUTTON_Ok()
	{
		if (this.selectedLizenz <= -1)
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
			this.mpCalls_.SERVER_Send_Help(this.mpCalls_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 2, this.selectedLizenz, this.licences_.licence_GEKAUFT[this.selectedLizenz], 0);
		}
		else
		{
			this.mpCalls_.CLIENT_Send_Help(this.mpCalls_.playersMP[this.selectedPlayer].playerID, 2, this.selectedLizenz, this.licences_.licence_GEKAUFT[this.selectedLizenz], 0);
		}
		this.licences_.licence_GEKAUFT[this.selectedLizenz] = 0;
		string text = this.tS_.GetText(1331);
		text = text.Replace("<NAME1>", this.mpCalls_.GetCompanyName(this.mpCalls_.playersMP[this.selectedPlayer].playerID));
		text = text.Replace("<NAME2>", this.licences_.GetName(this.selectedLizenz));
		this.guiMain_.MessageBox(text, false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040015C3 RID: 5571
	private mainScript mS_;

	// Token: 0x040015C4 RID: 5572
	private GameObject main_;

	// Token: 0x040015C5 RID: 5573
	private GUI_Main guiMain_;

	// Token: 0x040015C6 RID: 5574
	private sfxScript sfx_;

	// Token: 0x040015C7 RID: 5575
	private textScript tS_;

	// Token: 0x040015C8 RID: 5576
	private engineFeatures eF_;

	// Token: 0x040015C9 RID: 5577
	private genres genres_;

	// Token: 0x040015CA RID: 5578
	private mpCalls mpCalls_;

	// Token: 0x040015CB RID: 5579
	private licences licences_;

	// Token: 0x040015CC RID: 5580
	public GameObject[] uiPrefabs;

	// Token: 0x040015CD RID: 5581
	public GameObject[] uiObjects;

	// Token: 0x040015CE RID: 5582
	public GameObject[] uiPlayerButtons;

	// Token: 0x040015CF RID: 5583
	public int selectedPlayer = -1;

	// Token: 0x040015D0 RID: 5584
	public int selectedLizenz = -1;

	// Token: 0x040015D1 RID: 5585
	private float updateTimer;
}
