using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E7 RID: 231
public class Item_History : MonoBehaviour
{
	// Token: 0x060007BA RID: 1978 RVA: 0x00056864 File Offset: 0x00054A64
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x0005686C File Offset: 0x00054A6C
	public void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.mS_.history[this.index];
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x0003D679 File Offset: 0x0003B879
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
