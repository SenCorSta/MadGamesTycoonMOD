using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000217 RID: 535
public class Menu_Charts_BestGamesUmsatz : MonoBehaviour
{
	// Token: 0x0600149C RID: 5276 RVA: 0x000D5DBD File Offset: 0x000D3FBD
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x000D5DC8 File Offset: 0x000D3FC8
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

	// Token: 0x0600149E RID: 5278 RVA: 0x000D5EAE File Offset: 0x000D40AE
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x000D5EE8 File Offset: 0x000D40E8
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
		this.SetData(true);
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x000D5F38 File Offset: 0x000D4138
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

	// Token: 0x060014A1 RID: 5281 RVA: 0x000D5FA3 File Offset: 0x000D41A3
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x000D5FB1 File Offset: 0x000D41B1
	public void Init()
	{
		this.FindScripts();
		this.SetData(false);
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x000D5FC0 File Offset: 0x000D41C0
	private void SetData(bool check)
	{
		this.games_.CreateAllTimeChartsUmsatz(500);
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
				gameObject.name = script_.umsatzTotal.ToString();
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x000D60F2 File Offset: 0x000D42F2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018AA RID: 6314
	private mainScript mS_;

	// Token: 0x040018AB RID: 6315
	private GameObject main_;

	// Token: 0x040018AC RID: 6316
	private GUI_Main guiMain_;

	// Token: 0x040018AD RID: 6317
	private sfxScript sfx_;

	// Token: 0x040018AE RID: 6318
	private textScript tS_;

	// Token: 0x040018AF RID: 6319
	private genres genres_;

	// Token: 0x040018B0 RID: 6320
	private games games_;

	// Token: 0x040018B1 RID: 6321
	public GameObject[] uiPrefabs;

	// Token: 0x040018B2 RID: 6322
	public GameObject[] uiObjects;

	// Token: 0x040018B3 RID: 6323
	private float updateTimer;
}
