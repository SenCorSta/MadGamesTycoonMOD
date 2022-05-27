using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200022E RID: 558
public class Menu_Statistics_WochenCharts : MonoBehaviour
{
	// Token: 0x06001585 RID: 5509 RVA: 0x000DBA63 File Offset: 0x000D9C63
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x000DBA6C File Offset: 0x000D9C6C
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

	// Token: 0x06001587 RID: 5511 RVA: 0x000DBB52 File Offset: 0x000D9D52
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x000DBB8C File Offset: 0x000D9D8C
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

	// Token: 0x06001589 RID: 5513 RVA: 0x000DBBD8 File Offset: 0x000D9DD8
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

	// Token: 0x0600158A RID: 5514 RVA: 0x000DBC49 File Offset: 0x000D9E49
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x000DBC51 File Offset: 0x000D9E51
	public void Init()
	{
		this.FindScripts();
		this.TAB_Select(0);
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x000DBC60 File Offset: 0x000D9E60
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

	// Token: 0x0600158D RID: 5517 RVA: 0x000DBE87 File Offset: 0x000DA087
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x000DBEA4 File Offset: 0x000DA0A4
	public void TAB_Select(int t)
	{
		this.TAB = t;
		this.sfx_.PlaySound(3, true);
		this.guiMain_.SetTab(this.uiObjects[4], t);
		this.uiObjects[0].SetActive(false);
		this.uiObjects[0].SetActive(true);
		this.SetData();
	}

	// Token: 0x04001974 RID: 6516
	private mainScript mS_;

	// Token: 0x04001975 RID: 6517
	private GameObject main_;

	// Token: 0x04001976 RID: 6518
	private GUI_Main guiMain_;

	// Token: 0x04001977 RID: 6519
	private sfxScript sfx_;

	// Token: 0x04001978 RID: 6520
	private textScript tS_;

	// Token: 0x04001979 RID: 6521
	private genres genres_;

	// Token: 0x0400197A RID: 6522
	private games games_;

	// Token: 0x0400197B RID: 6523
	public GameObject[] uiPrefabs;

	// Token: 0x0400197C RID: 6524
	public GameObject[] uiObjects;

	// Token: 0x0400197D RID: 6525
	private int TAB;

	// Token: 0x0400197E RID: 6526
	private float updateTimer;
}
