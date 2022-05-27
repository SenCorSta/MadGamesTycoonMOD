using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034F RID: 847
public class Ellipse1 : MonoBehaviour
{
	// Token: 0x06001FBE RID: 8126 RVA: 0x0014AEB8 File Offset: 0x001490B8
	private void Start()
	{
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		VectorLine vectorLine = new VectorLine("Line", points, this.lineTexture, 3f, LineType.Continuous);
		vectorLine.MakeEllipse(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), this.xRadius, this.yRadius, this.segments, this.pointRotation);
		vectorLine.Draw();
	}

	// Token: 0x040027F0 RID: 10224
	public Texture lineTexture;

	// Token: 0x040027F1 RID: 10225
	public float xRadius = 120f;

	// Token: 0x040027F2 RID: 10226
	public float yRadius = 120f;

	// Token: 0x040027F3 RID: 10227
	public int segments = 60;

	// Token: 0x040027F4 RID: 10228
	public float pointRotation;
}
