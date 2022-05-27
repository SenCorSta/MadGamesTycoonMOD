using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000351 RID: 849
public class EndCapDemo : MonoBehaviour
{
	// Token: 0x06001FC2 RID: 8130 RVA: 0x0014B014 File Offset: 0x00149214
	private void Start()
	{
		VectorLine.SetEndCap("arrow", EndCap.Front, new Texture2D[]
		{
			this.lineTex,
			this.frontTex
		});
		VectorLine.SetEndCap("arrow2", EndCap.Both, new Texture2D[]
		{
			this.lineTex2,
			this.frontTex,
			this.backTex
		});
		VectorLine.SetEndCap("rounded", EndCap.Mirror, new Texture2D[]
		{
			this.lineTex3,
			this.capTex
		});
		VectorLine vectorLine = new VectorLine("Arrow", new List<Vector2>(50), 30f, LineType.Continuous, Joins.Weld);
		vectorLine.useViewportCoords = true;
		Vector2[] splinePoints = new Vector2[]
		{
			new Vector2(0.1f, 0.15f),
			new Vector2(0.3f, 0.5f),
			new Vector2(0.5f, 0.6f),
			new Vector2(0.7f, 0.5f),
			new Vector2(0.9f, 0.15f)
		};
		vectorLine.MakeSpline(splinePoints);
		vectorLine.endCap = "arrow";
		vectorLine.Draw();
		VectorLine vectorLine2 = new VectorLine("Arrow2", new List<Vector2>(50), 40f, LineType.Continuous, Joins.Weld);
		vectorLine2.useViewportCoords = true;
		splinePoints = new Vector2[]
		{
			new Vector2(0.1f, 0.85f),
			new Vector2(0.3f, 0.5f),
			new Vector2(0.5f, 0.4f),
			new Vector2(0.7f, 0.5f),
			new Vector2(0.9f, 0.85f)
		};
		vectorLine2.MakeSpline(splinePoints);
		vectorLine2.endCap = "arrow2";
		vectorLine2.continuousTexture = true;
		vectorLine2.Draw();
		new VectorLine("Rounded", new List<Vector2>
		{
			new Vector2(0.1f, 0.5f),
			new Vector2(0.9f, 0.5f)
		}, 20f)
		{
			useViewportCoords = true,
			endCap = "rounded"
		}.Draw();
	}

	// Token: 0x040027F8 RID: 10232
	public Texture2D lineTex;

	// Token: 0x040027F9 RID: 10233
	public Texture2D lineTex2;

	// Token: 0x040027FA RID: 10234
	public Texture2D lineTex3;

	// Token: 0x040027FB RID: 10235
	public Texture2D frontTex;

	// Token: 0x040027FC RID: 10236
	public Texture2D backTex;

	// Token: 0x040027FD RID: 10237
	public Texture2D capTex;
}
