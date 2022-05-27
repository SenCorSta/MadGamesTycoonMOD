using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007E RID: 126
public class Item_DevGame_Addon : MonoBehaviour
{
	// Token: 0x0600052C RID: 1324 RVA: 0x000053A2 File Offset: 0x000035A2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000053AA File Offset: 0x000035AA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0005AFB8 File Offset: 0x000591B8
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

	// Token: 0x0600052F RID: 1327 RVA: 0x0005B004 File Offset: 0x00059204
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

	// Token: 0x06000530 RID: 1328 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0005B1C8 File Offset: 0x000593C8
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
