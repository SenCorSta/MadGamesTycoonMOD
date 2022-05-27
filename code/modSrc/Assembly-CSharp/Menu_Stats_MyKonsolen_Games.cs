using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024D RID: 589
public class Menu_Stats_MyKonsolen_Games : MonoBehaviour
{
	// Token: 0x060016C5 RID: 5829 RVA: 0x0000FEB5 File Offset: 0x0000E0B5
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x000ED0B0 File Offset: 0x000EB2B0
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

	// Token: 0x060016C7 RID: 5831 RVA: 0x0000FEBD File Offset: 0x0000E0BD
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000ED178 File Offset: 0x000EB378
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

	// Token: 0x060016C9 RID: 5833 RVA: 0x000ED1C4 File Offset: 0x000EB3C4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyKonsolen_Games>().pS_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x0000FEF5 File Offset: 0x0000E0F5
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x0000FEFD File Offset: 0x0000E0FD
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000ED208 File Offset: 0x000EB408
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
					Item_MyKonsolen_Games component2 = gameObject.GetComponent<Item_MyKonsolen_Games>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.pS_ = component;
					gameObject.name = component.games.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
		string text = this.tS_.GetText(1662);
		text = text.Replace("<NUM>", this.uiObjects[0].transform.childCount.ToString());
		this.uiObjects[1].GetComponent<Text>().text = text;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x0000FF0B File Offset: 0x0000E10B
	public bool CheckKonsoleData(platformScript script_)
	{
		return script_ && script_.playerConsole && script_.isUnlocked;
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x0000FF28 File Offset: 0x0000E128
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001AA5 RID: 6821
	private mainScript mS_;

	// Token: 0x04001AA6 RID: 6822
	private GameObject main_;

	// Token: 0x04001AA7 RID: 6823
	private GUI_Main guiMain_;

	// Token: 0x04001AA8 RID: 6824
	private sfxScript sfx_;

	// Token: 0x04001AA9 RID: 6825
	private textScript tS_;

	// Token: 0x04001AAA RID: 6826
	private genres genres_;

	// Token: 0x04001AAB RID: 6827
	public GameObject[] uiPrefabs;

	// Token: 0x04001AAC RID: 6828
	public GameObject[] uiObjects;

	// Token: 0x04001AAD RID: 6829
	private float updateTimer;
}
