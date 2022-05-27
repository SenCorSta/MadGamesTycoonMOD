using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000209 RID: 521
public class Menu_QA_GameplayVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x060013E5 RID: 5093 RVA: 0x0000D95F File Offset: 0x0000BB5F
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x000DAB40 File Offset: 0x000D8D40
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

	// Token: 0x060013E7 RID: 5095 RVA: 0x0000D967 File Offset: 0x0000BB67
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x000DAC08 File Offset: 0x000D8E08
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

	// Token: 0x060013E9 RID: 5097 RVA: 0x000DAC54 File Offset: 0x000D8E54
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

	// Token: 0x060013EA RID: 5098 RVA: 0x0000D99F File Offset: 0x0000BB9F
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x000DACB0 File Offset: 0x000D8EB0
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

	// Token: 0x060013EC RID: 5100 RVA: 0x0000D9A7 File Offset: 0x0000BBA7
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x000DAD40 File Offset: 0x000D8F40
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

	// Token: 0x060013EE RID: 5102 RVA: 0x0000D9B5 File Offset: 0x0000BBB5
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x000DAE48 File Offset: 0x000D9048
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

	// Token: 0x04001816 RID: 6166
	private mainScript mS_;

	// Token: 0x04001817 RID: 6167
	private GameObject main_;

	// Token: 0x04001818 RID: 6168
	private GUI_Main guiMain_;

	// Token: 0x04001819 RID: 6169
	private sfxScript sfx_;

	// Token: 0x0400181A RID: 6170
	private textScript tS_;

	// Token: 0x0400181B RID: 6171
	private genres genres_;

	// Token: 0x0400181C RID: 6172
	public GameObject[] uiPrefabs;

	// Token: 0x0400181D RID: 6173
	public GameObject[] uiObjects;

	// Token: 0x0400181E RID: 6174
	private float updateTimer;
}
