using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007F RID: 127
public class Item_DevGame_AntiCheat : MonoBehaviour
{
	// Token: 0x0600053C RID: 1340 RVA: 0x0004805D File Offset: 0x0004625D
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00048065 File Offset: 0x00046265
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00048070 File Offset: 0x00046270
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

	// Token: 0x0600053F RID: 1343 RVA: 0x000480BC File Offset: 0x000462BC
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.acS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.acS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.acS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.acS_.effekt);
		this.tooltip_.c = this.acS_.GetTooltip();
		if (this.guiMain_.uiObjects[365].activeSelf && this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().gS_.gameAntiCheat == this.acS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
			this.uiObjects[1].GetComponent<Text>().text = "$0";
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0004821C File Offset: 0x0004641C
	private Color GetValColor(float val)
	{
		if (val < 30f)
		{
			return this.guiMain_.colorsBalken[0];
		}
		if (val >= 30f && val < 70f)
		{
			return this.guiMain_.colorsBalken[1];
		}
		if (val >= 70f)
		{
			return this.guiMain_.colorsBalken[2];
		}
		return this.guiMain_.colorsBalken[0];
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00048290 File Offset: 0x00046490
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetAntiCheat(this.acS_.myID);
			this.guiMain_.uiObjects[236].GetComponent<Menu_DevGame_AntiCheat>().BUTTON_Close();
		}
		if (this.guiMain_.uiObjects[365].activeSelf)
		{
			this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().SetAntiCheat(this.acS_.myID);
			this.guiMain_.uiObjects[236].GetComponent<Menu_DevGame_AntiCheat>().BUTTON_Close();
		}
	}

	// Token: 0x0400083A RID: 2106
	public antiCheatScript acS_;

	// Token: 0x0400083B RID: 2107
	public GameObject[] uiObjects;

	// Token: 0x0400083C RID: 2108
	public mainScript mS_;

	// Token: 0x0400083D RID: 2109
	public textScript tS_;

	// Token: 0x0400083E RID: 2110
	public sfxScript sfx_;

	// Token: 0x0400083F RID: 2111
	public GUI_Main guiMain_;

	// Token: 0x04000840 RID: 2112
	public tooltip tooltip_;

	// Token: 0x04000841 RID: 2113
	private float updateTimer;
}
