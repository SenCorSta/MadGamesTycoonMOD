using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CE RID: 206
public class Item_BundleSelect : MonoBehaviour
{
	// Token: 0x0600070F RID: 1807 RVA: 0x00005E4A File Offset: 0x0000404A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x00005E52 File Offset: 0x00004052
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00065F30 File Offset: 0x00064130
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

	// Token: 0x06000712 RID: 1810 RVA: 0x00065F7C File Offset: 0x0006417C
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

	// Token: 0x06000713 RID: 1811 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x000660D8 File Offset: 0x000642D8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.game_)
		{
			this.menu_.SetGame(this.guiMain_.uiObjects[268].GetComponent<Menu_BundleSelect>().slot, this.game_);
		}
		this.guiMain_.uiObjects[268].SetActive(false);
	}

	// Token: 0x04000AE9 RID: 2793
	public gameScript game_;

	// Token: 0x04000AEA RID: 2794
	public GameObject[] uiObjects;

	// Token: 0x04000AEB RID: 2795
	public mainScript mS_;

	// Token: 0x04000AEC RID: 2796
	public textScript tS_;

	// Token: 0x04000AED RID: 2797
	public sfxScript sfx_;

	// Token: 0x04000AEE RID: 2798
	public GUI_Main guiMain_;

	// Token: 0x04000AEF RID: 2799
	public tooltip tooltip_;

	// Token: 0x04000AF0 RID: 2800
	public genres genres_;

	// Token: 0x04000AF1 RID: 2801
	public Menu_Bundle menu_;

	// Token: 0x04000AF2 RID: 2802
	private float updateTimer;
}
