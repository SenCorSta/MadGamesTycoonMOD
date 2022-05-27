using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D2 RID: 210
public class Item_KonsolePreisSelect : MonoBehaviour
{
	// Token: 0x0600072B RID: 1835 RVA: 0x00005EBD File Offset: 0x000040BD
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00005EC5 File Offset: 0x000040C5
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00005ECD File Offset: 0x000040CD
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

	// Token: 0x0600072E RID: 1838 RVA: 0x000666D0 File Offset: 0x000648D0
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

	// Token: 0x0600072F RID: 1839 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x000667A4 File Offset: 0x000649A4
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
