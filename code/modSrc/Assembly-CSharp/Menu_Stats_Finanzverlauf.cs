using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200023C RID: 572
public class Menu_Stats_Finanzverlauf : MonoBehaviour
{
	// Token: 0x0600160B RID: 5643 RVA: 0x000E1154 File Offset: 0x000DF354
	private void Start()
	{
		this.FindScripts();
	}

	// Token: 0x0600160C RID: 5644 RVA: 0x000E115C File Offset: 0x000DF35C
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

	// Token: 0x0600160D RID: 5645 RVA: 0x000E1242 File Offset: 0x000DF442
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x000E124A File Offset: 0x000DF44A
	private void Update()
	{
		if (this.uiObjects[2].GetComponent<Animation>().IsPlaying("openMenu"))
		{
			this.uiObjects[3].GetComponent<Scrollbar>().value = 1f;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x0600160F RID: 5647 RVA: 0x000E1284 File Offset: 0x000DF484
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

	// Token: 0x06001610 RID: 5648 RVA: 0x000E12D0 File Offset: 0x000DF4D0
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

	// Token: 0x06001611 RID: 5649 RVA: 0x000E1310 File Offset: 0x000DF510
	public void Init()
	{
		this.FindScripts();
		for (int i = 0; i < this.uiObjects[0].transform.childCount; i++)
		{
			this.uiObjects[0].transform.GetChild(i).gameObject.SetActive(false);
		}
		this.SetData();
	}

	// Token: 0x06001612 RID: 5650 RVA: 0x000E1364 File Offset: 0x000DF564
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

	// Token: 0x06001613 RID: 5651 RVA: 0x000E142C File Offset: 0x000DF62C
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
