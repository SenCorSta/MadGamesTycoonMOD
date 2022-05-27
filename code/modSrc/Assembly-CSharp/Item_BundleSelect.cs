using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CE RID: 206
public class Item_BundleSelect : MonoBehaviour
{
	// Token: 0x06000718 RID: 1816 RVA: 0x000539A8 File Offset: 0x00051BA8
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000539B0 File Offset: 0x00051BB0
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000539B8 File Offset: 0x00051BB8
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

	// Token: 0x0600071B RID: 1819 RVA: 0x00053A04 File Offset: 0x00051C04
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

	// Token: 0x0600071C RID: 1820 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00053B60 File Offset: 0x00051D60
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
