using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class floorScript : MonoBehaviour
{
	// Token: 0x06001A22 RID: 6690 RVA: 0x000119F7 File Offset: 0x0000FBF7
	public void SetFilterTexture()
	{
		this.myObject.GetComponent<Renderer>().material = this.materials[1];
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x00011A11 File Offset: 0x0000FC11
	public void SetStandardTexture()
	{
		this.myObject.GetComponent<Renderer>().material = this.materials[0];
	}

	// Token: 0x04002157 RID: 8535
	public GameObject myObject;

	// Token: 0x04002158 RID: 8536
	public Material[] materials;
}
