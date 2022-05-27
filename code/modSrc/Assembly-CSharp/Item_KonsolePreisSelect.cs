using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D2 RID: 210
public class Item_KonsolePreisSelect : MonoBehaviour
{
	// Token: 0x06000734 RID: 1844 RVA: 0x000541BB File Offset: 0x000523BB
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x000541C3 File Offset: 0x000523C3
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x000541CB File Offset: 0x000523CB
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

	// Token: 0x06000737 RID: 1847 RVA: 0x00054200 File Offset: 0x00052400
	private void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.verkaufspreis, true);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.price, true);
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x000542D4 File Offset: 0x000524D4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[328].SetActive(true);
		this.guiMain_.uiObjects[328].GetComponent<Menu_Konsolenpreis>().Init(this.pS_, null);
	}

	// Token: 0x04000B11 RID: 2833
	public platformScript pS_;

	// Token: 0x04000B12 RID: 2834
	public GameObject[] uiObjects;

	// Token: 0x04000B13 RID: 2835
	public mainScript mS_;

	// Token: 0x04000B14 RID: 2836
	public textScript tS_;

	// Token: 0x04000B15 RID: 2837
	public sfxScript sfx_;

	// Token: 0x04000B16 RID: 2838
	public GUI_Main guiMain_;

	// Token: 0x04000B17 RID: 2839
	public tooltip tooltip_;

	// Token: 0x04000B18 RID: 2840
	public genres genres_;

	// Token: 0x04000B19 RID: 2841
	public Menu_Bundle menu_;

	// Token: 0x04000B1A RID: 2842
	private float updateTimer;
}
