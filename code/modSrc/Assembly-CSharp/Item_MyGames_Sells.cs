using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000ED RID: 237
public class Item_MyGames_Sells : MonoBehaviour
{
	// Token: 0x060007DD RID: 2013 RVA: 0x0005744C File Offset: 0x0005564C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007DE RID: 2014 RVA: 0x00057454 File Offset: 0x00055654
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		base.gameObject.name = this.game_.sellsTotal.ToString();
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x000574E0 File Offset: 0x000556E0
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		if (this.game_.pubOffer)
		{
			this.uiObjects[0].GetComponent<Text>().color = this.guiMain_.colors[23];
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x000575D4 File Offset: 0x000557D4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[91]);
		this.guiMain_.uiObjects[91].GetComponent<Menu_Game_Umsatz>().Init(this.game_);
	}

	// Token: 0x04000BF6 RID: 3062
	public GameObject[] uiObjects;

	// Token: 0x04000BF7 RID: 3063
	public mainScript mS_;

	// Token: 0x04000BF8 RID: 3064
	public textScript tS_;

	// Token: 0x04000BF9 RID: 3065
	public sfxScript sfx_;

	// Token: 0x04000BFA RID: 3066
	public GUI_Main guiMain_;

	// Token: 0x04000BFB RID: 3067
	public tooltip tooltip_;

	// Token: 0x04000BFC RID: 3068
	public gameScript game_;

	// Token: 0x04000BFD RID: 3069
	public genres genres_;
}
