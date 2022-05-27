using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000CB RID: 203
public class Item_AddonBundleSelect : MonoBehaviour
{
	// Token: 0x06000703 RID: 1795 RVA: 0x0005322E File Offset: 0x0005142E
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00053236 File Offset: 0x00051436
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00053240 File Offset: 0x00051440
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

	// Token: 0x06000706 RID: 1798 RVA: 0x0005328C File Offset: 0x0005148C
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x000533E8 File Offset: 0x000515E8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		if (this.game_)
		{
			this.menu_.SetGame(this.guiMain_.uiObjects[272].GetComponent<Menu_AddonBundleSelect>().slot, this.game_);
		}
		this.guiMain_.uiObjects[272].SetActive(false);
	}

	// Token: 0x04000ACB RID: 2763
	public gameScript game_;

	// Token: 0x04000ACC RID: 2764
	public GameObject[] uiObjects;

	// Token: 0x04000ACD RID: 2765
	public mainScript mS_;

	// Token: 0x04000ACE RID: 2766
	public textScript tS_;

	// Token: 0x04000ACF RID: 2767
	public sfxScript sfx_;

	// Token: 0x04000AD0 RID: 2768
	public GUI_Main guiMain_;

	// Token: 0x04000AD1 RID: 2769
	public tooltip tooltip_;

	// Token: 0x04000AD2 RID: 2770
	public genres genres_;

	// Token: 0x04000AD3 RID: 2771
	public Menu_AddonBundle menu_;

	// Token: 0x04000AD4 RID: 2772
	private float updateTimer;
}
