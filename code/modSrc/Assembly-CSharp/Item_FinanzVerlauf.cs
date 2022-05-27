using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E6 RID: 230
public class Item_FinanzVerlauf : MonoBehaviour
{
	// Token: 0x060007AD RID: 1965 RVA: 0x000061F4 File Offset: 0x000043F4
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x000688EC File Offset: 0x00066AEC
	public void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = (1976 + this.index).ToString();
		this.uiObjects[1].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.finanzVerlaufEinnahmen[this.index], true);
		this.uiObjects[2].GetComponent<Text>().text = this.mS_.GetMoney(this.mS_.finanzVerlaufAusgaben[this.index], true);
		long num = this.mS_.finanzVerlaufEinnahmen[this.index] - this.mS_.finanzVerlaufAusgaben[this.index];
		this.uiObjects[3].GetComponent<Text>().text = this.mS_.GetMoney(num, true);
		if (num < 0L)
		{
			this.uiObjects[3].GetComponent<Text>().color = this.guiMain_.colors[5];
		}
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000BC0 RID: 3008
	public GameObject[] uiObjects;

	// Token: 0x04000BC1 RID: 3009
	public mainScript mS_;

	// Token: 0x04000BC2 RID: 3010
	public textScript tS_;

	// Token: 0x04000BC3 RID: 3011
	public sfxScript sfx_;

	// Token: 0x04000BC4 RID: 3012
	public GUI_Main guiMain_;

	// Token: 0x04000BC5 RID: 3013
	public int index;
}
