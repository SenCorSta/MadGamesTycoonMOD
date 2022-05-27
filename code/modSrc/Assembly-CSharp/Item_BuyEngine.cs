using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B6 RID: 182
public class Item_BuyEngine : MonoBehaviour
{
	// Token: 0x06000682 RID: 1666 RVA: 0x00005B74 File Offset: 0x00003D74
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x000633FC File Offset: 0x000615FC
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.eS_.spezialgenre);
		this.eS_.SetSpezialPlatformSprite(this.uiObjects[5]);
		this.uiObjects[3].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
		string text = this.tS_.GetText(160) + ": " + this.eS_.GetFeaturesAmount().ToString();
		text = string.Concat(new string[]
		{
			text,
			"\n",
			this.tS_.GetText(260),
			": ",
			this.eS_.gewinnbeteiligung.ToString(),
			"%"
		});
		this.uiObjects[2].GetComponent<Text>().text = text;
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.eS_.preis, true);
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00063558 File Offset: 0x00061758
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

	// Token: 0x06000686 RID: 1670 RVA: 0x000635A4 File Offset: 0x000617A4
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[43]);
		this.guiMain_.uiObjects[43].GetComponent<Menu_BuyEngine_Details>().Init(this.eS_);
	}

	// Token: 0x04000A27 RID: 2599
	public engineScript eS_;

	// Token: 0x04000A28 RID: 2600
	public GameObject[] uiObjects;

	// Token: 0x04000A29 RID: 2601
	public mainScript mS_;

	// Token: 0x04000A2A RID: 2602
	public textScript tS_;

	// Token: 0x04000A2B RID: 2603
	public sfxScript sfx_;

	// Token: 0x04000A2C RID: 2604
	public engineFeatures eF_;

	// Token: 0x04000A2D RID: 2605
	public genres genres_;

	// Token: 0x04000A2E RID: 2606
	public GUI_Main guiMain_;

	// Token: 0x04000A2F RID: 2607
	public tooltip tooltip_;

	// Token: 0x04000A30 RID: 2608
	private float updateTimer;
}
