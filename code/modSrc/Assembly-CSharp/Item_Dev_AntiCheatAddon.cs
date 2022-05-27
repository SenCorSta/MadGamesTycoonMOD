using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000093 RID: 147
public class Item_Dev_AntiCheatAddon : MonoBehaviour
{
	// Token: 0x060005B3 RID: 1459 RVA: 0x000055E5 File Offset: 0x000037E5
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000055ED File Offset: 0x000037ED
	private void Update()
	{
		this.MultiplayerUpdate();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0005E448 File Offset: 0x0005C648
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

	// Token: 0x060005B6 RID: 1462 RVA: 0x0005E494 File Offset: 0x0005C694
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.acS_.GetName();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney((long)this.acS_.GetDevCosts(), true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.Round(this.acS_.effekt, 2) + "%";
		this.uiObjects[3].GetComponent<Image>().fillAmount = this.acS_.effekt * 0.01f;
		this.uiObjects[3].GetComponent<Image>().color = this.GetValColor(this.acS_.effekt);
		this.tooltip_.c = this.acS_.GetTooltip();
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0005E57C File Offset: 0x0005C77C
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

	// Token: 0x060005B8 RID: 1464 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0005E5F0 File Offset: 0x0005C7F0
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
