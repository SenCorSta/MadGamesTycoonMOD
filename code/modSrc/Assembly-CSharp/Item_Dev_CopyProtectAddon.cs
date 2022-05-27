using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000094 RID: 148
public class Item_Dev_CopyProtectAddon : MonoBehaviour
{
	// Token: 0x060005C4 RID: 1476 RVA: 0x0004B728 File Offset: 0x00049928
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0004B730 File Offset: 0x00049930
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0004B738 File Offset: 0x00049938
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

	// Token: 0x060005C7 RID: 1479 RVA: 0x0004B784 File Offset: 0x00049984
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.cpS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.cpS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.cpS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.cpS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.cpS_.effekt);
		this.tooltip_.c = this.cpS_.GetTooltip();
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0004B86C File Offset: 0x00049A6C
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

	// Token: 0x060005C9 RID: 1481 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0004B8E0 File Offset: 0x00049AE0
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
