using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007A RID: 122
public class Item_DevEngine_Engine : MonoBehaviour
{
	// Token: 0x0600051D RID: 1309 RVA: 0x00047562 File Offset: 0x00045762
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0004756A File Offset: 0x0004576A
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00047574 File Offset: 0x00045774
	private void MultiplayerUpdate()
	{
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x000475C0 File Offset: 0x000457C0
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
		if (!this.eS_.sellEngine)
		{
			this.uiObjects[5].SetActive(false);
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0004771C File Offset: 0x0004591C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[37]);
		this.guiMain_.uiObjects[37].GetComponent<Menu_Dev_Engine>().InitUpdateEngine(this.guiMain_.uiObjects[41].GetComponent<Menu_Dev_Engine_SelectOld>().rS_, this.eS_);
		this.guiMain_.uiObjects[41].SetActive(false);
	}

	// Token: 0x0400080A RID: 2058
	public engineScript eS_;

	// Token: 0x0400080B RID: 2059
	public GameObject[] uiObjects;

	// Token: 0x0400080C RID: 2060
	public mainScript mS_;

	// Token: 0x0400080D RID: 2061
	public textScript tS_;

	// Token: 0x0400080E RID: 2062
	public sfxScript sfx_;

	// Token: 0x0400080F RID: 2063
	public engineFeatures eF_;

	// Token: 0x04000810 RID: 2064
	public genres genres_;

	// Token: 0x04000811 RID: 2065
	public GUI_Main guiMain_;

	// Token: 0x04000812 RID: 2066
	public tooltip tooltip_;

	// Token: 0x04000813 RID: 2067
	private float updateTimer;
}
