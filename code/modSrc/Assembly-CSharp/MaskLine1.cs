using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000358 RID: 856
public class MaskLine1 : MonoBehaviour
{
	// Token: 0x06001FDF RID: 8159 RVA: 0x0014BE8C File Offset: 0x0014A08C
	private void Start()
	{
		this.rectLine = new VectorLine("Rects", new List<Vector3>(this.numberOfRects * 8), 2f);
		int num = 0;
		for (int i = 0; i < this.numberOfRects; i++)
		{
			this.rectLine.MakeRect(new Rect(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(0.25f, 3f), UnityEngine.Random.Range(0.25f, 2f)), num);
			num += 8;
		}
		this.rectLine.color = this.lineColor;
		this.rectLine.capLength = 1f;
		this.rectLine.drawTransform = base.transform;
		this.rectLine.SetMask(this.mask);
		this.startPos = base.transform.position;
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x0014BF78 File Offset: 0x0014A178
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime * this.moveSpeed, 360f);
		base.transform.position = new Vector2(this.startPos.x + Mathf.Sin(this.t) * 1.5f, this.startPos.y + Mathf.Cos(this.t) * 1.5f);
		this.rectLine.Draw();
	}

	// Token: 0x04002820 RID: 10272
	public int numberOfRects = 30;

	// Token: 0x04002821 RID: 10273
	public Color lineColor = Color.green;

	// Token: 0x04002822 RID: 10274
	public GameObject mask;

	// Token: 0x04002823 RID: 10275
	public float moveSpeed = 2f;

	// Token: 0x04002824 RID: 10276
	private VectorLine rectLine;

	// Token: 0x04002825 RID: 10277
	private float t;

	// Token: 0x04002826 RID: 10278
	private Vector3 startPos;
}
