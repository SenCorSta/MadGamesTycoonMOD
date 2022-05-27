using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E8 RID: 232
public class Item_KonsoleDetailsSelect : MonoBehaviour
{
	// Token: 0x060007BE RID: 1982 RVA: 0x00056896 File Offset: 0x00054A96
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x0005689E File Offset: 0x00054A9E
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x000568A6 File Offset: 0x00054AA6
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

	// Token: 0x060007C1 RID: 1985 RVA: 0x000568DC File Offset: 0x00054ADC
	private void SetData()
	{
		if (!this.pS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.pS_.GetName();
		if (!this.pS_.vomMarktGenommen)
		{
			this.uiObjects[1].GetComponent<Text>().text = this.pS_.GetDateString();
		}
		else
		{
			string text = this.tS_.GetText(1673);
			text = text.Replace("<DATE1>", this.pS_.GetDateString());
			text = text.Replace("<DATE2>", this.pS_.GetDateStringEnd());
			this.uiObjects[1].GetComponent<Text>().color = this.guiMain_.colors[5];
			this.uiObjects[1].GetComponent<Text>().text = text;
		}
		this.pS_.SetPic(this.uiObjects[2]);
		this.uiObjects[3].GetComponent<Image>().sprite = this.pS_.GetTypSprite();
		this.uiObjects[4].GetComponent<Text>().text = this.pS_.tech.ToString();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.pS_.performancePoints, false);
		this.tooltip_.c = this.pS_.GetTooltip();
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00056A48 File Offset: 0x00054C48
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[339].SetActive(true);
		this.guiMain_.uiObjects[339].GetComponent<Menu_ShowKonsoleDetails>().Init(this.pS_);
	}

	// Token: 0x04000BCC RID: 3020
	public platformScript pS_;

	// Token: 0x04000BCD RID: 3021
	public GameObject[] uiObjects;

	// Token: 0x04000BCE RID: 3022
	public mainScript mS_;

	// Token: 0x04000BCF RID: 3023
	public textScript tS_;

	// Token: 0x04000BD0 RID: 3024
	public sfxScript sfx_;

	// Token: 0x04000BD1 RID: 3025
	public GUI_Main guiMain_;

	// Token: 0x04000BD2 RID: 3026
	public tooltip tooltip_;

	// Token: 0x04000BD3 RID: 3027
	public genres genres_;

	// Token: 0x04000BD4 RID: 3028
	public Menu_Bundle menu_;

	// Token: 0x04000BD5 RID: 3029
	private float updateTimer;
}
