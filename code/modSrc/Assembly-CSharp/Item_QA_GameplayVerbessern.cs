using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DB RID: 219
public class Item_QA_GameplayVerbessern : MonoBehaviour
{
	// Token: 0x0600076E RID: 1902 RVA: 0x0005523C File Offset: 0x0005343C
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00055244 File Offset: 0x00053444
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0005524C File Offset: 0x0005344C
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

	// Token: 0x06000771 RID: 1905 RVA: 0x00055298 File Offset: 0x00053498
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

	// Token: 0x06000772 RID: 1906 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x00055338 File Offset: 0x00053538
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
