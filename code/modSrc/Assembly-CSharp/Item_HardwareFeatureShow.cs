using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A9 RID: 169
public class Item_HardwareFeatureShow : MonoBehaviour
{
	// Token: 0x06000641 RID: 1601 RVA: 0x0004F019 File Offset: 0x0004D219
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0004F024 File Offset: 0x0004D224
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.hardwareFeatures_.GetName(this.myID);
		this.tooltip_.c = this.hardwareFeatures_.GetTooltip(this.myID);
	}

	// Token: 0x040009CA RID: 2506
	public int myID;

	// Token: 0x040009CB RID: 2507
	public GameObject[] uiObjects;

	// Token: 0x040009CC RID: 2508
	public mainScript mS_;

	// Token: 0x040009CD RID: 2509
	public textScript tS_;

	// Token: 0x040009CE RID: 2510
	public sfxScript sfx_;

	// Token: 0x040009CF RID: 2511
	public hardwareFeatures hardwareFeatures_;

	// Token: 0x040009D0 RID: 2512
	public GUI_Main guiMain_;

	// Token: 0x040009D1 RID: 2513
	public tooltip tooltip_;
}
