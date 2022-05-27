using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000355 RID: 853
public class MaskLine1 : MonoBehaviour
{
	// Token: 0x06001F8C RID: 8076 RVA: 0x0014C8EC File Offset: 0x0014AAEC
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

	// Token: 0x06001F8D RID: 8077 RVA: 0x0014C9D8 File Offset: 0x0014ABD8
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime * this.moveSpeed, 360f);
		base.transform.position = new Vector2(this.startPos.x + Mathf.Sin(this.t) * 1.5f, this.startPos.y + Mathf.Cos(this.t) * 1.5f);
		this.rectLine.Draw();
	}

	// Token: 0x0400280A RID: 10250
	public int numberOfRects = 30;

	// Token: 0x0400280B RID: 10251
	public Color lineColor = Color.green;

	// Token: 0x0400280C RID: 10252
	public GameObject mask;

	// Token: 0x0400280D RID: 10253
	public float moveSpeed = 2f;

	// Token: 0x0400280E RID: 10254
	private VectorLine rectLine;

	// Token: 0x0400280F RID: 10255
	private float t;

	// Token: 0x04002810 RID: 10256
	private Vector3 startPos;
}
