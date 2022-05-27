using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000136 RID: 310
public class Menu_Dev_FanbriefeSelect : MonoBehaviour
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x00077EB7 File Offset: 0x000760B7
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00077EC0 File Offset: 0x000760C0
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

	// Token: 0x06000B1B RID: 2843 RVA: 0x00077F88 File Offset: 0x00076188
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00077FC0 File Offset: 0x000761C0
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

	// Token: 0x06000B1D RID: 2845 RVA: 0x0007800C File Offset: 0x0007620C
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

	// Token: 0x06000B1E RID: 2846 RVA: 0x00078068 File Offset: 0x00076268
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x0007807C File Offset: 0x0007627C
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

	// Token: 0x06000B20 RID: 2848 RVA: 0x00078138 File Offset: 0x00076338
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00078148 File Offset: 0x00076348
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component))
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

	// Token: 0x06000B22 RID: 2850 RVA: 0x0007829C File Offset: 0x0007649C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && !script_.archiv_fanbriefe && !script_.typ_addon && !script_.typ_budget && !script_.typ_mmoaddon && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.inDevelopment && script_.GetAmountFanbriefe() > 0;
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00078323 File Offset: 0x00076523
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00078340 File Offset: 0x00076540
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

	// Token: 0x06000B25 RID: 2853 RVA: 0x00078490 File Offset: 0x00076690
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

	// Token: 0x04000F6F RID: 3951
	private mainScript mS_;

	// Token: 0x04000F70 RID: 3952
	private GameObject main_;

	// Token: 0x04000F71 RID: 3953
	private GUI_Main guiMain_;

	// Token: 0x04000F72 RID: 3954
	private sfxScript sfx_;

	// Token: 0x04000F73 RID: 3955
	private textScript tS_;

	// Token: 0x04000F74 RID: 3956
	private genres genres_;

	// Token: 0x04000F75 RID: 3957
	public GameObject[] uiPrefabs;

	// Token: 0x04000F76 RID: 3958
	public GameObject[] uiObjects;

	// Token: 0x04000F77 RID: 3959
	private float updateTimer;

	// Token: 0x04000F78 RID: 3960
	private string searchStringA = "";
}
