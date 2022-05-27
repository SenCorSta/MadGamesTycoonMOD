using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001A9 RID: 425
public class Menu_Marketing_SelectGame : MonoBehaviour
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x0000B4ED File Offset: 0x000096ED
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x000B6B28 File Offset: 0x000B4D28
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

	// Token: 0x06000FFC RID: 4092 RVA: 0x0000B4F5 File Offset: 0x000096F5
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x000B6BF0 File Offset: 0x000B4DF0
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

	// Token: 0x06000FFE RID: 4094 RVA: 0x000B6C3C File Offset: 0x000B4E3C
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

	// Token: 0x06000FFF RID: 4095 RVA: 0x0000B52D File Offset: 0x0000972D
	private void OnEnable()
	{
		this.InitDropdowns();
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000B6C98 File Offset: 0x000B4E98
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

	// Token: 0x06001001 RID: 4097 RVA: 0x0000B535 File Offset: 0x00009735
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x000B6D40 File Offset: 0x000B4F40
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.playerGame && (component.inDevelopment || component.isOnMarket || component.schublade) && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06001003 RID: 4099 RVA: 0x0000B543 File Offset: 0x00009743
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x000B6E64 File Offset: 0x000B5064
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

	// Token: 0x04001487 RID: 5255
	private mainScript mS_;

	// Token: 0x04001488 RID: 5256
	private GameObject main_;

	// Token: 0x04001489 RID: 5257
	private GUI_Main guiMain_;

	// Token: 0x0400148A RID: 5258
	private sfxScript sfx_;

	// Token: 0x0400148B RID: 5259
	private textScript tS_;

	// Token: 0x0400148C RID: 5260
	private genres genres_;

	// Token: 0x0400148D RID: 5261
	public GameObject[] uiPrefabs;

	// Token: 0x0400148E RID: 5262
	public GameObject[] uiObjects;

	// Token: 0x0400148F RID: 5263
	private float updateTimer;
}
