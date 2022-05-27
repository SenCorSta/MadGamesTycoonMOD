using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200036C RID: 876
public class SplineFollow3D : MonoBehaviour
{
	// Token: 0x0600202E RID: 8238 RVA: 0x0014D94E File Offset: 0x0014BB4E
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

	// Token: 0x04002884 RID: 10372
	public int segments = 250;

	// Token: 0x04002885 RID: 10373
	public bool doLoop = true;

	// Token: 0x04002886 RID: 10374
	public Transform cube;

	// Token: 0x04002887 RID: 10375
	public float speed = 0.05f;
}
