using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022D RID: 557
public class Menu_Statistics_WochenCharts : MonoBehaviour
{
	// Token: 0x06001567 RID: 5479 RVA: 0x0000EBCD File Offset: 0x0000CDCD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000E42B4 File Offset: 0x000E24B4
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

	// Token: 0x06001569 RID: 5481 RVA: 0x0000EBD5 File Offset: 0x0000CDD5
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000E439C File Offset: 0x000E259C
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

	// Token: 0x0600156B RID: 5483 RVA: 0x000E43E8 File Offset: 0x000E25E8
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				Item_WochenCharts component = gameObject.GetComponent<Item_WochenCharts>();
				if (component.game_.myID == id_)
				{
					gameObject.name = component.game_.sellsPerWeek[0].ToString();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x0000EC0D File Offset: 0x0000CE0D
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x0000EC15 File Offset: 0x0000CE15
	public void Init()
	{
		this.FindScripts();
		this.TAB_Select(0);
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000E445C File Offset: 0x000E265C
	private void SetData()
	{
		for (int i = 0; i < this.games_.arrayGamesScripts.Length; i++)
		{
			if (this.games_.arrayGamesScripts[i])
			{
				gameScript gameScript = this.games_.arrayGamesScripts[i];
				if (gameScript && gameScript.sellsPerWeek[0] > 0 && gameScript.isOnMarket && !gameScript.inDevelopment && ((this.TAB == 0 && this.games_.arrayGamesScripts[i].gameTyp != 2 && !this.games_.arrayGamesScripts[i].handy && !this.games_.arrayGamesScripts[i].arcade) || (this.TAB == 1 && this.games_.arrayGamesScripts[i].gameTyp != 2 && this.games_.arrayGamesScripts[i].handy) || (this.TAB == 2 && this.games_.arrayGamesScripts[i].gameTyp != 2 && this.games_.arrayGamesScripts[i].arcade) || (this.TAB == 3 && this.games_.arrayGamesScripts[i].gameTyp == 2)) && !this.Exists(this.uiObjects[0], gameScript.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_WochenCharts component = gameObject.GetComponent<Item_WochenCharts>();
					component.mS_ = this.mS_;
					component.tS_ = this.tS_;
					component.sfx_ = this.sfx_;
					component.guiMain_ = this.guiMain_;
					component.genres_ = this.genres_;
					component.game_ = gameScript;
					gameObject.name = gameScript.sellsPerWeek[0].ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x0000EC24 File Offset: 0x0000CE24
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000E4684 File Offset: 0x000E2884
	public void TAB_Select(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x0400196D RID: 6509
	private mainScript mS_;

	// Token: 0x0400196E RID: 6510
	private GameObject main_;

	// Token: 0x0400196F RID: 6511
	private GUI_Main guiMain_;

	// Token: 0x04001970 RID: 6512
	private sfxScript sfx_;

	// Token: 0x04001971 RID: 6513
	private textScript tS_;

	// Token: 0x04001972 RID: 6514
	private genres genres_;

	// Token: 0x04001973 RID: 6515
	private games games_;

	// Token: 0x04001974 RID: 6516
	public GameObject[] uiPrefabs;

	// Token: 0x04001975 RID: 6517
	public GameObject[] uiObjects;

	// Token: 0x04001976 RID: 6518
	private int TAB;

	// Token: 0x04001977 RID: 6519
	private float updateTimer;
}
