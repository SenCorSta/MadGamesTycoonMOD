using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
	// Token: 0x0600007D RID: 125 RVA: 0x0000470A File Offset: 0x0000290A
	private void Start()
	{
		this.meshRenderer = base.GetComponent<MeshRenderer>();
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004718 File Offset: 0x00002918
	private void Update()
	{
		Material[] materials = this.meshRenderer.materials;
		materials[0].SetFloat("_Cutoff", Mathf.Sin(this.t * this.speed));
		this.t += Time.deltaTime;
		this.meshRenderer.materials = materials;
	}

	// Token: 0x0400008E RID: 142
	private MeshRenderer meshRenderer;

	// Token: 0x0400008F RID: 143
	public float speed = 0.5f;

	// Token: 0x04000090 RID: 144
	private float t;
}
