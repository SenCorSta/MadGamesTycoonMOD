using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000119 RID: 281
public class Menu_DevGame_AntiCheat : MonoBehaviour
{
	// Token: 0x0600099E RID: 2462 RVA: 0x000699C0 File Offset: 0x00067BC0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x000699C8 File Offset: 0x00067BC8
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
		if (!this.devGame_)
		{
			this.devGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.menuChangeCopyProtect_)
		{
			this.menuChangeCopyProtect_ = this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>();
		}
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00069AC1 File Offset: 0x00067CC1
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00069AFC File Offset: 0x00067CFC
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

	// Token: 0x060009A2 RID: 2466 RVA: 0x00069B48 File Offset: 0x00067D48
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_AntiCheat>().acS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00069BA4 File Offset: 0x00067DA4
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00069BB8 File Offset: 0x00067DB8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(218));
		list.Add(this.tS_.GetText(286));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00069C58 File Offset: 0x00067E58
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			if (this.devGame_.g_GameAntiCheat != -1)
			{
				this.uiObjects[4].GetComponent<Button>().interactable = true;
			}
			else
			{
				this.uiObjects[4].GetComponent<Button>().interactable = false;
			}
		}
		if (this.guiMain_.uiObjects[365].activeSelf)
		{
			if (this.menuChangeCopyProtect_.g_GameAntiCheat != -1)
			{
				this.uiObjects[4].GetComponent<Button>().interactable = true;
			}
			else
			{
				this.uiObjects[4].GetComponent<Button>().interactable = false;
			}
		}
		this.SetData();
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00069D48 File Offset: 0x00067F48
	private void SetData()
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("AntiCheat");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				antiCheatScript component = array[i].GetComponent<antiCheatScript>();
				if (component && component.isUnlocked && component.inBesitz && (component.effekt > 0f || !isOn) && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_DevGame_AntiCheat component2 = gameObject.GetComponent<Item_DevGame_AntiCheat>();
					component2.acS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					if (this.devGame_.g_GameAntiCheat == component.myID)
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00069EA0 File Offset: 0x000680A0
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
				Item_DevGame_AntiCheat component = gameObject.GetComponent<Item_DevGame_AntiCheat>();
				switch (value)
				{
				case 0:
					gameObject.name = component.acS_.GetName();
					break;
				case 1:
					gameObject.name = component.acS_.GetPrice().ToString();
					break;
				case 2:
					gameObject.name = component.acS_.effekt.ToString();
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

	// Token: 0x060009A8 RID: 2472 RVA: 0x00069FA7 File Offset: 0x000681A7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00069FC2 File Offset: 0x000681C2
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00069FCC File Offset: 0x000681CC
	public void BUTTON_AntiCheatEntfernen()
	{
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.devGame_.SetAntiCheat(-1);
		}
		if (this.guiMain_.uiObjects[365].activeSelf)
		{
			this.menuChangeCopyProtect_.SetAntiCheat(-1);
		}
		this.BUTTON_Close();
	}

	// Token: 0x04000DF6 RID: 3574
	public GameObject[] uiPrefabs;

	// Token: 0x04000DF7 RID: 3575
	public GameObject[] uiObjects;

	// Token: 0x04000DF8 RID: 3576
	private mainScript mS_;

	// Token: 0x04000DF9 RID: 3577
	private GameObject main_;

	// Token: 0x04000DFA RID: 3578
	private GUI_Main guiMain_;

	// Token: 0x04000DFB RID: 3579
	private sfxScript sfx_;

	// Token: 0x04000DFC RID: 3580
	private textScript tS_;

	// Token: 0x04000DFD RID: 3581
	private Menu_DevGame devGame_;

	// Token: 0x04000DFE RID: 3582
	private Menu_Dev_ChangeCopyProtect menuChangeCopyProtect_;

	// Token: 0x04000DFF RID: 3583
	private float updateTimer;
}
