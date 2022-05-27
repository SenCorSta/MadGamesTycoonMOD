using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000085 RID: 133
public class Item_DevGame_GameplayFeature : MonoBehaviour
{
	// Token: 0x0600055D RID: 1373 RVA: 0x0000542C File Offset: 0x0000362C
	private void Start()
	{
		this.SetData();
		this.FindScripts();
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0005C2E0 File Offset: 0x0005A4E0
	private void FindScripts()
	{
		if (!this.myButton)
		{
			this.myButton = base.GetComponent<Button>();
		}
		if (!this.menuDevGame_)
		{
			this.menuDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0005C32C File Offset: 0x0005A52C
	private void Update()
	{
		if (!this.menuDevGame_)
		{
			this.menuDevGame_ = this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>();
		}
		if (this.menuDevGame_.g_GameGameplayFeatures[this.myID])
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		else
		{
			base.GetComponent<Image>().color = Color.white;
		}
		this.SetPlattformLock();
		this.SetGoodBadIcon();
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevGame_.g_GameMainGenre);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0005C3D4 File Offset: 0x0005A5D4
	private void SetData()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevGame_.g_GameMainGenre);
		this.SetGoodBadIcon();
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0005C4B0 File Offset: 0x0005A6B0
	public void BUTTON_Click()
	{
		this.FindScripts();
		this.SetPlattformLock();
		if (!this.myButton.interactable)
		{
			base.GetComponent<Image>().color = Color.white;
			this.menuDevGame_.DisableGameplayFeature(this.myID);
			return;
		}
		this.sfx_.PlaySound(3, false);
		if (this.menuDevGame_.SetGameplayFeature(this.myID))
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0005C548 File Offset: 0x0005A748
	private void SetGoodBadIcon()
	{
		if (this.menuDevGame_.g_GameMainGenre != -1)
		{
			if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menuDevGame_.g_GameMainGenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
				this.goodBad = 2;
				return;
			}
			if (this.gF_.gameplayFeatures_BAD[this.myID, this.menuDevGame_.g_GameMainGenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
				this.goodBad = 0;
				return;
			}
		}
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
		this.goodBad = 1;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0005C624 File Offset: 0x0005A824
	private void SetPlattformLock()
	{
		int value = this.menuDevGame_.uiObjects[146].GetComponent<Dropdown>().value;
		if (value == 4 && this.gF_.gameplayFeatures_LOCKPLATFORM[this.myID, 4])
		{
			this.myButton.interactable = false;
			return;
		}
		if (value == 5 && this.gF_.gameplayFeatures_LOCKPLATFORM[this.myID, 3])
		{
			this.myButton.interactable = false;
			return;
		}
		this.myButton.interactable = true;
	}

	// Token: 0x04000873 RID: 2163
	public int myID;

	// Token: 0x04000874 RID: 2164
	public GameObject[] uiObjects;

	// Token: 0x04000875 RID: 2165
	public mainScript mS_;

	// Token: 0x04000876 RID: 2166
	public textScript tS_;

	// Token: 0x04000877 RID: 2167
	public sfxScript sfx_;

	// Token: 0x04000878 RID: 2168
	public gameplayFeatures gF_;

	// Token: 0x04000879 RID: 2169
	public GUI_Main guiMain_;

	// Token: 0x0400087A RID: 2170
	public tooltip tooltip_;

	// Token: 0x0400087B RID: 2171
	public Menu_DevGame menuDevGame_;

	// Token: 0x0400087C RID: 2172
	public int goodBad = 1;

	// Token: 0x0400087D RID: 2173
	private Button myButton;
}
