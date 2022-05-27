using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000222 RID: 546
public class Menu_Statistics_AllTimeCharts : MonoBehaviour
{
	// Token: 0x06001501 RID: 5377 RVA: 0x000D814D File Offset: 0x000D634D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000D8158 File Offset: 0x000D6358
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

	// Token: 0x06001503 RID: 5379 RVA: 0x000D823E File Offset: 0x000D643E
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x000D8270 File Offset: 0x000D6470
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

	// Token: 0x06001505 RID: 5381 RVA: 0x000D82DB File Offset: 0x000D64DB
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x000D82E3 File Offset: 0x000D64E3
	public void Init()
	{
		this.FindScripts();
		this.TAB_Select(0);
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x000D82F4 File Offset: 0x000D64F4
	private void SetData()
	{
		List<gameScript> list = new List<gameScript>();
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i] && this.games_.arrayGamesScripts[i].sellsTotal > 0L && ((this.TAB == 0 && this.games_.arrayGamesScripts[i].gameTyp != 2 && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade && !this.games_.arrayGamesScripts[i].typ_budget && !this.games_.arrayGamesScripts[i].typ_bundle && !this.games_.arrayGamesScripts[i].typ_addon && !this.games_.arrayGamesScripts[i].typ_addonStandalone && !this.games_.arrayGamesScripts[i].typ_mmoaddon) || (this.TAB == 1 && this.games_.arrayGamesScripts[i].gameTyp != 2 && this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].typ_budget && !this.games_.arrayGamesScripts[i].typ_bundle && !this.games_.arrayGamesScripts[i].typ_addon && !this.games_.arrayGamesScripts[i].typ_addonStandalone && !this.games_.arrayGamesScripts[i].typ_mmoaddon) || (this.TAB == 2 && this.games_.arrayGamesScripts[i].gameTyp != 2 && this.games_.arrayGamesScripts[i].arcade && !this.games_.arrayGamesScripts[i].typ_budget && !this.games_.arrayGamesScripts[i].typ_bundle && !this.games_.arrayGamesScripts[i].typ_addon && !this.games_.arrayGamesScripts[i].typ_addonStandalone && !this.games_.arrayGamesScripts[i].typ_mmoaddon) || (this.TAB == 3 && this.games_.arrayGamesScripts[i].gameTyp == 2 && !this.games_.arrayGamesScripts[i].typ_budget && !this.games_.arrayGamesScripts[i].typ_bundle && !this.games_.arrayGamesScripts[i].typ_addon && !this.games_.arrayGamesScripts[i].typ_addonStandalone && !this.games_.arrayGamesScripts[i].typ_mmoaddon) || (this.TAB == 4 && this.games_.arrayGamesScripts[i].gameTyp != 2 && this.games_.arrayGamesScripts[i].typ_budget) || (this.TAB == 5 && this.games_.arrayGamesScripts[i].gameTyp != 2 && (this.games_.arrayGamesScripts[i].typ_bundle || this.games_.arrayGamesScripts[i].typ_bundleAddon)) || (this.TAB == 6 && this.games_.arrayGamesScripts[i].gameTyp != 2 && (this.games_.arrayGamesScripts[i].typ_addon || this.games_.arrayGamesScripts[i].typ_addonStandalone || this.games_.arrayGamesScripts[i].typ_mmoaddon))))
			{
				list.Add(this.games_.arrayGamesScripts[i]);
			}
		}
		list.Sort((gameScript p1, gameScript p2) => p2.sellsTotal.CompareTo(p1.sellsTotal));
		while (list.Count > 200)
		{
			list.RemoveAt(list.Count - 1);
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j])
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
				Item_AllTimeCharts component = gameObject.GetComponent<Item_AllTimeCharts>();
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.genres_ = this.genres_;
				component.game_ = list[j];
				gameObject.name = list[j].sellsTotal.ToString();
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000D87DB File Offset: 0x000D69DB
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x000D87F8 File Offset: 0x000D69F8
	public void TAB_Select(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x04001905 RID: 6405
	private mainScript mS_;

	// Token: 0x04001906 RID: 6406
	private GameObject main_;

	// Token: 0x04001907 RID: 6407
	private GUI_Main guiMain_;

	// Token: 0x04001908 RID: 6408
	private sfxScript sfx_;

	// Token: 0x04001909 RID: 6409
	private textScript tS_;

	// Token: 0x0400190A RID: 6410
	private genres genres_;

	// Token: 0x0400190B RID: 6411
	private games games_;

	// Token: 0x0400190C RID: 6412
	public GameObject[] uiPrefabs;

	// Token: 0x0400190D RID: 6413
	public GameObject[] uiObjects;

	// Token: 0x0400190E RID: 6414
	private int TAB;
}
