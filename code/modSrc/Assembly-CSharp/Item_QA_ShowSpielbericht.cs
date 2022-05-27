using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DC RID: 220
public class Item_QA_ShowSpielbericht : MonoBehaviour
{
	// Token: 0x06000775 RID: 1909 RVA: 0x0005538A File Offset: 0x0005358A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00055392 File Offset: 0x00053592
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x0005539C File Offset: 0x0005359C
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

	// Token: 0x06000778 RID: 1912 RVA: 0x000553E8 File Offset: 0x000535E8
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.game_.reviewTotal.ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[5].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		if (this.game_.subgenre == -1)
		{
			this.uiObjects[6].GetComponent<Text>().text = this.game_.GetGenreString();
		}
		else
		{
			this.uiObjects[6].GetComponent<Text>().text = this.game_.GetGenreString() + " / " + this.game_.GetSubGenreString();
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600077A RID: 1914 RVA: 0x00055524 File Offset: 0x00053724
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[183]);
		this.guiMain_.uiObjects[183].GetComponent<Menu_QA_Spielbericht>().Init(this.game_);
	}

	// Token: 0x04000B69 RID: 2921
	public GameObject[] uiObjects;

	// Token: 0x04000B6A RID: 2922
	public mainScript mS_;

	// Token: 0x04000B6B RID: 2923
	public textScript tS_;

	// Token: 0x04000B6C RID: 2924
	public sfxScript sfx_;

	// Token: 0x04000B6D RID: 2925
	public GUI_Main guiMain_;

	// Token: 0x04000B6E RID: 2926
	public tooltip tooltip_;

	// Token: 0x04000B6F RID: 2927
	public gameScript game_;

	// Token: 0x04000B70 RID: 2928
	public genres genres_;

	// Token: 0x04000B71 RID: 2929
	private float updateTimer;
}
