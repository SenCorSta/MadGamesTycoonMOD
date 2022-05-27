using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021F RID: 543
public class Menu_Statistics_AllTimeBudget : MonoBehaviour
{
	// Token: 0x060014CF RID: 5327 RVA: 0x0000E28B File Offset: 0x0000C48B
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000E0D18 File Offset: 0x000DEF18
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

	// Token: 0x060014D1 RID: 5329 RVA: 0x0000E293 File Offset: 0x0000C493
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000E0DE0 File Offset: 0x000DEFE0
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

	// Token: 0x060014D3 RID: 5331 RVA: 0x000E0B40 File Offset: 0x000DED40
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				Item_AllTimeCharts component = parent_.transform.GetChild(i).GetComponent<Item_AllTimeCharts>();
				if (component.game_.myID == id_)
				{
					gameObject.name = component.game_.sellsTotal.ToString();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x0000E2CB File Offset: 0x0000C4CB
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x0000E2D3 File Offset: 0x0000C4D3
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000E0E2C File Offset: 0x000DF02C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.typ_budget && !component.inDevelopment && component.sellsTotal > 0L && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_AllTimeCharts component2 = gameObject.GetComponent<Item_AllTimeCharts>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					gameObject.name = component.sellsTotal.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018EC RID: 6380
	private mainScript mS_;

	// Token: 0x040018ED RID: 6381
	private GameObject main_;

	// Token: 0x040018EE RID: 6382
	private GUI_Main guiMain_;

	// Token: 0x040018EF RID: 6383
	private sfxScript sfx_;

	// Token: 0x040018F0 RID: 6384
	private textScript tS_;

	// Token: 0x040018F1 RID: 6385
	private genres genres_;

	// Token: 0x040018F2 RID: 6386
	public GameObject[] uiPrefabs;

	// Token: 0x040018F3 RID: 6387
	public GameObject[] uiObjects;

	// Token: 0x040018F4 RID: 6388
	private float updateTimer;
}
