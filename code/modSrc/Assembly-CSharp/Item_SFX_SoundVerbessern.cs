using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DD RID: 221
public class Item_SFX_SoundVerbessern : MonoBehaviour
{
	// Token: 0x06000773 RID: 1907 RVA: 0x000060F2 File Offset: 0x000042F2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x000060FA File Offset: 0x000042FA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00067858 File Offset: 0x00065A58
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

	// Token: 0x06000776 RID: 1910 RVA: 0x000678A4 File Offset: 0x00065AA4
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

	// Token: 0x06000777 RID: 1911 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00067944 File Offset: 0x00065B44
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
