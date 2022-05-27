using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200007F RID: 127
public class Item_DevGame_AntiCheat : MonoBehaviour
{
	// Token: 0x06000533 RID: 1331 RVA: 0x000053B2 File Offset: 0x000035B2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x000053BA File Offset: 0x000035BA
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0005B228 File Offset: 0x00059428
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

	// Token: 0x06000536 RID: 1334 RVA: 0x0005B274 File Offset: 0x00059474
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

	// Token: 0x06000537 RID: 1335 RVA: 0x0005B3D4 File Offset: 0x000595D4
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

	// Token: 0x06000538 RID: 1336 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0005B448 File Offset: 0x00059648
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
