﻿using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000246 RID: 582
public class Menu_Stats_MyGames_F2PDownloads : MonoBehaviour
{
	// Token: 0x0600167F RID: 5759 RVA: 0x000E3E16 File Offset: 0x000E2016
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x000E3E20 File Offset: 0x000E2020
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

	// Token: 0x06001681 RID: 5761 RVA: 0x000E3EE8 File Offset: 0x000E20E8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x000E3F20 File Offset: 0x000E2120
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

	// Token: 0x06001683 RID: 5763 RVA: 0x000E3F6C File Offset: 0x000E216C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_MyGames_Sells>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x000E3FC8 File Offset: 0x000E21C8
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x000E3FD0 File Offset: 0x000E21D0
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x000E3FE0 File Offset: 0x000E21E0
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
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_MyGames_Sells component2 = gameObject.GetComponent<Item_MyGames_Sells>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					gameObject.name = component.sellsTotal.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x000E4158 File Offset: 0x000E2358
	public bool CheckGameData(gameScript script_)
	{
		if (script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID))
		{
			if (this.uiObjects[6].GetComponent<Toggle>().isOn && script_.developerID != this.mS_.myID)
			{
				return false;
			}
			if (!script_.inDevelopment && !script_.schublade && script_.gameTyp == 2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x000E41D8 File Offset: 0x000E23D8
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x000E41F4 File Offset: 0x000E23F4
	public void TOGGLE_OnlyMyGames()
	{
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x04001A69 RID: 6761
	private mainScript mS_;

	// Token: 0x04001A6A RID: 6762
	private GameObject main_;

	// Token: 0x04001A6B RID: 6763
	private GUI_Main guiMain_;

	// Token: 0x04001A6C RID: 6764
	private sfxScript sfx_;

	// Token: 0x04001A6D RID: 6765
	private textScript tS_;

	// Token: 0x04001A6E RID: 6766
	private genres genres_;

	// Token: 0x04001A6F RID: 6767
	public GameObject[] uiPrefabs;

	// Token: 0x04001A70 RID: 6768
	public GameObject[] uiObjects;

	// Token: 0x04001A71 RID: 6769
	private float updateTimer;
}
