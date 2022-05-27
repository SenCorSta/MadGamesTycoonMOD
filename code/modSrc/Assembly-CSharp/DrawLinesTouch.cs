using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200034B RID: 843
public class DrawLinesTouch : MonoBehaviour
{
	// Token: 0x06001F68 RID: 8040 RVA: 0x0014B8EC File Offset: 0x00149AEC
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

	// Token: 0x06001F69 RID: 8041 RVA: 0x0014B994 File Offset: 0x00149B94
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

	// Token: 0x040027CD RID: 10189
	public Texture2D lineTex;

	// Token: 0x040027CE RID: 10190
	public int maxPoints = 5000;

	// Token: 0x040027CF RID: 10191
	public float lineWidth = 4f;

	// Token: 0x040027D0 RID: 10192
	public int minPixelMove = 5;

	// Token: 0x040027D1 RID: 10193
	public bool useEndCap;

	// Token: 0x040027D2 RID: 10194
	public Texture2D capLineTex;

	// Token: 0x040027D3 RID: 10195
	public Texture2D capTex;

	// Token: 0x040027D4 RID: 10196
	public float capLineWidth = 20f;

	// Token: 0x040027D5 RID: 10197
	private VectorLine line;

	// Token: 0x040027D6 RID: 10198
	private Vector2 previousPosition;

	// Token: 0x040027D7 RID: 10199
	private int sqrMinPixelMove;

	// Token: 0x040027D8 RID: 10200
	private bool canDraw;

	// Token: 0x040027D9 RID: 10201
	private Touch touch;
}
