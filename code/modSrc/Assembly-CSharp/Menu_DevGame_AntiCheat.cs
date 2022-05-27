using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000118 RID: 280
public class Menu_DevGame_AntiCheat : MonoBehaviour
{
	// Token: 0x0600098F RID: 2447 RVA: 0x00006EDE File Offset: 0x000050DE
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0007AA68 File Offset: 0x00078C68
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

	// Token: 0x06000991 RID: 2449 RVA: 0x00006EE6 File Offset: 0x000050E6
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0007AB64 File Offset: 0x00078D64
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

	// Token: 0x06000993 RID: 2451 RVA: 0x0007ABB0 File Offset: 0x00078DB0
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

	// Token: 0x06000994 RID: 2452 RVA: 0x00006F1E File Offset: 0x0000511E
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x0007AC0C File Offset: 0x00078E0C
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

	// Token: 0x06000996 RID: 2454 RVA: 0x0007ACAC File Offset: 0x00078EAC
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

	// Token: 0x06000997 RID: 2455 RVA: 0x0007AD9C File Offset: 0x00078F9C
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

	// Token: 0x06000998 RID: 2456 RVA: 0x0007AEF4 File Offset: 0x000790F4
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

	// Token: 0x06000999 RID: 2457 RVA: 0x00006F32 File Offset: 0x00005132
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00006F4D File Offset: 0x0000514D
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0007AFFC File Offset: 0x000791FC
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

	// Token: 0x04000DEE RID: 3566
	public GameObject[] uiPrefabs;

	// Token: 0x04000DEF RID: 3567
	public GameObject[] uiObjects;

	// Token: 0x04000DF0 RID: 3568
	private mainScript mS_;

	// Token: 0x04000DF1 RID: 3569
	private GameObject main_;

	// Token: 0x04000DF2 RID: 3570
	private GUI_Main guiMain_;

	// Token: 0x04000DF3 RID: 3571
	private sfxScript sfx_;

	// Token: 0x04000DF4 RID: 3572
	private textScript tS_;

	// Token: 0x04000DF5 RID: 3573
	private Menu_DevGame devGame_;

	// Token: 0x04000DF6 RID: 3574
	private Menu_Dev_ChangeCopyProtect menuChangeCopyProtect_;

	// Token: 0x04000DF7 RID: 3575
	private float updateTimer;
}
