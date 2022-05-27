using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000093 RID: 147
public class Item_Dev_AntiCheatAddon : MonoBehaviour
{
	// Token: 0x060005BC RID: 1468 RVA: 0x0004B4C9 File Offset: 0x000496C9
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0004B4D1 File Offset: 0x000496D1
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005BE RID: 1470 RVA: 0x0004B4DC File Offset: 0x000496DC
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

	// Token: 0x060005BF RID: 1471 RVA: 0x0004B528 File Offset: 0x00049728
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.acS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.acS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.acS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.acS_.effekt);
		this.tooltip_.c = this.acS_.GetTooltip();
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0004B610 File Offset: 0x00049810
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

	// Token: 0x060005C1 RID: 1473 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0004B684 File Offset: 0x00049884
	public void BUTTON_Click()
	{
		if (this.guiMain_.uiObjects[193].activeSelf)
		{
			this.guiMain_.uiObjects[193].GetComponent<Menu_Dev_AddonDo>().SetAntiCheat(this.acS_.myID);
		}
		if (this.guiMain_.uiObjects[247].activeSelf)
		{
			this.guiMain_.uiObjects[247].GetComponent<Menu_Dev_MMOAddon>().SetAntiCheat(this.acS_.myID);
		}
		this.guiMain_.uiObjects[240].GetComponent<Menu_Dev_AntiCheatAddon>().BUTTON_Close();
	}

	// Token: 0x040008EF RID: 2287
	public antiCheatScript acS_;

	// Token: 0x040008F0 RID: 2288
	public GameObject[] uiObjects;

	// Token: 0x040008F1 RID: 2289
	public mainScript mS_;

	// Token: 0x040008F2 RID: 2290
	public textScript tS_;

	// Token: 0x040008F3 RID: 2291
	public sfxScript sfx_;

	// Token: 0x040008F4 RID: 2292
	public GUI_Main guiMain_;

	// Token: 0x040008F5 RID: 2293
	public tooltip tooltip_;

	// Token: 0x040008F6 RID: 2294
	private float updateTimer;
}
