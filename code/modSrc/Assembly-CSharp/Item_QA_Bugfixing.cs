using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D9 RID: 217
public class Item_QA_Bugfixing : MonoBehaviour
{
	// Token: 0x06000760 RID: 1888 RVA: 0x00054EEA File Offset: 0x000530EA
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x00054EF2 File Offset: 0x000530F2
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00054EFC File Offset: 0x000530FC
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

	// Token: 0x06000763 RID: 1891 RVA: 0x00054F48 File Offset: 0x00053148
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[3].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)Mathf.RoundToInt(this.game_.points_bugs), false).ToString() + " " + this.tS_.GetText(424);
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0005508B File Offset: 0x0005328B
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.uiObjects[171].GetComponent<Menu_QA_BugfixingSelectGame>().StartBugfixing(this.game_);
	}

	// Token: 0x04000B4E RID: 2894
	public gameScript game_;

	// Token: 0x04000B4F RID: 2895
	public GameObject[] uiObjects;

	// Token: 0x04000B50 RID: 2896
	public mainScript mS_;

	// Token: 0x04000B51 RID: 2897
	public textScript tS_;

	// Token: 0x04000B52 RID: 2898
	public sfxScript sfx_;

	// Token: 0x04000B53 RID: 2899
	public GUI_Main guiMain_;

	// Token: 0x04000B54 RID: 2900
	public tooltip tooltip_;

	// Token: 0x04000B55 RID: 2901
	public genres genres_;

	// Token: 0x04000B56 RID: 2902
	public roomScript rS_;

	// Token: 0x04000B57 RID: 2903
	private float updateTimer;
}
