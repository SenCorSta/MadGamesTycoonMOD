using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000084 RID: 132
public class Item_DevGame_GameUpdate : MonoBehaviour
{
	// Token: 0x0600055F RID: 1375 RVA: 0x00048ED6 File Offset: 0x000470D6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00048EDE File Offset: 0x000470DE
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00048EE8 File Offset: 0x000470E8
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

	// Token: 0x06000562 RID: 1378 RVA: 0x00048F34 File Offset: 0x00047134
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.pubOffer)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[23];
		}
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Text>().text = this.game_.amountUpdates.ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00049128 File Offset: 0x00047328
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[108]);
		this.guiMain_.uiObjects[108].GetComponent<Menu_Dev_Update>().Init(this.rS_, this.game_);
		this.guiMain_.uiObjects[105].SetActive(false);
	}

	// Token: 0x04000869 RID: 2153
	public gameScript game_;

	// Token: 0x0400086A RID: 2154
	public GameObject[] uiObjects;

	// Token: 0x0400086B RID: 2155
	public mainScript mS_;

	// Token: 0x0400086C RID: 2156
	public textScript tS_;

	// Token: 0x0400086D RID: 2157
	public sfxScript sfx_;

	// Token: 0x0400086E RID: 2158
	public GUI_Main guiMain_;

	// Token: 0x0400086F RID: 2159
	public tooltip tooltip_;

	// Token: 0x04000870 RID: 2160
	public genres genres_;

	// Token: 0x04000871 RID: 2161
	public roomScript rS_;

	// Token: 0x04000872 RID: 2162
	private float updateTimer;
}
