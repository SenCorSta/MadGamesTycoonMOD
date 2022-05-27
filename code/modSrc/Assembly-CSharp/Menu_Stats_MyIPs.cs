using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024B RID: 587
public class Menu_Stats_MyIPs : MonoBehaviour
{
	// Token: 0x060016AB RID: 5803 RVA: 0x0000FD79 File Offset: 0x0000DF79
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000EC760 File Offset: 0x000EA960
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
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x0000FD81 File Offset: 0x0000DF81
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000EC828 File Offset: 0x000EAA28
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

	// Token: 0x060016AF RID: 5807 RVA: 0x000EC874 File Offset: 0x000EAA74
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_MyIPs>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x0000FDB9 File Offset: 0x0000DFB9
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x000EC8D0 File Offset: 0x000EAAD0
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		list.Add(this.tS_.GetText(1846));
		list.Add(this.tS_.GetText(1898));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x0000FDC7 File Offset: 0x0000DFC7
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000EC9A4 File Offset: 0x000EABA4
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
				{
					string text = component.GetIpName();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_MyGames_MyIPs component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_MyIPs>();
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.game_ = component;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x0000FDDB File Offset: 0x0000DFDB
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.pubOffer && !script_.auftragsspiel && !script_.typ_contractGame && script_.mainIP == script_.myID;
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x0000FE16 File Offset: 0x0000E016
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x000ECAF8 File Offset: 0x000EACF8
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
				Item_MyGames_MyIPs component = gameObject.GetComponent<Item_MyGames_MyIPs>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.ipPunkte.ToString();
					break;
				case 3:
					gameObject.name = component.game_.merchGesamtGewinn.ToString();
					break;
				case 4:
					gameObject.name = component.game_.ipTime.ToString();
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

	// Token: 0x060016B7 RID: 5815 RVA: 0x000ECC7C File Offset: 0x000EAE7C
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.Init();
	}

	// Token: 0x04001A93 RID: 6803
	private mainScript mS_;

	// Token: 0x04001A94 RID: 6804
	private GameObject main_;

	// Token: 0x04001A95 RID: 6805
	private GUI_Main guiMain_;

	// Token: 0x04001A96 RID: 6806
	private sfxScript sfx_;

	// Token: 0x04001A97 RID: 6807
	private textScript tS_;

	// Token: 0x04001A98 RID: 6808
	private genres genres_;

	// Token: 0x04001A99 RID: 6809
	public GameObject[] uiPrefabs;

	// Token: 0x04001A9A RID: 6810
	public GameObject[] uiObjects;

	// Token: 0x04001A9B RID: 6811
	private float updateTimer;

	// Token: 0x04001A9C RID: 6812
	private string searchStringA = "";
}
