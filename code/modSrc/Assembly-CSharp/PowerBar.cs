using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x0200035E RID: 862
public class PowerBar : MonoBehaviour
{
	// Token: 0x06001FAB RID: 8107 RVA: 0x0014D294 File Offset: 0x0014B494
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

	// Token: 0x06001FAC RID: 8108 RVA: 0x00014FFE File Offset: 0x000131FE
	private void SetTargetPower()
	{
		this.targetPower = UnityEngine.Random.value;
	}

	// Token: 0x06001FAD RID: 8109 RVA: 0x0014D3B8 File Offset: 0x0014B5B8
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

	// Token: 0x06001FAE RID: 8110 RVA: 0x0014D4B8 File Offset: 0x0014B6B8
	private void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width / 2 - 40), (float)(Screen.height / 2 - 15), 80f, 30f), "Power: " + (this.currentPower * 100f).ToString("f0") + "%");
	}

	// Token: 0x04002839 RID: 10297
	public float speed = 0.25f;

	// Token: 0x0400283A RID: 10298
	public int lineWidth = 25;

	// Token: 0x0400283B RID: 10299
	public float radius = 60f;

	// Token: 0x0400283C RID: 10300
	public int segmentCount = 200;

	// Token: 0x0400283D RID: 10301
	private VectorLine bar;

	// Token: 0x0400283E RID: 10302
	private Vector2 position;

	// Token: 0x0400283F RID: 10303
	private float currentPower;

	// Token: 0x04002840 RID: 10304
	private float targetPower;
}
