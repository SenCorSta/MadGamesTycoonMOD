using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A3 RID: 163
public class Item_GFX_GrafikVerbessern : MonoBehaviour
{
	// Token: 0x0600061F RID: 1567 RVA: 0x0004E1D7 File Offset: 0x0004C3D7
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0004E1DF File Offset: 0x0004C3DF
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0004E1E8 File Offset: 0x0004C3E8
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

	// Token: 0x06000622 RID: 1570 RVA: 0x0004E234 File Offset: 0x0004C434
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.tooltip_.c = this.game_.GetTooltip();
		for (int i = 0; i < 6; i++)
		{
			if (!this.game_.grafikStudio[i])
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.grey;
			}
			else
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.white;
			}
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[175].SetActive(false);
		this.guiMain_.uiObjects[174].GetComponent<Menu_GFX_GrafikVerbessern>().SetGame(this.game_);
	}

	// Token: 0x04000997 RID: 2455
	public GameObject[] uiObjects;

	// Token: 0x04000998 RID: 2456
	public mainScript mS_;

	// Token: 0x04000999 RID: 2457
	public textScript tS_;

	// Token: 0x0400099A RID: 2458
	public sfxScript sfx_;

	// Token: 0x0400099B RID: 2459
	public GUI_Main guiMain_;

	// Token: 0x0400099C RID: 2460
	public tooltip tooltip_;

	// Token: 0x0400099D RID: 2461
	public gameScript game_;

	// Token: 0x0400099E RID: 2462
	private float updateTimer;
}
