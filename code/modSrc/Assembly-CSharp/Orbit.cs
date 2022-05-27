using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000358 RID: 856
public class Orbit : MonoBehaviour
{
	// Token: 0x06001F95 RID: 8085 RVA: 0x0014CCE8 File Offset: 0x0014AEE8
	private void Start()
	{
		VectorLine vectorLine = new VectorLine("OrbitLine", new List<Vector3>(this.orbitLineResolution), 2f, LineType.Continuous);
		vectorLine.material = this.lineMaterial;
		vectorLine.MakeCircle(Vector3.zero, Vector3.up, Vector3.Distance(base.transform.position, Vector3.zero));
		vectorLine.Draw3DAuto();
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x0014CD48 File Offset: 0x0014AF48
	private void Update()
	{
		base.transform.RotateAround(Vector3.zero, Vector3.up, this.orbitSpeed * Time.deltaTime);
		base.transform.Rotate(Vector3.up * this.rotateSpeed * Time.deltaTime);
	}

	// Token: 0x0400281B RID: 10267
	public float orbitSpeed = -45f;

	// Token: 0x0400281C RID: 10268
	public float rotateSpeed = 200f;

	// Token: 0x0400281D RID: 10269
	public int orbitLineResolution = 150;

	// Token: 0x0400281E RID: 10270
	public Material lineMaterial;
}
