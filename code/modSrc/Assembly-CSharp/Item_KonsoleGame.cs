using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AA RID: 170
public class Item_KonsoleGame : MonoBehaviour
{
	// Token: 0x0600063B RID: 1595 RVA: 0x0000597C File Offset: 0x00003B7C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x00005984 File Offset: 0x00003B84
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00061B94 File Offset: 0x0005FD94
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

	// Token: 0x0600063E RID: 1598 RVA: 0x00061BE0 File Offset: 0x0005FDE0
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
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.Round(this.game_.GetIpBekanntheit(), 1).ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		if (this.menu_.gameID == this.game_.myID)
		{
			base.gameObject.GetComponent<Button>().interactable = false;
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x00061DDC File Offset: 0x0005FFDC
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.menu_.SetGame(this.game_.myID);
		this.guiMain_.uiObjects[320].SetActive(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040009D2 RID: 2514
	public gameScript game_;

	// Token: 0x040009D3 RID: 2515
	public GameObject[] uiObjects;

	// Token: 0x040009D4 RID: 2516
	public mainScript mS_;

	// Token: 0x040009D5 RID: 2517
	public textScript tS_;

	// Token: 0x040009D6 RID: 2518
	public sfxScript sfx_;

	// Token: 0x040009D7 RID: 2519
	public GUI_Main guiMain_;

	// Token: 0x040009D8 RID: 2520
	public tooltip tooltip_;

	// Token: 0x040009D9 RID: 2521
	public genres genres_;

	// Token: 0x040009DA RID: 2522
	public Menu_Dev_Konsole menu_;

	// Token: 0x040009DB RID: 2523
	private float updateTimer;
}
