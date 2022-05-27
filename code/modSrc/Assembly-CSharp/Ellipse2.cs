using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000350 RID: 848
public class Ellipse2 : MonoBehaviour
{
	// Token: 0x06001FC0 RID: 8128 RVA: 0x0014AF50 File Offset: 0x00149150
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments * 2 * this.numberOfEllipses);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f);
		for (int i = 0; i < this.numberOfEllipses; i++)
		{
			Vector2 v = new Vector2((float)UnityEngine.Random.Range(0, Screen.width), (float)UnityEngine.Random.Range(0, Screen.height));
			vectorLine.MakeEllipse(v, (float)UnityEngine.Random.Range(10, Screen.width / 2), (float)UnityEngine.Random.Range(10, Screen.height / 2), this.segments, i * (this.segments * 2));
		}
		vectorLine.Draw();
	}

	// Token: 0x040027F5 RID: 10229
	public Texture lineTexture;

	// Token: 0x040027F6 RID: 10230
	public int segments = 60;

	// Token: 0x040027F7 RID: 10231
	public int numberOfEllipses = 10;
}
