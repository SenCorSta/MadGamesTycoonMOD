using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C1 RID: 193
public class Item_Marketing_Game : MonoBehaviour
{
	// Token: 0x060006BC RID: 1724 RVA: 0x00005CD0 File Offset: 0x00003ED0
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x000643E8 File Offset: 0x000625E8
	private void Update()
	{
		if (this.game_ && !this.game_.inDevelopment && !this.game_.isOnMarket && !this.game_.schublade)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0006443C File Offset: 0x0006263C
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

	// Token: 0x060006BF RID: 1727 RVA: 0x00064488 File Offset: 0x00062688
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.game_.GetHype()).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00064520 File Offset: 0x00062720
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[90].SetActive(false);
		this.guiMain_.uiObjects[89].GetComponent<Menu_Marketing_GameKampagne>().SetGame(this.game_);
	}

	// Token: 0x04000A79 RID: 2681
	public GameObject[] uiObjects;

	// Token: 0x04000A7A RID: 2682
	public mainScript mS_;

	// Token: 0x04000A7B RID: 2683
	public textScript tS_;

	// Token: 0x04000A7C RID: 2684
	public sfxScript sfx_;

	// Token: 0x04000A7D RID: 2685
	public GUI_Main guiMain_;

	// Token: 0x04000A7E RID: 2686
	public tooltip tooltip_;

	// Token: 0x04000A7F RID: 2687
	public gameScript game_;

	// Token: 0x04000A80 RID: 2688
	public genres genres_;

	// Token: 0x04000A81 RID: 2689
	private float updateTimer;
}
