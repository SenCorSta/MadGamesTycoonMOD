using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D0 RID: 208
public class Item_Handypreis : MonoBehaviour
{
	// Token: 0x0600071D RID: 1821 RVA: 0x00005E6A File Offset: 0x0000406A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x00005E72 File Offset: 0x00004072
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00005E7A File Offset: 0x0000407A
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

	// Token: 0x06000720 RID: 1824 RVA: 0x000663DC File Offset: 0x000645DC
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[3], true);
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[308].GetComponent<Menu_Handypreise>().CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00066494 File Offset: 0x00064694
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_.handy)
		{
			this.guiMain_.uiObjects[301].SetActive(true);
			this.guiMain_.uiObjects[301].GetComponent<Menu_HandyPreis>().Init(this.game_);
		}
	}

	// Token: 0x04000AFD RID: 2813
	public gameScript game_;

	// Token: 0x04000AFE RID: 2814
	public GameObject[] uiObjects;

	// Token: 0x04000AFF RID: 2815
	public mainScript mS_;

	// Token: 0x04000B00 RID: 2816
	public textScript tS_;

	// Token: 0x04000B01 RID: 2817
	public sfxScript sfx_;

	// Token: 0x04000B02 RID: 2818
	public GUI_Main guiMain_;

	// Token: 0x04000B03 RID: 2819
	public tooltip tooltip_;

	// Token: 0x04000B04 RID: 2820
	public genres genres_;

	// Token: 0x04000B05 RID: 2821
	public roomScript rS_;

	// Token: 0x04000B06 RID: 2822
	private float updateTimer;
}
