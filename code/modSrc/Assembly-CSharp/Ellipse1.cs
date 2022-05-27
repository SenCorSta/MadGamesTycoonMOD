using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034C RID: 844
public class Ellipse1 : MonoBehaviour
{
	// Token: 0x06001F6B RID: 8043 RVA: 0x0014BAAC File Offset: 0x00149CAC
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f, LineType.Continuous);
		vectorLine.MakeEllipse(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), this.xRadius, this.yRadius, this.segments, this.pointRotation);
		vectorLine.Draw();
	}

	// Token: 0x040027DA RID: 10202
	public Texture lineTexture;

	// Token: 0x040027DB RID: 10203
	public float xRadius = 120f;

	// Token: 0x040027DC RID: 10204
	public float yRadius = 120f;

	// Token: 0x040027DD RID: 10205
	public int segments = 60;

	// Token: 0x040027DE RID: 10206
	public float pointRotation;
}
