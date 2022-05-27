using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035D RID: 861
public class AnimatePartialLine : MonoBehaviour
{
	// Token: 0x06001FED RID: 8173 RVA: 0x0014C410 File Offset: 0x0014A610
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

	// Token: 0x06001FEE RID: 8174 RVA: 0x0014C4D0 File Offset: 0x0014A6D0
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

	// Token: 0x04002836 RID: 10294
	public Texture lineTexture;

	// Token: 0x04002837 RID: 10295
	public int segments = 60;

	// Token: 0x04002838 RID: 10296
	public int visibleLineSegments = 20;

	// Token: 0x04002839 RID: 10297
	public float speed = 60f;

	// Token: 0x0400283A RID: 10298
	private float startIndex;

	// Token: 0x0400283B RID: 10299
	private float endIndex;

	// Token: 0x0400283C RID: 10300
	private VectorLine line;
}
