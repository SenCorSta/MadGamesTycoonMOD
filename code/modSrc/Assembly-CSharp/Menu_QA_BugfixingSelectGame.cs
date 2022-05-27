using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000208 RID: 520
public class Menu_QA_BugfixingSelectGame : MonoBehaviour
{
	// Token: 0x060013E0 RID: 5088 RVA: 0x000CFE6B File Offset: 0x000CE06B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x000CFE74 File Offset: 0x000CE074
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

	// Token: 0x060013E2 RID: 5090 RVA: 0x000CFF3C File Offset: 0x000CE13C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x000CFF74 File Offset: 0x000CE174
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

	// Token: 0x060013E4 RID: 5092 RVA: 0x000CFFC0 File Offset: 0x000CE1C0
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_QA_Bugfixing>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x000D001C File Offset: 0x000CE21C
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x000D002C File Offset: 0x000CE22C
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(424));
		list.Add(this.tS_.GetText(273));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x000D00CC File Offset: 0x000CE2CC
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

	// Token: 0x060013E8 RID: 5096 RVA: 0x000D0128 File Offset: 0x000CE328
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
					Item_QA_Bugfixing component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_QA_Bugfixing>();
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

	// Token: 0x060013E9 RID: 5097 RVA: 0x000D024F File Offset: 0x000CE44F
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.inDevelopment && !script_.isOnMarket;
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x000D0280 File Offset: 0x000CE480
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
				Item_QA_Bugfixing component = gameObject.GetComponent<Item_QA_Bugfixing>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = Mathf.RoundToInt(component.game_.points_bugs).ToString();
					break;
				case 2:
					gameObject.name = component.game_.maingenre.ToString();
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

	// Token: 0x060013EB RID: 5099 RVA: 0x000D038C File Offset: 0x000CE58C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x000D03B4 File Offset: 0x000CE5B4
	public void StartBugfixing(gameScript gS_)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		taskBugfixing taskBugfixing = this.guiMain_.AddTask_Bugfixing();
		taskBugfixing.Init(false);
		taskBugfixing.targetID = gS_.myID;
		taskBugfixing.points = 5f;
		taskBugfixing.pointsLeft = taskBugfixing.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskBugfixing.myID;
		}
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x000D0460 File Offset: 0x000CE660
	public void StartBugfixingAutomatic(gameScript gS_, int taskID)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.guiMain_)
		{
			this.FindScripts();
		}
		taskBugfixing taskBugfixing = this.guiMain_.AddTask_Bugfixing();
		taskBugfixing.Init(false);
		taskBugfixing.targetID = gS_.myID;
		taskBugfixing.points = 5f;
		taskBugfixing.pointsLeft = taskBugfixing.points;
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component && component.taskID == taskID)
				{
					component.taskGameObject = taskBugfixing.gameObject;
					component.taskID = taskBugfixing.myID;
					return;
				}
			}
		}
	}

	// Token: 0x040017FF RID: 6143
	public GameObject[] uiPrefabs;

	// Token: 0x04001800 RID: 6144
	public GameObject[] uiObjects;

	// Token: 0x04001801 RID: 6145
	private mainScript mS_;

	// Token: 0x04001802 RID: 6146
	private GameObject main_;

	// Token: 0x04001803 RID: 6147
	private GUI_Main guiMain_;

	// Token: 0x04001804 RID: 6148
	private sfxScript sfx_;

	// Token: 0x04001805 RID: 6149
	private textScript tS_;

	// Token: 0x04001806 RID: 6150
	private genres genres_;

	// Token: 0x04001807 RID: 6151
	public roomScript rS_;

	// Token: 0x04001808 RID: 6152
	private float updateTimer;
}
