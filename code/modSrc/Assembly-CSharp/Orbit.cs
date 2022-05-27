using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035B RID: 859
public class Orbit : MonoBehaviour
{
	// Token: 0x06001FE8 RID: 8168 RVA: 0x0014C2FC File Offset: 0x0014A4FC
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("OrbitLine", new List<Vector3>(this.orbitLineResolution), 2f, LineType.Continuous);
		vectorLine.material = this.lineMaterial;
		vectorLine.MakeCircle(Vector3.zero, Vector3.up, Vector3.Distance(base.transform.position, Vector3.zero));
		vectorLine.Draw3DAuto();
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x0014C35C File Offset: 0x0014A55C
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.up, this.orbitSpeed * Time.deltaTime);
		base.transform.Rotate(Vector3.up * this.rotateSpeed * Time.deltaTime);
	}

	// Token: 0x04002831 RID: 10289
	public float orbitSpeed = -45f;

	// Token: 0x04002832 RID: 10290
	public float rotateSpeed = 200f;

	// Token: 0x04002833 RID: 10291
	public int orbitLineResolution = 150;

	// Token: 0x04002834 RID: 10292
	public Material lineMaterial;
}
