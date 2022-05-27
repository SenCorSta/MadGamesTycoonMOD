using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CC RID: 204
public class Item_Arcadepreis : MonoBehaviour
{
	// Token: 0x0600070A RID: 1802 RVA: 0x0005345E File Offset: 0x0005165E
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00053466 File Offset: 0x00051666
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0005346E File Offset: 0x0005166E
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

	// Token: 0x0600070D RID: 1805 RVA: 0x000534A4 File Offset: 0x000516A4
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.verkaufspreis[0], true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)(this.game_.verkaufspreis[0] - this.game_.arcadeProdCosts), true);
		float num = (float)(this.game_.arcadeCase + this.game_.arcadeJoystick + this.game_.arcadeMonitor + this.game_.arcadeSound + 4);
		num /= 4f;
		this.guiMain_.DrawStars(this.uiObjects[3], Mathf.RoundToInt(num));
		base.GetComponent<tooltip>().c = this.game_.GetTooltip();
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[309].GetComponent<Menu_Arcadepreise>().CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x000535E8 File Offset: 0x000517E8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_.arcade)
		{
			this.guiMain_.uiObjects[307].SetActive(true);
			this.guiMain_.uiObjects[307].GetComponent<Menu_ArcadePreis>().Init(this.game_, null);
		}
	}

	// Token: 0x04000AD5 RID: 2773
	public gameScript game_;

	// Token: 0x04000AD6 RID: 2774
	public GameObject[] uiObjects;

	// Token: 0x04000AD7 RID: 2775
	public mainScript mS_;

	// Token: 0x04000AD8 RID: 2776
	public textScript tS_;

	// Token: 0x04000AD9 RID: 2777
	public sfxScript sfx_;

	// Token: 0x04000ADA RID: 2778
	public GUI_Main guiMain_;

	// Token: 0x04000ADB RID: 2779
	public tooltip tooltip_;

	// Token: 0x04000ADC RID: 2780
	public genres genres_;

	// Token: 0x04000ADD RID: 2781
	public roomScript rS_;

	// Token: 0x04000ADE RID: 2782
	private float updateTimer;
}
