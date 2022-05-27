using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D7 RID: 215
public class Item_Restbestand : MonoBehaviour
{
	// Token: 0x06000757 RID: 1879 RVA: 0x00054D07 File Offset: 0x00052F07
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00054D0F File Offset: 0x00052F0F
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00054D17 File Offset: 0x00052F17
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

	// Token: 0x0600075A RID: 1882 RVA: 0x00054D4C File Offset: 0x00052F4C
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

	// Token: 0x0600075B RID: 1883 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00054E84 File Offset: 0x00053084
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
