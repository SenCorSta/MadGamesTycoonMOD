using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000250 RID: 592
public class Menu_Stats_MyKonsolen_Sells : MonoBehaviour
{
	// Token: 0x060016FF RID: 5887 RVA: 0x000E68C2 File Offset: 0x000E4AC2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x000E68CC File Offset: 0x000E4ACC
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

	// Token: 0x06001701 RID: 5889 RVA: 0x000E6994 File Offset: 0x000E4B94
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x000E69CC File Offset: 0x000E4BCC
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

	// Token: 0x06001703 RID: 5891 RVA: 0x000E6A18 File Offset: 0x000E4C18
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyKonsolen_Sells>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x000E6A5C File Offset: 0x000E4C5C
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x000E6A64 File Offset: 0x000E4C64
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000E6A74 File Offset: 0x000E4C74
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Platform");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				platformScript component = array[i].GetComponent<platformScript>();
				if (component && this.CheckKonsoleData(component) && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_MyKonsolen_Sells component2 = gameObject.GetComponent<Item_MyKonsolen_Sells>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.pS_ = component;
					gameObject.name = component.units.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1662);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000E6BDB File Offset: 0x000E4DDB
	public bool CheckKonsoleData(platformScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.isUnlocked;
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000E6C03 File Offset: 0x000E4E03
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
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
