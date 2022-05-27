using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B0 RID: 176
public class Item_ArchivEngine : MonoBehaviour
{
	// Token: 0x0600066B RID: 1643 RVA: 0x000504BD File Offset: 0x0004E6BD
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x000504C8 File Offset: 0x0004E6C8
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

	// Token: 0x0600066D RID: 1645 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0005053B File Offset: 0x0004E73B
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
