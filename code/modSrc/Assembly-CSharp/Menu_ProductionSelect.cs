using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000200 RID: 512
public class Menu_ProductionSelect : MonoBehaviour
{
	// Token: 0x0600137B RID: 4987 RVA: 0x0000D522 File Offset: 0x0000B722
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000D7ED0 File Offset: 0x000D60D0
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

	// Token: 0x0600137D RID: 4989 RVA: 0x0000D52A File Offset: 0x0000B72A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000D7F98 File Offset: 0x000D6198
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

	// Token: 0x0600137F RID: 4991 RVA: 0x000D7FE4 File Offset: 0x000D61E4
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

	// Token: 0x06001380 RID: 4992 RVA: 0x0000D562 File Offset: 0x0000B762
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000D8040 File Offset: 0x000D6240
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

	// Token: 0x06001382 RID: 4994 RVA: 0x000D8124 File Offset: 0x000D6324
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

	// Token: 0x06001383 RID: 4995 RVA: 0x000D8180 File Offset: 0x000D6380
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

	// Token: 0x06001384 RID: 4996 RVA: 0x000D82A8 File Offset: 0x000D64A8
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && script_.isOnMarket && script_.retailVersion && script_.publisherID == -1 && script_.gameTyp != 2 && !script_.handy && !script_.arcade;
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000D8304 File Offset: 0x000D6504
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

	// Token: 0x06001386 RID: 4998 RVA: 0x0000D570 File Offset: 0x0000B770
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017B5 RID: 6069
	public GameObject[] uiPrefabs;

	// Token: 0x040017B6 RID: 6070
	public GameObject[] uiObjects;

	// Token: 0x040017B7 RID: 6071
	private mainScript mS_;

	// Token: 0x040017B8 RID: 6072
	private GameObject main_;

	// Token: 0x040017B9 RID: 6073
	private GUI_Main guiMain_;

	// Token: 0x040017BA RID: 6074
	private sfxScript sfx_;

	// Token: 0x040017BB RID: 6075
	private textScript tS_;

	// Token: 0x040017BC RID: 6076
	private genres genres_;

	// Token: 0x040017BD RID: 6077
	public roomScript rS_;

	// Token: 0x040017BE RID: 6078
	private float updateTimer;
}
