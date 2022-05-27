using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015B RID: 347
public class Menu_GFX_GrafikVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x06000CD1 RID: 3281 RVA: 0x0008C4A1 File Offset: 0x0008A6A1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0008C4AC File Offset: 0x0008A6AC
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

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0008C574 File Offset: 0x0008A774
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0008C5AC File Offset: 0x0008A7AC
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

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0008C5F8 File Offset: 0x0008A7F8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_GFX_GrafikVerbessern>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0008C654 File Offset: 0x0008A854
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0008C65C File Offset: 0x0008A85C
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

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0008C6EC File Offset: 0x0008A8EC
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0008C6FC File Offset: 0x0008A8FC
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
					Item_GFX_GrafikVerbessern component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_GFX_GrafikVerbessern>();
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

	// Token: 0x06000CDA RID: 3290 RVA: 0x0008C7FA File Offset: 0x0008A9FA
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.inDevelopment;
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0008C822 File Offset: 0x0008AA22
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0008C840 File Offset: 0x0008AA40
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
				Item_GFX_GrafikVerbessern component = gameObject.GetComponent<Item_GFX_GrafikVerbessern>();
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

	// Token: 0x04001156 RID: 4438
	private mainScript mS_;

	// Token: 0x04001157 RID: 4439
	private GameObject main_;

	// Token: 0x04001158 RID: 4440
	private GUI_Main guiMain_;

	// Token: 0x04001159 RID: 4441
	private sfxScript sfx_;

	// Token: 0x0400115A RID: 4442
	private textScript tS_;

	// Token: 0x0400115B RID: 4443
	private genres genres_;

	// Token: 0x0400115C RID: 4444
	public GameObject[] uiPrefabs;

	// Token: 0x0400115D RID: 4445
	public GameObject[] uiObjects;

	// Token: 0x0400115E RID: 4446
	private float updateTimer;
}
