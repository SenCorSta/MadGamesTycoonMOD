using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021E RID: 542
public class Menu_StatisticsAllTimeChartsHandy : MonoBehaviour
{
	// Token: 0x060014D9 RID: 5337 RVA: 0x000D73F9 File Offset: 0x000D55F9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000D7404 File Offset: 0x000D5604
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
		if (!this.games_)
		{
			this.games_ = this.main_.GetComponent<games>();
		}
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000D74EA File Offset: 0x000D56EA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000D7524 File Offset: 0x000D5724
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

	// Token: 0x060014DD RID: 5341 RVA: 0x000D7570 File Offset: 0x000D5770
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				Item_AllTimeCharts component = gameObject.GetComponent<Item_AllTimeCharts>();
				if (component.game_.myID == id_)
				{
					gameObject.name = component.game_.sellsTotal.ToString();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000D75DB File Offset: 0x000D57DB
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000D75E3 File Offset: 0x000D57E3
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000D75F4 File Offset: 0x000D57F4
	private void SetData()
	{
		this.games_.CreateAllTimeChartsHandy(500);
		for (int i = 0; i < this.games_.chartsList.Count; i++)
		{
			gameScript script_ = this.games_.chartsList[i].script_;
			if (script_ && !this.Exists(this.uiObjects[0], script_.myID))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_AllTimeCharts component = gameObject.GetComponent<Item_AllTimeCharts>();
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.genres_ = this.genres_;
				component.game_ = script_;
				gameObject.name = script_.sellsTotal.ToString();
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x000D7726 File Offset: 0x000D5926
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018E0 RID: 6368
	private mainScript mS_;

	// Token: 0x040018E1 RID: 6369
	private GameObject main_;

	// Token: 0x040018E2 RID: 6370
	private GUI_Main guiMain_;

	// Token: 0x040018E3 RID: 6371
	private sfxScript sfx_;

	// Token: 0x040018E4 RID: 6372
	private textScript tS_;

	// Token: 0x040018E5 RID: 6373
	private genres genres_;

	// Token: 0x040018E6 RID: 6374
	private games games_;

	// Token: 0x040018E7 RID: 6375
	public GameObject[] uiPrefabs;

	// Token: 0x040018E8 RID: 6376
	public GameObject[] uiObjects;

	// Token: 0x040018E9 RID: 6377
	private float updateTimer;
}
