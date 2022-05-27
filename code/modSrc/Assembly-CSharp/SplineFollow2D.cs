using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000367 RID: 871
public class SplineFollow2D : MonoBehaviour
{
	// Token: 0x06001FD3 RID: 8147 RVA: 0x000151AC File Offset: 0x000133AC
	private IEnumerator Start()
	{
		List<Vector2> list = new List<Vector2>();
		int num = 1;
		GameObject gameObject = GameObject.Find("Sphere" + num++);
		while (gameObject != null)
		{
			list.Add(Camera.main.WorldToScreenPoint(gameObject.transform.position));
			gameObject = GameObject.Find("Sphere" + num++);
		}
		VectorLine line = new VectorLine("Spline", new List<Vector2>(this.segments + 1), 2f, LineType.Continuous);
		line.MakeSpline(list.ToArray(), this.segments, this.loop);
		line.Draw();
		do
		{
			for (float dist = 0f; dist < 1f; dist += Time.deltaTime * this.speed)
			{
				Vector2 point = line.GetPoint01(dist);
				this.cube.position = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, 10f));
				yield return null;
			}
		}
		while (this.loop);
		yield break;
	}

	// Token: 0x04002865 RID: 10341
	public int segments = 250;

	// Token: 0x04002866 RID: 10342
	public bool loop = true;

	// Token: 0x04002867 RID: 10343
	public Transform cube;

	// Token: 0x04002868 RID: 10344
	public float speed = 0.05f;
}
