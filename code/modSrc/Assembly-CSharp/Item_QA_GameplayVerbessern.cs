using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DB RID: 219
public class Item_QA_GameplayVerbessern : MonoBehaviour
{
	// Token: 0x06000765 RID: 1893 RVA: 0x000060D2 File Offset: 0x000042D2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000060DA File Offset: 0x000042DA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00067538 File Offset: 0x00065738
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

	// Token: 0x06000768 RID: 1896 RVA: 0x00067584 File Offset: 0x00065784
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
			if (!this.game_.gameplayStudio[i])
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.grey;
			}
			else
			{
				this.uiObjects[1 + i].GetComponent<Image>().color = Color.white;
			}
		}
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00067624 File Offset: 0x00065824
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[173].SetActive(false);
		this.guiMain_.uiObjects[172].GetComponent<Menu_QA_GameplayVerbessern>().SetGame(this.game_);
	}

	// Token: 0x04000B61 RID: 2913
	public GameObject[] uiObjects;

	// Token: 0x04000B62 RID: 2914
	public mainScript mS_;

	// Token: 0x04000B63 RID: 2915
	public textScript tS_;

	// Token: 0x04000B64 RID: 2916
	public sfxScript sfx_;

	// Token: 0x04000B65 RID: 2917
	public GUI_Main guiMain_;

	// Token: 0x04000B66 RID: 2918
	public tooltip tooltip_;

	// Token: 0x04000B67 RID: 2919
	public gameScript game_;

	// Token: 0x04000B68 RID: 2920
	private float updateTimer;
}
