using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E7 RID: 231
public class Item_History : MonoBehaviour
{
	// Token: 0x060007B1 RID: 1969 RVA: 0x000061FC File Offset: 0x000043FC
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00006204 File Offset: 0x00004404
	public void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.history[this.index];
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000BC6 RID: 3014
	public GameObject[] uiObjects;

	// Token: 0x04000BC7 RID: 3015
	public mainScript mS_;

	// Token: 0x04000BC8 RID: 3016
	public textScript tS_;

	// Token: 0x04000BC9 RID: 3017
	public sfxScript sfx_;

	// Token: 0x04000BCA RID: 3018
	public GUI_Main guiMain_;

	// Token: 0x04000BCB RID: 3019
	public int index;
}
