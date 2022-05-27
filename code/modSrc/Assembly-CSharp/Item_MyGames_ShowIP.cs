using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000EE RID: 238
public class Item_MyGames_ShowIP : MonoBehaviour
{
	// Token: 0x060007E3 RID: 2019 RVA: 0x00057625 File Offset: 0x00055825
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x0005762D File Offset: 0x0005582D
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00057638 File Offset: 0x00055838
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

	// Token: 0x060007E6 RID: 2022 RVA: 0x00057684 File Offset: 0x00055884
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		if (this.game_.gameTyp != 2)
		{
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		}
		else
		{
			this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(697) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		}
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[7].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00057850 File Offset: 0x00055A50
	public void BUTTON_Click()
	{
		if (this.game_.reviewTotal > 0)
		{
			this.sfx_.PlaySound(3, true);
			this.guiMain_.uiObjects[46].SetActive(true);
			this.guiMain_.uiObjects[46].GetComponent<Menu_Review>().Init(this.game_);
		}
	}

	// Token: 0x04000BFE RID: 3070
	public gameScript game_;

	// Token: 0x04000BFF RID: 3071
	public GameObject[] uiObjects;

	// Token: 0x04000C00 RID: 3072
	public mainScript mS_;

	// Token: 0x04000C01 RID: 3073
	public textScript tS_;

	// Token: 0x04000C02 RID: 3074
	public sfxScript sfx_;

	// Token: 0x04000C03 RID: 3075
	public GUI_Main guiMain_;

	// Token: 0x04000C04 RID: 3076
	public tooltip tooltip_;

	// Token: 0x04000C05 RID: 3077
	public genres genres_;

	// Token: 0x04000C06 RID: 3078
	private float updateTimer;
}
