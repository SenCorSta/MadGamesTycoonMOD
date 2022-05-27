using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B2 RID: 178
public class Item_ArchivSpielbericht : MonoBehaviour
{
	// Token: 0x06000675 RID: 1653 RVA: 0x0005062F File Offset: 0x0004E82F
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00050638 File Offset: 0x0004E838
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

	// Token: 0x06000677 RID: 1655 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x000506A3 File Offset: 0x0004E8A3
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
