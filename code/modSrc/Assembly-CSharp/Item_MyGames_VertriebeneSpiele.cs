using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F1 RID: 241
public class Item_MyGames_VertriebeneSpiele : MonoBehaviour
{
	// Token: 0x060007EC RID: 2028 RVA: 0x000062E6 File Offset: 0x000044E6
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00069CEC File Offset: 0x00067EEC
	private void Update()
	{
		this.uiObjects[1].GetComponent<Text>().text = (base.gameObject.transform.GetSiblingIndex() + 1).ToString();
		if (!this.mS_.multiplayer)
		{
			return;
		}
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.game_.GetGesamtGewinn(), true);
		base.gameObject.name = this.game_.reviewTotal.ToString();
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x00069D78 File Offset: 0x00067F78
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		long gesamtGewinn = this.game_.GetGesamtGewinn();
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(gesamtGewinn, true);
		if (gesamtGewinn < 0L)
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
		else
		{
			this.uiObjects[2].GetComponent<Text>().color = this.guiMain_.colors[15];
		}
		if (!this.game_.devS_)
		{
			this.game_.FindMyDeveloper();
		}
		if (this.game_.devS_)
		{
			this.uiObjects[3].GetComponent<Image>().sprite = this.game_.devS_.GetLogo();
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x00069E90 File Offset: 0x00068090
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[91]);
		this.guiMain_.uiObjects[91].GetComponent<Menu_Game_Umsatz>().Init(this.game_);
	}

	// Token: 0x04000C18 RID: 3096
	public GameObject[] uiObjects;

	// Token: 0x04000C19 RID: 3097
	public mainScript mS_;

	// Token: 0x04000C1A RID: 3098
	public textScript tS_;

	// Token: 0x04000C1B RID: 3099
	public sfxScript sfx_;

	// Token: 0x04000C1C RID: 3100
	public GUI_Main guiMain_;

	// Token: 0x04000C1D RID: 3101
	public tooltip tooltip_;

	// Token: 0x04000C1E RID: 3102
	public gameScript game_;

	// Token: 0x04000C1F RID: 3103
	public genres genres_;
}
