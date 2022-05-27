using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B1 RID: 177
public class Item_ArchivFanbrief : MonoBehaviour
{
	// Token: 0x06000670 RID: 1648 RVA: 0x0005057B File Offset: 0x0004E77B
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00050584 File Offset: 0x0004E784
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

	// Token: 0x06000672 RID: 1650 RVA: 0x0003D679 File Offset: 0x0003B879
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x000505EF File Offset: 0x0004E7EF
	public void BUTTON_Click()
	{
		this.sfx_.PlaySound(3, true);
		if (this.game_)
		{
			this.game_.archiv_fanbriefe = !this.game_.archiv_fanbriefe;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000A01 RID: 2561
	public GameObject[] uiObjects;

	// Token: 0x04000A02 RID: 2562
	public mainScript mS_;

	// Token: 0x04000A03 RID: 2563
	public textScript tS_;

	// Token: 0x04000A04 RID: 2564
	public sfxScript sfx_;

	// Token: 0x04000A05 RID: 2565
	public GUI_Main guiMain_;

	// Token: 0x04000A06 RID: 2566
	public tooltip tooltip_;

	// Token: 0x04000A07 RID: 2567
	public gameScript game_;

	// Token: 0x04000A08 RID: 2568
	public genres genres_;
}
