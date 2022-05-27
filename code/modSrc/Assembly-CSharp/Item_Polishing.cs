using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A4 RID: 164
public class Item_Polishing : MonoBehaviour
{
	// Token: 0x06000626 RID: 1574 RVA: 0x0004E326 File Offset: 0x0004C526
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0004E32E File Offset: 0x0004C52E
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0004E338 File Offset: 0x0004C538
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

	// Token: 0x06000629 RID: 1577 RVA: 0x0004E384 File Offset: 0x0004C584
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

	// Token: 0x0600062A RID: 1578 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0004E434 File Offset: 0x0004C634
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
