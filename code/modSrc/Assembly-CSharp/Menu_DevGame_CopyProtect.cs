using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200011A RID: 282
public class Menu_DevGame_CopyProtect : MonoBehaviour
{
	// Token: 0x060009AF RID: 2479 RVA: 0x00006FF6 File Offset: 0x000051F6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0007C3EC File Offset: 0x0007A5EC
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

	// Token: 0x060009B1 RID: 2481 RVA: 0x00006FFE File Offset: 0x000051FE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0007C4E8 File Offset: 0x0007A6E8
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

	// Token: 0x060009B3 RID: 2483 RVA: 0x0007C534 File Offset: 0x0007A734
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

	// Token: 0x060009B4 RID: 2484 RVA: 0x00007036 File Offset: 0x00005236
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x0007C590 File Offset: 0x0007A790
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

	// Token: 0x060009B6 RID: 2486 RVA: 0x0007C630 File Offset: 0x0007A830
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

	// Token: 0x060009B7 RID: 2487 RVA: 0x0007C720 File Offset: 0x0007A920
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

	// Token: 0x060009B8 RID: 2488 RVA: 0x0007C878 File Offset: 0x0007AA78
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

	// Token: 0x060009B9 RID: 2489 RVA: 0x0000704A File Offset: 0x0000524A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00007065 File Offset: 0x00005265
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0007C980 File Offset: 0x0007AB80
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

	// Token: 0x04000E09 RID: 3593
	public GameObject[] uiPrefabs;

	// Token: 0x04000E0A RID: 3594
	public GameObject[] uiObjects;

	// Token: 0x04000E0B RID: 3595
	private mainScript mS_;

	// Token: 0x04000E0C RID: 3596
	private GameObject main_;

	// Token: 0x04000E0D RID: 3597
	private GUI_Main guiMain_;

	// Token: 0x04000E0E RID: 3598
	private sfxScript sfx_;

	// Token: 0x04000E0F RID: 3599
	private textScript tS_;

	// Token: 0x04000E10 RID: 3600
	private Menu_DevGame devGame_;

	// Token: 0x04000E11 RID: 3601
	private Menu_Dev_ChangeCopyProtect menuChangeCopyProtect_;

	// Token: 0x04000E12 RID: 3602
	private float updateTimer;
}
