using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000243 RID: 579
public class Menu_Stats_MyGamesVertriebeneSpiele : MonoBehaviour
{
	// Token: 0x06001647 RID: 5703 RVA: 0x0000F6EC File Offset: 0x0000D8EC
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x000EAE54 File Offset: 0x000E9054
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

	// Token: 0x06001649 RID: 5705 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x000EAF1C File Offset: 0x000E911C
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

	// Token: 0x0600164B RID: 5707 RVA: 0x000EAF68 File Offset: 0x000E9168
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_VertriebeneSpiele>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x0000F72C File Offset: 0x0000D92C
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x000EAFAC File Offset: 0x000E91AC
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(274));
		list.Add(this.tS_.GetText(489));
		list.Add(this.tS_.GetText(217));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x0000F734 File Offset: 0x0000D934
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x000EB068 File Offset: 0x000E9268
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
					Item_MyGames_VertriebeneSpiele component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_VertriebeneSpiele>();
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

	// Token: 0x06001650 RID: 5712 RVA: 0x0000F748 File Offset: 0x0000D948
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.playerGame && script_.pubOffer;
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x0000F765 File Offset: 0x0000D965
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x000EB174 File Offset: 0x000E9374
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
				Item_MyGames_VertriebeneSpiele component = gameObject.GetComponent<Item_MyGames_VertriebeneSpiele>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.GetDeveloperName();
					break;
				case 2:
					gameObject.name = component.game_.GetGesamtGewinn().ToString();
					break;
				case 3:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				}
			}
		}
		if (value == 0 || value == 1)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x04001A4E RID: 6734
	private mainScript mS_;

	// Token: 0x04001A4F RID: 6735
	private GameObject main_;

	// Token: 0x04001A50 RID: 6736
	private GUI_Main guiMain_;

	// Token: 0x04001A51 RID: 6737
	private sfxScript sfx_;

	// Token: 0x04001A52 RID: 6738
	private textScript tS_;

	// Token: 0x04001A53 RID: 6739
	private genres genres_;

	// Token: 0x04001A54 RID: 6740
	public GameObject[] uiPrefabs;

	// Token: 0x04001A55 RID: 6741
	public GameObject[] uiObjects;

	// Token: 0x04001A56 RID: 6742
	private float updateTimer;
}
