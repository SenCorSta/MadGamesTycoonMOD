using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200024E RID: 590
public class Menu_Stats_MyKonsolen_Games : MonoBehaviour
{
	// Token: 0x060016EA RID: 5866 RVA: 0x000E635D File Offset: 0x000E455D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x000E6368 File Offset: 0x000E4568
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

	// Token: 0x060016EC RID: 5868 RVA: 0x000E6430 File Offset: 0x000E4630
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x000E6468 File Offset: 0x000E4668
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

	// Token: 0x060016EE RID: 5870 RVA: 0x000E64B4 File Offset: 0x000E46B4
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

	// Token: 0x060016EF RID: 5871 RVA: 0x000E64F8 File Offset: 0x000E46F8
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000E6500 File Offset: 0x000E4700
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x000E6510 File Offset: 0x000E4710
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

	// Token: 0x060016F2 RID: 5874 RVA: 0x000E6677 File Offset: 0x000E4877
	public bool CheckKonsoleData(platformScript script_)
	{
		return script_ && script_.ownerID == this.mS_.myID && script_.isUnlocked;
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x000E669F File Offset: 0x000E489F
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001AAE RID: 6830
	private mainScript mS_;

	// Token: 0x04001AAF RID: 6831
	private GameObject main_;

	// Token: 0x04001AB0 RID: 6832
	private GUI_Main guiMain_;

	// Token: 0x04001AB1 RID: 6833
	private sfxScript sfx_;

	// Token: 0x04001AB2 RID: 6834
	private textScript tS_;

	// Token: 0x04001AB3 RID: 6835
	private genres genres_;

	// Token: 0x04001AB4 RID: 6836
	public GameObject[] uiPrefabs;

	// Token: 0x04001AB5 RID: 6837
	public GameObject[] uiObjects;

	// Token: 0x04001AB6 RID: 6838
	private float updateTimer;
}
