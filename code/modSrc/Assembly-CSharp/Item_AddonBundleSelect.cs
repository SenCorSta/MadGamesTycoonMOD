using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CB RID: 203
public class Item_AddonBundleSelect : MonoBehaviour
{
	// Token: 0x060006FA RID: 1786 RVA: 0x00005DE7 File Offset: 0x00003FE7
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00005DEF File Offset: 0x00003FEF
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0006581C File Offset: 0x00063A1C
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

	// Token: 0x060006FD RID: 1789 RVA: 0x00065868 File Offset: 0x00063A68
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x000659C4 File Offset: 0x00063BC4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.game_)
		{
			this.menu_.SetGame(this.guiMain_.uiObjects[272].GetComponent<Menu_AddonBundleSelect>().slot, this.game_);
		}
		this.guiMain_.uiObjects[272].SetActive(false);
	}

	// Token: 0x04000ACB RID: 2763
	public gameScript game_;

	// Token: 0x04000ACC RID: 2764
	public GameObject[] uiObjects;

	// Token: 0x04000ACD RID: 2765
	public mainScript mS_;

	// Token: 0x04000ACE RID: 2766
	public textScript tS_;

	// Token: 0x04000ACF RID: 2767
	public sfxScript sfx_;

	// Token: 0x04000AD0 RID: 2768
	public GUI_Main guiMain_;

	// Token: 0x04000AD1 RID: 2769
	public tooltip tooltip_;

	// Token: 0x04000AD2 RID: 2770
	public genres genres_;

	// Token: 0x04000AD3 RID: 2771
	public Menu_AddonBundle menu_;

	// Token: 0x04000AD4 RID: 2772
	private float updateTimer;
}
