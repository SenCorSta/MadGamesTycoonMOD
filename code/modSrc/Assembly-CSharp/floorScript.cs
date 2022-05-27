using System;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class floorScript : MonoBehaviour
{
	// Token: 0x06001A6C RID: 6764 RVA: 0x0010AED4 File Offset: 0x001090D4
	public void SetFilterTexture()
	{
		this.myObject.GetComponent<Renderer>().material = this.materials[1];
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x0010AEEE File Offset: 0x001090EE
	public void SetStandardTexture()
	{
		this.myObject.GetComponent<Renderer>().material = this.materials[0];
	}

	// Token: 0x04002171 RID: 8561
	public GameObject myObject;

	// Token: 0x04002172 RID: 8562
	public Material[] materials;
}
