using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024B RID: 587
public class Menu_Stats_MyGames_Umsatz : MonoBehaviour
{
	// Token: 0x060016C2 RID: 5826 RVA: 0x000E52E6 File Offset: 0x000E34E6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x000E52F0 File Offset: 0x000E34F0
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

	// Token: 0x060016C4 RID: 5828 RVA: 0x000E53B8 File Offset: 0x000E35B8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000E53F0 File Offset: 0x000E35F0
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

	// Token: 0x060016C6 RID: 5830 RVA: 0x000E543C File Offset: 0x000E363C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_Umsatz>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x000E5498 File Offset: 0x000E3698
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000E54A0 File Offset: 0x000E36A0
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x000E54B4 File Offset: 0x000E36B4
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[4].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(489));
		list.Add(this.tS_.GetText(1238));
		list.Add(this.tS_.GetText(1370));
		list.Add(this.tS_.GetText(1371));
		list.Add(this.tS_.GetText(6));
		this.uiObjects[4].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[4].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[4].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000E5584 File Offset: 0x000E3784
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
					Item_MyGames_Umsatz component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_Umsatz>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000E56E4 File Offset: 0x000E38E4
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID))
		{
			if (this.uiObjects[6].GetComponent<Toggle>().isOn && script_.developerID != this.mS_.myID)
			{
				return false;
			}
			if (!script_.inDevelopment && !script_.schublade)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000E575B File Offset: 0x000E395B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x000E5778 File Offset: 0x000E3978
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[4].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[4].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_MyGames_Umsatz component = gameObject.GetComponent<Item_MyGames_Umsatz>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetGesamtGewinn().ToString();
					break;
				case 1:
					gameObject.name = component.game_.umsatzTotal.ToString();
					break;
				case 2:
					gameObject.name = component.game_.umsatzAbos.ToString();
					break;
				case 3:
					gameObject.name = component.game_.umsatzInApp.ToString();
					break;
				case 4:
					gameObject.name = component.game_.GetEntwicklungskosten().ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x000E58B4 File Offset: 0x000E3AB4
	public void TOGGLE_OnlyMyGames()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
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
}
