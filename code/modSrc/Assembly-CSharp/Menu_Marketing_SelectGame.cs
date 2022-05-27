using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001AA RID: 426
public class Menu_Marketing_SelectGame : MonoBehaviour
{
	// Token: 0x06001012 RID: 4114 RVA: 0x000AA573 File Offset: 0x000A8773
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x000AA57C File Offset: 0x000A877C
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

	// Token: 0x06001014 RID: 4116 RVA: 0x000AA644 File Offset: 0x000A8844
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x000AA67C File Offset: 0x000A887C
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

	// Token: 0x06001016 RID: 4118 RVA: 0x000AA6C8 File Offset: 0x000A88C8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Marketing_Game>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x000AA724 File Offset: 0x000A8924
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x000AA72C File Offset: 0x000A892C
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(433));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x000AA7D2 File Offset: 0x000A89D2
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x000AA7E0 File Offset: 0x000A89E0
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
					Item_Marketing_Game component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Marketing_Game>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x000AA8EC File Offset: 0x000A8AEC
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.developerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && (script_.inDevelopment || script_.isOnMarket || script_.schublade) && !script_.typ_contractGame;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000AA94A File Offset: 0x000A8B4A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000AA968 File Offset: 0x000A8B68
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
				Item_Marketing_Game component = gameObject.GetComponent<Item_Marketing_Game>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.GetHype().ToString();
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

	// Token: 0x04001490 RID: 5264
	private mainScript mS_;

	// Token: 0x04001491 RID: 5265
	private GameObject main_;

	// Token: 0x04001492 RID: 5266
	private GUI_Main guiMain_;

	// Token: 0x04001493 RID: 5267
	private sfxScript sfx_;

	// Token: 0x04001494 RID: 5268
	private textScript tS_;

	// Token: 0x04001495 RID: 5269
	private genres genres_;

	// Token: 0x04001496 RID: 5270
	public GameObject[] uiPrefabs;

	// Token: 0x04001497 RID: 5271
	public GameObject[] uiObjects;

	// Token: 0x04001498 RID: 5272
	private float updateTimer;
}
