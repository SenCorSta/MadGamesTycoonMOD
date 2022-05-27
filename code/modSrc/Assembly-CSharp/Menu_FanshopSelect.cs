using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200026A RID: 618
public class Menu_FanshopSelect : MonoBehaviour
{
	// Token: 0x060017EC RID: 6124 RVA: 0x0001097A File Offset: 0x0000EB7A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000F60A4 File Offset: 0x000F42A4
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

	// Token: 0x060017EE RID: 6126 RVA: 0x00010982 File Offset: 0x0000EB82
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000F616C File Offset: 0x000F436C
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

	// Token: 0x060017F0 RID: 6128 RVA: 0x000F61B8 File Offset: 0x000F43B8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_Fanshop>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x000109BA File Offset: 0x0000EBBA
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000F61FC File Offset: 0x000F43FC
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		list.Add(this.tS_.GetText(1846));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000109C2 File Offset: 0x0000EBC2
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
		this.UpdateAmountAutomatic();
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x000F62B8 File Offset: 0x000F44B8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_Fanshop component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Fanshop>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x0000FDDB File Offset: 0x0000DFDB
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.pubOffer && !script_.auftragsspiel && !script_.typ_contractGame && script_.mainIP == script_.myID;
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x000109DC File Offset: 0x0000EBDC
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (!this.guiMain_.uiObjects[368].activeSelf)
		{
			this.guiMain_.CloseMenu();
		}
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x000F63C4 File Offset: 0x000F45C4
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
				Item_Fanshop component = gameObject.GetComponent<Item_Fanshop>();
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

	// Token: 0x060017F8 RID: 6136 RVA: 0x00002098 File Offset: 0x00000298
	public void TOGGLE_AllAuto()
	{
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000F652C File Offset: 0x000F472C
	public void BUTTON_SellAll()
	{
		bool flag = false;
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_Fanshop component = gameObject.GetComponent<Item_Fanshop>();
				if (component && component.game_)
				{
					if (i == 0)
					{
						flag = component.game_.merchKeinVerkauf;
						flag = !flag;
					}
					component.game_.merchKeinVerkauf = flag;
				}
			}
		}
		this.UpdateAmountAutomatic();
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x00002098 File Offset: 0x00000298
	public void BUTTON_AllAuto()
	{
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x00002098 File Offset: 0x00000298
	public void UpdateAmountAutomatic()
	{
	}

	// Token: 0x04001BC3 RID: 7107
	private mainScript mS_;

	// Token: 0x04001BC4 RID: 7108
	private GameObject main_;

	// Token: 0x04001BC5 RID: 7109
	private GUI_Main guiMain_;

	// Token: 0x04001BC6 RID: 7110
	private sfxScript sfx_;

	// Token: 0x04001BC7 RID: 7111
	private textScript tS_;

	// Token: 0x04001BC8 RID: 7112
	private genres genres_;

	// Token: 0x04001BC9 RID: 7113
	public GameObject[] uiPrefabs;

	// Token: 0x04001BCA RID: 7114
	public GameObject[] uiObjects;

	// Token: 0x04001BCB RID: 7115
	private float updateTimer;
}
