using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DA RID: 218
public class Item_QA_CreateSpielbericht : MonoBehaviour
{
	// Token: 0x06000767 RID: 1895 RVA: 0x000550C7 File Offset: 0x000532C7
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x000550CF File Offset: 0x000532CF
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000550D8 File Offset: 0x000532D8
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

	// Token: 0x0600076A RID: 1898 RVA: 0x00055124 File Offset: 0x00053324
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.game_.reviewTotal.ToString() + "%";
		this.uiObjects[3].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.uiObjects[5].GetComponent<Image>().sprite = this.game_.GetPlatformTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00055200 File Offset: 0x00053400
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		base.gameObject.SetActive(false);
		this.guiMain_.uiObjects[181].GetComponent<Menu_QA_NewSpielberichtSelectGame>().StartSpielbericht(this.game_);
	}

	// Token: 0x04000B58 RID: 2904
	public GameObject[] uiObjects;

	// Token: 0x04000B59 RID: 2905
	public mainScript mS_;

	// Token: 0x04000B5A RID: 2906
	public textScript tS_;

	// Token: 0x04000B5B RID: 2907
	public sfxScript sfx_;

	// Token: 0x04000B5C RID: 2908
	public GUI_Main guiMain_;

	// Token: 0x04000B5D RID: 2909
	public tooltip tooltip_;

	// Token: 0x04000B5E RID: 2910
	public gameScript game_;

	// Token: 0x04000B5F RID: 2911
	public genres genres_;

	// Token: 0x04000B60 RID: 2912
	private float updateTimer;
}
