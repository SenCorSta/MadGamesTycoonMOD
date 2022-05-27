using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200021E RID: 542
public class Menu_Statistics_AllTimeAddons : MonoBehaviour
{
	// Token: 0x060014C5 RID: 5317 RVA: 0x0000E21A File Offset: 0x0000C41A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x000E0A2C File Offset: 0x000DEC2C
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

	// Token: 0x060014C7 RID: 5319 RVA: 0x0000E222 File Offset: 0x0000C422
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000E0AF4 File Offset: 0x000DECF4
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

	// Token: 0x060014C9 RID: 5321 RVA: 0x000E0B40 File Offset: 0x000DED40
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

	// Token: 0x060014CA RID: 5322 RVA: 0x0000E25A File Offset: 0x0000C45A
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x0000E262 File Offset: 0x0000C462
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000E0BB8 File Offset: 0x000DEDB8
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

	// Token: 0x060014CD RID: 5325 RVA: 0x0000E270 File Offset: 0x0000C470
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018E3 RID: 6371
	private mainScript mS_;

	// Token: 0x040018E4 RID: 6372
	private GameObject main_;

	// Token: 0x040018E5 RID: 6373
	private GUI_Main guiMain_;

	// Token: 0x040018E6 RID: 6374
	private sfxScript sfx_;

	// Token: 0x040018E7 RID: 6375
	private textScript tS_;

	// Token: 0x040018E8 RID: 6376
	private genres genres_;

	// Token: 0x040018E9 RID: 6377
	public GameObject[] uiPrefabs;

	// Token: 0x040018EA RID: 6378
	public GameObject[] uiObjects;

	// Token: 0x040018EB RID: 6379
	private float updateTimer;
}
