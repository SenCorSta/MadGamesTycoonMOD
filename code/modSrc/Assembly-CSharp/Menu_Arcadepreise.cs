using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F0 RID: 496
public class Menu_Arcadepreise : MonoBehaviour
{
	// Token: 0x060012D3 RID: 4819 RVA: 0x000C7D06 File Offset: 0x000C5F06
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000C7D10 File Offset: 0x000C5F10
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

	// Token: 0x060012D5 RID: 4821 RVA: 0x000C7DD8 File Offset: 0x000C5FD8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000C7E10 File Offset: 0x000C6010
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

	// Token: 0x060012D7 RID: 4823 RVA: 0x000C7E5C File Offset: 0x000C605C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_Arcadepreis>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000C7EB8 File Offset: 0x000C60B8
	private void OnEnable()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x000C7ECC File Offset: 0x000C60CC
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(88));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(277));
		list.Add(this.tS_.GetText(273));
		list.Add(this.tS_.GetText(275));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060012DA RID: 4826 RVA: 0x000C7FAC File Offset: 0x000C61AC
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060012DB RID: 4827 RVA: 0x000C8000 File Offset: 0x000C6200
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
					Item_Arcadepreis component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_Arcadepreis>();
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

	// Token: 0x060012DC RID: 4828 RVA: 0x000C810C File Offset: 0x000C630C
	public bool CheckGameData(gameScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.developerID == this.mS_.myID && !script_.inDevelopment && script_.isOnMarket && script_.publisherID == this.mS_.myID && script_.gameTyp != 2 && script_.arcade;
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000C8180 File Offset: 0x000C6380
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
				Item_Arcadepreis component = gameObject.GetComponent<Item_Arcadepreis>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
					gameObject.name = component.game_.verkaufspreis[0].ToString();
					break;
				case 2:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					break;
				}
				case 3:
					gameObject.name = component.game_.reviewTotal.ToString();
					break;
				case 4:
					gameObject.name = component.game_.maingenre.ToString();
					break;
				case 5:
					gameObject.name = component.game_.sellsTotal.ToString();
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

	// Token: 0x060012DE RID: 4830 RVA: 0x000C8311 File Offset: 0x000C6511
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001723 RID: 5923
	public GameObject[] uiPrefabs;

	// Token: 0x04001724 RID: 5924
	public GameObject[] uiObjects;

	// Token: 0x04001725 RID: 5925
	private mainScript mS_;

	// Token: 0x04001726 RID: 5926
	private GameObject main_;

	// Token: 0x04001727 RID: 5927
	private GUI_Main guiMain_;

	// Token: 0x04001728 RID: 5928
	private sfxScript sfx_;

	// Token: 0x04001729 RID: 5929
	private textScript tS_;

	// Token: 0x0400172A RID: 5930
	private genres genres_;

	// Token: 0x0400172B RID: 5931
	private float updateTimer;
}
