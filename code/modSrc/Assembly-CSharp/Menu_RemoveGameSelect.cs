using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000202 RID: 514
public class Menu_RemoveGameSelect : MonoBehaviour
{
	// Token: 0x06001394 RID: 5012 RVA: 0x0000D617 File Offset: 0x0000B817
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000D8AC4 File Offset: 0x000D6CC4
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

	// Token: 0x06001396 RID: 5014 RVA: 0x0000D61F File Offset: 0x0000B81F
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000D8B8C File Offset: 0x000D6D8C
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

	// Token: 0x06001398 RID: 5016 RVA: 0x000D8BD8 File Offset: 0x000D6DD8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_RemoveGame>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x0000D657 File Offset: 0x0000B857
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x000D8C34 File Offset: 0x000D6E34
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(491));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x000D8D18 File Offset: 0x000D6F18
	public void Init()
	{
		this.FindScripts();
		this.uiObjects[4].GetComponent<Toggle>().isOn = this.mS_.automatic_RemoveGameFormMarket;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x000D8D8C File Offset: 0x000D6F8C
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
					string text = component.GetNameSimple();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_RemoveGame component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_RemoveGame>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.menuScript_ = this;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x0000D665 File Offset: 0x0000B865
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && script_.isOnMarket && script_.publisherID == -1;
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x000D8EEC File Offset: 0x000D70EC
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
				Item_RemoveGame component = gameObject.GetComponent<Item_RemoveGame>();
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
					break;
				}
				case 2:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 4:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 5:
					gameObject.name = component.game_.weeksOnMarket.ToString();
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

	// Token: 0x0600139F RID: 5023 RVA: 0x000D9074 File Offset: 0x000D7274
	public void BUTTON_Close()
	{
		this.mS_.automatic_RemoveGameFormMarket = this.uiObjects[4].GetComponent<Toggle>().isOn;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000D90C4 File Offset: 0x000D72C4
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

	// Token: 0x040017C9 RID: 6089
	public GameObject[] uiPrefabs;

	// Token: 0x040017CA RID: 6090
	public GameObject[] uiObjects;

	// Token: 0x040017CB RID: 6091
	private mainScript mS_;

	// Token: 0x040017CC RID: 6092
	private GameObject main_;

	// Token: 0x040017CD RID: 6093
	private GUI_Main guiMain_;

	// Token: 0x040017CE RID: 6094
	private sfxScript sfx_;

	// Token: 0x040017CF RID: 6095
	private textScript tS_;

	// Token: 0x040017D0 RID: 6096
	private genres genres_;

	// Token: 0x040017D1 RID: 6097
	private float updateTimer;

	// Token: 0x040017D2 RID: 6098
	private string searchStringA = "";
}
