using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D3 RID: 211
public class Item_KonsoleRemove : MonoBehaviour
{
	// Token: 0x0600073B RID: 1851 RVA: 0x00054327 File Offset: 0x00052527
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0005432F File Offset: 0x0005252F
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x00054337 File Offset: 0x00052537
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

	// Token: 0x0600073E RID: 1854 RVA: 0x0005436C File Offset: 0x0005256C
	private void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0005442C File Offset: 0x0005262C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[332].SetActive(true);
		this.guiMain_.uiObjects[332].GetComponent<Menu_W_KonsoleFromMarket>().Init(this.pS_);
	}

	// Token: 0x04000B1B RID: 2843
	public platformScript pS_;

	// Token: 0x04000B1C RID: 2844
	public GameObject[] uiObjects;

	// Token: 0x04000B1D RID: 2845
	public mainScript mS_;

	// Token: 0x04000B1E RID: 2846
	public textScript tS_;

	// Token: 0x04000B1F RID: 2847
	public sfxScript sfx_;

	// Token: 0x04000B20 RID: 2848
	public GUI_Main guiMain_;

	// Token: 0x04000B21 RID: 2849
	public tooltip tooltip_;

	// Token: 0x04000B22 RID: 2850
	public genres genres_;

	// Token: 0x04000B23 RID: 2851
	public Menu_Bundle menu_;

	// Token: 0x04000B24 RID: 2852
	private float updateTimer;
}
