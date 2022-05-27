using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000081 RID: 129
public class Item_DevGame_CopyProtect : MonoBehaviour
{
	// Token: 0x06000543 RID: 1347 RVA: 0x000053FC File Offset: 0x000035FC
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00005404 File Offset: 0x00003604
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0005B93C File Offset: 0x00059B3C
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

	// Token: 0x06000546 RID: 1350 RVA: 0x0005B988 File Offset: 0x00059B88
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cpS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.cpS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.cpS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.cpS_.effekt);
		this.tooltip_.c = this.cpS_.GetTooltip();
		if (this.guiMain_.uiObjects[365].activeSelf && this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().gS_.gameCopyProtect == this.cpS_.myID)
		{
			base.GetComponent<Image>().color = this.guiMain_.colors[7];
			this.uiObjects[1].GetComponent<Text>().text = "$0";
		}
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0005BAE8 File Offset: 0x00059CE8
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

	// Token: 0x06000548 RID: 1352 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0005BB5C File Offset: 0x00059D5C
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.guiMain_.uiObjects[56].activeSelf)
		{
			this.guiMain_.uiObjects[56].GetComponent<Menu_DevGame>().SetCopyProtect(this.cpS_.myID);
			this.guiMain_.uiObjects[68].GetComponent<Menu_DevGame_CopyProtect>().BUTTON_Close();
		}
		if (this.guiMain_.uiObjects[365].activeSelf)
		{
			this.guiMain_.uiObjects[365].GetComponent<Menu_Dev_ChangeCopyProtect>().SetCopyProtect(this.cpS_.myID);
			this.guiMain_.uiObjects[68].GetComponent<Menu_DevGame_CopyProtect>().BUTTON_Close();
		}
	}

	// Token: 0x0400084E RID: 2126
	public copyProtectScript cpS_;

	// Token: 0x0400084F RID: 2127
	public GameObject[] uiObjects;

	// Token: 0x04000850 RID: 2128
	public mainScript mS_;

	// Token: 0x04000851 RID: 2129
	public textScript tS_;

	// Token: 0x04000852 RID: 2130
	public sfxScript sfx_;

	// Token: 0x04000853 RID: 2131
	public GUI_Main guiMain_;

	// Token: 0x04000854 RID: 2132
	public tooltip tooltip_;

	// Token: 0x04000855 RID: 2133
	private float updateTimer;
}
