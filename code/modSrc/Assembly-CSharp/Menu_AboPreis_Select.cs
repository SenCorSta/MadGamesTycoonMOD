using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020F RID: 527
public class Menu_AboPreis_Select : MonoBehaviour
{
	// Token: 0x0600143F RID: 5183 RVA: 0x000D2DE1 File Offset: 0x000D0FE1
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x000D2DEC File Offset: 0x000D0FEC
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

	// Token: 0x06001441 RID: 5185 RVA: 0x000D2EB4 File Offset: 0x000D10B4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x000D2EEC File Offset: 0x000D10EC
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

	// Token: 0x06001443 RID: 5187 RVA: 0x000D2F38 File Offset: 0x000D1138
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_AboPreis>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x000D2F94 File Offset: 0x000D1194
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x000D2FA8 File Offset: 0x000D11A8
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(1236));
		list.Add(this.tS_.GetText(1229));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x000D3048 File Offset: 0x000D1248
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000D309C File Offset: 0x000D129C
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
					Item_AboPreis component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_AboPreis>();
					component2.game_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x000D31A8 File Offset: 0x000D13A8
	public bool CheckGameData(gameScript script_)
	{
		return script_ && (script_.ownerID == this.mS_.myID || script_.publisherID == this.mS_.myID) && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp == 1;
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x000D3200 File Offset: 0x000D1400
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
				Item_AboPreis component = gameObject.GetComponent<Item_AboPreis>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.abonnements.ToString();
					break;
				case 2:
					gameObject.name = component.game_.aboPreis.ToString();
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

	// Token: 0x0600144A RID: 5194 RVA: 0x000D3303 File Offset: 0x000D1503
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001852 RID: 6226
	public GameObject[] uiPrefabs;

	// Token: 0x04001853 RID: 6227
	public GameObject[] uiObjects;

	// Token: 0x04001854 RID: 6228
	private mainScript mS_;

	// Token: 0x04001855 RID: 6229
	private GameObject main_;

	// Token: 0x04001856 RID: 6230
	private GUI_Main guiMain_;

	// Token: 0x04001857 RID: 6231
	private sfxScript sfx_;

	// Token: 0x04001858 RID: 6232
	private textScript tS_;

	// Token: 0x04001859 RID: 6233
	private genres genres_;

	// Token: 0x0400185A RID: 6234
	private float updateTimer;
}
