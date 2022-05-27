using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020A RID: 522
public class Menu_QA_NewSpielberichtSelectGame : MonoBehaviour
{
	// Token: 0x060013F1 RID: 5105 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013F2 RID: 5106 RVA: 0x000DAF24 File Offset: 0x000D9124
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

	// Token: 0x060013F3 RID: 5107 RVA: 0x000DAFEC File Offset: 0x000D91EC
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

	// Token: 0x060013F4 RID: 5108 RVA: 0x000DB090 File Offset: 0x000D9290
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

	// Token: 0x060013F5 RID: 5109 RVA: 0x000DB0DC File Offset: 0x000D92DC
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

	// Token: 0x060013F6 RID: 5110 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x060013F7 RID: 5111 RVA: 0x000DB138 File Offset: 0x000D9338
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

	// Token: 0x060013F8 RID: 5112 RVA: 0x000DB1F0 File Offset: 0x000D93F0
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

	// Token: 0x060013F9 RID: 5113 RVA: 0x000DB24C File Offset: 0x000D944C
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

	// Token: 0x060013FA RID: 5114 RVA: 0x000DB364 File Offset: 0x000D9564
	public bool CheckGameData(gameScript script_)
	{
		if (!this.mS_)
		{
			this.FindScripts();
		}
		return script_ && (script_.playerGame || script_.IsMyAuftragsspiel()) && !script_.inDevelopment && !script_.spielbericht && !script_.typ_bundle && !script_.typ_budget && !script_.pubOffer && !script_.typ_bundleAddon && !script_.typ_goty && !script_.schublade && (script_.typ_standard || script_.typ_nachfolger || script_.typ_spinoff) && !this.BereitsInAnderenRaumAktiv(script_.myID);
	}

	// Token: 0x060013FB RID: 5115 RVA: 0x000DB408 File Offset: 0x000D9608
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

	// Token: 0x060013FC RID: 5116 RVA: 0x000DB4A0 File Offset: 0x000D96A0
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

	// Token: 0x060013FD RID: 5117 RVA: 0x0000D9E6 File Offset: 0x0000BBE6
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x000DB618 File Offset: 0x000D9818
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

	// Token: 0x060013FF RID: 5119 RVA: 0x0000DA01 File Offset: 0x0000BC01
	public int GetWorkPoints(gameScript gS_)
	{
		return Mathf.RoundToInt((float)gS_.GetGesamtDevPoints() * 0.1f + 25f);
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x000DB70C File Offset: 0x000D990C
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

	// Token: 0x06001401 RID: 5121 RVA: 0x000DB794 File Offset: 0x000D9994
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

	// Token: 0x0400181F RID: 6175
	public GameObject[] uiPrefabs;

	// Token: 0x04001820 RID: 6176
	public GameObject[] uiObjects;

	// Token: 0x04001821 RID: 6177
	private mainScript mS_;

	// Token: 0x04001822 RID: 6178
	private GameObject main_;

	// Token: 0x04001823 RID: 6179
	private GUI_Main guiMain_;

	// Token: 0x04001824 RID: 6180
	private sfxScript sfx_;

	// Token: 0x04001825 RID: 6181
	private textScript tS_;

	// Token: 0x04001826 RID: 6182
	private genres genres_;

	// Token: 0x04001827 RID: 6183
	public roomScript rS_;

	// Token: 0x04001828 RID: 6184
	private float updateTimer;

	// Token: 0x04001829 RID: 6185
	private float getNumSpielberichteCanCreate_Timer;

	// Token: 0x0400182A RID: 6186
	private int numSpielberichteCanCreate;
}
