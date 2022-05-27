using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B0 RID: 176
public class Item_ArchivEngine : MonoBehaviour
{
	// Token: 0x06000662 RID: 1634 RVA: 0x00005A12 File Offset: 0x00003C12
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x00062EE0 File Offset: 0x000610E0
	public void SetData()
	{
		if (!this.eS_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.eS_.GetName();
		this.uiObjects[2].GetComponent<Text>().text = this.eS_.GetTechLevel().ToString();
		this.tooltip_.c = this.eS_.GetTooltip();
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00005A1A File Offset: 0x00003C1A
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.eS_)
		{
			this.eS_.archiv_engine = !this.eS_.archiv_engine;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040009FA RID: 2554
	public GameObject[] uiObjects;

	// Token: 0x040009FB RID: 2555
	public mainScript mS_;

	// Token: 0x040009FC RID: 2556
	public textScript tS_;

	// Token: 0x040009FD RID: 2557
	public sfxScript sfx_;

	// Token: 0x040009FE RID: 2558
	public GUI_Main guiMain_;

	// Token: 0x040009FF RID: 2559
	public tooltip tooltip_;

	// Token: 0x04000A00 RID: 2560
	public engineScript eS_;
}
