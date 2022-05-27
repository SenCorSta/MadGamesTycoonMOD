using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021D RID: 541
public class Menu_StatisticsAllTimeChartsHandy : MonoBehaviour
{
	// Token: 0x060014BB RID: 5307 RVA: 0x0000E1A9 File Offset: 0x0000C3A9
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014BC RID: 5308 RVA: 0x000E07C4 File Offset: 0x000DE9C4
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

	// Token: 0x060014BD RID: 5309 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014BE RID: 5310 RVA: 0x000E08AC File Offset: 0x000DEAAC
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

	// Token: 0x060014BF RID: 5311 RVA: 0x000DF5FC File Offset: 0x000DD7FC
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

	// Token: 0x060014C0 RID: 5312 RVA: 0x0000E1E9 File Offset: 0x0000C3E9
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014C1 RID: 5313 RVA: 0x0000E1F1 File Offset: 0x0000C3F1
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000E08F8 File Offset: 0x000DEAF8
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

	// Token: 0x060014C3 RID: 5315 RVA: 0x0000E1FF File Offset: 0x0000C3FF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018D9 RID: 6361
	private mainScript mS_;

	// Token: 0x040018DA RID: 6362
	private GameObject main_;

	// Token: 0x040018DB RID: 6363
	private GUI_Main guiMain_;

	// Token: 0x040018DC RID: 6364
	private sfxScript sfx_;

	// Token: 0x040018DD RID: 6365
	private textScript tS_;

	// Token: 0x040018DE RID: 6366
	private genres genres_;

	// Token: 0x040018DF RID: 6367
	private games games_;

	// Token: 0x040018E0 RID: 6368
	public GameObject[] uiPrefabs;

	// Token: 0x040018E1 RID: 6369
	public GameObject[] uiObjects;

	// Token: 0x040018E2 RID: 6370
	private float updateTimer;
}
