using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D6 RID: 214
public class Item_RemoveGame : MonoBehaviour
{
	// Token: 0x06000750 RID: 1872 RVA: 0x00054A5D File Offset: 0x00052C5D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00054A65 File Offset: 0x00052C65
	private void Update()
	{
		this.MultiplayerUpdate();
		if (!this.menuScript_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00054A8C File Offset: 0x00052C8C
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

	// Token: 0x06000753 RID: 1875 RVA: 0x00054AD8 File Offset: 0x00052CD8
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Text>().text = this.game_.weeksOnMarket.ToString();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[8].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00054C9C File Offset: 0x00052E9C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menuScript_.CheckGameData(this.game_))
		{
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[224]);
		this.guiMain_.uiObjects[224].GetComponent<Menu_W_GameFromMarket>().Init(this.game_);
	}

	// Token: 0x04000B39 RID: 2873
	public gameScript game_;

	// Token: 0x04000B3A RID: 2874
	public GameObject[] uiObjects;

	// Token: 0x04000B3B RID: 2875
	public mainScript mS_;

	// Token: 0x04000B3C RID: 2876
	public textScript tS_;

	// Token: 0x04000B3D RID: 2877
	public sfxScript sfx_;

	// Token: 0x04000B3E RID: 2878
	public GUI_Main guiMain_;

	// Token: 0x04000B3F RID: 2879
	public tooltip tooltip_;

	// Token: 0x04000B40 RID: 2880
	public genres genres_;

	// Token: 0x04000B41 RID: 2881
	public Menu_RemoveGameSelect menuScript_;

	// Token: 0x04000B42 RID: 2882
	private float updateTimer;
}
