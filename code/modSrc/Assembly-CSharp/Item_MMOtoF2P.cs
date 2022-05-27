using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DF RID: 223
public class Item_MMOtoF2P : MonoBehaviour
{
	// Token: 0x0600078A RID: 1930 RVA: 0x00055842 File Offset: 0x00053A42
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x0005584A File Offset: 0x00053A4A
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00055854 File Offset: 0x00053A54
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

	// Token: 0x0600078D RID: 1933 RVA: 0x000558A0 File Offset: 0x00053AA0
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.abonnements, false);
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[4].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x0005597C File Offset: 0x00053B7C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (!this.menu_.CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[286]);
		this.guiMain_.uiObjects[286].GetComponent<Menu_W_MMOtoF2P>().Init(this.game_);
	}

	// Token: 0x04000B84 RID: 2948
	public gameScript game_;

	// Token: 0x04000B85 RID: 2949
	public GameObject[] uiObjects;

	// Token: 0x04000B86 RID: 2950
	public mainScript mS_;

	// Token: 0x04000B87 RID: 2951
	public textScript tS_;

	// Token: 0x04000B88 RID: 2952
	public sfxScript sfx_;

	// Token: 0x04000B89 RID: 2953
	public GUI_Main guiMain_;

	// Token: 0x04000B8A RID: 2954
	public tooltip tooltip_;

	// Token: 0x04000B8B RID: 2955
	public genres genres_;

	// Token: 0x04000B8C RID: 2956
	public Menu_MMOtoF2P menu_;

	// Token: 0x04000B8D RID: 2957
	private float updateTimer;
}
