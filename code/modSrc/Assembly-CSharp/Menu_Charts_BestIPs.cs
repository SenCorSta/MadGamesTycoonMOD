using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000218 RID: 536
public class Menu_Charts_BestIPs : MonoBehaviour
{
	// Token: 0x060014A6 RID: 5286 RVA: 0x000D610D File Offset: 0x000D430D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000D6118 File Offset: 0x000D4318
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

	// Token: 0x060014A8 RID: 5288 RVA: 0x000D61FE File Offset: 0x000D43FE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x000D6238 File Offset: 0x000D4438
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

	// Token: 0x060014AA RID: 5290 RVA: 0x000D6284 File Offset: 0x000D4484
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_MyGames_MyIPs>().game_.myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000D62C8 File Offset: 0x000D44C8
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000D62D0 File Offset: 0x000D44D0
	public void InitDropdowns()
	{
		this.FindScripts();
		int @int = PlayerPrefs.GetInt(this.uiObjects[1].name);
		List<string> list = new List<string>();
		list.Add(this.tS_.GetText(183));
		list.Add(this.tS_.GetText(217));
		list.Add(this.tS_.GetText(1555));
		this.uiObjects[1].GetComponent<Dropdown>().ClearOptions();
		this.uiObjects[1].GetComponent<Dropdown>().AddOptions(list);
		this.uiObjects[1].GetComponent<Dropdown>().value = @int;
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x000D6376 File Offset: 0x000D4576
	public void Init()
	{
		this.FindScripts();
		this.InitDropdowns();
		this.CreateBestIPsCharts(100);
		this.SetData();
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000D6394 File Offset: 0x000D4594
	private void SetData()
	{
		for (int i = 0; i < this.bestIPsList.Count; i++)
		{
			if (this.bestIPsList[i].script_)
			{
				gameScript script_ = this.bestIPsList[i].script_;
				if (script_ && this.CheckGameData(script_) && !this.Exists(this.uiObjects[0], script_.myID))
				{
					Item_MyGames_MyIPs component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_MyGames_MyIPs>();
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.sfx_ = this.sfx_;
					component.guiMain_ = this.guiMain_;
					component.genres_ = this.genres_;
					component.game_ = script_;
				}
			}
		}
		this.DROPDOWN_Sort();
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060014AF RID: 5295 RVA: 0x000D64B2 File Offset: 0x000D46B2
	public bool CheckGameData(gameScript script_)
	{
		return script_ && !script_.pubAngebot && !script_.auftragsspiel && script_.mainIP == script_.myID && !script_.inDevelopment;
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000D64E5 File Offset: 0x000D46E5
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000D6500 File Offset: 0x000D4700
	public void DROPDOWN_Sort()
	{
		int value = this.uiObjects[1].GetComponent<Dropdown>().value;
		PlayerPrefs.SetInt(this.uiObjects[1].name, value);
		int childCount = this.uiObjects[0].transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject gameObject = this.uiObjects[0].transform.GetChild(i).gameObject;
			if (gameObject)
			{
				Item_MyGames_MyIPs component = gameObject.GetComponent<Item_MyGames_MyIPs>();
				switch (value)
				{
				case 0:
					gameObject.name = component.game_.GetNameSimple();
					break;
				case 1:
				{
					float num = (float)component.game_.date_month;
					num /= 13f;
					gameObject.name = component.game_.date_year.ToString() + num.ToString();
					if (component.game_.inDevelopment)
					{
						gameObject.name = "999999";
					}
					break;
				}
				case 2:
					gameObject.name = component.game_.ipPunkte.ToString();
					break;
				}
			}
		}
		if (value == 0)
		{
			this.mS_.SortChildrenByName(this.uiObjects[0]);
			return;
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000D6648 File Offset: 0x000D4848
	public void CreateBestIPsCharts(int max)
	{
		this.bestIPsList.Clear();
		for (int j = 0; j < this.games_.arrayGamesScripts.Length; j++)
		{
			if (this.games_.arrayGamesScripts[j] && this.CheckGameData(this.games_.arrayGamesScripts[j]))
			{
				this.bestIPsList.Add(new BestIPsList(this.games_.arrayGamesScripts[j].myID, this.games_.arrayGamesScripts[j].GetIpBekanntheit(), this.games_.arrayGamesScripts[j]));
			}
		}
		this.bestIPsList = (from i in this.bestIPsList
		orderby i.wert descending
		select i).ToList<BestIPsList>();
		while (this.bestIPsList.Count > max)
		{
			this.bestIPsList.RemoveAt(this.bestIPsList.Count - 1);
		}
	}

	// Token: 0x040018B4 RID: 6324
	private mainScript mS_;

	// Token: 0x040018B5 RID: 6325
	private GameObject main_;

	// Token: 0x040018B6 RID: 6326
	private GUI_Main guiMain_;

	// Token: 0x040018B7 RID: 6327
	private sfxScript sfx_;

	// Token: 0x040018B8 RID: 6328
	private textScript tS_;

	// Token: 0x040018B9 RID: 6329
	private genres genres_;

	// Token: 0x040018BA RID: 6330
	private games games_;

	// Token: 0x040018BB RID: 6331
	public GameObject[] uiPrefabs;

	// Token: 0x040018BC RID: 6332
	public GameObject[] uiObjects;

	// Token: 0x040018BD RID: 6333
	private float updateTimer;

	// Token: 0x040018BE RID: 6334
	public List<BestIPsList> bestIPsList = new List<BestIPsList>();
}
