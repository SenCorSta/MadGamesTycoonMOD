using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023B RID: 571
public class Menu_Stats_Finanzverlauf : MonoBehaviour
{
	// Token: 0x060015ED RID: 5613 RVA: 0x0000F19A File Offset: 0x0000D39A
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x000E9190 File Offset: 0x000E7390
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
		if (!this.eF_)
		{
			this.eF_ = this.main_.GetComponent<engineFeatures>();
		}
		if (!this.genres_)
		{
			this.genres_ = this.main_.GetComponent<genres>();
		}
	}

	// Token: 0x060015EF RID: 5615 RVA: 0x0000F1A2 File Offset: 0x0000D3A2
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x0000F1AA File Offset: 0x0000D3AA
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x000E9278 File Offset: 0x000E7478
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

	// Token: 0x060015F2 RID: 5618 RVA: 0x000E92C4 File Offset: 0x000E74C4
	private bool Exists(GameObject parent_, int id_)
	{
		for (int i = 0; i < parent_.transform.childCount; i++)
		{
			if (parent_.transform.GetChild(i).GetComponent<Item_FinanzVerlauf>().index == id_)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x000E9304 File Offset: 0x000E7504
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x060015F4 RID: 5620 RVA: 0x000E9358 File Offset: 0x000E7558
	private void SetData()
	{
		for (int i = 0; i < this.mS_.finanzVerlaufEinnahmen.Count; i++)
		{
			if (!this.Exists(this.uiObjects[0], i))
			{
				Item_FinanzVerlauf component = UnityEngine.Object.Instantiate<GameObject>(this.uiPrefabs[0], new Vector3(0f, 0f, 0f), Quaternion.identity, this.uiObjects[0].transform).GetComponent<Item_FinanzVerlauf>();
				component.mS_ = this.mS_;
				component.tS_ = this.tS_;
				component.sfx_ = this.sfx_;
				component.guiMain_ = this.guiMain_;
				component.index = i;
			}
		}
		this.guiMain_.KeinEintrag(this.uiObjects[0], this.uiObjects[4]);
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x0000F1E2 File Offset: 0x0000D3E2
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040019FE RID: 6654
	private mainScript mS_;

	// Token: 0x040019FF RID: 6655
	private GameObject main_;

	// Token: 0x04001A00 RID: 6656
	private GUI_Main guiMain_;

	// Token: 0x04001A01 RID: 6657
	private sfxScript sfx_;

	// Token: 0x04001A02 RID: 6658
	private textScript tS_;

	// Token: 0x04001A03 RID: 6659
	private engineFeatures eF_;

	// Token: 0x04001A04 RID: 6660
	private genres genres_;

	// Token: 0x04001A05 RID: 6661
	public GameObject[] uiPrefabs;

	// Token: 0x04001A06 RID: 6662
	public GameObject[] uiObjects;

	// Token: 0x04001A07 RID: 6663
	private float updateTimer;
}
