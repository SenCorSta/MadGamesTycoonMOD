using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000080 RID: 128
public class Item_DevGame_ChangeGameplayFeature : MonoBehaviour
{
	// Token: 0x06000544 RID: 1348 RVA: 0x00048356 File Offset: 0x00046556
	private void Start()
	{
		if (this.uiObjects[5].activeSelf)
		{
			this.uiObjects[5].SetActive(false);
		}
		this.SetData();
		this.FindScripts();
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00048384 File Offset: 0x00046584
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

	// Token: 0x06000546 RID: 1350 RVA: 0x000483D4 File Offset: 0x000465D4
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

	// Token: 0x06000547 RID: 1351 RVA: 0x0004847C File Offset: 0x0004667C
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

	// Token: 0x06000548 RID: 1352 RVA: 0x000485E4 File Offset: 0x000467E4
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

	// Token: 0x06000549 RID: 1353 RVA: 0x00048658 File Offset: 0x00046858
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

	// Token: 0x0600054A RID: 1354 RVA: 0x0004873C File Offset: 0x0004693C
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
