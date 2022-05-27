using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F3 RID: 243
public class Item_MyKonsolen_Games : MonoBehaviour
{
	// Token: 0x06000801 RID: 2049 RVA: 0x0005805F File Offset: 0x0005625F
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x00058068 File Offset: 0x00056268
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.pS_.games, false));
		this.uiObjects[2].GetComponent<Text>().text = text;
		base.gameObject.name = this.pS_.games.ToString();
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00058114 File Offset: 0x00056314
	public void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		string text = this.tS_.GetText(297);
		text = text.Replace("<NUM>", this.mS_.GetMoney((long)this.pS_.games, false));
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x000581F4 File Offset: 0x000563F4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[340].SetActive(true);
		this.guiMain_.uiObjects[340].GetComponent<Menu_ShowKonsoleGames>().Init(this.pS_);
	}

	// Token: 0x04000C27 RID: 3111
	public GameObject[] uiObjects;

	// Token: 0x04000C28 RID: 3112
	public mainScript mS_;

	// Token: 0x04000C29 RID: 3113
	public textScript tS_;

	// Token: 0x04000C2A RID: 3114
	public sfxScript sfx_;

	// Token: 0x04000C2B RID: 3115
	public GUI_Main guiMain_;

	// Token: 0x04000C2C RID: 3116
	public tooltip tooltip_;

	// Token: 0x04000C2D RID: 3117
	public platformScript pS_;
}
