using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D4 RID: 212
public class Item_PackungSelect : MonoBehaviour
{
	// Token: 0x06000739 RID: 1849 RVA: 0x00005F43 File Offset: 0x00004143
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00005F4B File Offset: 0x0000414B
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00005F53 File Offset: 0x00004153
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

	// Token: 0x0600073C RID: 1852 RVA: 0x0006690C File Offset: 0x00064B0C
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetLagerbestandString();
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[0], true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[1], true);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[2], true);
		this.uiObjects[6].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[3], true);
		if (!this.game_.digitalVersion)
		{
			this.uiObjects[6].GetComponent<Text>().text = "-";
		}
		if (!this.game_.retailVersion)
		{
			this.uiObjects[3].GetComponent<Text>().text = "-";
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
		}
		if (this.game_.typ_budget)
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
		}
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.game_.arcade)
		{
			this.uiObjects[4].GetComponent<Text>().text = "-";
			this.uiObjects[5].GetComponent<Text>().text = "-";
			this.uiObjects[6].GetComponent<Text>().text = "-";
		}
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[220].GetComponent<Menu_PackungSelect>().CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x00066BA0 File Offset: 0x00064DA0
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_.handy)
		{
			this.guiMain_.uiObjects[301].SetActive(true);
			this.guiMain_.uiObjects[301].GetComponent<Menu_HandyPreis>().Init(this.game_);
			return;
		}
		if (this.game_.arcade)
		{
			this.guiMain_.uiObjects[307].SetActive(true);
			this.guiMain_.uiObjects[307].GetComponent<Menu_ArcadePreis>().Init(this.game_, null);
			return;
		}
		this.guiMain_.uiObjects[218].SetActive(true);
		this.guiMain_.uiObjects[218].GetComponent<Menu_Packung>().Init(this.game_, null, false, false);
	}

	// Token: 0x04000B25 RID: 2853
	public gameScript game_;

	// Token: 0x04000B26 RID: 2854
	public GameObject[] uiObjects;

	// Token: 0x04000B27 RID: 2855
	public mainScript mS_;

	// Token: 0x04000B28 RID: 2856
	public textScript tS_;

	// Token: 0x04000B29 RID: 2857
	public sfxScript sfx_;

	// Token: 0x04000B2A RID: 2858
	public GUI_Main guiMain_;

	// Token: 0x04000B2B RID: 2859
	public tooltip tooltip_;

	// Token: 0x04000B2C RID: 2860
	public genres genres_;

	// Token: 0x04000B2D RID: 2861
	public roomScript rS_;

	// Token: 0x04000B2E RID: 2862
	private float updateTimer;
}
