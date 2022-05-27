using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000201 RID: 513
public class Menu_ProductionSelect : MonoBehaviour
{
	// Token: 0x06001396 RID: 5014 RVA: 0x000CDCB4 File Offset: 0x000CBEB4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000CDCBC File Offset: 0x000CBEBC
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

	// Token: 0x06001398 RID: 5016 RVA: 0x000CDD84 File Offset: 0x000CBF84
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x000CDDBC File Offset: 0x000CBFBC
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

	// Token: 0x0600139A RID: 5018 RVA: 0x000CDE08 File Offset: 0x000CC008
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ProduceSelect>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x000CDE64 File Offset: 0x000CC064
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x000CDE74 File Offset: 0x000CC074
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1124));
		list.Add(this.tS_.GetText(1103));
		list.Add(this.tS_.GetText(1104));
		list.Add(this.tS_.GetText(1105));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x000CDF58 File Offset: 0x000CC158
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

	// Token: 0x0600139E RID: 5022 RVA: 0x000CDFB4 File Offset: 0x000CC1B4
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_ProduceSelect component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_ProduceSelect>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.rS_ = this.rS_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x000CE0DC File Offset: 0x000CC2DC
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.retailVersion && script_.gameTyp != 2 && !script_.handy && !script_.arcade;
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000CE138 File Offset: 0x000CC338
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
				Item_ProduceSelect component = gameObject.GetComponent<Item_ProduceSelect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.GetLagerbestand().ToString();
					break;
				case 2:
					gameObject.name = component.game_.lagerbestand[0].ToString();
					break;
				case 3:
					gameObject.name = component.game_.lagerbestand[1].ToString();
					break;
				case 4:
					gameObject.name = component.game_.lagerbestand[2].ToString();
					break;
				case 5:
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

	// Token: 0x060013A1 RID: 5025 RVA: 0x000CE2F5 File Offset: 0x000CC4F5
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017BE RID: 6078
	public GameObject[] uiPrefabs;

	// Token: 0x040017BF RID: 6079
	public GameObject[] uiObjects;

	// Token: 0x040017C0 RID: 6080
	private mainScript mS_;

	// Token: 0x040017C1 RID: 6081
	private GameObject main_;

	// Token: 0x040017C2 RID: 6082
	private GUI_Main guiMain_;

	// Token: 0x040017C3 RID: 6083
	private sfxScript sfx_;

	// Token: 0x040017C4 RID: 6084
	private textScript tS_;

	// Token: 0x040017C5 RID: 6085
	private genres genres_;

	// Token: 0x040017C6 RID: 6086
	public roomScript rS_;

	// Token: 0x040017C7 RID: 6087
	private float updateTimer;
}
