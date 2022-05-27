using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008F RID: 143
public class Item_DevGame_Spinoff : MonoBehaviour
{
	// Token: 0x060005A6 RID: 1446 RVA: 0x0004AD45 File Offset: 0x00048F45
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0004AD4D File Offset: 0x00048F4D
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0004AD58 File Offset: 0x00048F58
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

	// Token: 0x060005A9 RID: 1449 RVA: 0x0004ADA4 File Offset: 0x00048FA4
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetIpName();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Text>().text = this.game_.GetHypeSpinoff().ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[8].GetComponent<Text>().text = this.mS_.Round(this.game_.GetIpBekanntheit(), 1).ToString();
		this.tooltip_.c = this.game_.GetTooltipIP();
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0004AF7C File Offset: 0x0004917C
	public void BUTTON_Click()
	{
		base.gameObject.SetActive(false);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[370]);
		this.guiMain_.uiObjects[370].GetComponent<Menu_Dev_Platform_SpinOff>().Init(this.rS_, this.game_);
		this.guiMain_.uiObjects[310].SetActive(false);
	}

	// Token: 0x040008CE RID: 2254
	public gameScript game_;

	// Token: 0x040008CF RID: 2255
	public GameObject[] uiObjects;

	// Token: 0x040008D0 RID: 2256
	public mainScript mS_;

	// Token: 0x040008D1 RID: 2257
	public textScript tS_;

	// Token: 0x040008D2 RID: 2258
	public sfxScript sfx_;

	// Token: 0x040008D3 RID: 2259
	public GUI_Main guiMain_;

	// Token: 0x040008D4 RID: 2260
	public tooltip tooltip_;

	// Token: 0x040008D5 RID: 2261
	public genres genres_;

	// Token: 0x040008D6 RID: 2262
	public roomScript rS_;

	// Token: 0x040008D7 RID: 2263
	private float updateTimer;
}
