using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000089 RID: 137
public class Item_DevGame_Nachfolger : MonoBehaviour
{
	// Token: 0x0600057F RID: 1407 RVA: 0x00049C69 File Offset: 0x00047E69
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00049C71 File Offset: 0x00047E71
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00049C7C File Offset: 0x00047E7C
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

	// Token: 0x06000582 RID: 1410 RVA: 0x00049CC8 File Offset: 0x00047EC8
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
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Text>().text = this.game_.GetHypeNachfolger().ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.Round(this.game_.GetIpBekanntheit(), 1).ToString();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00049EA0 File Offset: 0x000480A0
	public void BUTTON_Click()
	{
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[369]);
		this.guiMain_.uiObjects[369].GetComponent<Menu_Dev_Platform_Nachfolger>().Init(this.rS_, this.game_);
		this.guiMain_.uiObjects[97].SetActive(false);
	}

	// Token: 0x04000899 RID: 2201
	public gameScript game_;

	// Token: 0x0400089A RID: 2202
	public GameObject[] uiObjects;

	// Token: 0x0400089B RID: 2203
	public mainScript mS_;

	// Token: 0x0400089C RID: 2204
	public textScript tS_;

	// Token: 0x0400089D RID: 2205
	public sfxScript sfx_;

	// Token: 0x0400089E RID: 2206
	public GUI_Main guiMain_;

	// Token: 0x0400089F RID: 2207
	public tooltip tooltip_;

	// Token: 0x040008A0 RID: 2208
	public genres genres_;

	// Token: 0x040008A1 RID: 2209
	public roomScript rS_;

	// Token: 0x040008A2 RID: 2210
	private float updateTimer;
}
