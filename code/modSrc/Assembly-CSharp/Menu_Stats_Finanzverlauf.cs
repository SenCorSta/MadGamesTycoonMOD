using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023C RID: 572
public class Menu_Stats_Finanzverlauf : MonoBehaviour
{
	// Token: 0x0600160B RID: 5643 RVA: 0x000E1180 File Offset: 0x000DF380
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000E1188 File Offset: 0x000DF388
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

	// Token: 0x0600160D RID: 5645 RVA: 0x000E126E File Offset: 0x000DF46E
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x000E1276 File Offset: 0x000DF476
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x000E12B0 File Offset: 0x000DF4B0
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

	// Token: 0x06001610 RID: 5648 RVA: 0x000E12FC File Offset: 0x000DF4FC
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

	// Token: 0x06001611 RID: 5649 RVA: 0x000E133C File Offset: 0x000DF53C
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000E1390 File Offset: 0x000DF590
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

	// Token: 0x06001613 RID: 5651 RVA: 0x000E1458 File Offset: 0x000DF658
	public void BUTTON_Close()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04001A07 RID: 6663
	private mainScript mS_;

	// Token: 0x04001A08 RID: 6664
	private GameObject main_;

	// Token: 0x04001A09 RID: 6665
	private GUI_Main guiMain_;

	// Token: 0x04001A0A RID: 6666
	private sfxScript sfx_;

	// Token: 0x04001A0B RID: 6667
	private textScript tS_;

	// Token: 0x04001A0C RID: 6668
	private engineFeatures eF_;

	// Token: 0x04001A0D RID: 6669
	private genres genres_;

	// Token: 0x04001A0E RID: 6670
	public GameObject[] uiPrefabs;

	// Token: 0x04001A0F RID: 6671
	public GameObject[] uiObjects;

	// Token: 0x04001A10 RID: 6672
	private float updateTimer;
}
