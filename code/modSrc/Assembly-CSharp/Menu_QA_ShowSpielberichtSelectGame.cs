using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020C RID: 524
public class Menu_QA_ShowSpielberichtSelectGame : MonoBehaviour
{
	// Token: 0x06001420 RID: 5152 RVA: 0x000D1BEB File Offset: 0x000CFDEB
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x000D1BF4 File Offset: 0x000CFDF4
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

	// Token: 0x06001422 RID: 5154 RVA: 0x000D1CBC File Offset: 0x000CFEBC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000D1CF4 File Offset: 0x000CFEF4
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

	// Token: 0x06001424 RID: 5156 RVA: 0x000D1D40 File Offset: 0x000CFF40
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_QA_ShowSpielbericht>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000D1D9C File Offset: 0x000CFF9C
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000D1DAC File Offset: 0x000CFFAC
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000D1E64 File Offset: 0x000D0064
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000D1EB8 File Offset: 0x000D00B8
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
						Item_QA_ShowSpielbericht component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_QA_ShowSpielbericht>();
						component2.game_ = component;
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001429 RID: 5161 RVA: 0x000D200C File Offset: 0x000D020C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && !script_.archiv_spielbericht && script_.spielbericht && !script_.typ_budget && !script_.typ_goty;
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x000D204C File Offset: 0x000D024C
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
				Item_QA_ShowSpielbericht component = gameObject.GetComponent<Item_QA_ShowSpielbericht>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 3:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment || component.game_.schublade)
					{
						gameObject.name = "999999";
					}
					break;
				}
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

	// Token: 0x0600142B RID: 5163 RVA: 0x000D21C1 File Offset: 0x000D03C1
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x000D21DC File Offset: 0x000D03DC
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

	// Token: 0x04001834 RID: 6196
	public GameObject[] uiPrefabs;

	// Token: 0x04001835 RID: 6197
	public GameObject[] uiObjects;

	// Token: 0x04001836 RID: 6198
	private mainScript mS_;

	// Token: 0x04001837 RID: 6199
	private GameObject main_;

	// Token: 0x04001838 RID: 6200
	private GUI_Main guiMain_;

	// Token: 0x04001839 RID: 6201
	private sfxScript sfx_;

	// Token: 0x0400183A RID: 6202
	private textScript tS_;

	// Token: 0x0400183B RID: 6203
	private genres genres_;

	// Token: 0x0400183C RID: 6204
	private float updateTimer;

	// Token: 0x0400183D RID: 6205
	private string searchStringA = "";
}
