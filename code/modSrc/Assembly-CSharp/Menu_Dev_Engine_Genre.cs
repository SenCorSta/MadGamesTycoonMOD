using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000110 RID: 272
public class Menu_Dev_Engine_Genre : MonoBehaviour
{
	// Token: 0x060008DD RID: 2269 RVA: 0x00060546 File Offset: 0x0005E746
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00060550 File Offset: 0x0005E750
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

	// Token: 0x060008DF RID: 2271 RVA: 0x00060618 File Offset: 0x0005E818
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00060650 File Offset: 0x0005E850
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

	// Token: 0x060008E1 RID: 2273 RVA: 0x0006069C File Offset: 0x0005E89C
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).gameObject.activeSelf && parent_.transform.GetChild(i).GetComponent<Item_DevEngine_Genre>().myID == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x000606F3 File Offset: 0x0005E8F3
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00060704 File Offset: 0x0005E904
	private void SetData()
	{
		for (int i = 0; i < this.genres_.genres_RES_POINTS.Length; i++)
		{
			if (this.genres_.genres_UNLOCK[i] && this.genres_.IsErforscht(i) && !this.Exists(this.uiObjects[0], i))
			{
				Item_DevEngine_Genre component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_DevEngine_Genre>();
				component.myID = i;
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.genres_ = this.genres_;
			}
		}
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x000607F8 File Offset: 0x0005E9F8
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000D56 RID: 3414
	private mainScript mS_;

	// Token: 0x04000D57 RID: 3415
	private GameObject main_;

	// Token: 0x04000D58 RID: 3416
	private GUI_Main guiMain_;

	// Token: 0x04000D59 RID: 3417
	private sfxScript sfx_;

	// Token: 0x04000D5A RID: 3418
	private textScript tS_;

	// Token: 0x04000D5B RID: 3419
	private genres genres_;

	// Token: 0x04000D5C RID: 3420
	public GameObject[] uiPrefabs;

	// Token: 0x04000D5D RID: 3421
	public GameObject[] uiObjects;

	// Token: 0x04000D5E RID: 3422
	private float updateTimer;
}
