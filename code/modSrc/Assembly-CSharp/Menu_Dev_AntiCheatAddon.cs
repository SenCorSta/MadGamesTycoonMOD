using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000129 RID: 297
public class Menu_Dev_AntiCheatAddon : MonoBehaviour
{
	// Token: 0x06000A7E RID: 2686 RVA: 0x00007853 File Offset: 0x00005A53
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x000833EC File Offset: 0x000815EC
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

	// Token: 0x06000A80 RID: 2688 RVA: 0x0000785B File Offset: 0x00005A5B
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x000834E8 File Offset: 0x000816E8
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

	// Token: 0x06000A82 RID: 2690 RVA: 0x00083534 File Offset: 0x00081734
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Dev_AntiCheatAddon>().acS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00007893 File Offset: 0x00005A93
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00083590 File Offset: 0x00081790
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

	// Token: 0x06000A85 RID: 2693 RVA: 0x00083630 File Offset: 0x00081830
	private void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			if (this.devAddon_.g_GameAntiCheat != -1)
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
			if (this.devMMOAddon_.g_GameAntiCheat != -1)
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

	// Token: 0x06000A86 RID: 2694 RVA: 0x00083720 File Offset: 0x00081920
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
					Item_Dev_AntiCheatAddon component2 = gameObject.GetComponent<Item_Dev_AntiCheatAddon>();
					component2.acS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					if (this.guiMain_.uiObjects[193].activeSelf && this.devAddon_.g_GameAntiCheat == component.myID)
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
					if (this.guiMain_.uiObjects[247].activeSelf && this.devMMOAddon_.g_GameAntiCheat == component.myID)
					{
						gameObject.GetComponent<Button>().interactable = false;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[6]);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x000838C8 File Offset: 0x00081AC8
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
				Item_Dev_AntiCheatAddon component = gameObject.GetComponent<Item_Dev_AntiCheatAddon>();
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

	// Token: 0x06000A88 RID: 2696 RVA: 0x000078A7 File Offset: 0x00005AA7
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x000078C2 File Offset: 0x00005AC2
	public void TOGGLE_Veraltet()
	{
		this.Init();
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x000839D0 File Offset: 0x00081BD0
	public void BUTTON_AntiCheatEntfernen()
	{
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.devAddon_.SetAntiCheat(-1);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.devMMOAddon_.SetAntiCheat(-1);
		}
		this.BUTTON_Close();
	}

	// Token: 0x04000EB9 RID: 3769
	public GameObject[] uiPrefabs;

	// Token: 0x04000EBA RID: 3770
	public GameObject[] uiObjects;

	// Token: 0x04000EBB RID: 3771
	private mainScript mS_;

	// Token: 0x04000EBC RID: 3772
	private GameObject main_;

	// Token: 0x04000EBD RID: 3773
	private GUI_Main guiMain_;

	// Token: 0x04000EBE RID: 3774
	private sfxScript sfx_;

	// Token: 0x04000EBF RID: 3775
	private textScript tS_;

	// Token: 0x04000EC0 RID: 3776
	private Menu_Dev_AddonDo devAddon_;

	// Token: 0x04000EC1 RID: 3777
	private Menu_Dev_MMOAddon devMMOAddon_;

	// Token: 0x04000EC2 RID: 3778
	private float updateTimer;
}
