using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000094 RID: 148
public class Item_Dev_CopyProtectAddon : MonoBehaviour
{
	// Token: 0x060005BB RID: 1467 RVA: 0x000055F5 File Offset: 0x000037F5
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x000055FD File Offset: 0x000037FD
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0005E694 File Offset: 0x0005C894
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

	// Token: 0x060005BE RID: 1470 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cpS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.cpS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.cpS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.cpS_.effekt);
		this.tooltip_.c = this.cpS_.GetTooltip();
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0005E7C8 File Offset: 0x0005C9C8
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

	// Token: 0x060005C0 RID: 1472 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0005E83C File Offset: 0x0005CA3C
	public void BUTTON_Click()
	{
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetCopyProtect(this.cpS_.myID);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetCopyProtect(this.cpS_.myID);
		}
		this.guiMain_.uiObjects[194].GetComponent<Menu_Dev_CopyProtectAddon>().BUTTON_Close();
	}

	// Token: 0x040008F7 RID: 2295
	public copyProtectScript cpS_;

	// Token: 0x040008F8 RID: 2296
	public GameObject[] uiObjects;

	// Token: 0x040008F9 RID: 2297
	public mainScript mS_;

	// Token: 0x040008FA RID: 2298
	public textScript tS_;

	// Token: 0x040008FB RID: 2299
	public sfxScript sfx_;

	// Token: 0x040008FC RID: 2300
	public GUI_Main guiMain_;

	// Token: 0x040008FD RID: 2301
	public tooltip tooltip_;

	// Token: 0x040008FE RID: 2302
	private float updateTimer;
}
