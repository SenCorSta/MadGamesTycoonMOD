using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C0 RID: 192
public class Item_MarketingSpezial_Game : MonoBehaviour
{
	// Token: 0x060006B5 RID: 1717 RVA: 0x00005CC8 File Offset: 0x00003EC8
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00064164 File Offset: 0x00062364
	private void Update()
	{
		if (this.game_ && !this.game_.inDevelopment && !this.game_.isOnMarket && !this.game_.schublade)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.MultiplayerUpdate();
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x000641B8 File Offset: 0x000623B8
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

	// Token: 0x060006B8 RID: 1720 RVA: 0x00064204 File Offset: 0x00062404
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = Mathf.RoundToInt(this.game_.GetHype()).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		if (this.game_.specialMarketing[0] == 0)
		{
			this.uiObjects[3].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[1] == 0)
		{
			this.uiObjects[4].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[2] == 0)
		{
			this.uiObjects[5].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[3] == 0)
		{
			this.uiObjects[6].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		if (this.game_.specialMarketing[4] == 0)
		{
			this.uiObjects[7].GetComponent<Image>().color = this.guiMain_.colors[6];
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00064394 File Offset: 0x00062594
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[295].SetActive(false);
		this.guiMain_.uiObjects[294].GetComponent<Menu_MarketingSpezial>().SetGame(this.game_);
	}

	// Token: 0x04000A70 RID: 2672
	public GameObject[] uiObjects;

	// Token: 0x04000A71 RID: 2673
	public mainScript mS_;

	// Token: 0x04000A72 RID: 2674
	public textScript tS_;

	// Token: 0x04000A73 RID: 2675
	public sfxScript sfx_;

	// Token: 0x04000A74 RID: 2676
	public GUI_Main guiMain_;

	// Token: 0x04000A75 RID: 2677
	public tooltip tooltip_;

	// Token: 0x04000A76 RID: 2678
	public gameScript game_;

	// Token: 0x04000A77 RID: 2679
	public genres genres_;

	// Token: 0x04000A78 RID: 2680
	private float updateTimer;
}
