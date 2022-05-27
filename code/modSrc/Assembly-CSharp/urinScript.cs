using System;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class urinScript : MonoBehaviour
{
	// Token: 0x06001A9E RID: 6814 RVA: 0x00111DD4 File Offset: 0x0010FFD4
	private void Start()
	{
		this.myGFX.GetComponent<MeshRenderer>().material = this.myMaterial[UnityEngine.Random.Range(0, this.myMaterial.Length)];
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, UnityEngine.Random.Range(0f, 360f), base.transform.eulerAngles.z);
	}

	// Token: 0x040021F1 RID: 8689
	public Material[] myMaterial;

	// Token: 0x040021F2 RID: 8690
	public GameObject myGFX;
}
