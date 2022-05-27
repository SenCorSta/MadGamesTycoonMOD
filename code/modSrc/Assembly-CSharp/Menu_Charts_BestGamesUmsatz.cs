using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000216 RID: 534
public class Menu_Charts_BestGamesUmsatz : MonoBehaviour
{
	// Token: 0x0600147E RID: 5246 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000DF4C4 File Offset: 0x000DD6C4
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

	// Token: 0x06001480 RID: 5248 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x000DF5AC File Offset: 0x000DD7AC
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

	// Token: 0x06001482 RID: 5250 RVA: 0x000DF5FC File Offset: 0x000DD7FC
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

	// Token: 0x06001483 RID: 5251 RVA: 0x0000DF00 File Offset: 0x0000C100
	private void OnEnable()
	{
		this.FindScripts();
		this.Init();
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x0000DF0E File Offset: 0x0000C10E
	public void Init()
	{
		this.FindScripts();
		this.SetData(false);
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x000DF668 File Offset: 0x000DD868
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

	// Token: 0x06001486 RID: 5254 RVA: 0x0000DF1D File Offset: 0x0000C11D
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018A3 RID: 6307
	private mainScript mS_;

	// Token: 0x040018A4 RID: 6308
	private GameObject main_;

	// Token: 0x040018A5 RID: 6309
	private GUI_Main guiMain_;

	// Token: 0x040018A6 RID: 6310
	private sfxScript sfx_;

	// Token: 0x040018A7 RID: 6311
	private textScript tS_;

	// Token: 0x040018A8 RID: 6312
	private genres genres_;

	// Token: 0x040018A9 RID: 6313
	private games games_;

	// Token: 0x040018AA RID: 6314
	public GameObject[] uiPrefabs;

	// Token: 0x040018AB RID: 6315
	public GameObject[] uiObjects;

	// Token: 0x040018AC RID: 6316
	private float updateTimer;
}
