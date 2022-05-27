using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001C4 RID: 452
public class Menu_MP_ForschungSchenken : MonoBehaviour
{
	// Token: 0x060010FB RID: 4347 RVA: 0x0000BF44 File Offset: 0x0000A144
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000C0D3C File Offset: 0x000BEF3C
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
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.gF_)
		{
			this.gF_ = this.main_.GetComponent<gameplayFeatures>();
		}
		if (!this.hardware_)
		{
			this.hardware_ = this.main_.GetComponent<hardware>();
		}
		if (!this.hardwareFeature_)
		{
			this.hardwareFeature_ = this.main_.GetComponent<hardwareFeatures>();
		}
		if (!this.copyProtect_)
		{
			this.copyProtect_ = this.main_.GetComponent<copyProtect>();
		}
		if (!this.unlock_)
		{
			this.unlock_ = this.main_.GetComponent<unlockScript>();
		}
		if (!this.fS_)
		{
			this.fS_ = this.main_.GetComponent<forschungSonstiges>();
		}
		if (!this.mpCalls_)
		{
			this.mpCalls_ = GameObject.Find("NetworkManager").GetComponent<mpCalls>();
		}
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0000BF4C File Offset: 0x0000A14C
	private void OnEnable()
	{
		this.selectedForschung = -1;
		this.selectedPlayer = -1;
		this.uiObjects[7].GetComponent<InputField>().text = "";
		this.FindScripts();
		this.InitDropdowns();
		this.InitPlayerButtons();
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x000C0F18 File Offset: 0x000BF118
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		if (this.selectedForschung == -1)
		{
			this.uiObjects[8].GetComponent<Button>().interactable = false;
		}
		else
		{
			this.uiObjects[8].GetComponent<Button>().interactable = true;
		}
		this.UpdatePlayerButtons();
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x000C0F8C File Offset: 0x000BF18C
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

	// Token: 0x06001100 RID: 4352 RVA: 0x000C0FFC File Offset: 0x000BF1FC
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

	// Token: 0x06001101 RID: 4353 RVA: 0x0000BF85 File Offset: 0x0000A185
	public void BUTTON_Player(int p)
	{
		this.sfx_.PlaySound(12, true);
		this.selectedPlayer = p;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x000C1128 File Offset: 0x000BF328
	public void Init(int i)
	{
		this.FindScripts();
		this.forschungsTyp = i;
		switch (i)
		{
		case 0:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(158);
			break;
		case 1:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(159);
			break;
		case 2:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(160);
			break;
		case 3:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(161);
			break;
		case 4:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(162);
			break;
		case 5:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(163);
			break;
		case 6:
			this.uiObjects[1].GetComponent<Text>().text = this.tS_.GetText(1617);
			break;
		}
		switch (i)
		{
		case 0:
			for (int j = 0; j < this.genres_.genres_RES_POINTS.Length; j++)
			{
				if (this.genres_.genres_UNLOCK[j] && this.genres_.IsErforscht(j))
				{
					string text = this.genres_.GetName(j);
					text = text.ToLower();
					if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA.ToLower()))
					{
						Item_ForschungSchenken component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
						component.myID = j;
						component.art = i;
						component.mS_ = this.mS_;
						component.tS_ = this.tS_;
						component.sfx_ = this.sfx_;
						component.guiMain_ = this.guiMain_;
						component.genres_ = this.genres_;
						component.menu_ = this;
					}
				}
			}
			break;
		case 1:
			for (int k = 0; k < this.themes_.themes_RES_POINTS_LEFT.Length; k++)
			{
				if (this.themes_.IsErforscht(k))
				{
					string text2 = this.tS_.GetThemes(k);
					text2 = text2.ToLower();
					if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text2.Contains(this.searchStringA.ToLower()))
					{
						Item_ForschungSchenken component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
						component2.myID = k;
						component2.art = i;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.themes_ = this.themes_;
						component2.menu_ = this;
					}
				}
			}
			break;
		case 2:
			for (int l = 0; l < this.eF_.engineFeatures_RES_POINTS.Length; l++)
			{
				if (this.eF_.engineFeatures_UNLOCK[l] && this.eF_.IsErforscht(l))
				{
					string text3 = this.eF_.GetName(l);
					text3 = text3.ToLower();
					if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text3.Contains(this.searchStringA.ToLower()))
					{
						Item_ForschungSchenken component3 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
						component3.myID = l;
						component3.art = i;
						component3.mS_ = this.mS_;
						component3.tS_ = this.tS_;
						component3.sfx_ = this.sfx_;
						component3.guiMain_ = this.guiMain_;
						component3.eF_ = this.eF_;
						component3.menu_ = this;
					}
				}
			}
			break;
		case 3:
			for (int m = 0; m < this.gF_.gameplayFeatures_RES_POINTS.Length; m++)
			{
				if (this.gF_.gameplayFeatures_UNLOCK[m] && this.gF_.IsErforscht(m))
				{
					string text4 = this.gF_.GetName(m);
					text4 = text4.ToLower();
					if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text4.Contains(this.searchStringA.ToLower()))
					{
						Item_ForschungSchenken component4 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
						component4.myID = m;
						component4.art = i;
						component4.mS_ = this.mS_;
						component4.tS_ = this.tS_;
						component4.sfx_ = this.sfx_;
						component4.guiMain_ = this.guiMain_;
						component4.gF_ = this.gF_;
						component4.menu_ = this;
					}
				}
			}
			break;
		case 4:
			for (int n = 0; n < this.hardware_.hardware_RES_POINTS.Length; n++)
			{
				if (this.hardware_.hardware_UNLOCK[n] && this.hardware_.IsErforscht(n))
				{
					string text5 = this.hardware_.GetName(n);
					text5 = text5.ToLower();
					if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text5.Contains(this.searchStringA.ToLower()))
					{
						Item_ForschungSchenken component5 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
						component5.myID = n;
						component5.art = i;
						component5.mS_ = this.mS_;
						component5.tS_ = this.tS_;
						component5.sfx_ = this.sfx_;
						component5.guiMain_ = this.guiMain_;
						component5.hardware_ = this.hardware_;
						component5.menu_ = this;
					}
				}
			}
			break;
		case 5:
			if (this.fS_.IsErforscht(0))
			{
				string text6 = this.fS_.GetName(0);
				text6 = text6.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text6.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(0);
				}
			}
			if (this.fS_.IsErforscht(1))
			{
				string text7 = this.fS_.GetName(1);
				text7 = text7.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text7.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(1);
				}
			}
			if (this.fS_.IsErforscht(2))
			{
				string text8 = this.fS_.GetName(2);
				text8 = text8.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text8.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(2);
				}
			}
			if (this.fS_.IsErforscht(3))
			{
				string text9 = this.fS_.GetName(3);
				text9 = text9.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text9.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(3);
				}
			}
			if (this.fS_.IsErforscht(35))
			{
				string text10 = this.fS_.GetName(35);
				text10 = text10.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text10.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(35);
				}
			}
			if (this.fS_.IsErforscht(36))
			{
				string text11 = this.fS_.GetName(36);
				text11 = text11.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text11.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(36);
				}
			}
			if (this.fS_.IsErforscht(28))
			{
				string text12 = this.fS_.GetName(28);
				text12 = text12.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text12.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(28);
				}
			}
			if (this.fS_.IsErforscht(29))
			{
				string text13 = this.fS_.GetName(29);
				text13 = text13.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text13.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(29);
				}
			}
			if (this.fS_.IsErforscht(30))
			{
				string text14 = this.fS_.GetName(30);
				text14 = text14.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text14.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(30);
				}
			}
			if (this.fS_.IsErforscht(31))
			{
				string text15 = this.fS_.GetName(31);
				text15 = text15.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text15.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(31);
				}
			}
			if (this.fS_.IsErforscht(32))
			{
				string text16 = this.fS_.GetName(32);
				text16 = text16.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text16.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(32);
				}
			}
			if (this.fS_.IsErforscht(33))
			{
				string text17 = this.fS_.GetName(33);
				text17 = text17.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text17.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(33);
				}
			}
			if (this.fS_.IsErforscht(34))
			{
				string text18 = this.fS_.GetName(34);
				text18 = text18.ToLower();
				if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text18.Contains(this.searchStringA.ToLower()))
				{
					this.CreateItem(34);
				}
			}
			break;
		case 6:
			for (int num = 0; num < this.hardwareFeature_.hardFeat_RES_POINTS.Length; num++)
			{
				if (this.hardwareFeature_.hardFeat_UNLOCK[num] && this.hardwareFeature_.IsErforscht(num))
				{
					string text19 = this.hardware_.GetName(num);
					text19 = text19.ToLower();
					if (this.uiObjects[7].GetComponent<InputField>().text.Length <= 0 || text19.Contains(this.searchStringA.ToLower()))
					{
						Item_ForschungSchenken component6 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
						component6.myID = num;
						component6.art = i;
						component6.mS_ = this.mS_;
						component6.tS_ = this.tS_;
						component6.sfx_ = this.sfx_;
						component6.guiMain_ = this.guiMain_;
						component6.hardwareFeature_ = this.hardwareFeature_;
						component6.menu_ = this;
					}
				}
			}
			break;
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000C1DF4 File Offset: 0x000BFFF4
	private void CreateItem(int id_)
	{
		if (this.fS_.IsErforscht(id_))
		{
			Item_ForschungSchenken component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ForschungSchenken>();
			component.myID = id_;
			component.art = 5;
			component.mS_ = this.mS_;
			component.tS_ = this.tS_;
			component.sfx_ = this.sfx_;
			component.guiMain_ = this.guiMain_;
			component.fS_ = this.fS_;
			component.menu_ = this;
		}
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x000C1E9C File Offset: 0x000C009C
	public void BUTTON_Search()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[7].GetComponent<InputField>().text;
		this.Init(this.forschungsTyp);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0000BF9C File Offset: 0x0000A19C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x000C1F08 File Offset: 0x000C0108
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[6].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(414));
		list.Add(this.tS_.GetText(415));
		this.uiObjects[6].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[6].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[6].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000C1FA8 File Offset: 0x000C01A8
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[6].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[6].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_ForschungSchenken component = gameObject.GetComponent<Item_ForschungSchenken>();
				switch (value)
				{
				case 0:
					switch (component.art)
					{
					case 0:
						gameObject.name = this.genres_.GetName(component.myID);
						break;
					case 1:
						gameObject.name = this.tS_.GetThemes(component.myID);
						break;
					case 2:
						gameObject.name = this.eF_.GetName(component.myID);
						break;
					case 3:
						gameObject.name = this.gF_.GetName(component.myID);
						break;
					case 4:
						gameObject.name = this.hardware_.GetName(component.myID);
						break;
					case 5:
						gameObject.name = this.fS_.GetName(component.myID);
						break;
					}
					break;
				case 1:
					switch (component.art)
					{
					case 0:
						gameObject.name = this.genres_.GetPrice(component.myID).ToString();
						break;
					case 1:
						gameObject.name = this.themes_.GetPrice(component.myID).ToString();
						break;
					case 2:
						gameObject.name = this.eF_.GetPrice(component.myID).ToString();
						break;
					case 3:
						gameObject.name = this.gF_.GetPrice(component.myID).ToString();
						break;
					case 4:
						gameObject.name = this.hardware_.GetPrice(component.myID).ToString();
						break;
					case 5:
						gameObject.name = this.fS_.RES_PRICE[component.myID].ToString();
						break;
					}
					break;
				case 2:
					switch (component.art)
					{
					case 0:
						gameObject.name = this.genres_.genres_RES_POINTS_LEFT[component.myID].ToString();
						break;
					case 1:
						gameObject.name = this.themes_.themes_RES_POINTS_LEFT[component.myID].ToString();
						break;
					case 2:
						gameObject.name = this.eF_.engineFeatures_RES_POINTS_LEFT[component.myID].ToString();
						break;
					case 3:
						gameObject.name = this.gF_.gameplayFeatures_RES_POINTS_LEFT[component.myID].ToString();
						break;
					case 4:
						gameObject.name = this.hardware_.hardware_RES_POINTS_LEFT[component.myID].ToString();
						break;
					case 5:
						gameObject.name = this.fS_.RES_POINTS_LEFT[component.myID].ToString();
						break;
					}
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

	// Token: 0x06001108 RID: 4360 RVA: 0x000C2364 File Offset: 0x000C0564
	public void BUTTON_Ok()
	{
		if (this.selectedForschung == -1)
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
			this.mpCalls_.SERVER_Send_Help(this.mpCalls_.myID, this.mpCalls_.playersMP[this.selectedPlayer].playerID, 3, this.selectedForschung, this.forschungsTyp, 0);
		}
		else
		{
			this.mpCalls_.CLIENT_Send_Help(this.mpCalls_.playersMP[this.selectedPlayer].playerID, 3, this.selectedForschung, this.forschungsTyp, 0);
		}
		string text = this.tS_.GetText(1338);
		text = text.Replace("<NAME1>", this.mpCalls_.GetCompanyName(this.mpCalls_.playersMP[this.selectedPlayer].playerID));
		switch (this.forschungsTyp)
		{
		case 0:
			text = text.Replace("<NAME2>", this.genres_.GetName(this.selectedForschung));
			break;
		case 1:
			text = text.Replace("<NAME2>", this.tS_.GetThemes(this.selectedForschung));
			break;
		case 2:
			text = text.Replace("<NAME2>", this.eF_.GetName(this.selectedForschung));
			break;
		case 3:
			text = text.Replace("<NAME2>", this.gF_.GetName(this.selectedForschung));
			break;
		case 4:
			text = text.Replace("<NAME2>", this.hardware_.GetName(this.selectedForschung));
			break;
		case 5:
			text = text.Replace("<NAME2>", this.fS_.GetName(this.selectedForschung));
			break;
		case 6:
			text = text.Replace("<NAME2>", this.hardwareFeature_.GetName(this.selectedForschung));
			break;
		}
		this.guiMain_.MessageBox(text, false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001593 RID: 5523
	private mainScript mS_;

	// Token: 0x04001594 RID: 5524
	private GameObject main_;

	// Token: 0x04001595 RID: 5525
	private GUI_Main guiMain_;

	// Token: 0x04001596 RID: 5526
	private sfxScript sfx_;

	// Token: 0x04001597 RID: 5527
	private textScript tS_;

	// Token: 0x04001598 RID: 5528
	private genres genres_;

	// Token: 0x04001599 RID: 5529
	private themes themes_;

	// Token: 0x0400159A RID: 5530
	private engineFeatures eF_;

	// Token: 0x0400159B RID: 5531
	private gameplayFeatures gF_;

	// Token: 0x0400159C RID: 5532
	private hardware hardware_;

	// Token: 0x0400159D RID: 5533
	private hardwareFeatures hardwareFeature_;

	// Token: 0x0400159E RID: 5534
	private unlockScript unlock_;

	// Token: 0x0400159F RID: 5535
	private forschungSonstiges fS_;

	// Token: 0x040015A0 RID: 5536
	private copyProtect copyProtect_;

	// Token: 0x040015A1 RID: 5537
	private mpCalls mpCalls_;

	// Token: 0x040015A2 RID: 5538
	private int forschungsTyp;

	// Token: 0x040015A3 RID: 5539
	public int selectedPlayer = -1;

	// Token: 0x040015A4 RID: 5540
	public int selectedForschung = -1;

	// Token: 0x040015A5 RID: 5541
	public GameObject[] uiPlayerButtons;

	// Token: 0x040015A6 RID: 5542
	public GameObject[] uiPrefabs;

	// Token: 0x040015A7 RID: 5543
	public GameObject[] uiObjects;

	// Token: 0x040015A8 RID: 5544
	private string searchStringA = "";
}
