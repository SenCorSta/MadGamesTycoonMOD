using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000203 RID: 515
public class Menu_RemoveGameSelect : MonoBehaviour
{
	// Token: 0x060013AF RID: 5039 RVA: 0x000CE99B File Offset: 0x000CCB9B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x000CE9A4 File Offset: 0x000CCBA4
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

	// Token: 0x060013B1 RID: 5041 RVA: 0x000CEA6C File Offset: 0x000CCC6C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x000CEAA4 File Offset: 0x000CCCA4
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

	// Token: 0x060013B3 RID: 5043 RVA: 0x000CEAF0 File Offset: 0x000CCCF0
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

	// Token: 0x060013B4 RID: 5044 RVA: 0x000CEB4C File Offset: 0x000CCD4C
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x000CEB5C File Offset: 0x000CCD5C
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

	// Token: 0x060013B6 RID: 5046 RVA: 0x000CEC40 File Offset: 0x000CCE40
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

	// Token: 0x060013B7 RID: 5047 RVA: 0x000CECB4 File Offset: 0x000CCEB4
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

	// Token: 0x060013B8 RID: 5048 RVA: 0x000CEE12 File Offset: 0x000CD012
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket;
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000CEE44 File Offset: 0x000CD044
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

	// Token: 0x060013BA RID: 5050 RVA: 0x000CEFCC File Offset: 0x000CD1CC
	public void BUTTON_Close()
	{
		this.mS_.automatic_RemoveGameFormMarket = this.uiObjects[4].GetComponent<Toggle>().isOn;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x000CF01C File Offset: 0x000CD21C
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

	// Token: 0x040017D2 RID: 6098
	public GameObject[] uiPrefabs;

	// Token: 0x040017D3 RID: 6099
	public GameObject[] uiObjects;

	// Token: 0x040017D4 RID: 6100
	private mainScript mS_;

	// Token: 0x040017D5 RID: 6101
	private GameObject main_;

	// Token: 0x040017D6 RID: 6102
	private GUI_Main guiMain_;

	// Token: 0x040017D7 RID: 6103
	private sfxScript sfx_;

	// Token: 0x040017D8 RID: 6104
	private textScript tS_;

	// Token: 0x040017D9 RID: 6105
	private genres genres_;

	// Token: 0x040017DA RID: 6106
	private float updateTimer;

	// Token: 0x040017DB RID: 6107
	private string searchStringA = "";
}
