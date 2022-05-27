using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001CA RID: 458
public class Menu_MP_LizenzSchenken : MonoBehaviour
{
	// Token: 0x0600114F RID: 4431 RVA: 0x000B76DA File Offset: 0x000B58DA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000B76E4 File Offset: 0x000B58E4
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

	// Token: 0x06001151 RID: 4433 RVA: 0x000B780A File Offset: 0x000B5A0A
	private void OnEnable()
	{
		this.selectedLizenz = -1;
		this.selectedPlayer = -1;
		this.InitDropdowns();
		this.Init();
		this.InitPlayerButtons();
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000B782C File Offset: 0x000B5A2C
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

	// Token: 0x06001153 RID: 4435 RVA: 0x000B78FC File Offset: 0x000B5AFC
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

	// Token: 0x06001154 RID: 4436 RVA: 0x000B7978 File Offset: 0x000B5B78
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

	// Token: 0x06001155 RID: 4437 RVA: 0x000B79E8 File Offset: 0x000B5BE8
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

	// Token: 0x06001156 RID: 4438 RVA: 0x000B7B13 File Offset: 0x000B5D13
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000B7B2C File Offset: 0x000B5D2C
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

	// Token: 0x06001158 RID: 4440 RVA: 0x000B7B78 File Offset: 0x000B5D78
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

	// Token: 0x06001159 RID: 4441 RVA: 0x000B7BD0 File Offset: 0x000B5DD0
	public void Init()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x000B7C20 File Offset: 0x000B5E20
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

	// Token: 0x0600115B RID: 4443 RVA: 0x000B7D14 File Offset: 0x000B5F14
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

	// Token: 0x0600115C RID: 4444 RVA: 0x000B7E8B File Offset: 0x000B608B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x000B7EA8 File Offset: 0x000B60A8
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
			this.mpCalls_.SERVER_Send_Help(this.mS_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 2, this.selectedLizenz, this.licences_.licence_GEKAUFT[this.selectedLizenz], 0);
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

	// Token: 0x040015CC RID: 5580
	private mainScript mS_;

	// Token: 0x040015CD RID: 5581
	private GameObject main_;

	// Token: 0x040015CE RID: 5582
	private GUI_Main guiMain_;

	// Token: 0x040015CF RID: 5583
	private sfxScript sfx_;

	// Token: 0x040015D0 RID: 5584
	private textScript tS_;

	// Token: 0x040015D1 RID: 5585
	private engineFeatures eF_;

	// Token: 0x040015D2 RID: 5586
	private genres genres_;

	// Token: 0x040015D3 RID: 5587
	private mpCalls mpCalls_;

	// Token: 0x040015D4 RID: 5588
	private licences licences_;

	// Token: 0x040015D5 RID: 5589
	public GameObject[] uiPrefabs;

	// Token: 0x040015D6 RID: 5590
	public GameObject[] uiObjects;

	// Token: 0x040015D7 RID: 5591
	public GameObject[] uiPlayerButtons;

	// Token: 0x040015D8 RID: 5592
	public int selectedPlayer = -1;

	// Token: 0x040015D9 RID: 5593
	public int selectedLizenz = -1;

	// Token: 0x040015DA RID: 5594
	private float updateTimer;
}
