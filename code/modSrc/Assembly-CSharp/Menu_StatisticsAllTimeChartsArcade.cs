using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021D RID: 541
public class Menu_StatisticsAllTimeChartsArcade : MonoBehaviour
{
	// Token: 0x060014CF RID: 5327 RVA: 0x000D70B2 File Offset: 0x000D52B2
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000D70BC File Offset: 0x000D52BC
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

	// Token: 0x060014D1 RID: 5329 RVA: 0x000D71A2 File Offset: 0x000D53A2
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000D71DC File Offset: 0x000D53DC
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

	// Token: 0x060014D3 RID: 5331 RVA: 0x000D7228 File Offset: 0x000D5428
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

	// Token: 0x060014D4 RID: 5332 RVA: 0x000D7293 File Offset: 0x000D5493
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x000D729B File Offset: 0x000D549B
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014D6 RID: 5334 RVA: 0x000D72AC File Offset: 0x000D54AC
	private void SetData()
	{
		this.games_.CreateAllTimeChartsArcade(500);
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

	// Token: 0x060014D7 RID: 5335 RVA: 0x000D73DE File Offset: 0x000D55DE
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018D6 RID: 6358
	private mainScript mS_;

	// Token: 0x040018D7 RID: 6359
	private GameObject main_;

	// Token: 0x040018D8 RID: 6360
	private GUI_Main guiMain_;

	// Token: 0x040018D9 RID: 6361
	private sfxScript sfx_;

	// Token: 0x040018DA RID: 6362
	private textScript tS_;

	// Token: 0x040018DB RID: 6363
	private genres genres_;

	// Token: 0x040018DC RID: 6364
	private games games_;

	// Token: 0x040018DD RID: 6365
	public GameObject[] uiPrefabs;

	// Token: 0x040018DE RID: 6366
	public GameObject[] uiObjects;

	// Token: 0x040018DF RID: 6367
	private float updateTimer;
}
