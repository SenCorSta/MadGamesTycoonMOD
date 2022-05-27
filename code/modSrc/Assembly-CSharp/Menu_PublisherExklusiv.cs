using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000197 RID: 407
public class Menu_PublisherExklusiv : MonoBehaviour
{
	// Token: 0x06000F5F RID: 3935 RVA: 0x0000AEDF File Offset: 0x000090DF
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000B11EC File Offset: 0x000AF3EC
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

	// Token: 0x06000F61 RID: 3937 RVA: 0x0000AEE7 File Offset: 0x000090E7
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x000B12F8 File Offset: 0x000AF4F8
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

	// Token: 0x06000F63 RID: 3939 RVA: 0x000B1344 File Offset: 0x000AF544
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

	// Token: 0x06000F64 RID: 3940 RVA: 0x0000AF1F File Offset: 0x0000911F
	private void OnEnable()
	{
		this.InitDropdowns();
		this.Init();
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0000AF2D File Offset: 0x0000912D
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

	// Token: 0x06000F66 RID: 3942 RVA: 0x000B13A0 File Offset: 0x000AF5A0
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Publisher");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				publisherScript component = array[i].GetComponent<publisherScript>();
				if (component.isUnlocked && component.publisher && component.GetRelation() >= 100f && !component.tochterfirma && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x06000F67 RID: 3943 RVA: 0x000B14C4 File Offset: 0x000AF6C4
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

	// Token: 0x06000F68 RID: 3944 RVA: 0x000B1580 File Offset: 0x000AF780
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

	// Token: 0x06000F69 RID: 3945 RVA: 0x0000AF6D File Offset: 0x0000916D
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.CloseMenu();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x0000AF93 File Offset: 0x00009193
	public void SelectPublisher(publisherScript pS_)
	{
		if (!pS_)
		{
			return;
		}
		this.guiMain_.uiObjects[211].SetActive(true);
		this.guiMain_.uiObjects[211].GetComponent<Menu_W_PublisherExklusiv>().Init(pS_);
	}

	// Token: 0x040013C7 RID: 5063
	private mainScript mS_;

	// Token: 0x040013C8 RID: 5064
	private GameObject main_;

	// Token: 0x040013C9 RID: 5065
	private GUI_Main guiMain_;

	// Token: 0x040013CA RID: 5066
	private sfxScript sfx_;

	// Token: 0x040013CB RID: 5067
	private textScript tS_;

	// Token: 0x040013CC RID: 5068
	private themes themes_;

	// Token: 0x040013CD RID: 5069
	private Menu_DevGame mDevGame_;

	// Token: 0x040013CE RID: 5070
	private genres genres_;

	// Token: 0x040013CF RID: 5071
	private gameScript gS_;

	// Token: 0x040013D0 RID: 5072
	private taskGame task_;

	// Token: 0x040013D1 RID: 5073
	public GameObject[] uiPrefabs;

	// Token: 0x040013D2 RID: 5074
	public GameObject[] uiObjects;

	// Token: 0x040013D3 RID: 5075
	private float updateTimer;
}
