using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000077 RID: 119
public class Item_DevAddon_GameplayFeature : MonoBehaviour
{
	// Token: 0x060004FD RID: 1277 RVA: 0x00005261 File Offset: 0x00003461
	private void Start()
	{
		this.FindScripts();
		this.SetData();
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00059760 File Offset: 0x00057960
	private void FindScripts()
	{
		if (!this.menuDevAddon_)
		{
			this.menuDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
		}
		if (!this.menuDevMMOAddon_)
		{
			this.menuDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
		}
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000597C0 File Offset: 0x000579C0
	private void Update()
	{
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			if (!this.menuDevAddon_)
			{
				this.menuDevAddon_ = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>();
			}
			if (this.menuDevAddon_.g_GameGameplayFeatures[this.myID])
			{
				base.GetComponent<Image>().color = this.guiMain_.colors[4];
			}
			else
			{
				base.GetComponent<Image>().color = Color.white;
			}
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			if (!this.menuDevMMOAddon_)
			{
				this.menuDevMMOAddon_ = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>();
			}
			if (this.menuDevMMOAddon_.g_GameGameplayFeatures[this.myID])
			{
				base.GetComponent<Image>().color = this.guiMain_.colors[4];
			}
			else
			{
				base.GetComponent<Image>().color = Color.white;
			}
		}
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevAddon_.gS_.maingenre);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevMMOAddon_.gS_.maingenre);
		}
		this.SetGoodBadIcon();
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x00059964 File Offset: 0x00057B64
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.gF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.gF_.GetDevCosts(this.myID), true);
		this.uiObjects[2].GetComponent<Image>().sprite = this.gF_.GetTypSprite(this.myID);
		this.guiMain_.DrawStars(this.uiObjects[3], this.gF_.gameplayFeatures_LEVEL[this.myID]);
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevAddon_.gS_.maingenre);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.tooltip_.c = this.gF_.GetTooltip(this.myID, this.menuDevMMOAddon_.gS_.maingenre);
		}
		this.SetGoodBadIcon();
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x00059A9C File Offset: 0x00057C9C
	public void BUTTON_Click()
	{
		if (!base.GetComponent<Button>().interactable)
		{
			return;
		}
		this.sfx_.PlaySound(3, false);
		bool flag = false;
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			flag = this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetGameplayFeature(this.myID);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			flag = this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetGameplayFeature(this.myID);
		}
		if (flag)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
			return;
		}
		base.GetComponent<Image>().color = Color.white;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00059B6C File Offset: 0x00057D6C
	private void SetGoodBadIcon()
	{
		this.FindScripts();
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			if (this.menuDevAddon_.gS_.maingenre != -1)
			{
				if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menuDevAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
					this.goodBad = 2;
					return;
				}
				if (this.gF_.gameplayFeatures_BAD[this.myID, this.menuDevAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
					this.goodBad = 0;
					return;
				}
			}
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
			this.goodBad = 1;
			return;
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			if (this.menuDevMMOAddon_.gS_.maingenre != -1)
			{
				if (this.gF_.gameplayFeatures_GOOD[this.myID, this.menuDevMMOAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[36];
					this.goodBad = 2;
					return;
				}
				if (this.gF_.gameplayFeatures_BAD[this.myID, this.menuDevMMOAddon_.gS_.maingenre])
				{
					this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[38];
					this.goodBad = 0;
					return;
				}
			}
			this.uiObjects[4].GetComponent<Image>().sprite = this.guiMain_.uiSprites[37];
			this.goodBad = 1;
			return;
		}
	}

	// Token: 0x040007EA RID: 2026
	public int myID;

	// Token: 0x040007EB RID: 2027
	public GameObject[] uiObjects;

	// Token: 0x040007EC RID: 2028
	public mainScript mS_;

	// Token: 0x040007ED RID: 2029
	public textScript tS_;

	// Token: 0x040007EE RID: 2030
	public sfxScript sfx_;

	// Token: 0x040007EF RID: 2031
	public gameplayFeatures gF_;

	// Token: 0x040007F0 RID: 2032
	public GUI_Main guiMain_;

	// Token: 0x040007F1 RID: 2033
	public tooltip tooltip_;

	// Token: 0x040007F2 RID: 2034
	public int goodBad = 1;

	// Token: 0x040007F3 RID: 2035
	private Menu_Dev_AddonDo menuDevAddon_;

	// Token: 0x040007F4 RID: 2036
	private Menu_Dev_MMOAddon menuDevMMOAddon_;
}
