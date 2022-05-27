using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000214 RID: 532
public class Menu_SFX_SoundVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x06001479 RID: 5241 RVA: 0x000D4931 File Offset: 0x000D2B31
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000D493C File Offset: 0x000D2B3C
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

	// Token: 0x0600147B RID: 5243 RVA: 0x000D4A04 File Offset: 0x000D2C04
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000D4A3C File Offset: 0x000D2C3C
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

	// Token: 0x0600147D RID: 5245 RVA: 0x000D4A88 File Offset: 0x000D2C88
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_SFX_SoundVerbessern>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x000D4AE4 File Offset: 0x000D2CE4
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000D4AEC File Offset: 0x000D2CEC
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

	// Token: 0x06001480 RID: 5248 RVA: 0x000D4B7C File Offset: 0x000D2D7C
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x000D4B8C File Offset: 0x000D2D8C
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
					Item_SFX_SoundVerbessern component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_SFX_SoundVerbessern>();
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

	// Token: 0x06001482 RID: 5250 RVA: 0x000D4C8A File Offset: 0x000D2E8A
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.developerID == this.mS_.myID && script_.inDevelopment;
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x000D4CB2 File Offset: 0x000D2EB2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000D4CD0 File Offset: 0x000D2ED0
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
				Item_SFX_SoundVerbessern component = gameObject.GetComponent<Item_SFX_SoundVerbessern>();
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

	// Token: 0x04001889 RID: 6281
	private mainScript mS_;

	// Token: 0x0400188A RID: 6282
	private GameObject main_;

	// Token: 0x0400188B RID: 6283
	private GUI_Main guiMain_;

	// Token: 0x0400188C RID: 6284
	private sfxScript sfx_;

	// Token: 0x0400188D RID: 6285
	private textScript tS_;

	// Token: 0x0400188E RID: 6286
	private genres genres_;

	// Token: 0x0400188F RID: 6287
	public GameObject[] uiPrefabs;

	// Token: 0x04001890 RID: 6288
	public GameObject[] uiObjects;

	// Token: 0x04001891 RID: 6289
	private float updateTimer;
}
