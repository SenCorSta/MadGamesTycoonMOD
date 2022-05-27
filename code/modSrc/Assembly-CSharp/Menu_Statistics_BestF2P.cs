using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000224 RID: 548
public class Menu_Statistics_BestF2P : MonoBehaviour
{
	// Token: 0x0600150E RID: 5390 RVA: 0x000D8842 File Offset: 0x000D6A42
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000D884C File Offset: 0x000D6A4C
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

	// Token: 0x06001510 RID: 5392 RVA: 0x000D8914 File Offset: 0x000D6B14
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x000D894C File Offset: 0x000D6B4C
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

	// Token: 0x06001512 RID: 5394 RVA: 0x000D8998 File Offset: 0x000D6B98
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

	// Token: 0x06001513 RID: 5395 RVA: 0x000D8A0E File Offset: 0x000D6C0E
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000D8A16 File Offset: 0x000D6C16
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000D8A24 File Offset: 0x000D6C24
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

	// Token: 0x06001516 RID: 5398 RVA: 0x000D8B72 File Offset: 0x000D6D72
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001911 RID: 6417
	private mainScript mS_;

	// Token: 0x04001912 RID: 6418
	private GameObject main_;

	// Token: 0x04001913 RID: 6419
	private GUI_Main guiMain_;

	// Token: 0x04001914 RID: 6420
	private sfxScript sfx_;

	// Token: 0x04001915 RID: 6421
	private textScript tS_;

	// Token: 0x04001916 RID: 6422
	private genres genres_;

	// Token: 0x04001917 RID: 6423
	public GameObject[] uiPrefabs;

	// Token: 0x04001918 RID: 6424
	public GameObject[] uiObjects;

	// Token: 0x04001919 RID: 6425
	private float updateTimer;
}
