using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F7 RID: 247
public class Item_Stats_Engine : MonoBehaviour
{
	// Token: 0x06000811 RID: 2065 RVA: 0x0000632D File Offset: 0x0000452D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x0006A9C0 File Offset: 0x00068BC0
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.eS_.SetSpezialPlatformSprite(this.uiObjects[6]);
		this.uiObjects[3].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
		string text = this.tS_.GetText(160) + ": " + this.eS_.GetFeaturesAmount().ToString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(261),
			": ",
			this.eS_.GetGamesAmount().ToString()
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = "";
		if (!this.eS_.sellEngine || !this.eS_.playerEngine)
		{
			this.uiObjects[5].SetActive(false);
		}
		if (this.eS_.sellEngine && this.eS_.playerEngine)
		{
			this.uiObjects[5].SetActive(true);
		}
		if (this.eS_.playerEngine)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00006335 File Offset: 0x00004535
	private void Update()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0006AB7C File Offset: 0x00068D7C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[122]);
		this.guiMain_.uiObjects[122].GetComponent<Menu_Stats_Engine_View>().Init(this.eS_);
	}

	// Token: 0x04000C48 RID: 3144
	public engineScript eS_;

	// Token: 0x04000C49 RID: 3145
	public GameObject[] uiObjects;

	// Token: 0x04000C4A RID: 3146
	public mainScript mS_;

	// Token: 0x04000C4B RID: 3147
	public textScript tS_;

	// Token: 0x04000C4C RID: 3148
	public sfxScript sfx_;

	// Token: 0x04000C4D RID: 3149
	public engineFeatures eF_;

	// Token: 0x04000C4E RID: 3150
	public genres genres_;

	// Token: 0x04000C4F RID: 3151
	public GUI_Main guiMain_;

	// Token: 0x04000C50 RID: 3152
	public tooltip tooltip_;

	// Token: 0x04000C51 RID: 3153
	private float updateTimer;
}
