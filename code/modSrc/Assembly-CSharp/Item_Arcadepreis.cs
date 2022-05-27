using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CC RID: 204
public class Item_Arcadepreis : MonoBehaviour
{
	// Token: 0x06000701 RID: 1793 RVA: 0x00005DF7 File Offset: 0x00003FF7
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00005DFF File Offset: 0x00003FFF
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x00005E07 File Offset: 0x00004007
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

	// Token: 0x06000704 RID: 1796 RVA: 0x00065A3C File Offset: 0x00063C3C
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

	// Token: 0x06000705 RID: 1797 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00065B80 File Offset: 0x00063D80
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
