using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DE RID: 222
public class Item_AboPreis : MonoBehaviour
{
	// Token: 0x0600077A RID: 1914 RVA: 0x00006102 File Offset: 0x00004302
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600077B RID: 1915 RVA: 0x0000610A File Offset: 0x0000430A
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x0600077C RID: 1916 RVA: 0x00006112 File Offset: 0x00004312
	private void DataUpdate()
	{
		this.updateTimer += Time.deltaTime;
		if (this.updateTimer < 0.5f)
		{
			return;
		}
		this.updateTimer = 0f;
		this.SetData();
	}

	// Token: 0x0600077D RID: 1917 RVA: 0x00067998 File Offset: 0x00065B98
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.abonnements, false);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney((long)this.game_.aboPreis, true);
		this.tooltip_.c = this.game_.GetTooltip();
		if (this.mS_.multiplayer && !this.guiMain_.uiObjects[245].GetComponent<Menu_AboPreis_Select>().CheckGameData(this.game_))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600077E RID: 1918 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600077F RID: 1919 RVA: 0x00067A78 File Offset: 0x00065C78
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.uiObjects[242].SetActive(true);
		this.guiMain_.uiObjects[242].GetComponent<Menu_Abo_Preis>().Init(this.game_);
	}

	// Token: 0x04000B7A RID: 2938
	public gameScript game_;

	// Token: 0x04000B7B RID: 2939
	public GameObject[] uiObjects;

	// Token: 0x04000B7C RID: 2940
	public mainScript mS_;

	// Token: 0x04000B7D RID: 2941
	public textScript tS_;

	// Token: 0x04000B7E RID: 2942
	public sfxScript sfx_;

	// Token: 0x04000B7F RID: 2943
	public GUI_Main guiMain_;

	// Token: 0x04000B80 RID: 2944
	public tooltip tooltip_;

	// Token: 0x04000B81 RID: 2945
	public genres genres_;

	// Token: 0x04000B82 RID: 2946
	public roomScript rS_;

	// Token: 0x04000B83 RID: 2947
	private float updateTimer;
}
