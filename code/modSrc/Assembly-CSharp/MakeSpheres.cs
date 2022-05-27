using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class MakeSpheres : MonoBehaviour
{
	// Token: 0x06001F88 RID: 8072 RVA: 0x0014C880 File Offset: 0x0014AA80
	private void Start()
	{
		for (int i = 0; i < this.numberOfSpheres; i++)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.spherePrefab, new Vector3(UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area), UnityEngine.Random.Range(-this.area, this.area)), UnityEngine.Random.rotation);
		}
	}

	// Token: 0x04002806 RID: 10246
	public GameObject spherePrefab;

	// Token: 0x04002807 RID: 10247
	public int numberOfSpheres = 12;

	// Token: 0x04002808 RID: 10248
	public float area = 4.5f;
}
