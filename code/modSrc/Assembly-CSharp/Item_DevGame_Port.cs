using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008B RID: 139
public class Item_DevGame_Port : MonoBehaviour
{
	// Token: 0x06000584 RID: 1412 RVA: 0x000054FB File Offset: 0x000036FB
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00005503 File Offset: 0x00003703
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0005D568 File Offset: 0x0005B768
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

	// Token: 0x06000587 RID: 1415 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
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
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[9].GetComponent<Text>().text = this.mS_.Round(this.game_.GetIpBekanntheit(), 1).ToString();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0005D760 File Offset: 0x0005B960
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[313]);
		this.guiMain_.uiObjects[313].GetComponent<Menu_Dev_PortChoosePlatform>().Init(this.rS_, this.game_);
	}

	// Token: 0x040008AE RID: 2222
	public gameScript game_;

	// Token: 0x040008AF RID: 2223
	public GameObject[] uiObjects;

	// Token: 0x040008B0 RID: 2224
	public mainScript mS_;

	// Token: 0x040008B1 RID: 2225
	public textScript tS_;

	// Token: 0x040008B2 RID: 2226
	public sfxScript sfx_;

	// Token: 0x040008B3 RID: 2227
	public GUI_Main guiMain_;

	// Token: 0x040008B4 RID: 2228
	public tooltip tooltip_;

	// Token: 0x040008B5 RID: 2229
	public genres genres_;

	// Token: 0x040008B6 RID: 2230
	public roomScript rS_;

	// Token: 0x040008B7 RID: 2231
	private float updateTimer;
}
