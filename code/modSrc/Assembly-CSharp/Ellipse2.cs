using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034D RID: 845
public class Ellipse2 : MonoBehaviour
{
	// Token: 0x06001F6D RID: 8045 RVA: 0x0014BB1C File Offset: 0x00149D1C
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

	// Token: 0x040027DF RID: 10207
	public Texture lineTexture;

	// Token: 0x040027E0 RID: 10208
	public int segments = 60;

	// Token: 0x040027E1 RID: 10209
	public int numberOfEllipses = 10;
}
