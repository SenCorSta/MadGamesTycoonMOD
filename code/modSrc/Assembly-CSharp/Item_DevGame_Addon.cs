using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007E RID: 126
public class Item_DevGame_Addon : MonoBehaviour
{
	// Token: 0x06000535 RID: 1333 RVA: 0x00047DDF File Offset: 0x00045FDF
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00047DE7 File Offset: 0x00045FE7
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00047DF0 File Offset: 0x00045FF0
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

	// Token: 0x06000538 RID: 1336 RVA: 0x00047E3C File Offset: 0x0004603C
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
		this.uiObjects[6].GetComponent<Text>().text = this.game_.amountAddons.ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00048000 File Offset: 0x00046200
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[193]);
		this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().Init(this.rS_, this.game_);
	}

	// Token: 0x04000830 RID: 2096
	public gameScript game_;

	// Token: 0x04000831 RID: 2097
	public GameObject[] uiObjects;

	// Token: 0x04000832 RID: 2098
	public mainScript mS_;

	// Token: 0x04000833 RID: 2099
	public textScript tS_;

	// Token: 0x04000834 RID: 2100
	public sfxScript sfx_;

	// Token: 0x04000835 RID: 2101
	public GUI_Main guiMain_;

	// Token: 0x04000836 RID: 2102
	public tooltip tooltip_;

	// Token: 0x04000837 RID: 2103
	public genres genres_;

	// Token: 0x04000838 RID: 2104
	public roomScript rS_;

	// Token: 0x04000839 RID: 2105
	private float updateTimer;
}
