using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FD RID: 509
public class Menu_PackungSelect : MonoBehaviour
{
	// Token: 0x0600136C RID: 4972 RVA: 0x000CCD28 File Offset: 0x000CAF28
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x000CCD30 File Offset: 0x000CAF30
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

	// Token: 0x0600136E RID: 4974 RVA: 0x000CCDF8 File Offset: 0x000CAFF8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x000CCE30 File Offset: 0x000CB030
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

	// Token: 0x06001370 RID: 4976 RVA: 0x000CCE7C File Offset: 0x000CB07C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_PackungSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x000CCED8 File Offset: 0x000CB0D8
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000CCEE8 File Offset: 0x000CB0E8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1124));
		list.Add(this.tS_.GetText(1133));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000CCFE0 File Offset: 0x000CB1E0
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000CD03C File Offset: 0x000CB23C
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
					Item_PackungSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_PackungSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x000CD148 File Offset: 0x000CB348
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp != 2 && !script_.handy && !script_.arcade;
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x000CD19C File Offset: 0x000CB39C
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
				Item_PackungSelect component = gameObject.GetComponent<Item_PackungSelect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.GetLagerbestand().ToString();
					break;
				case 2:
					gameObject.name = component.game_.verkaufspreis[0].ToString();
					break;
				case 3:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 4:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 5:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 6:
					gameObject.name = component.game_.sellsTotal.ToString();
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

	// Token: 0x06001377 RID: 4983 RVA: 0x000CD351 File Offset: 0x000CB551
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017A0 RID: 6048
	public GameObject[] uiPrefabs;

	// Token: 0x040017A1 RID: 6049
	public GameObject[] uiObjects;

	// Token: 0x040017A2 RID: 6050
	private mainScript mS_;

	// Token: 0x040017A3 RID: 6051
	private GameObject main_;

	// Token: 0x040017A4 RID: 6052
	private GUI_Main guiMain_;

	// Token: 0x040017A5 RID: 6053
	private sfxScript sfx_;

	// Token: 0x040017A6 RID: 6054
	private textScript tS_;

	// Token: 0x040017A7 RID: 6055
	private genres genres_;

	// Token: 0x040017A8 RID: 6056
	public roomScript rS_;

	// Token: 0x040017A9 RID: 6057
	private float updateTimer;
}
