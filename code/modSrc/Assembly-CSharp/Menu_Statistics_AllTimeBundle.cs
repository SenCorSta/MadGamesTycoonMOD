using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000221 RID: 545
public class Menu_Statistics_AllTimeBundle : MonoBehaviour
{
	// Token: 0x060014F7 RID: 5367 RVA: 0x000D7E05 File Offset: 0x000D6005
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x000D7E10 File Offset: 0x000D6010
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

	// Token: 0x060014F9 RID: 5369 RVA: 0x000D7ED8 File Offset: 0x000D60D8
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x000D7F10 File Offset: 0x000D6110
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

	// Token: 0x060014FB RID: 5371 RVA: 0x000D7F5C File Offset: 0x000D615C
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

	// Token: 0x060014FC RID: 5372 RVA: 0x000D7FD2 File Offset: 0x000D61D2
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x000D7FDA File Offset: 0x000D61DA
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000D7FE8 File Offset: 0x000D61E8
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && (component.typ_bundle || component.typ_bundleAddon) && !component.inDevelopment && component.sellsTotal > 0L && !this.Exists(this.uiObjects[0], component.myID))
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

	// Token: 0x060014FF RID: 5375 RVA: 0x000D8132 File Offset: 0x000D6332
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040018FC RID: 6396
	private mainScript mS_;

	// Token: 0x040018FD RID: 6397
	private GameObject main_;

	// Token: 0x040018FE RID: 6398
	private GUI_Main guiMain_;

	// Token: 0x040018FF RID: 6399
	private sfxScript sfx_;

	// Token: 0x04001900 RID: 6400
	private textScript tS_;

	// Token: 0x04001901 RID: 6401
	private genres genres_;

	// Token: 0x04001902 RID: 6402
	public GameObject[] uiPrefabs;

	// Token: 0x04001903 RID: 6403
	public GameObject[] uiObjects;

	// Token: 0x04001904 RID: 6404
	private float updateTimer;
}
