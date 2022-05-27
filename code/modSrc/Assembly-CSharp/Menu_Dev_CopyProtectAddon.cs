using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000131 RID: 305
public class Menu_Dev_CopyProtectAddon : MonoBehaviour
{
	// Token: 0x06000ADB RID: 2779 RVA: 0x00007C2D File Offset: 0x00005E2D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00086ABC File Offset: 0x00084CBC
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
		if (!this.devAddon_)
		{
			this.devAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.devMMOAddon_)
		{
			this.devMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00007C35 File Offset: 0x00005E35
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00086BB8 File Offset: 0x00084DB8
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

	// Token: 0x06000ADF RID: 2783 RVA: 0x00086C04 File Offset: 0x00084E04
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Dev_CopyProtectAddon>().cpS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00007C6D File Offset: 0x00005E6D
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00086C60 File Offset: 0x00084E60
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

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00086D00 File Offset: 0x00084F00
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			if (this.devAddon_.g_GameCopyProtect != -1)
			{
				this.uiObjects[4].GetComponent<Button>().interactable = true;
			}
			else
			{
				this.uiObjects[4].GetComponent<Button>().interactable = false;
			}
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			if (this.devMMOAddon_.g_GameCopyProtect != -1)
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

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00086DF0 File Offset: 0x00084FF0
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
					Item_Dev_CopyProtectAddon component2 = gameObject.GetComponent<Item_Dev_CopyProtectAddon>();
					component2.cpS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					if (this.guiMain_.uiObjects[193].activeSelf && this.devAddon_.g_GameCopyProtect == component.myID)
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
					if (this.guiMain_.uiObjects[247].activeSelf && this.devMMOAddon_.g_GameCopyProtect == component.myID)
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00086F98 File Offset: 0x00085198
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
				Item_Dev_CopyProtectAddon component = gameObject.GetComponent<Item_Dev_CopyProtectAddon>();
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

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00007C81 File Offset: 0x00005E81
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00007C9C File Offset: 0x00005E9C
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x000870A0 File Offset: 0x000852A0
	public void BUTTON_CopyProtectEntfernen()
	{
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.devAddon_.SetCopyProtect(-1);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.devMMOAddon_.SetCopyProtect(-1);
		}
		this.BUTTON_Close();
	}

	// Token: 0x04000F34 RID: 3892
	public GameObject[] uiPrefabs;

	// Token: 0x04000F35 RID: 3893
	public GameObject[] uiObjects;

	// Token: 0x04000F36 RID: 3894
	private mainScript mS_;

	// Token: 0x04000F37 RID: 3895
	private GameObject main_;

	// Token: 0x04000F38 RID: 3896
	private GUI_Main guiMain_;

	// Token: 0x04000F39 RID: 3897
	private sfxScript sfx_;

	// Token: 0x04000F3A RID: 3898
	private textScript tS_;

	// Token: 0x04000F3B RID: 3899
	private Menu_Dev_AddonDo devAddon_;

	// Token: 0x04000F3C RID: 3900
	private Menu_Dev_MMOAddon devMMOAddon_;

	// Token: 0x04000F3D RID: 3901
	private float updateTimer;
}
