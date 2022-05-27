using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000088 RID: 136
public class Item_DevGame_MMOAddon : MonoBehaviour
{
	// Token: 0x06000578 RID: 1400 RVA: 0x000499E4 File Offset: 0x00047BE4
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x000499EC File Offset: 0x00047BEC
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x000499F4 File Offset: 0x00047BF4
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

	// Token: 0x0600057B RID: 1403 RVA: 0x00049A40 File Offset: 0x00047C40
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(1236) + ": " + this.mS_.GetMoney((long)this.game_.abonnements, false);
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Text>().text = this.game_.amountMMOAddons.ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[9].GetComponent<Text>().text = Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00049C0C File Offset: 0x00047E0C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[247]);
		this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().Init(this.rS_, this.game_);
	}

	// Token: 0x0400088F RID: 2191
	public gameScript game_;

	// Token: 0x04000890 RID: 2192
	public GameObject[] uiObjects;

	// Token: 0x04000891 RID: 2193
	public mainScript mS_;

	// Token: 0x04000892 RID: 2194
	public textScript tS_;

	// Token: 0x04000893 RID: 2195
	public sfxScript sfx_;

	// Token: 0x04000894 RID: 2196
	public GUI_Main guiMain_;

	// Token: 0x04000895 RID: 2197
	public tooltip tooltip_;

	// Token: 0x04000896 RID: 2198
	public genres genres_;

	// Token: 0x04000897 RID: 2199
	public roomScript rS_;

	// Token: 0x04000898 RID: 2200
	private float updateTimer;
}
