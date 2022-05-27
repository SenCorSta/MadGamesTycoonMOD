using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D5 RID: 213
public class Item_ProduceSelect : MonoBehaviour
{
	// Token: 0x06000749 RID: 1865 RVA: 0x0005483A File Offset: 0x00052A3A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x00054842 File Offset: 0x00052A42
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0005484A File Offset: 0x00052A4A
	private void DataUpdate()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00054880 File Offset: 0x00052A80
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.GetLagerbestand(), false);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[0], false);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[1], false);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[2], false);
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[221].GetComponent<Menu_ProductionSelect>().CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x000549D4 File Offset: 0x00052BD4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[221].GetComponent<Menu_ProductionSelect>().CheckGameData(this.game_))
		{
			return;
		}
		this.guiMain_.uiObjects[222].SetActive(true);
		this.guiMain_.uiObjects[222].GetComponent<Menu_Production>().Init(this.rS_, this.game_);
	}

	// Token: 0x04000B2F RID: 2863
	public gameScript game_;

	// Token: 0x04000B30 RID: 2864
	public GameObject[] uiObjects;

	// Token: 0x04000B31 RID: 2865
	public mainScript mS_;

	// Token: 0x04000B32 RID: 2866
	public textScript tS_;

	// Token: 0x04000B33 RID: 2867
	public sfxScript sfx_;

	// Token: 0x04000B34 RID: 2868
	public GUI_Main guiMain_;

	// Token: 0x04000B35 RID: 2869
	public tooltip tooltip_;

	// Token: 0x04000B36 RID: 2870
	public genres genres_;

	// Token: 0x04000B37 RID: 2871
	public roomScript rS_;

	// Token: 0x04000B38 RID: 2872
	private float updateTimer;
}
