using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CFX_Demo_RandomDir : MonoBehaviour
{
	// Token: 0x06000040 RID: 64 RVA: 0x0000367C File Offset: 0x0000187C
	private void Start()
	{
		base.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(this.min.x, this.max.x), UnityEngine.Random.Range(this.min.y, this.max.y), UnityEngine.Random.Range(this.min.z, this.max.z));
	}

	// Token: 0x04000033 RID: 51
	public Vector3 min = new Vector3(0f, 0f, 0f);

	// Token: 0x04000034 RID: 52
	public Vector3 max = new Vector3(0f, 360f, 0f);
}
