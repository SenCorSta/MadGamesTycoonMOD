using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008E RID: 142
public class Item_DevGame_ShowConcept : MonoBehaviour
{
	// Token: 0x0600059E RID: 1438 RVA: 0x0004AA5B File Offset: 0x00048C5B
	private void Start()
	{
		if (this.game_)
		{
			this.uiObjects[7].GetComponent<Toggle>().isOn = this.game_.spielbericht_favorit;
		}
		this.SetData();
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0004AA8D File Offset: 0x00048C8D
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0004AA98 File Offset: 0x00048C98
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

	// Token: 0x060005A1 RID: 1441 RVA: 0x0004AAE4 File Offset: 0x00048CE4
	private void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[3].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.uiObjects[4].GetComponent<Text>().text = this.tS_.GetText(275) + ": " + this.mS_.GetMoney(this.game_.sellsTotal, false);
		this.uiObjects[5].GetComponent<Text>().text = this.tS_.GetText(277) + ": " + Mathf.RoundToInt((float)this.game_.reviewTotal).ToString() + "%";
		this.uiObjects[2].GetComponent<Image>().sprite = this.game_.GetScreenshot();
		this.uiObjects[1].GetComponent<Image>().sprite = this.genres_.GetPic(this.game_.maingenre);
		this.uiObjects[1].GetComponent<tooltip>().c = this.genres_.GetName(this.game_.maingenre);
		this.uiObjects[6].GetComponent<Image>().sprite = this.game_.GetTypSprite();
		if (this.game_.subgenre == -1)
		{
			this.uiObjects[8].GetComponent<Text>().text = this.game_.GetGenreString();
		}
		else
		{
			this.uiObjects[8].GetComponent<Text>().text = this.game_.GetGenreString() + " / " + this.game_.GetSubGenreString();
		}
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0004ACC8 File Offset: 0x00048EC8
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		this.guiMain_.ActivateMenu(this.guiMain_.uiObjects[110]);
		this.guiMain_.uiObjects[110].GetComponent<Menu_Dev_ShowConcept>().Init(this.game_);
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0004AD19 File Offset: 0x00048F19
	public void TOGGLE_Favorit()
	{
		if (this.game_)
		{
			this.game_.spielbericht_favorit = this.uiObjects[7].GetComponent<Toggle>().isOn;
		}
	}

	// Token: 0x040008C5 RID: 2245
	public gameScript game_;

	// Token: 0x040008C6 RID: 2246
	public GameObject[] uiObjects;

	// Token: 0x040008C7 RID: 2247
	public mainScript mS_;

	// Token: 0x040008C8 RID: 2248
	public textScript tS_;

	// Token: 0x040008C9 RID: 2249
	public sfxScript sfx_;

	// Token: 0x040008CA RID: 2250
	public GUI_Main guiMain_;

	// Token: 0x040008CB RID: 2251
	public tooltip tooltip_;

	// Token: 0x040008CC RID: 2252
	public genres genres_;

	// Token: 0x040008CD RID: 2253
	private float updateTimer;
}
