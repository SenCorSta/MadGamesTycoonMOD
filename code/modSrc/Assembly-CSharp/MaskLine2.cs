using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000359 RID: 857
public class MaskLine2 : MonoBehaviour
{
	// Token: 0x06001FE2 RID: 8162 RVA: 0x0014C028 File Offset: 0x0014A228
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

	// Token: 0x06001FE3 RID: 8163 RVA: 0x0014C108 File Offset: 0x0014A308
	private void Update()
	{
		this.t = Mathf.Repeat(this.t + Time.deltaTime, 360f);
		base.transform.position = new Vector2(this.startPos.x, this.startPos.y + Mathf.Cos(this.t) * 4f);
		this.spikeLine.Draw();
	}

	// Token: 0x04002827 RID: 10279
	public int numberOfPoints = 100;

	// Token: 0x04002828 RID: 10280
	public Color lineColor = Color.yellow;

	// Token: 0x04002829 RID: 10281
	public GameObject mask;

	// Token: 0x0400282A RID: 10282
	public float lineWidth = 9f;

	// Token: 0x0400282B RID: 10283
	public float lineHeight = 17f;

	// Token: 0x0400282C RID: 10284
	private VectorLine spikeLine;

	// Token: 0x0400282D RID: 10285
	private float t;

	// Token: 0x0400282E RID: 10286
	private Vector3 startPos;
}
