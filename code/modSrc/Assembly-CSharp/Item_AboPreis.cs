using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DE RID: 222
public class Item_AboPreis : MonoBehaviour
{
	// Token: 0x06000783 RID: 1923 RVA: 0x000556CA File Offset: 0x000538CA
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x000556D2 File Offset: 0x000538D2
	private void Update()
	{
		this.DataUpdate();
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x000556DA File Offset: 0x000538DA
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

	// Token: 0x06000786 RID: 1926 RVA: 0x00055710 File Offset: 0x00053910
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

	// Token: 0x06000787 RID: 1927 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x000557F0 File Offset: 0x000539F0
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
