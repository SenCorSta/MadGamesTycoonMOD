using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B6 RID: 182
public class Item_BuyEngine : MonoBehaviour
{
	// Token: 0x0600068B RID: 1675 RVA: 0x00050B39 File Offset: 0x0004ED39
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x00050B44 File Offset: 0x0004ED44
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

	// Token: 0x0600068D RID: 1677 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x00050CA0 File Offset: 0x0004EEA0
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

	// Token: 0x0600068F RID: 1679 RVA: 0x00050CEC File Offset: 0x0004EEEC
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
