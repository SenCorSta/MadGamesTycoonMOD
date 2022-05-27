using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000220 RID: 544
public class Menu_Statistics_AllTimeBudget : MonoBehaviour
{
	// Token: 0x060014ED RID: 5357 RVA: 0x000D7A9C File Offset: 0x000D5C9C
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000D7AA4 File Offset: 0x000D5CA4
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
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000D7B6C File Offset: 0x000D5D6C
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000D7BA4 File Offset: 0x000D5DA4
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

	// Token: 0x060014F1 RID: 5361 RVA: 0x000D7BF0 File Offset: 0x000D5DF0
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				Item_AllTimeCharts component = parent_.transform.GetChild(i).GetComponent<Item_AllTimeCharts>();
				if (component.game_.myID == id_)
				{
					gameObject.name = component.game_.sellsTotal.ToString();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000D7C66 File Offset: 0x000D5E66
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000D7C6E File Offset: 0x000D5E6E
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000D7C7C File Offset: 0x000D5E7C
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.typ_budget && !component.inDevelopment && component.sellsTotal > 0L && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_AllTimeCharts component2 = gameObject.GetComponent<Item_AllTimeCharts>();
					component2.mS_ = this.mS_;
					component2.tS_ = this.tS_;
					component2.sfx_ = this.sfx_;
					component2.guiMain_ = this.guiMain_;
					component2.genres_ = this.genres_;
					component2.game_ = component;
					gameObject.name = component.sellsTotal.ToString();
				}
			}
		}
		this.mS_.SortChildrenByFloat(this.uiObjects[0]);
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[5]);
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x000D7DBE File Offset: 0x000D5FBE
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018F3 RID: 6387
	private mainScript mS_;

	// Token: 0x040018F4 RID: 6388
	private GameObject main_;

	// Token: 0x040018F5 RID: 6389
	private GUI_Main guiMain_;

	// Token: 0x040018F6 RID: 6390
	private sfxScript sfx_;

	// Token: 0x040018F7 RID: 6391
	private textScript tS_;

	// Token: 0x040018F8 RID: 6392
	private genres genres_;

	// Token: 0x040018F9 RID: 6393
	public GameObject[] uiPrefabs;

	// Token: 0x040018FA RID: 6394
	public GameObject[] uiObjects;

	// Token: 0x040018FB RID: 6395
	private float updateTimer;
}
