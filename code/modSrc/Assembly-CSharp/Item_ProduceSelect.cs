using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D5 RID: 213
public class Item_ProduceSelect : MonoBehaviour
{
	// Token: 0x06000740 RID: 1856 RVA: 0x00005F86 File Offset: 0x00004186
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00005F8E File Offset: 0x0000418E
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x00005F96 File Offset: 0x00004196
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

	// Token: 0x06000743 RID: 1859 RVA: 0x00066C84 File Offset: 0x00064E84
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.GetLagerbestand(), false);
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[0], false);
		this.uiObjects[4].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[1], false);
		this.uiObjects[5].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.lagerbestand[2], false);
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[221].GetComponent<Menu_ProductionSelect>().CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00066DD8 File Offset: 0x00064FD8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[221].GetComponent<Menu_ProductionSelect>().CheckGameData(this.game_))
		{
			return;
		}
		this.guiMain_.uiObjects[222].SetActive(true);
		this.guiMain_.uiObjects[222].GetComponent<Menu_Production>().Init(this.rS_, this.game_);
	}

	// Token: 0x04000B2F RID: 2863
	public gameScript game_;

	// Token: 0x04000B30 RID: 2864
	public GameObject[] uiObjects;

	// Token: 0x04000B31 RID: 2865
	public mainScript mS_;

	// Token: 0x04000B32 RID: 2866
	public textScript tS_;

	// Token: 0x04000B33 RID: 2867
	public sfxScript sfx_;

	// Token: 0x04000B34 RID: 2868
	public GUI_Main guiMain_;

	// Token: 0x04000B35 RID: 2869
	public tooltip tooltip_;

	// Token: 0x04000B36 RID: 2870
	public genres genres_;

	// Token: 0x04000B37 RID: 2871
	public roomScript rS_;

	// Token: 0x04000B38 RID: 2872
	private float updateTimer;
}
