using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020B RID: 523
public class Menu_QA_NewSpielberichtSelectGame : MonoBehaviour
{
	// Token: 0x0600140E RID: 5134 RVA: 0x000D128D File Offset: 0x000CF48D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x000D1298 File Offset: 0x000CF498
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

	// Token: 0x06001410 RID: 5136 RVA: 0x000D1360 File Offset: 0x000CF560
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		if (this.uiObjects[4].GetComponent<Toggle>().isOn)
		{
			this.uiObjects[7].GetComponent<Toggle>().interactable = true;
		}
		else
		{
			this.uiObjects[7].GetComponent<Toggle>().interactable = false;
		}
		this.uiObjects[8].GetComponent<Button>().interactable = this.uiObjects[7].GetComponent<Toggle>().isOn;
		this.MultiplayerUpdate();
	}

	// Token: 0x06001411 RID: 5137 RVA: 0x000D1404 File Offset: 0x000CF604
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

	// Token: 0x06001412 RID: 5138 RVA: 0x000D1450 File Offset: 0x000CF650
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_QA_CreateSpielbericht>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001413 RID: 5139 RVA: 0x000D14AC File Offset: 0x000CF6AC
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x06001414 RID: 5140 RVA: 0x000D14BC File Offset: 0x000CF6BC
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

	// Token: 0x06001415 RID: 5141 RVA: 0x000D1574 File Offset: 0x000CF774
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

	// Token: 0x06001416 RID: 5142 RVA: 0x000D15D0 File Offset: 0x000CF7D0
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
					Item_QA_CreateSpielbericht component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_QA_CreateSpielbericht>();
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

	// Token: 0x06001417 RID: 5143 RVA: 0x000D16E8 File Offset: 0x000CF8E8
	public bool CheckGameData(gameScript script_)
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return script_ && script_.developerID == this.mS_.myID && !script_.inDevelopment && !script_.spielbericht && !script_.typ_bundle && !script_.typ_budget && !script_.pubOffer && !script_.typ_bundleAddon && !script_.typ_goty && !script_.schublade && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && !this.BereitsInAnderenRaumAktiv(script_.myID);
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x000D178C File Offset: 0x000CF98C
	public int GetNumSpielberichteCanCreate()
	{
		this.getNumSpielberichteCanCreate_Timer += Time.deltaTime;
		if (this.getNumSpielberichteCanCreate_Timer < 1f)
		{
			return this.numSpielberichteCanCreate;
		}
		this.getNumSpielberichteCanCreate_Timer = 0f;
		this.numSpielberichteCanCreate = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
				{
					this.numSpielberichteCanCreate++;
				}
			}
		}
		return this.numSpielberichteCanCreate;
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x000D1824 File Offset: 0x000CFA24
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
				Item_QA_CreateSpielbericht component = gameObject.GetComponent<Item_QA_CreateSpielbericht>();
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

	// Token: 0x0600141A RID: 5146 RVA: 0x000D1999 File Offset: 0x000CFB99
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000D19B4 File Offset: 0x000CFBB4
	public void StartSpielbericht(gameScript gS_)
	{
		if (!gS_)
		{
			return;
		}
		if (!this.rS_)
		{
			return;
		}
		taskSpielbericht taskSpielbericht = this.guiMain_.AddTask_Spielbericht();
		taskSpielbericht.Init(false);
		taskSpielbericht.targetID = gS_.myID;
		taskSpielbericht.automatic = this.uiObjects[4].GetComponent<Toggle>().isOn;
		taskSpielbericht.automaticWait = this.uiObjects[7].GetComponent<Toggle>().isOn;
		taskSpielbericht.points = (float)this.GetWorkPoints(gS_);
		taskSpielbericht.pointsLeft = taskSpielbericht.points;
		GameObject gameObject = GameObject.Find("Room_" + this.rS_.myID.ToString());
		if (gameObject)
		{
			gameObject.GetComponent<roomScript>().taskID = taskSpielbericht.myID;
		}
		this.guiMain_.CloseMenu();
		this.guiMain_.uiObjects[180].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x000D1AA8 File Offset: 0x000CFCA8
	public int GetWorkPoints(gameScript gS_)
	{
		return Mathf.RoundToInt((float)gS_.GetGesamtDevPoints() * 0.1f + 25f);
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x000D1AC4 File Offset: 0x000CFCC4
	public bool BereitsInAnderenRaumAktiv(int id_)
	{
		this.FindScripts();
		for (int i = 0; i < this.mS_.arrayRooms.Length; i++)
		{
			if (this.mS_.arrayRooms[i])
			{
				roomScript component = this.mS_.arrayRooms[i].GetComponent<roomScript>();
				if (component.typ == 3 && component.taskGameObject)
				{
					taskSpielbericht component2 = component.taskGameObject.GetComponent<taskSpielbericht>();
					if (component2 && component2.targetID == id_)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000D1B4C File Offset: 0x000CFD4C
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		taskWait taskWait = this.guiMain_.AddTask_Wait();
		taskWait.Init(false);
		taskWait.art = 0;
		this.rS_.taskID = taskWait.myID;
		this.guiMain_.CloseMenu();
		this.guiMain_.uiObjects[180].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001828 RID: 6184
	public GameObject[] uiPrefabs;

	// Token: 0x04001829 RID: 6185
	public GameObject[] uiObjects;

	// Token: 0x0400182A RID: 6186
	private mainScript mS_;

	// Token: 0x0400182B RID: 6187
	private GameObject main_;

	// Token: 0x0400182C RID: 6188
	private GUI_Main guiMain_;

	// Token: 0x0400182D RID: 6189
	private sfxScript sfx_;

	// Token: 0x0400182E RID: 6190
	private textScript tS_;

	// Token: 0x0400182F RID: 6191
	private genres genres_;

	// Token: 0x04001830 RID: 6192
	public roomScript rS_;

	// Token: 0x04001831 RID: 6193
	private float updateTimer;

	// Token: 0x04001832 RID: 6194
	private float getNumSpielberichteCanCreate_Timer;

	// Token: 0x04001833 RID: 6195
	private int numSpielberichteCanCreate;
}
