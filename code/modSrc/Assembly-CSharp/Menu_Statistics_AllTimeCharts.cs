using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000221 RID: 545
public class Menu_Statistics_AllTimeCharts : MonoBehaviour
{
	// Token: 0x060014E3 RID: 5347 RVA: 0x0000E36D File Offset: 0x0000C56D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000E11D0 File Offset: 0x000DF3D0
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

	// Token: 0x060014E5 RID: 5349 RVA: 0x0000E375 File Offset: 0x0000C575
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000DF5FC File Offset: 0x000DD7FC
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

	// Token: 0x060014E7 RID: 5351 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x0000E3AF File Offset: 0x0000C5AF
	public void Init()
	{
		this.FindScripts();
		this.TAB_Select(0);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000E12B8 File Offset: 0x000DF4B8
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

	// Token: 0x060014EA RID: 5354 RVA: 0x0000E3BE File Offset: 0x0000C5BE
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x000E17A0 File Offset: 0x000DF9A0
	public void TAB_Select(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x040018FE RID: 6398
	private mainScript mS_;

	// Token: 0x040018FF RID: 6399
	private GameObject main_;

	// Token: 0x04001900 RID: 6400
	private GUI_Main guiMain_;

	// Token: 0x04001901 RID: 6401
	private sfxScript sfx_;

	// Token: 0x04001902 RID: 6402
	private textScript tS_;

	// Token: 0x04001903 RID: 6403
	private genres genres_;

	// Token: 0x04001904 RID: 6404
	private games games_;

	// Token: 0x04001905 RID: 6405
	public GameObject[] uiPrefabs;

	// Token: 0x04001906 RID: 6406
	public GameObject[] uiObjects;

	// Token: 0x04001907 RID: 6407
	private int TAB;
}
