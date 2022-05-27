using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D1 RID: 209
public class Item_InAppVerwalten : MonoBehaviour
{
	// Token: 0x06000724 RID: 1828 RVA: 0x00005EAD File Offset: 0x000040AD
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00005EB5 File Offset: 0x000040B5
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x000664F4 File Offset: 0x000646F4
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

	// Token: 0x06000727 RID: 1831 RVA: 0x00066540 File Offset: 0x00064740
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		for (int i = 0; i < this.game_.inAppPurchase.Length; i++)
		{
			if (this.game_.inAppPurchase[i])
			{
				this.uiObjects[7 + i].GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.uiObjects[7 + i].GetComponent<Image>().color = this.guiMain_.colors[6];
			}
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00066658 File Offset: 0x00064858
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[278]);
		this.guiMain_.uiObjects[278].GetComponent<Menu_InAppPurchases>().Init(this.game_, false);
	}

	// Token: 0x04000B07 RID: 2823
	public gameScript game_;

	// Token: 0x04000B08 RID: 2824
	public GameObject[] uiObjects;

	// Token: 0x04000B09 RID: 2825
	public mainScript mS_;

	// Token: 0x04000B0A RID: 2826
	public textScript tS_;

	// Token: 0x04000B0B RID: 2827
	public sfxScript sfx_;

	// Token: 0x04000B0C RID: 2828
	public GUI_Main guiMain_;

	// Token: 0x04000B0D RID: 2829
	public tooltip tooltip_;

	// Token: 0x04000B0E RID: 2830
	public genres genres_;

	// Token: 0x04000B0F RID: 2831
	public Menu_InAppVerwalten menu_;

	// Token: 0x04000B10 RID: 2832
	private float updateTimer;
}
