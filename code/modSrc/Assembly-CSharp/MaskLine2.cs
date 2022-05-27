using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000356 RID: 854
public class MaskLine2 : MonoBehaviour
{
	// Token: 0x06001F8F RID: 8079 RVA: 0x0014CA64 File Offset: 0x0014AC64
	private void Start()
	{
		this.spikeLine = new VectorLine("SpikeLine", new List<Vector3>(this.numberOfPoints), 2f, LineType.Continuous);
		float num = this.lineHeight / 2f;
		for (int i = 0; i < this.numberOfPoints; i++)
		{
			this.spikeLine.points3[i] = new Vector2(UnityEngine.Random.Range(-this.lineWidth / 2f, this.lineWidth / 2f), num);
			num -= this.lineHeight / (float)this.numberOfPoints;
		}
		this.spikeLine.color = this.lineColor;
		this.spikeLine.drawTransform = base.transform;
		this.spikeLine.SetMask(this.mask);
		this.startPos = base.transform.position;
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x0014CB44 File Offset: 0x0014AD44
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime, 360f);
		base.transform.position = new Vector2(this.startPos.x, this.startPos.y + Mathf.Cos(this.t) * 4f);
		this.spikeLine.Draw();
	}

	// Token: 0x04002811 RID: 10257
	public int numberOfPoints = 100;

	// Token: 0x04002812 RID: 10258
	public Color lineColor = Color.yellow;

	// Token: 0x04002813 RID: 10259
	public GameObject mask;

	// Token: 0x04002814 RID: 10260
	public float lineWidth = 9f;

	// Token: 0x04002815 RID: 10261
	public float lineHeight = 17f;

	// Token: 0x04002816 RID: 10262
	private VectorLine spikeLine;

	// Token: 0x04002817 RID: 10263
	private float t;

	// Token: 0x04002818 RID: 10264
	private Vector3 startPos;
}
