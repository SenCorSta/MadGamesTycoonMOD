using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021F RID: 543
public class Menu_Statistics_AllTimeAddons : MonoBehaviour
{
	// Token: 0x060014E3 RID: 5347 RVA: 0x000D776D File Offset: 0x000D596D
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000D7778 File Offset: 0x000D5978
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

	// Token: 0x060014E5 RID: 5349 RVA: 0x000D7840 File Offset: 0x000D5A40
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000D7878 File Offset: 0x000D5A78
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

	// Token: 0x060014E7 RID: 5351 RVA: 0x000D78C4 File Offset: 0x000D5AC4
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

	// Token: 0x060014E8 RID: 5352 RVA: 0x000D793A File Offset: 0x000D5B3A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000D7942 File Offset: 0x000D5B42
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x000D7950 File Offset: 0x000D5B50
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && (component.typ_addon || component.typ_mmoaddon || component.typ_addonStandalone) && !component.inDevelopment && !component.pubAngebot && component.sellsTotal > 0L && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x060014EB RID: 5355 RVA: 0x000D7AAD File Offset: 0x000D5CAD
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018EA RID: 6378
	private mainScript mS_;

	// Token: 0x040018EB RID: 6379
	private GameObject main_;

	// Token: 0x040018EC RID: 6380
	private GUI_Main guiMain_;

	// Token: 0x040018ED RID: 6381
	private sfxScript sfx_;

	// Token: 0x040018EE RID: 6382
	private textScript tS_;

	// Token: 0x040018EF RID: 6383
	private genres genres_;

	// Token: 0x040018F0 RID: 6384
	public GameObject[] uiPrefabs;

	// Token: 0x040018F1 RID: 6385
	public GameObject[] uiObjects;

	// Token: 0x040018F2 RID: 6386
	private float updateTimer;
}
