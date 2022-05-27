using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000108 RID: 264
public class Menu_Dev_AuftragSelect : MonoBehaviour
{
	// Token: 0x06000876 RID: 2166 RVA: 0x0000649D File Offset: 0x0000469D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0006EF40 File Offset: 0x0006D140
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
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0006EFEC File Offset: 0x0006D1EC
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

	// Token: 0x06000879 RID: 2169 RVA: 0x0006F090 File Offset: 0x0006D290
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

	// Token: 0x0600087A RID: 2170 RVA: 0x0006F0DC File Offset: 0x0006D2DC
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_ContractWork>().contract_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x000064A5 File Offset: 0x000046A5
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0006F138 File Offset: 0x0006D338
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(600));
		list.Add(this.tS_.GetText(601));
		list.Add(this.tS_.GetText(602));
		list.Add(this.tS_.GetText(603));
		list.Add(this.tS_.GetText(604));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0006F204 File Offset: 0x0006D404
	public void Init(roomScript room_)
	{
		this.FindScripts();
		this.rS_ = room_;
		if (this.rS_.typ != 14)
		{
			this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(597);
		}
		else
		{
			this.uiObjects[6].GetComponent<Text>().text = this.tS_.GetText(1130);
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0006F2B4 File Offset: 0x0006D4B4
	private void SetData()
	{
		if (!this.rS_)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("ContractWork");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				contractWork component = array[i].GetComponent<contractWork>();
				if (component && ((this.rS_.typ == 1 && component.art == 0) || (this.rS_.typ == 3 && component.art == 1) || (this.rS_.typ == 4 && component.art == 2) || (this.rS_.typ == 5 && component.art == 3) || (this.rS_.typ == 10 && component.art == 4) || (this.rS_.typ == 14 && component.art == 5) || (this.rS_.typ == 17 && component.art == 6) || (this.rS_.typ == 8 && component.art == 7)) && !component.IsAngenommen() && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject;
					if (this.rS_.typ != 14)
					{
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					}
					else
					{
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[1], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					}
					Item_ContractWork component2 = gameObject.GetComponent<Item_ContractWork>();
					component2.contract_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0006F4D0 File Offset: 0x0006D6D0
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
				Item_ContractWork component = gameObject.GetComponent<Item_ContractWork>();
				switch (value)
				{
				case 0:
					gameObject.name = component.contract_.GetGehalt().ToString();
					break;
				case 1:
					gameObject.name = component.contract_.GetStrafe().ToString();
					break;
				case 2:
					gameObject.name = component.contract_.GetWochen().ToString();
					break;
				case 3:
					gameObject.name = component.contract_.GetArbeitsaufwand().ToString();
					break;
				case 4:
					gameObject.name = component.contract_.auftraggeberID.ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0006F614 File Offset: 0x0006D814
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		if (this.rS_.typ == 1)
		{
			this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[95]);
		}
		else
		{
			this.guiMain_.CloseMenu();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0006F670 File Offset: 0x0006D870
	public void BUTTON_OK()
	{
		this.sfx_.PlaySound(3, true);
		taskContractWait taskContractWait = this.guiMain_.AddTask_ContractWait();
		taskContractWait.Init(false);
		int typ = this.rS_.typ;
		switch (typ)
		{
		case 1:
			taskContractWait.art = 0;
			break;
		case 2:
		case 6:
		case 7:
		case 9:
			break;
		case 3:
			taskContractWait.art = 1;
			break;
		case 4:
			taskContractWait.art = 2;
			break;
		case 5:
			taskContractWait.art = 3;
			break;
		case 8:
			taskContractWait.art = 7;
			break;
		case 10:
			taskContractWait.art = 4;
			break;
		default:
			if (typ != 14)
			{
				if (typ == 17)
				{
					taskContractWait.art = 6;
				}
			}
			else
			{
				taskContractWait.art = 5;
			}
			break;
		}
		this.rS_.taskID = taskContractWait.myID;
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000D05 RID: 3333
	public GameObject[] uiPrefabs;

	// Token: 0x04000D06 RID: 3334
	public GameObject[] uiObjects;

	// Token: 0x04000D07 RID: 3335
	private mainScript mS_;

	// Token: 0x04000D08 RID: 3336
	private GameObject main_;

	// Token: 0x04000D09 RID: 3337
	private GUI_Main guiMain_;

	// Token: 0x04000D0A RID: 3338
	private sfxScript sfx_;

	// Token: 0x04000D0B RID: 3339
	private textScript tS_;

	// Token: 0x04000D0C RID: 3340
	public roomScript rS_;

	// Token: 0x04000D0D RID: 3341
	private float updateTimer;
}
