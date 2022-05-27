using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B2 RID: 178
public class Item_ArchivSpielbericht : MonoBehaviour
{
	// Token: 0x0600066C RID: 1644 RVA: 0x00005AA2 File Offset: 0x00003CA2
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00062FC0 File Offset: 0x000611C0
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

	// Token: 0x0600066E RID: 1646 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00005AAA File Offset: 0x00003CAA
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_)
		{
			this.game_.archiv_spielbericht = !this.game_.archiv_spielbericht;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000A09 RID: 2569
	public GameObject[] uiObjects;

	// Token: 0x04000A0A RID: 2570
	public mainScript mS_;

	// Token: 0x04000A0B RID: 2571
	public textScript tS_;

	// Token: 0x04000A0C RID: 2572
	public sfxScript sfx_;

	// Token: 0x04000A0D RID: 2573
	public GUI_Main guiMain_;

	// Token: 0x04000A0E RID: 2574
	public tooltip tooltip_;

	// Token: 0x04000A0F RID: 2575
	public gameScript game_;

	// Token: 0x04000A10 RID: 2576
	public genres genres_;
}
