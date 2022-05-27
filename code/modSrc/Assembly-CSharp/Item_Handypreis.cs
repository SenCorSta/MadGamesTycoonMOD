using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D0 RID: 208
public class Item_Handypreis : MonoBehaviour
{
	// Token: 0x06000726 RID: 1830 RVA: 0x00053E74 File Offset: 0x00052074
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00053E7C File Offset: 0x0005207C
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00053E84 File Offset: 0x00052084
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

	// Token: 0x06000729 RID: 1833 RVA: 0x00053EB8 File Offset: 0x000520B8
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

	// Token: 0x0600072A RID: 1834 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00053F70 File Offset: 0x00052170
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
