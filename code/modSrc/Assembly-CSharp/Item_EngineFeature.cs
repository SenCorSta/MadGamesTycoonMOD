using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B8 RID: 184
public class Item_EngineFeature : MonoBehaviour
{
	// Token: 0x0600068E RID: 1678 RVA: 0x00005BCB File Offset: 0x00003DCB
	private void Start()
	{
		this.SetData();
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x000637DC File Offset: 0x000619DC
	private void SetData()
	{
		this.uiObjects[0].GetComponent<Text>().text = this.eF_.GetName(this.myID);
		this.uiObjects[1].GetComponent<Image>().sprite = this.eF_.GetTypPic(this.myID);
		this.uiObjects[3].GetComponent<Text>().text = this.eF_.engineFeatures_TECH[this.myID].ToString();
		this.guiMain_.DrawStars(this.uiObjects[4], this.eF_.engineFeatures_LEVEL[this.myID]);
		this.tooltip_.c = this.eF_.GetTooltip(this.myID);
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00004174 File Offset: 0x00002374
	private void OnDisable()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04000A39 RID: 2617
	public int myID;

	// Token: 0x04000A3A RID: 2618
	public GameObject[] uiObjects;

	// Token: 0x04000A3B RID: 2619
	public mainScript mS_;

	// Token: 0x04000A3C RID: 2620
	public textScript tS_;

	// Token: 0x04000A3D RID: 2621
	public sfxScript sfx_;

	// Token: 0x04000A3E RID: 2622
	public engineFeatures eF_;

	// Token: 0x04000A3F RID: 2623
	public GUI_Main guiMain_;

	// Token: 0x04000A40 RID: 2624
	public tooltip tooltip_;
}
