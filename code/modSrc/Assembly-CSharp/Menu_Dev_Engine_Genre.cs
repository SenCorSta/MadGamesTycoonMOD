using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200010F RID: 271
public class Menu_Dev_Engine_Genre : MonoBehaviour
{
	// Token: 0x060008CE RID: 2254 RVA: 0x00006861 File Offset: 0x00004A61
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x00071C18 File Offset: 0x0006FE18
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

	// Token: 0x060008D0 RID: 2256 RVA: 0x00006869 File Offset: 0x00004A69
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00071CE0 File Offset: 0x0006FEE0
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

	// Token: 0x060008D2 RID: 2258 RVA: 0x00071D2C File Offset: 0x0006FF2C
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

	// Token: 0x060008D3 RID: 2259 RVA: 0x000068A1 File Offset: 0x00004AA1
	public void Init()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x00071D84 File Offset: 0x0006FF84
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

	// Token: 0x060008D5 RID: 2261 RVA: 0x000068AF File Offset: 0x00004AAF
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000D4E RID: 3406
	private mainScript mS_;

	// Token: 0x04000D4F RID: 3407
	private GameObject main_;

	// Token: 0x04000D50 RID: 3408
	private GUI_Main guiMain_;

	// Token: 0x04000D51 RID: 3409
	private sfxScript sfx_;

	// Token: 0x04000D52 RID: 3410
	private textScript tS_;

	// Token: 0x04000D53 RID: 3411
	private genres genres_;

	// Token: 0x04000D54 RID: 3412
	public GameObject[] uiPrefabs;

	// Token: 0x04000D55 RID: 3413
	public GameObject[] uiObjects;

	// Token: 0x04000D56 RID: 3414
	private float updateTimer;
}
