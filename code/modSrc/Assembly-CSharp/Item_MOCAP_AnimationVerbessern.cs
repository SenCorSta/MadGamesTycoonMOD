using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000C5 RID: 197
public class Item_MOCAP_AnimationVerbessern : MonoBehaviour
{
	// Token: 0x060006D4 RID: 1748 RVA: 0x00005D1E File Offset: 0x00003F1E
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00005D26 File Offset: 0x00003F26
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00064A38 File Offset: 0x00062C38
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

	// Token: 0x060006D7 RID: 1751 RVA: 0x00064A84 File Offset: 0x00062C84
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.tooltip_.c = this.game_.GetTooltip();
		for (int i = 0; i < 6; i++)
		{
			if (!this.game_.motionCaptureStudio[i])
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.grey;
			}
			else
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.white;
			}
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00064B24 File Offset: 0x00062D24
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[179].SetActive(false);
		this.guiMain_.uiObjects[178].GetComponent<Menu_MOCAP_AnimationVerbessern>().SetGame(this.game_);
	}

	// Token: 0x04000A9B RID: 2715
	public GameObject[] uiObjects;

	// Token: 0x04000A9C RID: 2716
	public mainScript mS_;

	// Token: 0x04000A9D RID: 2717
	public textScript tS_;

	// Token: 0x04000A9E RID: 2718
	public sfxScript sfx_;

	// Token: 0x04000A9F RID: 2719
	public GUI_Main guiMain_;

	// Token: 0x04000AA0 RID: 2720
	public tooltip tooltip_;

	// Token: 0x04000AA1 RID: 2721
	public gameScript game_;

	// Token: 0x04000AA2 RID: 2722
	private float updateTimer;
}
