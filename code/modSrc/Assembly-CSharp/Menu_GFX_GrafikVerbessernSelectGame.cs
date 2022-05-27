using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200015A RID: 346
public class Menu_GFX_GrafikVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x06000CBB RID: 3259 RVA: 0x00008E90 File Offset: 0x00007090
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x0009B368 File Offset: 0x00099568
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

	// Token: 0x06000CBD RID: 3261 RVA: 0x00008E98 File Offset: 0x00007098
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x0009B430 File Offset: 0x00099630
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

	// Token: 0x06000CBF RID: 3263 RVA: 0x0009B47C File Offset: 0x0009967C
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

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00008ED0 File Offset: 0x000070D0
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0009B4D8 File Offset: 0x000996D8
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

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00008ED8 File Offset: 0x000070D8
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0009B568 File Offset: 0x00099768
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && component.inDevelopment && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00008EE6 File Offset: 0x000070E6
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0009B670 File Offset: 0x00099870
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

	// Token: 0x0400114E RID: 4430
	private mainScript mS_;

	// Token: 0x0400114F RID: 4431
	private GameObject main_;

	// Token: 0x04001150 RID: 4432
	private GUI_Main guiMain_;

	// Token: 0x04001151 RID: 4433
	private sfxScript sfx_;

	// Token: 0x04001152 RID: 4434
	private textScript tS_;

	// Token: 0x04001153 RID: 4435
	private genres genres_;

	// Token: 0x04001154 RID: 4436
	public GameObject[] uiPrefabs;

	// Token: 0x04001155 RID: 4437
	public GameObject[] uiObjects;

	// Token: 0x04001156 RID: 4438
	private float updateTimer;
}
