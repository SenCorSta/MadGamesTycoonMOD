using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008D RID: 141
public class Item_DevGame_Remaster : MonoBehaviour
{
	// Token: 0x06000597 RID: 1431 RVA: 0x0004A7B4 File Offset: 0x000489B4
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0004A7BC File Offset: 0x000489BC
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0004A7C4 File Offset: 0x000489C4
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

	// Token: 0x0600059A RID: 1434 RVA: 0x0004A810 File Offset: 0x00048A10
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
		this.uiObjects[6].GetComponent<Image>().sprite = this.games_.gameSizeSprites[this.game_.gameSize];
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.Round(this.game_.GetIpBekanntheit(), 1).ToString();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0004A9EC File Offset: 0x00048BEC
	public void BUTTON_Click()
	{
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[56]);
		this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().InitRemaster(this.rS_, this.game_.myID);
		this.guiMain_.uiObjects[98].SetActive(false);
	}

	// Token: 0x040008BA RID: 2234
	public gameScript game_;

	// Token: 0x040008BB RID: 2235
	public GameObject[] uiObjects;

	// Token: 0x040008BC RID: 2236
	public mainScript mS_;

	// Token: 0x040008BD RID: 2237
	public textScript tS_;

	// Token: 0x040008BE RID: 2238
	public sfxScript sfx_;

	// Token: 0x040008BF RID: 2239
	public GUI_Main guiMain_;

	// Token: 0x040008C0 RID: 2240
	public tooltip tooltip_;

	// Token: 0x040008C1 RID: 2241
	public genres genres_;

	// Token: 0x040008C2 RID: 2242
	public roomScript rS_;

	// Token: 0x040008C3 RID: 2243
	public games games_;

	// Token: 0x040008C4 RID: 2244
	private float updateTimer;
}
