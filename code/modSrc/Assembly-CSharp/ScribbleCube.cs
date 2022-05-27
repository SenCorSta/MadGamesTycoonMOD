using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

// Token: 0x02000361 RID: 865
public class ScribbleCube : MonoBehaviour
{
	// Token: 0x06001FB6 RID: 8118 RVA: 0x0014D77C File Offset: 0x0014B97C
	private void Start()
	{
		this.line = new VectorLine("Line", new List<Vector3>(this.numberOfPoints), this.lineTexture, (float)this.lineWidth, LineType.Continuous);
		this.line.material = this.lineMaterial;
		this.line.drawTransform = base.transform;
		this.LineSetup(false);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x0014D7DC File Offset: 0x0014B9DC
	private void LineSetup(bool resize)
	{
		if (resize)
		{
			this.lineColors = null;
			this.line.Resize(this.numberOfPoints);
		}
		for (int i = 0; i < this.line.points3.Count; i++)
		{
			this.line.points3[i] = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
		}
		this.SetLineColors();
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x0014D868 File Offset: 0x0014BA68
	private void SetLineColors()
	{
		if (this.lineColors == null)
		{
			this.lineColors = new List<Color32>(new Color32[this.numberOfPoints - 1]);
		}
		for (int i = 0; i < this.lineColors.Count; i++)
		{
			this.lineColors[i] = Color.Lerp(this.color1, this.color2, (float)i / (float)this.lineColors.Count);
		}
		this.line.SetColors(this.lineColors);
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x00015071 File Offset: 0x00013271
	private void LateUpdate()
	{
		this.line.Draw();
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x0014D8F0 File Offset: 0x0014BAF0
	private void OnGUI()
	{
		GUI.Label(new Rect(20f, 10f, 250f, 30f), "Zoom with scrollwheel or arrow keys");
		if (GUI.Button(new Rect(20f, 50f, 100f, 30f), "Change colors"))
		{
			int num = UnityEngine.Random.Range(0, 3);
			int num2;
			do
			{
				num2 = UnityEngine.Random.Range(0, 3);
			}
			while (num2 == num);
			this.color1 = this.RandomColor(this.color1, num);
			this.color2 = this.RandomColor(this.color2, num2);
			this.SetLineColors();
		}
		GUI.Label(new Rect(20f, 100f, 150f, 30f), "Number of points: " + this.numberOfPoints);
		this.numberOfPoints = (int)GUI.HorizontalSlider(new Rect(20f, 130f, 120f, 30f), (float)this.numberOfPoints, 50f, 1000f);
		if (GUI.Button(new Rect(160f, 120f, 40f, 30f), "Set"))
		{
			this.LineSetup(true);
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x0014DA1C File Offset: 0x0014BC1C
	private Color RandomColor(Color color, int component)
	{
		for (int i = 0; i < 3; i++)
		{
			if (i == component)
			{
				color[i] = UnityEngine.Random.value * 0.25f;
			}
			else
			{
				color[i] = UnityEngine.Random.value * 0.5f + 0.5f;
			}
		}
		return color;
	}

	// Token: 0x0400284B RID: 10315
	public Texture lineTexture;

	// Token: 0x0400284C RID: 10316
	public Material lineMaterial;

	// Token: 0x0400284D RID: 10317
	public int lineWidth = 14;

	// Token: 0x0400284E RID: 10318
	private Color color1 = Color.green;

	// Token: 0x0400284F RID: 10319
	private Color color2 = Color.blue;

	// Token: 0x04002850 RID: 10320
	private VectorLine line;

	// Token: 0x04002851 RID: 10321
	private List<Color32> lineColors;

	// Token: 0x04002852 RID: 10322
	private int numberOfPoints = 350;
}
