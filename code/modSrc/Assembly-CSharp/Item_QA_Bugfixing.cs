using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D9 RID: 217
public class Item_QA_Bugfixing : MonoBehaviour
{
	// Token: 0x06000757 RID: 1879 RVA: 0x0000603A File Offset: 0x0000423A
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00006042 File Offset: 0x00004242
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00067280 File Offset: 0x00065480
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

	// Token: 0x0600075A RID: 1882 RVA: 0x000672CC File Offset: 0x000654CC
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

	// Token: 0x0600075B RID: 1883 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0000604A File Offset: 0x0000424A
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
