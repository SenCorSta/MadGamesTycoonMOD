using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000244 RID: 580
public class Menu_Stats_MyGamesVertriebeneSpiele : MonoBehaviour
{
	// Token: 0x06001666 RID: 5734 RVA: 0x000E34A6 File Offset: 0x000E16A6
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x000E34B0 File Offset: 0x000E16B0
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

	// Token: 0x06001668 RID: 5736 RVA: 0x000E3578 File Offset: 0x000E1778
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x000E35B0 File Offset: 0x000E17B0
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

	// Token: 0x0600166A RID: 5738 RVA: 0x000E35FC File Offset: 0x000E17FC
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

	// Token: 0x0600166B RID: 5739 RVA: 0x000E3640 File Offset: 0x000E1840
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x000E3648 File Offset: 0x000E1848
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

	// Token: 0x0600166D RID: 5741 RVA: 0x000E3704 File Offset: 0x000E1904
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x000E3718 File Offset: 0x000E1918
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

	// Token: 0x0600166F RID: 5743 RVA: 0x000E3822 File Offset: 0x000E1A22
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.publisherID == this.mS_.myID && script_.pubOffer;
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x000E384A File Offset: 0x000E1A4A
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000E3868 File Offset: 0x000E1A68
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

	// Token: 0x04001A57 RID: 6743
	private mainScript mS_;

	// Token: 0x04001A58 RID: 6744
	private GameObject main_;

	// Token: 0x04001A59 RID: 6745
	private GUI_Main guiMain_;

	// Token: 0x04001A5A RID: 6746
	private sfxScript sfx_;

	// Token: 0x04001A5B RID: 6747
	private textScript tS_;

	// Token: 0x04001A5C RID: 6748
	private genres genres_;

	// Token: 0x04001A5D RID: 6749
	public GameObject[] uiPrefabs;

	// Token: 0x04001A5E RID: 6750
	public GameObject[] uiObjects;

	// Token: 0x04001A5F RID: 6751
	private float updateTimer;
}
