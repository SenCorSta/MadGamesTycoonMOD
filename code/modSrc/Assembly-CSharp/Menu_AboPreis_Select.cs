using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020E RID: 526
public class Menu_AboPreis_Select : MonoBehaviour
{
	// Token: 0x06001422 RID: 5154 RVA: 0x0000DB6B File Offset: 0x0000BD6B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000DC8DC File Offset: 0x000DAADC
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

	// Token: 0x06001424 RID: 5156 RVA: 0x0000DB73 File Offset: 0x0000BD73
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000DC9A4 File Offset: 0x000DABA4
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

	// Token: 0x06001426 RID: 5158 RVA: 0x000DC9F0 File Offset: 0x000DABF0
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

	// Token: 0x06001427 RID: 5159 RVA: 0x0000DBAB File Offset: 0x0000BDAB
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000DCA4C File Offset: 0x000DAC4C
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

	// Token: 0x06001429 RID: 5161 RVA: 0x000DCAEC File Offset: 0x000DACEC
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x0600142A RID: 5162 RVA: 0x000DCB40 File Offset: 0x000DAD40
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

	// Token: 0x0600142B RID: 5163 RVA: 0x0000DBBF File Offset: 0x0000BDBF
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && !script_.inDevelopment && script_.isOnMarket && script_.gameTyp == 1;
	}

	// Token: 0x0600142C RID: 5164 RVA: 0x000DCC4C File Offset: 0x000DAE4C
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

	// Token: 0x0600142D RID: 5165 RVA: 0x0000DBED File Offset: 0x0000BDED
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001849 RID: 6217
	public GameObject[] uiPrefabs;

	// Token: 0x0400184A RID: 6218
	public GameObject[] uiObjects;

	// Token: 0x0400184B RID: 6219
	private mainScript mS_;

	// Token: 0x0400184C RID: 6220
	private GameObject main_;

	// Token: 0x0400184D RID: 6221
	private GUI_Main guiMain_;

	// Token: 0x0400184E RID: 6222
	private sfxScript sfx_;

	// Token: 0x0400184F RID: 6223
	private textScript tS_;

	// Token: 0x04001850 RID: 6224
	private genres genres_;

	// Token: 0x04001851 RID: 6225
	private float updateTimer;
}
