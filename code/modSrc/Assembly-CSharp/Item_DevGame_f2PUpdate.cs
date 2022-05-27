using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000092 RID: 146
public class Item_DevGame_f2PUpdate : MonoBehaviour
{
	// Token: 0x060005B5 RID: 1461 RVA: 0x0004B266 File Offset: 0x00049466
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0004B26E File Offset: 0x0004946E
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0004B278 File Offset: 0x00049478
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

	// Token: 0x060005B8 RID: 1464 RVA: 0x0004B2C4 File Offset: 0x000494C4
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(697) + ": " + this.mS_.GetMoney(this.game_.sellsTotalOnline, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(1392) + ": " + this.mS_.GetMoney((long)this.game_.abonnements, false);
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[9].GetComponent<Text>().text = Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0004B46C File Offset: 0x0004966C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[300]);
		this.guiMain_.uiObjects[300].GetComponent<Menu_Dev_F2PUpdate>().Init(this.rS_, this.game_);
	}

	// Token: 0x040008E5 RID: 2277
	public gameScript game_;

	// Token: 0x040008E6 RID: 2278
	public GameObject[] uiObjects;

	// Token: 0x040008E7 RID: 2279
	public mainScript mS_;

	// Token: 0x040008E8 RID: 2280
	public textScript tS_;

	// Token: 0x040008E9 RID: 2281
	public sfxScript sfx_;

	// Token: 0x040008EA RID: 2282
	public GUI_Main guiMain_;

	// Token: 0x040008EB RID: 2283
	public tooltip tooltip_;

	// Token: 0x040008EC RID: 2284
	public genres genres_;

	// Token: 0x040008ED RID: 2285
	public roomScript rS_;

	// Token: 0x040008EE RID: 2286
	private float updateTimer;
}
