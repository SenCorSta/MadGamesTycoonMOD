using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000148 RID: 328
public class Menu_Dev_ShowConceptSelect : MonoBehaviour
{
	// Token: 0x06000BFF RID: 3071 RVA: 0x00081791 File Offset: 0x0007F991
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0008179C File Offset: 0x0007F99C
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

	// Token: 0x06000C01 RID: 3073 RVA: 0x00081864 File Offset: 0x0007FA64
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0008189C File Offset: 0x0007FA9C
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

	// Token: 0x06000C03 RID: 3075 RVA: 0x000818E8 File Offset: 0x0007FAE8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevGame_ShowConcept>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x00081944 File Offset: 0x0007FB44
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00081958 File Offset: 0x0007FB58
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(275));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(1295));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00081A40 File Offset: 0x0007FC40
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00081A94 File Offset: 0x0007FC94
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
						Item_DevGame_ShowConcept component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevGame_ShowConcept>();
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

	// Token: 0x06000C08 RID: 3080 RVA: 0x00081BE8 File Offset: 0x0007FDE8
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.developerID == this.mS_.myID || script_.IsMyAuftragsspiel()) && !script_.archiv_spielkonzept && !script_.typ_budget && !script_.typ_bundle && !script_.typ_bundleAddon && !script_.typ_goty && !script_.pubOffer;
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00081C4B File Offset: 0x0007FE4B
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00081C68 File Offset: 0x0007FE68
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
				Item_DevGame_ShowConcept component = gameObject.GetComponent<Item_DevGame_ShowConcept>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment || component.game_.schublade)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 3:
					gameObject.name = component.game_.sellsTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 5:
					if (component.game_.spielbericht_favorit)
					{
						gameObject.name = "1";
					}
					else
					{
						gameObject.name = "0";
					}
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

	// Token: 0x06000C0B RID: 3083 RVA: 0x00081E2C File Offset: 0x0008002C
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

	// Token: 0x04001048 RID: 4168
	private mainScript mS_;

	// Token: 0x04001049 RID: 4169
	private GameObject main_;

	// Token: 0x0400104A RID: 4170
	private GUI_Main guiMain_;

	// Token: 0x0400104B RID: 4171
	private sfxScript sfx_;

	// Token: 0x0400104C RID: 4172
	private textScript tS_;

	// Token: 0x0400104D RID: 4173
	private genres genres_;

	// Token: 0x0400104E RID: 4174
	public GameObject[] uiPrefabs;

	// Token: 0x0400104F RID: 4175
	public GameObject[] uiObjects;

	// Token: 0x04001050 RID: 4176
	private float updateTimer;

	// Token: 0x04001051 RID: 4177
	private string searchStringA = "";
}
