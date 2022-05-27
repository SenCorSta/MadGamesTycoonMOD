using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000132 RID: 306
public class Menu_Dev_CopyProtectAddon : MonoBehaviour
{
	// Token: 0x06000AEC RID: 2796 RVA: 0x000767B8 File Offset: 0x000749B8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x000767C0 File Offset: 0x000749C0
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

	// Token: 0x06000AEE RID: 2798 RVA: 0x000768BC File Offset: 0x00074ABC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x000768F4 File Offset: 0x00074AF4
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

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00076940 File Offset: 0x00074B40
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

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0007699C File Offset: 0x00074B9C
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x000769B0 File Offset: 0x00074BB0
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

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00076A50 File Offset: 0x00074C50
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

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00076B40 File Offset: 0x00074D40
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

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00076CE8 File Offset: 0x00074EE8
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

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00076DEF File Offset: 0x00074FEF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x00076E0A File Offset: 0x0007500A
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00076E14 File Offset: 0x00075014
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

	// Token: 0x04000F3C RID: 3900
	public GameObject[] uiPrefabs;

	// Token: 0x04000F3D RID: 3901
	public GameObject[] uiObjects;

	// Token: 0x04000F3E RID: 3902
	private mainScript mS_;

	// Token: 0x04000F3F RID: 3903
	private GameObject main_;

	// Token: 0x04000F40 RID: 3904
	private GUI_Main guiMain_;

	// Token: 0x04000F41 RID: 3905
	private sfxScript sfx_;

	// Token: 0x04000F42 RID: 3906
	private textScript tS_;

	// Token: 0x04000F43 RID: 3907
	private Menu_Dev_AddonDo devAddon_;

	// Token: 0x04000F44 RID: 3908
	private Menu_Dev_MMOAddon devMMOAddon_;

	// Token: 0x04000F45 RID: 3909
	private float updateTimer;
}
