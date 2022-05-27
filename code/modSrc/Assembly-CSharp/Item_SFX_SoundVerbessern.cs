using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DD RID: 221
public class Item_SFX_SoundVerbessern : MonoBehaviour
{
	// Token: 0x0600077C RID: 1916 RVA: 0x0005557B File Offset: 0x0005377B
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00055583 File Offset: 0x00053783
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x0005558C File Offset: 0x0005378C
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

	// Token: 0x0600077F RID: 1919 RVA: 0x000555D8 File Offset: 0x000537D8
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
			if (!this.game_.soundStudio[i])
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.grey;
			}
			else
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.white;
			}
		}
	}

	// Token: 0x06000780 RID: 1920 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x00055678 File Offset: 0x00053878
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[177].SetActive(false);
		this.guiMain_.uiObjects[176].GetComponent<Menu_SFX_SoundVerbessern>().SetGame(this.game_);
	}

	// Token: 0x04000B72 RID: 2930
	public GameObject[] uiObjects;

	// Token: 0x04000B73 RID: 2931
	public mainScript mS_;

	// Token: 0x04000B74 RID: 2932
	public textScript tS_;

	// Token: 0x04000B75 RID: 2933
	public sfxScript sfx_;

	// Token: 0x04000B76 RID: 2934
	public GUI_Main guiMain_;

	// Token: 0x04000B77 RID: 2935
	public tooltip tooltip_;

	// Token: 0x04000B78 RID: 2936
	public gameScript game_;

	// Token: 0x04000B79 RID: 2937
	private float updateTimer;
}
