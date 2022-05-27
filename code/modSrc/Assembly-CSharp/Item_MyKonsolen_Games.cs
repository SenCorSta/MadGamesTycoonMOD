using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F3 RID: 243
public class Item_MyKonsolen_Games : MonoBehaviour
{
	// Token: 0x060007F8 RID: 2040 RVA: 0x000062F6 File Offset: 0x000044F6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0006A0F8 File Offset: 0x000682F8
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

	// Token: 0x060007FA RID: 2042 RVA: 0x0006A1A4 File Offset: 0x000683A4
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

	// Token: 0x060007FB RID: 2043 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x0006A284 File Offset: 0x00068484
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
