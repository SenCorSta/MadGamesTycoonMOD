using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000250 RID: 592
public class Menu_Stats_MyKonsolen_Umsatz : MonoBehaviour
{
	// Token: 0x060016E5 RID: 5861 RVA: 0x000100E4 File Offset: 0x0000E2E4
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000ED6DC File Offset: 0x000EB8DC
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

	// Token: 0x060016E7 RID: 5863 RVA: 0x000100EC File Offset: 0x0000E2EC
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000ED7A4 File Offset: 0x000EB9A4
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

	// Token: 0x060016E9 RID: 5865 RVA: 0x000ED7F0 File Offset: 0x000EB9F0
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyKonsolen_Umsatz>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00010124 File Offset: 0x0000E324
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x0001012C File Offset: 0x0000E32C
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.SetData();
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x000ED834 File Offset: 0x000EBA34
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[4].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(489));
		list.Add(this.tS_.GetText(1238));
		list.Add(this.tS_.GetText(6));
		this.uiObjects[4].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[4].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[4].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x000ED8D8 File Offset: 0x000EBAD8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && component.playerConsole && component.isUnlocked && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_MyKonsolen_Umsatz component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyKonsolen_Umsatz>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.pS_ = component;
					component2.menu_ = this;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1662);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x00010140 File Offset: 0x0000E340
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000EDA44 File Offset: 0x000EBC44
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
				Item_MyKonsolen_Umsatz component = gameObject.GetComponent<Item_MyKonsolen_Umsatz>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetGesamtGewinn().ToString();
					break;
				case 1:
					gameObject.name = component.pS_.umsatzTotal.ToString();
					break;
				case 2:
					gameObject.name = component.pS_.GetEntwicklungskosten().ToString();
					break;
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x04001ABE RID: 6846
	private mainScript mS_;

	// Token: 0x04001ABF RID: 6847
	private GameObject main_;

	// Token: 0x04001AC0 RID: 6848
	private GUI_Main guiMain_;

	// Token: 0x04001AC1 RID: 6849
	private sfxScript sfx_;

	// Token: 0x04001AC2 RID: 6850
	private textScript tS_;

	// Token: 0x04001AC3 RID: 6851
	private genres genres_;

	// Token: 0x04001AC4 RID: 6852
	public GameObject[] uiPrefabs;

	// Token: 0x04001AC5 RID: 6853
	public GameObject[] uiObjects;

	// Token: 0x04001AC6 RID: 6854
	private float updateTimer;
}
