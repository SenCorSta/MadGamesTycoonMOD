using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011B RID: 283
public class Menu_DevGame_CopyProtect : MonoBehaviour
{
	// Token: 0x060009BE RID: 2494 RVA: 0x0006B46B File Offset: 0x0006966B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0006B474 File Offset: 0x00069674
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

	// Token: 0x060009C0 RID: 2496 RVA: 0x0006B56D File Offset: 0x0006976D
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0006B5A8 File Offset: 0x000697A8
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

	// Token: 0x060009C2 RID: 2498 RVA: 0x0006B5F4 File Offset: 0x000697F4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_CopyProtect>().cpS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0006B650 File Offset: 0x00069850
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0006B664 File Offset: 0x00069864
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

	// Token: 0x060009C5 RID: 2501 RVA: 0x0006B704 File Offset: 0x00069904
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			if (this.devGame_.g_GameCopyProtect != -1)
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
			if (this.menuChangeCopyProtect_.g_GameCopyProtect != -1)
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

	// Token: 0x060009C6 RID: 2502 RVA: 0x0006B7F4 File Offset: 0x000699F4
	private void SetData()
	{
		bool isOn = this.uiObjects[5].GetComponent<Toggle>().isOn;
		GameObject[] array = GameObject.FindGameObjectsWithTag("CopyProtect");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				copyProtectScript component = array[i].GetComponent<copyProtectScript>();
				if (component && component.isUnlocked && component.inBesitz && (component.effekt > 0f || !isOn) && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_DevGame_CopyProtect component2 = gameObject.GetComponent<Item_DevGame_CopyProtect>();
					component2.cpS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					if (this.devGame_.g_GameCopyProtect == component.myID)
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0006B94C File Offset: 0x00069B4C
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
				Item_DevGame_CopyProtect component = gameObject.GetComponent<Item_DevGame_CopyProtect>();
				switch (value)
				{
				case 0:
					gameObject.name = component.cpS_.GetName();
					break;
				case 1:
					gameObject.name = component.cpS_.GetPrice().ToString();
					break;
				case 2:
					gameObject.name = component.cpS_.effekt.ToString();
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

	// Token: 0x060009C8 RID: 2504 RVA: 0x0006BA53 File Offset: 0x00069C53
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0006BA6E File Offset: 0x00069C6E
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x0006BA78 File Offset: 0x00069C78
	public void BUTTON_CopyProtectEntfernen()
	{
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.devGame_.SetCopyProtect(-1);
		}
		if (this.guiMain_.uiObjects[365].activeSelf)
		{
			this.menuChangeCopyProtect_.SetCopyProtect(-1);
		}
		this.BUTTON_Close();
	}

	// Token: 0x04000E11 RID: 3601
	public GameObject[] uiPrefabs;

	// Token: 0x04000E12 RID: 3602
	public GameObject[] uiObjects;

	// Token: 0x04000E13 RID: 3603
	private mainScript mS_;

	// Token: 0x04000E14 RID: 3604
	private GameObject main_;

	// Token: 0x04000E15 RID: 3605
	private GUI_Main guiMain_;

	// Token: 0x04000E16 RID: 3606
	private sfxScript sfx_;

	// Token: 0x04000E17 RID: 3607
	private textScript tS_;

	// Token: 0x04000E18 RID: 3608
	private Menu_DevGame devGame_;

	// Token: 0x04000E19 RID: 3609
	private Menu_Dev_ChangeCopyProtect menuChangeCopyProtect_;

	// Token: 0x04000E1A RID: 3610
	private float updateTimer;
}
