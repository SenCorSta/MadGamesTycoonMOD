using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class MakeSpheres : MonoBehaviour
{
	// Token: 0x06001FDB RID: 8155 RVA: 0x0014BDCC File Offset: 0x00149FCC
	private void Start()
	{
		for (int i = 0; i < this.numberOfSpheres; i++)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.spherePrefab, new Vector3(UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area)), UnityEngine.Random.rotation);
		}
	}

	// Token: 0x0400281C RID: 10268
	public GameObject spherePrefab;

	// Token: 0x0400281D RID: 10269
	public int numberOfSpheres = 12;

	// Token: 0x0400281E RID: 10270
	public float area = 4.5f;
}
