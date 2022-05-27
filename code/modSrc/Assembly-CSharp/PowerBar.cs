using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000361 RID: 865
public class PowerBar : MonoBehaviour
{
	// Token: 0x06001FFE RID: 8190 RVA: 0x0014C9BC File Offset: 0x0014ABBC
	private void Start()
	{
		this.position = new Vector2(this.radius + 20f, (float)Screen.height - (this.radius + 20f));
		VectorLine vectorLine = new VectorLine("BarBackground", new List<Vector2>(50), null, (float)this.lineWidth, LineType.Continuous, Joins.Weld);
		vectorLine.MakeCircle(this.position, this.radius);
		vectorLine.Draw();
		this.bar = new VectorLine("TotalBar", new List<Vector2>(this.segmentCount + 1), null, (float)(this.lineWidth - 4), LineType.Continuous, Joins.Weld);
		this.bar.color = Color.black;
		this.bar.MakeArc(this.position, this.radius, this.radius, 0f, 270f);
		this.bar.Draw();
		this.currentPower = UnityEngine.Random.value;
		this.SetTargetPower();
		this.bar.SetColor(Color.red, 0, (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower));
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x0014CADE File Offset: 0x0014ACDE
	private void SetTargetPower()
	{
		this.targetPower = UnityEngine.Random.value;
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x0014CAEC File Offset: 0x0014ACEC
	private void Update()
	{
		float t = this.currentPower;
		if (this.targetPower < this.currentPower)
		{
			this.currentPower -= this.speed * Time.deltaTime;
			if (this.currentPower < this.targetPower)
			{
				this.SetTargetPower();
			}
			this.bar.SetColor(Color.black, (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower), (int)Mathf.Lerp(0f, (float)this.segmentCount, t));
			return;
		}
		this.currentPower += this.speed * Time.deltaTime;
		if (this.currentPower > this.targetPower)
		{
			this.SetTargetPower();
		}
		this.bar.SetColor(Color.red, (int)Mathf.Lerp(0f, (float)this.segmentCount, t), (int)Mathf.Lerp(0f, (float)this.segmentCount, this.currentPower));
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x0014CBEC File Offset: 0x0014ADEC
	private void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width / 2 - 40), (float)(Screen.height / 2 - 15), 80f, 30f), "Power: " + (this.currentPower * 100f).ToString("f0") + "%");
	}

	// Token: 0x0400284F RID: 10319
	public float speed = 0.25f;

	// Token: 0x04002850 RID: 10320
	public int lineWidth = 25;

	// Token: 0x04002851 RID: 10321
	public float radius = 60f;

	// Token: 0x04002852 RID: 10322
	public int segmentCount = 200;

	// Token: 0x04002853 RID: 10323
	private VectorLine bar;

	// Token: 0x04002854 RID: 10324
	private Vector2 position;

	// Token: 0x04002855 RID: 10325
	private float currentPower;

	// Token: 0x04002856 RID: 10326
	private float targetPower;
}
