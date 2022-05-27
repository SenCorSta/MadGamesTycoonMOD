using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[RequireComponent(typeof(MeshRenderer))]
public class U10PS_SnowOverTime : MonoBehaviour
{
	// Token: 0x06000083 RID: 131 RVA: 0x00002619 File Offset: 0x00000819
	private void Start()
	{
		this.meshRenderer = base.gameObject.GetComponent<MeshRenderer>();
		this.totalTime = 1f / this.speed * 4.71f;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0001B034 File Offset: 0x00019234
	private void Update()
	{
		Material[] materials = this.meshRenderer.materials;
		materials[0].SetFloat("_SnowAmount", (Mathf.Sin(this.totalTime * this.speed) + 1f) / 2f);
		this.totalTime += Time.deltaTime;
		this.meshRenderer.materials = materials;
	}

	// Token: 0x04000096 RID: 150
	private MeshRenderer meshRenderer;

	// Token: 0x04000097 RID: 151
	public float speed = 0.6f;

	// Token: 0x04000098 RID: 152
	private float totalTime;
}
