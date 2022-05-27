using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000223 RID: 547
public class Menu_Statistics_BestF2P : MonoBehaviour
{
	// Token: 0x060014F0 RID: 5360 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000E17F8 File Offset: 0x000DF9F8
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

	// Token: 0x060014F2 RID: 5362 RVA: 0x0000E400 File Offset: 0x0000C600
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000E18C0 File Offset: 0x000DFAC0
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

	// Token: 0x060014F4 RID: 5364 RVA: 0x000E190C File Offset: 0x000DFB0C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			GameObject gameObject = parent_.transform.GetChild(i).gameObject;
			if (gameObject.activeSelf)
			{
				Item_BestF2P component = parent_.transform.GetChild(i).GetComponent<Item_BestF2P>();
				if (component.game_.myID == id_)
				{
					gameObject.name = component.game_.sellsTotal.ToString();
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x0000E438 File Offset: 0x0000C638
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x0000E440 File Offset: 0x0000C640
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x000E1984 File Offset: 0x000DFB84
	private void SetData()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Game");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				gameScript component = array[i].GetComponent<gameScript>();
				if (component && component.gameTyp == 2 && !component.inDevelopment && component.isOnMarket && component.sellsTotal > 0L && !this.Exists(this.uiObjects[0], component.myID))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform);
					Item_BestF2P component2 = gameObject.GetComponent<Item_BestF2P>();
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

	// Token: 0x060014F8 RID: 5368 RVA: 0x0000E44E File Offset: 0x0000C64E
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400190A RID: 6410
	private mainScript mS_;

	// Token: 0x0400190B RID: 6411
	private GameObject main_;

	// Token: 0x0400190C RID: 6412
	private GUI_Main guiMain_;

	// Token: 0x0400190D RID: 6413
	private sfxScript sfx_;

	// Token: 0x0400190E RID: 6414
	private textScript tS_;

	// Token: 0x0400190F RID: 6415
	private genres genres_;

	// Token: 0x04001910 RID: 6416
	public GameObject[] uiPrefabs;

	// Token: 0x04001911 RID: 6417
	public GameObject[] uiObjects;

	// Token: 0x04001912 RID: 6418
	private float updateTimer;
}
