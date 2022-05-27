using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D7 RID: 215
public class Item_Restbestand : MonoBehaviour
{
	// Token: 0x0600074E RID: 1870 RVA: 0x00005FF7 File Offset: 0x000041F7
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00005FFF File Offset: 0x000041FF
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00006007 File Offset: 0x00004207
	private void DataUpdate()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.3f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x000670E0 File Offset: 0x000652E0
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
		if (!this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00067218 File Offset: 0x00065418
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menu_.CheckGameData(this.game_))
		{
			return;
		}
		this.guiMain_.uiObjects[226].SetActive(true);
		this.guiMain_.uiObjects[226].GetComponent<Menu_W_Restbestand>().Init(this.game_);
	}

	// Token: 0x04000B43 RID: 2883
	public gameScript game_;

	// Token: 0x04000B44 RID: 2884
	public GameObject[] uiObjects;

	// Token: 0x04000B45 RID: 2885
	public mainScript mS_;

	// Token: 0x04000B46 RID: 2886
	public textScript tS_;

	// Token: 0x04000B47 RID: 2887
	public sfxScript sfx_;

	// Token: 0x04000B48 RID: 2888
	public GUI_Main guiMain_;

	// Token: 0x04000B49 RID: 2889
	public tooltip tooltip_;

	// Token: 0x04000B4A RID: 2890
	public genres genres_;

	// Token: 0x04000B4B RID: 2891
	public roomScript rS_;

	// Token: 0x04000B4C RID: 2892
	public Menu_LagerSelect menu_;

	// Token: 0x04000B4D RID: 2893
	private float updateTimer;
}
