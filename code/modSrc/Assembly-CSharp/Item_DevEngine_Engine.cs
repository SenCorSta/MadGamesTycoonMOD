using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007A RID: 122
public class Item_DevEngine_Engine : MonoBehaviour
{
	// Token: 0x06000514 RID: 1300 RVA: 0x0000530C File Offset: 0x0000350C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00005314 File Offset: 0x00003514
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0005A7D0 File Offset: 0x000589D0
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

	// Token: 0x06000517 RID: 1303 RVA: 0x0005A81C File Offset: 0x00058A1C
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

	// Token: 0x06000518 RID: 1304 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x0005A978 File Offset: 0x00058B78
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
