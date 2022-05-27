using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024A RID: 586
public class Menu_Stats_MyGames_Umsatz : MonoBehaviour
{
	// Token: 0x0600169F RID: 5791 RVA: 0x0000FD02 File Offset: 0x0000DF02
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x000EC288 File Offset: 0x000EA488
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

	// Token: 0x060016A1 RID: 5793 RVA: 0x0000FD0A File Offset: 0x0000DF0A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016A2 RID: 5794 RVA: 0x000EC350 File Offset: 0x000EA550
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

	// Token: 0x060016A3 RID: 5795 RVA: 0x000EC39C File Offset: 0x000EA59C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_Umsatz>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x0000FD42 File Offset: 0x0000DF42
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x0000FD4A File Offset: 0x0000DF4A
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x000EC3E0 File Offset: 0x000EA5E0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[4].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(489));
		list.Add(this.tS_.GetText(1238));
		list.Add(this.tS_.GetText(1370));
		list.Add(this.tS_.GetText(1371));
		list.Add(this.tS_.GetText(6));
		this.uiObjects[4].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[4].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[4].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000EC4B0 File Offset: 0x000EA6B0
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && !component.inDevelopment && component.playerGame && !component.schublade && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MyGames_Umsatz component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_Umsatz>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x0000FD5E File Offset: 0x0000DF5E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000EC624 File Offset: 0x000EA824
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[4].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[4].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_MyGames_Umsatz component = gameObject.GetComponent<Item_MyGames_Umsatz>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetGesamtGewinn().ToString();
					break;
				case 1:
					gameObject.name = component.game_.umsatzTotal.ToString();
					break;
				case 2:
					gameObject.name = component.game_.umsatzAbos.ToString();
					break;
				case 3:
					gameObject.name = component.game_.umsatzInApp.ToString();
					break;
				case 4:
					gameObject.name = component.game_.GetEntwicklungskosten().ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x04001A8A RID: 6794
	private mainScript mS_;

	// Token: 0x04001A8B RID: 6795
	private GameObject main_;

	// Token: 0x04001A8C RID: 6796
	private GUI_Main guiMain_;

	// Token: 0x04001A8D RID: 6797
	private sfxScript sfx_;

	// Token: 0x04001A8E RID: 6798
	private textScript tS_;

	// Token: 0x04001A8F RID: 6799
	private genres genres_;

	// Token: 0x04001A90 RID: 6800
	public GameObject[] uiPrefabs;

	// Token: 0x04001A91 RID: 6801
	public GameObject[] uiObjects;

	// Token: 0x04001A92 RID: 6802
	private float updateTimer;
}
