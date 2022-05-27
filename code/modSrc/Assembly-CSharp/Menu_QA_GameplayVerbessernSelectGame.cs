using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020A RID: 522
public class Menu_QA_GameplayVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x06001401 RID: 5121 RVA: 0x000D0E16 File Offset: 0x000CF016
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x000D0E20 File Offset: 0x000CF020
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

	// Token: 0x06001403 RID: 5123 RVA: 0x000D0EE8 File Offset: 0x000CF0E8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x000D0F20 File Offset: 0x000CF120
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

	// Token: 0x06001405 RID: 5125 RVA: 0x000D0F6C File Offset: 0x000CF16C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_QA_GameplayVerbessern>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001406 RID: 5126 RVA: 0x000D0FC8 File Offset: 0x000CF1C8
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001407 RID: 5127 RVA: 0x000D0FD0 File Offset: 0x000CF1D0
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(273));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001408 RID: 5128 RVA: 0x000D1060 File Offset: 0x000CF260
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001409 RID: 5129 RVA: 0x000D1070 File Offset: 0x000CF270
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && this.CheckGameData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_QA_GameplayVerbessern component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_QA_GameplayVerbessern>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600140A RID: 5130 RVA: 0x000D116E File Offset: 0x000CF36E
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.inDevelopment;
	}

	// Token: 0x0600140B RID: 5131 RVA: 0x000D1196 File Offset: 0x000CF396
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600140C RID: 5132 RVA: 0x000D11B4 File Offset: 0x000CF3B4
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
				Item_QA_GameplayVerbessern component = gameObject.GetComponent<Item_QA_GameplayVerbessern>();
				if (value != 0)
				{
					if (value == 1)
					{
						gameObject.name = component.game_.maingenre.ToString();
					}
				}
				else
				{
					gameObject.name = component.game_.GetNameSimple();
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

	// Token: 0x0400181F RID: 6175
	private mainScript mS_;

	// Token: 0x04001820 RID: 6176
	private GameObject main_;

	// Token: 0x04001821 RID: 6177
	private GUI_Main guiMain_;

	// Token: 0x04001822 RID: 6178
	private sfxScript sfx_;

	// Token: 0x04001823 RID: 6179
	private textScript tS_;

	// Token: 0x04001824 RID: 6180
	private genres genres_;

	// Token: 0x04001825 RID: 6181
	public GameObject[] uiPrefabs;

	// Token: 0x04001826 RID: 6182
	public GameObject[] uiObjects;

	// Token: 0x04001827 RID: 6183
	private float updateTimer;
}
