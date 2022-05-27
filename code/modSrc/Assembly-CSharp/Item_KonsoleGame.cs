using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AA RID: 170
public class Item_KonsoleGame : MonoBehaviour
{
	// Token: 0x06000644 RID: 1604 RVA: 0x0004F070 File Offset: 0x0004D270
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0004F078 File Offset: 0x0004D278
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0004F080 File Offset: 0x0004D280
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

	// Token: 0x06000647 RID: 1607 RVA: 0x0004F0CC File Offset: 0x0004D2CC
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

	// Token: 0x06000648 RID: 1608 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0004F2C8 File Offset: 0x0004D4C8
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
