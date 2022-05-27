using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000207 RID: 519
public class Menu_QA_BugfixingSelectGame : MonoBehaviour
{
	// Token: 0x060013C5 RID: 5061 RVA: 0x0000D824 File Offset: 0x0000BA24
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x000D9D28 File Offset: 0x000D7F28
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

	// Token: 0x060013C7 RID: 5063 RVA: 0x0000D82C File Offset: 0x0000BA2C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013C8 RID: 5064 RVA: 0x000D9DF0 File Offset: 0x000D7FF0
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

	// Token: 0x060013C9 RID: 5065 RVA: 0x000D9E3C File Offset: 0x000D803C
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

	// Token: 0x060013CA RID: 5066 RVA: 0x0000D864 File Offset: 0x0000BA64
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060013CB RID: 5067 RVA: 0x000D9E98 File Offset: 0x000D8098
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

	// Token: 0x060013CC RID: 5068 RVA: 0x000D9F38 File Offset: 0x000D8138
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

	// Token: 0x060013CD RID: 5069 RVA: 0x000D9F94 File Offset: 0x000D8194
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
				if (component && component.playerGame && component.inDevelopment && !component.isOnMarket && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x060013CE RID: 5070 RVA: 0x000DA0D0 File Offset: 0x000D82D0
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

	// Token: 0x060013CF RID: 5071 RVA: 0x0000D872 File Offset: 0x0000BA72
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x000DA1DC File Offset: 0x000D83DC
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

	// Token: 0x060013D1 RID: 5073 RVA: 0x000DA288 File Offset: 0x000D8488
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

	// Token: 0x040017F6 RID: 6134
	public GameObject[] uiPrefabs;

	// Token: 0x040017F7 RID: 6135
	public GameObject[] uiObjects;

	// Token: 0x040017F8 RID: 6136
	private mainScript mS_;

	// Token: 0x040017F9 RID: 6137
	private GameObject main_;

	// Token: 0x040017FA RID: 6138
	private GUI_Main guiMain_;

	// Token: 0x040017FB RID: 6139
	private sfxScript sfx_;

	// Token: 0x040017FC RID: 6140
	private textScript tS_;

	// Token: 0x040017FD RID: 6141
	private genres genres_;

	// Token: 0x040017FE RID: 6142
	public roomScript rS_;

	// Token: 0x040017FF RID: 6143
	private float updateTimer;
}
