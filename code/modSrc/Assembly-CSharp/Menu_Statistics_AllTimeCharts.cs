using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Statistics_AllTimeCharts : MonoBehaviour
{
	
	private void Start()
	{
		this.FindScripts();
	}

	
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

	
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
	}

	
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

	
	private void OnEnable()
	{
		this.Init();
	}

	
	public void Init()
	{
		this.FindScripts();
		this.TAB_Select(0);
	}

	
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

	
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	
	public void TAB_Select(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	
	private mainScript mS_;

	
	private GameObject main_;

	
	private GUI_Main guiMain_;

	
	private sfxScript sfx_;

	
	private textScript tS_;

	
	private genres genres_;

	
	private games games_;

	
	public GameObject[] uiPrefabs;

	
	public GameObject[] uiObjects;

	
	private int TAB;
}
