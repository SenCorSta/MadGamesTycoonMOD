using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DC RID: 220
public class Item_QA_ShowSpielbericht : MonoBehaviour
{
	// Token: 0x0600076C RID: 1900 RVA: 0x000060E2 File Offset: 0x000042E2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x000060EA File Offset: 0x000042EA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00067678 File Offset: 0x00065878
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

	// Token: 0x0600076F RID: 1903 RVA: 0x000676C4 File Offset: 0x000658C4
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

	// Token: 0x06000770 RID: 1904 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00067800 File Offset: 0x00065A00
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
