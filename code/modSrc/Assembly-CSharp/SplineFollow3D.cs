using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000369 RID: 873
public class SplineFollow3D : MonoBehaviour
{
	// Token: 0x06001FDB RID: 8155 RVA: 0x000151F7 File Offset: 0x000133F7
	private IEnumerator Start()
	{
		List<Vector3> list = new List<Vector3>();
		int num = 1;
		GameObject gameObject = GameObject.Find("Sphere" + num++);
		while (gameObject != null)
		{
			list.Add(gameObject.transform.position);
			gameObject = GameObject.Find("Sphere" + num++);
		}
		VectorLine line = new VectorLine("Spline", new List<Vector3>(this.segments + 1), 2f, LineType.Continuous);
		line.MakeSpline(list.ToArray(), this.segments, this.doLoop);
		line.Draw3D();
		do
		{
			for (float dist = 0f; dist < 1f; dist += Time.deltaTime * this.speed)
			{
				this.cube.position = line.GetPoint3D01(dist);
				yield return null;
			}
		}
		while (this.doLoop);
		yield break;
	}

	// Token: 0x0400286E RID: 10350
	public int segments = 250;

	// Token: 0x0400286F RID: 10351
	public bool doLoop = true;

	// Token: 0x04002870 RID: 10352
	public Transform cube;

	// Token: 0x04002871 RID: 10353
	public float speed = 0.05f;
}
