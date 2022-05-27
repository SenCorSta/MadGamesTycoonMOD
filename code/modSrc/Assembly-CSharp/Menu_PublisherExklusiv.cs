using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000198 RID: 408
public class Menu_PublisherExklusiv : MonoBehaviour
{
	// Token: 0x06000F77 RID: 3959 RVA: 0x000A4549 File Offset: 0x000A2749
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x000A4554 File Offset: 0x000A2754
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
		if (!this.themes_)
		{
			this.themes_ = this.main_.GetComponent<themes>();
		}
		if (!this.mDevGame_)
		{
			this.mDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x000A4660 File Offset: 0x000A2860
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x000A4698 File Offset: 0x000A2898
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

	// Token: 0x06000F7B RID: 3963 RVA: 0x000A46E4 File Offset: 0x000A28E4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_PublisherExklusiv>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x000A4740 File Offset: 0x000A2940
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000A474E File Offset: 0x000A294E
	public void Init()
	{
		this.FindScripts();
		if (this.mS_.exklusivVertrag_ID != -1)
		{
			this.guiMain_.uiObjects[382].SetActive(true);
			base.gameObject.SetActive(false);
			return;
		}
		this.SetData();
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x000A4790 File Offset: 0x000A2990
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.publisher && !component.isPlayer && component.GetRelation() >= 100f && !component.IsTochterfirma() && !this.Exists(this.uiObjects[0], component.myID))
				{
					Item_PublisherExklusiv component2 = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_PublisherExklusiv>();
					component2.pS_ = component;
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x000A48C0 File Offset: 0x000A2AC0
	public void InitDropdowns()
	{
		int @int = PlayerPrefs.GetInt(this.uiObjects[5].name);
		this.FindScripts();
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(355));
		list.Add(this.tS_.GetText(434));
		list.Add(this.tS_.GetText(436));
		list.Add(this.tS_.GetText(1024));
		this.uiObjects[5].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[5].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[5].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x000A497C File Offset: 0x000A2B7C
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[5].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[5].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_PublisherExklusiv component = gameObject.GetComponent<Item_PublisherExklusiv>();
				switch (value)
				{
				case 0:
					gameObject.name = component.pS_.GetName();
					break;
				case 1:
					gameObject.name = component.pS_.stars.ToString();
					break;
				case 2:
					gameObject.name = component.pS_.share.ToString();
					break;
				case 3:
					gameObject.name = component.pS_.GetMoneyExklusiv().ToString();
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

	// Token: 0x06000F81 RID: 3969 RVA: 0x000A4AA3 File Offset: 0x000A2CA3
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000A4AC9 File Offset: 0x000A2CC9
	public void SelectPublisher(publisherScript pS_)
	{
		if (!pS_)
		{
			return;
		}
		this.guiMain_.uiObjects[211].SetActive(true);
		this.guiMain_.uiObjects[211].GetComponent<Menu_W_PublisherExklusiv>().Init(pS_);
	}

	// Token: 0x040013D0 RID: 5072
	private mainScript mS_;

	// Token: 0x040013D1 RID: 5073
	private GameObject main_;

	// Token: 0x040013D2 RID: 5074
	private GUI_Main guiMain_;

	// Token: 0x040013D3 RID: 5075
	private sfxScript sfx_;

	// Token: 0x040013D4 RID: 5076
	private textScript tS_;

	// Token: 0x040013D5 RID: 5077
	private themes themes_;

	// Token: 0x040013D6 RID: 5078
	private Menu_DevGame mDevGame_;

	// Token: 0x040013D7 RID: 5079
	private genres genres_;

	// Token: 0x040013D8 RID: 5080
	private gameScript gS_;

	// Token: 0x040013D9 RID: 5081
	private taskGame task_;

	// Token: 0x040013DA RID: 5082
	public GameObject[] uiPrefabs;

	// Token: 0x040013DB RID: 5083
	public GameObject[] uiObjects;

	// Token: 0x040013DC RID: 5084
	private float updateTimer;
}
