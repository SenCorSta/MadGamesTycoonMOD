using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035A RID: 858
public class AnimatePartialLine : MonoBehaviour
{
	// Token: 0x06001F9A RID: 8090 RVA: 0x0014CD9C File Offset: 0x0014AF9C
	private void Start()
	{
		this.startIndex = (float)(-(float)this.visibleLineSegments);
		this.endIndex = 0f;
		List<Vector2> points = new List<Vector2>(this.segments + 1);
		this.line = new VectorLine("Spline", points, this.lineTexture, 30f, LineType.Continuous, Joins.Weld);
		int num = Screen.width / 5;
		int num2 = Screen.height / 3;
		this.line.MakeSpline(new Vector2[]
		{
			new Vector2((float)num, (float)num2),
			new Vector2((float)(num * 2), (float)(num2 * 2)),
			new Vector2((float)(num * 3), (float)(num2 * 2)),
			new Vector2((float)(num * 4), (float)num2)
		});
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x0014CE5C File Offset: 0x0014B05C
	private void Update()
	{
		this.startIndex += Time.deltaTime * this.speed;
		this.endIndex += Time.deltaTime * this.speed;
		if (this.startIndex >= (float)(this.segments + 1))
		{
			this.startIndex = (float)(-(float)this.visibleLineSegments);
			this.endIndex = 0f;
		}
		else if (this.startIndex < (float)(-(float)this.visibleLineSegments))
		{
			this.startIndex = (float)this.segments;
			this.endIndex = (float)(this.segments + this.visibleLineSegments);
		}
		this.line.drawStart = (int)this.startIndex;
		this.line.drawEnd = (int)this.endIndex;
		this.line.Draw();
	}

	// Token: 0x04002820 RID: 10272
	public Texture lineTexture;

	// Token: 0x04002821 RID: 10273
	public int segments = 60;

	// Token: 0x04002822 RID: 10274
	public int visibleLineSegments = 20;

	// Token: 0x04002823 RID: 10275
	public float speed = 60f;

	// Token: 0x04002824 RID: 10276
	private float startIndex;

	// Token: 0x04002825 RID: 10277
	private float endIndex;

	// Token: 0x04002826 RID: 10278
	private VectorLine line;
}
