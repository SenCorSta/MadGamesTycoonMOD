using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class urinScript : MonoBehaviour
{
	// Token: 0x06001AE8 RID: 6888 RVA: 0x0010E230 File Offset: 0x0010C430
	private void Start()
	{
		this.myGFX.GetComponent<MeshRenderer>().material = this.myMaterial[UnityEngine.Random.Range(0, this.myMaterial.Length)];
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 360f), base.transform.eulerAngles.z);
	}

	// Token: 0x0400220B RID: 8715
	public Material[] myMaterial;

	// Token: 0x0400220C RID: 8716
	public GameObject myGFX;
}
