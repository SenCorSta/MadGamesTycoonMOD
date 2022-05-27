using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B3 RID: 179
public class Item_ArchivSpielkonzept : MonoBehaviour
{
	// Token: 0x06000671 RID: 1649 RVA: 0x00005AEA File Offset: 0x00003CEA
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0006302C File Offset: 0x0006122C
	public void SetData()
	{
		if (!this.game_)
		{
			return;
		}
		this.uiObjects[0].GetComponent<Text>().text = this.game_.GetNameWithTag();
		this.uiObjects[2].GetComponent<Text>().text = this.game_.GetReleaseDateString();
		this.tooltip_.c = this.game_.GetTooltip();
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00005AF2 File Offset: 0x00003CF2
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_)
		{
			this.game_.archiv_spielkonzept = !this.game_.archiv_spielkonzept;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000A11 RID: 2577
	public GameObject[] uiObjects;

	// Token: 0x04000A12 RID: 2578
	public mainScript mS_;

	// Token: 0x04000A13 RID: 2579
	public textScript tS_;

	// Token: 0x04000A14 RID: 2580
	public sfxScript sfx_;

	// Token: 0x04000A15 RID: 2581
	public GUI_Main guiMain_;

	// Token: 0x04000A16 RID: 2582
	public tooltip tooltip_;

	// Token: 0x04000A17 RID: 2583
	public gameScript game_;

	// Token: 0x04000A18 RID: 2584
	public genres genres_;
}
