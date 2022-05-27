using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034E RID: 846
public class DrawLinesTouch : MonoBehaviour
{
	// Token: 0x06001FBB RID: 8123 RVA: 0x0014ACC8 File Offset: 0x00148EC8
	private void Start()
	{
		Texture2D texture;
		float width;
		if (this.useEndCap)
		{
			VectorLine.SetEndCap("RoundCap", EndCap.Mirror, new Texture2D[]
			{
				this.capLineTex,
				this.capTex
			});
			texture = this.capLineTex;
			width = this.capLineWidth;
		}
		else
		{
			texture = this.lineTex;
			width = this.lineWidth;
		}
		this.line = new VectorLine("DrawnLine", new List<Vector2>(), texture, width, LineType.Continuous, Joins.Weld);
		this.line.endPointsUpdate = 2;
		if (this.useEndCap)
		{
			this.line.endCap = "RoundCap";
		}
		this.sqrMinPixelMove = this.minPixelMove * this.minPixelMove;
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x0014AD70 File Offset: 0x00148F70
	private void Update()
	{
		if (Input.touchCount > 0)
		{
			this.touch = Input.GetTouch(0);
			if (this.touch.phase == TouchPhase.Began)
			{
				this.line.points2.Clear();
				this.line.Draw();
				this.previousPosition = this.touch.position;
				this.line.points2.Add(this.touch.position);
				this.canDraw = true;
				return;
			}
			if (this.touch.phase == TouchPhase.Moved && (this.touch.position - this.previousPosition).sqrMagnitude > (float)this.sqrMinPixelMove && this.canDraw)
			{
				this.previousPosition = this.touch.position;
				this.line.points2.Add(this.touch.position);
				if (this.line.points2.Count >= this.maxPoints)
				{
					this.canDraw = false;
				}
				this.line.Draw();
			}
		}
	}

	// Token: 0x040027E3 RID: 10211
	public Texture2D lineTex;

	// Token: 0x040027E4 RID: 10212
	public int maxPoints = 5000;

	// Token: 0x040027E5 RID: 10213
	public float lineWidth = 4f;

	// Token: 0x040027E6 RID: 10214
	public int minPixelMove = 5;

	// Token: 0x040027E7 RID: 10215
	public bool useEndCap;

	// Token: 0x040027E8 RID: 10216
	public Texture2D capLineTex;

	// Token: 0x040027E9 RID: 10217
	public Texture2D capTex;

	// Token: 0x040027EA RID: 10218
	public float capLineWidth = 20f;

	// Token: 0x040027EB RID: 10219
	private VectorLine line;

	// Token: 0x040027EC RID: 10220
	private Vector2 previousPosition;

	// Token: 0x040027ED RID: 10221
	private int sqrMinPixelMove;

	// Token: 0x040027EE RID: 10222
	private bool canDraw;

	// Token: 0x040027EF RID: 10223
	private Touch touch;
}
