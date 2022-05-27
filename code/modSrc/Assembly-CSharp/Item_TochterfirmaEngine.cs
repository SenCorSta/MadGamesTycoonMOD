using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000FF RID: 255
public class Item_TochterfirmaEngine : MonoBehaviour
{
	// Token: 0x06000847 RID: 2119 RVA: 0x00059C5A File Offset: 0x00057E5A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x00059C64 File Offset: 0x00057E64
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.eS_.SetSpezialPlatformSprite(this.uiObjects[4]);
		this.uiObjects[3].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
		string text = this.tS_.GetText(160) + ": <b>" + this.eS_.GetFeaturesAmount().ToString() + "</b>";
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(261),
			": <b>",
			this.mS_.GetMoney((long)this.eS_.GetGamesAmount(), false),
			"</b>"
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		if (this.eS_.myID == this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().g_GameEngine)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[4];
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00059DE4 File Offset: 0x00057FE4
	private void Update()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 1f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x00059E30 File Offset: 0x00058030
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.pS_.tf_engine = this.eS_.myID;
		this.guiMain_.uiObjects[393].GetComponent<Menu_Stats_TochterfirmaSettings>().UpdateData();
		this.guiMain_.uiObjects[401].GetComponent<Menu_Stats_TochterfirmaEngine>().BUTTON_Close();
	}

	// Token: 0x04000C88 RID: 3208
	public engineScript eS_;

	// Token: 0x04000C89 RID: 3209
	public GameObject[] uiObjects;

	// Token: 0x04000C8A RID: 3210
	public mainScript mS_;

	// Token: 0x04000C8B RID: 3211
	public textScript tS_;

	// Token: 0x04000C8C RID: 3212
	public sfxScript sfx_;

	// Token: 0x04000C8D RID: 3213
	public engineFeatures eF_;

	// Token: 0x04000C8E RID: 3214
	public genres genres_;

	// Token: 0x04000C8F RID: 3215
	public GUI_Main guiMain_;

	// Token: 0x04000C90 RID: 3216
	public tooltip tooltip_;

	// Token: 0x04000C91 RID: 3217
	public publisherScript pS_;

	// Token: 0x04000C92 RID: 3218
	private float updateTimer;
}
