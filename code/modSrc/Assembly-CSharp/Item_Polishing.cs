using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A4 RID: 164
public class Item_Polishing : MonoBehaviour
{
	// Token: 0x0600061D RID: 1565 RVA: 0x00005896 File Offset: 0x00003A96
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0000589E File Offset: 0x00003A9E
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x00060F34 File Offset: 0x0005F134
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

	// Token: 0x06000620 RID: 1568 RVA: 0x00060F80 File Offset: 0x0005F180
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x000058A6 File Offset: 0x00003AA6
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.uiObjects[279].GetComponent<Menu_ROOM_Polishing>().StartPolishing(this.game_);
	}

	// Token: 0x0400099F RID: 2463
	public gameScript game_;

	// Token: 0x040009A0 RID: 2464
	public GameObject[] uiObjects;

	// Token: 0x040009A1 RID: 2465
	public mainScript mS_;

	// Token: 0x040009A2 RID: 2466
	public textScript tS_;

	// Token: 0x040009A3 RID: 2467
	public sfxScript sfx_;

	// Token: 0x040009A4 RID: 2468
	public GUI_Main guiMain_;

	// Token: 0x040009A5 RID: 2469
	public tooltip tooltip_;

	// Token: 0x040009A6 RID: 2470
	public genres genres_;

	// Token: 0x040009A7 RID: 2471
	public roomScript rS_;

	// Token: 0x040009A8 RID: 2472
	private float updateTimer;
}
