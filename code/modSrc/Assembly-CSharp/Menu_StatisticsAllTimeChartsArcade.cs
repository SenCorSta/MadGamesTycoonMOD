using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021C RID: 540
public class Menu_StatisticsAllTimeChartsArcade : MonoBehaviour
{
	// Token: 0x060014B1 RID: 5297 RVA: 0x0000E138 File Offset: 0x0000C338
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000E055C File Offset: 0x000DE75C
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

	// Token: 0x060014B3 RID: 5299 RVA: 0x0000E140 File Offset: 0x0000C340
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000E0644 File Offset: 0x000DE844
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

	// Token: 0x060014B5 RID: 5301 RVA: 0x000DF5FC File Offset: 0x000DD7FC
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

	// Token: 0x060014B6 RID: 5302 RVA: 0x0000E178 File Offset: 0x0000C378
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014B7 RID: 5303 RVA: 0x0000E180 File Offset: 0x0000C380
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014B8 RID: 5304 RVA: 0x000E0690 File Offset: 0x000DE890
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

	// Token: 0x060014B9 RID: 5305 RVA: 0x0000E18E File Offset: 0x0000C38E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018CF RID: 6351
	private mainScript mS_;

	// Token: 0x040018D0 RID: 6352
	private GameObject main_;

	// Token: 0x040018D1 RID: 6353
	private GUI_Main guiMain_;

	// Token: 0x040018D2 RID: 6354
	private sfxScript sfx_;

	// Token: 0x040018D3 RID: 6355
	private textScript tS_;

	// Token: 0x040018D4 RID: 6356
	private genres genres_;

	// Token: 0x040018D5 RID: 6357
	private games games_;

	// Token: 0x040018D6 RID: 6358
	public GameObject[] uiPrefabs;

	// Token: 0x040018D7 RID: 6359
	public GameObject[] uiObjects;

	// Token: 0x040018D8 RID: 6360
	private float updateTimer;
}
