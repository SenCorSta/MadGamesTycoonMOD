using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000135 RID: 309
public class Menu_Dev_FanbriefeSelect : MonoBehaviour
{
	// Token: 0x06000B07 RID: 2823 RVA: 0x00007DBA File Offset: 0x00005FBA
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00087FF4 File Offset: 0x000861F4
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

	// Token: 0x06000B09 RID: 2825 RVA: 0x00007DC2 File Offset: 0x00005FC2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x000880BC File Offset: 0x000862BC
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

	// Token: 0x06000B0B RID: 2827 RVA: 0x00088108 File Offset: 0x00086308
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Fanbrief>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00007DFA File Offset: 0x00005FFA
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00088164 File Offset: 0x00086364
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(407));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(273));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00007E0E File Offset: 0x0000600E
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00088220 File Offset: 0x00086420
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && !component.archiv_fanbriefe && component.playerGame && !component.typ_addon && !component.typ_budget && !component.typ_mmoaddon && !component.typ_bundle && !component.typ_bundleAddon && !component.typ_goty && !component.inDevelopment && component.GetAmountFanbriefe() > 0)
				{
					string text = component.GetNameSimple();
					this.searchStringA = this.searchStringA.ToLower();
					text = text.ToLower();
					if ((this.uiObjects[6].GetComponent<InputField>().text.Length <= 0 || text.Contains(this.searchStringA)) && !this.Exists(this.uiObjects[0], component.myID))
					{
						Item_Fanbrief component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Fanbrief>();
						component2.mS_ = this.mS_;
						component2.tS_ = this.tS_;
						component2.sfx_ = this.sfx_;
						component2.guiMain_ = this.guiMain_;
						component2.genres_ = this.genres_;
						component2.game_ = component;
					}
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00007E1C File Offset: 0x0000601C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x000883D8 File Offset: 0x000865D8
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
				Item_Fanbrief component = gameObject.GetComponent<Item_Fanbrief>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.GetAmountFanbriefe().ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.game_.maingenre.ToString();
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

	// Token: 0x06000B12 RID: 2834 RVA: 0x00088528 File Offset: 0x00086728
	public void BUTTON_Search()
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.searchStringA = this.uiObjects[6].GetComponent<InputField>().text;
		this.Init();
	}

	// Token: 0x04000F67 RID: 3943
	private mainScript mS_;

	// Token: 0x04000F68 RID: 3944
	private GameObject main_;

	// Token: 0x04000F69 RID: 3945
	private GUI_Main guiMain_;

	// Token: 0x04000F6A RID: 3946
	private sfxScript sfx_;

	// Token: 0x04000F6B RID: 3947
	private textScript tS_;

	// Token: 0x04000F6C RID: 3948
	private genres genres_;

	// Token: 0x04000F6D RID: 3949
	public GameObject[] uiPrefabs;

	// Token: 0x04000F6E RID: 3950
	public GameObject[] uiObjects;

	// Token: 0x04000F6F RID: 3951
	private float updateTimer;

	// Token: 0x04000F70 RID: 3952
	private string searchStringA = "";
}
