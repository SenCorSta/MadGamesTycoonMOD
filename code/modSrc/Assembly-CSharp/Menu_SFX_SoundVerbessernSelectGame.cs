using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000213 RID: 531
public class Menu_SFX_SoundVerbessernSelectGame : MonoBehaviour
{
	// Token: 0x0600145C RID: 5212 RVA: 0x0000DDE6 File Offset: 0x0000BFE6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600145D RID: 5213 RVA: 0x000DE0F0 File Offset: 0x000DC2F0
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

	// Token: 0x0600145E RID: 5214 RVA: 0x0000DDEE File Offset: 0x0000BFEE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x000DE1B8 File Offset: 0x000DC3B8
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

	// Token: 0x06001460 RID: 5216 RVA: 0x000DE204 File Offset: 0x000DC404
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

	// Token: 0x06001461 RID: 5217 RVA: 0x0000DE26 File Offset: 0x0000C026
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x000DE260 File Offset: 0x000DC460
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

	// Token: 0x06001463 RID: 5219 RVA: 0x0000DE2E File Offset: 0x0000C02E
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x000DE2F0 File Offset: 0x000DC4F0
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

	// Token: 0x06001465 RID: 5221 RVA: 0x0000DE3C File Offset: 0x0000C03C
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x000DE3F8 File Offset: 0x000DC5F8
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

	// Token: 0x04001880 RID: 6272
	private mainScript mS_;

	// Token: 0x04001881 RID: 6273
	private GameObject main_;

	// Token: 0x04001882 RID: 6274
	private GUI_Main guiMain_;

	// Token: 0x04001883 RID: 6275
	private sfxScript sfx_;

	// Token: 0x04001884 RID: 6276
	private textScript tS_;

	// Token: 0x04001885 RID: 6277
	private genres genres_;

	// Token: 0x04001886 RID: 6278
	public GameObject[] uiPrefabs;

	// Token: 0x04001887 RID: 6279
	public GameObject[] uiObjects;

	// Token: 0x04001888 RID: 6280
	private float updateTimer;
}
