using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D1 RID: 209
public class Item_InAppVerwalten : MonoBehaviour
{
	// Token: 0x0600072D RID: 1837 RVA: 0x00053FCF File Offset: 0x000521CF
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00053FD7 File Offset: 0x000521D7
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00053FE0 File Offset: 0x000521E0
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

	// Token: 0x06000730 RID: 1840 RVA: 0x0005402C File Offset: 0x0005222C
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

	// Token: 0x06000731 RID: 1841 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00054144 File Offset: 0x00052344
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
