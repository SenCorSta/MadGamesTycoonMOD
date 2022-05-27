using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024F RID: 591
public class Menu_Stats_MyKonsolen_Sells : MonoBehaviour
{
	// Token: 0x060016DA RID: 5850 RVA: 0x00010073 File Offset: 0x0000E273
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000ED41C File Offset: 0x000EB61C
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

	// Token: 0x060016DC RID: 5852 RVA: 0x0001007B File Offset: 0x0000E27B
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x000ED4E4 File Offset: 0x000EB6E4
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

	// Token: 0x060016DE RID: 5854 RVA: 0x000ED530 File Offset: 0x000EB730
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

	// Token: 0x060016DF RID: 5855 RVA: 0x000100B3 File Offset: 0x0000E2B3
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x000100BB File Offset: 0x0000E2BB
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x000ED574 File Offset: 0x000EB774
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

	// Token: 0x060016E2 RID: 5858 RVA: 0x0000FF0B File Offset: 0x0000E10B
	public bool CheckKonsoleData(platformScript script_)
	{
		return script_ && script_.playerConsole && script_.isUnlocked;
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000100C9 File Offset: 0x0000E2C9
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001AB5 RID: 6837
	private mainScript mS_;

	// Token: 0x04001AB6 RID: 6838
	private GameObject main_;

	// Token: 0x04001AB7 RID: 6839
	private GUI_Main guiMain_;

	// Token: 0x04001AB8 RID: 6840
	private sfxScript sfx_;

	// Token: 0x04001AB9 RID: 6841
	private textScript tS_;

	// Token: 0x04001ABA RID: 6842
	private genres genres_;

	// Token: 0x04001ABB RID: 6843
	public GameObject[] uiPrefabs;

	// Token: 0x04001ABC RID: 6844
	public GameObject[] uiObjects;

	// Token: 0x04001ABD RID: 6845
	private float updateTimer;
}
