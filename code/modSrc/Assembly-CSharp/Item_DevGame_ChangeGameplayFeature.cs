using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000080 RID: 128
public class Item_DevGame_ChangeGameplayFeature : MonoBehaviour
{
	// Token: 0x0600053B RID: 1339 RVA: 0x000053C2 File Offset: 0x000035C2
	private void Start()
	{
		if (this.uiObjects[5].activeSelf)
		{
			this.uiObjects[5].SetActive(false);
		}
		this.SetData();
		this.FindScripts();
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0005B510 File Offset: 0x00059710
	private void FindScripts()
	{
		if (!this.myButton)
		{
			this.myButton = base.GetComponent<Button>();
		}
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[348].GetComponent<Menu_Dev_ChangeGameplayFeatures>();
		}
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x0005B560 File Offset: 0x00059760
	private void Update()
	{
		if (!this.menu_)
		{
			this.menu_ = this.guiMain_.uiObjects[348].GetComponent<Menu_Dev_ChangeGameplayFeatures>();
		}
		if (this.menu_.g_GameGameplayFeatures[this.myID])
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
		else
		{
			base.GetComponent<Image>().color = Color.white;
		}
		this.SetGoodBadIcon();
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menu_.g_GameMainGenre);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0005B608 File Offset: 0x00059808
	private void SetData()
	{
		this.FindScripts();
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menu_.g_GameMainGenre);
		this.SetGoodBadIcon();
		this.myButton.interactable = true;
		this.SetPlattformLock();
		if (this.gS_.gameplayFeatures_DevDone[this.myID])
		{
			this.myButton.interactable = false;
			if (!this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].SetActive(true);
			}
		}
		if (this.gS_.gameGameplayFeatures[this.myID])
		{
			this.myButton.interactable = false;
			if (!this.uiObjects[5].activeSelf)
			{
				this.uiObjects[5].SetActive(true);
			}
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0005B770 File Offset: 0x00059970
	public void BUTTON_Click()
	{
		this.FindScripts();
		this.SetPlattformLock();
		if (!this.myButton.interactable)
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		if (this.menu_.SetGameplayFeature(this.myID))
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0005B7E4 File Offset: 0x000599E4
	public void SetGoodBadIcon()
	{
		this.FindScripts();
		if (this.menu_.g_GameMainGenre != -1)
		{
			if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menu_.g_GameMainGenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
				this.goodBad = 2;
				return;
			}
			if (this.gF_.gameplayFeatures_BAD[this.myID, this.menu_.g_GameMainGenre])
			{
				this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
				this.goodBad = 0;
				return;
			}
		}
		this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
		this.goodBad = 1;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0005B8C8 File Offset: 0x00059AC8
	private void SetPlattformLock()
	{
		if (this.gS_.arcade && this.gF_.gameplayFeatures_LOCKPLATFORM[this.myID, 4])
		{
			this.myButton.interactable = false;
			return;
		}
		if (this.gS_.handy && this.gF_.gameplayFeatures_LOCKPLATFORM[this.myID, 3])
		{
			this.myButton.interactable = false;
			return;
		}
	}

	// Token: 0x04000842 RID: 2114
	public int myID;

	// Token: 0x04000843 RID: 2115
	public GameObject[] uiObjects;

	// Token: 0x04000844 RID: 2116
	public mainScript mS_;

	// Token: 0x04000845 RID: 2117
	public textScript tS_;

	// Token: 0x04000846 RID: 2118
	public sfxScript sfx_;

	// Token: 0x04000847 RID: 2119
	public gameplayFeatures gF_;

	// Token: 0x04000848 RID: 2120
	public GUI_Main guiMain_;

	// Token: 0x04000849 RID: 2121
	public tooltip tooltip_;

	// Token: 0x0400084A RID: 2122
	public Menu_Dev_ChangeGameplayFeatures menu_;

	// Token: 0x0400084B RID: 2123
	public int goodBad = 1;

	// Token: 0x0400084C RID: 2124
	private Button myButton;

	// Token: 0x0400084D RID: 2125
	public gameScript gS_;
}
